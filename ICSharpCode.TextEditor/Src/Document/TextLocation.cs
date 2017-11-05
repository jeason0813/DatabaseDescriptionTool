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

namespace ICSharpCode.TextEditor
{
	/// <summary>
	/// A line/column position.
	/// Text editor lines/columns are counting from zero.
	/// </summary>
	public struct TextLocation : IComparable<TextLocation>, IEquatable<TextLocation>
	{
		/// <summary>
		/// Represents no text location (-1, -1).
		/// </summary>
		public static readonly TextLocation Empty = new TextLocation(-1, -1);

		public TextLocation(int column, int line)
		{
			x = column;
			y = line;
		}

		private int x, y;

		public int X
		{
			get
			{
				return x;
			}
			set
			{
				x = value;
			}
		}

		public int Y
		{
			get
			{
				return y;
			}
			set
			{
				y = value;
			}
		}

		public int Line
		{
			get
			{
				return y;
			}
			set
			{
				y = value;
			}
		}

		public int Column
		{
			get
			{
				return x;
			}
			set
			{
				x = value;
			}
		}

		public bool IsEmpty
		{
			get
			{
				return x <= 0 && y <= 0;
			}
		}

		public override string ToString()
		{
			return string.Format("(Line {1}, Col {0})", x, y);
		}

		public override int GetHashCode()
		{
			return unchecked(87 * x.GetHashCode() ^ y.GetHashCode());
		}

		public override bool Equals(object obj)
		{
			if (!(obj is TextLocation))
			{
				return false;
			}

			return (TextLocation)obj == this;
		}

		public bool Equals(TextLocation other)
		{
			return this == other;
		}

		public static bool operator ==(TextLocation a, TextLocation b)
		{
			return a.x == b.x && a.y == b.y;
		}

		public static bool operator !=(TextLocation a, TextLocation b)
		{
			return a.x != b.x || a.y != b.y;
		}

		public static bool operator <(TextLocation a, TextLocation b)
		{
			if (a.y < b.y)
			{
				return true;
			}
			else if (a.y == b.y)
			{
				return a.x < b.x;
			}
			else
			{
				return false;
			}
		}

		public static bool operator >(TextLocation a, TextLocation b)
		{
			if (a.y > b.y)
			{
				return true;
			}
			else if (a.y == b.y)
			{
				return a.x > b.x;
			}
			else
			{
				return false;
			}
		}

		public static bool operator <=(TextLocation a, TextLocation b)
		{
			return !(a > b);
		}

		public static bool operator >=(TextLocation a, TextLocation b)
		{
			return !(a < b);
		}

		public int CompareTo(TextLocation other)
		{
			if (this == other)
			{
				return 0;
			}

			if (this < other)
			{
				return -1;
			}
			else
			{
				return 1;
			}
		}
	}
}
