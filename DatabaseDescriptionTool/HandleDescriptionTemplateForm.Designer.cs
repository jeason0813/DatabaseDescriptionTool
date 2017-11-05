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

partial class HandleDescriptionTemplateForm
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

	#region Windows Form Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HandleDescriptionTemplateForm));
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.nameLabel = new System.Windows.Forms.Label();
			this.templateLabel = new System.Windows.Forms.Label();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.timeDateButton = new System.Windows.Forms.Button();
			this.userInitialsButton = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.charactersLeftLabel = new System.Windows.Forms.Label();
			this.templateTextBox = new ICSharpCode.TextEditor.TextEditorControl();
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
			this.groupBox1.SuspendLayout();
			this.tabControlContextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// nameTextBox
			// 
			this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.nameTextBox.Location = new System.Drawing.Point(105, 6);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(275, 20);
			this.nameTextBox.TabIndex = 0;
			this.nameTextBox.TextChanged += new System.EventHandler(this.NameTextBox_TextChanged);
			// 
			// nameLabel
			// 
			this.nameLabel.AutoSize = true;
			this.nameLabel.Location = new System.Drawing.Point(12, 9);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(38, 13);
			this.nameLabel.TabIndex = 1;
			this.nameLabel.Text = "Name:";
			// 
			// templateLabel
			// 
			this.templateLabel.AutoSize = true;
			this.templateLabel.Location = new System.Drawing.Point(12, 35);
			this.templateLabel.Name = "templateLabel";
			this.templateLabel.Size = new System.Drawing.Size(54, 13);
			this.templateLabel.TabIndex = 2;
			this.templateLabel.Text = "Template:";
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(305, 246);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 24);
			this.cancelButton.TabIndex = 5;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(224, 246);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 24);
			this.okButton.TabIndex = 4;
			this.okButton.Text = "Ok";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// timeDateButton
			// 
			this.timeDateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.timeDateButton.Location = new System.Drawing.Point(6, 46);
			this.timeDateButton.Name = "timeDateButton";
			this.timeDateButton.Size = new System.Drawing.Size(75, 24);
			this.timeDateButton.TabIndex = 3;
			this.timeDateButton.Text = "Date && Time";
			this.timeDateButton.UseVisualStyleBackColor = true;
			this.timeDateButton.Click += new System.EventHandler(this.TimeDateButton_Click);
			// 
			// userInitialsButton
			// 
			this.userInitialsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.userInitialsButton.Location = new System.Drawing.Point(6, 17);
			this.userInitialsButton.Name = "userInitialsButton";
			this.userInitialsButton.Size = new System.Drawing.Size(75, 24);
			this.userInitialsButton.TabIndex = 2;
			this.userInitialsButton.Text = "User Initials";
			this.userInitialsButton.UseVisualStyleBackColor = true;
			this.userInitialsButton.Click += new System.EventHandler(this.UserInitialsButton_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add(this.userInitialsButton);
			this.groupBox1.Controls.Add(this.timeDateButton);
			this.groupBox1.Location = new System.Drawing.Point(12, 161);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(87, 75);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Insert";
			// 
			// charactersLeftLabel
			// 
			this.charactersLeftLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.charactersLeftLabel.AutoSize = true;
			this.charactersLeftLabel.Location = new System.Drawing.Point(9, 251);
			this.charactersLeftLabel.Name = "charactersLeftLabel";
			this.charactersLeftLabel.Size = new System.Drawing.Size(0, 13);
			this.charactersLeftLabel.TabIndex = 7;
			// 
			// templateTextBox
			// 
			this.templateTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.templateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.templateTextBox.ContextMenuStrip = this.tabControlContextMenuStrip;
			this.templateTextBox.IsReadOnly = false;
			this.templateTextBox.Location = new System.Drawing.Point(105, 32);
			this.templateTextBox.Name = "templateTextBox";
			this.templateTextBox.ShowVRuler = false;
			this.templateTextBox.Size = new System.Drawing.Size(275, 204);
			this.templateTextBox.TabIndex = 1;
			this.templateTextBox.TextChanged += new System.EventHandler(this.TextEditorControl1_TextChanged);
			this.templateTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextEditorControl1_KeyDown);
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
			// HandleDescriptionTemplateForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(392, 277);
			this.Controls.Add(this.templateTextBox);
			this.Controls.Add(this.charactersLeftLabel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.templateLabel);
			this.Controls.Add(this.nameLabel);
			this.Controls.Add(this.nameTextBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(408, 316);
			this.Name = "HandleDescriptionTemplateForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Description Template";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HandleDescriptionTemplateForm_FormClosing);
			this.groupBox1.ResumeLayout(false);
			this.tabControlContextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.TextBox nameTextBox;
	private System.Windows.Forms.Label nameLabel;
	private System.Windows.Forms.Label templateLabel;
	private System.Windows.Forms.Button cancelButton;
	private System.Windows.Forms.Button okButton;
	private System.Windows.Forms.Button timeDateButton;
	private System.Windows.Forms.Button userInitialsButton;
	private System.Windows.Forms.GroupBox groupBox1;
	private System.Windows.Forms.Label charactersLeftLabel;
	private ICSharpCode.TextEditor.TextEditorControl templateTextBox;
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
