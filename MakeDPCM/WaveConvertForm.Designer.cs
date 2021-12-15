namespace MakeDPCM
{
	partial class WaveConvertForm
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
			this.BtnLeft = new System.Windows.Forms.Button();
			this.BtnRight = new System.Windows.Forms.Button();
			this.BtnCenter = new System.Windows.Forms.Button();
			this.LblMsg = new System.Windows.Forms.Label();
			this.BtnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// BtnLeft
			// 
			this.BtnLeft.Location = new System.Drawing.Point(64, 88);
			this.BtnLeft.Name = "BtnLeft";
			this.BtnLeft.Size = new System.Drawing.Size(32, 32);
			this.BtnLeft.TabIndex = 1;
			this.BtnLeft.Text = "L";
			this.BtnLeft.UseVisualStyleBackColor = true;
			this.BtnLeft.Click += new System.EventHandler(this.BtnLeft_Click);
			// 
			// BtnRight
			// 
			this.BtnRight.Location = new System.Drawing.Point(184, 88);
			this.BtnRight.Name = "BtnRight";
			this.BtnRight.Size = new System.Drawing.Size(32, 32);
			this.BtnRight.TabIndex = 2;
			this.BtnRight.Text = "R";
			this.BtnRight.UseVisualStyleBackColor = true;
			this.BtnRight.Click += new System.EventHandler(this.BtnRight_Click);
			// 
			// BtnCenter
			// 
			this.BtnCenter.Location = new System.Drawing.Point(104, 88);
			this.BtnCenter.Name = "BtnCenter";
			this.BtnCenter.Size = new System.Drawing.Size(72, 32);
			this.BtnCenter.TabIndex = 0;
			this.BtnCenter.Text = "(L+R)/2";
			this.BtnCenter.UseVisualStyleBackColor = true;
			this.BtnCenter.Click += new System.EventHandler(this.BtnCenter_Click);
			// 
			// LblMsg
			// 
			this.LblMsg.Location = new System.Drawing.Point(8, 8);
			this.LblMsg.Name = "LblMsg";
			this.LblMsg.Size = new System.Drawing.Size(264, 72);
			this.LblMsg.TabIndex = 4;
			this.LblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// BtnCancel
			// 
			this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.BtnCancel.Location = new System.Drawing.Point(104, 144);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(72, 24);
			this.BtnCancel.TabIndex = 3;
			this.BtnCancel.Text = "Cancel";
			this.BtnCancel.UseVisualStyleBackColor = true;
			this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// WaveConvertForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.BtnCancel;
			this.ClientSize = new System.Drawing.Size(281, 175);
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.LblMsg);
			this.Controls.Add(this.BtnCenter);
			this.Controls.Add(this.BtnRight);
			this.Controls.Add(this.BtnLeft);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WaveConvertForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Please select convert type";
			this.Load += new System.EventHandler(this.WaveConvertForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button BtnLeft;
		private System.Windows.Forms.Button BtnRight;
		private System.Windows.Forms.Button BtnCenter;
		private System.Windows.Forms.Label LblMsg;
		private System.Windows.Forms.Button BtnCancel;

	}
}