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

using System.Windows.Forms;

namespace ICSharpCode.TextEditor.Gui.CompletionWindow
{
	public interface ICompletionDataProvider
	{
		ImageList ImageList
		{
			get;
		}

		string PreSelection
		{
			get;
		}

		/// <summary>
		/// Gets the index of the element in the list that is chosen by default.
		/// </summary>
		int DefaultIndex
		{
			get;
		}

		/// <summary>
		/// Processes a keypress. Returns the action to be run with the key.
		/// </summary>
		CompletionDataProviderKeyResult ProcessKey(char key);

		/// <summary>
		/// Executes the insertion. The provider should set the caret position and then
		/// call data.InsertAction.
		/// </summary>
		bool InsertAction(ICompletionData data, TextArea textArea, int insertionOffset, char key);

		/// <summary>
		/// Generates the completion data. This method is called by the text editor control.
		/// </summary>
		ICompletionData[] GenerateCompletionData(string fileName, TextArea textArea, char charTyped);
	}

	public enum CompletionDataProviderKeyResult
	{
		/// <summary>
		/// Normal key, used to choose an entry from the completion list
		/// </summary>
		NormalKey,
		/// <summary>
		/// This key triggers insertion of the completed expression
		/// </summary>
		InsertionKey,
		/// <summary>
		/// Increment both start and end offset of completion region when inserting this
		/// key. Can be used to insert whitespace (or other characters) in front of the expression
		/// while the completion window is open.
		/// </summary>
		BeforeStartKey
	}
}
