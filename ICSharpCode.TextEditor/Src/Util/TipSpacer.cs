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
	internal class TipSpacer : TipSection
	{
		private SizeF spacerSize;

		public TipSpacer(Graphics graphics, SizeF size) : base(graphics)
		{
			spacerSize = size;
		}

		public override void Draw(PointF location)
		{
		}

		protected override void OnMaximumSizeChanged()
		{
			base.OnMaximumSizeChanged();
			SetRequiredSize(new SizeF(Math.Min(MaximumSize.Width, spacerSize.Width), Math.Min(MaximumSize.Height, spacerSize.Height)));
		}
	}
}
