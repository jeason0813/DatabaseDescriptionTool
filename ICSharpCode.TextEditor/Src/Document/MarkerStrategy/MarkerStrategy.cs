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
using System.Collections.Generic;

namespace ICSharpCode.TextEditor.Document
{
	/// <summary>
	/// Manages the list of markers and provides ways to retrieve markers for specific positions.
	/// </summary>
	public sealed class MarkerStrategy
	{
		private readonly List<TextMarker> textMarker = new List<TextMarker>();
		private readonly IDocument document;

		public IDocument Document
		{
			get
			{
				return document;
			}
		}

		public IEnumerable<TextMarker> TextMarker
		{
			get
			{
				return textMarker.AsReadOnly();
			}
		}

		public void AddMarker(TextMarker item)
		{
			markersTable.Clear();
			textMarker.Add(item);
		}

		public void InsertMarker(int index, TextMarker item)
		{
			markersTable.Clear();
			textMarker.Insert(index, item);
		}

		public void RemoveMarker(TextMarker item)
		{
			markersTable.Clear();
			textMarker.Remove(item);
		}

		public void RemoveAll(Predicate<TextMarker> match)
		{
			markersTable.Clear();
			textMarker.RemoveAll(match);
		}

		public MarkerStrategy(IDocument document)
		{
			this.document = document;
			document.DocumentChanged += DocumentChanged;
		}

		private readonly Dictionary<int, List<TextMarker>> markersTable = new Dictionary<int, List<TextMarker>>();

		public List<TextMarker> GetMarkers(int offset)
		{
			if (!markersTable.ContainsKey(offset))
			{
				List<TextMarker> markers = new List<TextMarker>();

				for (int i = 0; i < textMarker.Count; ++i)
				{
					TextMarker marker = textMarker[i];

					if (marker.Offset <= offset && offset <= marker.EndOffset)
					{
						markers.Add(marker);
					}
				}

				markersTable[offset] = markers;
			}

			return markersTable[offset];
		}

		public List<TextMarker> GetMarkers(int offset, int length)
		{
			int endOffset = offset + length - 1;
			List<TextMarker> markers = new List<TextMarker>();

			for (int i = 0; i < textMarker.Count; ++i)
			{
				TextMarker marker = textMarker[i];

				if (// start in marker region
					marker.Offset <= offset && offset <= marker.EndOffset ||
					// end in marker region
					marker.Offset <= endOffset && endOffset <= marker.EndOffset ||
					// marker start in region
					offset <= marker.Offset && marker.Offset <= endOffset ||
					// marker end in region
					offset <= marker.EndOffset && marker.EndOffset <= endOffset
				   )
				{
					markers.Add(marker);
				}
			}

			return markers;
		}

		public List<TextMarker> GetMarkers(TextLocation position)
		{
			if (position.Y >= document.TotalNumberOfLines || position.Y < 0)
			{
				return new List<TextMarker>();
			}

			LineSegment segment = document.GetLineSegment(position.Y);
			return GetMarkers(segment.Offset + position.X);
		}

		private void DocumentChanged(object sender, DocumentEventArgs e)
		{
			// reset markers table
			markersTable.Clear();
			document.UpdateSegmentListOnDocumentChange(textMarker, e);
		}
	}
}
