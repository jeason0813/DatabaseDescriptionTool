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
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

public partial class DeleteDescriptionsInSubNodes : Form
{
	private string _activeDatabaseBeforeOperation;
	private readonly DatabaseOperation _databaseOperation;
	private BackgroundWorker _worker;
	private readonly Stopwatch _sw = new Stopwatch();
	private bool _running;
	private List<string> _selectedDatabaseFieldNames;
	private readonly ObjectsSelectorForm _objectsSelectorForm;
	private string _individualObjectsInitializedForDatabase;
	private bool _deleted;

	public DeleteDescriptionsInSubNodes(DatabaseOperation databaseOperation)
	{
		InitializeComponent();
		_databaseOperation = databaseOperation;
		_objectsSelectorForm = new ObjectsSelectorForm();
		InitializeDatabaseComboBox();
	}

	public bool GetDeleted()
	{
		return _deleted;
	}

	public void SetIndividualObjects(CustomNode node)
	{
		InitializeIndividualObjects();
		_objectsSelectorForm.SetIndividualObjects(node);
	}

	private void InitializeDatabaseComboBox()
	{
		DataTable dt = _databaseOperation.GetDatabases();

		foreach (DataRow dr in dt.Rows)
		{
			databaseComboBox.Items.Add(dr["name"].ToString());
		}

		_activeDatabaseBeforeOperation = _databaseOperation.GetDatabaseName();
		databaseComboBox.SelectedItem = _activeDatabaseBeforeOperation;
	}

	private void InitializeWorker()
	{
		_worker = new BackgroundWorker();

		_worker.DoWork += Worker_DoWork;
		_worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
		_worker.ProgressChanged += Worker_ProgressChanged;

		_worker.WorkerSupportsCancellation = false;
		_worker.WorkerReportsProgress = true;
	}

	private void DatabaseComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		SetStartButton();

		if (databaseComboBox.SelectedItem != null)
		{
			selectObjectsButton.Enabled = true;
		}
		else
		{
			selectObjectsButton.Enabled = false;
		}
	}

	private void SetStartButton()
	{
		if (databaseComboBox.SelectedItem != null && _objectsSelectorForm.AnyCheckBoxChecked())
		{
			startButton.Enabled = true;
		}
		else
		{
			startButton.Enabled = false;
		}
	}

	private void StartButton_Click(object sender, EventArgs e)
	{
		Start();
		_deleted = true;
	}

	private void Start()
	{
		_running = true;
		startButton.Enabled = false;
		cancelButton.Enabled = false;
		databaseComboBox.Enabled = false;
		selectObjectsButton.Enabled = false;

		timer1.Start();
		_sw.Reset();
		_sw.Start();

		InitializeIndividualObjects();

		if (!_objectsSelectorForm.allCheckBox.Checked)
		{
			_selectedDatabaseFieldNames = new List<string>();

			foreach (ListViewItem item in _objectsSelectorForm.fieldNameListView.SelectedItems)
			{
				_selectedDatabaseFieldNames.Add(item.Name);
			}
		}

		InitializeWorker();
		_worker.RunWorkerAsync();
	}

	private void Worker_DoWork(object sender, DoWorkEventArgs e)
	{
		DataTable dataTable = _databaseOperation.GetDescriptionsToDelete();

		ProgressObject progressObject = new ProgressObject();
		progressObject.ProgressBarMaximum = dataTable.Rows.Count;

		int i = 1;

		foreach (DataRow dataRow in dataTable.Rows)
		{
			progressObject.ProgressBarValue = i;
			_worker.ReportProgress(-1, progressObject);

			bool delete = false;

			if (_objectsSelectorForm.allCheckBox.Checked || _selectedDatabaseFieldNames.Contains(dataRow["DatabaseFieldName"].ToString()))
			{
				if (_objectsSelectorForm.AllCheckBoxCheckedIndividualTables() || _objectsSelectorForm.GetSelectedIndividualTables().Contains(dataRow["Level1Name"].ToString()))
				{
					if (_objectsSelectorForm.IncludeObjectType(NodeType.Tables))
					{
						if (dataRow["Level1Type"].ToString() == "Table" && dataRow["Level2Type"].ToString() == "")
						{
							delete = true;
						}
					}

					if (_objectsSelectorForm.IncludeObjectType(NodeType.TableColumns))
					{
						if (dataRow["Level1Type"].ToString() == "Table" && dataRow["Level2Type"].ToString() == "Column")
						{
							delete = true;
						}
					}

					if (_objectsSelectorForm.IncludeObjectType(NodeType.TableKeys))
					{
						if (dataRow["Level1Type"].ToString() == "Table" && dataRow["Level2Type"].ToString() == "Key")
						{
							delete = true;
						}
					}

					if (_objectsSelectorForm.IncludeObjectType(NodeType.TableConstraints))
					{
						if (dataRow["Level1Type"].ToString() == "Table" && dataRow["Level2Type"].ToString() == "Constraint")
						{
							delete = true;
						}
					}

					if (_objectsSelectorForm.IncludeObjectType(NodeType.TableIndexes))
					{
						if (dataRow["Level1Type"].ToString() == "Table" && dataRow["Level2Type"].ToString() == "Index")
						{
							delete = true;
						}
					}

					if (_objectsSelectorForm.IncludeObjectType(NodeType.TableTriggers))
					{
						if (dataRow["Level1Type"].ToString() == "Table" && dataRow["Level2Type"].ToString() == "Trigger")
						{
							delete = true;
						}
					}
				}

				if (_objectsSelectorForm.AllCheckBoxCheckedIndividualViews() || _objectsSelectorForm.GetSelectedIndividualViews().Contains(dataRow["Level1Name"].ToString()))
				{
					if (_objectsSelectorForm.IncludeObjectType(NodeType.Views))
					{
						if (dataRow["Level1Type"].ToString() == "View" && dataRow["Level2Type"].ToString() == "")
						{
							delete = true;
						}
					}

					if (_objectsSelectorForm.IncludeObjectType(NodeType.ViewColumns))
					{
						if (dataRow["Level1Type"].ToString() == "View" && dataRow["Level2Type"].ToString() == "Column")
						{
							delete = true;
						}
					}

					if (_objectsSelectorForm.IncludeObjectType(NodeType.ViewIndexes))
					{
						if (dataRow["Level1Type"].ToString() == "View" && dataRow["Level2Type"].ToString() == "Index")
						{
							delete = true;
						}
					}

					if (_objectsSelectorForm.IncludeObjectType(NodeType.ViewTriggers))
					{
						if (dataRow["Level1Type"].ToString() == "View" && dataRow["Level2Type"].ToString() == "Trigger")
						{
							delete = true;
						}
					}
				}

				if (_objectsSelectorForm.AllCheckBoxCheckedIndividualStoredProcedures() || _objectsSelectorForm.GetSelectedIndividualStoredProcedures().Contains(dataRow["Level1Name"].ToString()))
				{
					if (_objectsSelectorForm.IncludeObjectType(NodeType.StoredProcedures))
					{
						if (dataRow["Level1Type"].ToString() == "Procedure" && dataRow["Level2Type"].ToString() == "")
						{
							delete = true;
						}
					}

					if (_objectsSelectorForm.IncludeObjectType(NodeType.StoredProcedureParameters))
					{
						if (dataRow["Level1Type"].ToString() == "Procedure" && dataRow["Level2Type"].ToString() == "Parameter")
						{
							delete = true;
						}
					}
				}

				if (_objectsSelectorForm.AllCheckBoxCheckedIndividualTableValuedFunctions() || _objectsSelectorForm.GetSelectedIndividualTableValuedFunctions().Contains(dataRow["Level1Name"].ToString()))
				{
					if (_objectsSelectorForm.IncludeObjectType(NodeType.TableValuedFunctions))
					{
						if (dataRow["Level1Type"].ToString() == "Table-valued Function" && dataRow["Level2Type"].ToString() == "")
						{
							delete = true;
						}
					}

					if (_objectsSelectorForm.IncludeObjectType(NodeType.TableValuedFunctionParameters))
					{
						if (dataRow["Level1Type"].ToString() == "Table-valued Function" && dataRow["Level2Type"].ToString() == "Parameter")
						{
							delete = true;
						}
					}
				}

				if (_objectsSelectorForm.AllCheckBoxCheckedIndividualScalarValuedFunctions() || _objectsSelectorForm.GetSelectedIndividualScalarValuedFunctions().Contains(dataRow["Level1Name"].ToString()))
				{
					if (_objectsSelectorForm.IncludeObjectType(NodeType.ScalarValuedFunctions))
					{
						if (dataRow["Level1Type"].ToString() == "Scalar-valued Function" && dataRow["Level2Type"].ToString() == "")
						{
							delete = true;
						}
					}

					if (_objectsSelectorForm.IncludeObjectType(NodeType.ScalarValuedFunctionParameters))
					{
						if (dataRow["Level1Type"].ToString() == "Scalar-valued Function" && dataRow["Level2Type"].ToString() == "Parameter")
						{
							delete = true;
						}
					}
				}
			}

			if (delete)
			{
				DoDelete(dataRow["Level1Type"].ToString(), dataRow["Level1Name"].ToString(), dataRow["Level2Type"].ToString(), dataRow["Level2Name"].ToString(), dataRow["DatabaseFieldName"].ToString());
			}

			i++;
		}
	}

	private void DoDelete(string level1Type, string level1Name, string level2Type, string level2Name, string databaseFieldName)
	{
		string sql = ExtendedPropertiesHelper.GetDropSql(level1Type, level1Name, level2Type, level2Name, databaseFieldName);
		_databaseOperation.Execute(sql);
	}

	private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
	{
		ProgressObject progressObject = (ProgressObject)e.UserState;

		progressBar1.Maximum = progressObject.ProgressBarMaximum;
		progressBar1.Value = progressObject.ProgressBarValue;
		statusLabel.Text = "Status: Deleting...";
	}

	private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		_running = false;
		startButton.Enabled = true;
		cancelButton.Enabled = true;
		databaseComboBox.Enabled = true;
		selectObjectsButton.Enabled = true;

		timer1.Stop();
		_sw.Stop();

		statusLabel.Text = "Status: Completed";
	}

	private class ProgressObject
	{
		public int ProgressBarMaximum;
		public int ProgressBarValue;
	}

	private void Timer1_Tick(object sender, EventArgs e)
	{
		string hours = _sw.Elapsed.Hours.ToString();
		string minutes = _sw.Elapsed.Minutes.ToString();
		string seconds = _sw.Elapsed.Seconds.ToString();

		if (hours.Length == 1)
		{
			hours = string.Format("0{0}", hours);
		}

		if (minutes.Length == 1)
		{
			minutes = string.Format("0{0}", minutes);
		}

		if (seconds.Length == 1)
		{
			seconds = string.Format("0{0}", seconds);
		}

		timeElapsedLabel.Text = string.Format("{0}:{1}:{2}", hours, minutes, seconds);
	}

	private void DeleteForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (_running)
		{
			MessageBox.Show("Can't close while deleting.", ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			e.Cancel = true;
		}
		else
		{
			if (_activeDatabaseBeforeOperation != "")
			{
				_databaseOperation.SetDatabase(_activeDatabaseBeforeOperation);
			}
		}
	}

	private void SelectObjectsButton_Click(object sender, EventArgs e)
	{
		InitializeIndividualObjects();
		_objectsSelectorForm.ShowDialog();
		SetStartButton();
	}

	private void InitializeIndividualObjects()
	{
		if (databaseComboBox.SelectedItem.ToString() != _individualObjectsInitializedForDatabase)
		{
			_databaseOperation.SetDatabase(databaseComboBox.SelectedItem.ToString());
			_objectsSelectorForm.InitializeIndividualObjects(_databaseOperation);
			_individualObjectsInitializedForDatabase = databaseComboBox.SelectedItem.ToString();
		}
	}
}
