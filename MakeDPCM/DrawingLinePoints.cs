using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace MakeDPCM
{
	public class DrawingLinePoints// : IComparer<Point>
	{
		private class DrawingLineItem
		{
			public Point sample;
			public double PicX;
			public double PicY;
			public bool Visible;

			public DrawingLineItem(int x, int y)
			{
				sample = new Point(x, y);
			}
			public DrawingLineItem(Point p)
			{
				sample = p;
			}
		}

		// Const
		public const int HIT_RADIUS = 5;

		// Member
		private List<DrawingLineItem> _list = new List<DrawingLineItem>();

		// Property
		public Color color { set; get; }
		public bool Visible { set; get; }
		public Point this[int index]
		{
			get { return _list[index].sample; }
			set { _list[index].sample = value; }
		}
		public int Count
		{
			get { return _list.Count; }
		}

		// Constructor
		public DrawingLinePoints()
		{
			ClearPoints();
			color = Color.Black;
		}

		// points function
		public void SortPoints()
		{
			for (int m = 0; m < _list.Count(); m++)
			{
				for (int n = 1; n < _list.Count() - m; n++)
				{
					if (_list[n - 1].sample.X > _list[n].sample.X)
					{
						DrawingLineItem t = _list[n - 1];
						_list[n - 1] = _list[n];
						_list[n] = t;
					}
				}
			}
		}
		public void ClearPoints()
		{
			_list.Clear();
			_list.Add(new DrawingLineItem(0, 0));
			_list.Add(new DrawingLineItem(1, 0));
		}
		public void AddPoint(int x, int y)
		{
			AddPoint(new Point(x, y));
		}
		public void AddPoint(Point p)
		{
			if (_list.Count() >= 2 && _list[_list.Count() - 1].sample.X <= p.X) return;
			if (_list.Count() >= 2 && _list[0].sample.X >= p.X) return;
			_list.Add(new DrawingLineItem(p));
			SortPoints();
		}
		public void AddPoint(Point p, int pic_x, int pic_y)
		{
			if (_list.Count() >= 2 && _list[_list.Count() - 1].sample.X <= p.X) return;
			if (_list.Count() >= 2 && _list[0].sample.X >= p.X) return;
			DrawingLineItem di = new DrawingLineItem(p);
			di.PicX = pic_x;
			di.PicY = pic_y;
			di.Visible = true;
			_list.Add(di);
			SortPoints();
		}
		public Point[] GetPointArray()
		{
			Point[] p = new Point[_list.Count];
			for (int i = 0; i < p.Length; i++)
			{
				p[i] = new Point(_list[i].sample.X, _list[i].sample.Y);
			}
			return p;
		}
		private DrawingLineItem[] PointArrayToItemArray(Point[] p)
		{
			DrawingLineItem[] dlp = new DrawingLineItem[p.Length];
			for (int i = 0; i < p.Length; i++)
			{
				dlp[i] = new DrawingLineItem(p[i]);
			}
			return dlp;
		}
		public void ClearAndSetPointRange(Point[] p)
		{
			if (p == null) return;
			_list.Clear();
			_list.AddRange(PointArrayToItemArray(p));
			SortPoints();
		}
		public void MovePoint(int i, int x, int y)
		{
			if (i < 0) return;
			if (i >= _list.Count) return;
			if (i == 0)
			{
				_list[i].sample = new Point(0, y);
				return;
			}
			if (i == _list.Count - 1)
			{
				_list[i].sample = new Point(_list[i].sample.X, y);
				return;
			}
			if (_list[i - 1].sample.X >= x)
			{
				_list[i].sample = new Point(_list[i - 1].sample.X, y);
				return;
			}
			if (_list[i + 1].sample.X <= x)
			{
				_list[i].sample = new Point(_list[i + 1].sample.X, y);
				return;
			}
			_list[i].sample = new Point(x, y);
		}
		public int GetIndexFromSampleXY(int x, int y, int dx, int dy)
		{
			for (int i = 0; i < _list.Count; i++)
			{
				if (Math.Abs(_list[i].sample.X - x) < dx && Math.Abs(_list[i].sample.Y - y) < dy) return i;
			}
			return -1;
		}
		public int GetIndexFromPictureXY(int x, int y, double radius)
		{
			double dx;
			double dy;	
			int c = 0;
			int idx = -1;
			for (int i = 0; i < _list.Count; i++)
			{
				if (!_list[i].Visible) continue;
				dx = _list[i].PicX - x;
				dy = _list[i].PicY - y;
				if ((dx * dx + dy * dy) < (radius * radius * 4 * 4))//広く検索
				{
					c++;
					idx = i;
				}
			}
			if (c == 0) return -1;
			if (c == 1) return idx;
			for (int i = 0; i < _list.Count; i++)
			{
				if (!_list[i].Visible) continue;
				dx = _list[i].PicX - x;
				dy = _list[i].PicY - y;
				if ((dx * dx + dy * dy) < (radius * radius))//狭く検索
				{
					return i;
				}
			}
			return -1;
		}
		public void RemovePoint(int index)
		{
			if (index < 0) return;
			if (index >= _list.Count) return;
			if (index == 0) return;
			if (index >= _list.Count - 1) return;
			_list.RemoveAt(index);
		}
		public void MoveLastPoint(int x)
		{
			if (_list.Count < 2) return;
			int last = _list.Count - 1;
			_list[last] = new DrawingLineItem(x, _list[last].sample.Y);
			for (int i = 0; i < _list.Count; i++)
			{
				if (_list[i].sample.X > x)
				{
					_list.RemoveAt(i);
					i--;
				}
			}
			SortPoints();
		}
		public void MoveFirstPoint(int x)
		{
			if (_list.Count < 2) return;
			for (int i = 0; i < _list.Count; i++)
			{
				if (_list[i].sample.X < x)
				{
					_list.RemoveAt(i);
					i--;
				}
			}
			for (int i = 1; i < _list.Count; i++)
			{
				_list[i] = new DrawingLineItem(_list[i].sample.X - x, _list[i].sample.Y);
			}
			SortPoints();
			if (_list[0].sample.X != x)
			{
				_list.Add(new DrawingLineItem(0, _list[0].sample.Y));
				SortPoints();
			}
		}
		public void HideAll()
		{
			if (_list.Count <= 0) return;
			for (int i = 0; i < _list.Count; i++)
			{
				_list[i].Visible = false;
				_list[i].PicX = 0;
				_list[i].PicY = 0;
			}
		}
		public void Shown(int index, double x, double y)
		{
			if (_list.Count <= index) return;
			_list[index].Visible = true;
			_list[index].PicX = x;
			_list[index].PicY = y;
		}
		public WaveFile ToWaveFile(double rate)
		{
			int size = _list[_list.Count - 1].sample.X + 1;
			WaveFile wf = new WaveFile();
			wf.CreateBuffer(size);
			wf.Rate = rate;

			int n = 0;
			int dx;
			int dy;
			double a;
			double v = (double)_list[0].sample.Y;
			for (int i = 0; i < _list.Count - 1; i++)
			{
				dx = _list[i + 1].sample.X - _list[i].sample.X;
				dy = _list[i + 1].sample.Y - _list[i].sample.Y;
				if (dx == 0)
				{
					v = (double)_list[i + 1].sample.Y;
					continue;
				}
				a = (double)dy / (double)dx;
				for (int x = 0; x < dx; x++)
				{
					wf.SetSample(n, (int)v);
					v += a;
					n++;
				}
			}
			return wf;
		}
	}
}
