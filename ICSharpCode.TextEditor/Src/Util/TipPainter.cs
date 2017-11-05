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
using System.Drawing.Text;
using System.Windows.Forms;

namespace ICSharpCode.TextEditor.Util
{
	internal static class TipPainter
	{
		private const float HorizontalBorder = 2;
		private const float VerticalBorder = 1;

		//static StringFormat centerTipFormat = CreateTipStringFormat();

		public static Size GetTipSize(Control control, Graphics graphics, Font font, string description)
		{
			return GetTipSize(control, graphics, new TipText(graphics, font, description));
		}

		private static Rectangle GetWorkingArea(Control control)
		{
			Form ownerForm = control.FindForm();

			if (ownerForm.Owner != null)
			{
				ownerForm = ownerForm.Owner;
			}

			return Screen.GetWorkingArea(ownerForm);
		}

		public static Size GetTipSize(Control control, Graphics graphics, TipSection tipData)
		{
			Size tipSize = Size.Empty;
			RectangleF workingArea = GetWorkingArea(control);
			PointF screenLocation = control.PointToScreen(Point.Empty);
			SizeF maxLayoutSize = new SizeF(workingArea.Right - screenLocation.X - HorizontalBorder * 2, workingArea.Bottom - screenLocation.Y - VerticalBorder * 2);

			if (maxLayoutSize.Width > 0 && maxLayoutSize.Height > 0)
			{
				graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

				tipData.SetMaximumSize(maxLayoutSize);
				SizeF tipSizeF = tipData.GetRequiredSize();
				tipData.SetAllocatedSize(tipSizeF);

				tipSizeF += new SizeF(HorizontalBorder * 2, VerticalBorder * 2);
				tipSize = Size.Ceiling(tipSizeF);
			}

			if (control.ClientSize != tipSize)
			{
				control.ClientSize = tipSize;
			}

			return tipSize;
		}

		public static Size GetLeftHandSideTipSize(Control control, Graphics graphics, TipSection tipData, Point p)
		{
			Size tipSize = Size.Empty;
			RectangleF workingArea = GetWorkingArea(control);
			PointF screenLocation = p;
			SizeF maxLayoutSize = new SizeF(screenLocation.X - HorizontalBorder * 2, workingArea.Bottom - screenLocation.Y - VerticalBorder * 2);

			if (maxLayoutSize.Width > 0 && maxLayoutSize.Height > 0)
			{
				graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

				tipData.SetMaximumSize(maxLayoutSize);
				SizeF tipSizeF = tipData.GetRequiredSize();
				tipData.SetAllocatedSize(tipSizeF);

				tipSizeF += new SizeF(HorizontalBorder * 2, VerticalBorder * 2);
				tipSize = Size.Ceiling(tipSizeF);
			}

			return tipSize;
		}

		public static Size DrawTip(Control control, Graphics graphics, Font font, string description)
		{
			return DrawTip(control, graphics, new TipText(graphics, font, description));
		}

		public static Size DrawTip(Control control, Graphics graphics, TipSection tipData)
		{
			Size tipSize = Size.Empty;

			PointF screenLocation = control.PointToScreen(Point.Empty);
			RectangleF workingArea = GetWorkingArea(control);
			SizeF maxLayoutSize = new SizeF(workingArea.Right - screenLocation.X - HorizontalBorder * 2, workingArea.Bottom - screenLocation.Y - VerticalBorder * 2);

			if (maxLayoutSize.Width > 0 && maxLayoutSize.Height > 0)
			{
				graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

				tipData.SetMaximumSize(maxLayoutSize);
				SizeF tipSizeF = tipData.GetRequiredSize();
				tipData.SetAllocatedSize(tipSizeF);

				tipSizeF += new SizeF(HorizontalBorder * 2, VerticalBorder * 2);
				tipSize = Size.Ceiling(tipSizeF);
			}

			if (control.ClientSize != tipSize)
			{
				control.ClientSize = tipSize;
			}

			if (tipSize != Size.Empty)
			{
				Rectangle borderRectangle = new Rectangle(Point.Empty, tipSize - new Size(1, 1));

				//RectangleF displayRectangle = new RectangleF(HorizontalBorder, VerticalBorder, tipSizeF.Width - HorizontalBorder * 2, tipSizeF.Height - VerticalBorder * 2);

				// DrawRectangle draws from Left to Left + Width. A bug? :-/
				graphics.DrawRectangle(SystemPens.WindowFrame, borderRectangle);
				tipData.Draw(new PointF(HorizontalBorder, VerticalBorder));
			}

			return tipSize;
		}

		public static Size DrawFixedWidthTip(Control control, Graphics graphics, TipSection tipData)
		{
			Size tipSize = Size.Empty;

			PointF screenLocation = control.PointToScreen(new Point(control.Width, 0));
			RectangleF workingArea = GetWorkingArea(control);
			SizeF maxLayoutSize = new SizeF(screenLocation.X - HorizontalBorder * 2, workingArea.Bottom - screenLocation.Y - VerticalBorder * 2);

			if (maxLayoutSize.Width > 0 && maxLayoutSize.Height > 0)
			{
				graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

				tipData.SetMaximumSize(maxLayoutSize);
				SizeF tipSizeF = tipData.GetRequiredSize();
				tipData.SetAllocatedSize(tipSizeF);

				tipSizeF += new SizeF(HorizontalBorder * 2, VerticalBorder * 2);
				tipSize = Size.Ceiling(tipSizeF);
			}

			if (control.Height != tipSize.Height)
			{
				control.Height = tipSize.Height;
			}

			if (tipSize != Size.Empty)
			{
				Rectangle borderRectangle = new Rectangle(Point.Empty, control.Size - new Size(1, 1));

				//RectangleF displayRectangle = new RectangleF(HorizontalBorder, VerticalBorder, tipSizeF.Width - HorizontalBorder * 2, tipSizeF.Height - VerticalBorder * 2);

				// DrawRectangle draws from Left to Left + Width. A bug? :-/
				graphics.DrawRectangle(SystemPens.WindowFrame, borderRectangle);
				tipData.Draw(new PointF(HorizontalBorder, VerticalBorder));
			}

			return tipSize;
		}
	}
}
