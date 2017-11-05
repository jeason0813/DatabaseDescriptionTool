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

namespace ICSharpCode.TextEditor.Util
{
	internal struct RedBlackTreeIterator<T> : IEnumerator<T>
	{
		internal RedBlackTreeNode<T> node;

		internal RedBlackTreeIterator(RedBlackTreeNode<T> node)
		{
			this.node = node;
		}

		public bool IsValid
		{
			get
			{
				return node != null;
			}
		}

		public T Current
		{
			get
			{
				if (node != null)
				{
					return node.val;
				}
				else
				{
					throw new InvalidOperationException();
				}
			}
		}

		object System.Collections.IEnumerator.Current
		{
			get
			{
				return Current;
			}
		}

		void IDisposable.Dispose()
		{
		}

		void System.Collections.IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		public bool MoveNext()
		{
			if (node == null)
				return false;
			if (node.right != null)
			{
				node = node.right.LeftMost;
			}
			else
			{
				RedBlackTreeNode<T> oldNode;

				do
				{
					oldNode = node;
					node = node.parent;
					// we are on the way up from the right part, don't output node again
				} while (node != null && node.right == oldNode);
			}

			return node != null;
		}

		public bool MoveBack()
		{
			if (node == null)
			{
				return false;
			}

			if (node.left != null)
			{
				node = node.left.RightMost;
			}
			else
			{
				RedBlackTreeNode<T> oldNode;

				do
				{
					oldNode = node;
					node = node.parent;
					// we are on the way up from the left part, don't output node again
				} while (node != null && node.left == oldNode);
			}

			return node != null;
		}
	}
}
