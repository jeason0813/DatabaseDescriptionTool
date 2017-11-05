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
	/// A stack of Span instances. Works like Stack&lt;Span&gt;, but can be cloned quickly
	/// because it is implemented as linked list.
	/// </summary>
	public sealed class SpanStack : ICloneable, IEnumerable<Span>
	{
		internal sealed class StackNode
		{
			public readonly StackNode Previous;
			public readonly Span Data;

			public StackNode(StackNode previous, Span data)
			{
				Previous = previous;
				Data = data;
			}
		}

		private StackNode top;

		public Span Pop()
		{
			Span s = top.Data;
			top = top.Previous;
			return s;
		}

		public Span Peek()
		{
			return top.Data;
		}

		public void Push(Span s)
		{
			top = new StackNode(top, s);
		}

		public bool IsEmpty
		{
			get
			{
				return top == null;
			}
		}

		public SpanStack Clone()
		{
			SpanStack n = new SpanStack();
			n.top = top;
			return n;
		}
		object ICloneable.Clone()
		{
			return Clone();
		}

		public Enumerator GetEnumerator()
		{
			return new Enumerator(new StackNode(top, null));
		}
		IEnumerator<Span> IEnumerable<Span>.GetEnumerator()
		{
			return GetEnumerator();
		}
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public struct Enumerator : IEnumerator<Span>
		{
			private StackNode c;

			internal Enumerator(StackNode node)
			{
				c = node;
			}

			public Span Current
			{
				get
				{
					return c.Data;
				}
			}

			object System.Collections.IEnumerator.Current
			{
				get
				{
					return c.Data;
				}
			}

			public void Dispose()
			{
				c = null;
			}

			public bool MoveNext()
			{
				c = c.Previous;
				return c != null;
			}

			public void Reset()
			{
				throw new NotSupportedException();
			}
		}
	}
}
