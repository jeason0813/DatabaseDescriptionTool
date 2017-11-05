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

namespace ICSharpCode.TextEditor.Document
{
	public enum TextMarkerType
	{
		Invisible,
		SolidBlock,
		Underlined,
		WaveLine
	}

	/// <summary>
	/// Marks a part of a document.
	/// </summary>
	public class TextMarker : AbstractSegment
	{
		private readonly TextMarkerType textMarkerType;
		private readonly Color color;
		private readonly Color foreColor;
		private string toolTip;
		private readonly bool overrideForeColor;

		public TextMarkerType TextMarkerType
		{
			get
			{
				return textMarkerType;
			}
		}

		public Color Color
		{
			get
			{
				return color;
			}
		}

		public Color ForeColor
		{
			get
			{
				return foreColor;
			}
		}

		public bool OverrideForeColor
		{
			get
			{
				return overrideForeColor;
			}
		}

		/// <summary>
		/// Marks the text segment as read-only.
		/// </summary>
		public bool IsReadOnly { get; set; }

		public string ToolTip
		{
			get
			{
				return toolTip;
			}
			set
			{
				toolTip = value;
			}
		}

		/// <summary>
		/// Gets the last offset that is inside the marker region.
		/// </summary>
		public int EndOffset
		{
			get
			{
				return Offset + Length - 1;
			}
		}

		public TextMarker(int offset, int length, TextMarkerType textMarkerType) : this(offset, length, textMarkerType, Color.Red)
		{
		}

		public TextMarker(int offset, int length, TextMarkerType textMarkerType, Color color)
		{
			if (length < 1) length = 1;
			this.offset = offset;
			this.length = length;
			this.textMarkerType = textMarkerType;
			this.color = color;
		}

		public TextMarker(int offset, int length, TextMarkerType textMarkerType, Color color, Color foreColor)
		{
			if (length < 1) length = 1;
			this.offset = offset;
			this.length = length;
			this.textMarkerType = textMarkerType;
			this.color = color;
			this.foreColor = foreColor;
			overrideForeColor = true;
		}
	}
}
