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

namespace ICSharpCode.TextEditor.Util
{
	internal class CountTipText : TipText
	{
		private readonly float triHeight = 10;
		private readonly float triWidth = 10;

		public CountTipText(Graphics graphics, Font font, string text) : base(graphics, font, text)
		{
		}

		private void DrawTriangle(float x, float y, bool flipped)
		{
			Brush brush = BrushRegistry.GetBrush(Color.FromArgb(192, 192, 192));
			Graphics.FillRectangle(brush, new RectangleF(x, y, triHeight, triHeight));
			float triHeight2 = triHeight / 2;
			float triHeight4 = triHeight / 4;
			brush = Brushes.Black;

			if (flipped)
			{
				Graphics.FillPolygon(brush, new[] { new PointF(x, y + triHeight2 - triHeight4), new PointF(x + triWidth / 2, y + triHeight2 + triHeight4), new PointF(x + triWidth, y + triHeight2 - triHeight4) });

			}
			else
			{
				Graphics.FillPolygon(brush, new[] { new PointF(x, y + triHeight2 + triHeight4), new PointF(x + triWidth / 2, y + triHeight2 - triHeight4), new PointF(x + triWidth, y + triHeight2 + triHeight4) });
			}
		}

		public Rectangle DrawingRectangle1;
		public Rectangle DrawingRectangle2;

		public override void Draw(PointF location)
		{
			if (!string.IsNullOrEmpty(tipText))
			{
				base.Draw(new PointF(location.X + triWidth + 4, location.Y));
				DrawingRectangle1 = new Rectangle((int)location.X + 2, (int)location.Y + 2, (int)(triWidth), (int)(triHeight));
				DrawingRectangle2 = new Rectangle((int)(location.X + AllocatedSize.Width - triWidth - 2), (int)location.Y + 2, (int)(triWidth), (int)(triHeight));
				DrawTriangle(location.X + 2, location.Y + 2, false);
				DrawTriangle(location.X + AllocatedSize.Width - triWidth - 2, location.Y + 2, true);
			}
		}

		protected override void OnMaximumSizeChanged()
		{
			if (IsTextVisible())
			{
				SizeF tipSize = Graphics.MeasureString(tipText, tipFont, MaximumSize, GetInternalStringFormat());
				tipSize.Width += triWidth * 2 + 8;
				SetRequiredSize(tipSize);
			}
			else
			{
				SetRequiredSize(SizeF.Empty);
			}
		}

	}

	internal class TipText : TipSection
	{
		protected StringAlignment horzAlign;
		protected StringAlignment vertAlign;
		protected Color tipColor;
		protected Font tipFont;
		protected StringFormat tipFormat;
		protected string tipText;

		public TipText(Graphics graphics, Font font, string text) : base(graphics)
		{
			tipFont = font; tipText = text;

			if (text != null && text.Length > short.MaxValue)
			{
				throw new ArgumentException("TipText: text too long (max. is " + short.MaxValue + " characters)", "text");
			}

			Color = SystemColors.InfoText;
			HorizontalAlignment = StringAlignment.Near;
			VerticalAlignment = StringAlignment.Near;
		}

		public override void Draw(PointF location)
		{
			if (IsTextVisible())
			{
				RectangleF drawRectangle = new RectangleF(location, AllocatedSize);
				Graphics.DrawString(tipText, tipFont, BrushRegistry.GetBrush(Color), drawRectangle, GetInternalStringFormat());
			}
		}

		protected StringFormat GetInternalStringFormat()
		{
			if (tipFormat == null)
			{
				tipFormat = CreateTipStringFormat(horzAlign, vertAlign);
			}

			return tipFormat;
		}

		protected override void OnMaximumSizeChanged()
		{
			base.OnMaximumSizeChanged();

			if (IsTextVisible())
			{
				SizeF tipSize = Graphics.MeasureString(tipText, tipFont, MaximumSize, GetInternalStringFormat());
				SetRequiredSize(tipSize);
			}
			else
			{
				SetRequiredSize(SizeF.Empty);
			}
		}

		private static StringFormat CreateTipStringFormat(StringAlignment horizontalAlignment, StringAlignment verticalAlignment)
		{
			StringFormat format = (StringFormat)StringFormat.GenericTypographic.Clone();
			format.FormatFlags = StringFormatFlags.FitBlackBox | StringFormatFlags.MeasureTrailingSpaces;
			// note: Align Near, Line Center seemed to do something before

			format.Alignment = horizontalAlignment;
			format.LineAlignment = verticalAlignment;

			return format;
		}

		protected bool IsTextVisible()
		{
			return !string.IsNullOrEmpty(tipText);
		}

		public Color Color
		{
			get
			{
				return tipColor;
			}
			set
			{
				tipColor = value;
			}
		}

		public StringAlignment HorizontalAlignment
		{
			get
			{
				return horzAlign;
			}
			set
			{
				horzAlign = value;
				tipFormat = null;
			}
		}

		public StringAlignment VerticalAlignment
		{
			get
			{
				return vertAlign;
			}
			set
			{
				vertAlign = value;
				tipFormat = null;
			}
		}
	}
}
