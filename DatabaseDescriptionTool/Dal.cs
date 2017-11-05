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
using System.Windows.Forms;

public class Dal
{
	public delegate void InfoMessageEventHandler(string infoMessage);
	public event InfoMessageEventHandler InfoMessageEvent;

	public delegate void BeginCommEventHandler();
	public event BeginCommEventHandler BeginCommEvent;

	public delegate void EndCommEventHandler();
	public event EndCommEventHandler EndCommEvent;

	public bool Connected;
	private SqlConnectionStringBuilder _connString;
	private SqlConnection _conn;
	private SqlCommand _cmd;
	private bool _throwError;

	public Dal()
	{
		InitializeDal();
	}

	public Dal(string connectionString)
	{
		InitializeDal(connectionString);
	}

	public void ConnInfoMessage(object sender, SqlInfoMessageEventArgs e)
	{
		if (_throwError)
		{
			HandleError(e.Message, _cmd.CommandText, true);
		}
		else
		{
			FireSendMessageEvent(e.Message);
		}
	}

	public void SetDatabase(string databaseName)
	{
		_throwError = false;
		_conn.ChangeDatabase(databaseName);
		_connString.InitialCatalog = databaseName;
	}

	public SqlConnectionStringBuilder GetConnectionString()
	{
		return _connString;
	}

	public string GetDatabaseName()
	{
		return _connString.InitialCatalog;
	}

	public string GetSqlServerName()
	{
		return _connString.DataSource;
	}

	public string GetUserName()
	{
		return _connString.UserID;
	}

	public string GetPassword()
	{
		return _connString.Password;
	}

	public bool GetIntegratedSecurity()
	{
		return _connString.IntegratedSecurity;
	}

	public DataSet ExecuteDataSet(string sql)
	{
		_throwError = true;
		return DoExecuteDataSet(sql, true);
	}

	public DataSet ExecuteDataSet(string sql, bool throwError)
	{
		_throwError = throwError;
		return DoExecuteDataSet(sql, true);
	}

	public DataTable ExecuteDataTable(string sql)
	{
		_throwError = true;
		DataSet ds = DoExecuteDataSet(sql, true);

		if (ds.Tables.Count > 0)
		{
			return ds.Tables[0];
		}
		else
		{
			return null;
		}
	}

	public DataTable ExecuteDataTable(string sql, bool throwError)
	{
		_throwError = throwError;
		DataSet ds = DoExecuteDataSet(sql, true);
		return ds.Tables[0];
	}

	public void Execute(string sql)
	{
		_throwError = true;
		DoExecuteDataSet(sql, false);
	}

	public void Execute(string sql, bool throwError)
	{
		_throwError = throwError;
		DoExecuteDataSet(sql, false);
	}

	public void Dispose()
	{
		if (_cmd != null)
		{
			_cmd.Cancel();
			_cmd.Dispose();
		}

		if (_conn != null)
		{
			_conn.Close();
			_conn.Dispose();
		}

		SqlConnection.ClearAllPools();
	}

	public bool ChangeConnection(string connectionString)
	{
		Connected = OpenConnection(connectionString, false);
		return Connected;
	}

	public void FireBeginCommEvent()
	{
		if (BeginCommEvent != null)
		{
			BeginCommEvent();
		}
	}

	public void FireEndCommEvent()
	{
		if (EndCommEvent != null)
		{
			EndCommEvent();
		}
	}

	private void InitializeDal()
	{
		Connected = OpenConnection(ConfigHandler.ConnectionString, true);
	}

	private void InitializeDal(string connectionString)
	{
		Connected = OpenConnection(connectionString, false);
	}

	private bool OpenConnection(string connectionString, bool exitOnError)
	{
		SqlConnection conn = new SqlConnection(connectionString);

		try
		{
			conn.Open();

			bool isSqlServerVersionSupported = IsSqlServerVersionSupported(conn.ServerVersion);

			if (!isSqlServerVersionSupported)
			{
				conn.Close();
				HandleError("SQL Server version not supported.", "", exitOnError);
			}
			else
			{
				if (_conn != null)
				{
					_conn.Close();
				}

				_conn = conn;
				_conn.InfoMessage += ConnInfoMessage;
				_conn.FireInfoMessageEventOnUserErrors = true;

				_connString = new SqlConnectionStringBuilder(connectionString);

				return true;
			}
		}
		catch (SqlException ex)
		{
			HandleError(ex.Message, string.Format("Connection string:\r\n\r\n{0}", conn.ConnectionString), exitOnError);
		}
		catch (InvalidOperationException ex)
		{
			HandleError(ex.Message, "Please check the connection settings.", exitOnError);
		}

		return false;
	}

	private static bool IsSqlServerVersionSupported(string serverVersion)
	{
		int version = Convert.ToInt32(serverVersion.Substring(0, serverVersion.IndexOf(".")));

		if (version < 9 || version > 13)
		{
			return false;
		}

		return true;
	}

	private void FireSendMessageEvent(string message)
	{
		if (InfoMessageEvent != null)
		{
			InfoMessageEvent(message);
		}
	}

	private DataSet DoExecuteDataSet(string sql, bool returnDataSet)
	{
		FireBeginCommEvent();
		DataSet dataSet = new DataSet();

		using (_cmd = new SqlCommand())
		{
			_cmd.CommandTimeout = 0;
			_cmd.CommandType = CommandType.Text;

			try
			{
				dataSet = ExecuteCommand(sql, returnDataSet);
			}
			catch (SqlException ex)
			{
				MessageBox.Show(ex.Message, ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		FireEndCommEvent();
		return dataSet;
	}

	private DataSet ExecuteCommand(string sql, bool returnDataSet)
	{
		if (_conn == null)
		{
			return null;
		}

		DataSet dataSet = new DataSet();

		if (_conn.State == ConnectionState.Closed)
		{
			_conn.Open();
		}

		_cmd.Connection = _conn;
		_cmd.CommandText = "set arithabort on";
		_cmd.ExecuteNonQuery();
		_cmd.CommandText = sql;

		if (returnDataSet)
		{
			using (SqlDataAdapter dataAdapter = new SqlDataAdapter(_cmd))
			{
				dataAdapter.Fill(dataSet);
			}
		}
		else
		{
			_cmd.ExecuteNonQuery();
		}

		return dataSet;
	}

	private void HandleError(string message, string sql, bool exit)
	{
		string okButtonText = "Close";

		if (exit)
		{
			okButtonText = "Exit";
		}

		ErrorForm form = new ErrorForm(okButtonText, message, sql);
		form.ShowDialog();
		Application.DoEvents();

		if (exit)
		{
			Dispose();
			Environment.Exit(-1);
		}
	}
}
