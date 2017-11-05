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

using System.Collections.Generic;

namespace ICSharpCode.TextEditor.Document
{
	/// <summary>
	/// A highlighting strategy for a buffer.
	/// </summary>
	public interface IHighlightingStrategy
	{
		/// <value>
		/// The name of the highlighting strategy, must be unique
		/// </value>
		string Name
		{
			get;
		}

		/// <value>
		/// The file extenstions on which this highlighting strategy gets
		/// used
		/// </value>
		string[] Extensions
		{
			get;
		}

		Dictionary<string, string> Properties
		{
			get;
		}

		// returns special color. (BackGround Color, Cursor Color and so on)

		/// <remarks>
		/// Gets the color of an Environment element.
		/// </remarks>
		HighlightColor GetColorFor(string name);

		/// <remarks>
		/// Used internally, do not call
		/// </remarks>
		void MarkTokens(IDocument document, List<LineSegment> lines);

		/// <remarks>
		/// Used internally, do not call
		/// </remarks>
		void MarkTokens(IDocument document);
	}

	public interface IHighlightingStrategyUsingRuleSets : IHighlightingStrategy
	{
		/// <remarks>
		/// Used internally, do not call
		/// </remarks>
		HighlightRuleSet GetRuleSet(Span span);

		/// <remarks>
		/// Used internally, do not call
		/// </remarks>
		HighlightColor GetColor(IDocument document, LineSegment keyWord, int index, int length);
	}
}
