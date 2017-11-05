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
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

public partial class HandleDescriptionFieldForm : Form
{
	private bool _textChanged;
	private string _initialNameValue;
	private string _initialDatabaseFieldNameValue;

	public HandleDescriptionFieldForm(SqlConnectionStringBuilder connectionString)
	{
		InitializeComponent();
		InitializeDatabaseFieldNameComboBox(connectionString);
	}

	public void SetValues(DescriptionField descriptionField)
	{
		nameTextBox.Text = descriptionField.DisplayText;
		databaseFieldNameComboBox.Text = descriptionField.DatabaseFieldName;
		useForImageButton.Enabled = !descriptionField.UseForImage;
		infoTextBox.Text = descriptionField.Information;
		_textChanged = false;
		_initialNameValue = descriptionField.DisplayText.ToLower();
		_initialDatabaseFieldNameValue = descriptionField.DatabaseFieldName.ToLower();
	}

	public DescriptionField GetValue()
	{
		return new DescriptionField(nameTextBox.Text, databaseFieldNameComboBox.Text, !useForImageButton.Enabled, infoTextBox.Text);
	}

	private void InitializeDatabaseFieldNameComboBox(SqlConnectionStringBuilder connectionString)
	{
		databaseFieldNameComboBox.DrawMode = DrawMode.OwnerDrawFixed;
		databaseFieldNameComboBox.DrawItem += DatabaseFieldNameComboBox_DrawItem;

		DatabaseOperation databaseOperation = new DatabaseOperation(connectionString.ToString());

		DataTable databasesDataTable = databaseOperation.GetDatabases();

		foreach (DataRow databaseDataRow in databasesDataTable.Rows)
		{
			connectionString.InitialCatalog = databaseDataRow["name"].ToString();
			databaseOperation.ChangeConnection(connectionString.ToString());

			DataTable descriptionsDataTable = databaseOperation.GetDescriptionFields();

			if (descriptionsDataTable.Rows.Count > 0)
			{
				databaseFieldNameComboBox.Items.Add(new ComboBoxItem(databaseDataRow["name"].ToString(), true, -1));

				foreach (DataRow descriptionDataRow in descriptionsDataTable.Rows)
				{
					databaseFieldNameComboBox.Items.Add(new ComboBoxItem(descriptionDataRow["name"].ToString(), false, Convert.ToInt32(descriptionDataRow["count"].ToString())));
				}
			}
		}

		databaseOperation.Dispose();
	}

	private class ComboBoxItem
	{
		public readonly string Text;
		public readonly bool IsDatabaseName;
		public readonly int Count;

		public ComboBoxItem(string text, bool isDatabaseName, int count)
		{
			Text = text;
			IsDatabaseName = isDatabaseName;
			Count = count;
		}

		public override string ToString()
		{
			return Text;
		}
	}

	private void DatabaseFieldNameComboBox_DrawItem(object sender, DrawItemEventArgs e)
	{
		ComboBoxItem comboBoxItem = (ComboBoxItem)databaseFieldNameComboBox.Items[e.Index];

		string databaseDescriptionFieldExistsIn = DatabaseDescriptionFieldExistsIn(comboBoxItem.Text);

		string text = comboBoxItem.Text;

		if (!comboBoxItem.IsDatabaseName)
		{
			text = string.Format("    {0} (Unmapped, Descriptions: {1})", comboBoxItem.Text, comboBoxItem.Count);

			if (databaseDescriptionFieldExistsIn != null)
			{
				text = string.Format("    {0} (Mapped to: {1}, Descriptions: {2})", comboBoxItem.Text, databaseDescriptionFieldExistsIn, comboBoxItem.Count);
			}
		}

		using (SolidBrush brush = new SolidBrush(e.ForeColor))
		{
			Font font = e.Font;

			if (comboBoxItem.IsDatabaseName)
			{
				font = new Font(font, FontStyle.Bold);
			}
			else if (databaseDescriptionFieldExistsIn != null)
			{
				brush.Color = Color.DarkGray;
			}

			e.DrawBackground();
			e.Graphics.DrawString(text, font, brush, e.Bounds);
			e.DrawFocusRectangle();
		}
	}

	private static string DatabaseDescriptionFieldExistsIn(string name)
	{
		foreach (DescriptionField item in ConfigHandler.DescriptionFields)
		{
			if (item.DatabaseFieldName == name)
			{
				return item.DisplayText;
			}
		}

		return null;
	}

	private void OkButton_Click(object sender, EventArgs e)
	{
		Save();
	}

	private bool UniqueName(string keyName)
	{
		foreach (DescriptionField item in ConfigHandler.DescriptionFields)
		{
			if (keyName.ToLower() == item.DisplayText.ToLower() && _initialNameValue != keyName.ToLower())
			{
				MessageBox.Show("Another Description Field with the same name already exists.\r\n\r\nDescription Field names must be unique.", ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				nameTextBox.Focus();
				return false;
			}
		}

		return true;
	}

	private bool UniqueDatabaseFieldName(string keyName)
	{
		foreach (DescriptionField item in ConfigHandler.DescriptionFields)
		{
			if (keyName.ToLower() == item.DatabaseFieldName.ToLower() && _initialDatabaseFieldNameValue != keyName.ToLower())
			{
				MessageBox.Show("Another Database Field with the same name already exists.\r\n\r\nDatabase Field names must be unique.", ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				databaseFieldNameComboBox.Focus();
				return false;
			}
		}

		return true;
	}

	private bool ValidName(string keyName)
	{
		if (keyName == "")
		{
			MessageBox.Show("Name can't be empty.", ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			nameTextBox.Focus();
			return false;
		}

		return true;
	}

	private bool ValidDatabaseFieldName(string keyName)
	{
		if (keyName == "")
		{
			MessageBox.Show("Database field name can't be empty.", ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			databaseFieldNameComboBox.Focus();
			return false;
		}

		if (keyName.Contains(" "))
		{
			MessageBox.Show("Database field name can't contain spaces.", ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			databaseFieldNameComboBox.Focus();
			return false;
		}

		if (keyName.ToLower() == "microsoft_database_tools_support")
		{
			MessageBox.Show("Database field name can't be \"microsoft_database_tools_support\".\r\n\r\nThis is a reserved name.", ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			databaseFieldNameComboBox.Focus();
			return false;
		}

		try
		{
			XmlDocument xmlDocument = new XmlDocument();
			string testXml = string.Format("<{0}></{0}>", keyName);
			xmlDocument.LoadXml(testXml);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			return false;
		}

		return true;
	}

	private void NameTextBox_TextChanged(object sender, EventArgs e)
	{
		_textChanged = true;
	}

	private void HandleDescriptionFieldForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (_textChanged)
		{
			DialogResult result = MessageBox.Show("Save changes?", ConfigHandler.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

			if (result.ToString() == "Yes")
			{
				bool closeWindow = Save();

				if (!closeWindow)
				{
					e.Cancel = true;
				}
			}
			else if (result.ToString() == "Cancel")
			{
				e.Cancel = true;
			}
		}
	}

	private bool Save()
	{
		if (UniqueName(nameTextBox.Text) && UniqueDatabaseFieldName(databaseFieldNameComboBox.Text) && ValidName(nameTextBox.Text) && ValidDatabaseFieldName(databaseFieldNameComboBox.Text))
		{
			if (_textChanged)
			{
				DialogResult = DialogResult.OK;
			}
			else
			{
				DialogResult = DialogResult.Cancel;
			}

			_textChanged = false;
			return true;
		}

		return false;
	}

	private void UseForImageButton_Click(object sender, EventArgs e)
	{
		useForImageButton.Enabled = false;
		_textChanged = true;
	}

	private void InfoTextBox_TextChanged(object sender, EventArgs e)
	{
		_textChanged = true;
	}

	private void InfoTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.A)
		{
			infoTextBox.SelectAll();
		}
	}

	private void DatabaseFieldNameComboBox_TextChanged(object sender, EventArgs e)
	{
		_textChanged = true;
	}
}
