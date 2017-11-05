/*
Copyright (C) 2017 Lars Hove Christiansen
http://virtcore.com

This file is a part of Database Description Tool

	Database Description Tool is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	Database Description Tool is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with Database Description Tool. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ICSharpCode.TextEditor;

public partial class TabPageUserControl : UserControl
{
	[DllImport("user32.dll")]
	private static extern bool HideCaret(IntPtr hWnd);

	public delegate void SetCharacterCountEventEventHandler(string text);
	public event SetCharacterCountEventEventHandler SetCharacterCountEvent;

	public delegate void SaveEventHandler();
	public event SaveEventHandler SaveEvent;

	public delegate void CancelEventHandler();
	public event CancelEventHandler CancelEvent;

	public delegate void InvokeEditEventHandler();
	public event InvokeEditEventHandler InvokeEditEvent;

	private bool _descriptionChanged;
	private string _textBeforeEditing;
	private bool _readOnly = true;

	public TabPageUserControl()
	{
		InitializeComponent();
		infoTextBox.GotFocus += InfoTextBox_GotFocus;
		descriptionTextBox.ActiveTextAreaControl.TextArea.KeyPress += TextArea_KeyPress;
	}

	public void SetDescriptionFont(Font font)
	{
		descriptionTextBox.TextEditorProperties.Font = font;
		descriptionTextBox.Font = font;
		descriptionTextBox.ActiveTextAreaControl.TextArea.Refresh();
	}

	public int GetDescriptionLength()
	{
		return descriptionTextBox.Text.Length;
	}

	public bool GetDescriptionChanged()
	{
		return _descriptionChanged;
	}

	public string GetDescriptionBeforeEdit()
	{
		return _textBeforeEditing;
	}

	public string GetDescription()
	{
		return descriptionTextBox.Text;
	}

	public void GetCharacterCount()
	{
		if (!_readOnly)
		{
			FireSetCharacterCountEvent(descriptionTextBox.Text);
		}
	}

	public void SetInformation(string information)
	{
		infoTextBox.Text = information;
	}

	public void SetFocus()
	{
		if (!_readOnly)
		{
			descriptionTextBox.Focus();
		}
	}

	public void StartEdit()
	{
		_textBeforeEditing = descriptionTextBox.Text;
		_readOnly = false;
		descriptionTextBox.Document.ReadOnly = false;
	}

	public void StopEdit()
	{
		_readOnly = true;
		descriptionTextBox.Document.ReadOnly = true;
	}

	public void SetText(string text)
	{
		descriptionTextBox.Text = text;
		descriptionTextBox.ActiveTextAreaControl.Refresh();
		_descriptionChanged = false;
	}

	public void SetDescriptionChanged(bool value)
	{
		_descriptionChanged = value;
	}

	private void FireSetCharacterCountEvent(string text)
	{
		if (SetCharacterCountEvent != null)
		{
			SetCharacterCountEvent(text);
		}
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (descriptionTextBox.ActiveTextAreaControl.TextArea.Focused)
		{
			if (!_readOnly)
			{
				if ((int)keyData == 131155) // Keys.Control && Keys.S
				{
					FireSaveEvent();
					return true;
				}
				else if ((int)keyData == 27) // Keys.Escape
				{
					FireCancelEvent();
					return true;
				}
			}
			else
			{
				if ((int)keyData == 131160) // Keys.Control && Keys.X
				{
					cutToolStripMenuItem1.PerformClick();
					return true;
				}
				else if ((int)keyData == 131158) // Keys.Control && Keys.V
				{
					pasteToolStripMenuItem1.PerformClick();
					return true;
				}
				else if ((int)keyData == 46) // Keys.Delete
				{
					deleteToolStripMenuItem1.PerformClick();
					return true;
				}
			}

			if ((int)keyData == 131137) // Keys.Control && Keys.A
			{
				SelectAll();
				return true;
			}
		}

		return base.ProcessCmdKey(ref msg, keyData);
	}

	private void TextArea_KeyPress(object sender, KeyPressEventArgs e)
	{
		InvokeEditEvent();
	}

	private void FireSaveEvent()
	{
		if (SaveEvent != null)
		{
			SaveEvent();
		}
	}

	private void FireCancelEvent()
	{
		if (CancelEvent != null)
		{
			CancelEvent();
		}
	}

	private void FireInvokeEditEvent()
	{
		if (InvokeEditEvent != null)
		{
			InvokeEditEvent();
		}
	}

	private void InfoTextBox_GotFocus(object sender, EventArgs e)
	{
		infoTextBox.SelectionStart = 0;
		infoTextBox.SelectionLength = 0;
		HideCaret(infoTextBox.Handle);
	}

	private void InfoTextBox_Enter(object sender, EventArgs e)
	{
		infoTextBox.SelectionStart = 0;
		infoTextBox.SelectionLength = 0;
		HideCaret(infoTextBox.Handle);
	}

	private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
	{
		if (!_readOnly)
		{
			_descriptionChanged = true;
			FireSetCharacterCountEvent(descriptionTextBox.Text);
		}
	}

	private void SelectAll()
	{
		TextLocation startPosition = new TextLocation(0, 0);

		int textLength = descriptionTextBox.ActiveTextAreaControl.Document.TextLength;
		TextLocation endPosition = new TextLocation();
		endPosition.Column = descriptionTextBox.Document.OffsetToPosition(textLength).Column;
		endPosition.Line = descriptionTextBox.Document.OffsetToPosition(textLength).Line;

		descriptionTextBox.ActiveTextAreaControl.SelectionManager.SetSelection(startPosition, endPosition);
		descriptionTextBox.ActiveTextAreaControl.Caret.Position = endPosition;
	}

	private void UndoToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		if (_readOnly)
		{
			FireInvokeEditEvent();
		}

		descriptionTextBox.Undo();
	}

	private void RedoToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		if (_readOnly)
		{
			FireInvokeEditEvent();
		}

		descriptionTextBox.Redo();
	}

	private void CutToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		if (_readOnly)
		{
			FireInvokeEditEvent();
		}

		descriptionTextBox.ActiveTextAreaControl.TextArea.ClipboardHandler.Cut(sender, e);
	}

	private void CopyToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		descriptionTextBox.ActiveTextAreaControl.TextArea.ClipboardHandler.Copy(sender, e);
	}

	private void PasteToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		if (_readOnly)
		{
			FireInvokeEditEvent();
		}

		descriptionTextBox.ActiveTextAreaControl.TextArea.ClipboardHandler.Paste(sender, e);
	}

	private void DeleteToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		if (_readOnly)
		{
			FireInvokeEditEvent();
		}

		descriptionTextBox.ActiveTextAreaControl.TextArea.ClipboardHandler.Delete(sender, e);
	}

	private void SelectAllToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		SelectAll();
	}

	private void TabControlContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
	{
		if (descriptionTextBox.Document.UndoStack.CanUndo)
		{
			undoToolStripMenuItem1.Enabled = true;
		}
		else
		{
			undoToolStripMenuItem1.Enabled = false;
		}

		if (descriptionTextBox.Document.UndoStack.CanRedo)
		{
			redoToolStripMenuItem1.Enabled = true;
		}
		else
		{
			redoToolStripMenuItem1.Enabled = false;
		}
	}
}
