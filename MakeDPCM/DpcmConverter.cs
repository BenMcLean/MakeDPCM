using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MakeDPCM
{
	static class DpcmConverter : Object
	{
		public const int DPCM_VALUE_BIT = 64; // (int)Math.Pow(2, n);
		public const int DPCM_VALUE_WAVE = 65536 / DPCM_VALUE_BIT;
		public const int DPCM_VALUE_MAX = DPCM_VALUE_BIT - 1;
		public const int DPCM_VALUE_MIN = 0;

		public static int DpcmValue2WaveValue(int v)
		{
			if (v < DPCM_VALUE_MIN) v = DPCM_VALUE_MIN;
			if (v > DPCM_VALUE_MAX) v = DPCM_VALUE_MAX;
			return v * DPCM_VALUE_WAVE + short.MinValue;
		}
		public static WaveFile Dpcm2Wave(DpcmFile df, double rate, int first_value, int adjust)
		{
			if (df == null) throw new MyException("df == null");
			if (rate <= 0.0) throw new MyException("rate <= 0.0", rate);
			if (first_value < DPCM_VALUE_MIN || first_value > DPCM_VALUE_MAX) throw new MyException("first_value outrange");

			WaveFile wf = new WaveFile();
			wf.CreateBuffer(df.GetSize());
			wf.Rate = rate;
			int[] pcm = Dpcm2Pcm(df, first_value);
			for (int i = 0; i < pcm.Length; i++)
			{
				wf.SetSample(i, DpcmValue2WaveValue(pcm[i]) - adjust);
			}
			pcm = null;
			return wf;
		}
		//public static WaveFile Dpcm2Wave44(DpcmFile df, double rate, int first_value)
		//{
		//    if (df == null) throw new MyException("df == null");
		//    if (rate <= 0.0) throw new MyException("rate <= 0.0", rate);
		//    if (first_value < DPCM_VALUE_MIN || first_value > DPCM_VALUE_MAX) throw new MyException("first_value outrange", first_value);

		//    WaveFile wf = new WaveFile();
		//    double Wave2Dmc_Step = 44100.0 / rate;
		//    double Dmc2Wave_Step = rate / 44100.0;
		//    int wave_sample_num = (int)((double)df.GetSize() * Wave2Dmc_Step);
		//    wf.CreateBuffer(wave_sample_num);
		//    double step_count = 0.0;
		//    int v = 0;
		//    int[] pcm = Dpcm2Pcm(df, first_value);
		//    for (int i = 0; i < wave_sample_num; i++)
		//    {
		//        if ((int)step_count < pcm.Length)
		//        {
		//            v = DpcmValue2WaveValue(pcm[(int)step_count]);
		//        }
		//        wf.SetSample(i, v);
		//        step_count += Dmc2Wave_Step;
		//    }
		//    pcm = null;
		//    return wf;
		//}
		private static int[] Dpcm2Pcm(DpcmFile df, int first_value)
		{
			if (df == null) throw new MyException("df == null");
			if (first_value < DPCM_VALUE_MIN || first_value > DPCM_VALUE_MAX) throw new MyException("first_value outrange", first_value);

			int[] pcm = new int[df.GetSize()];
			int v = first_value;
			for (int i = 0; i < df.GetSize(); i++)
			{
				if (df.GetSample(i) == 0)
				{
					v--;
					if (v < 0) v = 0;
				}
				else
				{
					v++;
					if (v > DPCM_VALUE_MAX) v = DPCM_VALUE_MAX;
				}
				pcm[i] = v;
			}
			return pcm;
		}
		public static void Resize(DpcmFile df, int size)
		{
			if (df == null) throw new MyException("df == null");
			if (size <= 0) throw new MyException("size <= 0", size);

			DpcmFile df2 = df.Clone();
			df.Free();
			df.CreateBuffer(size);
			for (int i = 0; i < df2.GetSize(); i++)
			{
				df.AddSample(df2.GetSample(i));
			}
			df2.Free();
			df2 = null;
		}
		public static void RemoveTailZero(DpcmFile df, int first_value)
		{
			if (df == null) throw new MyException("df == null");
			if (first_value < DPCM_VALUE_MIN || first_value > DPCM_VALUE_MAX) throw new MyException("first_value outrange", first_value);

			int[] pcm = Dpcm2Pcm(df, first_value);
			int i;
			for (i = pcm.Length - 1; i >= 0; i--)
			{
				if (pcm[i] != 0) break;
			}
			pcm = null;
			i++;
			Resize(df, i);
		}

		public static void PaddingByte(DpcmFile df, bool proc, byte data)
		{
			if (df == null) throw new MyException("df == null");

			int sample = df.GetSize();
			int amari = sample % 8;
			if (amari == 0) return;
			int size = sample - amari;
			if (proc) size += 8;
			Resize(df, size);
			if (proc)
			{
				if (amari >= 1) df.AddSample(data & 0x01);
				if (amari >= 2) df.AddSample(data & 0x02);
				if (amari >= 3) df.AddSample(data & 0x04);
				if (amari >= 4) df.AddSample(data & 0x08);
				if (amari >= 5) df.AddSample(data & 0x10);
				if (amari >= 6) df.AddSample(data & 0x20);
				if (amari == 7) df.AddSample(data & 0x40);
			}
		}
		public static void Padding16Byte(DpcmFile df, bool proc, byte data)
		{
			if (df == null) throw new MyException("df == null");

			PaddingByte(df, proc, data);

			int bsize = df.GetSize() / 8;
			int amari = bsize % 16;
			if (proc)
			{
				if (amari == 0) bsize += 1;
				else if (amari != 1) bsize = bsize + (16 - amari) + 1;
			}
			else
			{
				if (amari == 0) bsize -= 15;
				else if (amari != 1) bsize = bsize - amari + 1;
			}
			Resize(df, bsize * 8);
			if (proc && amari != 1)
			{
				for (int i = amari; i < 16 + 1; i++)
				{
					if (i == 1) break;
					df.AddSample(data & 0x01);
					df.AddSample(data & 0x02);
					df.AddSample(data & 0x04);
					df.AddSample(data & 0x08);
					df.AddSample(data & 0x10);
					df.AddSample(data & 0x20);
					df.AddSample(data & 0x40);
					df.AddSample(data & 0x80);
				}
			}

		}
		public static void GetMinMax(DpcmFile df, int first_value, out int min, out int max)
		{
			if (df == null) throw new MyException("df == null");
			if (first_value < DPCM_VALUE_MIN || first_value > DPCM_VALUE_MAX) throw new MyException("first_value outrange", first_value);

			min = int.MaxValue;
			max = int.MinValue;
			int[] pcm = Dpcm2Pcm(df, first_value);
			for (int i = 0; i < df.GetSize(); i++)
			{
				if (min > pcm[i]) min = pcm[i];
				if (max < pcm[i]) max = pcm[i];
			}
			pcm = null;
		}
	}
}
