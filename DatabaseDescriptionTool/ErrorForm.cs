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
using System.Threading;
using System.Windows.Forms;

public partial class ErrorForm : Form
{
	public ErrorForm(string okButtonText, string message, string sql)
	{
		InitializeComponent();
		Initialize(okButtonText, message, sql);
	}

	private void Initialize(string okButtonText, string message, string sql)
	{
		MinimumSize = new Size(400, 455); // Error in .NET

		okButton.Text = okButtonText;
		infoLabel.Text = ConfigHandler.ApplicationName;
		errorTextBox.Text = message;
		infoTextBox.Text = sql;
	}

	private void OkButton_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void CopyButton_Click(object sender, EventArgs e)
	{
		Thread newThread = new Thread(ThreadMethod);
		newThread.SetApartmentState(ApartmentState.STA);

		string copy = string.Format("/*\r\nError:\r\n\r\n{0}\r\n*/\r\n\r\n{1}", errorTextBox.Text, infoTextBox.Text);
		newThread.Start(copy);
	}

	private static void ThreadMethod(object text)
	{
		Clipboard.SetText(text.ToString());
	}

	private void ErrorTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.A)
		{
			errorTextBox.SelectAll();
		}
	}

	private void InfoTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.A)
		{
			infoTextBox.SelectAll();
		}
	}
}
