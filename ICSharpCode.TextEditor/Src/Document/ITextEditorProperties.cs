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

using System.Drawing;
using System.Text;

namespace ICSharpCode.TextEditor.Document
{
	public interface ITextEditorProperties
	{
		bool CaretLine
		{
			get;
			set;
		}

		bool AutoInsertCurlyBracket
		{ // is wrapped in text editor control
			get;
			set;
		}

		bool HideMouseCursor
		{ // is wrapped in text editor control
			get;
			set;
		}

		bool IsIconBarVisible
		{ // is wrapped in text editor control
			get;
			set;
		}

		bool AllowCaretBeyondEOL
		{
			get;
			set;
		}

		bool ShowMatchingBracket
		{ // is wrapped in text editor control
			get;
			set;
		}

		bool CutCopyWholeLine
		{
			get;
			set;
		}

		System.Drawing.Text.TextRenderingHint TextRenderingHint
		{ // is wrapped in text editor control
			get;
			set;
		}

		bool MouseWheelScrollDown
		{
			get;
			set;
		}

		bool MouseWheelTextZoom
		{
			get;
			set;
		}

		string LineTerminator
		{
			get;
			set;
		}

		LineViewerStyle LineViewerStyle
		{ // is wrapped in text editor control
			get;
			set;
		}

		bool ShowInvalidLines
		{ // is wrapped in text editor control
			get;
			set;
		}

		int VerticalRulerRow
		{ // is wrapped in text editor control
			get;
			set;
		}

		bool ShowSpaces
		{ // is wrapped in text editor control
			get;
			set;
		}

		bool ShowTabs
		{ // is wrapped in text editor control
			get;
			set;
		}

		bool ShowEOLMarker
		{ // is wrapped in text editor control
			get;
			set;
		}

		bool ConvertTabsToSpaces
		{ // is wrapped in text editor control
			get;
			set;
		}

		bool ShowHorizontalRuler
		{ // is wrapped in text editor control
			get;
			set;
		}

		bool ShowVerticalRuler
		{ // is wrapped in text editor control
			get;
			set;
		}

		Encoding Encoding
		{
			get;
			set;
		}

		bool EnableFolding
		{ // is wrapped in text editor control
			get;
			set;
		}

		bool ShowLineNumbers
		{ // is wrapped in text editor control
			get;
			set;
		}

		/// <summary>
		/// The width of a tab.
		/// </summary>
		int TabIndent
		{ // is wrapped in text editor control
			get;
			set;
		}

		/// <summary>
		/// The amount of spaces a tab is converted to if ConvertTabsToSpaces is true.
		/// </summary>
		int IndentationSize
		{
			get;
			set;
		}

		IndentStyle IndentStyle
		{ // is wrapped in text editor control
			get;
			set;
		}

		DocumentSelectionMode DocumentSelectionMode
		{
			get;
			set;
		}

		Font Font
		{ // is wrapped in text editor control
			get;
			set;
		}

		FontContainer FontContainer
		{
			get;
		}

		BracketMatchingStyle BracketMatchingStyle
		{ // is wrapped in text editor control
			get;
			set;
		}

		bool SupportReadOnlySegments
		{
			get;
			set;
		}
	}
}
