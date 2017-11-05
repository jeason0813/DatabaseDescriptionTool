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
using System.Windows.Forms;
using ICSharpCode.TextEditor;

public partial class HandleDescriptionTemplateForm : Form
{
	private bool _textChanged;
	private string _initialNameValue;

	public HandleDescriptionTemplateForm()
	{
		InitializeComponent();

		templateTextBox.TextEditorProperties.Font = new Font(ConfigHandler.DescriptionFontFamily, float.Parse(ConfigHandler.DescriptionFontSize));
		templateTextBox.Font = new Font(ConfigHandler.DescriptionFontFamily, float.Parse(ConfigHandler.DescriptionFontSize));
	}

	public void SetValues(DescriptionTemplate descriptionTemplate)
	{
		nameTextBox.Text = descriptionTemplate.Name;
		templateTextBox.Text = descriptionTemplate.Template;
		_textChanged = false;
		_initialNameValue = descriptionTemplate.Name.ToLower();
	}

	public DescriptionTemplate GetValue()
	{
		return new DescriptionTemplate(nameTextBox.Text, templateTextBox.Text);
	}

	private void okButton_Click(object sender, EventArgs e)
	{
		Save();
	}

	private bool UniqueName(string keyName)
	{
		foreach (DescriptionTemplate item in ConfigHandler.DescriptionTemplates)
		{
			if (keyName.ToLower() == item.Name.ToLower() && _initialNameValue != keyName.ToLower())
			{
				MessageBox.Show("Another Description Template with the same name already exists.\r\n\r\nDescription Template names must be unique.", ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				nameTextBox.Focus();
				return false;
			}
		}

		return true;
	}

	private bool ValidName(string keyName)
	{
		if (keyName == "")
		{
			MessageBox.Show("Name can't be empty.", ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			nameTextBox.Focus();
			return false;
		}

		return true;
	}

	private bool ValidLength(string text)
	{
		if (text.Length > 7500)
		{
			MessageBox.Show("Number of characters exceeds 7500.\r\n\r\nOnly 7500 characters are allowed.", ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			templateTextBox.Focus();
			return false;
		}

		return true;
	}

	private void SetCharactersLeftLabel()
	{
		charactersLeftLabel.Text = string.Format("Characters left: {0}", 7500 - templateTextBox.Text.Length);
	}

	private void NameTextBox_TextChanged(object sender, EventArgs e)
	{
		_textChanged = true;
	}

	private void HandleDescriptionTemplateForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (_textChanged)
		{
			DialogResult result = MessageBox.Show("Save changes?", ConfigHandler.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

			if (result.ToString() == "Yes")
			{
				bool closeWindow = Save();

				if (!closeWindow)
				{
					e.Cancel = true;
				}
			}
			else if (result.ToString() == "Cancel")
			{
				e.Cancel = true;
			}
		}
	}

	private bool Save()
	{
		if (UniqueName(nameTextBox.Text) && ValidName(nameTextBox.Text) && ValidLength(templateTextBox.Text))
		{
			if (_textChanged)
			{
				DialogResult = DialogResult.OK;
			}
			else
			{
				DialogResult = DialogResult.Cancel;
			}

			_textChanged = false;
			return true;
		}

		return false;
	}

	private void UserInitialsButton_Click(object sender, EventArgs e)
	{
		InsertText("{user}");
		templateTextBox.Focus();
	}

	private void TimeDateButton_Click(object sender, EventArgs e)
	{
		InsertText("{datetime}");
		templateTextBox.Focus();
	}

	private void InsertText(string text)
	{
		string selectedText = templateTextBox.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;

		if (templateTextBox.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected)
		{
			templateTextBox.ActiveTextAreaControl.Caret.Position = templateTextBox.ActiveTextAreaControl.SelectionManager.SelectionCollection[0].StartPosition;
		}

		templateTextBox.Document.Replace(templateTextBox.ActiveTextAreaControl.TextArea.Caret.Offset, selectedText.Length, text);
		templateTextBox.ActiveTextAreaControl.TextArea.SelectionManager.ClearSelection();
		templateTextBox.ActiveTextAreaControl.Caret.Position = templateTextBox.Document.OffsetToPosition(templateTextBox.ActiveTextAreaControl.TextArea.Caret.Offset + text.Length);
		templateTextBox.ActiveTextAreaControl.TextArea.Refresh();
	}

	private void TextEditorControl1_TextChanged(object sender, EventArgs e)
	{
		_textChanged = true;
		SetCharactersLeftLabel();
	}

	private void TextEditorControl1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.A)
		{
			SelectAll();
		}
	}

	private void SelectAll()
	{
		TextLocation startPosition = new TextLocation(0, 0);

		int textLength = templateTextBox.ActiveTextAreaControl.Document.TextLength;
		TextLocation endPosition = new TextLocation();
		endPosition.Column = templateTextBox.Document.OffsetToPosition(textLength).Column;
		endPosition.Line = templateTextBox.Document.OffsetToPosition(textLength).Line;

		templateTextBox.ActiveTextAreaControl.SelectionManager.SetSelection(startPosition, endPosition);
		templateTextBox.ActiveTextAreaControl.Caret.Position = endPosition;
	}

	private void UndoToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		templateTextBox.Undo();
	}

	private void RedoToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		templateTextBox.Redo();
	}

	private void CutToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		templateTextBox.ActiveTextAreaControl.TextArea.ClipboardHandler.Cut(sender, e);
	}

	private void CopyToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		templateTextBox.ActiveTextAreaControl.TextArea.ClipboardHandler.Copy(sender, e);
	}

	private void PasteToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		templateTextBox.ActiveTextAreaControl.TextArea.ClipboardHandler.Paste(sender, e);
	}

	private void DeleteToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		templateTextBox.ActiveTextAreaControl.TextArea.ClipboardHandler.Delete(sender, e);
	}

	private void SelectAllToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		SelectAll();
	}

	private void TabControlContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
	{
		if (templateTextBox.Document.UndoStack.CanUndo)
		{
			undoToolStripMenuItem1.Enabled = true;
		}
		else
		{
			undoToolStripMenuItem1.Enabled = false;
		}

		if (templateTextBox.Document.UndoStack.CanRedo)
		{
			redoToolStripMenuItem1.Enabled = true;
		}
		else
		{
			redoToolStripMenuItem1.Enabled = false;
		}
	}
}
