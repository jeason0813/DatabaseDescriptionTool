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
using System.Diagnostics;
using System.Drawing;

namespace ICSharpCode.TextEditor.Util
{
	internal abstract class TipSection
	{
		private SizeF tipAllocatedSize;
		private readonly Graphics tipGraphics;
		private SizeF tipMaxSize;
		private SizeF tipRequiredSize;

		protected TipSection(Graphics graphics)
		{
			tipGraphics = graphics;
		}

		public abstract void Draw(PointF location);

		public SizeF GetRequiredSize()
		{
			return tipRequiredSize;
		}

		public void SetAllocatedSize(SizeF allocatedSize)
		{
			Debug.Assert(allocatedSize.Width >= tipRequiredSize.Width && allocatedSize.Height >= tipRequiredSize.Height);
			tipAllocatedSize = allocatedSize; OnAllocatedSizeChanged();
		}

		public void SetMaximumSize(SizeF maximumSize)
		{
			tipMaxSize = maximumSize; OnMaximumSizeChanged();
		}

		protected virtual void OnAllocatedSizeChanged()
		{
		}

		protected virtual void OnMaximumSizeChanged()
		{
		}

		protected void SetRequiredSize(SizeF requiredSize)
		{
			requiredSize.Width = Math.Max(0, requiredSize.Width);
			requiredSize.Height = Math.Max(0, requiredSize.Height);
			requiredSize.Width = Math.Min(tipMaxSize.Width, requiredSize.Width);
			requiredSize.Height = Math.Min(tipMaxSize.Height, requiredSize.Height);

			tipRequiredSize = requiredSize;
		}

		protected Graphics Graphics
		{
			get
			{
				return tipGraphics;
			}
		}

		protected SizeF AllocatedSize
		{
			get
			{
				return tipAllocatedSize;
			}
		}

		protected SizeF MaximumSize
		{
			get
			{
				return tipMaxSize;
			}
		}
	}
}
