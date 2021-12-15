namespace MakeDPCM
{
	partial class WaveView
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

		#region コンポーネント デザイナーで生成されたコード

		/// <summary> 
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.BtnVout = new System.Windows.Forms.Button();
			this.BtnVin = new System.Windows.Forms.Button();
			this.HBar = new System.Windows.Forms.HScrollBar();
			this.BtnHout = new System.Windows.Forms.Button();
			this.BtnHin = new System.Windows.Forms.Button();
			this.PBox = new System.Windows.Forms.PictureBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.THBar = new System.Windows.Forms.TrackBar();
			this.LblVZoom = new System.Windows.Forms.Label();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.VBar = new System.Windows.Forms.VScrollBar();
			this.TVBar = new System.Windows.Forms.TrackBar();
			((System.ComponentModel.ISupportInitialize)(this.PBox)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.THBar)).BeginInit();
			this.tableLayoutPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TVBar)).BeginInit();
			this.SuspendLayout();
			// 
			// BtnVout
			// 
			this.BtnVout.Location = new System.Drawing.Point(0, 184);
			this.BtnVout.Margin = new System.Windows.Forms.Padding(0);
			this.BtnVout.Name = "BtnVout";
			this.BtnVout.Size = new System.Drawing.Size(18, 18);
			this.BtnVout.TabIndex = 2;
			this.BtnVout.Text = "-";
			this.BtnVout.UseVisualStyleBackColor = true;
			this.BtnVout.Click += new System.EventHandler(this.BtnVout_Click);
			// 
			// BtnVin
			// 
			this.BtnVin.Location = new System.Drawing.Point(0, 166);
			this.BtnVin.Margin = new System.Windows.Forms.Padding(0);
			this.BtnVin.Name = "BtnVin";
			this.BtnVin.Size = new System.Drawing.Size(18, 18);
			this.BtnVin.TabIndex = 1;
			this.BtnVin.Text = "+";
			this.BtnVin.UseVisualStyleBackColor = true;
			this.BtnVin.Click += new System.EventHandler(this.BtnVin_Click);
			// 
			// HBar
			// 
			this.HBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.HBar.Location = new System.Drawing.Point(0, 0);
			this.HBar.Name = "HBar";
			this.HBar.Size = new System.Drawing.Size(126, 18);
			this.HBar.TabIndex = 0;
			this.HBar.ValueChanged += new System.EventHandler(this.HBar_ValueChanged);
			// 
			// BtnHout
			// 
			this.BtnHout.Location = new System.Drawing.Point(126, 0);
			this.BtnHout.Margin = new System.Windows.Forms.Padding(0);
			this.BtnHout.Name = "BtnHout";
			this.BtnHout.Size = new System.Drawing.Size(18, 18);
			this.BtnHout.TabIndex = 1;
			this.BtnHout.Text = "-";
			this.BtnHout.UseVisualStyleBackColor = true;
			this.BtnHout.Click += new System.EventHandler(this.BtnHout_Click);
			// 
			// BtnHin
			// 
			this.BtnHin.Location = new System.Drawing.Point(144, 0);
			this.BtnHin.Margin = new System.Windows.Forms.Padding(0);
			this.BtnHin.Name = "BtnHin";
			this.BtnHin.Size = new System.Drawing.Size(18, 18);
			this.BtnHin.TabIndex = 2;
			this.BtnHin.Text = "+";
			this.BtnHin.UseVisualStyleBackColor = true;
			this.BtnHin.Click += new System.EventHandler(this.BtnHin_Click);
			// 
			// PBox
			// 
			this.PBox.BackColor = System.Drawing.Color.White;
			this.PBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PBox.Location = new System.Drawing.Point(0, 0);
			this.PBox.Margin = new System.Windows.Forms.Padding(0);
			this.PBox.Name = "PBox";
			this.PBox.Size = new System.Drawing.Size(302, 302);
			this.PBox.TabIndex = 20;
			this.PBox.TabStop = false;
			this.PBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PBox_Paint);
			this.PBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PBox_MouseDown);
			this.PBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PBox_MouseMove);
			this.PBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PBox_MouseUp);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 18F));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.PBox, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(320, 320);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 5;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 18F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 18F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel2.Controls.Add(this.BtnHin, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.BtnHout, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.HBar, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.THBar, 3, 0);
			this.tableLayoutPanel2.Controls.Add(this.LblVZoom, 4, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 302);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(302, 18);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// THBar
			// 
			this.THBar.AutoSize = false;
			this.THBar.Location = new System.Drawing.Point(162, 0);
			this.THBar.Margin = new System.Windows.Forms.Padding(0);
			this.THBar.Minimum = -10;
			this.THBar.Name = "THBar";
			this.THBar.Size = new System.Drawing.Size(100, 18);
			this.THBar.TabIndex = 3;
			this.THBar.TickStyle = System.Windows.Forms.TickStyle.None;
			this.THBar.ValueChanged += new System.EventHandler(this.THBar_ValueChanged);
			// 
			// LblVZoom
			// 
			this.LblVZoom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.LblVZoom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblVZoom.Location = new System.Drawing.Point(265, 0);
			this.LblVZoom.Name = "LblVZoom";
			this.LblVZoom.Size = new System.Drawing.Size(34, 18);
			this.LblVZoom.TabIndex = 4;
			this.LblVZoom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.VBar, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.TVBar, 0, 3);
			this.tableLayoutPanel3.Controls.Add(this.BtnVin, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.BtnVout, 0, 2);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(302, 0);
			this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 4;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(18, 302);
			this.tableLayoutPanel3.TabIndex = 21;
			// 
			// VBar
			// 
			this.VBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.VBar.Location = new System.Drawing.Point(0, 0);
			this.VBar.Name = "VBar";
			this.VBar.Size = new System.Drawing.Size(18, 166);
			this.VBar.TabIndex = 0;
			this.VBar.ValueChanged += new System.EventHandler(this.VBar_ValueChanged);
			// 
			// TVBar
			// 
			this.TVBar.AutoSize = false;
			this.TVBar.Location = new System.Drawing.Point(0, 202);
			this.TVBar.Margin = new System.Windows.Forms.Padding(0);
			this.TVBar.Maximum = 20;
			this.TVBar.Name = "TVBar";
			this.TVBar.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.TVBar.Size = new System.Drawing.Size(18, 100);
			this.TVBar.TabIndex = 3;
			this.TVBar.TickStyle = System.Windows.Forms.TickStyle.None;
			this.TVBar.ValueChanged += new System.EventHandler(this.TVBar_ValueChanged);
			// 
			// WaveView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "WaveView";
			this.Size = new System.Drawing.Size(320, 320);
			this.Load += new System.EventHandler(this.WaveView_Load);
			this.Resize += new System.EventHandler(this.WaveView_Resize);
			((System.ComponentModel.ISupportInitialize)(this.PBox)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.THBar)).EndInit();
			this.tableLayoutPanel3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.TVBar)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button BtnVout;
		private System.Windows.Forms.Button BtnVin;
		private System.Windows.Forms.HScrollBar HBar;
		private System.Windows.Forms.Button BtnHout;
		private System.Windows.Forms.Button BtnHin;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TrackBar THBar;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.TrackBar TVBar;
		private System.Windows.Forms.VScrollBar VBar;
		private System.Windows.Forms.PictureBox PBox;
		private System.Windows.Forms.Label LblVZoom;
	}
}
