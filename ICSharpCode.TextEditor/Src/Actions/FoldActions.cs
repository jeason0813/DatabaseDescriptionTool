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
using ICSharpCode.TextEditor.Document;

namespace ICSharpCode.TextEditor.Actions
{
	public class ToggleFolding : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			List<FoldMarker> foldMarkers = textArea.Document.FoldingManager.GetFoldingsWithStart(textArea.Caret.Line);

			if (foldMarkers.Count != 0)
			{
				foreach (FoldMarker fm in foldMarkers)
				{
					fm.IsFolded = !fm.IsFolded;
				}
			}
			else
			{
				foldMarkers = textArea.Document.FoldingManager.GetFoldingsContainsLineNumber(textArea.Caret.Line);

				if (foldMarkers.Count != 0)
				{
					FoldMarker innerMost = foldMarkers[0];

					for (int i = 1; i < foldMarkers.Count; i++)
					{
						if (new TextLocation(foldMarkers[i].StartColumn, foldMarkers[i].StartLine) > new TextLocation(innerMost.StartColumn, innerMost.StartLine))
						{
							innerMost = foldMarkers[i];
						}
					}

					innerMost.IsFolded = !innerMost.IsFolded;
				}
			}

			textArea.Document.FoldingManager.NotifyFoldingsChanged(EventArgs.Empty);
		}
	}

	public class ToggleAllFoldings : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			bool doFold = true;

			foreach (FoldMarker fm in textArea.Document.FoldingManager.FoldMarker)
			{
				if (fm.IsFolded)
				{
					doFold = false;
					break;
				}
			}

			foreach (FoldMarker fm in textArea.Document.FoldingManager.FoldMarker)
			{
				fm.IsFolded = doFold;
			}

			textArea.Document.FoldingManager.NotifyFoldingsChanged(EventArgs.Empty);
		}
	}

	public class ShowDefinitionsOnly : AbstractEditAction
	{
		public override void Execute(TextArea textArea)
		{
			foreach (FoldMarker fm in textArea.Document.FoldingManager.FoldMarker)
			{
				fm.IsFolded = fm.FoldType == FoldType.MemberBody || fm.FoldType == FoldType.Region;
			}

			textArea.Document.FoldingManager.NotifyFoldingsChanged(EventArgs.Empty);
		}
	}
}
