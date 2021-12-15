using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace System.Diagnostics
{
	class StopwatchPeriod : Stopwatch
	{
		private double[] _data = null;
		private int _count;
		public StopwatchPeriod(int n)
		{
			CreateBuffer(n);
			ResetPeriod();
		}
		public void CreateBuffer(int n)
		{
			_data = null;
			_data = new double[n];
		}
		public void ResetPeriod()
		{
			for (int i = 0; i < _data.Length; i++)
			{
				_data[i] = 0.0;
			}
			_count = 0;
		}
		public double Millisecond
		{
			get
			{
				return (double)this.ElapsedTicks / (double)Stopwatch.Frequency * 1000.0;
			}
		}
		public void Period()
		{
			if (_data == null) return;
			_data[_count] = Millisecond;
			_count++;
			if (_count >= _data.Length)
			{
				_count = 0;
			}
		}
		public double[] GetPeriod()
		{
			return (double[])_data.Clone();
		}
		public double GetPeriod(int n)
		{
			if (_data == null) return 0.0;
			if (n >= _data.Length) return 0.0;
			return _data[n];
		}
		public string GetPeriodString()
		{
			string str = "";
			for (int i = 0; i < _count; i++)
			{
				str += _data[i].ToString("F3");
				str += " ";
				if (i != 0 && i % 16 == 0) str += Environment.NewLine;
			}
			return str;
		}
		public string GetPeriodString(int n)
		{
			return GetPeriod(n).ToString("F3");
		}
	}
}
