﻿/*
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

partial class DeleteDescriptionsInSubNodes
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeleteDescriptionsInSubNodes));
			this.cancelButton = new System.Windows.Forms.Button();
			this.startButton = new System.Windows.Forms.Button();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chooseDatabaseLabel = new System.Windows.Forms.Label();
			this.databaseComboBox = new ComboBoxCustom();
			this.timeElapsedLabel = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.statusLabel = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.descriptionsLabel = new System.Windows.Forms.Label();
			this.selectObjectsButton = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(307, 177);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 24);
			this.cancelButton.TabIndex = 5;
			this.cancelButton.Text = "Close";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// startButton
			// 
			this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.startButton.Enabled = false;
			this.startButton.Location = new System.Drawing.Point(226, 177);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(75, 24);
			this.startButton.TabIndex = 4;
			this.startButton.Text = "Start";
			this.startButton.UseVisualStyleBackColor = true;
			this.startButton.Click += new System.EventHandler(this.StartButton_Click);
			// 
			// progressBar1
			// 
			this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar1.Location = new System.Drawing.Point(12, 144);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(370, 23);
			this.progressBar1.TabIndex = 2;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.chooseDatabaseLabel);
			this.groupBox1.Controls.Add(this.databaseComboBox);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(370, 51);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Database";
			// 
			// chooseDatabaseLabel
			// 
			this.chooseDatabaseLabel.AutoSize = true;
			this.chooseDatabaseLabel.Location = new System.Drawing.Point(6, 22);
			this.chooseDatabaseLabel.Name = "chooseDatabaseLabel";
			this.chooseDatabaseLabel.Size = new System.Drawing.Size(95, 13);
			this.chooseDatabaseLabel.TabIndex = 1;
			this.chooseDatabaseLabel.Text = "Choose Database:";
			// 
			// databaseComboBox
			// 
			this.databaseComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.databaseComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.databaseComboBox.FormattingEnabled = true;
			this.databaseComboBox.Location = new System.Drawing.Point(107, 19);
			this.databaseComboBox.Name = "databaseComboBox";
			this.databaseComboBox.Size = new System.Drawing.Size(257, 21);
			this.databaseComboBox.TabIndex = 0;
			this.databaseComboBox.SelectedIndexChanged += new System.EventHandler(this.DatabaseComboBox_SelectedIndexChanged);
			// 
			// timeElapsedLabel
			// 
			this.timeElapsedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.timeElapsedLabel.AutoSize = true;
			this.timeElapsedLabel.Location = new System.Drawing.Point(333, 128);
			this.timeElapsedLabel.Name = "timeElapsedLabel";
			this.timeElapsedLabel.Size = new System.Drawing.Size(49, 13);
			this.timeElapsedLabel.TabIndex = 4;
			this.timeElapsedLabel.Text = "00:00:00";
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
			// 
			// statusLabel
			// 
			this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.statusLabel.AutoSize = true;
			this.statusLabel.Location = new System.Drawing.Point(12, 182);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(60, 13);
			this.statusLabel.TabIndex = 6;
			this.statusLabel.Text = "Status: Idle";
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.descriptionsLabel);
			this.groupBox3.Controls.Add(this.selectObjectsButton);
			this.groupBox3.Location = new System.Drawing.Point(12, 69);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(370, 51);
			this.groupBox3.TabIndex = 1;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Delete";
			// 
			// descriptionsLabel
			// 
			this.descriptionsLabel.AutoSize = true;
			this.descriptionsLabel.Location = new System.Drawing.Point(6, 23);
			this.descriptionsLabel.Name = "descriptionsLabel";
			this.descriptionsLabel.Size = new System.Drawing.Size(251, 13);
			this.descriptionsLabel.TabIndex = 3;
			this.descriptionsLabel.Text = "Descriptions will be deleted for the selected objects.";
			// 
			// selectObjectsButton
			// 
			this.selectObjectsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.selectObjectsButton.Enabled = false;
			this.selectObjectsButton.Location = new System.Drawing.Point(289, 17);
			this.selectObjectsButton.Name = "selectObjectsButton";
			this.selectObjectsButton.Size = new System.Drawing.Size(75, 24);
			this.selectObjectsButton.TabIndex = 2;
			this.selectObjectsButton.Text = "Objects...";
			this.selectObjectsButton.UseVisualStyleBackColor = true;
			this.selectObjectsButton.Click += new System.EventHandler(this.SelectObjectsButton_Click);
			// 
			// DeleteDescriptionsInSubNodes
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(394, 208);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.statusLabel);
			this.Controls.Add(this.timeElapsedLabel);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.startButton);
			this.Controls.Add(this.cancelButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DeleteDescriptionsInSubNodes";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Delete Descriptions";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DeleteForm_FormClosing);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Button cancelButton;
	private System.Windows.Forms.Button startButton;
	private System.Windows.Forms.ProgressBar progressBar1;
	private System.Windows.Forms.GroupBox groupBox1;
	private System.Windows.Forms.Label chooseDatabaseLabel;
	private ComboBoxCustom databaseComboBox;
	private System.Windows.Forms.Label timeElapsedLabel;
	private System.Windows.Forms.Timer timer1;
	private System.Windows.Forms.Label statusLabel;
	private System.Windows.Forms.GroupBox groupBox3;
	private System.Windows.Forms.Button selectObjectsButton;
	private System.Windows.Forms.Label descriptionsLabel;
}
