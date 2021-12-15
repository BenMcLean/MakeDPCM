using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace MakeDPCM
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Thread.GetDomain().UnhandledException += new UnhandledExceptionEventHandler(Application_UnhandledException);
			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

			Application.Run(new MainForm());
		}
		public static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			if (e.Exception is MyException)
			{
				MyException m = (MyException)e.Exception;
				if (m != null)
				{
					TextBoxForm.Show(e.Exception, m.DumpStr);
				}
			}
			else
			{
				TextBoxForm.Show(e.Exception);
			}
		}
		public static void Application_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Exception ex = e.ExceptionObject as Exception;
			if (ex != null)
			{
				if (ex is MyException)
				{
					MyException m = (MyException)ex;
					if (m != null)
					{
						TextBoxForm.Show(ex, m.DumpStr);
					}
				}
				else
				{
					TextBoxForm.Show(ex);
				}
			}
		}
	}
}
