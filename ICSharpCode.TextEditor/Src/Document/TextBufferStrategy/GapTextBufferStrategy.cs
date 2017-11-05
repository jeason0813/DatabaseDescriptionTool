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
using System.Text;

namespace ICSharpCode.TextEditor.Document
{
	public class GapTextBufferStrategy : ITextBufferStrategy
	{
#if DEBUG
		private readonly int creatorThread = System.Threading.Thread.CurrentThread.ManagedThreadId;

		private void CheckThread()
		{
			if (System.Threading.Thread.CurrentThread.ManagedThreadId != creatorThread)
			{
				throw new InvalidOperationException("GapTextBufferStategy is not thread-safe!");
			}
		}
#endif

		private char[] buffer = new char[0];
		private string cachedContent;

		private int gapBeginOffset;
		private int gapEndOffset;
		private int gapLength; // gapLength == gapEndOffset - gapBeginOffset

		private const int minGapLength = 128;
		private const int maxGapLength = 2048;

		public int Length
		{
			get
			{
				return buffer.Length - gapLength;
			}
		}

		public void SetContent(string text)
		{
			if (text == null)
			{
				text = string.Empty;
			}

			cachedContent = text;
			buffer = text.ToCharArray();
			gapBeginOffset = gapEndOffset = gapLength = 0;
		}

		public char GetCharAt(int offset)
		{
#if DEBUG
			CheckThread();
#endif

			if (offset < 0 || offset >= Length)
			{
				throw new ArgumentOutOfRangeException("offset", offset, "0 <= offset < " + Length.ToString());
			}

			return offset < gapBeginOffset ? buffer[offset] : buffer[offset + gapLength];
		}

		public string GetText(int offset, int length)
		{
#if DEBUG
			CheckThread();
#endif

			if (offset < 0 || offset > Length)
			{
				throw new ArgumentOutOfRangeException("offset", offset, "0 <= offset <= " + Length.ToString());
			}

			if (length < 0 || offset + length > Length)
			{
				throw new ArgumentOutOfRangeException("length", length, "0 <= length, offset(" + offset + ")+length <= " + Length.ToString());
			}

			if (offset == 0 && length == Length)
			{
				if (cachedContent != null)
				{
					return cachedContent;
				}
				else
				{
					return cachedContent = GetTextInternal(offset, length);
				}
			}
			else
			{
				return GetTextInternal(offset, length);
			}
		}

		private string GetTextInternal(int offset, int length)
		{
			int end = offset + length;

			if (end < gapBeginOffset)
			{
				return new string(buffer, offset, length);
			}

			if (offset > gapBeginOffset)
			{
				return new string(buffer, offset + gapLength, length);
			}

			int block1Size = gapBeginOffset - offset;
			int block2Size = end - gapBeginOffset;

			StringBuilder buf = new StringBuilder(block1Size + block2Size);
			buf.Append(buffer, offset, block1Size);
			buf.Append(buffer, gapEndOffset, block2Size);

			return buf.ToString();
		}

		public void Insert(int offset, string text)
		{
			Replace(offset, 0, text);
		}

		public void Remove(int offset, int length)
		{
			Replace(offset, length, string.Empty);
		}

		public void Replace(int offset, int length, string text)
		{
			if (text == null)
			{
				text = string.Empty;
			}

#if DEBUG
			CheckThread();
#endif

			if (offset < 0 || offset > Length)
			{
				throw new ArgumentOutOfRangeException("offset", offset, "0 <= offset <= " + Length.ToString());
			}

			if (length < 0 || offset + length > Length)
			{
				throw new ArgumentOutOfRangeException("length", length, "0 <= length, offset+length <= " + Length.ToString());
			}

			cachedContent = null;

			// Math.Max is used so that if we need to resize the array
			// the new array has enough space for all old chars
			PlaceGap(offset, text.Length - length);
			gapEndOffset += length; // delete removed text
			text.CopyTo(0, buffer, gapBeginOffset, text.Length);
			gapBeginOffset += text.Length;
			gapLength = gapEndOffset - gapBeginOffset;

			if (gapLength > maxGapLength)
			{
				MakeNewBuffer(gapBeginOffset, minGapLength);
			}
		}

		private void PlaceGap(int newGapOffset, int minRequiredGapLength)
		{
			if (gapLength < minRequiredGapLength)
			{
				// enlarge gap
				MakeNewBuffer(newGapOffset, minRequiredGapLength);
			}
			else
			{
				while (newGapOffset < gapBeginOffset)
				{
					buffer[--gapEndOffset] = buffer[--gapBeginOffset];
				}
				while (newGapOffset > gapBeginOffset)
				{
					buffer[gapBeginOffset++] = buffer[gapEndOffset++];
				}
			}
		}

		private void MakeNewBuffer(int newGapOffset, int newGapLength)
		{
			if (newGapLength < minGapLength)
			{
				newGapLength = minGapLength;
			}

			char[] newBuffer = new char[Length + newGapLength];

			if (newGapOffset < gapBeginOffset)
			{
				// gap is moving backwards

				// first part:
				Array.Copy(buffer, 0, newBuffer, 0, newGapOffset);
				// moving middle part:
				Array.Copy(buffer, newGapOffset, newBuffer, newGapOffset + newGapLength, gapBeginOffset - newGapOffset);
				// last part:
				Array.Copy(buffer, gapEndOffset, newBuffer, newBuffer.Length - (buffer.Length - gapEndOffset), buffer.Length - gapEndOffset);
			}
			else
			{
				// gap is moving forwards
				// first part:
				Array.Copy(buffer, 0, newBuffer, 0, gapBeginOffset);
				// moving middle part:
				Array.Copy(buffer, gapEndOffset, newBuffer, gapBeginOffset, newGapOffset - gapBeginOffset);
				// last part:
				int lastPartLength = newBuffer.Length - (newGapOffset + newGapLength);
				Array.Copy(buffer, buffer.Length - lastPartLength, newBuffer, newGapOffset + newGapLength, lastPartLength);
			}

			gapBeginOffset = newGapOffset;
			gapEndOffset = newGapOffset + newGapLength;
			gapLength = newGapLength;
			buffer = newBuffer;
		}
	}
}
