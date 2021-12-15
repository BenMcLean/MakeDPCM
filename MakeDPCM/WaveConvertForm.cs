using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MakeDPCM
{
	public partial class WaveConvertForm : Form
	{
		public WaveFile.ConvertType Result = WaveFile.ConvertType.Cancel;
		private string _msg;
		public WaveConvertForm(string msg)
		{
			_msg = msg;
			InitializeComponent();
			this.DialogResult = DialogResult.Cancel;
		}
		private void WaveConvertForm_Load(object sender, EventArgs e)
		{
			LblMsg.Text = _msg;
		}
		private void BtnLeft_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Result = WaveFile.ConvertType.Left;
			this.Close();
		}
		private void BtnCenter_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Result = WaveFile.ConvertType.Center;
			this.Close();
		}
		private void BtnRight_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Result = WaveFile.ConvertType.Right;
			this.Close();
		}
		private void BtnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Result = WaveFile.ConvertType.Cancel;
			this.Close();
		}
	}
}
