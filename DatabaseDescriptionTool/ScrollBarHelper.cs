/*
Copyright (C) 2017 Lars Hove Christiansen
http://virtcore.com

This file is a part of Database Description Tool

	Database Description Tool is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	Database Description Tool is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with Database Description Tool. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class Win32
{
	// offset of window style value
	public const int GWL_STYLE = -16;

	// window style constants for scrollbars
	public const int WS_VSCROLL = 0x00200000;
	public const int WS_HSCROLL = 0x00100000;

	[DllImport("user32.dll", SetLastError = true)]
	public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
}

public static class ScrollBarHelper
{
	public static ScrollBars GetVisibleScrollBars(Control ctl)
	{
		int wndStyle = Win32.GetWindowLong(ctl.Handle, Win32.GWL_STYLE);
		bool hsVisible = (wndStyle & Win32.WS_HSCROLL) != 0;
		bool vsVisible = (wndStyle & Win32.WS_VSCROLL) != 0;

		if (hsVisible)
		{
			return vsVisible ? ScrollBars.Both : ScrollBars.Horizontal;
		}
		else
		{
			return vsVisible ? ScrollBars.Vertical : ScrollBars.None;
		}
	}
}
