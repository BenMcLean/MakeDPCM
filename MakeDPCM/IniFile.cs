using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace MakeDPCM
{
	static class IniFile
	{
		// --------------------------------
		// Write
		static public void Write(string filename, string section, string key, string value)
		{
			if (filename == null) throw new MyException("filename == null");
			if (section == null) throw new MyException("section == null");
			WritePrivateProfileString(section, key, value, filename);
		}
		static public void Write(string filename, string section, string key, double value)
		{
			Write(filename, section, key, value.ToString());
		}
		static public void Write(string filename, string section, string key, int value)
		{
			Write(filename, section, key, value.ToString());
		}
		static public void Write(string filename, string section, string key, bool value)
		{
			Write(filename, section, key, value.ToString());
		}
		static public void Write(string filename, string section, string key, Color value)
		{
			int n = 0;
			n += value.A << 8 * 3;
			n += value.R << 8 * 2;
			n += value.G << 8 * 1;
			n += value.B << 8 * 0;
			Write(filename, section, key, "0x" + n.ToString("X08"));
		}

		// --------------------------------
		// Read
		static public void Read(string filename, string section, string key, out string value)
		{
			if (filename == null) throw new MyException("filename == null");
			if (section == null) throw new MyException("section == null");
			if (key == null) throw new MyException("key == null");
			StringBuilder sb = new StringBuilder(1024);
			GetPrivateProfileString(section, key, "", sb, (uint)sb.Capacity, filename);
			value = sb.ToString();
		}
		static public void Read(string filename, string section, string key, out double value)
		{
			string str;
			Read(filename, section, key, out str);
			try
			{
				value = Convert.ToDouble(str);
			}
			catch
			{
				value = 0.0;
			}
		}
		static public void Read(string filename, string section, string key, out int value)
		{
			string str;
			Read(filename, section, key, out str);
			try
			{
				if(str.StartsWith("0x"))
				{
					value = Convert.ToInt32(str, 16);
				}
				else
				{
					value = Convert.ToInt32(str, 10);
				}
			}
			catch
			{
				value = 0;
			}
		}
		static public void Read(string filename, string section, string key, out bool value)
		{
			string str;
			Read(filename, section, key, out str);
			try
			{
				value = Convert.ToBoolean(str);
			}
			catch
			{
				value = false;
			}
		}
		static public void Read(string filename, string section, string key, out Color value)
		{
			int n;
			Read(filename, section, key, out n);
			value = Color.FromArgb(n);
		}

		// --------------------------------
		// Delete
		static public void DeleteKey(string filename, string section, string key)
		{
			Write(filename, section, key, null);
		}
		static public void DeleteSection(string filename, string section)
		{
			Write(filename, section, null, null);
		}

		// --------------------------------
		// DLL Import
		[DllImport("KERNEL32.DLL")]
		public static extern uint
			GetPrivateProfileString(string lpAppName,
			string lpKeyName, string lpDefault,
			StringBuilder lpReturnedString, uint nSize,
			string lpFileName);

		[DllImport("KERNEL32.DLL")]
		public static extern uint
			GetPrivateProfileInt(string lpAppName,
			string lpKeyName, int nDefault, string lpFileName);

		[DllImport("KERNEL32.DLL")]
		public static extern uint WritePrivateProfileString(
			string lpAppName,
			string lpKeyName,
			string lpString,
			string lpFileName);
	}
}
