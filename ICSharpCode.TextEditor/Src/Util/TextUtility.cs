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

using ICSharpCode.TextEditor.Document;

namespace ICSharpCode.TextEditor.Util
{
	public class TextUtility
	{

		public static bool RegionMatches(IDocument document, int offset, int length, string word)
		{
			if (length != word.Length || document.TextLength < offset + length)
			{
				return false;
			}

			for (int i = 0; i < length; ++i)
			{
				if (document.GetCharAt(offset + i) != word[i])
				{
					return false;
				}
			}

			return true;
		}

		public static bool RegionMatches(IDocument document, bool casesensitive, int offset, int length, string word)
		{
			if (casesensitive)
			{
				return RegionMatches(document, offset, length, word);
			}

			if (length != word.Length || document.TextLength < offset + length)
			{
				return false;
			}

			for (int i = 0; i < length; ++i)
			{
				if (char.ToUpper(document.GetCharAt(offset + i)) != char.ToUpper(word[i]))
				{
					return false;
				}
			}

			return true;
		}

		public static bool RegionMatches(IDocument document, int offset, int length, char[] word)
		{
			if (length != word.Length || document.TextLength < offset + length)
			{
				return false;
			}

			for (int i = 0; i < length; ++i)
			{
				if (document.GetCharAt(offset + i) != word[i])
				{
					return false;
				}
			}

			return true;
		}

		public static bool RegionMatches(IDocument document, bool casesensitive, int offset, int length, char[] word)
		{
			if (casesensitive)
			{
				return RegionMatches(document, offset, length, word);
			}

			if (length != word.Length || document.TextLength < offset + length)
			{
				return false;
			}

			for (int i = 0; i < length; ++i)
			{
				if (char.ToUpper(document.GetCharAt(offset + i)) != char.ToUpper(word[i]))
				{
					return false;
				}
			}

			return true;
		}
	}
}
