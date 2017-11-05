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
using System.Windows.Forms;

namespace ICSharpCode.TextEditor.Util
{
	/// <summary>
	/// Accumulates mouse wheel deltas and reports the actual number of lines to scroll.
	/// </summary>
	internal class MouseWheelHandler
	{
		// CODE DUPLICATION: See ICSharpCode.SharpDevelop.Widgets.MouseWheelHandler

		private const int WHEEL_DELTA = 120;
		private int mouseWheelDelta;

		public int GetScrollAmount(MouseEventArgs e)
		{
			// accumulate the delta to support high-resolution mice
			mouseWheelDelta += e.Delta;

			int linesPerClick = Math.Max(SystemInformation.MouseWheelScrollLines, 1);

			int scrollDistance = mouseWheelDelta * linesPerClick / WHEEL_DELTA;
			mouseWheelDelta %= Math.Max(1, WHEEL_DELTA / linesPerClick);
			return scrollDistance;
		}
	}
}
