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
using System.Collections.Generic;

namespace ICSharpCode.TextEditor.Undo
{
	/// <summary>
	/// This class implements an undo stack
	/// </summary>
	public class UndoStack
	{
		private readonly Stack<IUndoableOperation> undostack = new Stack<IUndoableOperation>();
		private readonly Stack<IUndoableOperation> redostack = new Stack<IUndoableOperation>();

		public TextEditorControlBase TextEditorControl = null;

		public event EventHandler ActionUndone;
		public event EventHandler ActionRedone;

		public event OperationEventHandler OperationPushed;

		/// <summary>
		/// Gets/Sets if changes to the document are protocolled by the undo stack.
		/// Used internally to disable the undo stack temporarily while undoing an action.
		/// </summary>
		internal bool AcceptChanges = true;

		/// <summary>
		/// Gets if there are actions on the undo stack.
		/// </summary>
		public bool CanUndo
		{
			get
			{
				return undostack.Count > 0;
			}
		}

		/// <summary>
		/// Gets if there are actions on the redo stack.
		/// </summary>
		public bool CanRedo
		{
			get
			{
				return redostack.Count > 0;
			}
		}

		/// <summary>
		/// Gets the number of actions on the undo stack.
		/// </summary>
		public int UndoItemCount
		{
			get
			{
				return undostack.Count;
			}
		}

		/// <summary>
		/// Gets the number of actions on the redo stack.
		/// </summary>
		public int RedoItemCount
		{
			get
			{
				return redostack.Count;
			}
		}

		private int undoGroupDepth;
		private int actionCountInUndoGroup;

		public void StartUndoGroup()
		{
			if (undoGroupDepth == 0)
			{
				actionCountInUndoGroup = 0;
			}

			undoGroupDepth++;
			//Util.LoggingService.Debug("Open undo group (new depth=" + undoGroupDepth + ")");
		}

		public void EndUndoGroup()
		{
			if (undoGroupDepth == 0)
			{
				throw new InvalidOperationException("There are no open undo groups");
			}

			undoGroupDepth--;

			//Util.LoggingService.Debug("Close undo group (new depth=" + undoGroupDepth + ")");
			if (undoGroupDepth == 0 && actionCountInUndoGroup > 1)
			{
				UndoQueue op = new UndoQueue(undostack, actionCountInUndoGroup);
				undostack.Push(op);

				if (OperationPushed != null)
				{
					OperationPushed(this, new OperationEventArgs(op));
				}
			}
		}

		public void AssertNoUndoGroupOpen()
		{
			if (undoGroupDepth != 0)
			{
				undoGroupDepth = 0;
				throw new InvalidOperationException("No undo group should be open at this point");
			}
		}

		/// <summary>
		/// Call this method to undo the last operation on the stack
		/// </summary>
		public void Undo()
		{
			AssertNoUndoGroupOpen();

			if (undostack.Count > 0)
			{
				IUndoableOperation uedit = undostack.Pop();
				redostack.Push(uedit);
				uedit.Undo();
				OnActionUndone();
			}
		}

		/// <summary>
		/// Call this method to redo the last undone operation
		/// </summary>
		public void Redo()
		{
			AssertNoUndoGroupOpen();

			if (redostack.Count > 0)
			{
				IUndoableOperation uedit = redostack.Pop();
				undostack.Push(uedit);
				uedit.Redo();
				OnActionRedone();
			}
		}

		/// <summary>
		/// Call this method to push an UndoableOperation on the undostack, the redostack
		/// will be cleared, if you use this method.
		/// </summary>
		public void Push(IUndoableOperation operation)
		{
			if (operation == null)
			{
				throw new ArgumentNullException("operation");
			}

			if (AcceptChanges)
			{
				StartUndoGroup();
				undostack.Push(operation);
				actionCountInUndoGroup++;

				if (TextEditorControl != null)
				{
					undostack.Push(new UndoableSetCaretPosition(this, TextEditorControl.ActiveTextAreaControl.Caret.Position));
					actionCountInUndoGroup++;
				}

				EndUndoGroup();
				ClearRedoStack();
			}
		}

		/// <summary>
		/// Call this method, if you want to clear the redo stack
		/// </summary>
		public void ClearRedoStack()
		{
			redostack.Clear();
		}

		/// <summary>
		/// Clears both the undo and redo stack.
		/// </summary>
		public void ClearAll()
		{
			AssertNoUndoGroupOpen();
			undostack.Clear();
			redostack.Clear();
			actionCountInUndoGroup = 0;
		}

		/// <summary>
		/// </summary>
		protected void OnActionUndone()
		{
			if (ActionUndone != null)
			{
				ActionUndone(null, null);
			}
		}

		/// <summary>
		/// </summary>
		protected void OnActionRedone()
		{
			if (ActionRedone != null)
			{
				ActionRedone(null, null);
			}
		}

		private class UndoableSetCaretPosition : IUndoableOperation
		{
			private readonly UndoStack stack;
			private readonly TextLocation pos;
			private TextLocation redoPos;

			public UndoableSetCaretPosition(UndoStack stack, TextLocation pos)
			{
				this.stack = stack;
				this.pos = pos;
			}

			public void Undo()
			{
				redoPos = stack.TextEditorControl.ActiveTextAreaControl.Caret.Position;
				stack.TextEditorControl.ActiveTextAreaControl.Caret.Position = pos;
				stack.TextEditorControl.ActiveTextAreaControl.SelectionManager.ClearSelection();
			}

			public void Redo()
			{
				stack.TextEditorControl.ActiveTextAreaControl.Caret.Position = redoPos;
				stack.TextEditorControl.ActiveTextAreaControl.SelectionManager.ClearSelection();
			}
		}
	}

	public class OperationEventArgs : EventArgs
	{
		public OperationEventArgs(IUndoableOperation op)
		{
			this.op = op;
		}

		private readonly IUndoableOperation op;

		public IUndoableOperation Operation
		{
			get
			{
				return op;
			}
		}
	}

	public delegate void OperationEventHandler(object sender, OperationEventArgs e);
}
