namespace MakeDPCM
{
	partial class MainForm
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.TBoxStatus = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.BtnPlayWave = new System.Windows.Forms.Button();
			this.BtnPlayDpcm = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.CBoxFileOutDmc = new System.Windows.Forms.CheckBox();
			this.CBoxFileOutWav = new System.Windows.Forms.CheckBox();
			this.CBoxFileOutPrm = new System.Windows.Forms.CheckBox();
			this.BtnStop = new System.Windows.Forms.Button();
			this.BtnReset = new System.Windows.Forms.Button();
			this.BtnConv = new System.Windows.Forms.Button();
			this.CBoxSlope = new System.Windows.Forms.CheckBox();
			this.GBoxDpcm = new System.Windows.Forms.GroupBox();
			this.NumDpcmAdjust = new System.Windows.Forms.NumericUpDown();
			this.NumDpcmFirst = new System.Windows.Forms.NumericUpDown();
			this.CmbDpcmRate = new System.Windows.Forms.ComboBox();
			this.CBoxDpcmFirstAuto = new System.Windows.Forms.CheckBox();
			this.LblDpcmRate = new System.Windows.Forms.Label();
			this.LblDpcmAdjust = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.GBoxDmc = new System.Windows.Forms.GroupBox();
			this.CmbDmcSize = new System.Windows.Forms.ComboBox();
			this.CBoxDmcPadding = new System.Windows.Forms.CheckBox();
			this.CBoxMa = new System.Windows.Forms.CheckBox();
			this.GBoxWave = new System.Windows.Forms.GroupBox();
			this.NumWaveEnvelope = new System.Windows.Forms.NumericUpDown();
			this.NumWaveSpeed = new System.Windows.Forms.NumericUpDown();
			this.CBoxWaveSpeed = new System.Windows.Forms.CheckBox();
			this.NumWaveLpfFreq = new System.Windows.Forms.NumericUpDown();
			this.NumWaveVolume = new System.Windows.Forms.NumericUpDown();
			this.CBoxWaveVolume = new System.Windows.Forms.CheckBox();
			this.CBoxLPF = new System.Windows.Forms.CheckBox();
			this.CBoxWaveTrimZero = new System.Windows.Forms.CheckBox();
			this.CBoxWaveEnvelope = new System.Windows.Forms.CheckBox();
			this.CBoxWaveNormalize = new System.Windows.Forms.CheckBox();
			this.NumWaveLpfN = new System.Windows.Forms.NumericUpDown();
			this.GBoxMa = new System.Windows.Forms.GroupBox();
			this.NumMaLength = new System.Windows.Forms.NumericUpDown();
			this.NumMaBias = new System.Windows.Forms.NumericUpDown();
			this.NumMaAmp = new System.Windows.Forms.NumericUpDown();
			this.CmbMaType = new System.Windows.Forms.ComboBox();
			this.LblMaBias = new System.Windows.Forms.Label();
			this.LblMaAmp = new System.Windows.Forms.Label();
			this.LblMaType = new System.Windows.Forms.Label();
			this.LblMaLength = new System.Windows.Forms.Label();
			this.GBoxView = new System.Windows.Forms.GroupBox();
			this.CBoxViewMa = new System.Windows.Forms.CheckBox();
			this.CBoxViewSlope = new System.Windows.Forms.CheckBox();
			this.CBoxViewEnvelope = new System.Windows.Forms.CheckBox();
			this.CBoxViewWaveOut = new System.Windows.Forms.CheckBox();
			this.CBoxViewWaveIn = new System.Windows.Forms.CheckBox();
			this.CBoxViewDpcm = new System.Windows.Forms.CheckBox();
			this.GBoxDraw = new System.Windows.Forms.GroupBox();
			this.RBtnSlopeZero = new System.Windows.Forms.RadioButton();
			this.RBtnSlopeMin = new System.Windows.Forms.RadioButton();
			this.WView = new MakeDPCM.WaveView();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.GBoxDpcm.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumDpcmAdjust)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NumDpcmFirst)).BeginInit();
			this.GBoxDmc.SuspendLayout();
			this.GBoxWave.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumWaveEnvelope)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NumWaveSpeed)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NumWaveLpfFreq)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NumWaveVolume)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NumWaveLpfN)).BeginInit();
			this.GBoxMa.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumMaLength)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NumMaBias)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NumMaAmp)).BeginInit();
			this.GBoxView.SuspendLayout();
			this.GBoxDraw.SuspendLayout();
			this.SuspendLayout();
			// 
			// TBoxStatus
			// 
			this.TBoxStatus.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TBoxStatus.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.TBoxStatus.Location = new System.Drawing.Point(560, 8);
			this.TBoxStatus.Margin = new System.Windows.Forms.Padding(0, 8, 8, 8);
			this.TBoxStatus.Multiline = true;
			this.TBoxStatus.Name = "TBoxStatus";
			this.TBoxStatus.ReadOnly = true;
			this.TBoxStatus.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.TBoxStatus.Size = new System.Drawing.Size(43, 160);
			this.TBoxStatus.TabIndex = 1;
			this.TBoxStatus.WordWrap = false;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.WView, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 176F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(611, 341);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 560F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.TBoxStatus, 1, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(611, 176);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.BtnPlayWave);
			this.panel1.Controls.Add(this.BtnPlayDpcm);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Controls.Add(this.BtnStop);
			this.panel1.Controls.Add(this.BtnReset);
			this.panel1.Controls.Add(this.BtnConv);
			this.panel1.Controls.Add(this.CBoxSlope);
			this.panel1.Controls.Add(this.GBoxDpcm);
			this.panel1.Controls.Add(this.GBoxDmc);
			this.panel1.Controls.Add(this.CBoxMa);
			this.panel1.Controls.Add(this.GBoxWave);
			this.panel1.Controls.Add(this.GBoxMa);
			this.panel1.Controls.Add(this.GBoxView);
			this.panel1.Controls.Add(this.GBoxDraw);
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(560, 176);
			this.panel1.TabIndex = 0;
			// 
			// BtnPlayWave
			// 
			this.BtnPlayWave.Location = new System.Drawing.Point(216, 128);
			this.BtnPlayWave.Name = "BtnPlayWave";
			this.BtnPlayWave.Size = new System.Drawing.Size(48, 40);
			this.BtnPlayWave.TabIndex = 10;
			this.BtnPlayWave.Text = "WAVE";
			this.BtnPlayWave.UseVisualStyleBackColor = true;
			this.BtnPlayWave.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BtnPlayWave_KeyDown);
			this.BtnPlayWave.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnPlayWave_MouseDown);
			// 
			// BtnPlayDpcm
			// 
			this.BtnPlayDpcm.Location = new System.Drawing.Point(272, 128);
			this.BtnPlayDpcm.Name = "BtnPlayDpcm";
			this.BtnPlayDpcm.Size = new System.Drawing.Size(48, 40);
			this.BtnPlayDpcm.TabIndex = 11;
			this.BtnPlayDpcm.Text = "DPCM";
			this.BtnPlayDpcm.UseVisualStyleBackColor = true;
			this.BtnPlayDpcm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BtnPlayDpcm_KeyDown);
			this.BtnPlayDpcm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnPlayDpcm_MouseDown);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.CBoxFileOutDmc);
			this.groupBox1.Controls.Add(this.CBoxFileOutWav);
			this.groupBox1.Controls.Add(this.CBoxFileOutPrm);
			this.groupBox1.Location = new System.Drawing.Point(488, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(64, 80);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "FileOut";
			// 
			// CBoxFileOutDmc
			// 
			this.CBoxFileOutDmc.AutoSize = true;
			this.CBoxFileOutDmc.Location = new System.Drawing.Point(8, 56);
			this.CBoxFileOutDmc.Name = "CBoxFileOutDmc";
			this.CBoxFileOutDmc.Size = new System.Drawing.Size(49, 16);
			this.CBoxFileOutDmc.TabIndex = 2;
			this.CBoxFileOutDmc.Text = "DMC";
			this.CBoxFileOutDmc.UseVisualStyleBackColor = true;
			// 
			// CBoxFileOutWav
			// 
			this.CBoxFileOutWav.AutoSize = true;
			this.CBoxFileOutWav.Location = new System.Drawing.Point(8, 16);
			this.CBoxFileOutWav.Name = "CBoxFileOutWav";
			this.CBoxFileOutWav.Size = new System.Drawing.Size(49, 16);
			this.CBoxFileOutWav.TabIndex = 0;
			this.CBoxFileOutWav.Text = "WAV";
			this.CBoxFileOutWav.UseVisualStyleBackColor = true;
			// 
			// CBoxFileOutPrm
			// 
			this.CBoxFileOutPrm.AutoSize = true;
			this.CBoxFileOutPrm.Location = new System.Drawing.Point(8, 36);
			this.CBoxFileOutPrm.Name = "CBoxFileOutPrm";
			this.CBoxFileOutPrm.Size = new System.Drawing.Size(44, 16);
			this.CBoxFileOutPrm.TabIndex = 1;
			this.CBoxFileOutPrm.Text = "Prm";
			this.CBoxFileOutPrm.UseVisualStyleBackColor = true;
			// 
			// BtnStop
			// 
			this.BtnStop.Location = new System.Drawing.Point(328, 128);
			this.BtnStop.Name = "BtnStop";
			this.BtnStop.Size = new System.Drawing.Size(48, 40);
			this.BtnStop.TabIndex = 12;
			this.BtnStop.Text = "Stop";
			this.BtnStop.UseVisualStyleBackColor = true;
			this.BtnStop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BtnStop_KeyDown);
			this.BtnStop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnStop_MouseDown);
			// 
			// BtnReset
			// 
			this.BtnReset.Location = new System.Drawing.Point(248, 88);
			this.BtnReset.Name = "BtnReset";
			this.BtnReset.Size = new System.Drawing.Size(48, 24);
			this.BtnReset.TabIndex = 13;
			this.BtnReset.Text = "Reset";
			this.BtnReset.UseVisualStyleBackColor = true;
			this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
			// 
			// BtnConv
			// 
			this.BtnConv.Location = new System.Drawing.Point(160, 128);
			this.BtnConv.Name = "BtnConv";
			this.BtnConv.Size = new System.Drawing.Size(48, 40);
			this.BtnConv.TabIndex = 9;
			this.BtnConv.Text = "Conv";
			this.BtnConv.UseVisualStyleBackColor = true;
			this.BtnConv.Click += new System.EventHandler(this.BtnConv_Click);
			// 
			// CBoxSlope
			// 
			this.CBoxSlope.AutoSize = true;
			this.CBoxSlope.Location = new System.Drawing.Point(248, 8);
			this.CBoxSlope.Name = "CBoxSlope";
			this.CBoxSlope.Size = new System.Drawing.Size(52, 16);
			this.CBoxSlope.TabIndex = 3;
			this.CBoxSlope.Text = "Slope";
			this.CBoxSlope.UseVisualStyleBackColor = true;
			this.CBoxSlope.CheckedChanged += new System.EventHandler(this.Common_CheckedChanged);
			this.CBoxSlope.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CBoxDrawingLineCommon_MouseDown);
			// 
			// GBoxDpcm
			// 
			this.GBoxDpcm.Controls.Add(this.NumDpcmAdjust);
			this.GBoxDpcm.Controls.Add(this.NumDpcmFirst);
			this.GBoxDpcm.Controls.Add(this.CmbDpcmRate);
			this.GBoxDpcm.Controls.Add(this.CBoxDpcmFirstAuto);
			this.GBoxDpcm.Controls.Add(this.LblDpcmRate);
			this.GBoxDpcm.Controls.Add(this.LblDpcmAdjust);
			this.GBoxDpcm.Controls.Add(this.label1);
			this.GBoxDpcm.Location = new System.Drawing.Point(304, 8);
			this.GBoxDpcm.Name = "GBoxDpcm";
			this.GBoxDpcm.Size = new System.Drawing.Size(88, 112);
			this.GBoxDpcm.TabIndex = 5;
			this.GBoxDpcm.TabStop = false;
			this.GBoxDpcm.Text = "DPCM";
			// 
			// NumDpcmAdjust
			// 
			this.NumDpcmAdjust.Location = new System.Drawing.Point(32, 80);
			this.NumDpcmAdjust.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
			this.NumDpcmAdjust.Name = "NumDpcmAdjust";
			this.NumDpcmAdjust.Size = new System.Drawing.Size(48, 19);
			this.NumDpcmAdjust.TabIndex = 6;
			this.NumDpcmAdjust.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.NumDpcmAdjust.Leave += new System.EventHandler(this.Num_Leave);
			// 
			// NumDpcmFirst
			// 
			this.NumDpcmFirst.Location = new System.Drawing.Point(32, 56);
			this.NumDpcmFirst.Name = "NumDpcmFirst";
			this.NumDpcmFirst.Size = new System.Drawing.Size(48, 19);
			this.NumDpcmFirst.TabIndex = 4;
			this.NumDpcmFirst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.NumDpcmFirst.Leave += new System.EventHandler(this.Num_Leave);
			// 
			// CmbDpcmRate
			// 
			this.CmbDpcmRate.DropDownHeight = 200;
			this.CmbDpcmRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CmbDpcmRate.DropDownWidth = 100;
			this.CmbDpcmRate.FormattingEnabled = true;
			this.CmbDpcmRate.IntegralHeight = false;
			this.CmbDpcmRate.Location = new System.Drawing.Point(32, 16);
			this.CmbDpcmRate.Name = "CmbDpcmRate";
			this.CmbDpcmRate.Size = new System.Drawing.Size(48, 20);
			this.CmbDpcmRate.TabIndex = 1;
			// 
			// CBoxDpcmFirstAuto
			// 
			this.CBoxDpcmFirstAuto.AutoSize = true;
			this.CBoxDpcmFirstAuto.Location = new System.Drawing.Point(8, 40);
			this.CBoxDpcmFirstAuto.Name = "CBoxDpcmFirstAuto";
			this.CBoxDpcmFirstAuto.Size = new System.Drawing.Size(48, 16);
			this.CBoxDpcmFirstAuto.TabIndex = 2;
			this.CBoxDpcmFirstAuto.Text = "Auto";
			this.CBoxDpcmFirstAuto.UseVisualStyleBackColor = true;
			this.CBoxDpcmFirstAuto.CheckedChanged += new System.EventHandler(this.Common_CheckedChanged);
			// 
			// LblDpcmRate
			// 
			this.LblDpcmRate.Location = new System.Drawing.Point(2, 16);
			this.LblDpcmRate.Name = "LblDpcmRate";
			this.LblDpcmRate.Size = new System.Drawing.Size(32, 20);
			this.LblDpcmRate.TabIndex = 0;
			this.LblDpcmRate.Text = "Rate";
			this.LblDpcmRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// LblDpcmAdjust
			// 
			this.LblDpcmAdjust.Location = new System.Drawing.Point(2, 80);
			this.LblDpcmAdjust.Name = "LblDpcmAdjust";
			this.LblDpcmAdjust.Size = new System.Drawing.Size(32, 19);
			this.LblDpcmAdjust.TabIndex = 5;
			this.LblDpcmAdjust.Text = "Ajst";
			this.LblDpcmAdjust.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(2, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 19);
			this.label1.TabIndex = 3;
			this.label1.Text = "First";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// GBoxDmc
			// 
			this.GBoxDmc.Controls.Add(this.CmbDmcSize);
			this.GBoxDmc.Controls.Add(this.CBoxDmcPadding);
			this.GBoxDmc.Location = new System.Drawing.Point(392, 8);
			this.GBoxDmc.Name = "GBoxDmc";
			this.GBoxDmc.Size = new System.Drawing.Size(96, 72);
			this.GBoxDmc.TabIndex = 6;
			this.GBoxDmc.TabStop = false;
			this.GBoxDmc.Text = "DMC";
			// 
			// CmbDmcSize
			// 
			this.CmbDmcSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CmbDmcSize.FormattingEnabled = true;
			this.CmbDmcSize.Location = new System.Drawing.Point(8, 40);
			this.CmbDmcSize.Name = "CmbDmcSize";
			this.CmbDmcSize.Size = new System.Drawing.Size(80, 20);
			this.CmbDmcSize.TabIndex = 1;
			// 
			// CBoxDmcPadding
			// 
			this.CBoxDmcPadding.AutoSize = true;
			this.CBoxDmcPadding.Location = new System.Drawing.Point(8, 16);
			this.CBoxDmcPadding.Name = "CBoxDmcPadding";
			this.CBoxDmcPadding.Size = new System.Drawing.Size(64, 16);
			this.CBoxDmcPadding.TabIndex = 0;
			this.CBoxDmcPadding.Text = "Padding";
			this.CBoxDmcPadding.UseVisualStyleBackColor = true;
			// 
			// CBoxMa
			// 
			this.CBoxMa.AutoSize = true;
			this.CBoxMa.Location = new System.Drawing.Point(152, 8);
			this.CBoxMa.Name = "CBoxMa";
			this.CBoxMa.Size = new System.Drawing.Size(41, 16);
			this.CBoxMa.TabIndex = 1;
			this.CBoxMa.Text = "MA";
			this.CBoxMa.UseVisualStyleBackColor = true;
			this.CBoxMa.CheckedChanged += new System.EventHandler(this.Common_CheckedChanged);
			// 
			// GBoxWave
			// 
			this.GBoxWave.Controls.Add(this.NumWaveEnvelope);
			this.GBoxWave.Controls.Add(this.NumWaveSpeed);
			this.GBoxWave.Controls.Add(this.CBoxWaveSpeed);
			this.GBoxWave.Controls.Add(this.NumWaveLpfFreq);
			this.GBoxWave.Controls.Add(this.NumWaveVolume);
			this.GBoxWave.Controls.Add(this.CBoxWaveVolume);
			this.GBoxWave.Controls.Add(this.CBoxLPF);
			this.GBoxWave.Controls.Add(this.CBoxWaveTrimZero);
			this.GBoxWave.Controls.Add(this.CBoxWaveEnvelope);
			this.GBoxWave.Controls.Add(this.CBoxWaveNormalize);
			this.GBoxWave.Controls.Add(this.NumWaveLpfN);
			this.GBoxWave.Location = new System.Drawing.Point(8, 8);
			this.GBoxWave.Name = "GBoxWave";
			this.GBoxWave.Size = new System.Drawing.Size(136, 160);
			this.GBoxWave.TabIndex = 0;
			this.GBoxWave.TabStop = false;
			this.GBoxWave.Text = "WAVE";
			// 
			// NumWaveEnvelope
			// 
			this.NumWaveEnvelope.DecimalPlaces = 2;
			this.NumWaveEnvelope.Location = new System.Drawing.Point(80, 112);
			this.NumWaveEnvelope.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            65536});
			this.NumWaveEnvelope.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.NumWaveEnvelope.Name = "NumWaveEnvelope";
			this.NumWaveEnvelope.Size = new System.Drawing.Size(48, 19);
			this.NumWaveEnvelope.TabIndex = 9;
			this.NumWaveEnvelope.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.NumWaveEnvelope.Value = new decimal(new int[] {
            40,
            0,
            0,
            65536});
			this.NumWaveEnvelope.Leave += new System.EventHandler(this.Num_Leave);
			// 
			// NumWaveSpeed
			// 
			this.NumWaveSpeed.DecimalPlaces = 2;
			this.NumWaveSpeed.Location = new System.Drawing.Point(72, 16);
			this.NumWaveSpeed.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            131072});
			this.NumWaveSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this.NumWaveSpeed.Name = "NumWaveSpeed";
			this.NumWaveSpeed.Size = new System.Drawing.Size(56, 19);
			this.NumWaveSpeed.TabIndex = 1;
			this.NumWaveSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.NumWaveSpeed.Value = new decimal(new int[] {
            10000,
            0,
            0,
            131072});
			this.NumWaveSpeed.Leave += new System.EventHandler(this.Num_Leave);
			// 
			// CBoxWaveSpeed
			// 
			this.CBoxWaveSpeed.AutoSize = true;
			this.CBoxWaveSpeed.Location = new System.Drawing.Point(8, 16);
			this.CBoxWaveSpeed.Name = "CBoxWaveSpeed";
			this.CBoxWaveSpeed.Size = new System.Drawing.Size(55, 16);
			this.CBoxWaveSpeed.TabIndex = 0;
			this.CBoxWaveSpeed.Text = "Speed";
			this.CBoxWaveSpeed.UseVisualStyleBackColor = true;
			this.CBoxWaveSpeed.CheckedChanged += new System.EventHandler(this.Common_CheckedChanged);
			// 
			// NumWaveLpfFreq
			// 
			this.NumWaveLpfFreq.Location = new System.Drawing.Point(72, 40);
			this.NumWaveLpfFreq.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
			this.NumWaveLpfFreq.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.NumWaveLpfFreq.Name = "NumWaveLpfFreq";
			this.NumWaveLpfFreq.Size = new System.Drawing.Size(56, 19);
			this.NumWaveLpfFreq.TabIndex = 3;
			this.NumWaveLpfFreq.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.NumWaveLpfFreq.Value = new decimal(new int[] {
            22050,
            0,
            0,
            0});
			this.NumWaveLpfFreq.Leave += new System.EventHandler(this.Num_Leave);
			// 
			// NumWaveVolume
			// 
			this.NumWaveVolume.DecimalPlaces = 2;
			this.NumWaveVolume.Location = new System.Drawing.Point(72, 88);
			this.NumWaveVolume.Maximum = new decimal(new int[] {
            90000,
            0,
            0,
            131072});
			this.NumWaveVolume.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.NumWaveVolume.Name = "NumWaveVolume";
			this.NumWaveVolume.Size = new System.Drawing.Size(56, 19);
			this.NumWaveVolume.TabIndex = 6;
			this.NumWaveVolume.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.NumWaveVolume.Value = new decimal(new int[] {
            10000,
            0,
            0,
            131072});
			this.NumWaveVolume.Leave += new System.EventHandler(this.Num_Leave);
			// 
			// CBoxWaveVolume
			// 
			this.CBoxWaveVolume.AutoSize = true;
			this.CBoxWaveVolume.Location = new System.Drawing.Point(8, 88);
			this.CBoxWaveVolume.Name = "CBoxWaveVolume";
			this.CBoxWaveVolume.Size = new System.Drawing.Size(62, 16);
			this.CBoxWaveVolume.TabIndex = 7;
			this.CBoxWaveVolume.Text = "Volume";
			this.CBoxWaveVolume.UseVisualStyleBackColor = true;
			this.CBoxWaveVolume.CheckedChanged += new System.EventHandler(this.Common_CheckedChanged);
			// 
			// CBoxLPF
			// 
			this.CBoxLPF.AutoSize = true;
			this.CBoxLPF.Location = new System.Drawing.Point(8, 40);
			this.CBoxLPF.Name = "CBoxLPF";
			this.CBoxLPF.Size = new System.Drawing.Size(44, 16);
			this.CBoxLPF.TabIndex = 2;
			this.CBoxLPF.Text = "LPF";
			this.CBoxLPF.UseVisualStyleBackColor = true;
			this.CBoxLPF.CheckedChanged += new System.EventHandler(this.Common_CheckedChanged);
			// 
			// CBoxWaveTrimZero
			// 
			this.CBoxWaveTrimZero.AutoSize = true;
			this.CBoxWaveTrimZero.Location = new System.Drawing.Point(8, 136);
			this.CBoxWaveTrimZero.Name = "CBoxWaveTrimZero";
			this.CBoxWaveTrimZero.Size = new System.Drawing.Size(70, 16);
			this.CBoxWaveTrimZero.TabIndex = 10;
			this.CBoxWaveTrimZero.Text = "TrimZero";
			this.CBoxWaveTrimZero.UseVisualStyleBackColor = true;
			// 
			// CBoxWaveEnvelope
			// 
			this.CBoxWaveEnvelope.AutoSize = true;
			this.CBoxWaveEnvelope.Location = new System.Drawing.Point(8, 112);
			this.CBoxWaveEnvelope.Name = "CBoxWaveEnvelope";
			this.CBoxWaveEnvelope.Size = new System.Drawing.Size(70, 16);
			this.CBoxWaveEnvelope.TabIndex = 8;
			this.CBoxWaveEnvelope.Text = "Envelope";
			this.CBoxWaveEnvelope.UseVisualStyleBackColor = true;
			this.CBoxWaveEnvelope.CheckedChanged += new System.EventHandler(this.Common_CheckedChanged);
			this.CBoxWaveEnvelope.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CBoxDrawingLineCommon_MouseDown);
			// 
			// CBoxWaveNormalize
			// 
			this.CBoxWaveNormalize.AutoSize = true;
			this.CBoxWaveNormalize.Location = new System.Drawing.Point(8, 64);
			this.CBoxWaveNormalize.Name = "CBoxWaveNormalize";
			this.CBoxWaveNormalize.Size = new System.Drawing.Size(74, 16);
			this.CBoxWaveNormalize.TabIndex = 4;
			this.CBoxWaveNormalize.Text = "Normalize";
			this.CBoxWaveNormalize.UseVisualStyleBackColor = true;
			// 
			// NumWaveLpfN
			// 
			this.NumWaveLpfN.Location = new System.Drawing.Point(88, 64);
			this.NumWaveLpfN.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.NumWaveLpfN.Name = "NumWaveLpfN";
			this.NumWaveLpfN.Size = new System.Drawing.Size(40, 19);
			this.NumWaveLpfN.TabIndex = 5;
			this.NumWaveLpfN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.NumWaveLpfN.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.NumWaveLpfN.Leave += new System.EventHandler(this.Num_Leave);
			// 
			// GBoxMa
			// 
			this.GBoxMa.Controls.Add(this.NumMaLength);
			this.GBoxMa.Controls.Add(this.NumMaBias);
			this.GBoxMa.Controls.Add(this.NumMaAmp);
			this.GBoxMa.Controls.Add(this.CmbMaType);
			this.GBoxMa.Controls.Add(this.LblMaBias);
			this.GBoxMa.Controls.Add(this.LblMaAmp);
			this.GBoxMa.Controls.Add(this.LblMaType);
			this.GBoxMa.Controls.Add(this.LblMaLength);
			this.GBoxMa.Location = new System.Drawing.Point(144, 8);
			this.GBoxMa.Name = "GBoxMa";
			this.GBoxMa.Size = new System.Drawing.Size(96, 112);
			this.GBoxMa.TabIndex = 2;
			this.GBoxMa.TabStop = false;
			// 
			// NumMaLength
			// 
			this.NumMaLength.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.NumMaLength.Location = new System.Drawing.Point(32, 40);
			this.NumMaLength.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.NumMaLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.NumMaLength.Name = "NumMaLength";
			this.NumMaLength.Size = new System.Drawing.Size(56, 19);
			this.NumMaLength.TabIndex = 3;
			this.NumMaLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.NumMaLength.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.NumMaLength.Leave += new System.EventHandler(this.Num_Leave);
			// 
			// NumMaBias
			// 
			this.NumMaBias.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.NumMaBias.Location = new System.Drawing.Point(32, 88);
			this.NumMaBias.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.NumMaBias.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
			this.NumMaBias.Name = "NumMaBias";
			this.NumMaBias.Size = new System.Drawing.Size(56, 19);
			this.NumMaBias.TabIndex = 7;
			this.NumMaBias.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.NumMaBias.Leave += new System.EventHandler(this.Num_Leave);
			// 
			// NumMaAmp
			// 
			this.NumMaAmp.DecimalPlaces = 2;
			this.NumMaAmp.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.NumMaAmp.Location = new System.Drawing.Point(32, 64);
			this.NumMaAmp.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.NumMaAmp.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this.NumMaAmp.Name = "NumMaAmp";
			this.NumMaAmp.Size = new System.Drawing.Size(56, 19);
			this.NumMaAmp.TabIndex = 5;
			this.NumMaAmp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.NumMaAmp.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.NumMaAmp.Leave += new System.EventHandler(this.Num_Leave);
			// 
			// CmbMaType
			// 
			this.CmbMaType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CmbMaType.FormattingEnabled = true;
			this.CmbMaType.Location = new System.Drawing.Point(32, 16);
			this.CmbMaType.Name = "CmbMaType";
			this.CmbMaType.Size = new System.Drawing.Size(56, 20);
			this.CmbMaType.TabIndex = 1;
			// 
			// LblMaBias
			// 
			this.LblMaBias.Location = new System.Drawing.Point(2, 88);
			this.LblMaBias.Name = "LblMaBias";
			this.LblMaBias.Size = new System.Drawing.Size(32, 19);
			this.LblMaBias.TabIndex = 6;
			this.LblMaBias.Text = "Bias";
			this.LblMaBias.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// LblMaAmp
			// 
			this.LblMaAmp.Location = new System.Drawing.Point(2, 64);
			this.LblMaAmp.Name = "LblMaAmp";
			this.LblMaAmp.Size = new System.Drawing.Size(32, 19);
			this.LblMaAmp.TabIndex = 4;
			this.LblMaAmp.Text = "Amp";
			this.LblMaAmp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// LblMaType
			// 
			this.LblMaType.Location = new System.Drawing.Point(2, 16);
			this.LblMaType.Name = "LblMaType";
			this.LblMaType.Size = new System.Drawing.Size(32, 19);
			this.LblMaType.TabIndex = 0;
			this.LblMaType.Text = "Type";
			this.LblMaType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// LblMaLength
			// 
			this.LblMaLength.Location = new System.Drawing.Point(2, 40);
			this.LblMaLength.Name = "LblMaLength";
			this.LblMaLength.Size = new System.Drawing.Size(32, 19);
			this.LblMaLength.TabIndex = 2;
			this.LblMaLength.Text = "Leng";
			this.LblMaLength.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// GBoxView
			// 
			this.GBoxView.Controls.Add(this.CBoxViewMa);
			this.GBoxView.Controls.Add(this.CBoxViewSlope);
			this.GBoxView.Controls.Add(this.CBoxViewEnvelope);
			this.GBoxView.Controls.Add(this.CBoxViewWaveOut);
			this.GBoxView.Controls.Add(this.CBoxViewWaveIn);
			this.GBoxView.Controls.Add(this.CBoxViewDpcm);
			this.GBoxView.Location = new System.Drawing.Point(392, 88);
			this.GBoxView.Name = "GBoxView";
			this.GBoxView.Size = new System.Drawing.Size(160, 80);
			this.GBoxView.TabIndex = 8;
			this.GBoxView.TabStop = false;
			this.GBoxView.Text = "View";
			// 
			// CBoxViewMa
			// 
			this.CBoxViewMa.AutoSize = true;
			this.CBoxViewMa.Location = new System.Drawing.Point(96, 36);
			this.CBoxViewMa.Name = "CBoxViewMa";
			this.CBoxViewMa.Size = new System.Drawing.Size(41, 16);
			this.CBoxViewMa.TabIndex = 4;
			this.CBoxViewMa.Text = "MA";
			this.CBoxViewMa.UseVisualStyleBackColor = true;
			this.CBoxViewMa.CheckedChanged += new System.EventHandler(this.CBoxViewCommon_CheckedChanged);
			// 
			// CBoxViewSlope
			// 
			this.CBoxViewSlope.AutoSize = true;
			this.CBoxViewSlope.Location = new System.Drawing.Point(96, 16);
			this.CBoxViewSlope.Name = "CBoxViewSlope";
			this.CBoxViewSlope.Size = new System.Drawing.Size(52, 16);
			this.CBoxViewSlope.TabIndex = 3;
			this.CBoxViewSlope.Text = "Slope";
			this.CBoxViewSlope.UseVisualStyleBackColor = true;
			this.CBoxViewSlope.CheckedChanged += new System.EventHandler(this.CBoxViewDrawingLineCommon_CheckedChanged);
			// 
			// CBoxViewEnvelope
			// 
			this.CBoxViewEnvelope.AutoSize = true;
			this.CBoxViewEnvelope.Location = new System.Drawing.Point(8, 16);
			this.CBoxViewEnvelope.Name = "CBoxViewEnvelope";
			this.CBoxViewEnvelope.Size = new System.Drawing.Size(70, 16);
			this.CBoxViewEnvelope.TabIndex = 0;
			this.CBoxViewEnvelope.Text = "Envelope";
			this.CBoxViewEnvelope.UseVisualStyleBackColor = true;
			this.CBoxViewEnvelope.CheckedChanged += new System.EventHandler(this.CBoxViewDrawingLineCommon_CheckedChanged);
			// 
			// CBoxViewWaveOut
			// 
			this.CBoxViewWaveOut.AutoSize = true;
			this.CBoxViewWaveOut.Location = new System.Drawing.Point(8, 56);
			this.CBoxViewWaveOut.Name = "CBoxViewWaveOut";
			this.CBoxViewWaveOut.Size = new System.Drawing.Size(80, 16);
			this.CBoxViewWaveOut.TabIndex = 2;
			this.CBoxViewWaveOut.Text = "WAVE(out)";
			this.CBoxViewWaveOut.UseVisualStyleBackColor = true;
			this.CBoxViewWaveOut.CheckedChanged += new System.EventHandler(this.CBoxViewCommon_CheckedChanged);
			// 
			// CBoxViewWaveIn
			// 
			this.CBoxViewWaveIn.AutoSize = true;
			this.CBoxViewWaveIn.Location = new System.Drawing.Point(8, 36);
			this.CBoxViewWaveIn.Name = "CBoxViewWaveIn";
			this.CBoxViewWaveIn.Size = new System.Drawing.Size(73, 16);
			this.CBoxViewWaveIn.TabIndex = 1;
			this.CBoxViewWaveIn.Text = "WAVE(in)";
			this.CBoxViewWaveIn.UseVisualStyleBackColor = true;
			this.CBoxViewWaveIn.CheckedChanged += new System.EventHandler(this.CBoxViewCommon_CheckedChanged);
			// 
			// CBoxViewDpcm
			// 
			this.CBoxViewDpcm.AutoSize = true;
			this.CBoxViewDpcm.Location = new System.Drawing.Point(96, 56);
			this.CBoxViewDpcm.Name = "CBoxViewDpcm";
			this.CBoxViewDpcm.Size = new System.Drawing.Size(56, 16);
			this.CBoxViewDpcm.TabIndex = 5;
			this.CBoxViewDpcm.Text = "DPCM";
			this.CBoxViewDpcm.UseVisualStyleBackColor = true;
			this.CBoxViewDpcm.CheckedChanged += new System.EventHandler(this.CBoxViewCommon_CheckedChanged);
			// 
			// GBoxDraw
			// 
			this.GBoxDraw.Controls.Add(this.RBtnSlopeZero);
			this.GBoxDraw.Controls.Add(this.RBtnSlopeMin);
			this.GBoxDraw.Location = new System.Drawing.Point(240, 8);
			this.GBoxDraw.Name = "GBoxDraw";
			this.GBoxDraw.Size = new System.Drawing.Size(64, 72);
			this.GBoxDraw.TabIndex = 4;
			this.GBoxDraw.TabStop = false;
			// 
			// RBtnSlopeZero
			// 
			this.RBtnSlopeZero.AutoSize = true;
			this.RBtnSlopeZero.Location = new System.Drawing.Point(8, 24);
			this.RBtnSlopeZero.Name = "RBtnSlopeZero";
			this.RBtnSlopeZero.Size = new System.Drawing.Size(46, 16);
			this.RBtnSlopeZero.TabIndex = 0;
			this.RBtnSlopeZero.TabStop = true;
			this.RBtnSlopeZero.Text = "Zero";
			this.RBtnSlopeZero.UseVisualStyleBackColor = true;
			// 
			// RBtnSlopeMin
			// 
			this.RBtnSlopeMin.AutoSize = true;
			this.RBtnSlopeMin.Location = new System.Drawing.Point(8, 48);
			this.RBtnSlopeMin.Name = "RBtnSlopeMin";
			this.RBtnSlopeMin.Size = new System.Drawing.Size(41, 16);
			this.RBtnSlopeMin.TabIndex = 1;
			this.RBtnSlopeMin.TabStop = true;
			this.RBtnSlopeMin.Text = "Min";
			this.RBtnSlopeMin.UseVisualStyleBackColor = true;
			// 
			// WView
			// 
			this.WView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.WView.DrawWaveLine = false;
			this.WView.Location = new System.Drawing.Point(8, 176);
			this.WView.Margin = new System.Windows.Forms.Padding(8, 0, 8, 4);
			this.WView.Name = "WView";
			this.WView.Size = new System.Drawing.Size(595, 161);
			this.WView.TabIndex = 1;
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(561, 341);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(577, 379);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "MakeDPCM Ver1.1.0.0";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.GBoxDpcm.ResumeLayout(false);
			this.GBoxDpcm.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumDpcmAdjust)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NumDpcmFirst)).EndInit();
			this.GBoxDmc.ResumeLayout(false);
			this.GBoxDmc.PerformLayout();
			this.GBoxWave.ResumeLayout(false);
			this.GBoxWave.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumWaveEnvelope)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NumWaveSpeed)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NumWaveLpfFreq)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NumWaveVolume)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NumWaveLpfN)).EndInit();
			this.GBoxMa.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.NumMaLength)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NumMaBias)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NumMaAmp)).EndInit();
			this.GBoxView.ResumeLayout(false);
			this.GBoxView.PerformLayout();
			this.GBoxDraw.ResumeLayout(false);
			this.GBoxDraw.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private WaveView WView;
		private System.Windows.Forms.TextBox TBoxStatus;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.CheckBox CBoxLPF;
		private System.Windows.Forms.CheckBox CBoxSlope;
		private System.Windows.Forms.GroupBox GBoxDpcm;
		private System.Windows.Forms.NumericUpDown NumDpcmAdjust;
		private System.Windows.Forms.NumericUpDown NumDpcmFirst;
		private System.Windows.Forms.ComboBox CmbDpcmRate;
		private System.Windows.Forms.Label LblDpcmAdjust;
		private System.Windows.Forms.CheckBox CBoxDpcmFirstAuto;
		private System.Windows.Forms.Label LblDpcmRate;
		private System.Windows.Forms.GroupBox GBoxDmc;
		private System.Windows.Forms.ComboBox CmbDmcSize;
		private System.Windows.Forms.CheckBox CBoxFileOutDmc;
		private System.Windows.Forms.CheckBox CBoxDmcPadding;
		private System.Windows.Forms.CheckBox CBoxMa;
		private System.Windows.Forms.GroupBox GBoxWave;
		private System.Windows.Forms.NumericUpDown NumWaveVolume;
		private System.Windows.Forms.CheckBox CBoxWaveVolume;
		private System.Windows.Forms.CheckBox CBoxWaveTrimZero;
		private System.Windows.Forms.CheckBox CBoxWaveEnvelope;
		private System.Windows.Forms.CheckBox CBoxWaveNormalize;
		private System.Windows.Forms.Button BtnConv;
		private System.Windows.Forms.Button BtnPlayWave;
		private System.Windows.Forms.Button BtnStop;
		private System.Windows.Forms.Button BtnPlayDpcm;
		private System.Windows.Forms.GroupBox GBoxMa;
		private System.Windows.Forms.ComboBox CmbMaType;
		private System.Windows.Forms.Label LblMaBias;
		private System.Windows.Forms.NumericUpDown NumMaLength;
		private System.Windows.Forms.Label LblMaAmp;
		private System.Windows.Forms.NumericUpDown NumMaBias;
		private System.Windows.Forms.Label LblMaType;
		private System.Windows.Forms.Label LblMaLength;
		private System.Windows.Forms.NumericUpDown NumMaAmp;
		private System.Windows.Forms.GroupBox GBoxDraw;
		private System.Windows.Forms.RadioButton RBtnSlopeZero;
		private System.Windows.Forms.RadioButton RBtnSlopeMin;
		private System.Windows.Forms.NumericUpDown NumWaveLpfFreq;
		private System.Windows.Forms.NumericUpDown NumWaveLpfN;
		private System.Windows.Forms.GroupBox GBoxView;
		private System.Windows.Forms.CheckBox CBoxViewDpcm;
		private System.Windows.Forms.CheckBox CBoxViewMa;
		private System.Windows.Forms.CheckBox CBoxViewEnvelope;
		private System.Windows.Forms.CheckBox CBoxViewWaveIn;
		private System.Windows.Forms.CheckBox CBoxViewWaveOut;
		private System.Windows.Forms.CheckBox CBoxViewSlope;
		private System.Windows.Forms.CheckBox CBoxWaveSpeed;
		private System.Windows.Forms.NumericUpDown NumWaveSpeed;
		private System.Windows.Forms.Button BtnReset;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox CBoxFileOutPrm;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox CBoxFileOutWav;
		private System.Windows.Forms.NumericUpDown NumWaveEnvelope;


	}
}

