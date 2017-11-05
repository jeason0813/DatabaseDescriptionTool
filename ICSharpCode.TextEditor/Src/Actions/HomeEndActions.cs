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

using System.Collections.Generic;

using ICSharpCode.TextEditor.Document;

namespace ICSharpCode.TextEditor.Actions
{
	public class Home : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			TextLocation newPos = textArea.Caret.Position;
			bool jumpedIntoFolding;

			do
			{
				LineSegment curLine = textArea.Document.GetLineSegment(newPos.Y);

				if (TextUtilities.IsEmptyLine(textArea.Document, newPos.Y))
				{
					if (newPos.X != 0)
					{
						newPos.X = 0;
					}
					else
					{
						newPos.X = curLine.Length;
					}
				}
				else
				{
					int firstCharOffset = TextUtilities.GetFirstNonWSChar(textArea.Document, curLine.Offset);
					int firstCharColumn = firstCharOffset - curLine.Offset;

					if (newPos.X == firstCharColumn)
					{
						newPos.X = 0;
					}
					else
					{
						newPos.X = firstCharColumn;
					}
				}

				List<FoldMarker> foldings = textArea.Document.FoldingManager.GetFoldingsFromPosition(newPos.Y, newPos.X);
				jumpedIntoFolding = false;

				foreach (FoldMarker foldMarker in foldings)
				{
					if (foldMarker.IsFolded)
					{
						newPos = new TextLocation(foldMarker.StartColumn, foldMarker.StartLine);
						jumpedIntoFolding = true;
						break;
					}
				}

			} while (jumpedIntoFolding);

			if (newPos != textArea.Caret.Position)
			{
				textArea.Caret.Position = newPos;
				textArea.SetDesiredColumn();
			}
		}
	}

	public class End : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			TextLocation newPos = textArea.Caret.Position;
			bool jumpedIntoFolding;

			do
			{
				LineSegment curLine = textArea.Document.GetLineSegment(newPos.Y);
				newPos.X = curLine.Length;

				List<FoldMarker> foldings = textArea.Document.FoldingManager.GetFoldingsFromPosition(newPos.Y, newPos.X);
				jumpedIntoFolding = false;

				foreach (FoldMarker foldMarker in foldings)
				{
					if (foldMarker.IsFolded)
					{
						newPos = new TextLocation(foldMarker.EndColumn, foldMarker.EndLine);
						jumpedIntoFolding = true;
						break;
					}
				}
			} while (jumpedIntoFolding);

			if (newPos != textArea.Caret.Position)
			{
				textArea.Caret.Position = newPos;
				textArea.SetDesiredColumn();
			}
		}
	}

	public class MoveToStart : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			if (textArea.Caret.Line != 0 || textArea.Caret.Column != 0)
			{
				textArea.Caret.Position = new TextLocation(0, 0);
				textArea.SetDesiredColumn();
			}
		}
	}


	public class MoveToEnd : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			TextLocation endPos = textArea.Document.OffsetToPosition(textArea.Document.TextLength);
			if (textArea.Caret.Position != endPos)
			{
				textArea.Caret.Position = endPos;
				textArea.SetDesiredColumn();
			}
		}
	}
}
