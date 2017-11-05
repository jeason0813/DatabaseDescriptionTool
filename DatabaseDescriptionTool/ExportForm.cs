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
using System.Text;
using System.Windows.Forms;
using System.Xml;

public partial class ExportForm : Form
{
	private string _activeDatabaseBeforeOperation;
	private readonly DatabaseOperation _databaseOperation;
	private BackgroundWorker _worker;
	private readonly Stopwatch _sw = new Stopwatch();
	private bool _running;
	private string _fileName;
	private bool _exportParentObject;
	private List<string> _selectedDatabaseFieldNames;
	private string _fileFormat;
	private readonly ObjectsSelectorForm _objectsSelectorForm;
	private string _individualObjectsInitializedForDatabase;

	public ExportForm(DatabaseOperation databaseOperation)
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
		fileFormatComboBox.Items.Add("Sql");
		fileFormatComboBox.Items.Add("Xml");
		fileFormatComboBox.Items.Add("Excel");

		fileFormatComboBox.SelectedItem = "Sql";
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
		_fileFormat = saveFileDialog1.DefaultExt;

		string dialogFileName = Path.GetExtension(saveFileDialog1.FileName);

		if (dialogFileName != null && dialogFileName.ToLower() != string.Format(".{0}", _fileFormat.ToLower()))
		{
			saveFileDialog1.FileName = "";
		}
	}

	private void startButton_Click(object sender, EventArgs e)
	{
		FixDialogFileNameExtension();

		DialogResult result = saveFileDialog1.ShowDialog();

		if (result.ToString() == "OK")
		{
			Application.DoEvents();
			_fileName = saveFileDialog1.FileName;
			Start();
		}
	}

	private void Start()
	{
		_running = true;
		startButton.Enabled = false;
		cancelButton.Enabled = false;
		databaseComboBox.Enabled = false;
		fileFormatComboBox.Enabled = false;
		selectObjectsButton.Enabled = false;
		additionalOptionsCheckBox.Enabled = false;
		generateGoStatementsCheckBox.Enabled = false;

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
		if (File.Exists(_fileName))
		{
			File.Delete(_fileName);
		}

		string extension = saveFileDialog1.DefaultExt;

		if (extension != null)
		{
			extension = extension.ToLower();
		}

		CustomNode serverNode = GenerateServerNode();

		ProgressObject progressObject = new ProgressObject();
		progressObject.OperationLabelText = "";
		progressObject.ProgressBarValue = -1;
		_worker.ReportProgress(-1, progressObject);

		if (extension == "xml" || extension == "sql")
		{
			StringBuilder stringBuilder = new StringBuilder();
			WriteHeader(stringBuilder, extension);
			ExportNode(serverNode, stringBuilder, extension);
			WriteFile(stringBuilder, extension);
		}
		else if (extension == "xls")
		{
			List<string> dataRows = new List<string>();
			ExcelHelper.CreateExcelFile(_fileName, ConfigHandler.ExcelSheetName, GetExcelHeaderRow());
			ExportNode(serverNode, dataRows, extension);
			WriteFile(dataRows, extension);
		}
	}

	private static void WriteHeader(StringBuilder stringBuilder, string extension)
	{
		if (extension == "xml")
		{
			stringBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
		}
	}

	private void WriteFile(StringBuilder stringBuilder, string extension)
	{
		if (extension == "xml")
		{
			WriteXml(stringBuilder);
		}
		else if (extension == "sql")
		{
			File.AppendAllText(_fileName, stringBuilder.ToString(), Encoding.UTF8);
		}
	}

	private void WriteFile(List<string> dataRows, string extension)
	{
		if (extension == "xls")
		{
			ExcelHelper.AppendToExcel(_fileName, ConfigHandler.ExcelSheetName, GetExcelHeaderRow(), dataRows);
		}
	}

	private void WriteXml(StringBuilder stringBuilder)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(stringBuilder.ToString());

		XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//*[. = '' and count(*) = 0]");

		if (xmlNodeList != null)
		{
			foreach (XmlElement xmlElement in xmlNodeList)
			{
				xmlElement.IsEmpty = true;
			}
		}

		XmlTextWriter xmlTextWriter = new XmlTextWriter(_fileName, Encoding.UTF8);
		xmlTextWriter.IndentChar = '\t';
		xmlTextWriter.Indentation = 1;
		xmlTextWriter.Formatting = Formatting.Indented;
		xmlDocument.WriteContentTo(xmlTextWriter);

		xmlTextWriter.Flush();
		xmlTextWriter.Close();
	}

	private void ExportNode(CustomNode serverNode, StringBuilder stringBuilder, string extension)
	{
		if (extension == "xml")
		{
			ExportNodeXml(serverNode, stringBuilder);
		}
		else if (extension == "sql")
		{
			ExportNodeSql(serverNode, stringBuilder);
		}
	}

	private void ExportNode(CustomNode serverNode, List<string> dataRow, string extension)
	{
		if (extension == "xls")
		{
			ExportNodeExcel(serverNode, dataRow);
		}
	}

	private static string GetExcelHeaderRow()
	{
		return "Level1type, Level1name, Level2type, Level2name, DatabaseFieldName, Description";
	}

	private void ExportNodeExcel(CustomNode node, List<string> dataRows)
	{
		foreach (CustomNode childNode in node.Nodes)
		{
			if (childNode.DescriptionChangeable)
			{
				if (_objectsSelectorForm.IncludeObjectType(node.Type))
				{
					if (_objectsSelectorForm.IncludeIndividualObject(childNode))
					{
						DoExportNodeExcel(childNode, dataRows);
					}
				}
			}

			ExportNodeExcel(childNode, dataRows);
		}
	}

	private void DoExportNodeExcel(CustomNode node, List<string> dataRows)
	{
		SortDescriptions(node);
		AddEmptyDescriptions(node);

		foreach (DescriptionItem item in node.Descriptions)
		{
			if (_objectsSelectorForm.allCheckBox.Checked || (!_objectsSelectorForm.allCheckBox.Checked && _selectedDatabaseFieldNames.Contains(item.Type.DatabaseFieldName)))
			{
				if (!additionalOptionsCheckBox.Checked || (additionalOptionsCheckBox.Checked && !string.IsNullOrEmpty(item.Description)))
				{
					dataRows.Add(ExtendedPropertiesHelper.GetExcelData(node, item.Description, item.Type.DatabaseFieldName));
				}
			}
		}
	}

	private void ExportNodeSql(CustomNode node, StringBuilder stringBuilder)
	{
		foreach (CustomNode childNode in node.Nodes)
		{
			if (childNode.DescriptionChangeable)
			{
				if (_objectsSelectorForm.IncludeObjectType(node.Type))
				{
					if (_objectsSelectorForm.IncludeIndividualObject(childNode))
					{
						DoExportNodeSql(childNode, stringBuilder);
					}
				}
			}

			ExportNodeSql(childNode, stringBuilder);
		}
	}

	private void DoExportNodeSql(CustomNode node, StringBuilder stringBuilder)
	{
		SortDescriptions(node);
		AddEmptyDescriptions(node);

		foreach (DescriptionItem item in node.Descriptions)
		{
			string sql = "";

			if (_objectsSelectorForm.allCheckBox.Checked || (!_objectsSelectorForm.allCheckBox.Checked && _selectedDatabaseFieldNames.Contains(item.Type.DatabaseFieldName)))
			{
				string databaseFieldName = item.Type.DatabaseFieldName;

				if (string.IsNullOrEmpty(item.Description))
				{
					if (additionalOptionsCheckBox.Checked)
					{
						sql = string.Format("if exists ({0})\r\nbegin\r\n\t{1}\r\nend", ExtendedPropertiesHelper.GetCheckSql(node, databaseFieldName), ExtendedPropertiesHelper.GetDropSql(node, databaseFieldName));
					}
				}
				else
				{
					sql = string.Format("if exists ({0})\r\nbegin\r\n\t{1}\r\nend\r\nelse\r\nbegin\r\n\t{2}\r\nend", ExtendedPropertiesHelper.GetCheckSql(node, databaseFieldName), ExtendedPropertiesHelper.GetUpdateSql(node, item.Description, databaseFieldName), ExtendedPropertiesHelper.GetAddSql(node, item.Description, databaseFieldName));
				}
			}

			if (sql != "")
			{
				sql = CheckWrapper.GetCheckWrapper(node, sql);

				if (generateGoStatementsCheckBox.Checked)
				{
					sql = string.Format("{0}\r\ngo", sql);
				}

				stringBuilder.Append(string.Format("{0}\r\n\r\n", sql));
			}
		}
	}

	private void ExportNodeXml(CustomNode rootNode, StringBuilder stringBuilder)
	{
		DoExportNodeXml(rootNode, stringBuilder);
	}

	private void DoExportNodeXml(CustomNode node, StringBuilder stringBuilder)
	{
		foreach (CustomNode childNode in node.Nodes)
		{
			if (childNode.Nodes.Count > 0)
			{
				if (_objectsSelectorForm.IncludeObjectType(childNode.Type))
				{
					if (_objectsSelectorForm.IncludeIndividualObject(childNode))
					{
						if (childNode.DescriptionChangeable)
						{
							stringBuilder.Append(string.Format("<{0} name=\"{1}\"{2}>", GetXmlNodeType(childNode.Type), childNode.Text, GetXmlDescription(childNode)));
						}
						else
						{
							stringBuilder.Append(string.Format("<{0}>", GetXmlNodeType(childNode.Type)));
						}
					}
				}

				DoExportNodeXml(childNode, stringBuilder);

				if (_objectsSelectorForm.IncludeObjectType(childNode.Type))
				{
					if (_objectsSelectorForm.IncludeIndividualObject(childNode))
					{
						stringBuilder.Append(string.Format("</{0}>", GetXmlNodeType(childNode.Type)));
					}
				}
			}
			else
			{
				if (_objectsSelectorForm.IncludeObjectType(childNode.Type))
				{
					if (_objectsSelectorForm.IncludeIndividualObject(childNode))
					{
						if (childNode.DescriptionChangeable)
						{
							stringBuilder.Append(string.Format("<{0} name=\"{1}\"{2} />", GetXmlNodeType(childNode.Type), childNode.Text, GetXmlDescription(childNode)));
						}
						else
						{
							stringBuilder.Append(string.Format("<{0} />", GetXmlNodeType(childNode.Type)));
						}
					}
				}
			}
		}
	}

	private static string GetXmlNodeType(NodeType nodeType)
	{
		switch (nodeType)
		{
			case NodeType.TableIndexes:
				return "indexes";
			case NodeType.TableIndex:
				return "index";
			case NodeType.TableTriggers:
				return "triggers";
			case NodeType.TableTrigger:
				return "trigger";
			case NodeType.TableKeys:
				return "keys";
			case NodeType.TableKey:
				return "key";
			case NodeType.TableConstraints:
				return "constraints";
			case NodeType.TableConstraint:
				return "constraint";
			case NodeType.TableColumns:
				return "columns";
			case NodeType.TableColumn:
				return "column";
			case NodeType.ViewIndexes:
				return "indexes";
			case NodeType.ViewIndex:
				return "index";
			case NodeType.ViewTriggers:
				return "triggers";
			case NodeType.ViewTrigger:
				return "trigger";
			case NodeType.ViewColumns:
				return "columns";
			case NodeType.ViewColumn:
				return "column";
			case NodeType.StoredProcedureParameters:
				return "parameters";
			case NodeType.StoredProcedureParameter:
				return "parameter";
			case NodeType.TableValuedFunctionParameters:
				return "parameters";
			case NodeType.TableValuedFunctionParameter:
				return "parameter";
			case NodeType.ScalarValuedFunctionParameters:
				return "parameters";
			case NodeType.ScalarValuedFunctionParameter:
				return "parameter";
		}

		return nodeType.ToString().ToLower();
	}

	private string GetXmlDescription(CustomNode node)
	{
		SortDescriptions(node);
		AddEmptyDescriptions(node);

		string xml = "";

		foreach (DescriptionItem item in node.Descriptions)
		{
			if (_objectsSelectorForm.allCheckBox.Checked || (!_objectsSelectorForm.allCheckBox.Checked && _selectedDatabaseFieldNames.Contains(item.Type.DatabaseFieldName)))
			{
				xml += string.Format(" {0}=\"{1}\"", System.Security.SecurityElement.Escape(item.Type.DatabaseFieldName), System.Security.SecurityElement.Escape(item.Description));
			}
		}

		return xml;
	}

	private static void SortDescriptions(CustomNode node)
	{
		List<DescriptionItem> sortedList = new List<DescriptionItem>();

		foreach (DescriptionField item in ConfigHandler.DescriptionFields)
		{
			foreach (DescriptionItem nodeItem in node.Descriptions)
			{
				if (item.DatabaseFieldName == nodeItem.Type.DatabaseFieldName)
				{
					sortedList.Add(nodeItem);
				}
			}
		}

		node.Descriptions = sortedList;
	}

	private static void AddEmptyDescriptions(CustomNode node)
	{
		List<DescriptionField> types = ConfigHandler.DescriptionFields;

		foreach (DescriptionField type in types)
		{
			bool itemCreated = false;

			foreach (DescriptionItem createdFieldName in node.Descriptions)
			{
				if (createdFieldName.Type.DatabaseFieldName == type.DatabaseFieldName)
				{
					if (createdFieldName.Description == null)
					{
						createdFieldName.Description = "";
					}

					itemCreated = true;
					break;
				}
			}

			if (!itemCreated)
			{
				DescriptionItem item = new DescriptionItem(type.DisplayText, type.DatabaseFieldName, type.UseForImage, type.Information);
				item.Description = "";
				node.Descriptions.Add(item);
			}
		}
	}

	private int GetTotalCount()
	{
		int totalCount = 0;

		if (_objectsSelectorForm.IncludeObjectType(NodeType.TableColumns))
		{
			totalCount++;
		}

		if (_objectsSelectorForm.IncludeObjectType(NodeType.TableKeys))
		{
			totalCount++;
		}

		if (_objectsSelectorForm.IncludeObjectType(NodeType.TableConstraints))
		{
			totalCount++;
		}

		if (_objectsSelectorForm.IncludeObjectType(NodeType.TableTriggers))
		{
			totalCount++;
		}

		if (_objectsSelectorForm.IncludeObjectType(NodeType.TableIndexes))
		{
			totalCount++;
		}

		if (_objectsSelectorForm.IncludeObjectType(NodeType.ViewColumns))
		{
			totalCount++;
		}

		if (_objectsSelectorForm.IncludeObjectType(NodeType.ViewTriggers))
		{
			totalCount++;
		}

		if (_objectsSelectorForm.IncludeObjectType(NodeType.ViewIndexes))
		{
			totalCount++;
		}

		if (_objectsSelectorForm.IncludeObjectType(NodeType.StoredProcedureParameters))
		{
			totalCount++;
		}

		if (_objectsSelectorForm.IncludeObjectType(NodeType.TableValuedFunctionParameters))
		{
			totalCount++;
		}

		if (_objectsSelectorForm.IncludeObjectType(NodeType.ScalarValuedFunctionParameters))
		{
			totalCount++;
		}

		return totalCount;
	}

	private CustomNode GenerateServerNode()
	{
		int count = 0;
		int totalCount = GetTotalCount();

		CustomNode serverNode = new CustomNode(NodeType.Server, null, ConfigHandler.ServerName, NodeImage.ServerDatabase, 0);
		NodeDataGenerator.GenerateDatabaseNode(serverNode, _databaseOperation.GetDatabaseName(), 0);

		if (_objectsSelectorForm.IncludeObjectType(NodeType.Tables) || ((_fileFormat == "sql" || _fileFormat == "xls") && (_objectsSelectorForm.tableColumnsCheckBox.Checked || _objectsSelectorForm.tableKeysCheckBox.Checked || _objectsSelectorForm.tableConstraintsCheckBox.Checked || _objectsSelectorForm.tableTriggersCheckBox.Checked || _objectsSelectorForm.tableIndexesCheckBox.Checked)))
		{
			CustomNode tablesNode = (CustomNode)serverNode.Nodes[0].Nodes["Tables"];
			NodeDataGenerator.GenerateTablesNode(tablesNode, _databaseOperation);

			if (_objectsSelectorForm.IncludeObjectType(NodeType.TableColumns))
			{
				ExportTableColumns(tablesNode, ++count, totalCount);
			}

			if (_objectsSelectorForm.IncludeObjectType(NodeType.TableKeys))
			{
				ExportTableKeys(tablesNode, ++count, totalCount);
			}

			if (_objectsSelectorForm.IncludeObjectType(NodeType.TableConstraints))
			{
				ExportTableConstraints(tablesNode, ++count, totalCount);
			}

			if (_objectsSelectorForm.IncludeObjectType(NodeType.TableTriggers))
			{
				ExportTableTriggers(tablesNode, ++count, totalCount);
			}

			if (_objectsSelectorForm.IncludeObjectType(NodeType.TableIndexes))
			{
				ExportTableIndexes(tablesNode, ++count, totalCount);
			}
		}

		if (_objectsSelectorForm.IncludeObjectType(NodeType.Views) || ((_fileFormat == "sql" || _fileFormat == "xls") && (_objectsSelectorForm.viewColumnsCheckBox.Checked || _objectsSelectorForm.viewTriggersCheckBox.Checked || _objectsSelectorForm.viewIndexesCheckBox.Checked)))
		{
			CustomNode viewsNode = (CustomNode)serverNode.Nodes[0].Nodes["Views"];
			NodeDataGenerator.GenerateViewsNode(viewsNode, _databaseOperation);

			if (_objectsSelectorForm.IncludeObjectType(NodeType.ViewColumns))
			{
				ExportViewColumns(viewsNode, ++count, totalCount);
			}

			if (_objectsSelectorForm.IncludeObjectType(NodeType.ViewTriggers))
			{
				ExportViewTriggers(viewsNode, ++count, totalCount);
			}

			if (_objectsSelectorForm.IncludeObjectType(NodeType.ViewIndexes))
			{
				ExportViewIndexes(viewsNode, ++count, totalCount);
			}
		}

		if (_objectsSelectorForm.IncludeObjectType(NodeType.StoredProcedures) || ((_fileFormat == "sql" || _fileFormat == "xls") && (_objectsSelectorForm.storedProcedureParametersCheckBox.Checked)))
		{
			CustomNode proceduresNode = (CustomNode)serverNode.Nodes[0].Nodes["Programmability"].Nodes["Stored Procedures"];
			NodeDataGenerator.GenerateProceduresNode(proceduresNode, _databaseOperation);

			if (_objectsSelectorForm.IncludeObjectType(NodeType.StoredProcedureParameters))
			{
				ExportStoredProcedureParameters(proceduresNode, ++count, totalCount);
			}
		}

		if (_objectsSelectorForm.IncludeObjectType(NodeType.TableValuedFunctions) || ((_fileFormat == "sql" || _fileFormat == "xls") && (_objectsSelectorForm.tableValuedFunctionParametersCheckBox.Checked)))
		{
			CustomNode tableValuedFunctionsNode = (CustomNode)serverNode.Nodes[0].Nodes["Programmability"].Nodes["Functions"].Nodes["Table-valued Functions"];
			NodeDataGenerator.GenerateTableValuedFunctionsNode(tableValuedFunctionsNode, _databaseOperation);

			if (_objectsSelectorForm.IncludeObjectType(NodeType.TableValuedFunctionParameters))
			{
				ExportTableValuedFunctionParameters(tableValuedFunctionsNode, ++count, totalCount);
			}
		}

		if (_objectsSelectorForm.IncludeObjectType(NodeType.ScalarValuedFunctions) || ((_fileFormat == "sql" || _fileFormat == "xls") && (_objectsSelectorForm.scalarValuedFunctionParametersCheckBox.Checked)))
		{
			CustomNode scalarValuedFunctionsNode = (CustomNode)serverNode.Nodes[0].Nodes["Programmability"].Nodes["Functions"].Nodes["Scalar-valued Functions"];
			NodeDataGenerator.GenerateScalarValuedFunctionsNode(scalarValuedFunctionsNode, _databaseOperation);

			if (_objectsSelectorForm.IncludeObjectType(NodeType.ScalarValuedFunctionParameters))
			{
				ExportScalarValuedFunctionParameters(scalarValuedFunctionsNode, ++count, totalCount);
			}
		}

		return serverNode;
	}

	private void ExportTableColumns(CustomNode tablesNode, int count, int totalCount)
	{
		ProgressObject progressObject = new ProgressObject();
		progressObject.ProgressBarMaximum = tablesNode.Nodes.Count;
		progressObject.OperationLabelText = string.Format("Exporting Table Columns ({0}/{1})", count, totalCount);

		for (int i = 0; i < tablesNode.Nodes.Count; i++)
		{
			progressObject.ProgressBarValue = i + 1;
			_worker.ReportProgress(-1, progressObject);
			CustomNode node = (CustomNode)tablesNode.Nodes[i];

			if (_objectsSelectorForm.IncludeIndividualObject(node))
			{
				NodeDataGenerator.GenerateTableColumnsNode((CustomNode)node.Nodes["Columns"], node.Text, _databaseOperation);
			}
		}
	}

	private void ExportTableKeys(CustomNode tablesNode, int count, int totalCount)
	{
		ProgressObject progressObject = new ProgressObject();
		progressObject.ProgressBarMaximum = tablesNode.Nodes.Count;
		progressObject.OperationLabelText = string.Format("Exporting Table Keys ({0}/{1})", count, totalCount);

		for (int i = 0; i < tablesNode.Nodes.Count; i++)
		{
			progressObject.ProgressBarValue = i + 1;
			_worker.ReportProgress(-1, progressObject);
			CustomNode node = (CustomNode)tablesNode.Nodes[i];

			if (_objectsSelectorForm.IncludeIndividualObject(node))
			{
				NodeDataGenerator.GenerateTableKeysNode((CustomNode)node.Nodes["Keys"], node.Text, _databaseOperation);
			}
		}
	}

	private void ExportTableConstraints(CustomNode tablesNode, int count, int totalCount)
	{
		ProgressObject progressObject = new ProgressObject();
		progressObject.ProgressBarMaximum = tablesNode.Nodes.Count;
		progressObject.OperationLabelText = string.Format("Exporting Table Constraints ({0}/{1})", count, totalCount);

		for (int i = 0; i < tablesNode.Nodes.Count; i++)
		{
			progressObject.ProgressBarValue = i + 1;
			_worker.ReportProgress(-1, progressObject);
			CustomNode node = (CustomNode)tablesNode.Nodes[i];

			if (_objectsSelectorForm.IncludeIndividualObject(node))
			{
				NodeDataGenerator.GenerateTableConstraintsNode((CustomNode)node.Nodes["Constraints"], node.Text, _databaseOperation);
			}
		}
	}

	private void ExportTableTriggers(CustomNode tablesNode, int count, int totalCount)
	{
		ProgressObject progressObject = new ProgressObject();
		progressObject.ProgressBarMaximum = tablesNode.Nodes.Count;
		progressObject.OperationLabelText = string.Format("Exporting Table Triggers ({0}/{1})", count, totalCount);

		for (int i = 0; i < tablesNode.Nodes.Count; i++)
		{
			progressObject.ProgressBarValue = i + 1;
			_worker.ReportProgress(-1, progressObject);
			CustomNode node = (CustomNode)tablesNode.Nodes[i];

			if (_objectsSelectorForm.IncludeIndividualObject(node))
			{
				NodeDataGenerator.GenerateTableTriggersNode((CustomNode)node.Nodes["Triggers"], node.Text, _databaseOperation);
			}
		}
	}

	private void ExportTableIndexes(CustomNode tablesNode, int count, int totalCount)
	{
		ProgressObject progressObject = new ProgressObject();
		progressObject.ProgressBarMaximum = tablesNode.Nodes.Count;
		progressObject.OperationLabelText = string.Format("Exporting Table Indexes ({0}/{1})", count, totalCount);

		for (int i = 0; i < tablesNode.Nodes.Count; i++)
		{
			progressObject.ProgressBarValue = i + 1;
			_worker.ReportProgress(-1, progressObject);
			CustomNode node = (CustomNode)tablesNode.Nodes[i];

			if (_objectsSelectorForm.IncludeIndividualObject(node))
			{
				NodeDataGenerator.GenerateTableIndexesNode((CustomNode)node.Nodes["Indexes"], node.Text, _databaseOperation);
			}
		}
	}

	private void ExportViewColumns(CustomNode viewsNode, int count, int totalCount)
	{
		ProgressObject progressObject = new ProgressObject();
		progressObject.ProgressBarMaximum = viewsNode.Nodes.Count;
		progressObject.OperationLabelText = string.Format("Exporting View Columns ({0}/{1})", count, totalCount);

		for (int i = 0; i < viewsNode.Nodes.Count; i++)
		{
			progressObject.ProgressBarValue = i + 1;
			_worker.ReportProgress(-1, progressObject);
			CustomNode node = (CustomNode)viewsNode.Nodes[i];

			if (_objectsSelectorForm.IncludeIndividualObject(node))
			{
				NodeDataGenerator.GenerateViewColumnsNode((CustomNode)node.Nodes["Columns"], node.Text, _databaseOperation);
			}
		}
	}

	private void ExportViewTriggers(CustomNode viewsNode, int count, int totalCount)
	{
		ProgressObject progressObject = new ProgressObject();
		progressObject.ProgressBarMaximum = viewsNode.Nodes.Count;
		progressObject.OperationLabelText = string.Format("Exporting View Triggers ({0}/{1})", count, totalCount);

		for (int i = 0; i < viewsNode.Nodes.Count; i++)
		{
			progressObject.ProgressBarValue = i + 1;
			_worker.ReportProgress(-1, progressObject);
			CustomNode node = (CustomNode)viewsNode.Nodes[i];

			if (_objectsSelectorForm.IncludeIndividualObject(node))
			{
				NodeDataGenerator.GenerateViewTriggersNode((CustomNode)node.Nodes["Triggers"], node.Text, _databaseOperation);
			}
		}
	}

	private void ExportViewIndexes(CustomNode viewsNode, int count, int totalCount)
	{
		ProgressObject progressObject = new ProgressObject();
		progressObject.ProgressBarMaximum = viewsNode.Nodes.Count;
		progressObject.OperationLabelText = string.Format("Exporting View Indexes ({0}/{1})", count, totalCount);

		for (int i = 0; i < viewsNode.Nodes.Count; i++)
		{
			progressObject.ProgressBarValue = i + 1;
			_worker.ReportProgress(-1, progressObject);
			CustomNode node = (CustomNode)viewsNode.Nodes[i];

			if (_objectsSelectorForm.IncludeIndividualObject(node))
			{
				NodeDataGenerator.GenerateViewIndexesNode((CustomNode)node.Nodes["Indexes"], node.Text, _databaseOperation);
			}
		}
	}

	private void ExportStoredProcedureParameters(CustomNode proceduresNode, int count, int totalCount)
	{
		ProgressObject progressObject = new ProgressObject();
		progressObject.ProgressBarMaximum = proceduresNode.Nodes.Count;
		progressObject.OperationLabelText = string.Format("Exporting Stored Procedure Parameters ({0}/{1})", count, totalCount);

		for (int i = 0; i < proceduresNode.Nodes.Count; i++)
		{
			progressObject.ProgressBarValue = i + 1;
			_worker.ReportProgress(-1, progressObject);
			CustomNode node = (CustomNode)proceduresNode.Nodes[i];

			if (_objectsSelectorForm.IncludeIndividualObject(node))
			{
				NodeDataGenerator.GenerateStoredProcedureParametersNode((CustomNode)node.Nodes["Parameters"], node.Text, _databaseOperation);
			}
		}
	}

	private void ExportTableValuedFunctionParameters(CustomNode functionsNode, int count, int totalCount)
	{
		ProgressObject progressObject = new ProgressObject();
		progressObject.ProgressBarMaximum = functionsNode.Nodes.Count;
		progressObject.OperationLabelText = string.Format("Exporting Table-valued Functions Parameters ({0}/{1})", count, totalCount);

		for (int i = 0; i < functionsNode.Nodes.Count; i++)
		{
			progressObject.ProgressBarValue = i + 1;
			_worker.ReportProgress(-1, progressObject);
			CustomNode node = (CustomNode)functionsNode.Nodes[i];

			if (_objectsSelectorForm.IncludeIndividualObject(node))
			{
				NodeDataGenerator.GenerateTableValuedFunctionParametersNode((CustomNode)node.Nodes["Parameters"], node.Text, _databaseOperation);
			}
		}
	}

	private void ExportScalarValuedFunctionParameters(CustomNode functionsNode, int count, int totalCount)
	{
		ProgressObject progressObject = new ProgressObject();
		progressObject.ProgressBarMaximum = functionsNode.Nodes.Count;
		progressObject.OperationLabelText = string.Format("Exporting Scalar-valued Functions Parameters ({0}/{1})", count, totalCount);

		for (int i = 0; i < functionsNode.Nodes.Count; i++)
		{
			progressObject.ProgressBarValue = i + 1;
			_worker.ReportProgress(-1, progressObject);
			CustomNode node = (CustomNode)functionsNode.Nodes[i];

			if (_objectsSelectorForm.IncludeIndividualObject(node))
			{
				NodeDataGenerator.GenerateScalarValuedFunctionParametersNode((CustomNode)node.Nodes["Parameters"], node.Text, _databaseOperation);
			}
		}
	}

	private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
	{
		ProgressObject progressObject = (ProgressObject)e.UserState;
		operationLabel.Text = progressObject.OperationLabelText;

		if (progressObject.ProgressBarValue == -1)
		{
			statusLabel.Text = "Status: Saving...";
		}
		else
		{
			progressBar1.Maximum = progressObject.ProgressBarMaximum;
			progressBar1.Value = progressObject.ProgressBarValue;
			statusLabel.Text = string.Format("Status: Exporting ({0}/{1})", progressObject.ProgressBarValue, progressObject.ProgressBarMaximum);
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
		additionalOptionsCheckBox.Enabled = true;
		generateGoStatementsCheckBox.Enabled = true;

		timer1.Stop();
		_sw.Stop();

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

	private void ExportForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (_running)
		{
			MessageBox.Show("Can't close while exporting.", ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

	private void FileFormatComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (fileFormatComboBox.SelectedItem.ToString() == "Xml")
		{
			if (_objectsSelectorForm.tableColumnsCheckBox.Checked || _objectsSelectorForm.tableKeysCheckBox.Checked || _objectsSelectorForm.tableConstraintsCheckBox.Checked || _objectsSelectorForm.tableTriggersCheckBox.Checked || _objectsSelectorForm.tableIndexesCheckBox.Checked)
			{
				_objectsSelectorForm.tablesCheckBox.Checked = true;
			}

			if (_objectsSelectorForm.viewColumnsCheckBox.Checked || _objectsSelectorForm.viewTriggersCheckBox.Checked || _objectsSelectorForm.viewIndexesCheckBox.Checked)
			{
				_objectsSelectorForm.viewsCheckBox.Checked = true;
			}

			if (_objectsSelectorForm.storedProcedureParametersCheckBox.Checked)
			{
				_objectsSelectorForm.storedProceduresCheckBox.Checked = true;
			}

			if (_objectsSelectorForm.tableValuedFunctionParametersCheckBox.Checked)
			{
				_objectsSelectorForm.tableValuedFunctionsCheckBox.Checked = true;
			}

			if (_objectsSelectorForm.scalarValuedFunctionParametersCheckBox.Checked)
			{
				_objectsSelectorForm.scalarValuedFunctionsCheckBox.Checked = true;
			}

			_exportParentObject = true;
			saveFileDialog1.Filter = "Xml files|*.xml";
			saveFileDialog1.DefaultExt = "xml";
			additionalOptionsCheckBox.Visible = false;
			generateGoStatementsCheckBox.Visible = false;
		}
		else if (fileFormatComboBox.SelectedItem.ToString() == "Sql")
		{
			_exportParentObject = false;
			saveFileDialog1.Filter = "Sql files|*.sql";
			saveFileDialog1.DefaultExt = "sql";
			additionalOptionsCheckBox.Text = "Generate DROP statements for empty descriptions";
			additionalOptionsCheckBox.Visible = true;
			generateGoStatementsCheckBox.Visible = true;
		}
		else if (fileFormatComboBox.SelectedItem.ToString() == "Excel")
		{
			_exportParentObject = false;
			saveFileDialog1.Filter = "Excel 97-2003 Workbook|*.xls";
			saveFileDialog1.DefaultExt = "xls";
			additionalOptionsCheckBox.Text = "Do not export empty descriptions";
			additionalOptionsCheckBox.Visible = true;
			generateGoStatementsCheckBox.Visible = false;
		}

		_objectsSelectorForm.SetIncludeParentObject(_exportParentObject);
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
