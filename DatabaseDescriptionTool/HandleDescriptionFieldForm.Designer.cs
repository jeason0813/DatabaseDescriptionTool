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

partial class HandleDescriptionFieldForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HandleDescriptionFieldForm));
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.nameLabel = new System.Windows.Forms.Label();
			this.databaseFieldNameLabel = new System.Windows.Forms.Label();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.useForImageButton = new System.Windows.Forms.Button();
			this.useForImageLabel = new System.Windows.Forms.Label();
			this.infoTextBox = new System.Windows.Forms.TextBox();
			this.infoLabel = new System.Windows.Forms.Label();
			this.databaseFieldNameComboBox = new ComboBoxCustom();
			this.SuspendLayout();
			// 
			// nameTextBox
			// 
			this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.nameTextBox.Location = new System.Drawing.Point(130, 6);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(250, 20);
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
			// databaseFieldNameLabel
			// 
			this.databaseFieldNameLabel.AutoSize = true;
			this.databaseFieldNameLabel.Location = new System.Drawing.Point(12, 35);
			this.databaseFieldNameLabel.Name = "databaseFieldNameLabel";
			this.databaseFieldNameLabel.Size = new System.Drawing.Size(112, 13);
			this.databaseFieldNameLabel.TabIndex = 2;
			this.databaseFieldNameLabel.Text = "Database Field Name:";
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
			this.okButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// useForImageButton
			// 
			this.useForImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.useForImageButton.Location = new System.Drawing.Point(130, 212);
			this.useForImageButton.Name = "useForImageButton";
			this.useForImageButton.Size = new System.Drawing.Size(75, 24);
			this.useForImageButton.TabIndex = 3;
			this.useForImageButton.Text = "Set";
			this.useForImageButton.UseVisualStyleBackColor = true;
			this.useForImageButton.Click += new System.EventHandler(this.UseForImageButton_Click);
			// 
			// useForImageLabel
			// 
			this.useForImageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.useForImageLabel.AutoSize = true;
			this.useForImageLabel.Location = new System.Drawing.Point(12, 217);
			this.useForImageLabel.Name = "useForImageLabel";
			this.useForImageLabel.Size = new System.Drawing.Size(85, 13);
			this.useForImageLabel.TabIndex = 5;
			this.useForImageLabel.Text = "Mark as Primary:";
			// 
			// infoTextBox
			// 
			this.infoTextBox.AcceptsReturn = true;
			this.infoTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.infoTextBox.Location = new System.Drawing.Point(130, 58);
			this.infoTextBox.Multiline = true;
			this.infoTextBox.Name = "infoTextBox";
			this.infoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.infoTextBox.Size = new System.Drawing.Size(250, 148);
			this.infoTextBox.TabIndex = 2;
			this.infoTextBox.TextChanged += new System.EventHandler(this.InfoTextBox_TextChanged);
			this.infoTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InfoTextBox_KeyDown);
			// 
			// infoLabel
			// 
			this.infoLabel.AutoSize = true;
			this.infoLabel.Location = new System.Drawing.Point(12, 61);
			this.infoLabel.Name = "infoLabel";
			this.infoLabel.Size = new System.Drawing.Size(62, 13);
			this.infoLabel.TabIndex = 7;
			this.infoLabel.Text = "Information:";
			// 
			// databaseFieldNameComboBox
			// 
			this.databaseFieldNameComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.databaseFieldNameComboBox.FormattingEnabled = true;
			this.databaseFieldNameComboBox.Location = new System.Drawing.Point(130, 32);
			this.databaseFieldNameComboBox.Name = "databaseFieldNameComboBox";
			this.databaseFieldNameComboBox.Size = new System.Drawing.Size(250, 21);
			this.databaseFieldNameComboBox.TabIndex = 1;
			this.databaseFieldNameComboBox.TextChanged += new System.EventHandler(this.DatabaseFieldNameComboBox_TextChanged);
			// 
			// HandleDescriptionFieldForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(392, 277);
			this.Controls.Add(this.databaseFieldNameComboBox);
			this.Controls.Add(this.infoLabel);
			this.Controls.Add(this.infoTextBox);
			this.Controls.Add(this.useForImageLabel);
			this.Controls.Add(this.useForImageButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.databaseFieldNameLabel);
			this.Controls.Add(this.nameLabel);
			this.Controls.Add(this.nameTextBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(408, 316);
			this.Name = "HandleDescriptionFieldForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Description Field";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HandleDescriptionFieldForm_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.TextBox nameTextBox;
	private System.Windows.Forms.Label nameLabel;
	private System.Windows.Forms.Label databaseFieldNameLabel;
	private System.Windows.Forms.Button cancelButton;
	private System.Windows.Forms.Button okButton;
	private System.Windows.Forms.Button useForImageButton;
	private System.Windows.Forms.Label useForImageLabel;
	private System.Windows.Forms.TextBox infoTextBox;
	private System.Windows.Forms.Label infoLabel;
	private ComboBoxCustom databaseFieldNameComboBox;
}
