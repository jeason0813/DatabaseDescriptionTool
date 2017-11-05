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
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using Microsoft.Win32;

public static class ExcelHelper
{
	public static bool CreateExcelFile(string excelFile, string sheetName, string headerRow)
	{
		string connectionString = string.Format("Provider=Microsoft.Jet.OleDb.4.0; Data Source={0}; Extended Properties=\"Excel 8.0;\"", excelFile);

		try
		{
			using (OleDbConnection connection = new OleDbConnection(connectionString))
			{
				connection.Open();

				using (OleDbCommand command = new OleDbCommand())
				{
					command.Connection = connection;

					string[] headerItems = headerRow.Split(',');
					string createRow = "";

					for (int i = 0; i < headerItems.Length; i++)
					{
						createRow = createRow + headerItems[i].Trim() + " ntext";

						if (i != headerItems.Length - 1)
						{
							createRow = createRow + ", ";
						}
					}

					command.CommandText = string.Format("create table [{0}] ({1})", sheetName, createRow);
					command.ExecuteNonQuery();
				}
			}

			return true;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			return false;
		}
	}

	public static void AppendToExcel(string excelFile, string sheetName, string headerRow, List<string> dataRows)
	{
		string connectionString = string.Format("Provider=Microsoft.Jet.OleDb.4.0; Data Source={0}; Extended Properties=\"Excel 8.0;\"", excelFile);

		try
		{
			using (OleDbConnection connection = new OleDbConnection(connectionString))
			{
				connection.Open();

				using (OleDbCommand command = new OleDbCommand())
				{
					command.Connection = connection;

					foreach (string dataRow in dataRows)
					{
						command.CommandText = string.Format("insert into [{0}$] ({1}) values ({2})", sheetName, headerRow, dataRow);
						command.ExecuteNonQuery();
					}
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}

	public static DataTable ReadFromExcelFile(string excelFile, string sheetName)
	{
		FixKb189897();

		DataTable dataTable = new DataTable();
		string connectionString = string.Format("Provider=Microsoft.Jet.OleDb.4.0; Data Source={0}; Extended Properties=\"Excel 8.0;HDR=No;IMEX=1;\"", excelFile);

		try
		{
			using (OleDbConnection connection = new OleDbConnection(connectionString))
			{
				connection.Open();

				using (OleDbCommand command = new OleDbCommand())
				{
					command.Connection = connection;
					command.CommandText = string.Format("select * from [{0}$]", sheetName);

					using (OleDbDataAdapter adapter = new OleDbDataAdapter())
					{
						adapter.SelectCommand = command;
						adapter.Fill(dataTable);
					}
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		return dataTable;
	}

	private static void FixKb189897()
	{
		RegistryKey rk = Registry.LocalMachine;
		RegistryKey sk = rk.CreateSubKey(@"SOFTWARE\Microsoft\Jet\4.0\Engines\Excel\");

		if (sk != null)
		{
			try
			{
				sk.SetValue("TypeGuessRows", 0);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
	}
}
