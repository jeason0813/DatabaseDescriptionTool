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
	public class LineCountChangeEventArgs : EventArgs
	{
		private readonly IDocument document;
		private readonly int start;
		private readonly int moved;

		/// <returns>
		/// always a valid Document which is related to the Event.
		/// </returns>
		public IDocument Document
		{
			get
			{
				return document;
			}
		}

		/// <returns>
		/// -1 if no offset was specified for this event
		/// </returns>
		public int LineStart
		{
			get
			{
				return start;
			}
		}

		/// <returns>
		/// -1 if no length was specified for this event
		/// </returns>
		public int LinesMoved
		{
			get
			{
				return moved;
			}
		}

		public LineCountChangeEventArgs(IDocument document, int lineStart, int linesMoved)
		{
			this.document = document;
			start = lineStart;
			moved = linesMoved;
		}
	}

	public class LineEventArgs : EventArgs
	{
		private readonly IDocument document;
		private readonly LineSegment lineSegment;

		public IDocument Document
		{
			get { return document; }
		}

		public LineSegment LineSegment
		{
			get { return lineSegment; }
		}

		public LineEventArgs(IDocument document, LineSegment lineSegment)
		{
			this.document = document;
			this.lineSegment = lineSegment;
		}

		public override string ToString()
		{
			return string.Format("[LineEventArgs Document={0} LineSegment={1}]", document, lineSegment);
		}
	}

	public class LineLengthChangeEventArgs : LineEventArgs
	{
		private readonly int lengthDelta;

		public int LengthDelta
		{
			get
			{
				return lengthDelta;
			}
		}

		public LineLengthChangeEventArgs(IDocument document, LineSegment lineSegment, int moved) : base(document, lineSegment)
		{
			lengthDelta = moved;
		}

		public override string ToString()
		{
			return string.Format("[LineLengthEventArgs Document={0} LineSegment={1} LengthDelta={2}]", Document, LineSegment, lengthDelta);
		}
	}
}
