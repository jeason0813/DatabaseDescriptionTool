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

namespace ICSharpCode.TextEditor
{
	public class Highlight
	{
		public TextLocation OpenBrace { get; set; }
		public TextLocation CloseBrace { get; set; }

		public Highlight(TextLocation openBrace, TextLocation closeBrace)
		{
			OpenBrace = openBrace;
			CloseBrace = closeBrace;
		}
	}

	public class BracketHighlightingSheme
	{
		private char opentag;
		private char closingtag;

		public char OpenTag
		{
			get
			{
				return opentag;
			}
			set
			{
				opentag = value;
			}
		}

		public char ClosingTag
		{
			get
			{
				return closingtag;
			}
			set
			{
				closingtag = value;
			}
		}

		public BracketHighlightingSheme(char opentag, char closingtag)
		{
			this.opentag = opentag;
			this.closingtag = closingtag;
		}

		public Highlight GetHighlight(IDocument document, int offset)
		{
			int searchOffset;

			if (document.TextEditorProperties.BracketMatchingStyle == BracketMatchingStyle.After)
			{
				searchOffset = offset;
			}
			else
			{
				searchOffset = offset + 1;
			}

			char word = document.GetCharAt(Math.Max(0, Math.Min(document.TextLength - 1, searchOffset)));

			TextLocation endP = document.OffsetToPosition(searchOffset);

			if (word == opentag)
			{
				if (searchOffset < document.TextLength)
				{
					int bracketOffset = TextUtilities.SearchBracketForward(document, searchOffset + 1, opentag, closingtag);

					if (bracketOffset >= 0)
					{
						TextLocation p = document.OffsetToPosition(bracketOffset);
						return new Highlight(p, endP);
					}
				}
			}
			else if (word == closingtag)
			{
				if (searchOffset > 0)
				{
					int bracketOffset = TextUtilities.SearchBracketBackward(document, searchOffset - 1, opentag, closingtag);

					if (bracketOffset >= 0)
					{
						TextLocation p = document.OffsetToPosition(bracketOffset);
						return new Highlight(p, endP);
					}
				}
			}

			return null;
		}
	}
}
