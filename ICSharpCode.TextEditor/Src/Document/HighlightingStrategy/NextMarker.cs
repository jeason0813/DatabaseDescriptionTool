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

using System.Xml;

namespace ICSharpCode.TextEditor.Document
{
	/// <summary>
	/// Used for mark next token
	/// </summary>
	public class NextMarker
	{
		private readonly string what;
		private readonly HighlightColor color;
		private readonly bool markMarker;

		/// <value>
		/// String value to indicate to mark next token
		/// </value>
		public string What
		{
			get
			{
				return what;
			}
		}

		/// <value>
		/// Color for marking next token
		/// </value>
		public HighlightColor Color
		{
			get
			{
				return color;
			}
		}

		/// <value>
		/// If true the indication text will be marked with the same color
		/// too
		/// </value>
		public bool MarkMarker
		{
			get
			{
				return markMarker;
			}
		}

		/// <summary>
		/// Creates a new instance of <see cref="NextMarker"/>
		/// </summary>
		public NextMarker(XmlElement mark)
		{
			color = new HighlightColor(mark);
			what = mark.InnerText;

			if (mark.Attributes["markmarker"] != null)
			{
				markMarker = bool.Parse(mark.Attributes["markmarker"].InnerText);
			}
		}
	}
}
