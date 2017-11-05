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

using System.Text;

namespace ICSharpCode.TextEditor.Document
{
	/// <summary>
	/// This interface represents a container which holds a text sequence and
	/// all necessary information about it. It is used as the base for a text editor.
	/// </summary>
	public class DocumentFactory
	{
		/// <remarks>
		/// Creates a new <see cref="IDocument"/> object. Only create
		/// <see cref="IDocument"/> with this method.
		/// </remarks>
		public IDocument CreateDocument()
		{
			DefaultDocument doc = new DefaultDocument();
			doc.TextBufferStrategy = new GapTextBufferStrategy();
			doc.FormattingStrategy = new DefaultFormattingStrategy();
			doc.LineManager = new LineManager(doc, null);
			doc.FoldingManager = new FoldingManager(doc, doc.LineManager);
			doc.FoldingManager.FoldingStrategy = null; //new ParserFoldingStrategy();
			doc.MarkerStrategy = new MarkerStrategy(doc);
			doc.BookmarkManager = new BookmarkManager(doc, doc.LineManager);
			return doc;
		}

		/// <summary>
		/// Creates a new document and loads the given file
		/// </summary>
		public IDocument CreateFromTextBuffer(ITextBufferStrategy textBuffer)
		{
			DefaultDocument doc = (DefaultDocument)CreateDocument();
			doc.TextContent = textBuffer.GetText(0, textBuffer.Length);
			doc.TextBufferStrategy = textBuffer;
			return doc;
		}

		/// <summary>
		/// Creates a new document and loads the given file
		/// </summary>
		public IDocument CreateFromFile(string fileName)
		{
			IDocument document = CreateDocument();
			document.TextContent = Util.FileReader.ReadFileContent(fileName, Encoding.Default);
			return document;
		}
	}
}
