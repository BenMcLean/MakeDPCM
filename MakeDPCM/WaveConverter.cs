using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeDPCM
{
	static class WaveConverter : Object
	{
		private const double PI = 3.14159265358979;
		public static void Normalize(WaveFile wf)
		{
			if (wf == null) throw new MyException("wf == null");
			wf.SearchMinMax();
			double upper = Math.Abs((double)short.MaxValue / ((wf.Max == 0) ? 1 : wf.Max));
			double lower = Math.Abs((double)short.MinValue / ((wf.Min == 0) ? 1 : wf.Min));
			Volume(wf, Math.Min(upper, lower));
		}
		public static void Volume(WaveFile wf, double amp)
		{
			if (wf == null) throw new MyException("wf == null");
			for (int i = 0; i < wf.GetSize(); i++)
			{
				wf.SetSample(i, (int)((double)wf.GetSample(i) * amp));
			}
		}
		public static void Bias(WaveFile wf, int bias)
		{
			if (wf == null) throw new MyException("wf == null");
			int v = 0;
			for (int i = 0; i < wf.GetSize(); i++)
			{
				v = wf.GetSample(i) + bias;
				wf.SetSample(i, v);
			}
		}
		public static void Sub(WaveFile wf1, WaveFile wf2)
		{
			if (wf1 == null) throw new MyException("wf1 == null");
			if (wf2 == null) throw new MyException("wf2 == null");
			if (wf1.GetSize() != wf2.GetSize()) throw new MyException("not match size", wf1.GetSize(), wf2.GetSize());
			int v1;
			int v2;
			for (int i = 0; i < wf1.GetSize(); i++)
			{
				v1 = wf1.GetSample(i);
				v2 = wf2.GetSample(i);
				v1 -= v2;
				wf1.SetSample(i, v1);
			}
		}
		public static void Add(WaveFile wf1, WaveFile wf2)
		{
			if (wf1 == null) throw new MyException("wf1 == null");
			if (wf2 == null) throw new MyException("wf2 == null");
			if (wf1.GetSize() != wf2.GetSize()) throw new MyException("not match size", wf1.GetSize(), wf2.GetSize());

			int v1;
			int v2;
			for (int i = 0; i < wf1.GetSize(); i++)
			{
				v1 = wf1.GetSample(i);
				v2 = wf2.GetSample(i);
				v1 += v2;
				wf1.SetSample(i, v1);
			}
		}
		public static void Multi(WaveFile wf1, WaveFile wf2, double mag)
		{
			if (wf1 == null) throw new MyException("wf1 == null");
			if (wf2 == null) throw new MyException("wf2 == null");
			if (wf1.GetSize() != wf2.GetSize()) throw new MyException("not match size", wf1.GetSize(), wf2.GetSize());

			int v1;
			int v2;
			for (int i = 0; i < wf1.GetSize(); i++)
			{
				v1 = wf1.GetSample(i);
				v2 = wf2.GetSample(i);
				if (v2 > 0)
				{
					v1 = (int)((double)v1 * (((double)v2 / (double)short.MaxValue) * (mag - 1.0) + 1.0));
				}
				else
				{
					v1 = (int)((double)v1 * (1.0 - (double)v2 / (double)short.MinValue));
				}
				wf1.SetSample(i, v1);
			}
		}
		public static void Trimming(WaveFile wf, int start, int length)
		{
			if (wf == null) throw new MyException("wf == null");
			if (start < 0) throw new MyException("start < 0");
			if (length < 0) throw new MyException("length <= 0");
			if (length == 0) return;
			if (wf.GetSize() - 1 < start) return;
			if (wf.GetSize() < (start + length)) length = wf.GetSize() - start;

			WaveFile wf2 = wf.Clone();
			wf.Free();
			wf.CreateBuffer(length);

			for (int i = 0; i < length; i++)
			{
				wf.SetSample(i, wf2.GetSample(start + i));
			}
			wf2.Free();
			wf2 = null;
		}
		public static void Remove(WaveFile wf, int start, int length)
		{
			if (wf == null) throw new MyException("wf == null");
			if (start < 0) throw new MyException("start < 0");
			if (length < 0) throw new MyException("length <= 0");
			if (length == 0) return;
			if (wf.GetSize() - 1 < start) return;
			if (wf.GetSize() < (start + length)) length = wf.GetSize() - start;
		
			int size = wf.GetSize() - length;
			WaveFile wf2 = wf.Clone();
			wf.Free();
			wf.CreateBuffer(size);

			int ni = 0;
			for (int i = 0; i < wf.GetSize(); i++)
			{
				if (i >= start)
				{
					wf.SetSample(ni, wf2.GetSample(i));
					ni++;
					if (ni >= size) break;
				}
			}
			wf2.Free();
			wf2 = null;
		}
		public static void HeadTailToSilence(WaveFile wf, short value)
		{
			if (wf == null) throw new MyException("wf == null");
			if (value <= 0) throw new MyException("value <= 0", value);

			int head_inedx = 0;
			int v;
			int i;
			for (i = 0; i < wf.GetSize() - 1; i++)
			{
				v = wf.GetSample(i);
				if (Math.Abs(v) > value) break;
				wf.SetSample(i, 0);
			}
			head_inedx = i;
			for (i = wf.GetSize() - 1; i >= 0; i--)
			{
				v = wf.GetSample(i);
				if (Math.Abs(v) > value) break;
				wf.SetSample(i, 0);
			}
		}
		public static void HeadTailRemoveSilence(WaveFile wf, short value, out int head_inedx, out int tail_inedx)
		{
			head_inedx = 0;
			tail_inedx = 0;
	
			if (wf == null) throw new MyException("wf == null");
			if (value <= 0) throw new MyException("value <= 0", value);

			int v;
			int i;
			for (i = 0; i < wf.GetSize(); i++)
			{
				v = wf.GetSample(i);
				head_inedx = i;
				if (Math.Abs(v) > value) break;
			}
			for (i = wf.GetSize() - 1; i >= 0; i--)
			{
				v = wf.GetSample(i);
				tail_inedx = i;
				if (Math.Abs(v) > value) break;
			}
			int leng = tail_inedx - head_inedx + 1;
			if (leng <= 0) throw new MyException("leng <= 0", leng);

			WaveFile wf2 = wf.Clone();
			wf.Free();
			wf.CreateBuffer(leng);
			for (i = head_inedx; i <= tail_inedx; i++)
			{
				wf.SetSample(i - head_inedx, wf2.GetSample(i));
			}
			wf2.Free();
			wf2 = null;
		}
		public static void Joint(WaveFile wf1, WaveFile wf2)
		{
			if (wf1 == null) throw new MyException("wf1 == null");
			if (wf2 == null) throw new MyException("wf2 == null");
			WaveFile wf3 = wf1.Clone();
			wf1.Free();
			wf1.CreateBuffer(wf3.GetSize() + wf2.GetSize());
			for (int i = 0; i < wf3.GetSize(); i++)
			{
				wf1.SetSample(i, wf3.GetSample(i));
			}
			for (int i = 0; i < wf2.GetSize(); i++)
			{
				wf1.SetSample(wf3.GetSize() + i, wf2.GetSample(i));
			}
			wf3.Free();
			wf3 = null;
		}
		public static void SMA(WaveFile wf, int length)
		{
			if (wf == null) throw new MyException("wf == null");
			if (length <= 0) throw new MyException("length <= 0", length);

			WaveFile wf2 = wf.Clone();

			Parallel.For(0, wf.GetSize(), (n) =>
			{
				int sum = 0;
				int hit = 0;
				int v;
				for (int i = 0; i < length; i++)
				{
					v = (int)wf2.GetSample(i - length / 2 + n);
					if (v < 0)
					{
						sum += v;
						hit++;
					}
				}
				if (hit == 0)
				{
					wf.SetSample(n, 0);
				}
				else
				{
					wf.SetSample(n, (int)((double)sum / (double)hit));
				}
			});
			wf2.Free();
			wf2 = null;
		}
		public static void WMA(WaveFile wf, int length)
		{
			SMA(wf, length);
			SMA(wf, length);
		}
		public static void EMA(WaveFile wf, int length)
		{
			if (wf == null) throw new MyException("wf == null");
			if (length <= 0) throw new MyException("length <= 0", length);

			WaveFile wf2 = wf.Clone();
			double buf = 0;
			for (int i = 0; i < wf.GetSize(); i++)
			{
				buf += (double)(wf2.GetSample(i) - wf2.Min);
				wf.SetSample(i, (int)(buf / (double)length));
				buf *= (1.0 - 1.0 / length);
			}
			wf2.Free();
			wf2 = null;
		}
		public static void LPF2(WaveFile wf, int n)
		{
			if (n <= 0) return;
			WaveFile wf2 = wf.Clone();
			for (int i = n; i < wf.GetSize() - n; i++)
			{
				wf.SetSample(i, (wf2.GetSample(i-n)/n/4 + wf2.GetSample(i)/2 + wf2.GetSample(i+n)/n/4));
			}
		}
		public static void LPF(WaveFile wf, double freq, int n)
		{
			if (wf == null) return;
			if (freq <= 0.0) return;

			double fe = freq / wf.Rate;
			if (n < 3) n = 3;
			if ((n & 1) == 0) n++;
			double[] h = new double[n];
			for (int m = 0; m < n; m++)
			{
				h[m] = 2.0 * fe * sinc(2.0 * PI * fe * (double)(m - n / 2));
				h[m] *= (0.5 - 0.5 * Math.Cos(2.0 * PI * ((double)m / (double)n)));
			}
			int[] buf1 = wf.GetBuffer();
			int[] buf2 = new int[buf1.Length];
			double v;
			int length = wf.GetSize();
			int n2 = n / 2;
			int index;
			for (int i = 0; i < length; i++)
			{
				v = 0.0;
				for (int m = 0; m < n; m++)
				{
					index = i + (m - n2);
					if (index >= 0 && index < length)
					{
						v += buf1[i + (m - n2)] * h[m];
					}
				}
				buf2[i] = (int)v;
			}
			wf.SetBuffer(buf2);
		}
		private static double sinc(double v)
		{
			if (v == 0.0) return 1.0;
			return Math.Sin(v) / v;
		}
		public static WaveFile Stretch(WaveFile wf, double n, bool flgRate)
		{
			if (wf == null) throw new MyException("wf == null");
			if (n <= 0.0) throw new MyException("n <= 0.0");
			WaveFile wf2 = new WaveFile();
			wf2.CreateBuffer((int)(wf.GetSize() * n));
			if (flgRate) wf2.Rate = wf.Rate * n;
			double step = 1.0 / n;
			double c = 0.0;
			for (int i = 0; i < wf2.GetSize(); i++)
			{
				wf2.SetSample(i, wf.GetSample((int)c));
				c += step;
			}
			return wf2;
		}
		public static WaveFile WaveRateConvert(WaveFile wf1, double rate, int num)
		{
			if (wf1 == null) throw new MyException("wf == null");
			if (rate <= 0.0) throw new MyException("rate <= 0.0");
			if (num <= 0.0) throw new MyException("num <= 0");

			if (wf1.Rate == rate) return wf1.Clone();

			double h1 = rate / wf1.Rate;
			double h2 = wf1.Rate / rate;

			double dms = 0.0;
			if (wf1.Rate > rate)
			{
				dms = 1.0;
			}
			else
			{
				double r2 = rate;
				double i = 2.0;
				r2 = rate / i;
				while (wf1.Rate < r2)
				{
					i += 1.0;
					r2 = rate / i;
				}
				dms = 1.0 / i;
			}

			WaveFile wf2 = new WaveFile();
			wf2.CreateBuffer((int)((double)wf1.GetSize() * h1));
			wf2.Rate = rate;

			int[] buf1 = wf1.GetBuffer();
			int[] buf2 = wf2.GetBuffer();

			double v;
			double dm;
			double di = 0.0;
			double sv;
			int size = wf2.GetSize();
			int i1;
			int i2;
			double ph = PI * h1;
			for (int i = 0; i < size; i++)
			{
				v = 0.0;
				dm = dms;
				for (int m = 1; m < num; m++)
				{
					sv = ph * dm;
					i1 = (int)(di + dm);
					i2 = (int)(di - dm);
					if (i1 >= wf1.GetSize())
					{
						v += (double)buf1[i2] * Math.Sin(sv) / sv;
					}
					else if (i2 < 0)
					{
						v += (double)buf1[i1] * Math.Sin(sv) / sv;
					}
					else
					{
						v += (double)(buf1[i1] + buf1[i2]) * Math.Sin(sv) / sv;
					}
					dm += dms;
				}
				v += (double)buf1[(int)di];
				di += h2;
				buf2[i] = (int)(v * h1 * dms);
			}
			return wf2;
		}
		public static int WaveValue2DpcmValue(int v)
		{
			if (v < short.MinValue) v = short.MinValue;
			if (v > short.MaxValue) v = short.MaxValue;
			return (v - short.MinValue) / DpcmConverter.DPCM_VALUE_WAVE;
		}
		public enum Wave2DpcmType : int { WaveRateConvert, Stretch };
		public static DpcmFile Wave2Dpcm(Wave2DpcmType type, WaveFile wf, double rate, int first_value, int adjust)
		{
			if (wf == null) throw new MyException("wf == null");
			if (rate <= 0.0) throw new MyException("rate <= 0.0", rate);
			if (first_value < DpcmConverter.DPCM_VALUE_MIN || first_value > DpcmConverter.DPCM_VALUE_MAX) throw new MyException("first_value outrange", first_value);

			WaveFile wf2 = null;
			if (type == Wave2DpcmType.WaveRateConvert) wf2 = WaveRateConvert(wf, rate, 10);
			else if (type == Wave2DpcmType.Stretch) wf2 = Stretch(wf, rate / wf.Rate, true);
			else return null;

			DpcmFile df = new DpcmFile();
			df.CreateBuffer(wf2.GetSize());

			int d = first_value;
			int wv;
			int wd;
			for (int i = 0; i < df.GetSize(); i++)
			{
				wv = wf2.GetSample(i) + adjust;
				wd = DpcmConverter.DpcmValue2WaveValue(d);
				if (wd <= wv)
				{
					df.AddSample(1);
					d++;
					if (d > DpcmConverter.DPCM_VALUE_MAX) d = DpcmConverter.DPCM_VALUE_MAX;
				}
				else
				{
					df.AddSample(0);
					d--;
					if (d < 0) d = 0;
				}
			}
			return df;
		}
		public static DpcmFile Wave2DpcmOld(WaveFile wf, double rate, int first_value, int adjust)
		{
			if (wf == null) throw new MyException("wf == null");
			if (rate <= 0.0) throw new MyException("rate <= 0.0", rate);
			if (first_value < DpcmConverter.DPCM_VALUE_MIN || first_value > DpcmConverter.DPCM_VALUE_MAX) throw new MyException("first_value outrange", first_value);

			double Wave2Dmc_Step = wf.Rate / rate;
			double Dmc2Wave_Step = rate / wf.Rate;
			int dpcm_sample_num = (int)((double)wf.GetSize() * Dmc2Wave_Step);
			DpcmFile df = new DpcmFile();
			df.CreateBuffer(dpcm_sample_num);
			double step_count = 0.0;
			int d = first_value;
			int wv;
			int wd;
			while (step_count <= (double)wf.GetSize())
			{
				wv = wf.GetSample((int)step_count) + adjust;
				wd = DpcmConverter.DpcmValue2WaveValue(d);
				if (wd < wv)
				{
					df.AddSample(1);
					d++;
					if (d > DpcmConverter.DPCM_VALUE_MAX) d = DpcmConverter.DPCM_VALUE_MAX;
				}
				else
				{
					df.AddSample(0);
					d--;
					if (d < 0) d = 0;
				}
				step_count += Wave2Dmc_Step;
			}
			return df;
		}
	}
}
