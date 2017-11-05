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

namespace ICSharpCode.TextEditor
{
	public delegate void ToolTipRequestEventHandler(object sender, ToolTipRequestEventArgs e);

	public class ToolTipRequestEventArgs
	{
		private readonly Point mousePosition;
		private readonly TextLocation logicalPosition;
		private readonly bool inDocument;

		public Point MousePosition
		{
			get
			{
				return mousePosition;
			}
		}

		public TextLocation LogicalPosition
		{
			get
			{
				return logicalPosition;
			}
		}

		public bool InDocument
		{
			get
			{
				return inDocument;
			}
		}

		/// <summary>
		/// Gets if some client handling the event has already shown a tool tip.
		/// </summary>
		public bool ToolTipShown
		{
			get
			{
				return toolTipText != null;
			}
		}

		internal string toolTipText;

		public void ShowToolTip(string text)
		{
			toolTipText = text;
		}

		public ToolTipRequestEventArgs(Point mousePosition, TextLocation logicalPosition, bool inDocument)
		{
			this.mousePosition = mousePosition;
			this.logicalPosition = logicalPosition;
			this.inDocument = inDocument;
		}
	}
}
