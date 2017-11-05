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

namespace ICSharpCode.TextEditor.Document
{
	public enum AnchorMovementType
	{
		/// <summary>
		/// Behaves like a start marker - when text is inserted at the anchor position, the anchor will stay
		/// before the inserted text.
		/// </summary>
		BeforeInsertion,
		/// <summary>
		/// Behave like an end marker - when text is insered at the anchor position, the anchor will move
		/// after the inserted text.
		/// </summary>
		AfterInsertion
	}

	/// <summary>
	/// An anchor that can be put into a document and moves around when the document is changed.
	/// </summary>
	public sealed class TextAnchor
	{
		private static Exception AnchorDeletedError()
		{
			return new InvalidOperationException("The text containing the anchor was deleted");
		}

		private LineSegment lineSegment;
		private int columnNumber;

		public LineSegment Line
		{
			get
			{
				if (lineSegment == null)
				{
					throw AnchorDeletedError();
				}

				return lineSegment;
			}
			internal set
			{
				lineSegment = value;
			}
		}

		public bool IsDeleted
		{
			get
			{
				return lineSegment == null;
			}
		}

		public int LineNumber
		{
			get
			{
				return Line.LineNumber;
			}
		}

		public int ColumnNumber
		{
			get
			{
				if (lineSegment == null)
				{
					throw AnchorDeletedError();
				}

				return columnNumber;
			}
			internal set
			{
				columnNumber = value;
			}
		}

		public TextLocation Location
		{
			get
			{
				return new TextLocation(ColumnNumber, LineNumber);
			}
		}

		public int Offset
		{
			get
			{
				return Line.Offset + columnNumber;
			}
		}

		/// <summary>
		/// Controls how the anchor moves.
		/// </summary>
		public AnchorMovementType MovementType { get; set; }

		public event EventHandler Deleted;

		internal void Delete(ref DeferredEventList deferredEventList)
		{
			// we cannot fire an event here because this method is called while the LineManager adjusts the
			// lineCollection, so an event handler could see inconsistent state
			lineSegment = null;
			deferredEventList.AddDeletedAnchor(this);
		}

		internal void RaiseDeleted()
		{
			if (Deleted != null)
			{
				Deleted(this, EventArgs.Empty);
			}
		}

		internal TextAnchor(LineSegment lineSegment, int columnNumber)
		{
			this.lineSegment = lineSegment;
			this.columnNumber = columnNumber;
		}

		public override string ToString()
		{
			if (IsDeleted)
			{
				return "[TextAnchor (deleted)]";
			}
			else
			{
				return "[TextAnchor " + Location + "]";
			}
		}
	}
}
