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

namespace ICSharpCode.TextEditor.Actions
{
	public class ShiftCaretRight : CaretRight
	{
		public override void Execute(TextArea textArea)
		{
			TextLocation oldCaretPos = textArea.Caret.Position;
			base.Execute(textArea);
			textArea.AutoClearSelection = false;
			textArea.SelectionManager.ExtendSelection(oldCaretPos, textArea.Caret.Position);
		}
	}

	public class ShiftCaretLeft : CaretLeft
	{
		public override void Execute(TextArea textArea)
		{
			TextLocation oldCaretPos = textArea.Caret.Position;
			base.Execute(textArea);
			textArea.AutoClearSelection = false;
			textArea.SelectionManager.ExtendSelection(oldCaretPos, textArea.Caret.Position);
		}
	}

	public class ShiftCaretUp : CaretUp
	{
		public override void Execute(TextArea textArea)
		{
			TextLocation oldCaretPos = textArea.Caret.Position;
			base.Execute(textArea);
			textArea.AutoClearSelection = false;
			textArea.SelectionManager.ExtendSelection(oldCaretPos, textArea.Caret.Position);
		}
	}

	public class ShiftCaretDown : CaretDown
	{
		public override void Execute(TextArea textArea)
		{
			TextLocation oldCaretPos = textArea.Caret.Position;
			base.Execute(textArea);
			textArea.AutoClearSelection = false;
			textArea.SelectionManager.ExtendSelection(oldCaretPos, textArea.Caret.Position);
		}
	}

	public class ShiftWordRight : WordRight
	{
		public override void Execute(TextArea textArea)
		{
			TextLocation oldCaretPos = textArea.Caret.Position;
			base.Execute(textArea);
			textArea.AutoClearSelection = false;
			textArea.SelectionManager.ExtendSelection(oldCaretPos, textArea.Caret.Position);
		}
	}

	public class ShiftWordLeft : WordLeft
	{
		public override void Execute(TextArea textArea)
		{
			TextLocation oldCaretPos = textArea.Caret.Position;
			base.Execute(textArea);
			textArea.AutoClearSelection = false;
			textArea.SelectionManager.ExtendSelection(oldCaretPos, textArea.Caret.Position);
		}
	}

	public class ShiftHome : Home
	{
		public override void Execute(TextArea textArea)
		{
			TextLocation oldCaretPos = textArea.Caret.Position;
			base.Execute(textArea);
			textArea.AutoClearSelection = false;
			textArea.SelectionManager.ExtendSelection(oldCaretPos, textArea.Caret.Position);
		}
	}

	public class ShiftEnd : End
	{
		public override void Execute(TextArea textArea)
		{
			TextLocation oldCaretPos = textArea.Caret.Position;
			base.Execute(textArea);
			textArea.AutoClearSelection = false;
			textArea.SelectionManager.ExtendSelection(oldCaretPos, textArea.Caret.Position);
		}
	}

	public class ShiftMoveToStart : MoveToStart
	{
		public override void Execute(TextArea textArea)
		{
			TextLocation oldCaretPos = textArea.Caret.Position;
			base.Execute(textArea);
			textArea.AutoClearSelection = false;
			textArea.SelectionManager.ExtendSelection(oldCaretPos, textArea.Caret.Position);
		}
	}

	public class ShiftMoveToEnd : MoveToEnd
	{
		public override void Execute(TextArea textArea)
		{
			TextLocation oldCaretPos = textArea.Caret.Position;
			base.Execute(textArea);
			textArea.AutoClearSelection = false;
			textArea.SelectionManager.ExtendSelection(oldCaretPos, textArea.Caret.Position);
		}
	}

	public class ShiftMovePageUp : MovePageUp
	{
		public override void Execute(TextArea textArea)
		{
			TextLocation oldCaretPos = textArea.Caret.Position;
			base.Execute(textArea);
			textArea.AutoClearSelection = false;
			textArea.SelectionManager.ExtendSelection(oldCaretPos, textArea.Caret.Position);
		}
	}

	public class ShiftMovePageDown : MovePageDown
	{
		public override void Execute(TextArea textArea)
		{
			TextLocation oldCaretPos = textArea.Caret.Position;
			base.Execute(textArea);
			textArea.AutoClearSelection = false;
			textArea.SelectionManager.ExtendSelection(oldCaretPos, textArea.Caret.Position);
		}
	}

	public class SelectWholeDocument : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			textArea.AutoClearSelection = false;
			TextLocation startPoint = new TextLocation(0, 0);
			TextLocation endPoint = textArea.Document.OffsetToPosition(textArea.Document.TextLength);

			if (textArea.SelectionManager.HasSomethingSelected)
			{
				if (textArea.SelectionManager.SelectionCollection[0].StartPosition == startPoint &&
					textArea.SelectionManager.SelectionCollection[0].EndPosition == endPoint)
				{
					return;
				}
			}

			textArea.Caret.Position = textArea.SelectionManager.NextValidPosition(endPoint.Y);
			textArea.SelectionManager.ExtendSelection(startPoint, endPoint);
			// after a SelectWholeDocument selection, the caret is placed correctly,
			// but it is not positioned internally.  The effect is when the cursor
			// is moved up or down a line, the caret will take on the column that
			// it was in before the SelectWholeDocument
			textArea.SetDesiredColumn();
		}
	}

	public class ClearAllSelections : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			textArea.SelectionManager.ClearSelection();
		}
	}
}
