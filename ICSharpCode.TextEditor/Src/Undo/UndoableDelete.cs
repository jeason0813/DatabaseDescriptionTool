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
using ICSharpCode.TextEditor.Document;

namespace ICSharpCode.TextEditor.Undo
{
	/// <summary>
	/// This class is for the undo of Document insert operations
	/// </summary>
	public class UndoableDelete : IUndoableOperation
	{
		private readonly IDocument document;
		//		int      oldCaretPos;
		private readonly int offset;
		private readonly string text;

		/// <summary>
		/// Creates a new instance of <see cref="UndoableDelete"/>
		/// </summary>	
		public UndoableDelete(IDocument document, int offset, string text)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}

			if (offset < 0 || offset > document.TextLength)
			{
				throw new ArgumentOutOfRangeException("offset");
			}

			Debug.Assert(text != null, "text can't be null");
			//			oldCaretPos   = document.Caret.Offset;
			this.document = document;
			this.offset = offset;
			this.text = text;
		}

		/// <remarks>
		/// Undo last operation
		/// </remarks>
		public void Undo()
		{
			// we clear all selection direct, because the redraw
			// is done per refresh at the end of the action
			//			textArea.SelectionManager.SelectionCollection.Clear();
			document.UndoStack.AcceptChanges = false;
			document.Insert(offset, text);
			//			document.Caret.Offset = Math.Min(document.TextLength, Math.Max(0, oldCaretPos));
			document.UndoStack.AcceptChanges = true;
		}

		/// <remarks>
		/// Redo last undone operation
		/// </remarks>
		public void Redo()
		{
			// we clear all selection direct, because the redraw
			// is done per refresh at the end of the action
			//			textArea.SelectionManager.SelectionCollection.Clear();

			document.UndoStack.AcceptChanges = false;
			document.Remove(offset, text.Length);
			//			document.Caret.Offset = Math.Min(document.TextLength, Math.Max(0, document.Caret.Offset));
			document.UndoStack.AcceptChanges = true;
		}
	}
}
