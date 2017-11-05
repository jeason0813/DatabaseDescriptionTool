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
using System.Drawing;
using SWF = System.Windows.Forms;

namespace ICSharpCode.TextEditor.Document
{
	/// <summary>
	/// Description of Bookmark.
	/// </summary>
	public class Bookmark
	{
		private IDocument document;
		private TextAnchor anchor;
		private TextLocation location;
		private bool isEnabled;

		public IDocument Document
		{
			get
			{
				return document;
			}
			set
			{
				if (document != value)
				{
					if (anchor != null)
					{
						location = anchor.Location;
						anchor = null;
					}

					document = value;
					CreateAnchor();
					OnDocumentChanged(EventArgs.Empty);
				}
			}
		}

		private void CreateAnchor()
		{
			if (document != null)
			{
				LineSegment line = document.GetLineSegment(Math.Max(0, Math.Min(location.Line, document.TotalNumberOfLines - 1)));
				anchor = line.CreateAnchor(Math.Max(0, Math.Min(location.Column, line.Length)));
				// after insertion: keep bookmarks after the initial whitespace (see DefaultFormattingStrategy.SmartReplaceLine)
				anchor.MovementType = AnchorMovementType.AfterInsertion;
				anchor.Deleted += AnchorDeleted;
			}
		}

		private void AnchorDeleted(object sender, EventArgs e)
		{
			document.BookmarkManager.RemoveMark(this);
		}

		/// <summary>
		/// Gets the TextAnchor used for this bookmark.
		/// Is null if the bookmark is not connected to a document.
		/// </summary>
		public TextAnchor Anchor
		{
			get { return anchor; }
		}

		public TextLocation Location
		{
			get
			{
				if (anchor != null)
				{
					return anchor.Location;
				}
				else
				{
					return location;
				}
			}
			set
			{
				location = value;
				CreateAnchor();
			}
		}

		public event EventHandler DocumentChanged;

		protected virtual void OnDocumentChanged(EventArgs e)
		{
			if (DocumentChanged != null)
			{
				DocumentChanged(this, e);
			}
		}

		public bool IsEnabled
		{
			get
			{
				return isEnabled;
			}
			set
			{
				if (isEnabled != value)
				{
					isEnabled = value;

					if (document != null)
					{
						document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.SingleLine, LineNumber));
						document.CommitUpdate();
					}

					OnIsEnabledChanged(EventArgs.Empty);
				}
			}
		}

		public event EventHandler IsEnabledChanged;

		protected virtual void OnIsEnabledChanged(EventArgs e)
		{
			if (IsEnabledChanged != null)
			{
				IsEnabledChanged(this, e);
			}
		}

		public int LineNumber
		{
			get
			{
				if (anchor != null)
				{
					return anchor.LineNumber;
				}
				else
				{
					return location.Line;
				}
			}
		}

		public int ColumnNumber
		{
			get
			{
				if (anchor != null)
				{
					return anchor.ColumnNumber;
				}
				else
				{
					return location.Column;
				}
			}
		}

		/// <summary>
		/// Gets if the bookmark can be toggled off using the 'set/unset bookmark' command.
		/// </summary>
		public virtual bool CanToggle
		{
			get
			{
				return true;
			}
		}

		public Bookmark(IDocument document, TextLocation location) : this(document, location, true)
		{
		}

		public Bookmark(IDocument document, TextLocation location, bool isEnabled)
		{
			this.document = document;
			this.isEnabled = isEnabled;
			Location = location;
		}

		public virtual bool Click(SWF.Control parent, SWF.MouseEventArgs e)
		{
			if (e.Button == SWF.MouseButtons.Left && CanToggle)
			{
				document.BookmarkManager.RemoveMark(this);
				return true;
			}

			return false;
		}

		public virtual void Draw(IconBarMargin margin, Graphics g, Point p)
		{
			margin.DrawBookmark(g, p.Y, isEnabled);
		}
	}
}
