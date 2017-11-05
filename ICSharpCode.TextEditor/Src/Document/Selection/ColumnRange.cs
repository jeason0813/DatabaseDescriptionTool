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

namespace ICSharpCode.TextEditor.Document
{
	public class ColumnRange
	{
		public static readonly ColumnRange NoColumn = new ColumnRange(-2, -2);
		public static readonly ColumnRange WholeColumn = new ColumnRange(-1, -1);

		private int startColumn;
		private int endColumn;

		public int StartColumn
		{
			get
			{
				return startColumn;
			}
			set
			{
				startColumn = value;
			}
		}

		public int EndColumn
		{
			get
			{
				return endColumn;
			}
			set
			{
				endColumn = value;
			}
		}

		public ColumnRange(int startColumn, int endColumn)
		{
			this.startColumn = startColumn;
			this.endColumn = endColumn;

		}

		public override int GetHashCode()
		{
			return startColumn + (endColumn << 16);
		}

		public override bool Equals(object obj)
		{
			if (obj is ColumnRange)
			{
				return ((ColumnRange)obj).startColumn == startColumn && ((ColumnRange)obj).endColumn == endColumn;

			}

			return false;
		}

		public override string ToString()
		{
			return string.Format("[ColumnRange: StartColumn={0}, EndColumn={1}]", startColumn, endColumn);
		}
	}
}
