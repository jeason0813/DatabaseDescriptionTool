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

partial class SearchTextForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchTextForm));
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.matchCaseCheckBox = new System.Windows.Forms.CheckBox();
			this.matchWholeWordCheckBox = new System.Windows.Forms.CheckBox();
			this.wrapAroundCheckBox = new System.Windows.Forms.CheckBox();
			this.showNoMoreMatchesMessageCheckBox = new System.Windows.Forms.CheckBox();
			this.directionGroupBox = new System.Windows.Forms.GroupBox();
			this.downRadioButton = new System.Windows.Forms.RadioButton();
			this.upRadioButton = new System.Windows.Forms.RadioButton();
			this.useRegExCheckBox = new System.Windows.Forms.CheckBox();
			this.searchTermComboBox = new System.Windows.Forms.ComboBox();
			this.optionsGroupBox = new System.Windows.Forms.GroupBox();
			this.searchGroupBox = new System.Windows.Forms.GroupBox();
			this.directionGroupBox.SuspendLayout();
			this.optionsGroupBox.SuspendLayout();
			this.searchGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.BackColor = System.Drawing.Color.Transparent;
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(297, 214);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 24);
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = false;
			this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.BackColor = System.Drawing.Color.Transparent;
			this.okButton.Enabled = false;
			this.okButton.Location = new System.Drawing.Point(216, 214);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 24);
			this.okButton.TabIndex = 2;
			this.okButton.Text = "Find Next";
			this.okButton.UseVisualStyleBackColor = false;
			this.okButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.SystemColors.Control;
			this.label4.Location = new System.Drawing.Point(6, 23);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(59, 13);
			this.label4.TabIndex = 32;
			this.label4.Text = "Search for:";
			// 
			// matchCaseCheckBox
			// 
			this.matchCaseCheckBox.AutoSize = true;
			this.matchCaseCheckBox.BackColor = System.Drawing.SystemColors.Control;
			this.matchCaseCheckBox.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.matchCaseCheckBox.Location = new System.Drawing.Point(6, 42);
			this.matchCaseCheckBox.Name = "matchCaseCheckBox";
			this.matchCaseCheckBox.Size = new System.Drawing.Size(82, 17);
			this.matchCaseCheckBox.TabIndex = 2;
			this.matchCaseCheckBox.Text = "Match case";
			this.matchCaseCheckBox.UseVisualStyleBackColor = false;
			this.matchCaseCheckBox.CheckedChanged += new System.EventHandler(this.MatchCaseCheckBox_CheckedChanged);
			// 
			// matchWholeWordCheckBox
			// 
			this.matchWholeWordCheckBox.AutoSize = true;
			this.matchWholeWordCheckBox.BackColor = System.Drawing.SystemColors.Control;
			this.matchWholeWordCheckBox.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.matchWholeWordCheckBox.Location = new System.Drawing.Point(6, 19);
			this.matchWholeWordCheckBox.Name = "matchWholeWordCheckBox";
			this.matchWholeWordCheckBox.Size = new System.Drawing.Size(113, 17);
			this.matchWholeWordCheckBox.TabIndex = 1;
			this.matchWholeWordCheckBox.Text = "Match whole word";
			this.matchWholeWordCheckBox.UseVisualStyleBackColor = false;
			this.matchWholeWordCheckBox.CheckedChanged += new System.EventHandler(this.MatchWholeWordCheckBox_CheckedChanged);
			// 
			// wrapAroundCheckBox
			// 
			this.wrapAroundCheckBox.AutoSize = true;
			this.wrapAroundCheckBox.BackColor = System.Drawing.SystemColors.Control;
			this.wrapAroundCheckBox.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.wrapAroundCheckBox.Location = new System.Drawing.Point(6, 65);
			this.wrapAroundCheckBox.Name = "wrapAroundCheckBox";
			this.wrapAroundCheckBox.Size = new System.Drawing.Size(88, 17);
			this.wrapAroundCheckBox.TabIndex = 3;
			this.wrapAroundCheckBox.Text = "Wrap around";
			this.wrapAroundCheckBox.UseVisualStyleBackColor = false;
			this.wrapAroundCheckBox.CheckedChanged += new System.EventHandler(this.WrapAroundCheckBox_CheckedChanged);
			// 
			// showNoMoreMatchesMessageCheckBox
			// 
			this.showNoMoreMatchesMessageCheckBox.AutoSize = true;
			this.showNoMoreMatchesMessageCheckBox.BackColor = System.Drawing.SystemColors.Control;
			this.showNoMoreMatchesMessageCheckBox.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.showNoMoreMatchesMessageCheckBox.Location = new System.Drawing.Point(6, 88);
			this.showNoMoreMatchesMessageCheckBox.Name = "showNoMoreMatchesMessageCheckBox";
			this.showNoMoreMatchesMessageCheckBox.Size = new System.Drawing.Size(152, 17);
			this.showNoMoreMatchesMessageCheckBox.TabIndex = 4;
			this.showNoMoreMatchesMessageCheckBox.Text = "Show \"No more matches.\"";
			this.showNoMoreMatchesMessageCheckBox.UseVisualStyleBackColor = false;
			this.showNoMoreMatchesMessageCheckBox.CheckedChanged += new System.EventHandler(this.ShowNoMoreMatchesMessageCheckBox_CheckedChanged);
			// 
			// directionGroupBox
			// 
			this.directionGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.directionGroupBox.BackColor = System.Drawing.SystemColors.Control;
			this.directionGroupBox.Controls.Add(this.downRadioButton);
			this.directionGroupBox.Controls.Add(this.upRadioButton);
			this.directionGroupBox.Location = new System.Drawing.Point(242, 19);
			this.directionGroupBox.Name = "directionGroupBox";
			this.directionGroupBox.Size = new System.Drawing.Size(112, 63);
			this.directionGroupBox.TabIndex = 6;
			this.directionGroupBox.TabStop = false;
			this.directionGroupBox.Text = "Direction";
			// 
			// downRadioButton
			// 
			this.downRadioButton.AutoSize = true;
			this.downRadioButton.Location = new System.Drawing.Point(6, 42);
			this.downRadioButton.Name = "downRadioButton";
			this.downRadioButton.Size = new System.Drawing.Size(53, 17);
			this.downRadioButton.TabIndex = 1;
			this.downRadioButton.Text = "Down";
			this.downRadioButton.UseVisualStyleBackColor = true;
			this.downRadioButton.CheckedChanged += new System.EventHandler(this.DownRadioButton_CheckedChanged);
			// 
			// upRadioButton
			// 
			this.upRadioButton.AutoSize = true;
			this.upRadioButton.Location = new System.Drawing.Point(6, 19);
			this.upRadioButton.Name = "upRadioButton";
			this.upRadioButton.Size = new System.Drawing.Size(39, 17);
			this.upRadioButton.TabIndex = 0;
			this.upRadioButton.Text = "Up";
			this.upRadioButton.UseVisualStyleBackColor = true;
			this.upRadioButton.CheckedChanged += new System.EventHandler(this.UpRadioButton_CheckedChanged);
			// 
			// useRegExCheckBox
			// 
			this.useRegExCheckBox.AutoSize = true;
			this.useRegExCheckBox.BackColor = System.Drawing.SystemColors.Control;
			this.useRegExCheckBox.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.useRegExCheckBox.Location = new System.Drawing.Point(6, 111);
			this.useRegExCheckBox.Name = "useRegExCheckBox";
			this.useRegExCheckBox.Size = new System.Drawing.Size(144, 17);
			this.useRegExCheckBox.TabIndex = 5;
			this.useRegExCheckBox.Text = "Use Regular Expressions";
			this.useRegExCheckBox.UseVisualStyleBackColor = false;
			this.useRegExCheckBox.CheckedChanged += new System.EventHandler(this.UseRegExCheckBox_CheckedChanged);
			// 
			// searchTermComboBox
			// 
			this.searchTermComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.searchTermComboBox.BackColor = System.Drawing.Color.White;
			this.searchTermComboBox.FormattingEnabled = true;
			this.searchTermComboBox.Location = new System.Drawing.Point(71, 20);
			this.searchTermComboBox.Name = "searchTermComboBox";
			this.searchTermComboBox.Size = new System.Drawing.Size(283, 21);
			this.searchTermComboBox.TabIndex = 0;
			this.searchTermComboBox.TextChanged += new System.EventHandler(this.ComboBox1_TextChanged);
			// 
			// optionsGroupBox
			// 
			this.optionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.optionsGroupBox.Controls.Add(this.matchWholeWordCheckBox);
			this.optionsGroupBox.Controls.Add(this.matchCaseCheckBox);
			this.optionsGroupBox.Controls.Add(this.useRegExCheckBox);
			this.optionsGroupBox.Controls.Add(this.wrapAroundCheckBox);
			this.optionsGroupBox.Controls.Add(this.directionGroupBox);
			this.optionsGroupBox.Controls.Add(this.showNoMoreMatchesMessageCheckBox);
			this.optionsGroupBox.Location = new System.Drawing.Point(12, 70);
			this.optionsGroupBox.Name = "optionsGroupBox";
			this.optionsGroupBox.Size = new System.Drawing.Size(360, 133);
			this.optionsGroupBox.TabIndex = 1;
			this.optionsGroupBox.TabStop = false;
			this.optionsGroupBox.Text = "Options";
			// 
			// searchGroupBox
			// 
			this.searchGroupBox.Controls.Add(this.label4);
			this.searchGroupBox.Controls.Add(this.searchTermComboBox);
			this.searchGroupBox.Location = new System.Drawing.Point(12, 12);
			this.searchGroupBox.Name = "searchGroupBox";
			this.searchGroupBox.Size = new System.Drawing.Size(360, 52);
			this.searchGroupBox.TabIndex = 0;
			this.searchGroupBox.TabStop = false;
			this.searchGroupBox.Text = "Search";
			// 
			// SearchTextForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(384, 245);
			this.Controls.Add(this.searchGroupBox);
			this.Controls.Add(this.optionsGroupBox);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SearchTextForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Title";
			this.Activated += new System.EventHandler(this.SearchForm_Activated);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SearchForm_FormClosing);
			this.Load += new System.EventHandler(this.SearchForm_Load);
			this.directionGroupBox.ResumeLayout(false);
			this.directionGroupBox.PerformLayout();
			this.optionsGroupBox.ResumeLayout(false);
			this.optionsGroupBox.PerformLayout();
			this.searchGroupBox.ResumeLayout(false);
			this.searchGroupBox.PerformLayout();
			this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.Button cancelButton;
	private System.Windows.Forms.Button okButton;
	private System.Windows.Forms.Label label4;
	private System.Windows.Forms.CheckBox matchCaseCheckBox;
	private System.Windows.Forms.CheckBox matchWholeWordCheckBox;
	private System.Windows.Forms.CheckBox wrapAroundCheckBox;
	private System.Windows.Forms.CheckBox showNoMoreMatchesMessageCheckBox;
	private System.Windows.Forms.GroupBox directionGroupBox;
	private System.Windows.Forms.RadioButton downRadioButton;
	private System.Windows.Forms.RadioButton upRadioButton;
	private System.Windows.Forms.CheckBox useRegExCheckBox;
	private System.Windows.Forms.ComboBox searchTermComboBox;
	private System.Windows.Forms.GroupBox optionsGroupBox;
	private System.Windows.Forms.GroupBox searchGroupBox;
}
