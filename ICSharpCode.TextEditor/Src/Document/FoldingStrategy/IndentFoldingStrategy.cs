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

namespace ICSharpCode.TextEditor.Document
{
	/// <summary>
	/// A simple folding strategy which calculates the folding level
	/// using the indent level of the line.
	/// </summary>
	public class IndentFoldingStrategy : IFoldingStrategy
	{
		public List<FoldMarker> GenerateFoldMarkers(IDocument document, string fileName, object parseInformation)
		{
			List<FoldMarker> l = new List<FoldMarker>();
			//Stack<int> offsetStack = new Stack<int>();
			//Stack<string> textStack = new Stack<string>();
			//int level = 0;

			//foreach (LineSegment segment in document.LineSegmentCollection) {
			//	
			//}

			return l;
		}

		//int GetLevel(IDocument document, int offset)
		//{
		//	int level = 0;
		//	int spaces = 0;
		//	for (int i = offset; i < document.TextLength; ++i)
		//	{
		//		char c = document.GetCharAt(i);
		//		if (c == '\t' || (c == ' ' && ++spaces == 4))
		//		{
		//			spaces = 0;
		//			++level;
		//		}
		//		else
		//		{
		//			break;
		//		}
		//	}
		//	return level;
		//}
	}
}
