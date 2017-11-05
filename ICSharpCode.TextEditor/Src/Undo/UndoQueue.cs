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
using System.Diagnostics;

namespace ICSharpCode.TextEditor.Undo
{
	/// <summary>
	/// This class stacks the last x operations from the undostack and makes
	/// one undo/redo operation from it.
	/// </summary>
	internal sealed class UndoQueue : IUndoableOperation
	{
		private readonly List<IUndoableOperation> undolist = new List<IUndoableOperation>();

		/// <summary>
		/// </summary>
		public UndoQueue(Stack<IUndoableOperation> stack, int numops)
		{
			if (stack == null)
			{
				throw new ArgumentNullException("stack");
			}

			Debug.Assert(numops > 0, "ICSharpCode.TextEditor.Undo.UndoQueue : numops should be > 0");
			if (numops > stack.Count)
			{
				numops = stack.Count;
			}

			for (int i = 0; i < numops; ++i)
			{
				undolist.Add(stack.Pop());
			}
		}

		public void Undo()
		{
			for (int i = 0; i < undolist.Count; ++i)
			{
				undolist[i].Undo();
			}
		}

		public void Redo()
		{
			for (int i = undolist.Count - 1; i >= 0; --i)
			{
				undolist[i].Redo();
			}
		}
	}
}
