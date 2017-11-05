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

namespace ICSharpCode.TextEditor.Util
{
	internal static class TipPainterTools
	{
		//const int SpacerSize = 4;

		public static Size GetLeftHandSideDrawingSizeHelpTipFromCombinedDescription(Control control, Graphics graphics, Font font, string countMessage, string description, Point p)
		{
			string basicDescription = null;
			string documentation = null;

			if (IsVisibleText(description))
			{
				string[] splitDescription = description.Split(new[] { '\n' }, 2);

				if (splitDescription.Length > 0)
				{
					basicDescription = splitDescription[0];

					if (splitDescription.Length > 1)
					{
						documentation = splitDescription[1].Trim();
					}
				}
			}

			return GetLeftHandSideDrawingSizeDrawHelpTip(control, graphics, font, countMessage, basicDescription, documentation, p);
		}

		public static Size GetDrawingSizeHelpTipFromCombinedDescription(Control control, Graphics graphics, Font font, string countMessage, string description)
		{
			string basicDescription = null;
			string documentation = null;

			if (IsVisibleText(description))
			{
				string[] splitDescription = description.Split(new[] { '\n' }, 2);

				if (splitDescription.Length > 0)
				{
					basicDescription = splitDescription[0];

					if (splitDescription.Length > 1)
					{
						documentation = splitDescription[1].Trim();
					}
				}
			}

			return GetDrawingSizeDrawHelpTip(control, graphics, font, countMessage, basicDescription, documentation);
		}

		public static Size DrawHelpTipFromCombinedDescription(Control control, Graphics graphics, Font font, string countMessage, string description)
		{
			string basicDescription = null;
			string documentation = null;

			if (IsVisibleText(description))
			{
				string[] splitDescription = description.Split(new[] { '\n' }, 2);

				if (splitDescription.Length > 0)
				{
					basicDescription = splitDescription[0];

					if (splitDescription.Length > 1)
					{
						documentation = splitDescription[1].Trim();
					}
				}
			}

			return DrawHelpTip(control, graphics, font, countMessage, basicDescription, documentation);
		}

		public static Size DrawFixedWidthHelpTipFromCombinedDescription(Control control, Graphics graphics, Font font, string countMessage, string description)
		{
			string basicDescription = null;
			string documentation = null;

			if (IsVisibleText(description))
			{
				string[] splitDescription = description.Split(new[] { '\n' }, 2);

				if (splitDescription.Length > 0)
				{
					basicDescription = splitDescription[0];

					if (splitDescription.Length > 1)
					{
						documentation = splitDescription[1].Trim();
					}
				}
			}

			return DrawFixedWidthHelpTip(control, graphics, font, countMessage, basicDescription, documentation);
		}

		// btw. I know it's ugly.
		public static Rectangle DrawingRectangle1;
		public static Rectangle DrawingRectangle2;

		public static Size GetDrawingSizeDrawHelpTip(Control control, Graphics graphics, Font font, string countMessage, string basicDescription, string documentation)
		{
			if (IsVisibleText(countMessage) || IsVisibleText(basicDescription) || IsVisibleText(documentation))
			{
				// Create all the TipSection objects.
				CountTipText countMessageTip = new CountTipText(graphics, font, countMessage);

				TipSpacer countSpacer = new TipSpacer(graphics, new SizeF(IsVisibleText(countMessage) ? 4 : 0, 0));

				TipText descriptionTip = new TipText(graphics, font, basicDescription);

				TipSpacer docSpacer = new TipSpacer(graphics, new SizeF(0, IsVisibleText(documentation) ? 4 : 0));

				TipText docTip = new TipText(graphics, font, documentation);

				// Now put them together.
				TipSplitter descSplitter = new TipSplitter(graphics, false, descriptionTip, docSpacer);

				TipSplitter mainSplitter = new TipSplitter(graphics, true, countMessageTip, countSpacer, descSplitter);

				TipSplitter mainSplitter2 = new TipSplitter(graphics, false, mainSplitter, docTip);

				// Show it.
				Size size = TipPainter.GetTipSize(control, graphics, mainSplitter2);
				DrawingRectangle1 = countMessageTip.DrawingRectangle1;
				DrawingRectangle2 = countMessageTip.DrawingRectangle2;
				return size;
			}

			return Size.Empty;
		}
		public static Size GetLeftHandSideDrawingSizeDrawHelpTip(Control control, Graphics graphics, Font font, string countMessage, string basicDescription, string documentation, Point p)
		{
			if (IsVisibleText(countMessage) || IsVisibleText(basicDescription) || IsVisibleText(documentation))
			{
				// Create all the TipSection objects.
				CountTipText countMessageTip = new CountTipText(graphics, font, countMessage);

				TipSpacer countSpacer = new TipSpacer(graphics, new SizeF(IsVisibleText(countMessage) ? 4 : 0, 0));

				TipText descriptionTip = new TipText(graphics, font, basicDescription);

				TipSpacer docSpacer = new TipSpacer(graphics, new SizeF(0, IsVisibleText(documentation) ? 4 : 0));

				TipText docTip = new TipText(graphics, font, documentation);

				// Now put them together.
				TipSplitter descSplitter = new TipSplitter(graphics, false, descriptionTip, docSpacer);

				TipSplitter mainSplitter = new TipSplitter(graphics, true, countMessageTip, countSpacer, descSplitter);

				TipSplitter mainSplitter2 = new TipSplitter(graphics, false, mainSplitter, docTip);

				// Show it.
				Size size = TipPainter.GetLeftHandSideTipSize(control, graphics, mainSplitter2, p);
				return size;
			}

			return Size.Empty;
		}
		public static Size DrawHelpTip(Control control, Graphics graphics, Font font, string countMessage, string basicDescription, string documentation)
		{
			if (IsVisibleText(countMessage) || IsVisibleText(basicDescription) || IsVisibleText(documentation))
			{
				// Create all the TipSection objects.
				CountTipText countMessageTip = new CountTipText(graphics, font, countMessage);

				TipSpacer countSpacer = new TipSpacer(graphics, new SizeF(IsVisibleText(countMessage) ? 4 : 0, 0));

				TipText descriptionTip = new TipText(graphics, font, basicDescription);

				TipSpacer docSpacer = new TipSpacer(graphics, new SizeF(0, IsVisibleText(documentation) ? 4 : 0));

				TipText docTip = new TipText(graphics, font, documentation);

				// Now put them together.
				TipSplitter descSplitter = new TipSplitter(graphics, false, descriptionTip, docSpacer);

				TipSplitter mainSplitter = new TipSplitter(graphics, true, countMessageTip, countSpacer, descSplitter);

				TipSplitter mainSplitter2 = new TipSplitter(graphics, false, mainSplitter, docTip);

				// Show it.
				Size size = TipPainter.DrawTip(control, graphics, mainSplitter2);
				DrawingRectangle1 = countMessageTip.DrawingRectangle1;
				DrawingRectangle2 = countMessageTip.DrawingRectangle2;
				return size;
			}

			return Size.Empty;
		}

		public static Size DrawFixedWidthHelpTip(Control control, Graphics graphics, Font font, string countMessage, string basicDescription, string documentation)
		{
			if (IsVisibleText(countMessage) || IsVisibleText(basicDescription) || IsVisibleText(documentation))
			{
				// Create all the TipSection objects.
				CountTipText countMessageTip = new CountTipText(graphics, font, countMessage);

				TipSpacer countSpacer = new TipSpacer(graphics, new SizeF(IsVisibleText(countMessage) ? 4 : 0, 0));

				TipText descriptionTip = new TipText(graphics, font, basicDescription);

				TipSpacer docSpacer = new TipSpacer(graphics, new SizeF(0, IsVisibleText(documentation) ? 4 : 0));

				TipText docTip = new TipText(graphics, font, documentation);

				// Now put them together.
				TipSplitter descSplitter = new TipSplitter(graphics, false, descriptionTip, docSpacer);

				TipSplitter mainSplitter = new TipSplitter(graphics, true, countMessageTip, countSpacer, descSplitter);

				TipSplitter mainSplitter2 = new TipSplitter(graphics, false, mainSplitter, docTip);

				// Show it.
				Size size = TipPainter.DrawFixedWidthTip(control, graphics, mainSplitter2);
				DrawingRectangle1 = countMessageTip.DrawingRectangle1;
				DrawingRectangle2 = countMessageTip.DrawingRectangle2;
				return size;
			}

			return Size.Empty;
		}

		private static bool IsVisibleText(string text)
		{
			return !string.IsNullOrEmpty(text);
		}
	}
}
