using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MakeDPCM
{
	public partial class TextBoxForm : IDisposable
	{
		// -----------------------------------------------------
		// static method
		// -----------------------------------------------------
		public static void Show(string msg)
		{
			TextBoxForm x = new TextBoxForm();
			x._form.Text = Application.ProductName;
			x._tbox.Text = msg;
			x._form.ShowDialog();
		}
		public static void Show(string title, string msg)
		{
			TextBoxForm x = new TextBoxForm();
			x._form.Text = title;
			x._tbox.Text = msg;
			x._form.ShowDialog();
		}
		public static void Show(Exception e)
		{
			TextBoxForm x = new TextBoxForm();
			x.ShowException(e);
			x._form.ShowDialog();
		}
		public static void Show(Exception e, string msg)
		{
			TextBoxForm x = new TextBoxForm();
			x.ShowException(e);
			x.AddText(msg);
			x._form.ShowDialog();
		}



		// -----------------------------------------------------
		// Constructor
		// -----------------------------------------------------
		public TextBoxForm()
		{
			initForm();
		}
		~TextBoxForm()
		{
			Dispose();
		}
		public void Dispose()
		{
			if (_tbox != null)
			{
				_tbox.Dispose();
			}
			_tbox = null;

			if (_form != null)
			{
				_form.Dispose();
			}
			_form = null;
		}

	
		
		// -----------------------------------------------------
		// Form and TextBox
		// -----------------------------------------------------
		private TextBox _tbox;
		private Form _form;
		private void initForm()
		{
			_tbox = new System.Windows.Forms.TextBox();
			_tbox.Dock = System.Windows.Forms.DockStyle.Fill;
			_tbox.Location = new System.Drawing.Point(0, 0);
			_tbox.Multiline = true;
			_tbox.Name = "TextBox";
			_tbox.ReadOnly = true;
			_tbox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			_tbox.Size = new System.Drawing.Size(256, 256);
			_tbox.TabIndex = 0;
			_tbox.WordWrap = false;

			_form = new Form();
			_form.SuspendLayout();
			_form.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			_form.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			_form.ClientSize = new System.Drawing.Size(256, 256);
			_form.Controls.Add(_tbox);
			_form.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
			_form.KeyPreview = true;
			_form.Name = "TextBoxForm";
			_form.Text = "TextBoxForm";
			_form.ResumeLayout(false);
			_form.PerformLayout();
		}
		public void Show()
		{
			_form.Show();
		}
		public void ShowDialog()
		{
			_form.ShowDialog();
		}
		public void ShowDialog(IWin32Window owner)
		{
			_form.ShowDialog(owner);
		}
		private void Form_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				_form.Close();
			}
		}



		// -----------------------------------------------------
		// message
		// -----------------------------------------------------
		public void ClearText()
		{
			_tbox.Text = "";
		}
		public void ShowText(string text)
		{
			_tbox.Text = text;
		}
		public void AddText(string text)
		{
			_tbox.Text += text;
		}
		public string GetText()
		{
			return _tbox.Text;
		}
		public void ShowException(Exception e)
		{
			_form.Text = "Exception";
			string str = "";
			str += "Data: " + e.Data + Environment.NewLine;
			str += "Message: " + e.Message + Environment.NewLine;
			str += "Source: " + e.Source + Environment.NewLine;
			str += "StackTrace: " + e.StackTrace + Environment.NewLine;
			str += "TargetSite: " + e.TargetSite + Environment.NewLine;
			_tbox.Text = str;
		}
	}
}
