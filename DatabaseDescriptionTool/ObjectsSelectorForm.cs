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

using System.Collections.Generic;
using System.Windows.Forms;

public partial class ObjectsSelectorForm : Form
{
	private bool _includeParentObject;
	private List<string> _selectedIndividualTables;
	private List<string> _selectedIndividualViews;
	private List<string> _selectedIndividualStoredProcedures;
	private List<string> _selectedIndividualTableValuedFunctions;
	private List<string> _selectedIndividualScalarValuedFunctions;
	ChooseIndividualObjectsForm _chooseIndividualScalarValuedFunctionsForm;
	ChooseIndividualObjectsForm _chooseIndividualTableValuedFunctionsForm;
	ChooseIndividualObjectsForm _chooseIndividualStoredProceduresForm;
	ChooseIndividualObjectsForm _chooseIndividualViewsForm;
	ChooseIndividualObjectsForm _chooseIndividualTablesForm;

	public ObjectsSelectorForm()
	{
		InitializeComponent();
		GenerateImageList(fieldNameListView);
		fieldNameListView.Columns[0].Width = fieldNameListView.Width - 4;
		GenerateFieldItems(fieldNameListView);
	}

	public void SetIncludeParentObject(bool value)
	{
		_includeParentObject = value;
	}

	public List<string> GetSelectedIndividualTables()
	{
		return _chooseIndividualTablesForm.GetSelectedIndividualObjects();
	}

	public List<string> GetSelectedIndividualViews()
	{
		return _chooseIndividualViewsForm.GetSelectedIndividualObjects();
	}

	public List<string> GetSelectedIndividualStoredProcedures()
	{
		return _chooseIndividualStoredProceduresForm.GetSelectedIndividualObjects();
	}

	public List<string> GetSelectedIndividualTableValuedFunctions()
	{
		return _chooseIndividualTableValuedFunctionsForm.GetSelectedIndividualObjects();
	}

	public List<string> GetSelectedIndividualScalarValuedFunctions()
	{
		return _chooseIndividualScalarValuedFunctionsForm.GetSelectedIndividualObjects();
	}

	public bool AnyCheckBoxCheckedIndividualTables()
	{
		return _chooseIndividualTablesForm.AnyCheckBoxChecked();
	}

	public bool AllCheckBoxCheckedIndividualTables()
	{
		return _chooseIndividualTablesForm.AllCheckBoxChecked();
	}

	public bool AnyCheckBoxCheckedIndividualViews()
	{
		return _chooseIndividualViewsForm.AnyCheckBoxChecked();
	}

	public bool AllCheckBoxCheckedIndividualViews()
	{
		return _chooseIndividualViewsForm.AllCheckBoxChecked();
	}

	public bool AnyCheckBoxCheckedIndividualStoredProcedures()
	{
		return _chooseIndividualStoredProceduresForm.AnyCheckBoxChecked();
	}

	public bool AllCheckBoxCheckedIndividualStoredProcedures()
	{
		return _chooseIndividualStoredProceduresForm.AllCheckBoxChecked();
	}

	public bool AnyCheckBoxCheckedIndividualTableValuedFunctions()
	{
		return _chooseIndividualTableValuedFunctionsForm.AnyCheckBoxChecked();
	}

	public bool AllCheckBoxCheckedIndividualTableValuedFunctions()
	{
		return _chooseIndividualTableValuedFunctionsForm.AllCheckBoxChecked();
	}

	public bool AnyCheckBoxCheckedIndividualScalarValuedFunctions()
	{
		return _chooseIndividualScalarValuedFunctionsForm.AnyCheckBoxChecked();
	}

	public bool AllCheckBoxCheckedIndividualScalarValuedFunctions()
	{
		return _chooseIndividualScalarValuedFunctionsForm.AllCheckBoxChecked();
	}

	public bool AnyCheckBoxChecked()
	{
		if (!tablesCheckBox.Checked && !tableColumnsCheckBox.Checked && !tableKeysCheckBox.Checked && !tableConstraintsCheckBox.Checked && !tableTriggersCheckBox.Checked && !tableIndexesCheckBox.Checked && !viewsCheckBox.Checked && !viewColumnsCheckBox.Checked && !viewTriggersCheckBox.Checked && !viewIndexesCheckBox.Checked && !storedProceduresCheckBox.Checked && !tableValuedFunctionsCheckBox.Checked && !scalarValuedFunctionsCheckBox.Checked && !storedProcedureParametersCheckBox.Checked && !tableValuedFunctionParametersCheckBox.Checked && !scalarValuedFunctionParametersCheckBox.Checked)
		{
			return false;
		}

		return true;
	}

	public bool AllCheckBoxChecked()
	{
		if (tablesCheckBox.Checked && tableColumnsCheckBox.Checked && tableKeysCheckBox.Checked && tableConstraintsCheckBox.Checked && tableTriggersCheckBox.Checked && tableIndexesCheckBox.Checked && viewsCheckBox.Checked && viewColumnsCheckBox.Checked && viewTriggersCheckBox.Checked && viewIndexesCheckBox.Checked && storedProceduresCheckBox.Checked && tableValuedFunctionsCheckBox.Checked && scalarValuedFunctionsCheckBox.Checked && storedProcedureParametersCheckBox.Checked && tableValuedFunctionParametersCheckBox.Checked && scalarValuedFunctionParametersCheckBox.Checked)
		{
			return true;
		}

		return false;
	}

	public bool IncludeIndividualObject(CustomNode node)
	{
		if (node.Type == NodeType.Server || node.Type == NodeType.Database || node.Type == NodeType.Tables || node.Type == NodeType.Views || node.Type == NodeType.StoredProcedures || node.Type == NodeType.TableValuedFunctions || node.Type == NodeType.ScalarValuedFunctions)
		{
			return true;
		}

		if (node.Type == NodeType.TableColumn || node.Type == NodeType.TableIndex || node.Type == NodeType.TableTrigger || node.Type == NodeType.TableConstraint || node.Type == NodeType.TableKey || node.Type == NodeType.ViewColumn || node.Type == NodeType.ViewIndex || node.Type == NodeType.ViewTrigger || node.Type == NodeType.StoredProcedureParameter || node.Type == NodeType.TableValuedFunctionParameter || node.Type == NodeType.ScalarValuedFunctionParameter)
		{
			return true;
		}

		if (node.Type == NodeType.Table || node.Type == NodeType.View || node.Type == NodeType.StoredProcedure || node.Type == NodeType.TableValuedFunction || node.Type == NodeType.ScalarValuedFunction)
		{
			return CheckIncludeIndividualObject(node);
		}
		else
		{
			return CheckIncludeIndividualObject(node.ParentNode);
		}
	}

	public bool IncludeIndividualObject(string level1Type, string name)
	{
		return CheckIncludeIndividualObject(level1Type, name);
	}

	public bool IncludeObjectType(NodeType nodeType)
	{
		if (nodeType == NodeType.Server && _includeParentObject)
		{
			return true;
		}
		else if (nodeType == NodeType.Database && _includeParentObject)
		{
			return true;
		}
		else if (nodeType == NodeType.Tables || nodeType == NodeType.Table)
		{
			if (tablesCheckBox.Checked || (_includeParentObject && (tableColumnsCheckBox.Checked || tableKeysCheckBox.Checked || tableConstraintsCheckBox.Checked || tableTriggersCheckBox.Checked || tableIndexesCheckBox.Checked)))
			{
				return true;
			}
		}
		else if (nodeType == NodeType.TableColumns && tableColumnsCheckBox.Checked)
		{
			return true;
		}
		else if (nodeType == NodeType.TableKeys && tableKeysCheckBox.Checked)
		{
			return true;
		}
		else if (nodeType == NodeType.TableConstraints && tableConstraintsCheckBox.Checked)
		{
			return true;
		}
		else if (nodeType == NodeType.TableTriggers && tableTriggersCheckBox.Checked)
		{
			return true;
		}
		else if (nodeType == NodeType.TableIndexes && tableIndexesCheckBox.Checked)
		{
			return true;
		}
		else if (nodeType == NodeType.Views || nodeType == NodeType.View)
		{
			if (viewsCheckBox.Checked || (_includeParentObject && (viewColumnsCheckBox.Checked || viewTriggersCheckBox.Checked || viewIndexesCheckBox.Checked)))
			{
				return true;
			}
		}
		else if (nodeType == NodeType.ViewColumns && viewColumnsCheckBox.Checked)
		{
			return true;
		}
		else if (nodeType == NodeType.ViewTriggers && viewTriggersCheckBox.Checked)
		{
			return true;
		}
		else if (nodeType == NodeType.ViewIndexes && viewIndexesCheckBox.Checked)
		{
			return true;
		}
		else if (nodeType == NodeType.StoredProcedures || nodeType == NodeType.StoredProcedure)
		{
			if (storedProceduresCheckBox.Checked || (_includeParentObject && (storedProcedureParametersCheckBox.Checked)))
			{
				return true;
			}
		}
		else if (nodeType == NodeType.StoredProcedureParameters && storedProcedureParametersCheckBox.Checked)
		{
			return true;
		}
		else if (nodeType == NodeType.TableValuedFunctions || nodeType == NodeType.TableValuedFunction)
		{
			if (tableValuedFunctionsCheckBox.Checked || (_includeParentObject && (tableValuedFunctionParametersCheckBox.Checked)))
			{
				return true;
			}
		}
		else if (nodeType == NodeType.TableValuedFunctionParameters && tableValuedFunctionParametersCheckBox.Checked)
		{
			return true;
		}
		else if (nodeType == NodeType.ScalarValuedFunctions || nodeType == NodeType.ScalarValuedFunction)
		{
			if (scalarValuedFunctionsCheckBox.Checked || (_includeParentObject && (scalarValuedFunctionParametersCheckBox.Checked)))
			{
				return true;
			}
		}
		else if (nodeType == NodeType.ScalarValuedFunctionParameters && scalarValuedFunctionParametersCheckBox.Checked)
		{
			return true;
		}
		else if (nodeType == NodeType.TableColumn && _includeParentObject && tableColumnsCheckBox.Checked)
		{
			return true;
		}
		else if (nodeType == NodeType.TableKey && _includeParentObject && tableKeysCheckBox.Checked)
		{
			return true;
		}
		else if (nodeType == NodeType.TableConstraint && _includeParentObject && tableConstraintsCheckBox.Checked)
		{
			return true;
		}
		else if (nodeType == NodeType.TableTrigger && _includeParentObject && tableTriggersCheckBox.Checked)
		{
			return true;
		}
		else if (nodeType == NodeType.TableIndex && _includeParentObject && tableIndexesCheckBox.Checked)
		{
			return true;
		}
		else if (nodeType == NodeType.ViewColumn && _includeParentObject && viewColumnsCheckBox.Checked)
		{
			return true;
		}
		else if (nodeType == NodeType.ViewTrigger && _includeParentObject && viewTriggersCheckBox.Checked)
		{
			return true;
		}
		else if (nodeType == NodeType.ViewIndex && _includeParentObject && viewIndexesCheckBox.Checked)
		{
			return true;
		}
		else if (nodeType == NodeType.StoredProcedureParameter && _includeParentObject && storedProcedureParametersCheckBox.Checked)
		{
			return true;
		}
		else if (nodeType == NodeType.TableValuedFunctionParameter && _includeParentObject && tableValuedFunctionParametersCheckBox.Checked)
		{
			return true;
		}
		else if (nodeType == NodeType.ScalarValuedFunctionParameter && _includeParentObject && scalarValuedFunctionParametersCheckBox.Checked)
		{
			return true;
		}

		return false;
	}

	public bool IncludeObjectType(string level1Type, string level2Type)
	{
		if (level1Type == "TABLE" && level2Type == "" && tablesCheckBox.Checked)
		{
			return true;
		}
		else if (level1Type == "TABLE" && level2Type == "COLUMN" && tableColumnsCheckBox.Checked)
		{
			return true;
		}
		else if (level1Type == "TABLE" && level2Type == "CONSTRAINT" && tableKeysCheckBox.Checked)
		{
			return true;
		}
		else if (level1Type == "TABLE" && level2Type == "CONSTRAINT" && tableConstraintsCheckBox.Checked)
		{
			return true;
		}
		else if (level1Type == "TABLE" && level2Type == "TRIGGER" && tableTriggersCheckBox.Checked)
		{
			return true;
		}
		else if (level1Type == "TABLE" && level2Type == "INDEX" && tableIndexesCheckBox.Checked)
		{
			return true;
		}
		else if (level1Type == "VIEW" && level2Type == "" && viewsCheckBox.Checked)
		{
			return true;
		}
		else if (level1Type == "VIEW" && level2Type == "COLUMN" && viewColumnsCheckBox.Checked)
		{
			return true;
		}
		else if (level1Type == "VIEW" && level2Type == "TRIGGER" && viewTriggersCheckBox.Checked)
		{
			return true;
		}
		else if (level1Type == "VIEW" && level2Type == "INDEX" && viewIndexesCheckBox.Checked)
		{
			return true;
		}
		else if (level1Type == "PROCEDURE" && level2Type == "" && storedProceduresCheckBox.Checked)
		{
			return true;
		}
		else if (level1Type == "PROCEDURE" && level2Type == "PARAMETER" && storedProcedureParametersCheckBox.Checked)
		{
			return true;
		}
		else if (level1Type == "FUNCTION" && level2Type == "" && tableValuedFunctionsCheckBox.Checked)
		{
			return true;
		}
		else if (level1Type == "FUNCTION" && level2Type == "PARAMETER" && tableValuedFunctionParametersCheckBox.Checked)
		{
			return true;
		}
		else if (level1Type == "FUNCTION" && level2Type == "" && scalarValuedFunctionsCheckBox.Checked)
		{
			return true;
		}
		else if (level1Type == "FUNCTION" && level2Type == "PARAMETER" && scalarValuedFunctionParametersCheckBox.Checked)
		{
			return true;
		}

		return false;
	}

	public void InitializeIndividualObjects(DatabaseOperation databaseOperation)
	{
		_chooseIndividualTablesForm = new ChooseIndividualObjectsForm(NodeType.Tables, databaseOperation, "Tables");
		_chooseIndividualViewsForm = new ChooseIndividualObjectsForm(NodeType.Views, databaseOperation, "Views");
		_chooseIndividualStoredProceduresForm = new ChooseIndividualObjectsForm(NodeType.StoredProcedures, databaseOperation, "Stored Procedures");
		_chooseIndividualTableValuedFunctionsForm = new ChooseIndividualObjectsForm(NodeType.TableValuedFunctions, databaseOperation, "Table-valued Functions");
		_chooseIndividualScalarValuedFunctionsForm = new ChooseIndividualObjectsForm(NodeType.ScalarValuedFunctions, databaseOperation, "Scalar-valued Functions");

		_selectedIndividualTables = _chooseIndividualTablesForm.GetSelectedIndividualObjects();
		_selectedIndividualViews = _chooseIndividualViewsForm.GetSelectedIndividualObjects();
		_selectedIndividualStoredProcedures = _chooseIndividualStoredProceduresForm.GetSelectedIndividualObjects();
		_selectedIndividualTableValuedFunctions = _chooseIndividualTableValuedFunctionsForm.GetSelectedIndividualObjects();
		_selectedIndividualScalarValuedFunctions = _chooseIndividualScalarValuedFunctionsForm.GetSelectedIndividualObjects();
	}

	public void SetIndividualObjects(CustomNode node)
	{
		if (node.Type == NodeType.Table || node.Type == NodeType.View || node.Type == NodeType.StoredProcedure || node.Type == NodeType.TableValuedFunction || node.Type == NodeType.ScalarValuedFunction || node.Type == NodeType.TableColumns || node.Type == NodeType.TableIndexes || node.Type == NodeType.TableTriggers || node.Type == NodeType.TableConstraints || node.Type == NodeType.TableKeys || node.Type == NodeType.ViewColumns || node.Type == NodeType.ViewTriggers || node.Type == NodeType.ViewIndexes || node.Type == NodeType.StoredProcedureParameters || node.Type == NodeType.TableValuedFunctionParameters || node.Type == NodeType.ScalarValuedFunctionParameters)
		{
			_selectedIndividualTables.Clear();
			_selectedIndividualViews.Clear();
			_selectedIndividualStoredProcedures.Clear();
			_selectedIndividualTableValuedFunctions.Clear();
			_selectedIndividualScalarValuedFunctions.Clear();
		}

		switch (node.Type)
		{
			case NodeType.Table:
				_selectedIndividualTables.Add(node.Name);
				EnableTablesOnly();
				break;
			case NodeType.View:
				_selectedIndividualViews.Add(node.Name);
				EnableViewsOnly();
				break;
			case NodeType.StoredProcedure:
				_selectedIndividualStoredProcedures.Add(node.Name);
				EnableStoredProceduresOnly();
				break;
			case NodeType.TableValuedFunction:
				_selectedIndividualTableValuedFunctions.Add(node.Name);
				EnableTableValuedFunctionsOnly();
				break;
			case NodeType.ScalarValuedFunction:
				_selectedIndividualScalarValuedFunctions.Add(node.Name);
				EnableScalarValuedFunctionsOnly();
				break;
			case NodeType.Tables:
				_selectedIndividualTables = _chooseIndividualTablesForm.GetSelectedIndividualObjects();
				_selectedIndividualViews.Clear();
				_selectedIndividualStoredProcedures.Clear();
				_selectedIndividualTableValuedFunctions.Clear();
				_selectedIndividualScalarValuedFunctions.Clear();
				EnableTablesOnly();
				break;
			case NodeType.Views:
				_selectedIndividualViews = _chooseIndividualViewsForm.GetSelectedIndividualObjects();
				_selectedIndividualTables.Clear();
				_selectedIndividualStoredProcedures.Clear();
				_selectedIndividualTableValuedFunctions.Clear();
				_selectedIndividualScalarValuedFunctions.Clear();
				EnableViewsOnly();
				break;
			case NodeType.StoredProcedures:
				_selectedIndividualStoredProcedures = _chooseIndividualStoredProceduresForm.GetSelectedIndividualObjects();
				_selectedIndividualTables.Clear();
				_selectedIndividualViews.Clear();
				_selectedIndividualTableValuedFunctions.Clear();
				_selectedIndividualScalarValuedFunctions.Clear();
				EnableStoredProceduresOnly();
				break;
			case NodeType.TableValuedFunctions:
				_selectedIndividualTableValuedFunctions = _chooseIndividualTableValuedFunctionsForm.GetSelectedIndividualObjects();
				_selectedIndividualTables.Clear();
				_selectedIndividualViews.Clear();
				_selectedIndividualStoredProcedures.Clear();
				_selectedIndividualScalarValuedFunctions.Clear();
				EnableTableValuedFunctionsOnly();
				break;
			case NodeType.ScalarValuedFunctions:
				_selectedIndividualScalarValuedFunctions = _chooseIndividualScalarValuedFunctionsForm.GetSelectedIndividualObjects();
				_selectedIndividualTables.Clear();
				_selectedIndividualViews.Clear();
				_selectedIndividualStoredProcedures.Clear();
				_selectedIndividualTableValuedFunctions.Clear();
				EnableScalarValuedFunctionsOnly();
				break;
			case NodeType.Programmability:
				_selectedIndividualTableValuedFunctions = _chooseIndividualTableValuedFunctionsForm.GetSelectedIndividualObjects();
				_selectedIndividualScalarValuedFunctions = _chooseIndividualScalarValuedFunctionsForm.GetSelectedIndividualObjects();
				_selectedIndividualStoredProcedures = _chooseIndividualStoredProceduresForm.GetSelectedIndividualObjects();
				_selectedIndividualTables.Clear();
				_selectedIndividualViews.Clear();
				EnableProgrammabilityOnly();
				break;
			case NodeType.Functions:
				_selectedIndividualTableValuedFunctions = _chooseIndividualTableValuedFunctionsForm.GetSelectedIndividualObjects();
				_selectedIndividualScalarValuedFunctions = _chooseIndividualScalarValuedFunctionsForm.GetSelectedIndividualObjects();
				_selectedIndividualTables.Clear();
				_selectedIndividualViews.Clear();
				_selectedIndividualStoredProcedures.Clear();
				EnableFunctionsOnly();
				break;
			case NodeType.TableColumns:
				_selectedIndividualTables.Add(node.ParentNode.Name);
				EnableTableColumnsOnly();
				break;
			case NodeType.TableIndexes:
				_selectedIndividualTables.Add(node.ParentNode.Name);
				EnableTableIndexesOnly();
				break;
			case NodeType.TableTriggers:
				_selectedIndividualTables.Add(node.ParentNode.Name);
				EnableTableTriggersOnly();
				break;
			case NodeType.TableConstraints:
				_selectedIndividualTables.Add(node.ParentNode.Name);
				EnableTableConstraintsOnly();
				break;
			case NodeType.TableKeys:
				_selectedIndividualTables.Add(node.ParentNode.Name);
				EnableTableKeysOnly();
				break;
			case NodeType.ViewColumns:
				_selectedIndividualViews.Add(node.ParentNode.Name);
				EnableViewColumnsOnly();
				break;
			case NodeType.ViewIndexes:
				_selectedIndividualViews.Add(node.ParentNode.Name);
				EnableViewIndexesOnly();
				break;
			case NodeType.ViewTriggers:
				_selectedIndividualViews.Add(node.ParentNode.Name);
				EnableViewTriggersOnly();
				break;
			case NodeType.StoredProcedureParameters:
				_selectedIndividualStoredProcedures.Add(node.ParentNode.Name);
				EnableStoredProcedureParametersOnly();
				break;
			case NodeType.TableValuedFunctionParameters:
				_selectedIndividualTableValuedFunctions.Add(node.ParentNode.Name);
				EnableTableValuedFunctionParametersOnly();
				break;
			case NodeType.ScalarValuedFunctionParameters:
				_selectedIndividualScalarValuedFunctions.Add(node.ParentNode.Name);
				EnableScalarValuedFunctionParametersOnly();
				break;
		}

		_chooseIndividualTablesForm.SetSelectedIndividualObjects(_selectedIndividualTables);
		_chooseIndividualViewsForm.SetSelectedIndividualObjects(_selectedIndividualViews);
		_chooseIndividualStoredProceduresForm.SetSelectedIndividualObjects(_selectedIndividualStoredProcedures);
		_chooseIndividualTableValuedFunctionsForm.SetSelectedIndividualObjects(_selectedIndividualTableValuedFunctions);
		_chooseIndividualScalarValuedFunctionsForm.SetSelectedIndividualObjects(_selectedIndividualScalarValuedFunctions);
	}

	private void EnableTableColumnsOnly()
	{
		DeselectAll();
		tableColumnsCheckBox.Checked = true;
	}

	private void EnableTableIndexesOnly()
	{
		DeselectAll();
		tableIndexesCheckBox.Checked = true;
	}

	private void EnableTableTriggersOnly()
	{
		DeselectAll();
		tableTriggersCheckBox.Checked = true;
	}

	private void EnableTableConstraintsOnly()
	{
		DeselectAll();
		tableConstraintsCheckBox.Checked = true;
	}

	private void EnableTableKeysOnly()
	{
		DeselectAll();
		tableKeysCheckBox.Checked = true;
	}

	private void EnableViewColumnsOnly()
	{
		DeselectAll();
		viewColumnsCheckBox.Checked = true;
	}

	private void EnableViewIndexesOnly()
	{
		DeselectAll();
		viewIndexesCheckBox.Checked = true;
	}

	private void EnableViewTriggersOnly()
	{
		DeselectAll();
		viewTriggersCheckBox.Checked = true;
	}

	private void EnableStoredProcedureParametersOnly()
	{
		DeselectAll();
		storedProcedureParametersCheckBox.Checked = true;
	}

	private void EnableTableValuedFunctionParametersOnly()
	{
		DeselectAll();
		tableValuedFunctionParametersCheckBox.Checked = true;
	}

	private void EnableScalarValuedFunctionParametersOnly()
	{
		DeselectAll();
		scalarValuedFunctionParametersCheckBox.Checked = true;
	}

	private void EnableTablesOnly()
	{
		DeselectAll();
		tablesCheckBox.Checked = true;
		tableColumnsCheckBox.Checked = true;
		tableKeysCheckBox.Checked = true;
		tableConstraintsCheckBox.Checked = true;
		tableTriggersCheckBox.Checked = true;
		tableIndexesCheckBox.Checked = true;
	}

	private void EnableViewsOnly()
	{
		DeselectAll();
		viewsCheckBox.Checked = true;
		viewColumnsCheckBox.Checked = true;
		viewTriggersCheckBox.Checked = true;
		viewIndexesCheckBox.Checked = true;
	}

	private void EnableStoredProceduresOnly()
	{
		DeselectAll();
		storedProceduresCheckBox.Checked = true;
		storedProcedureParametersCheckBox.Checked = true;
	}

	private void EnableTableValuedFunctionsOnly()
	{
		DeselectAll();
		tableValuedFunctionsCheckBox.Checked = true;
		tableValuedFunctionParametersCheckBox.Checked = true;
	}

	private void EnableScalarValuedFunctionsOnly()
	{
		DeselectAll();
		scalarValuedFunctionsCheckBox.Checked = true;
		scalarValuedFunctionParametersCheckBox.Checked = true;
	}

	private void EnableFunctionsOnly()
	{
		DeselectAll();
		scalarValuedFunctionsCheckBox.Checked = true;
		scalarValuedFunctionParametersCheckBox.Checked = true;
		tableValuedFunctionsCheckBox.Checked = true;
		tableValuedFunctionParametersCheckBox.Checked = true;
	}

	private void EnableProgrammabilityOnly()
	{
		DeselectAll();
		storedProceduresCheckBox.Checked = true;
		storedProcedureParametersCheckBox.Checked = true;
		scalarValuedFunctionsCheckBox.Checked = true;
		scalarValuedFunctionParametersCheckBox.Checked = true;
		tableValuedFunctionsCheckBox.Checked = true;
		tableValuedFunctionParametersCheckBox.Checked = true;
	}

	private bool CheckIncludeIndividualObject(CustomNode node)
	{
		switch (node.Type)
		{
			case NodeType.Table:
				if (_selectedIndividualTables.Contains(node.Name))
				{
					return true;
				}
				break;
			case NodeType.View:
				if (_selectedIndividualViews.Contains(node.Name))
				{
					return true;
				}
				break;
			case NodeType.StoredProcedure:
				if (_selectedIndividualStoredProcedures.Contains(node.Name))
				{
					return true;
				}
				break;
			case NodeType.TableValuedFunction:
				if (_selectedIndividualTableValuedFunctions.Contains(node.Name))
				{
					return true;
				}
				break;
			case NodeType.ScalarValuedFunction:
				if (_selectedIndividualScalarValuedFunctions.Contains(node.Name))
				{
					return true;
				}
				break;
		}

		return false;
	}

	private bool CheckIncludeIndividualObject(string level1Type, string name)
	{
		switch (level1Type)
		{
			case "TABLE":
				if (_selectedIndividualTables.Contains(name))
				{
					return true;
				}
				break;
			case "VIEW":
				if (_selectedIndividualViews.Contains(name))
				{
					return true;
				}
				break;
			case "PROCEDURE":
				if (_selectedIndividualStoredProcedures.Contains(name))
				{
					return true;
				}
				break;
			case "FUNCTION":
				if (_selectedIndividualTableValuedFunctions.Contains(name))
				{
					return true;
				}

				if (_selectedIndividualScalarValuedFunctions.Contains(name))
				{
					return true;
				}
				break;
		}

		return false;
	}

	private static void GenerateImageList(ListView listView)
	{
		ImageList imageList = new ImageList();
		imageList.Images.Add(DatabaseDescriptionTool.Properties.Resources.book);
		listView.SmallImageList = imageList;
	}

	private void AllCheckBox_CheckedChanged(object sender, System.EventArgs e)
	{
		fieldNameListView.Enabled = !allCheckBox.Checked;
	}

	private void TablesCheckBox_CheckedChanged(object sender, System.EventArgs e)
	{
		if (_includeParentObject && !tablesCheckBox.Checked)
		{
			tableColumnsCheckBox.Checked = false;
			tableKeysCheckBox.Checked = false;
			tableConstraintsCheckBox.Checked = false;
			tableTriggersCheckBox.Checked = false;
			tableIndexesCheckBox.Checked = false;
		}
	}

	private void TableColumnsCheckBox_CheckedChanged(object sender, System.EventArgs e)
	{
		if (_includeParentObject && tableColumnsCheckBox.Checked)
		{
			tablesCheckBox.Checked = true;
		}
	}

	private void TableKeysCheckBox_CheckedChanged(object sender, System.EventArgs e)
	{
		if (_includeParentObject && tableKeysCheckBox.Checked)
		{
			tablesCheckBox.Checked = true;
		}
	}

	private void TableConstraintsCheckBox_CheckedChanged(object sender, System.EventArgs e)
	{
		if (_includeParentObject && tableConstraintsCheckBox.Checked)
		{
			tablesCheckBox.Checked = true;
		}
	}

	private void TableTriggersCheckBox_CheckedChanged(object sender, System.EventArgs e)
	{
		if (_includeParentObject && tableTriggersCheckBox.Checked)
		{
			tablesCheckBox.Checked = true;
		}
	}

	private void TableIndexesCheckBox_CheckedChanged(object sender, System.EventArgs e)
	{
		if (_includeParentObject && tableIndexesCheckBox.Checked)
		{
			tablesCheckBox.Checked = true;
		}
	}

	private void ViewsCheckBox_CheckedChanged(object sender, System.EventArgs e)
	{
		if (_includeParentObject && !viewsCheckBox.Checked)
		{
			viewColumnsCheckBox.Checked = false;
			viewTriggersCheckBox.Checked = false;
			viewIndexesCheckBox.Checked = false;
		}
	}

	private void ViewColumnsCheckBox_CheckedChanged(object sender, System.EventArgs e)
	{
		if (_includeParentObject && viewColumnsCheckBox.Checked)
		{
			viewsCheckBox.Checked = true;
		}
	}

	private void ViewTriggersCheckBox_CheckedChanged(object sender, System.EventArgs e)
	{
		if (_includeParentObject && viewTriggersCheckBox.Checked)
		{
			viewsCheckBox.Checked = true;
		}
	}

	private void ViewIndexesCheckBox_CheckedChanged(object sender, System.EventArgs e)
	{
		if (_includeParentObject && viewIndexesCheckBox.Checked)
		{
			viewsCheckBox.Checked = true;
		}
	}

	private void StoredProceduresCheckBox_CheckedChanged(object sender, System.EventArgs e)
	{
		if (_includeParentObject && !storedProceduresCheckBox.Checked)
		{
			storedProcedureParametersCheckBox.Checked = false;
		}
	}

	private void TableValuedFunctionsCheckBox_CheckedChanged(object sender, System.EventArgs e)
	{
		if (_includeParentObject && !tableValuedFunctionsCheckBox.Checked)
		{
			tableValuedFunctionParametersCheckBox.Checked = false;
		}
	}

	private void ScalarValuedFunctionsCheckBox_CheckedChanged(object sender, System.EventArgs e)
	{
		if (_includeParentObject && !scalarValuedFunctionsCheckBox.Checked)
		{
			scalarValuedFunctionParametersCheckBox.Checked = false;
		}
	}

	private void StoredProcedureParametersCheckBox_CheckedChanged(object sender, System.EventArgs e)
	{
		if (_includeParentObject && storedProcedureParametersCheckBox.Checked)
		{
			storedProceduresCheckBox.Checked = true;
		}
	}

	private void TableValuedFunctionParametersCheckBox_CheckedChanged(object sender, System.EventArgs e)
	{
		if (_includeParentObject && tableValuedFunctionParametersCheckBox.Checked)
		{
			tableValuedFunctionsCheckBox.Checked = true;
		}
	}

	private void ScalarValuedFunctionParametersCheckBox_CheckedChanged(object sender, System.EventArgs e)
	{
		if (_includeParentObject && scalarValuedFunctionParametersCheckBox.Checked)
		{
			scalarValuedFunctionsCheckBox.Checked = true;
		}
	}

	private void SelectAllButton_Click(object sender, System.EventArgs e)
	{
		tablesCheckBox.Checked = true;
		tableColumnsCheckBox.Checked = true;
		tableKeysCheckBox.Checked = true;
		tableConstraintsCheckBox.Checked = true;
		tableTriggersCheckBox.Checked = true;
		tableIndexesCheckBox.Checked = true;
		viewsCheckBox.Checked = true;
		viewColumnsCheckBox.Checked = true;
		viewTriggersCheckBox.Checked = true;
		viewIndexesCheckBox.Checked = true;
		storedProceduresCheckBox.Checked = true;
		tableValuedFunctionsCheckBox.Checked = true;
		scalarValuedFunctionsCheckBox.Checked = true;
		storedProcedureParametersCheckBox.Checked = true;
		tableValuedFunctionParametersCheckBox.Checked = true;
		scalarValuedFunctionParametersCheckBox.Checked = true;
	}

	private void DeselectAllButton_Click(object sender, System.EventArgs e)
	{
		DeselectAll();
	}

	private void DeselectAll()
	{
		tablesCheckBox.Checked = false;
		tableColumnsCheckBox.Checked = false;
		tableKeysCheckBox.Checked = false;
		tableConstraintsCheckBox.Checked = false;
		tableTriggersCheckBox.Checked = false;
		tableIndexesCheckBox.Checked = false;
		viewsCheckBox.Checked = false;
		viewColumnsCheckBox.Checked = false;
		viewTriggersCheckBox.Checked = false;
		viewIndexesCheckBox.Checked = false;
		storedProceduresCheckBox.Checked = false;
		tableValuedFunctionsCheckBox.Checked = false;
		scalarValuedFunctionsCheckBox.Checked = false;
		storedProcedureParametersCheckBox.Checked = false;
		tableValuedFunctionParametersCheckBox.Checked = false;
		scalarValuedFunctionParametersCheckBox.Checked = false;
	}

	private void FieldNameListView_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.A)
		{
			foreach (ListViewItem item in fieldNameListView.Items)
			{
				item.Selected = true;
			}
		}
	}

	private void FieldNameListView_ColumnClick(object sender, ColumnClickEventArgs e)
	{
		if (fieldNameListView.Sorting == SortOrder.Ascending)
		{
			fieldNameListView.Sorting = SortOrder.Descending;
		}
		else
		{
			fieldNameListView.Sorting = SortOrder.Ascending;
		}

		fieldNameListView.Sort();
	}

	private void FieldNameListView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
	{
		if (fieldNameListView.Width > 0)
		{
			ScrollBars scrollBars = ScrollBarHelper.GetVisibleScrollBars(fieldNameListView);

			if (scrollBars.ToString() == "Vertical" || scrollBars.ToString() == "Both")
			{
				if (fieldNameListView.Columns[0].Width != fieldNameListView.Width - 21)
				{
					fieldNameListView.Columns[0].Width = fieldNameListView.Width - 21;
				}
			}
			else
			{
				if (fieldNameListView.Columns[0].Width != fieldNameListView.Width - 4)
				{
					fieldNameListView.Columns[0].Width = fieldNameListView.Width - 4;
				}
			}
		}
	}

	private static void GenerateFieldItems(ListView listView)
	{
		foreach (DescriptionField item in ConfigHandler.DescriptionFields)
		{
			AddItem(listView, item);
		}
	}

	private static void AddItem(ListView listView, DescriptionField item)
	{
		ListViewItem listViewItem = new ListViewItem();
		listViewItem.ImageIndex = 0;
		listViewItem.Name = item.DatabaseFieldName;
		listViewItem.Text = item.DisplayText;

		listView.Items.Add(listViewItem);
	}

	private void ChooseTabelsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		_chooseIndividualTablesForm.ShowDialog();
		_selectedIndividualTables = _chooseIndividualTablesForm.GetSelectedIndividualObjects();
	}

	private void ChooseViewsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		_chooseIndividualViewsForm.ShowDialog();
		_selectedIndividualViews = _chooseIndividualViewsForm.GetSelectedIndividualObjects();
	}

	private void ChooseStoredProceduresLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		_chooseIndividualStoredProceduresForm.ShowDialog();
		_selectedIndividualStoredProcedures = _chooseIndividualStoredProceduresForm.GetSelectedIndividualObjects();
	}

	private void ChooseTableValuedFunctionsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		_chooseIndividualTableValuedFunctionsForm.ShowDialog();
		_selectedIndividualTableValuedFunctions = _chooseIndividualTableValuedFunctionsForm.GetSelectedIndividualObjects();
	}

	private void ChooseScalarValuedFunctionsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		_chooseIndividualScalarValuedFunctionsForm.ShowDialog();
		_selectedIndividualScalarValuedFunctions = _chooseIndividualScalarValuedFunctionsForm.GetSelectedIndividualObjects();
	}
}
