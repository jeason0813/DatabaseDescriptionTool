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

namespace ICSharpCode.TextEditor
{
	/// <summary>
	/// This enum describes all implemented request types
	/// </summary>
	public enum TextAreaUpdateType
	{
		WholeTextArea,
		SingleLine,
		SinglePosition,
		PositionToLineEnd,
		PositionToEnd,
		LinesBetween
	}

	/// <summary>
	/// This class is used to request an update of the textarea
	/// </summary>
	public class TextAreaUpdate
	{
		private readonly TextLocation position;
		private readonly TextAreaUpdateType type;

		public TextAreaUpdateType TextAreaUpdateType
		{
			get
			{
				return type;
			}
		}

		public TextLocation Position
		{
			get
			{
				return position;
			}
		}

		/// <summary>
		/// Creates a new instance of <see cref="TextAreaUpdate"/>
		/// </summary>
		public TextAreaUpdate(TextAreaUpdateType type)
		{
			this.type = type;
		}

		/// <summary>
		/// Creates a new instance of <see cref="TextAreaUpdate"/>
		/// </summary>
		public TextAreaUpdate(TextAreaUpdateType type, TextLocation position)
		{
			this.type = type;
			this.position = position;
		}

		/// <summary>
		/// Creates a new instance of <see cref="TextAreaUpdate"/>
		/// </summary>
		public TextAreaUpdate(TextAreaUpdateType type, int startLine, int endLine)
		{
			this.type = type;
			position = new TextLocation(startLine, endLine);
		}

		/// <summary>
		/// Creates a new instance of <see cref="TextAreaUpdate"/>
		/// </summary>
		public TextAreaUpdate(TextAreaUpdateType type, int singleLine)
		{
			this.type = type;
			position = new TextLocation(0, singleLine);
		}

		public override string ToString()
		{
			return string.Format("[TextAreaUpdate: Type={0}, Position={1}]", type, position);
		}
	}
}
