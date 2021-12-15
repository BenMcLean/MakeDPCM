using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MakeDPCM
{
	public partial class WaveView : UserControl
	{
		// -----------------------------------
		// Const and Static
		// -----------------------------------
		private const int ZOOM_H_MAX = 10;
		private const int ZOOM_H_MIN = -10;
		private const int ZOOM_V_MAX = 10;
		private const int ZOOM_V_MIN = 0;
		private const double HEIGHT_MARGIN_PER = 0.1;
		private static Brush BRUSH_BACK = Brushes.White;
		private static Pen PEN_FORE = Pens.Silver;
		private const double SMIN = (double)short.MinValue;
		private const double SMAX = (double)short.MaxValue;
		private const double SMM = (double)(short.MaxValue - short.MinValue);
		private const double BASE_RATE = 1789772.5 / 54.0;

	
		// -----------------------------------
		// Member
		// -----------------------------------
		private bool _EventCancel = false;
		private int _zoomH = 0;
		private int _zoomV = 0;
		private WaveViewItem[] _wvi = null;
		private int _WaveMaxLength = 0;
		private DrawingLinePoints[] _dlp = null;
		private int _hitIndex = -1;
		private int _hitLine = -1;
		private double _drawingline_rate = 44100.0;
		public bool DrawWaveLine { set; get; }


		// -----------------------------------
		// Constructor
		// -----------------------------------
		public WaveView()
		{
			DrawWaveLine = false;
			InitializeComponent();
		}
		private void WaveView_Load(object sender, EventArgs e)
		{
			THBar.Maximum = ZOOM_H_MAX;
			THBar.Minimum = ZOOM_H_MIN;
			TVBar.Maximum = ZOOM_V_MAX;
			TVBar.Minimum = ZOOM_V_MIN;
		}


		// -----------------------------------
		// Graphics Functions
		// -----------------------------------
		private void PBox_Paint(object sender, PaintEventArgs e)
		{
			double offset = (double)VBar.Value;
			double zv = GetZoomVValue();
			double mgn = getHeightMargin();
			int h = PBox.Height;
			double y;

			y = GetBaseY(h, mgn, zv, offset);
			if (y >= 0 || y < h) e.Graphics.DrawLine(PEN_FORE, (float)0.0, (float)y, (float)PBox.Width, (float)y);

			y = GetBaseY(h, (double)h - mgn, zv, offset);
			if (y >= 0 || y < h) e.Graphics.DrawLine(PEN_FORE, (float)0.0, (float)y, (float)PBox.Width, (float)y);

			y = GetBaseY(h, (double)h / 2.0, zv, offset);
			if (y >= 0 || y < h) e.Graphics.DrawLine(PEN_FORE, (float)0.0, (float)y, (float)PBox.Width, (float)y);

			if (!DrawWaveLine) return;

			if (_wvi != null)
			{
				for (int i = 0; i < _wvi.Length; i++)
				{
					if (_wvi[i].wf == null) continue;
					if (!_wvi[i].visible) continue;
					DrawWaveImageLine(_wvi[i].color, _wvi[i].wf, _wvi[i].HeadDisableSample, HBar.Value, VBar.Value, e.Graphics);
				}
			}
			if (_dlp != null)
			{
				for (int i = 0; i < _dlp.Length; i++)
				{
					if (!_dlp[i].Visible) continue;
					DrawDrawingLineImage(i, e.Graphics);
				}
			}

			LblVZoom.Text = GetZoomHValue().ToString();
		}
		private void WaveView_Resize(object sender, EventArgs e)
		{
			Redraw();
		}
		public void Redraw()
		{
			SetHBar();
			SetVBar();
			PBox.Invalidate();
		}


		// -----------------------------------
		// DrawingLineImage Functions
		// -----------------------------------
		public void CreateDrawingLine(int n)
		{
			_dlp = null;
			_dlp = new DrawingLinePoints[n];
			for (int i = 0; i < _dlp.Length; i++)
			{
				_dlp[i] = new DrawingLinePoints();
			}
		}
		public void SetDrawingLineColor(int index, Color color)
		{
			if (index < 0) throw new MyException("n < 0", index);
			if (_dlp == null) throw new MyException("_dlp == null");
			if (_dlp.Length <= index) throw new MyException("_dlp.Length <= index", _dlp.Length, index);
			_dlp[index].color = color;
			Redraw();
		}
		public void SetDrawingLineRate(double rate)
		{
			if (rate < 0.0) throw new MyException("rate < 0.0");
			_drawingline_rate = rate;
		}
		public void SetDrawingLineVisible(int index, bool flg)
		{
			if (index < 0) throw new MyException("n < 0", index);
			if (_dlp == null) throw new MyException("_dlp == null");
			if (_dlp.Length <= index) throw new MyException("_dlp.Length <= index", _dlp.Length, index);
			_dlp[index].Visible = flg;
		}
		private void DrawDrawingLineImage(int index, Graphics g)
		{
			if (_dlp == null) return;
			if (index >= _dlp.Length) return;
			if (index < 0) return;
			if (_dlp[index].Count < 2) return;
			_dlp[index].HideAll();
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			int start = HBar.Value;
			int y_offset = VBar.Value;
			double x_step = BASE_RATE / _drawingline_rate;
			int zh = GetZoomHValue();
			double zv = GetZoomVValue();

			Pen pen = new Pen(_dlp[index].color);
			double x;
			double y;
			double xb = 0.0;
			double yb = 0.0;
			double dxzh = zh < 0 ? x_step / (double)-zh : (double)zh * x_step;
			double ds = zh < 0 ? (double)start / (double)-zh : (double)(zh * start);

			yb = GetWaveY(PBox.Height, _dlp[index][0].Y, zv, y_offset);
			xb = (double)_dlp[index][0].X * dxzh - ds;
			for (int i = 0; i < _dlp[index].Count; i++)
			{
				y = GetWaveY(PBox.Height, _dlp[index][i].Y, zv, y_offset);
				x = (double)_dlp[index][i].X * dxzh - ds;
				g.DrawEllipse(pen, (float)(x - 3.0), (float)(y - 3.0), 6.0f, 6.0f);
				g.DrawLine(pen, (float)xb, (float)yb, (float)x, (float)y);
				if (x >= 0 && x < PBox.Width) _dlp[index].Shown(i, x, y);
				if ((x + 4.0) > PBox.Width) break;
				xb = x;
				yb = y;
			}
		}
		private void PBox_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != System.Windows.Forms.MouseButtons.Left) return;
			if (_dlp == null) throw new MyException("_dlp == null");

			_hitIndex = -1;
			_hitLine = -1;
			for (int i = 0; i < _dlp.Length; i++)
			{
				if (!_dlp[i].Visible) continue;
				if (Control.ModifierKeys == Keys.Shift)
				{
					_dlp[i].AddPoint(new Point(Mouse2WaveX(e.X), Mouse2WaveY(e.Y)), e.X, e.Y);
				}
				int index = _dlp[i].GetIndexFromPictureXY(e.X, e.Y, DrawingLinePoints.HIT_RADIUS);
				if (Control.ModifierKeys == Keys.Control)
				{
					_dlp[i].RemovePoint(index);
					Redraw();
					break;
				}
				if (index >= 0)
				{
					_dlp[i].Shown(index, e.X, e.Y);
					_hitIndex = index;
					_hitLine = i;
				}
				Redraw();
				break;
			}
		}
		private void PBox_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button != System.Windows.Forms.MouseButtons.Left) return;
			_hitIndex = -1;
		}
		private void PBox_MouseMove(object sender, MouseEventArgs e)
		{
			if (_hitIndex < 0) return;
			if (_hitLine < 0) return;
			if (_dlp == null) throw new MyException("_dlp == null");
			_dlp[_hitLine].MovePoint(_hitIndex, Mouse2WaveX(e.X), Mouse2WaveY(e.Y));
			Redraw();
		}
		private int Mouse2WaveY(int y)
		{
			double offset = VBar.Value;
			double zv = GetZoomVValue();
			double height = (double)PBox.Height;
			return (int)(((-y - offset + height / 2.0) / (height - getHeightMargin() * 2.0) / zv - 0.5) * SMM - SMIN);
		}
		private int Mouse2WaveX(int x)
		{
			double v;
			int zh = GetZoomHValue();
			double x_step = BASE_RATE / _drawingline_rate;
			if (zh < 0)
			{
				v = (double)x / (x_step / (double)-zh);
			}
			else
			{
				v = (double)x / (x_step * (double)zh);
			}
			v += (int)((double)HBar.Value / x_step);
			return (int)v;
		}
		public void SetDrawingLineWidth(int n, int width)
		{
			if (n < 0) throw new MyException("n < 0", n);
			if (_dlp == null) throw new MyException("_dlp == null");
			if (_dlp.Length <= n) throw new MyException("_dlp.Length <= n", _dlp.Length, n);
			_dlp[n].MoveFirstPoint(0);
			_dlp[n].MoveLastPoint(width - 1);
		}
		public void ResetDrawingLine(int n)
		{
			if (n < 0) throw new MyException("n < 0", n);
			if (_dlp == null) throw new MyException("_dlp == null");
			if (_dlp.Length <= n) throw new MyException("_dlp.Length <= n", _dlp.Length, n);
			_dlp[n].ClearPoints();
		}
		public WaveFile GetWaveFileFromDrawingLine(int n, double rate)
		{
			if (n < 0) throw new MyException("n < 0", n);
			if (_dlp == null) throw new MyException("_dlp == null");
			if (_dlp.Length <= n) throw new MyException("_dlp.Length <= n", _dlp.Length, n);
			return _dlp[n].ToWaveFile(rate);
		}
		public Point[] GetDrawLinePoints(int n)
		{
			if (n < 0) throw new MyException("n < 0", n);
			if (_dlp == null) throw new MyException("_dlp == null");
			if (_dlp.Length <= n) throw new MyException("_dlp.Length <= n", _dlp.Length, n);
			return _dlp[n].GetPointArray();
		}
		public void SetDrawLinePoints(int n, Point[] points)
		{
			if (n < 0) throw new MyException("n < 0", n);
			if (_dlp == null) throw new MyException("_dlp == null");
			if (_dlp.Length <= n) throw new MyException("_dlp.Length <= n", _dlp.Length, n);
			_dlp[n].ClearAndSetPointRange(points);
		}

	
		// -----------------------------------
		// WaveImage Functions
		// -----------------------------------
		private double getHeightMargin()
		{
			return PBox.Height * HEIGHT_MARGIN_PER;
		}
		private double GetBaseY(int height, double v, double zv, double offset)
		{
			return ((double)height / 2.0 - v) * zv + (double)height / 2.0 - offset;
		}
		private double GetWaveY(int height, double v, double zv, double offset)
		{
			return (0.5 - (v - SMIN) / SMM) * ((double)height - getHeightMargin() * 2.0) * zv + (double)height / 2.0 - offset;
		}
		private void DrawWaveImageLine(Color c, WaveFile wf, double head, int start, int y_offset, Graphics g)
		{
			if (wf == null) throw new MyException("wf == null");

			Pen p = new Pen(c);
			int length = wf.GetSize();
			int zh = GetZoomHValue();
			double zv = GetZoomVValue();
			double x = -1.0;
			double y = 0.0;
			double xb = -1.0;
			double yb = 0.0;
			double xds = 0.0;
			double x_step = BASE_RATE / wf.Rate;
			double ds = start - head * x_step;

			if(zh>0)
			{
				x_step *= zh;
				ds *= zh;
			}
			else
			{
				x_step /= -zh;
				ds /= -zh;
			}
			if (zh < 4)
			{
				g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			}

			xb = x - ds;
			yb = GetWaveY(PBox.Height, wf.GetSample(0), zv, y_offset);

			if (zh < 0)
			{
				int f;
				int fb = 0;
				int v = 0;
				int vb = 0;
				for (int index = 0; index < length; index++)
				{
					if ((x + x_step + 1.0) >= ds)
					{
						xds = x - ds;
						v = wf.GetSample(index);
						y = GetWaveY(PBox.Height, v, zv, y_offset);
						f = (v - vb > 0.0) ? 1 : -1;
						if (f != fb || x - xb > 1.0)
						{
							g.DrawLine(p, (float)xb, (float)yb, (float)xds, (float)y);
							fb = f;
							xb = xds;
							yb = y;
						}
						vb = v;
						if (xds >= PBox.Width) break;
					}
					x += x_step;
				}
			}
			else if (zh > 0 && zh<4)
			{
				for (int index = 0; index < length; index++)
				{
					if ((x + x_step + 1.0) >= ds)
					{
						xds = x - ds;
						y = GetWaveY(PBox.Height, wf.GetSample(index), zv, y_offset);
						g.DrawLine(p, (float)xb, (float)yb, (float)xds, (float)y);
						xb = xds;
						yb = y;
						if (xds >= PBox.Width) break;
					}
					x += x_step;
				}
			}
			else if (zh >= 4)
			{
				for (int index = 0; index < length; index++)
				{
					if ((x + x_step + 1.0) >= ds)
					{
						xds = x - ds;
						y = GetWaveY(PBox.Height, wf.GetSample(index), zv, y_offset);
						g.DrawLine(p, (float)xb, (float)yb, (float)xds, (float)yb);
						g.DrawLine(p, (float)xds, (float)yb, (float)xds, (float)y);
						xb = xds;
						yb = y;
						if (xds >= PBox.Width) break;
					}
					x += x_step;
				}
			}
		}


		// -----------------------------------
		// Zoom and Scroll
		// -----------------------------------
		private double GetZoomVValue()
		{
			return (double)Math.Pow(1.5, (double)_zoomV);
		}
		private int GetZoomHValue()
		{
			int zh = _zoomH;
			if (zh <= 0) zh -= 2;
			return zh;
		}
		private void BtnHin_Click(object sender, EventArgs e)
		{
			_zoomH++;
			if (_zoomH > ZOOM_H_MAX) _zoomH = ZOOM_H_MAX;
			_EventCancel = true;
			THBar.Value = _zoomH;
			_EventCancel = false;
			SetHBar();
			Redraw();
		}
		private void BtnHout_Click(object sender, EventArgs e)
		{
			_zoomH--;
			if (_zoomH < ZOOM_H_MIN) _zoomH = ZOOM_H_MIN;
			_EventCancel = true;
			THBar.Value = _zoomH;
			_EventCancel = false;
			SetHBar();
			Redraw();
		}
		private void BtnVin_Click(object sender, EventArgs e)
		{
			_zoomV++;
			if (_zoomV > ZOOM_V_MAX) _zoomV = ZOOM_V_MAX;
			_EventCancel = true;
			TVBar.Value = _zoomV;
			_EventCancel = false;
			SetVBar();
			Redraw();
		}
		private void BtnVout_Click(object sender, EventArgs e)
		{
			_zoomV--;
			if (_zoomV < ZOOM_V_MIN) _zoomV = ZOOM_V_MIN;
			_EventCancel = true;
			TVBar.Value = _zoomV;
			_EventCancel = false;
			SetVBar();
			Redraw();
		}
		private void TVBar_ValueChanged(object sender, EventArgs e)
		{
			if (_EventCancel) return;
			_zoomV = TVBar.Value;
			SetVBar();
			Redraw();
		}
		private void THBar_ValueChanged(object sender, EventArgs e)
		{
			if (_EventCancel) return;
			_zoomH = THBar.Value;
			SetHBar();
			Redraw();
		}
		public void SetHBar()
		{
			int zh = GetZoomHValue();

			int w;
			if (zh > 0)
			{
				w = _WaveMaxLength * zh;
			}
			else
			{
				w = _WaveMaxLength / -zh;
			}

			if (w <= PBox.Width)
			{
				HBar.LargeChange = HBar.Maximum;
			}
			else
			{
				if (zh > 0)
				{
					HBar.LargeChange = PBox.Width / zh;
				}
				else
				{
					HBar.LargeChange = PBox.Width * -zh;
				}
			}
			_EventCancel = true;
			if (HBar.Value < HBar.Minimum) HBar.Value = HBar.Minimum;
			if (HBar.Value + HBar.LargeChange > HBar.Maximum) HBar.Value = HBar.Maximum - HBar.LargeChange;
			_EventCancel = false;
		}
		public void SetHBarMax(int size, double rate)
		{
			int s = (int)((double)(size + 128) * BASE_RATE / rate);
			if (_WaveMaxLength < s)
			{
				_WaveMaxLength = s;
				HBar.Maximum = s;
			}
		}
		private void HBar_ValueChanged(object sender, EventArgs e)
		{
			if (_EventCancel) return;
			Redraw();
		}
		public void SetVBar()
		{
			double hi = PBox.Height * GetZoomVValue();
			VBar.Minimum = (int)(-hi / 2.0) + PBox.Height / 2 + 1;
			VBar.Maximum = (int)(hi / 2.0) + PBox.Height / 2;
			VBar.LargeChange = PBox.Height;
			_EventCancel = true;
			if (VBar.Value < VBar.Minimum) VBar.Value = VBar.Minimum;
			if (VBar.Value + VBar.LargeChange > VBar.Maximum) VBar.Value = VBar.Maximum - VBar.LargeChange + 1;
			_EventCancel = false;
		}
		private void VBar_ValueChanged(object sender, EventArgs e)
		{
			if (_EventCancel) return;
			Redraw();
		}


		// -----------------------------------
		// WaveViewItem Functions
		// -----------------------------------
		public void FreeWave()
		{
			if (_wvi == null) return;
			for (int i = 0; i < _wvi.Length; i++)
			{
				_wvi[i].Free();
				_wvi[i] = null;
			}
			_wvi = null;
			GC.Collect();
			_WaveMaxLength = 0;
		}
		public void CreateWave(int n)
		{
			FreeWave();
			_wvi = new WaveViewItem[n];
			for (int i = 0; i < _wvi.Length; i++)
			{
				_wvi[i] = new WaveViewItem();
			}
		}
		public void SetWave(int index, WaveFile wf, Color c, bool view, double head)
		{
			if (_wvi == null) throw new MyException("_WaveViewItem == null");
			if (_wvi.Length <= index) throw new MyException("_WaveViewItem.Length <= index", _wvi.Length, index);
			if (wf == null) throw new MyException("wf == null");
			_wvi[index].Free();
			_wvi[index].wf = wf;
			_wvi[index].color = c;
			_wvi[index].visible = view;
			_wvi[index].HeadDisableSample = head;
			_EventCancel = true;
			SetHBarMax(wf.GetSize(), wf.Rate);
			_EventCancel = false;
		}
		public void SetVisible(int index, bool visible)
		{
			if (_wvi == null) throw new MyException("_WaveViewItem == null");
			if (_wvi.Length <= index) throw new MyException("_WaveViewItem.Length <= index", _wvi.Length, index);
			_wvi[index].visible = visible;
		}
		private class WaveViewItem
		{
			public WaveFile wf { set; get; }
			public Color color { set; get; }
			public bool visible { set; get; }
			public double HeadDisableSample { set; get; }

			public WaveViewItem()
			{
				wf = null;
				color = Color.Black;
				visible = true;
			}
			public void Free()
			{
				if (wf != null)
				{
					wf.Free();
					wf = null;
				}
				color = Color.Black;
				visible = false;
			}
		}
		public void ResetWaveMaxLength()
		{
			_WaveMaxLength = 0;
		}
	}
}
