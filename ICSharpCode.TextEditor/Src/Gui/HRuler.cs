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

using System.Drawing;
using System.Windows.Forms;

namespace ICSharpCode.TextEditor
{
	/// <summary>
	/// Horizontal ruler - text column measuring ruler at the top of the text area.
	/// </summary>
	public class HRuler : Control
	{
		private readonly TextArea textArea;

		public HRuler(TextArea textArea)
		{
			this.textArea = textArea;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			int num = 0;

			for (float x = textArea.TextView.DrawingPosition.Left; x < textArea.TextView.DrawingPosition.Right; x += textArea.TextView.WideSpaceWidth)
			{
				int offset = (Height * 2) / 3;
				if (num % 5 == 0)
				{
					offset = (Height * 4) / 5;
				}

				if (num % 10 == 0)
				{
					offset = 1;
				}
				++num;
				g.DrawLine(Pens.Black, (int)x, offset, (int)x, Height - offset);
			}
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			e.Graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, Width, Height));
		}
	}
}
