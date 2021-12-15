using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MakeDPCM
{
	class DpcmFile
	{
		private const int PADDING_SIZE = 0x10 * 8;
		private byte[] _data = null;
		private int _sample_pos = 0;

		public DpcmFile Clone()
		{
			DpcmFile df = new DpcmFile();
			if(_data != null) df._data = (byte[])_data.Clone();
			df._sample_pos = _sample_pos;
			return df;
		}
		public DpcmFile()
		{
		}
		~DpcmFile()
		{
			Free();
		}
		public void Free()
		{
			_data = null;
			GC.Collect();
		}
		public void CreateBuffer(int samples)
		{
			if (samples <= 0) throw new MyException("samples <= 0", samples);
			if (_data != null) Free();
			_data = new byte[samples];
			_sample_pos = 0;
		}
		public void AddSample(int s)
		{
			if (_sample_pos >= _data.Length) return;
			SetSample(_sample_pos, s);
			_sample_pos++;
		}
		public void SetSample(int n, int s)
		{
			if (n >= _data.Length) return;
			if (s == 0)
			{
				_data[n] = 0;
			}
			else
			{
				_data[n] = 1;
			}
		}
		public byte GetSample(int n)
		{
			if (n >= _data.Length) throw new MyException("n >= _data.Length", n, _data.Length);
			return _data[n];
		}
		public int GetSize()
		{
			if (_data == null) throw new MyException("_data == null");
			return _data.Length;
		}
		public int GetSsamplePos()
		{
			if (_data == null) throw new MyException("_data == null");
			return _sample_pos;
		}
		public void Save(string filename)
		{
			if (filename == null) throw new MyException("filename == null");
			if (_data == null) throw new MyException("_data == null");
			BinaryWriter bw = null;
			try
			{
				File.Delete(filename);
				bw = new BinaryWriter(File.OpenWrite(filename));
				byte buf;

				for (int i = 0; i < _data.Length; i += 8)
				{
					buf  = (byte)(_data[i + 0] * 1);
					buf += (byte)(_data[i + 1] * 2);
					buf += (byte)(_data[i + 2] * 4);
					buf += (byte)(_data[i + 3] * 8);
					buf += (byte)(_data[i + 4] * 16);
					buf += (byte)(_data[i + 5] * 32);
					buf += (byte)(_data[i + 6] * 64);
					buf += (byte)(_data[i + 7] * 128);
					bw.Write(buf);
				}
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
		public void Load(string filename)
		{
			if (filename == null) throw new MyException("filename == null");
			if (!File.Exists(filename)) throw new MyException("file not found", filename);
			if (_data != null) Free();
			byte[] buf = null;
			try
			{
				buf = File.ReadAllBytes(filename);
				CreateBuffer(buf.Length * 8);
				for (int i = 0; i < buf.Length; i++)
				{
					_data[i * 8 + 0] = (byte)((buf[i] & 0x01) >> 0);
					_data[i * 8 + 1] = (byte)((buf[i] & 0x02) >> 1);
					_data[i * 8 + 2] = (byte)((buf[i] & 0x04) >> 2);
					_data[i * 8 + 3] = (byte)((buf[i] & 0x08) >> 3);
					_data[i * 8 + 4] = (byte)((buf[i] & 0x10) >> 4);
					_data[i * 8 + 5] = (byte)((buf[i] & 0x20) >> 5);
					_data[i * 8 + 6] = (byte)((buf[i] & 0x40) >> 6);
					_data[i * 8 + 7] = (byte)((buf[i] & 0x80) >> 7);
				}
			}
			finally
			{
				buf = null;
				_sample_pos = 0;
			}
		}
	}
}
