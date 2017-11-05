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
	public enum BracketMatchingStyle
	{
		Before,
		After
	}

	public class DefaultTextEditorProperties : ITextEditorProperties
	{
		private int tabIndent = 4;
		private int indentationSize = 4;
		private IndentStyle indentStyle = IndentStyle.Smart;
		private DocumentSelectionMode documentSelectionMode = DocumentSelectionMode.Normal;
		private Encoding encoding = Encoding.UTF8;
		private BracketMatchingStyle bracketMatchingStyle = BracketMatchingStyle.After;
		private readonly FontContainer fontContainer;
		private static Font DefaultFont;

		public DefaultTextEditorProperties()
		{
			if (DefaultFont == null)
			{
				DefaultFont = new Font("Courier New", 10);
			}

			fontContainer = new FontContainer(DefaultFont);
		}

		private bool allowCaretBeyondEOL;

		private bool caretLine;

		private bool showMatchingBracket = true;
		private bool showLineNumbers = true;

		private bool showSpaces;
		private bool showTabs;
		private bool showEOLMarker;

		private bool showInvalidLines;

		private bool isIconBarVisible;
		private bool enableFolding = true;
		private bool showHorizontalRuler;
		private bool showVerticalRuler = true;
		private bool convertTabsToSpaces;
		private System.Drawing.Text.TextRenderingHint textRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
		private bool mouseWheelScrollDown = true;
		private bool mouseWheelTextZoom = true;

		private bool hideMouseCursor;
		private bool cutCopyWholeLine = true;

		private int verticalRulerRow = 80;
		private LineViewerStyle lineViewerStyle = LineViewerStyle.None;
		private string lineTerminator = "\r\n";
		private bool autoInsertCurlyBracket = true;
		private bool supportReadOnlySegments;

		public int TabIndent
		{
			get
			{
				return tabIndent;
			}
			set
			{
				tabIndent = value;
			}
		}

		public int IndentationSize
		{
			get
			{
				return indentationSize;
			}
			set
			{
				indentationSize = value;
			}
		}

		public IndentStyle IndentStyle
		{
			get
			{
				return indentStyle;
			}
			set
			{
				indentStyle = value;
			}
		}

		public bool CaretLine
		{
			get
			{
				return caretLine;
			}
			set
			{
				caretLine = value;
			}
		}

		public DocumentSelectionMode DocumentSelectionMode
		{
			get
			{
				return documentSelectionMode;
			}
			set
			{
				documentSelectionMode = value;
			}
		}
		public bool AllowCaretBeyondEOL
		{
			get
			{
				return allowCaretBeyondEOL;
			}
			set
			{
				allowCaretBeyondEOL = value;
			}
		}
		public bool ShowMatchingBracket
		{
			get
			{
				return showMatchingBracket;
			}
			set
			{
				showMatchingBracket = value;
			}
		}
		public bool ShowLineNumbers
		{
			get
			{
				return showLineNumbers;
			}
			set
			{
				showLineNumbers = value;
			}
		}
		public bool ShowSpaces
		{
			get
			{
				return showSpaces;
			}
			set
			{
				showSpaces = value;
			}
		}
		public bool ShowTabs
		{
			get
			{
				return showTabs;
			}
			set
			{
				showTabs = value;
			}
		}
		public bool ShowEOLMarker
		{
			get
			{
				return showEOLMarker;
			}
			set
			{
				showEOLMarker = value;
			}
		}
		public bool ShowInvalidLines
		{
			get
			{
				return showInvalidLines;
			}
			set
			{
				showInvalidLines = value;
			}
		}
		public bool IsIconBarVisible
		{
			get
			{
				return isIconBarVisible;
			}
			set
			{
				isIconBarVisible = value;
			}
		}
		public bool EnableFolding
		{
			get
			{
				return enableFolding;
			}
			set
			{
				enableFolding = value;
			}
		}
		public bool ShowHorizontalRuler
		{
			get
			{
				return showHorizontalRuler;
			}
			set
			{
				showHorizontalRuler = value;
			}
		}
		public bool ShowVerticalRuler
		{
			get
			{
				return showVerticalRuler;
			}
			set
			{
				showVerticalRuler = value;
			}
		}
		public bool ConvertTabsToSpaces
		{
			get
			{
				return convertTabsToSpaces;
			}
			set
			{
				convertTabsToSpaces = value;
			}
		}
		public System.Drawing.Text.TextRenderingHint TextRenderingHint
		{
			get
			{
				return textRenderingHint;
			}
			set
			{
				textRenderingHint = value;
			}
		}

		public bool MouseWheelScrollDown
		{
			get
			{
				return mouseWheelScrollDown;
			}
			set
			{
				mouseWheelScrollDown = value;
			}
		}
		public bool MouseWheelTextZoom
		{
			get
			{
				return mouseWheelTextZoom;
			}
			set
			{
				mouseWheelTextZoom = value;
			}
		}

		public bool HideMouseCursor
		{
			get
			{
				return hideMouseCursor;
			}
			set
			{
				hideMouseCursor = value;
			}
		}

		public bool CutCopyWholeLine
		{
			get
			{
				return cutCopyWholeLine;
			}
			set
			{
				cutCopyWholeLine = value;
			}
		}

		public Encoding Encoding
		{
			get
			{
				return encoding;
			}
			set
			{
				encoding = value;
			}
		}
		public int VerticalRulerRow
		{
			get
			{
				return verticalRulerRow;
			}
			set
			{
				verticalRulerRow = value;
			}
		}
		public LineViewerStyle LineViewerStyle
		{
			get
			{
				return lineViewerStyle;
			}
			set
			{
				lineViewerStyle = value;
			}
		}
		public string LineTerminator
		{
			get
			{
				return lineTerminator;
			}
			set
			{
				lineTerminator = value;
			}
		}
		public bool AutoInsertCurlyBracket
		{
			get
			{
				return autoInsertCurlyBracket;
			}
			set
			{
				autoInsertCurlyBracket = value;
			}
		}

		public Font Font
		{
			get
			{
				return fontContainer.DefaultFont;
			}
			set
			{
				fontContainer.DefaultFont = value;
			}
		}

		public FontContainer FontContainer
		{
			get
			{
				return fontContainer;
			}
		}

		public BracketMatchingStyle BracketMatchingStyle
		{
			get
			{
				return bracketMatchingStyle;
			}
			set
			{
				bracketMatchingStyle = value;
			}
		}

		public bool SupportReadOnlySegments
		{
			get
			{
				return supportReadOnlySegments;
			}
			set
			{
				supportReadOnlySegments = value;
			}
		}
	}
}
