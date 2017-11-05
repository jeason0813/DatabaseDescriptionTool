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
using System.Windows.Forms;

using ICSharpCode.TextEditor.Document;

namespace ICSharpCode.TextEditor
{
	public delegate void MarginMouseEventHandler(AbstractMargin sender, Point mousepos, MouseButtons mouseButtons);
	public delegate void MarginPaintEventHandler(AbstractMargin sender, Graphics g, Rectangle rect);

	/// <summary>
	/// This class views the line numbers and folding markers.
	/// </summary>
	public abstract class AbstractMargin
	{
		private Cursor cursor = Cursors.Default;

		[CLSCompliant(false)]
		protected Rectangle drawingPosition = new Rectangle(0, 0, 0, 0);
		[CLSCompliant(false)]
		protected TextArea textArea;

		public Rectangle DrawingPosition
		{
			get
			{
				return drawingPosition;
			}
			set
			{
				drawingPosition = value;
			}
		}

		public TextArea TextArea
		{
			get
			{
				return textArea;
			}
		}

		public IDocument Document
		{
			get
			{
				return textArea.Document;
			}
		}

		public ITextEditorProperties TextEditorProperties
		{
			get
			{
				return textArea.Document.TextEditorProperties;
			}
		}

		public virtual Cursor Cursor
		{
			get
			{
				return cursor;
			}
			set
			{
				cursor = value;
			}
		}

		public virtual Size Size
		{
			get
			{
				return new Size(-1, -1);
			}
		}

		public virtual bool IsVisible
		{
			get
			{
				return true;
			}
		}

		protected AbstractMargin(TextArea textArea)
		{
			this.textArea = textArea;
		}

		public virtual void HandleMouseDown(Point mousepos, MouseButtons mouseButtons)
		{
			if (MouseDown != null)
			{
				MouseDown(this, mousepos, mouseButtons);
			}
		}
		public virtual void HandleMouseMove(Point mousepos, MouseButtons mouseButtons)
		{
			if (MouseMove != null)
			{
				MouseMove(this, mousepos, mouseButtons);
			}
		}
		public virtual void HandleMouseLeave(EventArgs e)
		{
			if (MouseLeave != null)
			{
				MouseLeave(this, e);
			}
		}

		public virtual void Paint(Graphics g, Rectangle rect)
		{
			if (Painted != null)
			{
				Painted(this, g, rect);
			}
		}

		public event MarginPaintEventHandler Painted;
		public event MarginMouseEventHandler MouseDown;
		public event MarginMouseEventHandler MouseMove;
		public event EventHandler MouseLeave;
	}
}
