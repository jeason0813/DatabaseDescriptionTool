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

partial class TabPageUserControl
{
	/// <summary> 
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary> 
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Component Designer generated code

	/// <summary> 
	/// Required method for Designer support - do not modify 
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
			this.components = new System.ComponentModel.Container();
			this.infoTextBox = new System.Windows.Forms.TextBox();
			this.descriptionTextBox = new ICSharpCode.TextEditor.TextEditorControl();
			this.tabControlContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.undoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.redoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.cutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.selectAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControlContextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// infoTextBox
			// 
			this.infoTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.infoTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.infoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.infoTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.infoTextBox.Location = new System.Drawing.Point(0, 182);
			this.infoTextBox.Multiline = true;
			this.infoTextBox.Name = "infoTextBox";
			this.infoTextBox.ReadOnly = true;
			this.infoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.infoTextBox.Size = new System.Drawing.Size(361, 39);
			this.infoTextBox.TabIndex = 10;
			this.infoTextBox.TabStop = false;
			this.infoTextBox.Enter += new System.EventHandler(this.InfoTextBox_Enter);
			// 
			// descriptionTextBox
			// 
			this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.descriptionTextBox.BackColor = System.Drawing.SystemColors.Menu;
			this.descriptionTextBox.ContextMenuStrip = this.tabControlContextMenuStrip;
			this.descriptionTextBox.IsReadOnly = false;
			this.descriptionTextBox.Location = new System.Drawing.Point(0, 0);
			this.descriptionTextBox.Name = "descriptionTextBox";
			this.descriptionTextBox.ShowVRuler = false;
			this.descriptionTextBox.Size = new System.Drawing.Size(361, 176);
			this.descriptionTextBox.TabIndex = 11;
			this.descriptionTextBox.TextChanged += new System.EventHandler(this.DescriptionTextBox_TextChanged);
			// 
			// tabControlContextMenuStrip
			// 
			this.tabControlContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem1,
            this.redoToolStripMenuItem1,
            this.toolStripSeparator6,
            this.cutToolStripMenuItem1,
            this.copyToolStripMenuItem1,
            this.pasteToolStripMenuItem1,
            this.deleteToolStripMenuItem1,
            this.toolStripSeparator7,
            this.selectAllToolStripMenuItem1});
			this.tabControlContextMenuStrip.Name = "ddlContextMenuStrip1";
			this.tabControlContextMenuStrip.Size = new System.Drawing.Size(165, 170);
			this.tabControlContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.TabControlContextMenuStrip_Opening);
			// 
			// undoToolStripMenuItem1
			// 
			this.undoToolStripMenuItem1.Name = "undoToolStripMenuItem1";
			this.undoToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl+Z";
			this.undoToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
			this.undoToolStripMenuItem1.Text = "Undo";
			this.undoToolStripMenuItem1.Click += new System.EventHandler(this.UndoToolStripMenuItem1_Click);
			// 
			// redoToolStripMenuItem1
			// 
			this.redoToolStripMenuItem1.Name = "redoToolStripMenuItem1";
			this.redoToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl+Y";
			this.redoToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
			this.redoToolStripMenuItem1.Text = "Redo";
			this.redoToolStripMenuItem1.Click += new System.EventHandler(this.RedoToolStripMenuItem1_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(161, 6);
			// 
			// cutToolStripMenuItem1
			// 
			this.cutToolStripMenuItem1.Name = "cutToolStripMenuItem1";
			this.cutToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl+X";
			this.cutToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
			this.cutToolStripMenuItem1.Text = "Cut";
			this.cutToolStripMenuItem1.Click += new System.EventHandler(this.CutToolStripMenuItem1_Click);
			// 
			// copyToolStripMenuItem1
			// 
			this.copyToolStripMenuItem1.Name = "copyToolStripMenuItem1";
			this.copyToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl+C";
			this.copyToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
			this.copyToolStripMenuItem1.Text = "Copy";
			this.copyToolStripMenuItem1.Click += new System.EventHandler(this.CopyToolStripMenuItem1_Click);
			// 
			// pasteToolStripMenuItem1
			// 
			this.pasteToolStripMenuItem1.Name = "pasteToolStripMenuItem1";
			this.pasteToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl+V";
			this.pasteToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
			this.pasteToolStripMenuItem1.Text = "Paste";
			this.pasteToolStripMenuItem1.Click += new System.EventHandler(this.PasteToolStripMenuItem1_Click);
			// 
			// deleteToolStripMenuItem1
			// 
			this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
			this.deleteToolStripMenuItem1.ShortcutKeyDisplayString = "Del";
			this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
			this.deleteToolStripMenuItem1.Text = "Delete";
			this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.DeleteToolStripMenuItem1_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(161, 6);
			// 
			// selectAllToolStripMenuItem1
			// 
			this.selectAllToolStripMenuItem1.Name = "selectAllToolStripMenuItem1";
			this.selectAllToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl+A";
			this.selectAllToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
			this.selectAllToolStripMenuItem1.Text = "Select All";
			this.selectAllToolStripMenuItem1.Click += new System.EventHandler(this.SelectAllToolStripMenuItem1_Click);
			// 
			// TabPageUserControl
			// 
			this.Controls.Add(this.descriptionTextBox);
			this.Controls.Add(this.infoTextBox);
			this.Name = "TabPageUserControl";
			this.Size = new System.Drawing.Size(361, 221);
			this.tabControlContextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.TextBox infoTextBox;
	private ICSharpCode.TextEditor.TextEditorControl descriptionTextBox;
	private System.Windows.Forms.ContextMenuStrip tabControlContextMenuStrip;
	private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem1;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
	private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
	private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem1;
}
