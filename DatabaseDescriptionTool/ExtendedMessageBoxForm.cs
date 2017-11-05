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

using System.Windows.Forms;

public partial class ExtendedMessageBoxForm : Form
{
	public bool YesButtonClicked = false;

	public ExtendedMessageBoxForm()
	{
		InitializeComponent();
		Initialize();
	}

	public void SetTitle(string title)
	{
		Text = title;
	}

	public void SetTextLabel(string text)
	{
		textLabel.Text = text;
	}

	public void SetTextBox(string text)
	{
		textBox.Text = text;
	}

	private void Initialize()
	{
		Text = ConfigHandler.ApplicationName;
	}

	private void YesButton_Click(object sender, System.EventArgs e)
	{
		YesButtonClicked = true;
		Close();
	}

	private void NoButton_Click(object sender, System.EventArgs e)
	{
		Close();
	}
}
