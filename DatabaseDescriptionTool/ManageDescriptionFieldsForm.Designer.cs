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

partial class ManageDescriptionFieldsForm
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageDescriptionFieldsForm));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.okButton = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.ImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
			this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.editButton = new System.Windows.Forms.Button();
			this.moveDownButton = new System.Windows.Forms.Button();
			this.moveUpButton = new System.Windows.Forms.Button();
			this.deleteButton = new System.Windows.Forms.Button();
			this.createButton = new System.Windows.Forms.Button();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.actionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteAllMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.createMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.editMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.cutMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.copyMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.selectAllMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.moveUpMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.moveDownMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.deleteAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.createMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.cutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
			this.selectAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.moveUpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moveDownMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.okButton.Location = new System.Drawing.Point(305, 246);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 24);
			this.okButton.TabIndex = 6;
			this.okButton.Text = "Ok";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.dataGridView1);
			this.groupBox1.Controls.Add(this.editButton);
			this.groupBox1.Controls.Add(this.moveDownButton);
			this.groupBox1.Controls.Add(this.moveUpButton);
			this.groupBox1.Controls.Add(this.deleteButton);
			this.groupBox1.Controls.Add(this.createButton);
			this.groupBox1.Location = new System.Drawing.Point(12, 27);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(368, 209);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Description Fields";
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowDrop = true;
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			this.dataGridView1.ColumnHeadersHeight = 20;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ImageColumn,
            this.ItemName});
			this.dataGridView1.Location = new System.Drawing.Point(6, 19);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new System.Drawing.Size(275, 184);
			this.dataGridView1.StandardTab = true;
			this.dataGridView1.TabIndex = 0;
			this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellDoubleClick);
			this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView1_CellMouseClick);
			this.dataGridView1.SelectionChanged += new System.EventHandler(this.DataGridView1_SelectionChanged);
			this.dataGridView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.DataGridView1_DragDrop);
			this.dataGridView1.DragOver += new System.Windows.Forms.DragEventHandler(this.DataGridView1_DragOver);
			this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGridView1_KeyDown);
			this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataGridView1_MouseDown);
			this.dataGridView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DataGridView1_MouseMove);
			// 
			// ImageColumn
			// 
			this.ImageColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle1.NullValue")));
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.ImageColumn.DefaultCellStyle = dataGridViewCellStyle1;
			this.ImageColumn.HeaderText = "";
			this.ImageColumn.Image = global::DatabaseDescriptionTool.Properties.Resources.book;
			this.ImageColumn.MinimumWidth = 26;
			this.ImageColumn.Name = "ImageColumn";
			this.ImageColumn.ReadOnly = true;
			this.ImageColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.ImageColumn.Width = 26;
			// 
			// ItemName
			// 
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.ItemName.DefaultCellStyle = dataGridViewCellStyle2;
			this.ItemName.HeaderText = "Name";
			this.ItemName.MinimumWidth = 235;
			this.ItemName.Name = "ItemName";
			this.ItemName.ReadOnly = true;
			this.ItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.ItemName.Width = 235;
			// 
			// editButton
			// 
			this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.editButton.Enabled = false;
			this.editButton.Location = new System.Drawing.Point(287, 48);
			this.editButton.Name = "editButton";
			this.editButton.Size = new System.Drawing.Size(75, 24);
			this.editButton.TabIndex = 2;
			this.editButton.Text = "Edit";
			this.editButton.UseVisualStyleBackColor = true;
			this.editButton.Click += new System.EventHandler(this.EditButton_Click);
			// 
			// moveDownButton
			// 
			this.moveDownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.moveDownButton.Enabled = false;
			this.moveDownButton.Location = new System.Drawing.Point(287, 180);
			this.moveDownButton.Name = "moveDownButton";
			this.moveDownButton.Size = new System.Drawing.Size(75, 24);
			this.moveDownButton.TabIndex = 5;
			this.moveDownButton.Text = "Move Down";
			this.moveDownButton.UseVisualStyleBackColor = true;
			this.moveDownButton.Click += new System.EventHandler(this.MoveDownButton_Click);
			// 
			// moveUpButton
			// 
			this.moveUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.moveUpButton.Enabled = false;
			this.moveUpButton.Location = new System.Drawing.Point(287, 151);
			this.moveUpButton.Name = "moveUpButton";
			this.moveUpButton.Size = new System.Drawing.Size(75, 24);
			this.moveUpButton.TabIndex = 4;
			this.moveUpButton.Text = "Move Up";
			this.moveUpButton.UseVisualStyleBackColor = true;
			this.moveUpButton.Click += new System.EventHandler(this.MoveUpButton_Click);
			// 
			// deleteButton
			// 
			this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.deleteButton.Enabled = false;
			this.deleteButton.Location = new System.Drawing.Point(287, 77);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(75, 24);
			this.deleteButton.TabIndex = 3;
			this.deleteButton.Text = "Delete";
			this.deleteButton.UseVisualStyleBackColor = true;
			this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			// 
			// createButton
			// 
			this.createButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.createButton.Location = new System.Drawing.Point(287, 19);
			this.createButton.Name = "createButton";
			this.createButton.Size = new System.Drawing.Size(75, 24);
			this.createButton.TabIndex = 1;
			this.createButton.Text = "Create";
			this.createButton.UseVisualStyleBackColor = true;
			this.createButton.Click += new System.EventHandler(this.CreateButton_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.actionToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.menuStrip1.Size = new System.Drawing.Size(392, 24);
			this.menuStrip1.TabIndex = 7;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.toolStripSeparator1,
            this.closeToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// importToolStripMenuItem
			// 
			this.importToolStripMenuItem.Image = global::DatabaseDescriptionTool.Properties.Resources.folder;
			this.importToolStripMenuItem.Name = "importToolStripMenuItem";
			this.importToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.importToolStripMenuItem.Text = "&Open...";
			this.importToolStripMenuItem.Click += new System.EventHandler(this.ImportToolStripMenuItem_Click);
			// 
			// exportToolStripMenuItem
			// 
			this.exportToolStripMenuItem.Image = global::DatabaseDescriptionTool.Properties.Resources.disk;
			this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
			this.exportToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.exportToolStripMenuItem.Text = "&Save...";
			this.exportToolStripMenuItem.Click += new System.EventHandler(this.ExportToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(109, 6);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.closeToolStripMenuItem.Text = "&Close";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
			// 
			// actionToolStripMenuItem
			// 
			this.actionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteAllMenuItem1,
            this.toolStripSeparator2,
            this.createMenuItem1,
            this.editMenuItem1,
            this.deleteMenuItem1,
            this.toolStripSeparator3,
            this.cutMenuItem1,
            this.copyMenuItem1,
            this.pasteMenuItem1,
            this.toolStripSeparator4,
            this.selectAllMenuItem1,
            this.toolStripSeparator5,
            this.moveUpMenuItem1,
            this.moveDownMenuItem1});
			this.actionToolStripMenuItem.Name = "actionToolStripMenuItem";
			this.actionToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
			this.actionToolStripMenuItem.Text = "&Action";
			this.actionToolStripMenuItem.DropDownOpening += new System.EventHandler(this.ActionToolStripMenuItem_DropDownOpening);
			// 
			// deleteAllMenuItem1
			// 
			this.deleteAllMenuItem1.Name = "deleteAllMenuItem1";
			this.deleteAllMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.deleteAllMenuItem1.Text = "&Delete all";
			this.deleteAllMenuItem1.Click += new System.EventHandler(this.DeleteAllToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
			// 
			// createMenuItem1
			// 
			this.createMenuItem1.Name = "createMenuItem1";
			this.createMenuItem1.ShortcutKeyDisplayString = "Ctrl+N";
			this.createMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.createMenuItem1.Text = "Create";
			this.createMenuItem1.Click += new System.EventHandler(this.CreateToolStripMenuItem_Click);
			// 
			// editMenuItem1
			// 
			this.editMenuItem1.Name = "editMenuItem1";
			this.editMenuItem1.ShortcutKeyDisplayString = "Enter";
			this.editMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.editMenuItem1.Text = "Edit";
			this.editMenuItem1.Click += new System.EventHandler(this.EditToolStripMenuItem_Click);
			// 
			// deleteMenuItem1
			// 
			this.deleteMenuItem1.Name = "deleteMenuItem1";
			this.deleteMenuItem1.ShortcutKeyDisplayString = "Del";
			this.deleteMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.deleteMenuItem1.Text = "Delete";
			this.deleteMenuItem1.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
			// 
			// cutMenuItem1
			// 
			this.cutMenuItem1.Name = "cutMenuItem1";
			this.cutMenuItem1.ShortcutKeyDisplayString = "Ctrl+X";
			this.cutMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.cutMenuItem1.Text = "Cut";
			this.cutMenuItem1.Click += new System.EventHandler(this.CutToolStripMenuItem_Click);
			// 
			// copyMenuItem1
			// 
			this.copyMenuItem1.Name = "copyMenuItem1";
			this.copyMenuItem1.ShortcutKeyDisplayString = "Ctrl+C";
			this.copyMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.copyMenuItem1.Text = "Copy";
			this.copyMenuItem1.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
			// 
			// pasteMenuItem1
			// 
			this.pasteMenuItem1.Name = "pasteMenuItem1";
			this.pasteMenuItem1.ShortcutKeyDisplayString = "Ctrl+V";
			this.pasteMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.pasteMenuItem1.Text = "Paste";
			this.pasteMenuItem1.Click += new System.EventHandler(this.PasteToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(177, 6);
			// 
			// selectAllMenuItem1
			// 
			this.selectAllMenuItem1.Name = "selectAllMenuItem1";
			this.selectAllMenuItem1.ShortcutKeyDisplayString = "Ctrl+A";
			this.selectAllMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.selectAllMenuItem1.Text = "Select All";
			this.selectAllMenuItem1.Click += new System.EventHandler(this.SelectAllToolStripMenuItem_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(177, 6);
			// 
			// moveUpMenuItem1
			// 
			this.moveUpMenuItem1.Name = "moveUpMenuItem1";
			this.moveUpMenuItem1.ShortcutKeyDisplayString = "Ctrl+U";
			this.moveUpMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.moveUpMenuItem1.Text = "Move Up";
			this.moveUpMenuItem1.Click += new System.EventHandler(this.MoveUpToolStripMenuItem_Click);
			// 
			// moveDownMenuItem1
			// 
			this.moveDownMenuItem1.Name = "moveDownMenuItem1";
			this.moveDownMenuItem1.ShortcutKeyDisplayString = "Ctrl+D";
			this.moveDownMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.moveDownMenuItem1.Text = "Move Down";
			this.moveDownMenuItem1.Click += new System.EventHandler(this.MoveDownToolStripMenuItem_Click);
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.DefaultExt = "xml";
			this.saveFileDialog1.Filter = "Xml files|*.xml|All files|*.*";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.DefaultExt = "xml";
			this.openFileDialog1.Filter = "Xml files|*.xml|All files|*.*";
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteAllMenuItem,
            this.toolStripSeparator7,
            this.createMenuItem,
            this.editMenuItem,
            this.deleteMenuItem,
            this.toolStripSeparator8,
            this.cutMenuItem,
            this.copyMenuItem,
            this.pasteMenuItem,
            this.toolStripSeparator10,
            this.selectAllMenuItem,
            this.toolStripSeparator6,
            this.moveUpMenuItem,
            this.moveDownMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(181, 248);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip1_Opening);
			// 
			// deleteAllMenuItem
			// 
			this.deleteAllMenuItem.Name = "deleteAllMenuItem";
			this.deleteAllMenuItem.Size = new System.Drawing.Size(180, 22);
			this.deleteAllMenuItem.Text = "&Delete all";
			this.deleteAllMenuItem.Click += new System.EventHandler(this.DeleteAllMenuItem_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(177, 6);
			// 
			// createMenuItem
			// 
			this.createMenuItem.Name = "createMenuItem";
			this.createMenuItem.ShortcutKeyDisplayString = "Ctrl+N";
			this.createMenuItem.Size = new System.Drawing.Size(180, 22);
			this.createMenuItem.Text = "Create";
			this.createMenuItem.Click += new System.EventHandler(this.CreateMenuItem_Click);
			// 
			// editMenuItem
			// 
			this.editMenuItem.Name = "editMenuItem";
			this.editMenuItem.ShortcutKeyDisplayString = "Enter";
			this.editMenuItem.Size = new System.Drawing.Size(180, 22);
			this.editMenuItem.Text = "Edit";
			this.editMenuItem.Click += new System.EventHandler(this.EditMenuItem_Click);
			// 
			// deleteMenuItem
			// 
			this.deleteMenuItem.Name = "deleteMenuItem";
			this.deleteMenuItem.ShortcutKeyDisplayString = "Del";
			this.deleteMenuItem.Size = new System.Drawing.Size(180, 22);
			this.deleteMenuItem.Text = "Delete";
			this.deleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(177, 6);
			// 
			// cutMenuItem
			// 
			this.cutMenuItem.Name = "cutMenuItem";
			this.cutMenuItem.ShortcutKeyDisplayString = "Ctrl+X";
			this.cutMenuItem.Size = new System.Drawing.Size(180, 22);
			this.cutMenuItem.Text = "Cut";
			this.cutMenuItem.Click += new System.EventHandler(this.CutToolStripMenuItem1_Click);
			// 
			// copyMenuItem
			// 
			this.copyMenuItem.Name = "copyMenuItem";
			this.copyMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
			this.copyMenuItem.Size = new System.Drawing.Size(180, 22);
			this.copyMenuItem.Text = "Copy";
			this.copyMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem1_Click);
			// 
			// pasteMenuItem
			// 
			this.pasteMenuItem.Name = "pasteMenuItem";
			this.pasteMenuItem.ShortcutKeyDisplayString = "Ctrl+V";
			this.pasteMenuItem.Size = new System.Drawing.Size(180, 22);
			this.pasteMenuItem.Text = "Paste";
			this.pasteMenuItem.Click += new System.EventHandler(this.PasteToolStripMenuItem1_Click);
			// 
			// toolStripSeparator10
			// 
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(177, 6);
			// 
			// selectAllMenuItem
			// 
			this.selectAllMenuItem.Name = "selectAllMenuItem";
			this.selectAllMenuItem.ShortcutKeyDisplayString = "Ctrl+A";
			this.selectAllMenuItem.Size = new System.Drawing.Size(180, 22);
			this.selectAllMenuItem.Text = "Select All";
			this.selectAllMenuItem.Click += new System.EventHandler(this.SelectAllToolStripMenuItem1_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(177, 6);
			// 
			// moveUpMenuItem
			// 
			this.moveUpMenuItem.Name = "moveUpMenuItem";
			this.moveUpMenuItem.ShortcutKeyDisplayString = "Ctrl+U";
			this.moveUpMenuItem.Size = new System.Drawing.Size(180, 22);
			this.moveUpMenuItem.Text = "Move Up";
			this.moveUpMenuItem.Click += new System.EventHandler(this.MoveUpMenuItem_Click);
			// 
			// moveDownMenuItem
			// 
			this.moveDownMenuItem.Name = "moveDownMenuItem";
			this.moveDownMenuItem.ShortcutKeyDisplayString = "Ctrl+D";
			this.moveDownMenuItem.Size = new System.Drawing.Size(180, 22);
			this.moveDownMenuItem.Text = "Move Down";
			this.moveDownMenuItem.Click += new System.EventHandler(this.MoveDownMenuItem_Click);
			// 
			// ManageDescriptionFieldsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.okButton;
			this.ClientSize = new System.Drawing.Size(392, 277);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(408, 316);
			this.Name = "ManageDescriptionFieldsForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Manage Description Fields";
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Button okButton;
	private System.Windows.Forms.GroupBox groupBox1;
	private System.Windows.Forms.Button moveDownButton;
	private System.Windows.Forms.Button moveUpButton;
	private System.Windows.Forms.Button deleteButton;
	private System.Windows.Forms.Button createButton;
	private System.Windows.Forms.Button editButton;
	private System.Windows.Forms.MenuStrip menuStrip1;
	private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
	private System.Windows.Forms.SaveFileDialog saveFileDialog1;
	private System.Windows.Forms.OpenFileDialog openFileDialog1;
	private System.Windows.Forms.DataGridView dataGridView1;
	private System.Windows.Forms.ToolStripMenuItem actionToolStripMenuItem;
	private System.Windows.Forms.ToolStripMenuItem deleteAllMenuItem1;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	private System.Windows.Forms.ToolStripMenuItem createMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem editMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem deleteMenuItem1;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
	private System.Windows.Forms.ToolStripMenuItem moveUpMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem moveDownMenuItem1;
	private System.Windows.Forms.DataGridViewImageColumn ImageColumn;
	private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
	private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
	private System.Windows.Forms.ToolStripMenuItem deleteAllMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
	private System.Windows.Forms.ToolStripMenuItem createMenuItem;
	private System.Windows.Forms.ToolStripMenuItem editMenuItem;
	private System.Windows.Forms.ToolStripMenuItem deleteMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
	private System.Windows.Forms.ToolStripMenuItem moveUpMenuItem;
	private System.Windows.Forms.ToolStripMenuItem moveDownMenuItem;
	private System.Windows.Forms.ToolStripMenuItem copyMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem pasteMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem copyMenuItem;
	private System.Windows.Forms.ToolStripMenuItem pasteMenuItem;
	private System.Windows.Forms.ToolStripMenuItem cutMenuItem1;
	private System.Windows.Forms.ToolStripMenuItem cutMenuItem;
	private System.Windows.Forms.ToolStripMenuItem selectAllMenuItem1;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
	private System.Windows.Forms.ToolStripMenuItem selectAllMenuItem;
	private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
}
