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

using System.Collections.Generic;
using System.Xml;

namespace ICSharpCode.TextEditor.Document
{
	/// <summary>
	/// This class is used for storing the state of a bookmark manager 
	/// </summary>
	public class BookmarkManagerMemento
	{
		private List<int> bookmarks = new List<int>();

		/// <value>
		/// Contains all bookmarks as int values
		/// </value>
		public List<int> Bookmarks
		{
			get
			{
				return bookmarks;
			}
			set
			{
				bookmarks = value;
			}
		}

		/// <summary>
		/// Validates all bookmarks if they're in range of the document.
		/// (removing all bookmarks &lt; 0 and bookmarks &gt; max. line number
		/// </summary>
		public void CheckMemento(IDocument document)
		{
			for (int i = 0; i < bookmarks.Count; ++i)
			{
				int mark = bookmarks[i];

				if (mark < 0 || mark >= document.TotalNumberOfLines)
				{
					bookmarks.RemoveAt(i);
					--i;
				}
			}
		}

		/// <summary>
		/// Creates a new instance of <see cref="BookmarkManagerMemento"/>
		/// </summary>
		public BookmarkManagerMemento()
		{
		}

		/// <summary>
		/// Creates a new instance of <see cref="BookmarkManagerMemento"/>
		/// </summary>
		public BookmarkManagerMemento(XmlElement element)
		{
			foreach (XmlElement el in element.ChildNodes)
			{
				bookmarks.Add(int.Parse(el.Attributes["line"].InnerText));
			}
		}

		/// <summary>
		/// Creates a new instance of <see cref="BookmarkManagerMemento"/>
		/// </summary>
		public BookmarkManagerMemento(List<int> bookmarks)
		{
			this.bookmarks = bookmarks;
		}

		/// <summary>
		/// Converts a xml element to a <see cref="BookmarkManagerMemento"/> object
		/// </summary>
		public object FromXmlElement(XmlElement element)
		{
			return new BookmarkManagerMemento(element);
		}

		/// <summary>
		/// Converts this <see cref="BookmarkManagerMemento"/> to a xml element
		/// </summary>
		public XmlElement ToXmlElement(XmlDocument doc)
		{
			XmlElement bookmarknode = doc.CreateElement("Bookmarks");

			foreach (int line in bookmarks)
			{
				XmlElement markNode = doc.CreateElement("Mark");

				XmlAttribute lineAttr = doc.CreateAttribute("line");
				lineAttr.InnerText = line.ToString();
				markNode.Attributes.Append(lineAttr);

				bookmarknode.AppendChild(markNode);
			}

			return bookmarknode;
		}
	}
}
