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

namespace ICSharpCode.TextEditor.Gui.InsightWindow
{
	public interface IInsightDataProvider
	{
		/// <summary>
		/// Tells the insight provider to prepare its data.
		/// </summary>
		/// <param name="fileName">The name of the edited file</param>
		/// <param name="textArea">The text area in which the file is being edited</param>
		void SetupDataProvider(string fileName, TextArea textArea);

		/// <summary>
		/// Notifies the insight provider that the caret offset has changed.
		/// </summary>
		/// <returns>Return true to close the insight window (e.g. when the
		/// caret was moved outside the region where insight is displayed for).
		/// Return false to keep the window open.</returns>
		bool CaretOffsetChanged();

		/// <summary>
		/// Gets the text to display in the insight window.
		/// </summary>
		/// <param name="number">The number of the active insight entry.
		/// Multiple insight entries might be multiple overloads of the same method.</param>
		/// <returns>The text to display, e.g. a multi-line string where
		/// the first line is the method definition, followed by a description.</returns>
		string GetInsightData(int number);

		/// <summary>
		/// Gets the number of available insight entries, e.g. the number of available
		/// overloads to call.
		/// </summary>
		int InsightDataCount
		{
			get;
		}

		/// <summary>
		/// Gets the index of the entry to initially select.
		/// </summary>
		int DefaultIndex
		{
			get;
		}
	}
}
