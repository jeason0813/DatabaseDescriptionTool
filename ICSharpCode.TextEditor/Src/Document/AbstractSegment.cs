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
	/// <summary>
	/// This interface is used to describe a span inside a text sequence
	/// </summary>
	public class AbstractSegment : ISegment
	{
		[CLSCompliant(false)]
		protected int offset = -1;
		[CLSCompliant(false)]
		protected int length = -1;

		#region ICSharpCode.TextEditor.Document.ISegment interface implementation
		public virtual int Offset
		{
			get
			{
				return offset;
			}
			set
			{
				offset = value;
			}
		}

		public virtual int Length
		{
			get
			{
				return length;
			}
			set
			{
				length = value;
			}
		}

		#endregion

		public override string ToString()
		{
			return string.Format("[AbstractSegment: Offset = {0}, Length = {1}]", Offset, Length);
		}
	}
}
