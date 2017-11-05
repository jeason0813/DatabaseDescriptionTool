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
using System.IO;
using System.Text;

namespace ICSharpCode.TextEditor.Document
{
	/// <summary>
	/// Simple implementation of the ITextBuffer interface implemented using a
	/// string.
	/// Only for fall-back purposes.
	/// </summary>
	public class StringTextBufferStrategy : ITextBufferStrategy
	{
		private string storedText = "";

		public int Length
		{
			get
			{
				return storedText.Length;
			}
		}

		public void Insert(int offset, string text)
		{
			if (text != null)
			{
				storedText = storedText.Insert(offset, text);
			}
		}

		public void Remove(int offset, int length)
		{
			storedText = storedText.Remove(offset, length);
		}

		public void Replace(int offset, int length, string text)
		{
			Remove(offset, length);
			Insert(offset, text);
		}

		public string GetText(int offset, int length)
		{
			if (length == 0)
			{
				return "";
			}

			if (offset == 0 && length >= storedText.Length)
			{
				return storedText;
			}

			return storedText.Substring(offset, Math.Min(length, storedText.Length - offset));
		}

		public char GetCharAt(int offset)
		{
			if (offset == Length)
			{
				return '\0';
			}

			return storedText[offset];
		}

		public void SetContent(string text)
		{
			storedText = text;
		}

		public static ITextBufferStrategy CreateTextBufferFromFile(string fileName)
		{
			if (!File.Exists(fileName))
			{
				throw new FileNotFoundException(fileName);
			}

			StringTextBufferStrategy s = new StringTextBufferStrategy();
			s.SetContent(Util.FileReader.ReadFileContent(fileName, Encoding.Default));
			return s;
		}
	}
}
