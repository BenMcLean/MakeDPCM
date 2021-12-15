using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace MakeDPCM
{
	public partial class MainForm : Form
	{
		// -----------------------------------
		// const
		// -----------------------------------
		private const string EXT_WAV = ".wav";
		private const string EXT_DMC = ".dmc";
		private const string EXT_PRM = ".prm";
		private const string EXT_INI = ".ini";
		private const double CPU_CLOCK = 21477270.0 / 12.0;
		private const int DPCM_SIZE_LIMIT = 0x0FF1 * 8;
		private const int INDEX_WAVE_SRC = 0;
		private const int INDEX_WAVE_IN = 1;
		private const int INDEX_MA = 2;
		private const int INDEX_WAVE_OUT = 3;
		private const int INDEX_DPCM = 4;
		private const int INDEX_ENVELOPE = 0;
		private const int INDEX_SLOPE = 1;
		private const int WV_NUM_WAVE = 5;
		private const int WV_NUM_DL = 2;
		private const string SECTION_ENVELOPE = "MakeDPCM_Envelope";
		private const string SECTION_SLOPE = "MakeDPCM_Slope";


		// -----------------------------------
		// type
		// -----------------------------------
		private enum MA_TYPE : int { SMA, WMA, EMA };
		private enum SLOPE_TYPE : int { ZeroLine, MinLine };
		private enum DMC_SIZE : int { Limit, NoLimit, Division };
		private enum INPUT_TYPE : int { WAV, PRM, DMC };


		// -----------------------------------
		// Member
		// -----------------------------------
		private double[] _DpcmRate = new double[16];
		private ParameterFile _pf = new ParameterFile();
		private bool _ThreadResult = false;
		private bool _EventCancel = false;
		private WavePlayer _player = new WavePlayer();
		private WaveFile _wf_src = null;
		private WaveFile _wf_wav = null;
		private WaveFile _wf_dmc = null;
		private Keys[] _kb = new Keys[16];
		private StopwatchPeriod _sw = new StopwatchPeriod(256);
		private INPUT_TYPE _input_type = INPUT_TYPE.WAV;
		private string _filename_wav;
		private string _filename_prm;
		private string _filename_dmc;
		private bool _flg_inputdata = false;


		// -----------------------------------
		// Constructor
		// -----------------------------------
		public MainForm()
		{
			InitializeComponent();

			_DpcmRate[0] = CPU_CLOCK / 428.0;
			_DpcmRate[1] = CPU_CLOCK / 380.0;
			_DpcmRate[2] = CPU_CLOCK / 340.0;
			_DpcmRate[3] = CPU_CLOCK / 320.0;
			_DpcmRate[4] = CPU_CLOCK / 286.0;
			_DpcmRate[5] = CPU_CLOCK / 254.0;
			_DpcmRate[6] = CPU_CLOCK / 226.0;
			_DpcmRate[7] = CPU_CLOCK / 214.0;
			_DpcmRate[8] = CPU_CLOCK / 190.0;
			_DpcmRate[9] = CPU_CLOCK / 160.0;
			_DpcmRate[10] = CPU_CLOCK / 142.0;
			_DpcmRate[11] = CPU_CLOCK / 128.0;
			_DpcmRate[12] = CPU_CLOCK / 106.0;
			_DpcmRate[13] = CPU_CLOCK / 85.0;
			_DpcmRate[14] = CPU_CLOCK / 72.0;
			_DpcmRate[15] = CPU_CLOCK / 54.0;

			_kb[0] = Keys.Oemplus;
			_kb[1] = Keys.OemPeriod;
			_kb[2] = Keys.L;
			_kb[3] = Keys.Oemcomma;
			_kb[4] = Keys.K;
			_kb[5] = Keys.M;
			_kb[6] = Keys.J;
			_kb[7] = Keys.N;
			_kb[8] = Keys.H;
			_kb[9] = Keys.B;
			_kb[10] = Keys.G;
			_kb[11] = Keys.V;
			_kb[12] = Keys.F;
			_kb[13] = Keys.C;
			_kb[14] = Keys.D;
			_kb[15] = Keys.X;

			_pf.Load();
			ClearStatus();
			ShowStatus();
		}
		private void MainForm_Load(object sender, EventArgs e)
		{
			NumWaveSpeed.Minimum = (decimal)ParameterFile.WAVE_SPEED_FREQ_MIN;
			NumWaveSpeed.Maximum = (decimal)ParameterFile.WAVE_SPEED_FREQ_MAX;
			NumWaveLpfFreq.Minimum = (decimal)(int)ParameterFile.WAVE_LPF_FREQ_MIN;
			NumWaveLpfFreq.Maximum = (decimal)(int)ParameterFile.WAVE_LPF_FREQ_MAX;
			NumWaveLpfN.Minimum = (decimal)ParameterFile.WAVE_LPF_N_MIN;
			NumWaveLpfN.Maximum = (decimal)ParameterFile.WAVE_LPF_N_MAX;
			NumWaveVolume.Minimum = (decimal)ParameterFile.WAVE_VLUME_AMP_MIN;
			NumWaveVolume.Maximum = (decimal)ParameterFile.WAVE_VLUME_AMP_MAX;
			NumWaveEnvelope.Minimum = (decimal)ParameterFile.WAVE_ENVELOPE_AMP_MIN;
			NumWaveEnvelope.Maximum = (decimal)ParameterFile.WAVE_ENVELOPE_AMP_MAX;

			CmbMaType.Items.Add(MA_TYPE.SMA.ToString());
			CmbMaType.Items.Add(MA_TYPE.WMA.ToString());
			CmbMaType.Items.Add(MA_TYPE.EMA.ToString());
			CmbMaType.SelectedIndex = 0;

			NumMaLength.Minimum = (decimal)ParameterFile.MA_LENGTH_MIN;
			NumMaLength.Maximum = (decimal)ParameterFile.MA_LENGTH_MAX;
			NumMaAmp.Minimum = (decimal)ParameterFile.MA_AMP_MIN;
			NumMaAmp.Maximum = (decimal)ParameterFile.MA_AMP_MAX;
			NumMaBias.Minimum = (decimal)ParameterFile.MA_BIAS_MIN;
			NumMaBias.Maximum = (decimal)ParameterFile.MA_BIAS_MAX;

			CmbDpcmRate.Items.Clear();
			for (int i = 15; i >= 0; i--)
			{
				CmbDpcmRate.Items.Add(i.ToString("D02") + "   " + _DpcmRate[i].ToString("0.00") + "Hz");
			}
			CmbDpcmRate.SelectedIndex = 0;
			NumDpcmFirst.Minimum = (decimal)ParameterFile.DPCM_FIRST_VALUE_MIN;
			NumDpcmFirst.Maximum = (decimal)ParameterFile.DPCM_FIRST_VALUE_MAX;
			NumDpcmAdjust.Minimum = (decimal)ParameterFile.DPCM_ADJUST_MIN;
			NumDpcmAdjust.Maximum = (decimal)ParameterFile.DPCM_ADJUST_MAX;

			CmbDmcSize.Items.Add(DMC_SIZE.Limit);
			CmbDmcSize.Items.Add(DMC_SIZE.NoLimit);
			CmbDmcSize.Items.Add("Streaming");
			CmbDmcSize.SelectedIndex = 0;

			WView.CreateWave(WV_NUM_WAVE);
			WView.CreateDrawingLine(WV_NUM_DL);
			WView.SetDrawingLineColor(INDEX_ENVELOPE, _pf.view_color_envelope);
			WView.SetDrawingLineColor(INDEX_SLOPE, _pf.view_color_slope);

			this.Top = _pf.window_top;
			this.Left = _pf.window_left;
			this.Size = SizeFromClientSize(new Size(_pf.window_width, _pf.window_height));

			Set2Edit();
			SetUI();
		}
		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (e.CloseReason != CloseReason.None &&
				e.CloseReason != CloseReason.TaskManagerClosing)
			{
				Edit2Set();
				OnResize(EventArgs.Empty);
				_pf.Save();
			}
		}
		private void BtnReset_Click(object sender, EventArgs e)
		{
			_pf.Reset(false);
			Set2Edit();
			SetUI();
		}


		// -----------------------------------
		// UI Control
		// -----------------------------------
		private void EnableUI(bool flg)
		{
			foreach (Control ctr in this.Controls)
			{
				ctr.Enabled = flg;
			}
		}
		private void SetUI()
		{
			if (_EventCancel) return;
			NumWaveLpfFreq.Enabled = CBoxLPF.Checked;
			NumWaveLpfN.Enabled = CBoxLPF.Checked;
			NumWaveSpeed.Enabled = CBoxWaveSpeed.Checked;
			NumWaveVolume.Enabled = CBoxWaveVolume.Checked;
			NumWaveEnvelope.Enabled = CBoxWaveEnvelope.Checked;
			GBoxMa.Enabled = CBoxMa.Checked;
			GBoxDraw.Enabled = CBoxSlope.Checked;
			NumDpcmFirst.Enabled = !CBoxDpcmFirstAuto.Checked;
			CBoxFileOutWav.Enabled = !(_input_type == INPUT_TYPE.WAV || _input_type == INPUT_TYPE.PRM);
		}
		private void Common_CheckedChanged(object sender, EventArgs e)
		{
			if (_EventCancel) return;
			if (sender.Equals(CBoxMa)) CBoxViewCommon_CheckedChanged(sender, e);
			SetUI();
		}
		private void Set2Edit()
		{
			_EventCancel = true;

			CBoxWaveSpeed.Checked = _pf.wave_speed;
			NumWaveSpeed.Value = (decimal)_pf.wave_speed_amp;
			CBoxLPF.Checked = _pf.wave_lpf;
			NumWaveLpfFreq.Value = (decimal)(int)_pf.wave_lpf_freq;
			NumWaveLpfN.Value = (decimal)_pf.wave_lpf_n;
			CBoxWaveNormalize.Checked = _pf.wave_normalize;
			CBoxWaveVolume.Checked = _pf.wave_volume;
			NumWaveVolume.Value = (decimal)_pf.wave_volume_amp;
			CBoxWaveEnvelope.Checked = _pf.wave_envelope;
			NumWaveEnvelope.Value = (decimal)_pf.wave_envelope_amp;
			CBoxWaveTrimZero.Checked = _pf.wave_trim;

			CBoxMa.Checked = _pf.ma;
			CmbMaType.SelectedIndex = (int)_pf.ma_type;
			NumMaLength.Value = (decimal)_pf.ma_length;
			NumMaAmp.Value = (decimal)_pf.ma_amp;
			NumMaBias.Value = (decimal)_pf.ma_bias;

			CBoxSlope.Checked = _pf.slope;
			RBtnSlopeZero.Checked = _pf.slope_type == SLOPE_TYPE.ZeroLine;
			RBtnSlopeMin.Checked = _pf.slope_type == SLOPE_TYPE.MinLine;

			CmbDpcmRate.SelectedIndex = 15 - _pf.dpcm_rate;
			NumDpcmFirst.Value = (decimal)_pf.dpcm_first_value;
			CBoxDpcmFirstAuto.Checked = _pf.dpcm_first_auto;
			NumDpcmAdjust.Value = (decimal)_pf.dpcm_adjust;

			CBoxDmcPadding.Checked = _pf.dmc_padding;
			CBoxFileOutDmc.Checked = _pf.file_out_dmc;
			CmbDmcSize.SelectedIndex = (int)_pf.dmc_size;

			CBoxViewWaveIn.Checked = _pf.view_wave_in;
			CBoxViewDpcm.Checked = _pf.view_dpcm;
			CBoxViewMa.Checked = _pf.view_ma;
			CBoxViewWaveOut.Checked = _pf.view_wave_out;
			CBoxViewEnvelope.Checked = _pf.view_envelope;
			CBoxViewSlope.Checked = _pf.view_slope;

			CBoxFileOutWav.Checked = _pf.file_out_wav;
			CBoxFileOutPrm.Checked = _pf.file_out_prm;
			CBoxFileOutDmc.Checked = _pf.file_out_dmc;

			_EventCancel = false;
		}
		private void Edit2Set()
		{
			_pf.wave_speed = CBoxWaveSpeed.Checked;
			_pf.wave_speed_amp = (double)NumWaveSpeed.Value;
			_pf.wave_lpf = CBoxLPF.Checked;
			_pf.wave_lpf_freq = (double)NumWaveLpfFreq.Value;
			_pf.wave_lpf_n = (int)NumWaveLpfN.Value;
			_pf.wave_normalize = CBoxWaveNormalize.Checked;
			_pf.wave_volume = CBoxWaveVolume.Checked;
			_pf.wave_volume_amp = (double)NumWaveVolume.Value;
			_pf.wave_envelope = CBoxWaveEnvelope.Checked;
			_pf.wave_envelope_amp = (double)NumWaveEnvelope.Value;
			_pf.wave_trim = CBoxWaveTrimZero.Checked;

			_pf.ma = CBoxMa.Checked;
			_pf.ma_type = (MA_TYPE)CmbMaType.SelectedIndex;
			_pf.ma_length = (int)NumMaLength.Value;
			_pf.ma_amp = (double)NumMaAmp.Value;
			_pf.ma_bias = (int)NumMaBias.Value;

			_pf.slope = CBoxSlope.Checked;
			if (RBtnSlopeZero.Checked) _pf.slope_type = SLOPE_TYPE.ZeroLine;
			if (RBtnSlopeMin.Checked) _pf.slope_type = SLOPE_TYPE.MinLine;

			_pf.dpcm_rate = 15 - CmbDpcmRate.SelectedIndex;
			_pf.dpcm_first_value = (int)NumDpcmFirst.Value;
			_pf.dpcm_first_auto = CBoxDpcmFirstAuto.Checked;
			_pf.dpcm_adjust = (int)NumDpcmAdjust.Value;

			_pf.dmc_padding = CBoxDmcPadding.Checked;
			_pf.file_out_dmc = CBoxFileOutDmc.Checked;
			_pf.dmc_size = (DMC_SIZE)CmbDmcSize.SelectedIndex;

			_pf.view_wave_in = CBoxViewWaveIn.Checked;
			_pf.view_dpcm = CBoxViewDpcm.Checked;
			_pf.view_ma = CBoxViewMa.Checked;
			_pf.view_wave_out = CBoxViewWaveOut.Checked;
			_pf.view_envelope = CBoxViewEnvelope.Checked;
			_pf.view_slope = CBoxViewSlope.Checked;

			_pf.file_out_wav = CBoxFileOutWav.Checked;
			_pf.file_out_prm = CBoxFileOutPrm.Checked;
			_pf.file_out_dmc = CBoxFileOutDmc.Checked;
		}
		private void MainForm_Resize(object sender, EventArgs e)
		{
			if (this.WindowState != FormWindowState.Normal) return;
			_pf.window_top = this.Top;
			_pf.window_left = this.Left;
			_pf.window_width = this.ClientSize.Width;
			_pf.window_height = this.ClientSize.Height;
		}
		private void Num_Leave(object sender, EventArgs e)
		{
			NumericUpDown num = (NumericUpDown)sender;
			num.Text = num.Value.ToString();
		}


		// -----------------------------------
		// Drag&Drop
		// -----------------------------------
		private void MainForm_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
				return;
			}
			e.Effect = DragDropEffects.None;
		}
		private void MainForm_DragDrop(object sender, DragEventArgs e)
		{
			_drag_event_args = e;
			this.BeginInvoke((Action)delegate()
			{
				DropFileThrower();
			});
		}
		static private string ReplaceExt(string filename, string ext)
		{
			return Path.GetDirectoryName(filename) + "\\" + Path.GetFileNameWithoutExtension(filename) + ext;
		}
		private DragEventArgs _drag_event_args;
		private void DropFileThrower()
		{
			string[] files = (string[])_drag_event_args.Data.GetData(DataFormats.FileDrop, false);
			if (files.Length <= 0) return;
			string lower;
			for (int i = 0; i < files.Length; i++)
			{
				lower = files[i].ToLower();
				if (lower.EndsWith(EXT_WAV)) _input_type = INPUT_TYPE.WAV;
				else if (lower.EndsWith(EXT_PRM)) _input_type = INPUT_TYPE.PRM;
				else if (lower.EndsWith(EXT_DMC)) _input_type = INPUT_TYPE.DMC;
				else continue;
				ConvertProc(files[i]);
			}
		}


		// -----------------------------------
		// WaveView
		// -----------------------------------
		private void CBoxViewCommon_CheckedChanged(object sender, EventArgs e)
		{
			if (_EventCancel) return;
			Edit2Set();
			WView.SetVisible(INDEX_WAVE_SRC, _pf.view_wave_src && _pf.view_wave_in);
			WView.SetVisible(INDEX_WAVE_IN, _pf.view_wave_in);
			WView.SetVisible(INDEX_MA, _pf.view_ma && _pf.ma);
			WView.SetVisible(INDEX_WAVE_OUT, _pf.view_wave_out);
			WView.SetVisible(INDEX_DPCM, _pf.view_dpcm);
			WView.Redraw();
		}
		private void CBoxViewDrawingLineCommon_CheckedChanged(object sender, EventArgs e)
		{
			if (sender.Equals(CBoxViewEnvelope) && CBoxViewEnvelope.Checked == true)
			{
				CBoxViewSlope.Checked = false;
			}
			if (sender.Equals(CBoxViewSlope) && CBoxViewSlope.Checked == true)
			{
				CBoxViewEnvelope.Checked = false;
			}
			WView.SetDrawingLineVisible(INDEX_ENVELOPE, CBoxViewEnvelope.Checked);
			WView.SetDrawingLineVisible(INDEX_SLOPE, CBoxViewSlope.Checked);
			WView.Redraw();
		}


		// -----------------------------------
		// Envelope & Slope Function
		// -----------------------------------
		private void CBoxDrawingLineCommon_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != System.Windows.Forms.MouseButtons.Right) return;
			int index = 0;
			if (sender.Equals(CBoxWaveEnvelope)) index = INDEX_ENVELOPE;
			if (sender.Equals(CBoxSlope)) index = INDEX_SLOPE;
			WView.ResetDrawingLine(index);
			if (_wf_wav != null)
			{
				WView.SetDrawingLineWidth(index, _wf_src.GetSize());
			}
			else
			{
				WView.SetDrawingLineWidth(index, 1000);
			}
			WView.Redraw();
		}


		// -----------------------------------
		// PlayWAVE PlayDPCM
		// -----------------------------------
		private void BtnPlayWave_MouseDown(object sender, MouseEventArgs e)
		{
			if (_wf_wav == null) return;
			_player.SetWaveFile(_wf_wav);
			_player.Play();
		}
		private void BtnPlayDpcm_MouseDown(object sender, MouseEventArgs e)
		{
			if (_wf_dmc == null) return;
			_player.SetWaveFile(_wf_dmc);
			_player.Play();
		}
		private void BtnPlayWave_KeyDown(object sender, KeyEventArgs e)
		{
			if (!(e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)) return;
			if (_wf_wav == null) return;
			_player.SetWaveFile(_wf_wav);
			_player.Play();
		}
		private void BtnPlayDpcm_KeyDown(object sender, KeyEventArgs e)
		{
			if (!(e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)) return;
			if (_wf_dmc == null) return;
			_player.SetWaveFile(_wf_dmc);
			_player.Play();
		}
		private void BtnStop_KeyDown(object sender, KeyEventArgs e)
		{
			if (!(e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)) return;
			_player.Stop();
		}
		private void BtnStop_MouseDown(object sender, MouseEventArgs e)
		{
			_player.Stop();
		}
		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.S)
			{
				_player.Stop();
				return;
			}
			if (e.KeyCode == Keys.A)
			{
				BtnConv_Click(null, EventArgs.Empty);
				return;
			}
			if (e.KeyCode == Keys.Z)
			{
				BtnPlayWave_KeyDown(null, new KeyEventArgs(Keys.Space));
				return;
			}
			for (int i = 0; i < _kb.Length; i++)
			{
				if (e.KeyCode == _kb[i])
				{
					if (_wf_dmc != null)
					{
						_player.SetWaveFile(_wf_dmc, _DpcmRate[i]);
						_player.Play();
					}
					break;
				}
			}
			if (e.KeyCode == Keys.R)
			{
				BtnReset_Click(null, EventArgs.Empty);
				return;
			}
			if (e.KeyCode == Keys.T)
			{
				TextBoxForm.Show(_sw.GetPeriodString());
			}
		}


		// -----------------------------------
		// Conversion functions
		// -----------------------------------
		private void BtnConv_Click(object sender, EventArgs e)
		{
			if (!_flg_inputdata) return;
			ConvertProc(null);
		}
		private void ConvertProc(string filename)
		{
			try
			{
				_sw.Restart();

				// ---------------------------------
				// パラメータ
				Edit2Set();
				if (filename != null)
				{
					if (_input_type == INPUT_TYPE.WAV)
					{
						_filename_wav = ReplaceExt(filename, EXT_WAV);
						_filename_prm = ReplaceExt(filename, EXT_PRM);
						_filename_dmc = ReplaceExt(filename, EXT_DMC);
						_pf.file_out_wav = false;
					}
					if (_input_type == INPUT_TYPE.PRM)
					{
						_filename_wav = ReplaceExt(filename, EXT_WAV);
						_filename_prm = ReplaceExt(filename, EXT_PRM);
						_filename_dmc = ReplaceExt(filename, EXT_DMC);
						_pf.Load(_filename_prm);
						_pf.file_out_wav = false;
					}
					if (_input_type == INPUT_TYPE.DMC)
					{
						string addstr = "_dmc";
						_filename_wav = ReplaceExt(filename, addstr + EXT_WAV);
						_filename_prm = ReplaceExt(filename, addstr + EXT_PRM);
						_filename_dmc = ReplaceExt(filename, addstr + EXT_DMC);
						_pf.wave_speed = false;
						_pf.wave_lpf = false;
						_pf.wave_normalize = false;
						_pf.wave_volume = false;
						_pf.wave_envelope = false;
						_pf.wave_trim = false;
						_pf.ma = false;
						_pf.slope = false;
						_pf.dpcm_first_auto = false;
						_pf.file_out_wav = false;
						_pf.file_out_prm = false;
						_pf.file_out_dmc = false;
					}
				}
				Set2Edit();
				SetUI();

				if (filename != null)
				{
					// ---------------------------------
					// WAVファイル
					if (_wf_src != null)
					{
						_wf_src.Free();
						_wf_src = null;
					}
					_flg_inputdata = false;
					_wf_src = new WaveFile();
					if (_input_type == INPUT_TYPE.WAV || _input_type == INPUT_TYPE.PRM)
					{
						if (!File.Exists(_filename_wav))
						{
							ClearStatus();
							AddStatus("not found " + _filename_wav);
							ShowStatus();
							return;
						}
						if (!_wf_src.CheckFormat(_filename_wav, true))
						{
							ClearStatus();
							AddStatus("unknown format " + _filename_wav);
							ShowStatus();
							return;
						}
						if (!_wf_src.Load(_filename_wav))
						{
							ClearStatus();
							AddStatus("failed load " + _filename_wav);
							ShowStatus();
							return;
						}
					}
					if (_input_type == INPUT_TYPE.DMC)
					{
						DpcmFile df = new DpcmFile();
						df.Load(filename);
						_wf_src = DpcmConverter.Dpcm2Wave(df, _DpcmRate[_pf.dpcm_rate], _pf.dpcm_first_value, _pf.dpcm_adjust);
						df.Free();
						df = null;
					}

					// ---------------------------------
					// WaveView
					WView.DrawWaveLine = true;
					WView.ResetWaveMaxLength();
					WView.SetHBarMax(_wf_src.GetSize(), _wf_src.Rate);
					WView.SetDrawingLineRate(_wf_src.Rate);
					if (_input_type == INPUT_TYPE.PRM)
					{
						Point[] points = null;
						ParameterFile.LoadDrawLine(_filename_prm, SECTION_ENVELOPE, out points);
						WView.SetDrawLinePoints(INDEX_ENVELOPE, points);
						ParameterFile.LoadDrawLine(_filename_prm, SECTION_SLOPE, out points);
						WView.SetDrawLinePoints(INDEX_SLOPE, points);
						points = null;
					}
					WView.SetDrawingLineWidth(INDEX_ENVELOPE, _wf_src.GetSize());
					WView.SetDrawingLineWidth(INDEX_SLOPE, _wf_src.GetSize());
				}

				// ---------------------------------
				// スレッド
				EnableUI(false);
				ClearStatus();
				_flg_inputdata = true;

				Thread trd = new Thread(ConvertWave2Dmc);
				trd.IsBackground = true;
				_ThreadResult = false;
				trd.Start();

				while (trd.IsAlive)
				{
					Thread.Sleep(1);
					Application.DoEvents();
				}
				if (!_ThreadResult)
				{
					AddStatus("*** Failed... ***");
				}

				ShowStatus();
				WView.Redraw();
				EnableUI(true);
				Set2Edit();
				SetUI();
			}
			catch (MyException ex)
			{
				ClearStatus();
				AddStatus(ex);
				ShowStatus();
				EnableUI(true);
			}
			_sw.Period();
		}
		private void ConvertWave2Dmc()
		{
			try
			{
				if (_wf_wav != null)
				{
					_wf_wav.Free();
					_wf_wav = null;
				}
				if (_wf_dmc != null)
				{
					_wf_dmc.Free();
					_wf_dmc = null;
				}

				// -------------------------
				// SRC zone
				WaveFile wf = _wf_src.Clone();
				WView.SetWave(INDEX_WAVE_SRC, wf.Clone(), _pf.view_color_wave_src, _pf.view_wave_src && _pf.view_wave_in, 0);

				// -------------------------
				// WAVE zone
				if (_pf.wave_speed)
				{
					wf.Rate *= (_pf.wave_speed_amp / 100.0);
					WView.SetDrawingLineRate(wf.Rate);
				}
				if (_pf.wave_lpf) WaveConverter.LPF(wf, _pf.wave_lpf_freq, _pf.wave_lpf_n);
				if (_pf.wave_normalize) WaveConverter.Normalize(wf);
				if (_pf.wave_volume) WaveConverter.Volume(wf, _pf.wave_volume_amp / 100.0);
				if (_pf.wave_envelope)
				{
					WaveFile wf2 = WView.GetWaveFileFromDrawingLine(INDEX_ENVELOPE, wf.Rate);
					WaveConverter.Multi(wf, wf2, _pf.wave_envelope_amp);
					wf2.Free();
					wf2 = null;
				}
				int head_int = 0;
				int tail_int = 0;
				double head_wav = 0;
				double head_dpcm = 0;
				if (_pf.wave_trim)
				{
					WaveConverter.HeadTailRemoveSilence(wf, (short)(DpcmConverter.DPCM_VALUE_WAVE / 2), out head_int, out tail_int);
					head_wav = (double)head_int;
					head_dpcm = (double)head_int * _DpcmRate[_pf.dpcm_rate] / wf.Rate;
				}
				wf.SearchMinMax();
				WView.SetWave(INDEX_WAVE_IN, wf.Clone(), _pf.view_color_wave_in, _pf.view_wave_in, head_wav);
				_wf_wav = wf.Clone();
				AddStatus(wf);

				// -------------------------
				// MA zone
				if (_pf.ma)
				{
					WaveFile wf2 = wf.Clone();
					if (_pf.ma_type == MA_TYPE.SMA) WaveConverter.SMA(wf2, _pf.ma_length);
					if (_pf.ma_type == MA_TYPE.WMA) WaveConverter.WMA(wf2, _pf.ma_length);
					if (_pf.ma_type == MA_TYPE.EMA) WaveConverter.EMA(wf2, _pf.ma_length);
					if (_pf.ma_amp != 1.0) WaveConverter.Volume(wf2, _pf.ma_amp);
					if (_pf.ma_bias != 0.0) WaveConverter.Bias(wf2, _pf.ma_bias);
					if (_pf.ma_type == MA_TYPE.EMA) WaveConverter.Bias(wf2, short.MinValue);
					WView.SetWave(INDEX_MA, wf2.Clone(), _pf.view_color_ma, _pf.view_ma, head_wav);
					WaveConverter.Bias(wf2, -short.MinValue);
					WaveConverter.Sub(wf, wf2);
					wf2.Free();
					wf2 = null;
				}

				// -------------------------
				// Slope zone
				if (_pf.slope)
				{
					WaveFile wf2 = WView.GetWaveFileFromDrawingLine(INDEX_SLOPE, wf.Rate);
					if (head_wav != 0 || tail_int != 0)
					{
						WaveConverter.Trimming(wf2, head_int, tail_int - head_int + 1);
					}
					if (_pf.slope_type == SLOPE_TYPE.ZeroLine)
					{
						WaveConverter.Add(wf, wf2);
					}
					if (_pf.slope_type == SLOPE_TYPE.MinLine)
					{
						WaveConverter.Bias(wf2, -short.MinValue);
						WaveConverter.Sub(wf, wf2);
					}
					wf2.Free();
					wf2 = null;
				}
				WView.SetWave(INDEX_WAVE_OUT, wf.Clone(), _pf.view_color_wave_out, _pf.view_wave_out, head_wav);

				// -------------------------
				// DPCM zone
				if (_pf.dpcm_first_auto) _pf.dpcm_first_value = WaveConverter.WaveValue2DpcmValue(wf.GetSample(0));
				DpcmFile df = WaveConverter.Wave2Dpcm(WaveConverter.Wave2DpcmType.Stretch, wf, _DpcmRate[_pf.dpcm_rate], _pf.dpcm_first_value, _pf.dpcm_adjust);

				// -------------------------
				// DMC zone
				if (_pf.dmc_padding)
				{
					if (_pf.ma || _pf.slope)
						DpcmConverter.Padding16Byte(df, _pf.dmc_padding_proc, (byte)0x00);
					else
						DpcmConverter.Padding16Byte(df, _pf.dmc_padding_proc, (byte)0x55);
				}
				else
				{
					if (_pf.ma || _pf.slope)
						DpcmConverter.PaddingByte(df, _pf.dmc_padding_proc, (byte)0x00);
					else
						DpcmConverter.PaddingByte(df, _pf.dmc_padding_proc, (byte)0x55);
				}
				if (_pf.dmc_size == DMC_SIZE.Limit && df.GetSize() > DPCM_SIZE_LIMIT) DpcmConverter.Resize(df, DPCM_SIZE_LIMIT);
				_wf_dmc = DpcmConverter.Dpcm2Wave(df, _DpcmRate[_pf.dpcm_rate], _pf.dpcm_first_value, _pf.dpcm_adjust);
				WView.SetWave(INDEX_DPCM, _wf_dmc.Clone(), _pf.view_color_dpcm, _pf.view_dpcm, head_dpcm);
				if (_pf.ma)
				{
					_wf_dmc.SearchMinMax();
					WaveConverter.Bias(_wf_dmc, -_wf_dmc.Min);
				}
				AddStatus("");
				AddStatus(df, _DpcmRate[_pf.dpcm_rate], _pf.dpcm_first_value);

				// -------------------------
				// File zone
				if (_pf.file_out_wav)
				{
					wf.Save(_filename_wav);
				}
				if (_pf.file_out_prm)
				{
					if (File.Exists(_filename_prm)) File.Delete(_filename_prm);
					_pf.Save(_filename_prm);
					ParameterFile.SaveDrawLine(_filename_prm, SECTION_ENVELOPE, WView.GetDrawLinePoints(INDEX_ENVELOPE));
					ParameterFile.SaveDrawLine(_filename_prm, SECTION_SLOPE, WView.GetDrawLinePoints(INDEX_SLOPE));
				}
				if (_pf.file_out_dmc)
				{
					df.Save(_filename_dmc);
					if (_pf.dmc_size == DMC_SIZE.Division) DmcDivProc(df);
				}

				wf.Free();
				wf = null;
				df.Free();
				df = null;
				_ThreadResult = true;
			}
			catch (MyException ex)
			{
				ClearStatus();
				AddStatus(ex);
			}
		}
		private void DmcDivProc(DpcmFile df)
		{
			DpcmFile dfDiv = new DpcmFile();
			dfDiv.CreateBuffer(DPCM_SIZE_LIMIT);

			int amari = df.GetSize() % DPCM_SIZE_LIMIT;
			int num = (df.GetSize() - amari) / DPCM_SIZE_LIMIT;
			int pos = 0;

			string NL = Environment.NewLine;
			string mml = "";
			mml += "#Code	\"../../bin/nsd_all.bin\"" + NL;
			mml += "#Bank" + NL;
			mml += "DPCM{" + NL;

			string divname;

			for (int n = 0; n < num; n++)
			{
				for (int i = 0; i < DPCM_SIZE_LIMIT; i++)
				{
					dfDiv.SetSample(i, df.GetSample(pos));
					pos++;
				}
				divname = n.ToString("D03") + Path.GetFileName(_filename_dmc);
				mml += "\tn" + n.ToString("D03") + ",";
				mml += "\"" + divname + "\",";
				mml += _pf.dpcm_rate.ToString("D") + ",";
				mml += "2,";
				if (n == 0)
					mml += (_pf.dpcm_first_value * 2).ToString("D") + ",";
				else
					mml += "-1,";
				mml += (n + 1).ToString("D03");
				mml += NL;
				dfDiv.Save(Path.GetDirectoryName(_filename_dmc) + "\\" + divname);
			}
			if (amari != 0)
			{
				DpcmFile dfLast = new DpcmFile();
				dfLast.CreateBuffer(amari);
				for (int i = 0; i < amari; i++)
				{
					dfLast.SetSample(i, df.GetSample(pos));
					pos++;
				}
				if (_pf.dmc_padding)
				{
					if (_pf.ma || _pf.slope)
					{
						DpcmConverter.RemoveTailZero(dfLast, _pf.dpcm_first_value);
						DpcmConverter.Padding16Byte(dfLast, _pf.dmc_padding_proc, (byte)0x00);
					}
					else
					{
						DpcmConverter.Padding16Byte(dfLast, _pf.dmc_padding_proc, (byte)0x55);
					}
				}
				divname = num.ToString("D03") + Path.GetFileName(_filename_dmc);
				mml += "\tn" + num.ToString("D03") + ",";
				mml += "\"" + divname + "\",";
				mml += _pf.dpcm_rate.ToString("D") + ",";
				mml += "0,";
				mml += "-1";
				mml += NL;
				dfLast.Save(Path.GetDirectoryName(_filename_dmc) + "\\" + divname);
			}
			mml += "}" + NL;
			mml += "bgm(0){" + NL;
			mml += "TR5 l1 o2 <c& L r&" + NL;
			mml += "}" + NL;

			string mmlname;
			mmlname = Path.GetDirectoryName(_filename_dmc) + "\\" + Path.GetFileNameWithoutExtension(_filename_dmc) + ".mml";
			File.WriteAllText(mmlname, mml);
		}


		// -----------------------------------
		// Status functions
		// -----------------------------------
		private string _status_message = "";
		private void ClearStatus()
		{
			_status_message = "";
			ShowStatus();
		}
		private void ShowStatus()
		{
			TBoxStatus.Text = _status_message;
		}
		private void AddStatus(string str)
		{
			if (str == null) return;
			_status_message += str + Environment.NewLine;
		}
		private void AddStatus(WaveFile wf)
		{
			if (wf == null) return;
			wf.SearchMinMax();
			double t = wf.getSecond() * 1000.0;
			AddStatus("WAVE file: " + _filename_wav);
			AddStatus("     rate: " + wf.Rate.ToString("F2") + " Hz");
			AddStatus("   sample: " + wf.GetSize().ToString());
			AddStatus("   length: " + t.ToString("F2") + " msec");
			AddStatus("    value: " + wf.Min.ToString() + " / " + wf.Max.ToString());
		}
		private void AddStatus(DpcmFile df, double rate, int first_value)
		{
			if (df == null) return;
			int s = df.GetSize();
			double t = s / rate * 1000.0;
			int min;
			int max;
			DpcmConverter.GetMinMax(df, first_value, out min, out max);
			AddStatus("DMC  file: " + _filename_dmc);
			AddStatus("     size: " + (s / 8).ToString() + " byte");
			AddStatus("   sample: " + s.ToString());
			AddStatus("   length: " + t.ToString("F2") + " msec");
			AddStatus("    value: " + min + " / " + max + " (" + (max - min) + ")");
		}
		private void AddStatus(MyException ex)
		{
			if (ex == null) return;
			string str = "*** Error occured ***";
			str += Environment.NewLine;
			str += ex.Message;
			str += Environment.NewLine;
			str += ex.DumpStr;
			str += Environment.NewLine;
			str += ex.StackTrace;
			str += Environment.NewLine;
			AddStatus(str);
		}


		// -----------------------------------
		// ParameterFile Class
		// -----------------------------------
		private class ParameterFile
		{
			private string _def_filename;
			private string _section;

			public bool wave_speed;
			public double wave_speed_amp;
			public bool wave_lpf;
			public double wave_lpf_freq;
			public int wave_lpf_n;
			public bool wave_normalize;
			public bool wave_volume;
			public double wave_volume_amp;
			public bool wave_envelope;
			public double wave_envelope_amp;
			public bool wave_trim;
			public bool ma;
			public MA_TYPE ma_type;
			public int ma_length;
			public double ma_amp;
			public int ma_bias;
			public bool slope;
			public SLOPE_TYPE slope_type;
			public int dpcm_rate;
			public int dpcm_first_value;
			public bool dpcm_first_auto;
			public int dpcm_adjust;
			public DMC_SIZE dmc_size;
			public bool dmc_padding;
			public bool dmc_padding_proc;
			public bool file_out_wav;
			public bool file_out_prm;
			public bool file_out_dmc;
			public bool view_wave_src;
			public bool view_wave_in;
			public bool view_ma;
			public bool view_wave_out;
			public bool view_dpcm;
			public bool view_envelope;
			public bool view_slope;
			public Color view_color_wave_src;
			public Color view_color_wave_in;
			public Color view_color_ma;
			public Color view_color_wave_out;
			public Color view_color_dpcm;
			public Color view_color_envelope;
			public Color view_color_slope;
			public int window_top;
			public int window_left;
			public int window_width;
			public int window_height;

			public const double WAVE_SPEED_FREQ_MIN = 1.0;
			public const double WAVE_SPEED_FREQ_MAX = 999.0;
			public const double WAVE_LPF_FREQ_MIN = 1.0;
			public const double WAVE_LPF_FREQ_MAX = 99999.0;
			public const int WAVE_LPF_N_MIN = 3;
			public const int WAVE_LPF_N_MAX = 100;
			public const double WAVE_VLUME_AMP_MIN = 1.0;
			public const double WAVE_VLUME_AMP_MAX = 999.0;
			public const double WAVE_ENVELOPE_AMP_MIN = 2.0;
			public const double WAVE_ENVELOPE_AMP_MAX = 10.0;
			public const int MA_LENGTH_MIN = 1;
			public const int MA_LENGTH_MAX = 10000;
			public const double MA_AMP_MIN = 0.1;
			public const double MA_AMP_MAX = 10.0;
			public const int MA_BIAS_MIN = -10000;
			public const int MA_BIAS_MAX = 10000;
			public const int DPCM_RATE_MIN = 0;
			public const int DPCM_RATE_MAX = 15;
			public const int DPCM_FIRST_VALUE_MIN = DpcmConverter.DPCM_VALUE_MIN;
			public const int DPCM_FIRST_VALUE_MAX = DpcmConverter.DPCM_VALUE_MAX;
			public const int DPCM_ADJUST_MIN = 0;
			public const int DPCM_ADJUST_MAX = DpcmConverter.DPCM_VALUE_WAVE - 1;

			public ParameterFile()
			{
				_def_filename = ReplaceExt(Application.ExecutablePath, EXT_INI);
				_section = Application.ProductName;

				Reset(true);
			}
			public void Reset(bool All)
			{
				wave_speed = false;
				wave_speed_amp = 100.0;
				wave_lpf = false;
				wave_lpf_freq = 22050.0;
				wave_lpf_n = 10;
				wave_normalize = false;
				wave_volume = false;
				wave_volume_amp = 100.0;
				wave_envelope = false;
				wave_envelope_amp = 4.0;
				wave_trim = true;
				ma = false;
				ma_type = MA_TYPE.SMA;
				ma_length = 1000;
				ma_amp = 1.0;
				ma_bias = 0;
				slope = false;
				slope_type = SLOPE_TYPE.ZeroLine;
				dpcm_rate = 15;
				dpcm_first_value = 31;
				dpcm_first_auto = true;
				dpcm_adjust = 0;
				dmc_size = DMC_SIZE.Limit;
				dmc_padding = true;
				if (All) dmc_padding_proc = true;
				file_out_wav = false;
				file_out_prm = false;
				file_out_dmc = true;
				if (All) view_wave_src = false;
				view_wave_in = true;
				view_ma = true;
				view_wave_out = true;
				view_dpcm = true;
				view_envelope = false;
				view_slope = false;
				if (All) view_color_wave_src = Color.FromArgb(0xFF, 0xB0, 0xE0, 0xB0);
				if (All) view_color_wave_in = Color.FromArgb(0xFF, 0x00, 0x00, 0xFF);
				if (All) view_color_ma = Color.FromArgb(0xFF, 0x00, 0x80, 0x00);
				if (All) view_color_wave_out = Color.FromArgb(0xFF, 0xC0, 0xC0, 0xC0);
				if (All) view_color_dpcm = Color.FromArgb(0xFF, 0xFF, 0x00, 0x00);
				if (All) view_color_envelope = Color.FromArgb(0xFF, 0xFF, 0x40, 0xFF);
				if (All) view_color_slope = Color.FromArgb(0xFF, 0xA0, 0xA0, 0x00);
			}
			public void Load()
			{
				Load(_def_filename);
			}
			public void Load(string filename)
			{
				if (filename == null) return;
				if (!File.Exists(filename)) return;

				int index_ma;
				int index_slope;
				int index_dmcsize;
				IniFile.Read(filename, _section, "wave_speed", out wave_speed);
				IniFile.Read(filename, _section, "wave_speed_amp", out wave_speed_amp);
				IniFile.Read(filename, _section, "wave_lpf", out wave_lpf);
				IniFile.Read(filename, _section, "wave_lpf_freq", out wave_lpf_freq);
				IniFile.Read(filename, _section, "wave_lpf_n", out wave_lpf_n);
				IniFile.Read(filename, _section, "wave_normalize", out wave_normalize);
				IniFile.Read(filename, _section, "wave_volume", out wave_volume);
				IniFile.Read(filename, _section, "wave_volume_amp", out wave_volume_amp);
				IniFile.Read(filename, _section, "wave_envelope", out wave_envelope);
				IniFile.Read(filename, _section, "wave_envelope_amp", out wave_envelope_amp);
				IniFile.Read(filename, _section, "wave_trim", out wave_trim);
				IniFile.Read(filename, _section, "ma", out ma);
				IniFile.Read(filename, _section, "ma_type", out index_ma);
				IniFile.Read(filename, _section, "ma_length", out ma_length);
				IniFile.Read(filename, _section, "ma_amp", out ma_amp);
				IniFile.Read(filename, _section, "ma_bias", out ma_bias);
				IniFile.Read(filename, _section, "slope", out slope);
				IniFile.Read(filename, _section, "slope_type", out index_slope);
				IniFile.Read(filename, _section, "dpcm_rate", out dpcm_rate);
				IniFile.Read(filename, _section, "dpcm_first_value", out dpcm_first_value);
				IniFile.Read(filename, _section, "dpcm_first_auto", out dpcm_first_auto);
				IniFile.Read(filename, _section, "dpcm_adjust", out dpcm_adjust);
				IniFile.Read(filename, _section, "dmc_size", out index_dmcsize);
				IniFile.Read(filename, _section, "dmc_padding", out dmc_padding);
				IniFile.Read(filename, _section, "dmc_padding_proc", out dmc_padding_proc);
				IniFile.Read(filename, _section, "file_out_wav", out file_out_wav);
				IniFile.Read(filename, _section, "file_out_prm", out file_out_prm);
				IniFile.Read(filename, _section, "file_out_dmc", out file_out_dmc);
				IniFile.Read(filename, _section, "view_wave_src", out view_wave_src);
				IniFile.Read(filename, _section, "view_wave_in", out view_wave_in);
				IniFile.Read(filename, _section, "view_ma", out view_ma);
				IniFile.Read(filename, _section, "view_wave_out", out view_wave_out);
				IniFile.Read(filename, _section, "view_dpcm", out view_dpcm);
				IniFile.Read(filename, _section, "view_envelope", out view_envelope);
				IniFile.Read(filename, _section, "view_slope", out view_slope);
				IniFile.Read(filename, _section, "color_wave_src", out view_color_wave_src);
				IniFile.Read(filename, _section, "color_wave_in", out view_color_wave_in);
				IniFile.Read(filename, _section, "color_ma", out view_color_ma);
				IniFile.Read(filename, _section, "color_wave_out", out view_color_wave_out);
				IniFile.Read(filename, _section, "color_dpcm", out view_color_dpcm);
				IniFile.Read(filename, _section, "color_envelope", out view_color_envelope);
				IniFile.Read(filename, _section, "color_slope", out view_color_slope);
				IniFile.Read(filename, _section, "window_top", out window_top);
				IniFile.Read(filename, _section, "window_left", out window_left);
				IniFile.Read(filename, _section, "window_width", out window_width);
				IniFile.Read(filename, _section, "window_height", out window_height);

				if (index_ma <= 0) ma_type = MA_TYPE.SMA;
				if (index_ma == 1) ma_type = MA_TYPE.WMA;
				if (index_ma >= 2) ma_type = MA_TYPE.EMA;
				if (index_slope <= 0) slope_type = SLOPE_TYPE.ZeroLine;
				if (index_slope >= 1) slope_type = SLOPE_TYPE.MinLine;
				if (index_dmcsize <= 0) dmc_size = DMC_SIZE.Limit;
				if (index_dmcsize == 1) dmc_size = DMC_SIZE.NoLimit;
				if (index_dmcsize >= 2) dmc_size = DMC_SIZE.Division;

				Limit(ref wave_speed_amp, WAVE_SPEED_FREQ_MIN, WAVE_SPEED_FREQ_MAX);
				Limit(ref wave_lpf_freq, WAVE_LPF_FREQ_MIN, WAVE_LPF_FREQ_MAX);
				Limit(ref wave_lpf_n, WAVE_LPF_N_MIN, WAVE_LPF_N_MAX);
				Limit(ref wave_volume_amp, WAVE_VLUME_AMP_MIN, WAVE_VLUME_AMP_MAX);
				Limit(ref wave_envelope_amp, WAVE_ENVELOPE_AMP_MIN, WAVE_ENVELOPE_AMP_MAX);
				Limit(ref ma_length, MA_LENGTH_MIN, MA_LENGTH_MAX);
				Limit(ref ma_amp, MA_AMP_MIN, MA_AMP_MAX);
				Limit(ref ma_bias, MA_BIAS_MIN, MA_BIAS_MAX);
				Limit(ref dpcm_rate, DPCM_RATE_MIN, DPCM_RATE_MAX);
				Limit(ref dpcm_first_value, DPCM_FIRST_VALUE_MIN, DPCM_FIRST_VALUE_MAX);
				Limit(ref dpcm_adjust, DPCM_ADJUST_MIN, DPCM_ADJUST_MAX);
				Limit(ref window_top, 0, 99999);
				Limit(ref window_left, 0, 99999);
				Limit(ref window_width, 100, 99999);
				Limit(ref window_height, 100, 99999);
			}
			static private void Limit(ref int data, int lower, int upper)
			{
				if (data < lower) data = lower;
				if (data > upper) data = upper;
			}
			static private void Limit(ref double data, double lower, double upper)
			{
				if (data < lower) data = lower;
				if (data > upper) data = upper;
			}
			//static private void LimitAssert(string name, int data, int lower, int upper)
			//{
			//    if (data < lower || data > upper)
			//        throw new MyException("data < lower || data > upper", name, data, lower, upper);
			//}
			//static private void LimitAssert(string name, double data, double lower, double upper)
			//{
			//    if (data < lower || data > upper)
			//        throw new MyException("data < lower || data > upper", name, data, lower, upper);
			//}
			public void Save()
			{
				Save(_def_filename);
			}
			public void Save(string filename)
			{
				if (filename == null) return;

				IniFile.Write(filename, _section, "wave_speed", wave_speed);
				IniFile.Write(filename, _section, "wave_speed_amp", wave_speed_amp);
				IniFile.Write(filename, _section, "wave_lpf", wave_lpf);
				IniFile.Write(filename, _section, "wave_lpf_freq", wave_lpf_freq);
				IniFile.Write(filename, _section, "wave_lpf_n", wave_lpf_n);
				IniFile.Write(filename, _section, "wave_normalize", wave_normalize);
				IniFile.Write(filename, _section, "wave_volume", wave_volume);
				IniFile.Write(filename, _section, "wave_volume_amp", wave_volume_amp);
				IniFile.Write(filename, _section, "wave_envelope", wave_envelope);
				IniFile.Write(filename, _section, "wave_envelope_amp", wave_envelope_amp);
				IniFile.Write(filename, _section, "wave_trim", wave_trim);
				IniFile.Write(filename, _section, "ma", ma);
				IniFile.Write(filename, _section, "ma_type", (int)ma_type);
				IniFile.Write(filename, _section, "ma_length", ma_length);
				IniFile.Write(filename, _section, "ma_amp", ma_amp);
				IniFile.Write(filename, _section, "ma_bias", ma_bias);
				IniFile.Write(filename, _section, "slope", slope);
				IniFile.Write(filename, _section, "slope_type", (int)slope_type);
				IniFile.Write(filename, _section, "dpcm_rate", dpcm_rate);
				IniFile.Write(filename, _section, "dpcm_first_value", dpcm_first_value);
				IniFile.Write(filename, _section, "dpcm_first_auto", dpcm_first_auto);
				IniFile.Write(filename, _section, "dpcm_adjust", dpcm_adjust);
				IniFile.Write(filename, _section, "dmc_size", (int)dmc_size);
				IniFile.Write(filename, _section, "dmc_padding", dmc_padding);
				IniFile.Write(filename, _section, "dmc_padding_proc", dmc_padding_proc);
				IniFile.Write(filename, _section, "file_out_wav", file_out_wav);
				IniFile.Write(filename, _section, "file_out_prm", file_out_prm);
				IniFile.Write(filename, _section, "file_out_dmc", file_out_dmc);
				IniFile.Write(filename, _section, "view_wave_src", view_wave_src);
				IniFile.Write(filename, _section, "view_wave_in", view_wave_in);
				IniFile.Write(filename, _section, "view_ma", view_ma);
				IniFile.Write(filename, _section, "view_wave_out", view_wave_out);
				IniFile.Write(filename, _section, "view_dpcm", view_dpcm);
				IniFile.Write(filename, _section, "view_envelope", view_envelope);
				IniFile.Write(filename, _section, "view_slope", view_slope);
				IniFile.Write(filename, _section, "color_wave_src", view_color_wave_src);
				IniFile.Write(filename, _section, "color_wave_in", view_color_wave_in);
				IniFile.Write(filename, _section, "color_ma", view_color_ma);
				IniFile.Write(filename, _section, "color_wave_out", view_color_wave_out);
				IniFile.Write(filename, _section, "color_dpcm", view_color_dpcm);
				IniFile.Write(filename, _section, "color_envelope", view_color_envelope);
				IniFile.Write(filename, _section, "color_slope", view_color_slope);
				IniFile.Write(filename, _section, "window_top", window_top);
				IniFile.Write(filename, _section, "window_left", window_left);
				IniFile.Write(filename, _section, "window_width", window_width);
				IniFile.Write(filename, _section, "window_height", window_height);
			}
			static public bool SaveDrawLine(string filename, string section, Point[] points)
			{
				if (filename == null) return false;
				if (filename.Length <= 0) return false;
				if (section == null) return false;
				if (section.Length <= 0) return false;
				if (points == null) return false;
				if (points.Length < 2) return false;
				if (points.Length > 9999) return false;

				IniFile.Write(filename, section, "num", points.Length);
				for (int i = 0; i < points.Length; i++)
				{
					IniFile.Write(filename, section, "X" + i.ToString("D4"), points[i].X.ToString());
					IniFile.Write(filename, section, "Y" + i.ToString("D4"), points[i].Y.ToString());
				}
				return true;
			}
			static public bool LoadDrawLine(string filename, string section, out Point[] points)
			{
				points = null;
				if (filename == null) return false;
				if (filename.Length <= 0) return false;
				if (section == null) return false;
				if (section.Length <= 0) return false;
				if (!File.Exists(filename)) return false;

				int num = 0;
				IniFile.Read(filename, section, "num", out num);
				if (num < 2) return false;
				if (num > 9999) return false;
				points = new Point[num];
				int x = 0;
				int y = 0;
				for (int i = 0; i < num; i++)
				{
					IniFile.Read(filename, section, "X" + i.ToString("D4"), out x);
					IniFile.Read(filename, section, "Y" + i.ToString("D4"), out y);
					points[i] = new Point(x, y);
				}
				return true;
			}
		}
	}
}
