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

partial class ConnectionDialogForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionDialogForm));
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.serverNameLabel = new System.Windows.Forms.Label();
			this.authenticationLabel = new System.Windows.Forms.Label();
			this.authenticationComboBox = new ComboBoxCustom();
			this.usernameLabel = new System.Windows.Forms.Label();
			this.passwordLabel = new System.Windows.Forms.Label();
			this.userNameTextBox = new System.Windows.Forms.TextBox();
			this.passwordTextBox = new System.Windows.Forms.TextBox();
			this.inputGroupBox = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.saveValuesCheckBox = new System.Windows.Forms.CheckBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.infoLabel = new System.Windows.Forms.Label();
			this.serverNameComboBox = new System.Windows.Forms.ComboBox();
			this.inputGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(226, 176);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 24);
			this.okButton.TabIndex = 6;
			this.okButton.Text = "Ok";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(307, 176);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 24);
			this.cancelButton.TabIndex = 7;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
			// 
			// serverNameLabel
			// 
			this.serverNameLabel.AutoSize = true;
			this.serverNameLabel.Location = new System.Drawing.Point(6, 22);
			this.serverNameLabel.Name = "serverNameLabel";
			this.serverNameLabel.Size = new System.Drawing.Size(70, 13);
			this.serverNameLabel.TabIndex = 11;
			this.serverNameLabel.Text = "Server name:";
			// 
			// authenticationLabel
			// 
			this.authenticationLabel.AutoSize = true;
			this.authenticationLabel.Location = new System.Drawing.Point(6, 48);
			this.authenticationLabel.Name = "authenticationLabel";
			this.authenticationLabel.Size = new System.Drawing.Size(78, 13);
			this.authenticationLabel.TabIndex = 12;
			this.authenticationLabel.Text = "Authentication:";
			// 
			// authenticationComboBox
			// 
			this.authenticationComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.authenticationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.authenticationComboBox.FormattingEnabled = true;
			this.authenticationComboBox.Items.AddRange(new object[] {
            "Windows Authentication",
            "SQL Server Authentication"});
			this.authenticationComboBox.Location = new System.Drawing.Point(100, 45);
			this.authenticationComboBox.MaxDropDownItems = 2;
			this.authenticationComboBox.Name = "authenticationComboBox";
			this.authenticationComboBox.Size = new System.Drawing.Size(256, 21);
			this.authenticationComboBox.TabIndex = 2;
			this.authenticationComboBox.SelectedIndexChanged += new System.EventHandler(this.AuthenticationComboBox_SelectedIndexChanged);
			// 
			// usernameLabel
			// 
			this.usernameLabel.AutoSize = true;
			this.usernameLabel.Location = new System.Drawing.Point(23, 75);
			this.usernameLabel.Name = "usernameLabel";
			this.usernameLabel.Size = new System.Drawing.Size(61, 13);
			this.usernameLabel.TabIndex = 14;
			this.usernameLabel.Text = "User name:";
			// 
			// passwordLabel
			// 
			this.passwordLabel.AutoSize = true;
			this.passwordLabel.Location = new System.Drawing.Point(23, 101);
			this.passwordLabel.Name = "passwordLabel";
			this.passwordLabel.Size = new System.Drawing.Size(56, 13);
			this.passwordLabel.TabIndex = 15;
			this.passwordLabel.Text = "Password:";
			// 
			// userNameTextBox
			// 
			this.userNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.userNameTextBox.Location = new System.Drawing.Point(123, 72);
			this.userNameTextBox.Name = "userNameTextBox";
			this.userNameTextBox.Size = new System.Drawing.Size(233, 20);
			this.userNameTextBox.TabIndex = 3;
			// 
			// passwordTextBox
			// 
			this.passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.passwordTextBox.Location = new System.Drawing.Point(123, 98);
			this.passwordTextBox.Name = "passwordTextBox";
			this.passwordTextBox.PasswordChar = '*';
			this.passwordTextBox.Size = new System.Drawing.Size(233, 20);
			this.passwordTextBox.TabIndex = 4;
			// 
			// inputGroupBox
			// 
			this.inputGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.inputGroupBox.Controls.Add(this.serverNameComboBox);
			this.inputGroupBox.Controls.Add(this.serverNameLabel);
			this.inputGroupBox.Controls.Add(this.authenticationLabel);
			this.inputGroupBox.Controls.Add(this.authenticationComboBox);
			this.inputGroupBox.Controls.Add(this.passwordTextBox);
			this.inputGroupBox.Controls.Add(this.usernameLabel);
			this.inputGroupBox.Controls.Add(this.userNameTextBox);
			this.inputGroupBox.Controls.Add(this.passwordLabel);
			this.inputGroupBox.Location = new System.Drawing.Point(12, 39);
			this.inputGroupBox.Name = "inputGroupBox";
			this.inputGroupBox.Size = new System.Drawing.Size(370, 128);
			this.inputGroupBox.TabIndex = 18;
			this.inputGroupBox.TabStop = false;
			this.inputGroupBox.Text = "Connect to SQL Server";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label3.Location = new System.Drawing.Point(12, 28);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(370, 2);
			this.label3.TabIndex = 20;
			// 
			// saveValuesCheckBox
			// 
			this.saveValuesCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.saveValuesCheckBox.AutoSize = true;
			this.saveValuesCheckBox.Location = new System.Drawing.Point(12, 180);
			this.saveValuesCheckBox.Name = "saveValuesCheckBox";
			this.saveValuesCheckBox.Size = new System.Drawing.Size(85, 17);
			this.saveValuesCheckBox.TabIndex = 5;
			this.saveValuesCheckBox.Text = "Save values";
			this.saveValuesCheckBox.UseVisualStyleBackColor = true;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::DatabaseDescriptionTool.Properties.Resources.database_key;
			this.pictureBox1.Location = new System.Drawing.Point(12, 7);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(18, 19);
			this.pictureBox1.TabIndex = 22;
			this.pictureBox1.TabStop = false;
			// 
			// infoLabel
			// 
			this.infoLabel.AutoSize = true;
			this.infoLabel.Location = new System.Drawing.Point(29, 9);
			this.infoLabel.Name = "infoLabel";
			this.infoLabel.Size = new System.Drawing.Size(25, 13);
			this.infoLabel.TabIndex = 21;
			this.infoLabel.Text = "Info";
			// 
			// serverNameComboBox
			// 
			this.serverNameComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.serverNameComboBox.FormattingEnabled = true;
			this.serverNameComboBox.Location = new System.Drawing.Point(100, 19);
			this.serverNameComboBox.Name = "serverNameComboBox";
			this.serverNameComboBox.Size = new System.Drawing.Size(256, 21);
			this.serverNameComboBox.TabIndex = 1;
			// 
			// ConnectionDialogForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(394, 207);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.infoLabel);
			this.Controls.Add(this.saveValuesCheckBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.inputGroupBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConnectionDialogForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Title";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConnectionDialogForm_FormClosing);
			this.inputGroupBox.ResumeLayout(false);
			this.inputGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Button okButton;
	private System.Windows.Forms.Button cancelButton;
	private System.Windows.Forms.Label serverNameLabel;
	private System.Windows.Forms.Label authenticationLabel;
	private ComboBoxCustom authenticationComboBox;
	private System.Windows.Forms.Label usernameLabel;
	private System.Windows.Forms.Label passwordLabel;
	private System.Windows.Forms.TextBox userNameTextBox;
	private System.Windows.Forms.TextBox passwordTextBox;
	private System.Windows.Forms.GroupBox inputGroupBox;
	private System.Windows.Forms.Label label3;
	private System.Windows.Forms.CheckBox saveValuesCheckBox;
	private System.Windows.Forms.PictureBox pictureBox1;
	private System.Windows.Forms.Label infoLabel;
	private System.Windows.Forms.ComboBox serverNameComboBox;
}
