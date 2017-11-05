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
using System.Data;
using System.Windows.Forms;

public partial class ChooseIndividualObjectsForm : Form
{
	private readonly NodeType _objectsType;
	private readonly DatabaseOperation _databaseOperation;
	private readonly List<string> _selectedIndividualObjects;

	public ChooseIndividualObjectsForm(NodeType objectsType, DatabaseOperation databaseOperation, string caption)
	{
		InitializeComponent();
		_objectsType = objectsType;
		_databaseOperation = databaseOperation;
		_selectedIndividualObjects = new List<string>();
		FillListView();
		objectsGroupBox.Text = caption;
		SetButtons();
	}

	public List<string> GetSelectedIndividualObjects()
	{
		return _selectedIndividualObjects;
	}

	public void SetSelectedIndividualObjects(List<string> selectedIndividualObjectNames)
	{
		foreach (ListViewItem item in objectsListView.Items)
		{
			item.Checked = false;
		}

		foreach (string individualObjectName in selectedIndividualObjectNames)
		{
			foreach (ListViewItem item in objectsListView.Items)
			{
				if (item.Name == individualObjectName)
				{
					item.Checked = true;
				}
			}
		}
	}

	public bool AnyCheckBoxChecked()
	{
		foreach (ListViewItem item in objectsListView.Items)
		{
			if (item.Checked)
			{
				return true;
			}
		}

		return false;
	}

	public bool AllCheckBoxChecked()
	{
		foreach (ListViewItem item in objectsListView.Items)
		{
			if (!item.Checked)
			{
				return false;
			}
		}

		return true;
	}

	private void SetButtons()
	{
		if (objectsListView.Items.Count == 0)
		{
			selectAllButton.Enabled = false;
			deselectAllButton.Enabled = false;
		}
		else
		{
			selectAllButton.Enabled = true;
			deselectAllButton.Enabled = true;
		}
	}

	private DataTable GetObjectNames(NodeType objectsType)
	{
		DataTable dataTable = new DataTable();

		switch (objectsType)
		{
			case NodeType.Tables:
				dataTable = _databaseOperation.GetTableNames();
				break;
			case NodeType.Views:
				dataTable = _databaseOperation.GetViewNames();
				break;
			case NodeType.StoredProcedures:
				dataTable = _databaseOperation.GetProcedureNames();
				break;
			case NodeType.TableValuedFunctions:
				dataTable = _databaseOperation.GetTableValuedFunctionNames();
				break;
			case NodeType.ScalarValuedFunctions:
				dataTable = _databaseOperation.GetScalarValuedFunctionNames();
				break;
		}

		return dataTable;
	}

	private void FillListView()
	{
		foreach (DataRow dataRow in GetObjectNames(_objectsType).Rows)
		{
			ListViewItem item = new ListViewItem();
			item.Checked = true;
			item.Name = dataRow["name"].ToString();
			item.Text = dataRow["name"].ToString();
			objectsListView.Items.Add(item);
			_selectedIndividualObjects.Add(dataRow["name"].ToString());
		}
	}

	private void SelectAllButton_Click(object sender, System.EventArgs e)
	{
		foreach (ListViewItem item in objectsListView.Items)
		{
			item.Checked = true;
		}
	}

	private void DeselectAllButton_Click(object sender, System.EventArgs e)
	{
		foreach (ListViewItem item in objectsListView.Items)
		{
			item.Checked = false;
		}
	}

	private void OkButton_Click(object sender, System.EventArgs e)
	{
		_selectedIndividualObjects.Clear();

		foreach (ListViewItem item in objectsListView.CheckedItems)
		{
			_selectedIndividualObjects.Add(item.Name);
		}

		Close();
	}

	private void ObjectsListView_ColumnClick(object sender, ColumnClickEventArgs e)
	{
		if (objectsListView.Sorting == SortOrder.Ascending)
		{
			objectsListView.Sorting = SortOrder.Descending;
		}
		else
		{
			objectsListView.Sorting = SortOrder.Ascending;
		}

		objectsListView.Sort();
	}

	private void ObjectsListView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
	{
		ResetColumnWidth();
	}

	private void ChooseIndividualObjectsForm_Resize(object sender, System.EventArgs e)
	{
		ResetColumnWidth();
	}

	private void ResetColumnWidth()
	{
		if (objectsListView.Width > 0)
		{
			ScrollBars scrollBars = ScrollBarHelper.GetVisibleScrollBars(objectsListView);

			if (scrollBars.ToString() == "Vertical" || scrollBars.ToString() == "Both")
			{
				if (objectsListView.Columns[0].Width != objectsListView.Width - 21)
				{
					objectsListView.Columns[0].Width = objectsListView.Width - 21;
				}
			}
			else
			{
				if (objectsListView.Columns[0].Width != objectsListView.Width - 4)
				{
					objectsListView.Columns[0].Width = objectsListView.Width - 4;
				}
			}
		}
	}

	private void ObjectsListView_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.A)
		{
			foreach (ListViewItem item in objectsListView.Items)
			{
				item.Selected = true;
			}
		}
	}
}
