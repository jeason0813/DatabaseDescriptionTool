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

using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Forms;

public partial class ConnectionDialogForm : Form
{
	public bool ConnectionChanged;
	private DatabaseOperation _databaseOperation;
	private BackgroundWorker _worker;
	private bool _runWorkerActive;

	public ConnectionDialogForm(DatabaseOperation databaseOperation)
	{
		InitializeComponent();
		SetApplicationName();

		_databaseOperation = databaseOperation;
		authenticationComboBox.SelectedIndex = 0;

		SetDefaultValues();
		SearchHistoryHandler.LoadItems(serverNameComboBox, "RecentListServerName");

		if (ConfigHandler.SaveConnectionString.ToLower() == "true")
		{
			saveValuesCheckBox.Checked = true;
		}
		else
		{
			saveValuesCheckBox.Checked = false;
		}

		InitializeWorker();
	}

	public DatabaseOperation GetDatabaseOperation()
	{
		return _databaseOperation;
	}

	protected override void OnLoad(System.EventArgs e)
	{
		BeginInvoke(new MethodInvoker(SetFocus));
		base.OnLoad(e);
	}

	private void InitializeWorker()
	{
		_worker = new BackgroundWorker();
		_worker.DoWork += Worker_DoWork;
		_worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
	}

	private void Worker_DoWork(object sender, DoWorkEventArgs e)
	{
		RunWorkerArgument arg = (RunWorkerArgument)e.Argument;

		string connectionString = arg.ConnectionString;

		if (_databaseOperation == null) // new connection, no connection has been made
		{
			_databaseOperation = new DatabaseOperation(connectionString);
		}
		else // a connection has already been made, but should be changed
		{
			ConnectionChanged = _databaseOperation.ChangeConnection(connectionString);
		}

		if (_databaseOperation.Connected)
		{
			ConnectionChanged = true;
		}

		if (!saveValuesCheckBox.Checked)
		{
			ConfigHandler.SaveConnectionString = "false";
			ConfigHandler.SaveConfig();
		}

		if (ConnectionChanged)
		{
			ConfigHandler.ConnectionString = connectionString;
		}

		if (saveValuesCheckBox.Checked && ConnectionChanged)
		{
			ConfigHandler.ConnectionStringToSave = connectionString;
			ConfigHandler.SaveConnectionString = "true";
			ConfigHandler.SaveConfig();
		}
	}

	private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		_runWorkerActive = false;
		EndConnect();

		if (ConnectionChanged)
		{
			if (saveValuesCheckBox.Checked)
			{
				SearchHistoryHandler.AddItem(serverNameComboBox, serverNameComboBox.Text, "RecentListServerName");
			}

			Close();
		}
	}

	private void SetApplicationName()
	{
		infoLabel.Text = ConfigHandler.ApplicationName;
		Text = ConfigHandler.ApplicationName;
	}

	private void SetDefaultValues()
	{
		SqlConnectionStringBuilder tempConnString;

		if (_databaseOperation == null) // new connection, no connection has been made
		{
			tempConnString = new SqlConnectionStringBuilder(ConfigHandler.ConnectionString);
		}
		else // a connection has already been made, but should be changed
		{
			if (ConfigHandler.SaveConnectionString.ToLower() != "true")
			{
				tempConnString = new SqlConnectionStringBuilder(ConfigHandler.ConnectionString);
			}
			else
			{
				tempConnString = new SqlConnectionStringBuilder(ConfigHandler.ConnectionStringToSave);
			}
		}

		serverNameComboBox.Text = tempConnString.DataSource;

		if (!tempConnString.IntegratedSecurity)
		{
			authenticationComboBox.SelectedIndex = 1;

			userNameTextBox.Text = tempConnString.UserID;
			passwordTextBox.Text = tempConnString.Password;
		}
	}

	private void SetFocus()
	{
		serverNameComboBox.Focus();
	}

	private void CancelButton_Click(object sender, System.EventArgs e)
	{
		Close();
	}

	private void AuthenticationComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		if (authenticationComboBox.SelectedIndex == 1)
		{
			userNameTextBox.Enabled = true;
			passwordTextBox.Enabled = true;
			usernameLabel.Enabled = true;
			passwordLabel.Enabled = true;
		}
		else
		{
			userNameTextBox.Enabled = false;
			passwordTextBox.Enabled = false;
			usernameLabel.Enabled = false;
			passwordLabel.Enabled = false;
		}
	}

	private void BeginConnect()
	{
		inputGroupBox.Enabled = false;
		cancelButton.Enabled = false;
		okButton.Enabled = false;
		saveValuesCheckBox.Enabled = false;

		Application.DoEvents();
	}

	private void EndConnect()
	{
		inputGroupBox.Enabled = true;
		cancelButton.Enabled = true;
		okButton.Enabled = true;
		saveValuesCheckBox.Enabled = true;
	}

	private bool VerifyFields()
	{
		if (serverNameComboBox.Text.Trim() == "")
		{
			MessageBox.Show("Please enter server name.", ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			serverNameComboBox.Focus();
			return false;
		}

		if (authenticationComboBox.SelectedIndex == 1)
		{
			if (userNameTextBox.Text.Trim() == "")
			{
				MessageBox.Show("Please enter user name.", ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				userNameTextBox.Focus();
				return false;
			}
		}

		return true;
	}

	private void OkButton_Click(object sender, System.EventArgs e)
	{
		if (!VerifyFields())
		{
			return;
		}

		BeginConnect();

		RunWorkerArgument arg = new RunWorkerArgument();
		arg.ConnectionString = GetConnectionString();

		_runWorkerActive = true;
		_worker.RunWorkerAsync(arg);
	}

	private string GetConnectionString()
	{
		SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();

		if (authenticationComboBox.SelectedIndex == 1)
		{
			connectionString.IntegratedSecurity = false;
			connectionString.UserID = userNameTextBox.Text;
			connectionString.Password = passwordTextBox.Text;
		}
		else
		{
			connectionString.IntegratedSecurity = true;
		}

		connectionString.DataSource = serverNameComboBox.Text;
		connectionString.ApplicationName = ConfigHandler.ApplicationName;

		return connectionString.ToString();
	}

	private class RunWorkerArgument
	{
		public string ConnectionString;
	}

	private void ConnectionDialogForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (_runWorkerActive)
		{
			e.Cancel = true;
		}
	}
}
