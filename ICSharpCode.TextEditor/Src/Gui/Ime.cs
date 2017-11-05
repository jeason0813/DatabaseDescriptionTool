/*
Copyright (C) 2005 SharpDevelop

Modified 2017 by Lars Hove Christiansen
http://virtcore.com

This file is a part of ICSharpCode.TextEditor

	This library is free software; you can redistribute it and/or modify it
	under the terms of the GNU Lesser General Public License as published
	by the Free Software Foundation; either version 2.1 of the License, or
	(at your option) any later version.

	This library is distributed in the hope that it will be useful, but
	WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser
	General Public License for more details.

	You should have received a copy of the GNU Lesser General Public
	License along with this library; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
*/

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ICSharpCode.TextEditor
{
	/// <summary>
	/// Used internally, not for own use.
	/// </summary>
	internal class Ime
	{
		public Ime(IntPtr hWnd, Font font)
		{
			// For unknown reasons, the IME support is causing crashes when used in a WOW64 process
			// or when used in .NET 4.0. We'll disable IME support in those cases.
			string PROCESSOR_ARCHITEW6432 = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432");

			if (PROCESSOR_ARCHITEW6432 == "IA64" || PROCESSOR_ARCHITEW6432 == "AMD64" || Environment.OSVersion.Platform == PlatformID.Unix || Environment.Version >= new Version(4, 0))
			{
				disableIME = true;
			}
			else
			{
				hIMEWnd = ImmGetDefaultIMEWnd(hWnd);
			}

			this.hWnd = hWnd;
			this.font = font;

			SetIMEWindowFont(font);
		}

		private Font font;
		public Font Font
		{
			get
			{
				return font;
			}
			set
			{
				if (!value.Equals(font))
				{
					font = value;
					lf = null;
					SetIMEWindowFont(value);
				}
			}
		}

		public IntPtr HWnd
		{
			set
			{
				if (hWnd != value)
				{
					hWnd = value;

					if (!disableIME)
					{
						hIMEWnd = ImmGetDefaultIMEWnd(value);
					}

					SetIMEWindowFont(font);
				}
			}
		}

		[DllImport("imm32.dll")]
		private static extern IntPtr ImmGetDefaultIMEWnd(IntPtr hWnd);

		[DllImport("user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, COMPOSITIONFORM lParam);
		[DllImport("user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, [In, MarshalAs(UnmanagedType.LPStruct)] LOGFONT lParam);

		[StructLayout(LayoutKind.Sequential)]
		private class COMPOSITIONFORM
		{
			public int dwStyle;
			public POINT ptCurrentPos;
			public RECT rcArea;
		}

		[StructLayout(LayoutKind.Sequential)]
		private class POINT
		{
			public int x;
			public int y;
		}

		[StructLayout(LayoutKind.Sequential)]
		private class RECT
		{
		}

		private const int WM_IME_CONTROL = 0x0283;

		private const int IMC_SETCOMPOSITIONWINDOW = 0x000c;
		private IntPtr hIMEWnd;
		private IntPtr hWnd;
		private const int CFS_POINT = 0x0002;

		[StructLayout(LayoutKind.Sequential)]
		private class LOGFONT
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string lfFaceName;
		}

		private const int IMC_SETCOMPOSITIONFONT = 0x000a;
		private LOGFONT lf;
		private static bool disableIME;

		private void SetIMEWindowFont(Font f)
		{
			if (disableIME || hIMEWnd == IntPtr.Zero)
			{
				return;
			}

			if (lf == null)
			{
				lf = new LOGFONT();
				f.ToLogFont(lf);
				lf.lfFaceName = f.Name;  // This is very important! "Font.ToLogFont" Method sets invalid value to LOGFONT.lfFaceName
			}

			try
			{
				SendMessage(hIMEWnd, WM_IME_CONTROL, new IntPtr(IMC_SETCOMPOSITIONFONT), lf);
			}
			catch (AccessViolationException ex)
			{
				Handle(ex);
			}
		}

		public void SetIMEWindowLocation(int x, int y)
		{
			if (disableIME || hIMEWnd == IntPtr.Zero)
			{
				return;
			}

			POINT p = new POINT();
			p.x = x;
			p.y = y;

			COMPOSITIONFORM lParam = new COMPOSITIONFORM();
			lParam.dwStyle = CFS_POINT;
			lParam.ptCurrentPos = p;
			lParam.rcArea = new RECT();

			try
			{
				SendMessage(hIMEWnd, WM_IME_CONTROL, new IntPtr(IMC_SETCOMPOSITIONWINDOW), lParam);
			}
			catch (AccessViolationException ex)
			{
				Handle(ex);
			}
		}

		private static void Handle(Exception ex)
		{
			Console.WriteLine(ex);

			if (!disableIME)
			{
				disableIME = true;
				OutputHandler outputHandler = new OutputHandler();
				outputHandler.Show("Error calling IME: " + ex.Message + "\nIME is disabled.", "IME error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
