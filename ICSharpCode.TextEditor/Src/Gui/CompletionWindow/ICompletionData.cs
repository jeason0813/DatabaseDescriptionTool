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

namespace ICSharpCode.TextEditor.Gui.CompletionWindow
{
	public interface ICompletionData
	{
		int ImageIndex
		{
			get;
		}

		string Text
		{
			get;
			set;
		}

		string Description
		{
			get;
		}

		/// <summary>
		/// Gets a priority value for the completion data item.
		/// When selecting items by their start characters, the item with the highest
		/// priority is selected first.
		/// </summary>
		double Priority
		{
			get;
		}

		/// <summary>
		/// Insert the element represented by the completion data into the text
		/// editor.
		/// </summary>
		/// <param name="textArea">TextArea to insert the completion data in.</param>
		/// <param name="ch">Character that should be inserted after the completion data.
		/// \0 when no character should be inserted.</param>
		/// <returns>Returns true when the insert action has processed the character
		/// <paramref name="ch"/>; false when the character was not processed.</returns>
		bool InsertAction(TextArea textArea, char ch);
	}

	public class DefaultCompletionData : ICompletionData
	{
		private string text;
		private readonly string description;
		private readonly int imageIndex;

		public int ImageIndex
		{
			get
			{
				return imageIndex;
			}
		}

		public string Text
		{
			get
			{
				return text;
			}
			set
			{
				text = value;
			}
		}

		public virtual string Description
		{
			get
			{
				return description;
			}
		}

		private double priority;

		public double Priority
		{
			get
			{
				return priority;
			}
			set
			{
				priority = value;
			}
		}

		public virtual bool InsertAction(TextArea textArea, char ch)
		{
			textArea.InsertString(text);
			return false;
		}

		public DefaultCompletionData(string text, int imageIndex)
		{
			this.text = text;
			this.imageIndex = imageIndex;
		}

		public DefaultCompletionData(string text, string description, int imageIndex)
		{
			this.text = text;
			this.description = description;
			this.imageIndex = imageIndex;
		}

		public static int Compare(ICompletionData a, ICompletionData b)
		{
			if (a == null)
			{
				throw new ArgumentNullException("a");
			}

			if (b == null)
			{
				throw new ArgumentNullException("b");
			}

			return string.Compare(a.Text, b.Text, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}
