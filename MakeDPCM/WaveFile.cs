using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace MakeDPCM
{
	// 16bit 44.1kHz Mono. Linear
	public class WaveFile : Object
	{
		private byte[] RIFF_HEAD =
		{
			0x52, 0x49, 0x46, 0x46, // RIFF
			0x00, 0x00, 0x00, 0x00, // file size
			0x57, 0x41, 0x56, 0x45, // WAVE
			0x66, 0x6D, 0x74, 0x20, // fmt 
			0x10, 0x00, 0x00, 0x00, // fmt size = 16
			0x01, 0x00,             // fmt id   = 1 Linear
			0x01, 0x00,             // fmt ch   = 1 mono
			0x44, 0xac, 0x00, 0x00, // fmt hz   = 44100
			0x88, 0x58, 0x01, 0x00, // fmt rate = 88200
			0x02, 0x00,             // fmt block size = 2 byte
			0x10, 0x00,             // fmt bit size = 16
			0x64, 0x61, 0x74, 0x61  // data
		};

		private byte[] HEAD_RIFF = { 0x52, 0x49, 0x46, 0x46 };
		private byte[] HEAD_WAVE = { 0x57, 0x41, 0x56, 0x45 };
		private byte[] CHUNK_fmt_ = { 0x66, 0x6D, 0x74, 0x20 };
		private byte[] CHUNK_fact = { 0x66, 0x61, 0x63, 0x74 };
		private byte[] CHUNK_data = { 0x64, 0x61, 0x74, 0x61 };
		private byte[] CHUNK_LIST = { 0x4C, 0x49, 0x53, 0x54 };

		private const int CH_MONO = 1;
		private const int CH_STEREO = 2;
		private const int ID_PCM = 1;
		private const int BIT_16 = 16;
		private const int BIT_8 = 8;

		private struct WaveFormat
		{
			public int ID;
			public int Ch;
			public int Hz;
			public int Rate;
			public int BlockSize;
			public int BitSize;
		}
		private WaveFormat _fmt = new WaveFormat();

		public enum ConvertType : int { None, Left, Right, Center, Cancel };
		private ConvertType _conv_type = ConvertType.None;

		private int[] _data = null;
		public int Max { set; get; }
		public int Min { set; get; }
		public int MaxIndex { set; get; }
		public int MinIndex { set; get; }
		public double Rate { set; get; }

		public WaveFile Clone()
		{
			WaveFile wf = new WaveFile();
			wf.Min = Min;
			wf.Max = Max;
			wf.MinIndex = MinIndex;
			wf.MaxIndex = MaxIndex;
			wf.Rate = Rate;
			wf._fmt = _fmt;
			if(_data != null) wf._data = (int[])_data.Clone();
			return wf;
		}
		public WaveFile()
		{
			Max = 0;
			Min = 0;
			MaxIndex = 0;
			MinIndex = 0;
			Rate = 44100.0;
		}
		~WaveFile()
		{
			Free();
		}
		public void Free()
		{
			_data = null;
			GC.Collect();
		}
		public void CreateBuffer(int sample_num)
		{
			if (sample_num <= 0) throw new MyException("sample_num <= 0", sample_num);
			if (_data != null) Free();
			_data = new int[sample_num];
		}
		public bool IsLoaded
		{
			get
			{
				return _data != null;
			}
		}
		public int[] GetBuffer()
		{
			return _data;
		}
		public void SetBuffer(int[] buf)
		{
			if (buf == null) throw new MyException("buf == null");
			_data = null;
			_data = buf;
		}
		public int GetSize()
		{
			if (_data == null) throw new MyException("_data == null");
			return _data.Length;
		}
		public int GetSample(int n)
		{
			if (_data == null) throw new MyException("_data == null");
			if (n < 0) return 0;
			if (n >= _data.Length) return 0;
			return _data[n];
		}
		public void SetSample(int n, int s)
		{
			if (_data == null) throw new MyException("_data == null");
			if (n < 0) return;
			if (n >= _data.Length) return;
			_data[n] = s;
		}
		public double getSecond()
		{
			return (double)this.GetSize() / this.Rate;
		}
		public void SearchMinMax()
		{
			if (_data == null) throw new MyException("_data == null");
			Max = short.MinValue;
			Min = short.MaxValue;
			for (int i = 0; i < _data.Length; i++)
			{
				if (Max < _data[i])
				{
					Max = _data[i];
					MaxIndex = i;
				}
				if (Min > _data[i])
				{
					Min = _data[i];
					MinIndex = i;
				}
			}
		}
		private bool CompareBytes(byte[] buf1, byte[] buf2)
		{
			for (int i = 0; i < buf2.Length; i++)
			{
				if (buf1[i] != buf2[i]) return false;
			}
			return true;
		}
		public bool CheckFormat(string filename, bool showDlg)
		{
			return LoadOrCheckFormat(filename, true, showDlg);
		}
		public bool Load(string filename)
		{
			return LoadOrCheckFormat(filename, false, false);
		}
		private bool LoadOrCheckFormat(string filename, bool CheckOnly, bool showDlg)
		{
			if (filename == null) throw new MyException("filename == null");
			if (!File.Exists(filename)) throw new MyException("file not found", filename);
			if (_data != null) Free();
			BinaryReader br = null;
			try
			{
				br = new BinaryReader(File.OpenRead(filename));

				// RIFF header
				byte[] buf;
				buf = br.ReadBytes(4);
				if(!CompareBytes(buf, HEAD_RIFF)) throw new MyException("not RIFF file.");
				int filesize = br.ReadInt32();

				int readsize = 0;

				// WAVE header
				buf = br.ReadBytes(4);
				if (!CompareBytes(buf, HEAD_WAVE)) throw new MyException("not WAVE file.");
				readsize += 4;
				
				// WAVE Chunks
				WaveFormat fmt = new WaveFormat();
				byte[] chunk_type;
				int chunk_size;
				while(readsize < filesize)
				{
					chunk_type = br.ReadBytes(4);
					chunk_size = br.ReadInt32();
					readsize += 8;
					if (CompareBytes(chunk_type, CHUNK_fmt_))
					{
						readsize += readChunkFormat(br, ref fmt);
						if (CheckOnly)
						{
							if (fmt.ID != 1) return false;
							if (fmt.Ch != CH_MONO && fmt.Ch != CH_STEREO) return false;
							if (fmt.BitSize != BIT_8 && fmt.BitSize != BIT_16) return false;

							if (showDlg)
							{
								if(!checkConvertType(fmt, out _conv_type, filename)) return false;
							}
							else
							{
								_conv_type = ConvertType.Left;
							}
							return true;
						}
					}
					else if (CompareBytes(chunk_type, CHUNK_data) && !CheckOnly)
					{
						int ret = readChunkData(br, ref fmt, chunk_size, out _data, _conv_type);
						if (ret == 0) return false;
						readsize += ret;
					}
					else
					{
						readsize += chunk_size;
						br.BaseStream.Seek((uint)chunk_size, SeekOrigin.Current);
					}
				}
				_fmt = fmt;
				this.Rate = (double)_fmt.Hz;
				SearchMinMax();
			}
			finally
			{
				if (br != null)
				{
					br.Close();
					br = null;
				}
			}
			return true;
		}
		private int readChunkFormat(BinaryReader br, ref WaveFormat fmt)
		{
			fmt.ID = br.ReadInt16();
			fmt.Ch = br.ReadInt16();
			fmt.Hz = br.ReadInt32();
			fmt.Rate = br.ReadInt32();
			fmt.BlockSize = br.ReadInt16();
			fmt.BitSize = br.ReadInt16();
			return 16;
		}
		private int readChunkData(BinaryReader br, ref WaveFormat fmt, int data_size, out int[] buf, ConvertType ct)
		{
			buf = null;

			if (ct == ConvertType.Cancel) return 0;

			if (fmt.Ch == CH_MONO && fmt.BitSize == BIT_16)
			{
				buf = new int[data_size / (1 * 2)];
				for (int i = 0; i < buf.Length; i++)
				{
					buf[i] = br.ReadInt16();
				}
			}
			else if (fmt.Ch == CH_MONO && fmt.BitSize == BIT_8)
			{
				buf = new int[data_size / (1 * 1)];
				for (int i = 0; i < buf.Length; i++)
				{
					buf[i] = ((int)br.ReadByte() - 128) * 256;
				}
			}
			else if (fmt.Ch == CH_STEREO && fmt.BitSize == BIT_16)
			{
				buf = new int[data_size / (2 * 2)];
				if (ct == ConvertType.Left)
				{
					for (int i = 0; i < buf.Length; i++)
					{
						buf[i] = br.ReadInt16();
						br.BaseStream.Seek(2, SeekOrigin.Current);
					}
				}
				else if (ct == ConvertType.Right)
				{
					for (int i = 0; i < buf.Length; i++)
					{
						br.BaseStream.Seek(2, SeekOrigin.Current);
						buf[i] = br.ReadInt16();
					}
				}
				else
				{
					for (int i = 0; i < buf.Length; i++)
					{
						buf[i] = (br.ReadInt16() + br.ReadInt16()) / 2;
					}
				}
			}
			else if (fmt.Ch == CH_STEREO && fmt.BitSize == BIT_8)
			{
				buf = new int[data_size / (2 * 1)];
				if (ct == ConvertType.Left)
				{
					for (int i = 0; i < buf.Length; i++)
					{
						buf[i] = ((int)br.ReadByte() - 128) * 256;
						br.BaseStream.Seek(1, SeekOrigin.Current);
					}
				}
				else if (ct == ConvertType.Right)
				{
					for (int i = 0; i < buf.Length; i++)
					{
						br.BaseStream.Seek(1, SeekOrigin.Current);
						buf[i] = ((int)br.ReadByte() - 128) * 256;
					}
				}
				else
				{
					for (int i = 0; i < buf.Length; i++)
					{
						buf[i] = ((int)br.ReadByte() - 128 + br.ReadByte() - 128) / 2 * 256;
					}
				}
			}
			else
			{
				return 0;
			}

			fmt.Ch = CH_MONO;
			fmt.BitSize = BIT_16;
			fmt.BlockSize = 1 * 2;
			fmt.Rate = fmt.Hz * fmt.BlockSize;
			return data_size;
		}
		private bool checkConvertType(WaveFormat fmt, out ConvertType cnvtype, string filename)
		{
			cnvtype = ConvertType.None;
			if (fmt.Ch == CH_MONO && fmt.BitSize == BIT_16) return true;

			string fmtstr = "";
			if (fmt.Ch == CH_MONO) fmtstr += "Mono ";
			else if (fmt.Ch == CH_STEREO) fmtstr += "Stereo ";
			if (fmt.BitSize == BIT_8) fmtstr += "8bit";
			else if (fmt.BitSize == BIT_16) fmtstr += "16bit";

			string msg = "";
			msg += filename + Environment.NewLine + Environment.NewLine;
			msg += "This wav file format \"" + fmtstr + "\"" + Environment.NewLine;
			msg += "It will be convert to \"Mono 16bit\"." + Environment.NewLine;
			msg += "Please select convert type.";

			WaveConvertForm wcf = new WaveConvertForm(msg);
			wcf.ShowDialog();
			if (wcf.DialogResult == DialogResult.Cancel) return false;
			cnvtype = wcf.Result;
			return true;
		}
		public byte[] GetFileByteArray()
		{
			if (_data == null) throw new MyException("_data == null");

			int data_size = _data.Length * 2;
			int file_size = RIFF_HEAD.Length - 8 + 4 + data_size;

			byte[] buf = new byte[file_size + 8];

			Array.Copy(RIFF_HEAD, buf, RIFF_HEAD.Length);

			buf[RIFF_HEAD.Length + 0] = (byte)((data_size & 0x000000FF) >> 8 * 0);
			buf[RIFF_HEAD.Length + 1] = (byte)((data_size & 0x0000FF00) >> 8 * 1);
			buf[RIFF_HEAD.Length + 2] = (byte)((data_size & 0x00FF0000) >> 8 * 2);
			buf[RIFF_HEAD.Length + 3] = (byte)((data_size & 0xFF000000) >> 8 * 3);

			short s;
			for (int i = 0; i < _data.Length; i++)
			{
				if (_data[i] < short.MinValue) s = short.MinValue;
				else if (_data[i] > short.MaxValue) s = short.MaxValue;
				else s = (short)_data[i];
				buf[RIFF_HEAD.Length + 4 + i * 2 + 0] = (byte)((s & 0x00FF) >> 8 * 0);
				buf[RIFF_HEAD.Length + 4 + i * 2 + 1] = (byte)((s & 0xFF00) >> 8 * 1);
			}

			buf[4 + 0] = (byte)((file_size & 0x000000FF) >> 8 * 0);
			buf[4 + 1] = (byte)((file_size & 0x0000FF00) >> 8 * 1);
			buf[4 + 2] = (byte)((file_size & 0x00FF0000) >> 8 * 2);
			buf[4 + 3] = (byte)((file_size & 0xFF000000) >> 8 * 3);

			int r = (int)Rate;
			buf[0x18 + 0] = (byte)((r & 0x000000FF) >> 8 * 0);
			buf[0x18 + 1] = (byte)((r & 0x0000FF00) >> 8 * 1);
			buf[0x18 + 2] = (byte)((r & 0x00FF0000) >> 8 * 2);
			buf[0x18 + 3] = (byte)((r & 0xFF000000) >> 8 * 3);

			r *= 2;
			buf[0x1c + 0] = (byte)((r & 0x000000FF) >> 8 * 0);
			buf[0x1c + 1] = (byte)((r & 0x0000FF00) >> 8 * 1);
			buf[0x1c + 2] = (byte)((r & 0x00FF0000) >> 8 * 2);
			buf[0x1c + 3] = (byte)((r & 0xFF000000) >> 8 * 3);

			return buf;
		}
		public void Save(string filename)
		{
			if (filename == null) throw new MyException("filename == null");
			if (_data == null) throw new MyException("_data == null");
			BinaryWriter bw = null;
			try
			{
				bw = new BinaryWriter(File.OpenWrite(filename));

				byte[] buf = new byte[4];
				int data_size = _data.Length * 2;
				int file_size = RIFF_HEAD.Length - 8 + 4 + data_size;

				bw.Write(RIFF_HEAD);

				buf[0] = (byte)((data_size & 0x000000FF) >> 8 * 0);
				buf[1] = (byte)((data_size & 0x0000FF00) >> 8 * 1);
				buf[2] = (byte)((data_size & 0x00FF0000) >> 8 * 2);
				buf[3] = (byte)((data_size & 0xFF000000) >> 8 * 3);
				bw.Write(buf, 0, 4);

				short s;
				for (int i = 0; i < _data.Length; i++)
				{
					if (_data[i] < short.MinValue) s = short.MinValue;
					else if (_data[i] > short.MaxValue) s = short.MaxValue;
					else s = (short)_data[i];
					buf[0] = (byte)((s & 0x00FF) >> 8 * 0);
					buf[1] = (byte)((s & 0xFF00) >> 8 * 1);
					bw.Write(buf, 0, 2);
				}

				buf[0] = (byte)((file_size & 0x000000FF) >> 8 * 0);
				buf[1] = (byte)((file_size & 0x0000FF00) >> 8 * 1);
				buf[2] = (byte)((file_size & 0x00FF0000) >> 8 * 2);
				buf[3] = (byte)((file_size & 0xFF000000) >> 8 * 3);
				bw.Seek(4, SeekOrigin.Begin);
				bw.Write(buf, 0, 4);

				int r = (int)Rate;
				buf[0] = (byte)((r & 0x000000FF) >> 8 * 0);
				buf[1] = (byte)((r & 0x0000FF00) >> 8 * 1);
				buf[2] = (byte)((r & 0x00FF0000) >> 8 * 2);
				buf[3] = (byte)((r & 0xFF000000) >> 8 * 3);
				bw.Seek(0x18, SeekOrigin.Begin);
				bw.Write(buf, 0, 4);

				r *= 2;
				buf[0] = (byte)((r & 0x000000FF) >> 8 * 0);
				buf[1] = (byte)((r & 0x0000FF00) >> 8 * 1);
				buf[2] = (byte)((r & 0x00FF0000) >> 8 * 2);
				buf[3] = (byte)((r & 0xFF000000) >> 8 * 3);
				bw.Seek(0x1C, SeekOrigin.Begin);
				bw.Write(buf, 0, 4);
			}
			finally
			{
				if (bw != null)
				{
					bw.Close();
					bw = null;
				}
			}
		}
	}
}
