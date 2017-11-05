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
using ICSharpCode.TextEditor.Document;

namespace ICSharpCode.TextEditor.Actions
{
	public class ToggleBookmark : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			textArea.Document.BookmarkManager.ToggleMarkAt(textArea.Caret.Position);
			textArea.Document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.SingleLine, textArea.Caret.Line));
			textArea.Document.CommitUpdate();
		}
	}

	public class GotoPrevBookmark : AbstractEditAction
	{
		private readonly Predicate<Bookmark> predicate;

		public GotoPrevBookmark(Predicate<Bookmark> predicate)
		{
			this.predicate = predicate;
		}

		public override void Execute(TextArea textArea)
		{
			Bookmark mark = textArea.Document.BookmarkManager.GetPrevMark(textArea.Caret.Line, predicate);
			if (mark != null)
			{
				textArea.Caret.Position = mark.Location;
				textArea.SelectionManager.ClearSelection();
				textArea.SetDesiredColumn();
			}
		}
	}

	public class GotoNextBookmark : AbstractEditAction
	{
		private readonly Predicate<Bookmark> predicate;

		public GotoNextBookmark(Predicate<Bookmark> predicate)
		{
			this.predicate = predicate;
		}

		public override void Execute(TextArea textArea)
		{
			Bookmark mark = textArea.Document.BookmarkManager.GetNextMark(textArea.Caret.Line, predicate);
			if (mark != null)
			{
				textArea.Caret.Position = mark.Location;
				textArea.SelectionManager.ClearSelection();
				textArea.SetDesiredColumn();
			}
		}
	}

	public class ClearAllBookmarks : AbstractEditAction
	{
		private readonly Predicate<Bookmark> predicate;

		public ClearAllBookmarks(Predicate<Bookmark> predicate)
		{
			this.predicate = predicate;
		}

		public override void Execute(TextArea textArea)
		{
			textArea.Document.BookmarkManager.RemoveMarks(predicate);
			textArea.Document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.WholeTextArea));
			textArea.Document.CommitUpdate();
		}
	}
}
