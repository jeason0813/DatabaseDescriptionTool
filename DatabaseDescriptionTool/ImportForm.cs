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
using System.IO;
using System.Windows.Forms;
using System.Xml;

public partial class ImportForm : Form
{
	private string _activeDatabaseBeforeOperation;
	private readonly DatabaseOperation _databaseOperation;
	private BackgroundWorker _worker;
	private readonly Stopwatch _sw = new Stopwatch();
	private bool _running;
	private bool _reloadTreeViewAfterRun;
	private string _fileName;
	private List<string> _selectedDatabaseFieldNames;
	private string _fileFormat;
	private readonly ObjectsSelectorForm _objectsSelectorForm;
	private string _currentXmlOperation;
	private readonly ProgressObject _progressObject = new ProgressObject();
	private string _individualObjectsInitializedForDatabase;

	public ImportForm(DatabaseOperation databaseOperation)
	{
		InitializeComponent();
		_databaseOperation = databaseOperation;
		_objectsSelectorForm = new ObjectsSelectorForm();
		InitializeDatabaseComboBox();
		InitializeFileFormatComboBox();
	}

	public void SetIndividualObjects(CustomNode node)
	{
		InitializeIndividualObjects();
		_objectsSelectorForm.SetIndividualObjects(node);
	}

	public bool GetReloadTreeViewAfterRun()
	{
		return _reloadTreeViewAfterRun;
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

	private void InitializeFileFormatComboBox()
	{
		fileFormatComboBox.Items.Add("Xml");
		fileFormatComboBox.Items.Add("Excel");

		fileFormatComboBox.SelectedItem = "Xml";
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

	private void FixDialogFileNameExtension()
	{
		_fileFormat = openFileDialog1.DefaultExt;

		string dialogFileName = Path.GetExtension(openFileDialog1.FileName);

		if (dialogFileName != null && dialogFileName.ToLower() != string.Format(".{0}", _fileFormat.ToLower()))
		{
			openFileDialog1.FileName = "";
		}
	}

	private void StartButton_Click(object sender, EventArgs e)
	{
		FixDialogFileNameExtension();

		DialogResult result = openFileDialog1.ShowDialog();

		if (result.ToString() == "OK")
		{
			Application.DoEvents();
			_fileName = openFileDialog1.FileName;
			Start();
		}
	}

	private void Start()
	{
		_reloadTreeViewAfterRun = true;
		_running = true;
		startButton.Enabled = false;
		cancelButton.Enabled = false;
		databaseComboBox.Enabled = false;
		fileFormatComboBox.Enabled = false;
		selectObjectsButton.Enabled = false;

		_currentXmlOperation = null;

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
		string extension = openFileDialog1.DefaultExt;

		if (extension != null)
		{
			extension = extension.ToLower();
		}

		ProgressObject progressObject = new ProgressObject();
		progressObject.OperationLabelText = "";
		progressObject.ProgressBarValue = -1;
		_worker.ReportProgress(-1, progressObject);

		ImportNode(extension);
	}

	private void ImportNode(string extension)
	{
		if (extension == "xls")
		{
			ImportNodeExcel();
		}
		else if (extension == "xml")
		{
			ImportNodeXml();
		}
	}

	private int GetTotalCountXml()
	{
		_objectsSelectorForm.SetIncludeParentObject(true);

		int totalCount = 0;

		if (_objectsSelectorForm.IncludeObjectType(NodeType.Tables) || _objectsSelectorForm.IncludeObjectType(NodeType.TableColumns) || _objectsSelectorForm.IncludeObjectType(NodeType.TableKeys) || _objectsSelectorForm.IncludeObjectType(NodeType.TableConstraints) || _objectsSelectorForm.IncludeObjectType(NodeType.TableTriggers) || _objectsSelectorForm.IncludeObjectType(NodeType.TableIndexes))
		{
			totalCount++;
		}

		if (_objectsSelectorForm.IncludeObjectType(NodeType.Views) || _objectsSelectorForm.IncludeObjectType(NodeType.ViewColumns) || _objectsSelectorForm.IncludeObjectType(NodeType.ViewTriggers) || _objectsSelectorForm.IncludeObjectType(NodeType.ViewIndexes))
		{
			totalCount++;
		}

		if (_objectsSelectorForm.IncludeObjectType(NodeType.StoredProcedures) || _objectsSelectorForm.IncludeObjectType(NodeType.StoredProcedureParameters))
		{
			totalCount++;
		}

		if (_objectsSelectorForm.IncludeObjectType(NodeType.TableValuedFunctions) || _objectsSelectorForm.IncludeObjectType(NodeType.TableValuedFunctionParameters))
		{
			totalCount++;
		}

		if (_objectsSelectorForm.IncludeObjectType(NodeType.ScalarValuedFunctions) || _objectsSelectorForm.IncludeObjectType(NodeType.ScalarValuedFunctionParameters))
		{
			totalCount++;
		}

		_objectsSelectorForm.SetIncludeParentObject(false);

		return totalCount;
	}

	private void ImportNodeXml()
	{
		try
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(_fileName);

			int total = GetTotalCountXml();
			int current = 0;

			ImportXml(xmlDocument, "/database/tables", NodeType.Tables, null, string.Format("Importing Tables ({0}/{1})", ++current, total));
			ImportXml(xmlDocument, "/database/views", NodeType.Views, null, string.Format("Importing Views ({0}/{1})", ++current, total));
			ImportXml(xmlDocument, "/database/storedprocedures", NodeType.StoredProcedures, null, string.Format("Importing Stored Procedures ({0}/{1})", ++current, total));
			ImportXml(xmlDocument, "/database/tablevaluedfunctions", NodeType.TableValuedFunctions, null, string.Format("Importing Table-valued Functions ({0}/{1})", ++current, total));
			ImportXml(xmlDocument, "/database/scalarvaluedfunctions", NodeType.ScalarValuedFunctions, null, string.Format("Importing Scalar-valued Functions ({0}/{1})", ++current, total));
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}

	private void ImportXml(XmlNode xmlDocument, string xPath, NodeType nodeType, string parentNodeName, string operationLabelText)
	{
		XmlNode selectSingleNode = xmlDocument.SelectSingleNode(xPath);

		if (selectSingleNode != null)
		{
			if (_currentXmlOperation != operationLabelText)
			{
				_currentXmlOperation = operationLabelText;
				_progressObject.ProgressBarValue = 0;
				_progressObject.ProgressBarMaximum = selectSingleNode.ChildNodes.Count;
				_progressObject.OperationLabelText = operationLabelText;
				_worker.ReportProgress(-1, _progressObject);
			}

			for (int i = 0; i < selectSingleNode.ChildNodes.Count; i++)
			{
				switch (nodeType)
				{
					case NodeType.Tables:
						{
							string name = GetXmlNodeName(selectSingleNode.ChildNodes[i]);

							_progressObject.ProgressBarValue++;
							_worker.ReportProgress(-1, _progressObject);

							ImportXmlNodeValue(selectSingleNode.ChildNodes[i], NodeType.Table, name);
							ImportXml(selectSingleNode.ChildNodes[i], "indexes", NodeType.TableIndexes, name, operationLabelText);
							ImportXml(selectSingleNode.ChildNodes[i], "triggers", NodeType.TableTriggers, name, operationLabelText);
							ImportXml(selectSingleNode.ChildNodes[i], "constraints", NodeType.TableConstraints, name, operationLabelText);
							ImportXml(selectSingleNode.ChildNodes[i], "keys", NodeType.TableKeys, name, operationLabelText);
							ImportXml(selectSingleNode.ChildNodes[i], "columns", NodeType.TableColumns, name, operationLabelText);
						}
						break;
					case NodeType.TableIndexes:
						{
							ImportXmlNodeValue(selectSingleNode.ChildNodes[i], NodeType.TableIndex, parentNodeName);
						}
						break;
					case NodeType.TableTriggers:
						{
							ImportXmlNodeValue(selectSingleNode.ChildNodes[i], NodeType.TableTrigger, parentNodeName);
						}
						break;
					case NodeType.TableConstraints:
						{
							ImportXmlNodeValue(selectSingleNode.ChildNodes[i], NodeType.TableConstraint, parentNodeName);
						}
						break;
					case NodeType.TableKeys:
						{
							ImportXmlNodeValue(selectSingleNode.ChildNodes[i], NodeType.TableKey, parentNodeName);
						}
						break;
					case NodeType.TableColumns:
						{
							ImportXmlNodeValue(selectSingleNode.ChildNodes[i], NodeType.TableColumn, parentNodeName);
						}
						break;
					case NodeType.Views:
						{
							string name = GetXmlNodeName(selectSingleNode.ChildNodes[i]);

							_progressObject.ProgressBarValue++;
							_worker.ReportProgress(-1, _progressObject);

							ImportXmlNodeValue(selectSingleNode.ChildNodes[i], NodeType.View, name);
							ImportXml(selectSingleNode.ChildNodes[i], "indexes", NodeType.ViewIndexes, name, operationLabelText);
							ImportXml(selectSingleNode.ChildNodes[i], "triggers", NodeType.ViewTriggers, name, operationLabelText);
							ImportXml(selectSingleNode.ChildNodes[i], "columns", NodeType.ViewColumns, name, operationLabelText);
						}
						break;
					case NodeType.ViewIndexes:
						{
							ImportXmlNodeValue(selectSingleNode.ChildNodes[i], NodeType.ViewIndex, parentNodeName);
						}
						break;
					case NodeType.ViewTriggers:
						{
							ImportXmlNodeValue(selectSingleNode.ChildNodes[i], NodeType.ViewTrigger, parentNodeName);
						}
						break;
					case NodeType.ViewColumns:
						{
							ImportXmlNodeValue(selectSingleNode.ChildNodes[i], NodeType.ViewColumn, parentNodeName);
						}
						break;
					case NodeType.StoredProcedures:
						{
							string name = GetXmlNodeName(selectSingleNode.ChildNodes[i]);

							_progressObject.ProgressBarValue++;
							_worker.ReportProgress(-1, _progressObject);

							ImportXmlNodeValue(selectSingleNode.ChildNodes[i], NodeType.StoredProcedure, name);
							ImportXml(selectSingleNode.ChildNodes[i], "parameters", NodeType.StoredProcedureParameters, name, operationLabelText);
						}
						break;
					case NodeType.StoredProcedureParameters:
						{
							ImportXmlNodeValue(selectSingleNode.ChildNodes[i], NodeType.StoredProcedureParameter, parentNodeName);
						}
						break;
					case NodeType.TableValuedFunctions:
						{
							string name = GetXmlNodeName(selectSingleNode.ChildNodes[i]);

							_progressObject.ProgressBarValue++;
							_worker.ReportProgress(-1, _progressObject);

							ImportXmlNodeValue(selectSingleNode.ChildNodes[i], NodeType.TableValuedFunction, name);
							ImportXml(selectSingleNode.ChildNodes[i], "parameters", NodeType.TableValuedFunctionParameters, name, operationLabelText);
						}
						break;
					case NodeType.TableValuedFunctionParameters:
						{
							ImportXmlNodeValue(selectSingleNode.ChildNodes[i], NodeType.TableValuedFunctionParameter, parentNodeName);
						}
						break;
					case NodeType.ScalarValuedFunctions:
						{
							string name = GetXmlNodeName(selectSingleNode.ChildNodes[i]);

							_progressObject.ProgressBarValue++;
							_worker.ReportProgress(-1, _progressObject);

							ImportXmlNodeValue(selectSingleNode.ChildNodes[i], NodeType.ScalarValuedFunction, name);
							ImportXml(selectSingleNode.ChildNodes[i], "parameters", NodeType.ScalarValuedFunctionParameters, name, operationLabelText);
						}
						break;
					case NodeType.ScalarValuedFunctionParameters:
						{

							ImportXmlNodeValue(selectSingleNode.ChildNodes[i], NodeType.ScalarValuedFunctionParameter, parentNodeName);
						}
						break;
				}
			}
		}
	}

	private static string GetXmlNodeName(XmlNode xmlNode)
	{
		XmlAttributeCollection xmlAttributeCollection = xmlNode.Attributes;

		if (xmlAttributeCollection != null)
		{
			foreach (XmlAttribute xmlAttribute in xmlAttributeCollection)
			{
				if (xmlAttribute.Name == "name")
				{
					return xmlAttribute.Value;
				}
			}
		}

		return null;
	}

	private void ImportXmlNodeValue(XmlNode xmlNode, NodeType nodeType, string parentNodeName)
	{
		XmlAttributeCollection xmlAttributeCollection = xmlNode.Attributes;

		if (xmlAttributeCollection != null)
		{
			string name = null;

			foreach (XmlAttribute xmlAttribute in xmlAttributeCollection)
			{
				if (xmlAttribute.Name == "name")
				{
					name = xmlAttribute.Value;
				}
				else
				{
					string databaseFieldName = xmlAttribute.Name;
					string description = xmlAttribute.Value;

					SaveXmlValue(name, parentNodeName, nodeType, description, databaseFieldName);
				}
			}
		}
	}

	private void SaveXmlValue(string name, string parentNodeName, NodeType nodeType, string description, string databaseFieldName)
	{
		string level1Name = name;
		string level2Name = null;

		if (parentNodeName != null)
		{
			level1Name = parentNodeName;
			level2Name = name;
		}

		string level1Type = ExtendedPropertiesHelper.GetLevel1Type(nodeType);
		string level2Type = ExtendedPropertiesHelper.GetLevel2Type(nodeType);

		if (level2Type == "")
		{
			level2Name = "";
		}

		if (_objectsSelectorForm.allCheckBox.Checked || (!_objectsSelectorForm.allCheckBox.Checked && _selectedDatabaseFieldNames.Contains(databaseFieldName)))
		{
			if (_objectsSelectorForm.IncludeObjectType(level1Type, level2Type))
			{
				if (_objectsSelectorForm.IncludeIndividualObject(level1Type, level1Name))
				{
					_databaseOperation.SaveDescription(level1Type, level1Name, level2Type, level2Name, description, databaseFieldName);
				}
			}
		}
	}

	private void ImportNodeExcel()
	{
		DataTable dataTable = ExcelHelper.ReadFromExcelFile(_fileName, ConfigHandler.ExcelSheetName);

		ProgressObject progressObject = new ProgressObject();
		progressObject.ProgressBarMaximum = dataTable.Rows.Count;
		progressObject.OperationLabelText = "Importing...";

		for (int i = 1; i < dataTable.Rows.Count; i++)
		{
			progressObject.ProgressBarValue = i;
			_worker.ReportProgress(-1, progressObject);

			string level1Type = dataTable.Rows[i][0].ToString();
			string level1Name = dataTable.Rows[i][1].ToString();
			string level2Type = dataTable.Rows[i][2].ToString();
			string level2Name = dataTable.Rows[i][3].ToString();
			string databaseFieldName = dataTable.Rows[i][4].ToString();
			string description = dataTable.Rows[i][5].ToString();

			if (_objectsSelectorForm.allCheckBox.Checked || (!_objectsSelectorForm.allCheckBox.Checked && _selectedDatabaseFieldNames.Contains(databaseFieldName)))
			{
				if (_objectsSelectorForm.IncludeObjectType(level1Type, level2Type))
				{
					if (_objectsSelectorForm.IncludeIndividualObject(level1Type, level1Name))
					{
						_databaseOperation.SaveDescription(level1Type, level1Name, level2Type, level2Name, description, databaseFieldName);
					}
				}
			}
		}
	}

	private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
	{
		ProgressObject progressObject = (ProgressObject)e.UserState;
		operationLabel.Text = progressObject.OperationLabelText;

		if (progressObject.ProgressBarValue == -1)
		{
			statusLabel.Text = "Status: Loading...";
		}
		else
		{
			progressBar1.Maximum = progressObject.ProgressBarMaximum;
			progressBar1.Value = progressObject.ProgressBarValue;
			statusLabel.Text = string.Format("Status: Importing ({0}/{1})", progressObject.ProgressBarValue, progressObject.ProgressBarMaximum);
		}
	}

	private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		_running = false;
		startButton.Enabled = true;
		cancelButton.Enabled = true;
		databaseComboBox.Enabled = true;
		fileFormatComboBox.Enabled = true;
		selectObjectsButton.Enabled = true;

		timer1.Stop();
		_sw.Stop();

		if (_activeDatabaseBeforeOperation != "")
		{
			_databaseOperation.SetDatabase(_activeDatabaseBeforeOperation);
		}

		progressBar1.Maximum = 1;
		progressBar1.Value = 1;
		statusLabel.Text = "Status: Completed";
		operationLabel.Text = "";
	}

	private class ProgressObject
	{
		public int ProgressBarMaximum;
		public int ProgressBarValue;
		public string OperationLabelText;
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

	private void ImportForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (_running)
		{
			MessageBox.Show("Can't close while importing.", ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			e.Cancel = true;
		}
	}

	private void FileFormatComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (fileFormatComboBox.SelectedItem.ToString() == "Excel")
		{
			openFileDialog1.Filter = "Excel 97-2003 Workbook|*.xls";
			openFileDialog1.DefaultExt = "xls";
		}
		else if (fileFormatComboBox.SelectedItem.ToString() == "Xml")
		{
			openFileDialog1.Filter = "Xml files|*.xml";
			openFileDialog1.DefaultExt = "xml";
		}

		_objectsSelectorForm.SetIncludeParentObject(false);
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
