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

namespace ICSharpCode.TextEditor.Document
{
	/// <summary>
	/// This class is used to generate bold, italic and bold/italic fonts out
	/// of a base font.
	/// </summary>
	public class FontContainer
	{
		private Font defaultFont;
		private Font regularfont, boldfont, italicfont, bolditalicfont;

		/// <value>
		/// The scaled, regular version of the base font
		/// </value>
		public Font RegularFont
		{
			get
			{
				return regularfont;
			}
		}

		/// <value>
		/// The scaled, bold version of the base font
		/// </value>
		public Font BoldFont
		{
			get
			{
				return boldfont;
			}
		}

		/// <value>
		/// The scaled, italic version of the base font
		/// </value>
		public Font ItalicFont
		{
			get
			{
				return italicfont;
			}
		}

		/// <value>
		/// The scaled, bold/italic version of the base font
		/// </value>
		public Font BoldItalicFont
		{
			get
			{
				return bolditalicfont;
			}
		}

		private static float twipsPerPixelY;

		public static float TwipsPerPixelY
		{
			get
			{
				if (twipsPerPixelY == 0)
				{
					using (Bitmap bmp = new Bitmap(1, 1))
					{
						using (Graphics g = Graphics.FromImage(bmp))
						{
							twipsPerPixelY = 1440 / g.DpiY;
						}
					}
				}

				return twipsPerPixelY;
			}
		}

		/// <value>
		/// The base font
		/// </value>
		public Font DefaultFont
		{
			get
			{
				return defaultFont;
			}
			set
			{
				// 1440 twips is one inch
				float pixelSize = (float)Math.Round(value.SizeInPoints * 20 / TwipsPerPixelY);

				defaultFont = value;
				regularfont = new Font(value.FontFamily, pixelSize * TwipsPerPixelY / 20f, FontStyle.Regular);
				boldfont = new Font(regularfont, FontStyle.Bold);
				italicfont = new Font(regularfont, FontStyle.Italic);
				bolditalicfont = new Font(regularfont, FontStyle.Bold | FontStyle.Italic);
			}
		}

		public static Font ParseFont(string font)
		{
			string[] descr = font.Split(',', '=');
			return new Font(descr[1], float.Parse(descr[3]));
		}

		public FontContainer(Font defaultFont)
		{
			DefaultFont = defaultFont;
		}
	}
}
