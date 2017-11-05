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

using ICSharpCode.TextEditor.Util;

namespace ICSharpCode.TextEditor.Gui.CompletionWindow
{
	public interface IDeclarationViewWindow
	{
		string Description
		{
			get;
			set;
		}

		void ShowDeclarationViewWindow();
		void CloseDeclarationViewWindow();
	}

	public class DeclarationViewWindow : Form, IDeclarationViewWindow
	{
		private string description = string.Empty;
		private bool fixedWidth;

		public string Description
		{
			get
			{
				return description;
			}
			set
			{
				description = value;

				if (value == null && Visible)
				{
					Visible = false;
				}
				else if (value != null)
				{
					if (!Visible)
					{
						ShowDeclarationViewWindow();
					}

					Refresh();
				}
			}
		}

		public bool FixedWidth
		{
			get
			{
				return fixedWidth;
			}
			set
			{
				fixedWidth = value;
			}
		}

		public int GetRequiredLeftHandSideWidth(Point p)
		{
			if (!string.IsNullOrEmpty(description))
			{
				using (Graphics g = CreateGraphics())
				{
					Size s = TipPainterTools.GetLeftHandSideDrawingSizeHelpTipFromCombinedDescription(this, g, Font, null, description, p);
					return s.Width;
				}
			}

			return 0;
		}

		public bool HideOnClick;

		public DeclarationViewWindow(Form parent)
		{
			SetStyle(ControlStyles.Selectable, false);
			StartPosition = FormStartPosition.Manual;
			FormBorderStyle = FormBorderStyle.None;
			Owner = parent;
			ShowInTaskbar = false;
			Size = new Size(0, 0);
			CreateHandle();
		}

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams p = base.CreateParams;
				AbstractCompletionWindow.AddShadowToWindow(p);
				return p;
			}
		}

		protected override bool ShowWithoutActivation
		{
			get
			{
				return true;
			}
		}

		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			if (HideOnClick) Hide();
		}

		public void ShowDeclarationViewWindow()
		{
			Show();
		}

		public void CloseDeclarationViewWindow()
		{
			Close();
			Dispose();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			if (!string.IsNullOrEmpty(description))
			{
				if (fixedWidth)
				{
					TipPainterTools.DrawFixedWidthHelpTipFromCombinedDescription(this, pe.Graphics, Font, null, description);
				}
				else
				{
					TipPainterTools.DrawHelpTipFromCombinedDescription(this, pe.Graphics, Font, null, description);
				}
			}
		}

		protected override void OnPaintBackground(PaintEventArgs pe)
		{
			pe.Graphics.FillRectangle(SystemBrushes.Info, pe.ClipRectangle);
		}
	}
}
