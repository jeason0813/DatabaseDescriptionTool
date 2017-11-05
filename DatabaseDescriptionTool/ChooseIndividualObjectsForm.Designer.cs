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

partial class ChooseIndividualObjectsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseIndividualObjectsForm));
			this.objectsGroupBox = new System.Windows.Forms.GroupBox();
			this.deselectAllButton = new System.Windows.Forms.Button();
			this.selectAllButton = new System.Windows.Forms.Button();
			this.objectsListView = new System.Windows.Forms.ListView();
			this.columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.objectsGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// objectsGroupBox
			// 
			this.objectsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.objectsGroupBox.Controls.Add(this.deselectAllButton);
			this.objectsGroupBox.Controls.Add(this.selectAllButton);
			this.objectsGroupBox.Controls.Add(this.objectsListView);
			this.objectsGroupBox.Location = new System.Drawing.Point(12, 12);
			this.objectsGroupBox.Name = "objectsGroupBox";
			this.objectsGroupBox.Size = new System.Drawing.Size(497, 406);
			this.objectsGroupBox.TabIndex = 0;
			this.objectsGroupBox.TabStop = false;
			this.objectsGroupBox.Text = "Objects";
			// 
			// deselectAllButton
			// 
			this.deselectAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.deselectAllButton.Location = new System.Drawing.Point(87, 377);
			this.deselectAllButton.Name = "deselectAllButton";
			this.deselectAllButton.Size = new System.Drawing.Size(75, 24);
			this.deselectAllButton.TabIndex = 2;
			this.deselectAllButton.Text = "Deselect All";
			this.deselectAllButton.UseVisualStyleBackColor = true;
			this.deselectAllButton.Click += new System.EventHandler(this.DeselectAllButton_Click);
			// 
			// selectAllButton
			// 
			this.selectAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.selectAllButton.Location = new System.Drawing.Point(6, 377);
			this.selectAllButton.Name = "selectAllButton";
			this.selectAllButton.Size = new System.Drawing.Size(75, 24);
			this.selectAllButton.TabIndex = 1;
			this.selectAllButton.Text = "Select All";
			this.selectAllButton.UseVisualStyleBackColor = true;
			this.selectAllButton.Click += new System.EventHandler(this.SelectAllButton_Click);
			// 
			// objectsListView
			// 
			this.objectsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.objectsListView.CheckBoxes = true;
			this.objectsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader});
			this.objectsListView.HideSelection = false;
			this.objectsListView.Location = new System.Drawing.Point(6, 19);
			this.objectsListView.Name = "objectsListView";
			this.objectsListView.Size = new System.Drawing.Size(485, 352);
			this.objectsListView.TabIndex = 0;
			this.objectsListView.UseCompatibleStateImageBehavior = false;
			this.objectsListView.View = System.Windows.Forms.View.Details;
			this.objectsListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ObjectsListView_ColumnClick);
			this.objectsListView.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.ObjectsListView_ColumnWidthChanged);
			this.objectsListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ObjectsListView_KeyDown);
			// 
			// columnHeader
			// 
			this.columnHeader.Text = "Name";
			this.columnHeader.Width = 150;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(353, 428);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 24);
			this.okButton.TabIndex = 3;
			this.okButton.Text = "Ok";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(434, 428);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 24);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// ChooseIndividualObjectsForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(521, 459);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.objectsGroupBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(408, 355);
			this.Name = "ChooseIndividualObjectsForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Choose individual objects";
			this.Resize += new System.EventHandler(this.ChooseIndividualObjectsForm_Resize);
			this.objectsGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.GroupBox objectsGroupBox;
	public System.Windows.Forms.ListView objectsListView;
	private System.Windows.Forms.ColumnHeader columnHeader;
	private System.Windows.Forms.Button okButton;
	private System.Windows.Forms.Button cancelButton;
	private System.Windows.Forms.Button deselectAllButton;
	private System.Windows.Forms.Button selectAllButton;
}
