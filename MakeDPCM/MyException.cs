using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MakeDPCM
{
	class MyException : Exception
	{
		public MyException() { }
		public MyException(string message) : base(message) { }
		public string DumpStr = null;
		public MyException(string message, params object[] obj)	: base(message)
		{
			if (obj == null) return;
			for (int i = 0; i < obj.Length; i++)
			{
				DumpStr += obj[i].ToString();
				DumpStr += ", ";
			}
			DumpStr += Environment.NewLine;
		}
	}
}
