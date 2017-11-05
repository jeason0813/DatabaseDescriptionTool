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

using System.Collections.Generic;
using System.Drawing;

namespace ICSharpCode.TextEditor
{
	/// <summary>
	/// Contains brushes/pens for the text editor to speed up drawing. Re-Creation of brushes and pens
	/// seems too costly.
	/// </summary>
	public class BrushRegistry
	{
		private static readonly Dictionary<Color, Brush> brushes = new Dictionary<Color, Brush>();
		private static readonly Dictionary<Color, Pen> pens = new Dictionary<Color, Pen>();
		private static readonly Dictionary<Color, Pen> dotPens = new Dictionary<Color, Pen>();

		public static Brush GetBrush(Color color)
		{
			lock (brushes)
			{
				Brush brush;

				if (!brushes.TryGetValue(color, out brush))
				{
					brush = new SolidBrush(color);
					brushes.Add(color, brush);
				}

				return brush;
			}
		}

		public static Pen GetPen(Color color)
		{
			lock (pens)
			{
				Pen pen;

				if (!pens.TryGetValue(color, out pen))
				{
					pen = new Pen(color);
					pens.Add(color, pen);
				}

				return pen;
			}
		}

		private static readonly float[] dotPattern = { 1, 1, 1, 1 };

		public static Pen GetDotPen(Color color)
		{
			lock (dotPens)
			{
				Pen pen;

				if (!dotPens.TryGetValue(color, out pen))
				{
					pen = new Pen(color);
					pen.DashPattern = dotPattern;
					dotPens.Add(color, pen);
				}

				return pen;
			}
		}
	}
}
