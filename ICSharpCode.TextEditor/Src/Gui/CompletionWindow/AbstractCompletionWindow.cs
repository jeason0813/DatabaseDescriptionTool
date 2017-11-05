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
using System.Windows.Forms;

namespace ICSharpCode.TextEditor.Gui.CompletionWindow
{
	/// <summary>
	/// Description of AbstractCompletionWindow.
	/// </summary>
	public abstract class AbstractCompletionWindow : Form
	{
		protected TextEditorControl control;
		protected Size drawingSize;
		private Rectangle workingScreen;
		private readonly Form parentForm;

		protected AbstractCompletionWindow(Form parentForm, TextEditorControl control)
		{
			workingScreen = Screen.GetWorkingArea(parentForm);
			//			SetStyle(ControlStyles.Selectable, false);
			this.parentForm = parentForm;
			this.control = control;

			SetLocation();
			StartPosition = FormStartPosition.Manual;
			FormBorderStyle = FormBorderStyle.None;
			ShowInTaskbar = false;
			MinimumSize = new Size(1, 1);
			Size = new Size(1, 1);
		}

		protected virtual void SetLocation()
		{
			TextArea textArea = control.ActiveTextAreaControl.TextArea;
			TextLocation caretPos = textArea.Caret.Position;

			int xpos = textArea.TextView.GetDrawingXPos(caretPos.Y, caretPos.X);
			int rulerHeight = textArea.TextEditorProperties.ShowHorizontalRuler ? textArea.TextView.FontHeight : 0;
			Point pos = new Point(textArea.TextView.DrawingPosition.X + xpos, textArea.TextView.DrawingPosition.Y + (textArea.Document.GetVisibleLine(caretPos.Y)) * textArea.TextView.FontHeight - textArea.TextView.TextArea.VirtualTop.Y + textArea.TextView.FontHeight + rulerHeight);
			Point location = control.ActiveTextAreaControl.PointToScreen(pos);

			// set bounds
			Rectangle bounds = new Rectangle(location, drawingSize);

			if (!workingScreen.Contains(bounds))
			{
				if (bounds.Right > workingScreen.Right)
				{
					bounds.X = workingScreen.Right - bounds.Width;
				}

				if (bounds.Left < workingScreen.Left)
				{
					bounds.X = workingScreen.Left;
				}

				if (bounds.Top < workingScreen.Top)
				{
					bounds.Y = workingScreen.Top;
				}

				if (bounds.Bottom > workingScreen.Bottom)
				{
					bounds.Y = bounds.Y - bounds.Height - control.ActiveTextAreaControl.TextArea.TextView.FontHeight;

					if (bounds.Bottom > workingScreen.Bottom)
					{
						bounds.Y = workingScreen.Bottom - bounds.Height;
					}
				}
			}

			Bounds = bounds;
		}

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams p = base.CreateParams;
				AddShadowToWindow(p);
				return p;
			}
		}

		private static int shadowStatus;

		/// <summary>
		/// Adds a shadow to the create params if it is supported by the operating system.
		/// </summary>
		public static void AddShadowToWindow(CreateParams createParams)
		{
			if (shadowStatus == 0)
			{
				// Test OS version
				shadowStatus = -1; // shadow not supported

				if (Environment.OSVersion.Platform == PlatformID.Win32NT)
				{
					Version ver = Environment.OSVersion.Version;

					if (ver.Major > 5 || ver.Major == 5 && ver.Minor >= 1)
					{
						shadowStatus = 1;
					}
				}
			}

			if (shadowStatus == 1)
			{
				createParams.ClassStyle |= 0x00020000; // set CS_DROPSHADOW
			}
		}

		protected override bool ShowWithoutActivation
		{
			get
			{
				return true;
			}
		}

		protected void ShowCompletionWindow()
		{
			Owner = parentForm;
			Enabled = true;
			Show();

			control.Focus();

			if (parentForm != null)
			{
				parentForm.LocationChanged += ParentFormLocationChanged;
			}

			control.ActiveTextAreaControl.VScrollBar.ValueChanged += ParentFormLocationChanged;
			control.ActiveTextAreaControl.HScrollBar.ValueChanged += ParentFormLocationChanged;
			control.ActiveTextAreaControl.TextArea.DoProcessDialogKey += ProcessTextAreaKey;
			control.ActiveTextAreaControl.Caret.PositionChanged += CaretOffsetChanged;
			control.ActiveTextAreaControl.TextArea.LostFocus += TextEditorLostFocus;
			control.Resize += ParentFormLocationChanged;

			foreach (Control c in Controls)
			{
				c.MouseMove += ControlMouseMove;
			}
		}

		private void ParentFormLocationChanged(object sender, EventArgs e)
		{
			SetLocation();
		}

		public virtual bool ProcessKeyEvent(char ch)
		{
			return false;
		}

		protected virtual bool ProcessTextAreaKey(Keys keyData)
		{
			if (!Visible)
			{
				return false;
			}

			switch (keyData)
			{
				case Keys.Escape:
					Close();
					return true;
			}

			return false;
		}

		protected virtual void CaretOffsetChanged(object sender, EventArgs e)
		{
		}

		protected void TextEditorLostFocus(object sender, EventArgs e)
		{
			if (!control.ActiveTextAreaControl.TextArea.Focused && !ContainsFocus)
			{
				Close();
			}
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			// take out the inserted methods
			parentForm.LocationChanged -= ParentFormLocationChanged;

			foreach (Control c in Controls)
			{
				c.MouseMove -= ControlMouseMove;
			}

			if (control.ActiveTextAreaControl.VScrollBar != null)
			{
				control.ActiveTextAreaControl.VScrollBar.ValueChanged -= ParentFormLocationChanged;
			}

			if (control.ActiveTextAreaControl.HScrollBar != null)
			{
				control.ActiveTextAreaControl.HScrollBar.ValueChanged -= ParentFormLocationChanged;
			}

			control.ActiveTextAreaControl.TextArea.LostFocus -= TextEditorLostFocus;
			control.ActiveTextAreaControl.Caret.PositionChanged -= CaretOffsetChanged;
			control.ActiveTextAreaControl.TextArea.DoProcessDialogKey -= ProcessTextAreaKey;
			control.Resize -= ParentFormLocationChanged;

			Dispose();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			ControlMouseMove(this, e);
		}

		/// <summary>
		/// Invoked when the mouse moves over this form or any child control.
		/// Shows the mouse cursor on the text area if it has been hidden.
		/// </summary>
		/// <remarks>
		/// Derived classes should attach this handler to the MouseMove event
		/// of all created controls which are not added to the Controls
		/// collection.
		/// </remarks>
		protected void ControlMouseMove(object sender, MouseEventArgs e)
		{
			control.ActiveTextAreaControl.TextArea.ShowHiddenCursor(false);
		}
	}
}
