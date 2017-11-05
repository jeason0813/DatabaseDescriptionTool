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

partial class ExtendedMessageBoxForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtendedMessageBoxForm));
			this.yesButton = new System.Windows.Forms.Button();
			this.noButton = new System.Windows.Forms.Button();
			this.textLabel = new System.Windows.Forms.Label();
			this.textBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// yesButton
			// 
			this.yesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.yesButton.Location = new System.Drawing.Point(194, 136);
			this.yesButton.Name = "yesButton";
			this.yesButton.Size = new System.Drawing.Size(75, 24);
			this.yesButton.TabIndex = 2;
			this.yesButton.Text = "Yes";
			this.yesButton.UseVisualStyleBackColor = true;
			this.yesButton.Click += new System.EventHandler(this.YesButton_Click);
			// 
			// noButton
			// 
			this.noButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.noButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.noButton.Location = new System.Drawing.Point(275, 136);
			this.noButton.Name = "noButton";
			this.noButton.Size = new System.Drawing.Size(75, 24);
			this.noButton.TabIndex = 0;
			this.noButton.Text = "No";
			this.noButton.UseVisualStyleBackColor = true;
			this.noButton.Click += new System.EventHandler(this.NoButton_Click);
			// 
			// textLabel
			// 
			this.textLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textLabel.Location = new System.Drawing.Point(12, 9);
			this.textLabel.Name = "textLabel";
			this.textLabel.Size = new System.Drawing.Size(338, 13);
			this.textLabel.TabIndex = 2;
			this.textLabel.Text = "textLabel";
			// 
			// textBox
			// 
			this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox.Location = new System.Drawing.Point(12, 25);
			this.textBox.Multiline = true;
			this.textBox.Name = "textBox";
			this.textBox.ReadOnly = true;
			this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox.Size = new System.Drawing.Size(338, 101);
			this.textBox.TabIndex = 1;
			this.textBox.WordWrap = false;
			// 
			// ExtendedMessageBoxForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.noButton;
			this.ClientSize = new System.Drawing.Size(362, 168);
			this.Controls.Add(this.textBox);
			this.Controls.Add(this.textLabel);
			this.Controls.Add(this.noButton);
			this.Controls.Add(this.yesButton);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(378, 207);
			this.Name = "ExtendedMessageBoxForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Title";
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Button yesButton;
	private System.Windows.Forms.Button noButton;
	private System.Windows.Forms.Label textLabel;
	private System.Windows.Forms.TextBox textBox;
}
