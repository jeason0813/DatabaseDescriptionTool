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
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

public partial class ManageDescriptionFieldsForm : Form
{
	private bool _anyChanges;
	private Rectangle _dragBoxFromMouseDown;
	private int _rowIndexFromMouseDown;
	private int _rowIndexOfItemUnderMouseToDrop;
	private readonly List<DescriptionField> _copiedItems = new List<DescriptionField>();
	private bool _cutActivated;
	private readonly SqlConnectionStringBuilder _connectionString;

	public ManageDescriptionFieldsForm(SqlConnectionStringBuilder connectionString)
	{
		InitializeComponent();

		_connectionString = connectionString;

		FillList();
		SelectFirstItem();
	}

	public bool AnyChanges()
	{
		return _anyChanges;
	}

	private void FillList()
	{
		dataGridView1.Rows.Clear();

		for (int i = 0; i < ConfigHandler.DescriptionFields.Count; i++)
		{
			int index = dataGridView1.Rows.Add();
			DataGridViewRow row = dataGridView1.Rows[index];
			row.Cells["ItemName"].Value = ConfigHandler.DescriptionFields[i].DisplayText;
			row.Cells["ItemName"].ToolTipText = ConfigHandler.DescriptionFields[i].DisplayText;
		}

		SetUseForImage();
		EnableItems();
	}

	private void SetUseForImage()
	{
		foreach (DescriptionField item in ConfigHandler.DescriptionFields)
		{
			foreach (DataGridViewRow row in dataGridView1.Rows)
			{
				DataGridViewImageCell cell = row.Cells["ImageColumn"] as DataGridViewImageCell;

				if (row.Cells["ItemName"].Value.ToString() == item.DisplayText)
				{
					if (item.UseForImage)
					{
						if (cell != null)
						{
							cell.Value = DatabaseDescriptionTool.Properties.Resources.book_add;
						}
					}
					else
					{
						if (cell != null)
						{
							cell.Value = DatabaseDescriptionTool.Properties.Resources.book;
						}
					}
				}
			}
		}
	}

	private void SelectFirstItem()
	{
		if (dataGridView1.Rows.Count > 0)
		{
			dataGridView1.Rows[0].Selected = true;
			EnableItems();
		}
	}

	private void MoveItem(int currentIndex, int newIndex)
	{
		DataGridViewRow row = dataGridView1.Rows[currentIndex];

		dataGridView1.Rows.RemoveAt(currentIndex);
		dataGridView1.Rows.Insert(newIndex, row);

		dataGridView1.CurrentCell = dataGridView1.Rows[newIndex].Cells["ItemName"];
		dataGridView1.Focus();

		Save();
		EnableItems();
	}

	private void MoveUpButton_Click(object sender, EventArgs e)
	{
		MoveUp();
	}

	private void MoveUp()
	{
		int currentIndex = dataGridView1.SelectedRows[0].Index;
		int newIndex = dataGridView1.SelectedRows[0].Index - 1;
		MoveItem(currentIndex, newIndex);
	}

	private void MoveDownButton_Click(object sender, EventArgs e)
	{
		MoveDown();
	}

	private void MoveDown()
	{
		int currentIndex = dataGridView1.SelectedRows[0].Index;
		int newIndex = dataGridView1.SelectedRows[0].Index + 1;
		MoveItem(currentIndex, newIndex);
	}

	private void Save()
	{
		List<DescriptionField> newList = new List<DescriptionField>();

		foreach (DataGridViewRow row in dataGridView1.Rows)
		{
			foreach (DescriptionField item in ConfigHandler.DescriptionFields)
			{
				if (row.Cells["ItemName"].Value.ToString() == item.DisplayText)
				{
					newList.Add(item);
				}
			}
		}

		ConfigHandler.DescriptionFields = newList;
		_anyChanges = true;
		ConfigHandler.SaveConfig();
	}

	private void OkButton_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void EnableItems()
	{
		if (dataGridView1.SelectedRows.Count == 0)
		{
			DisableItems();
		}
		else if (dataGridView1.SelectedRows.Count == 1)
		{
			createButton.Enabled = true;
			editButton.Enabled = true;
			deleteButton.Enabled = true;

			createMenuItem1.Enabled = true;
			editMenuItem1.Enabled = true;
			deleteMenuItem1.Enabled = true;
			cutMenuItem1.Enabled = true;
			copyMenuItem1.Enabled = true;
			deleteAllMenuItem1.Enabled = true;

			createMenuItem.Enabled = true;
			editMenuItem.Enabled = true;
			deleteMenuItem.Enabled = true;
			cutMenuItem.Enabled = true;
			copyMenuItem.Enabled = true;
			deleteAllMenuItem.Enabled = true;
		}
		else if (dataGridView1.SelectedRows.Count > 1)
		{
			createButton.Enabled = true;
			editButton.Enabled = false;
			deleteButton.Enabled = true;

			createMenuItem1.Enabled = true;
			editMenuItem1.Enabled = false;
			deleteMenuItem1.Enabled = true;
			cutMenuItem1.Enabled = true;
			copyMenuItem1.Enabled = true;
			deleteAllMenuItem1.Enabled = true;

			createMenuItem.Enabled = true;
			editMenuItem.Enabled = false;
			deleteMenuItem.Enabled = true;
			cutMenuItem.Enabled = true;
			copyMenuItem.Enabled = true;
			deleteAllMenuItem.Enabled = true;
		}

		if (dataGridView1.Rows.Count == 0)
		{
			selectAllMenuItem1.Enabled = false;
		}
		else
		{
			selectAllMenuItem1.Enabled = true;
		}

		if (dataGridView1.Rows.Count <= 1)
		{
			moveUpButton.Enabled = false;
			moveUpMenuItem1.Enabled = false;
			moveUpMenuItem.Enabled = false;
		}
		else
		{
			if (dataGridView1.Rows[0].Selected || dataGridView1.SelectedRows.Count == 0 || dataGridView1.SelectedRows.Count > 1)
			{
				moveUpButton.Enabled = false;
				moveUpMenuItem1.Enabled = false;
				moveUpMenuItem.Enabled = false;
			}
			else
			{
				moveUpButton.Enabled = true;
				moveUpMenuItem1.Enabled = true;
				moveUpMenuItem.Enabled = true;
			}
		}

		if (dataGridView1.Rows.Count <= 1)
		{
			moveDownButton.Enabled = false;
			moveDownMenuItem1.Enabled = false;
			moveDownMenuItem.Enabled = false;
		}
		else
		{
			if (dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected || dataGridView1.SelectedRows.Count == 0 || dataGridView1.SelectedRows.Count > 1)
			{
				moveDownButton.Enabled = false;
				moveDownMenuItem1.Enabled = false;
				moveDownMenuItem.Enabled = false;
			}
			else
			{
				moveDownButton.Enabled = true;
				moveDownMenuItem1.Enabled = true;
				moveDownMenuItem.Enabled = true;
			}
		}
	}

	private void DisableItems()
	{
		createButton.Enabled = true;
		editButton.Enabled = false;
		deleteButton.Enabled = false;

		createMenuItem1.Enabled = true;
		editMenuItem1.Enabled = false;
		deleteMenuItem1.Enabled = false;
		cutMenuItem1.Enabled = false;
		copyMenuItem1.Enabled = false;
		deleteAllMenuItem1.Enabled = false;

		createMenuItem.Enabled = true;
		editMenuItem.Enabled = false;
		deleteMenuItem.Enabled = false;
		cutMenuItem.Enabled = false;
		copyMenuItem.Enabled = false;
		deleteAllMenuItem.Enabled = false;
	}

	private void CreateButton_Click(object sender, EventArgs e)
	{
		Create();
	}

	private void Create()
	{
		HandleDescriptionFieldForm form = new HandleDescriptionFieldForm(_connectionString);
		DialogResult result = form.ShowDialog();

		if (result.ToString() == "OK")
		{
			DescriptionField newItem = form.GetValue();

			int insertNewRowAt = 0;

			if (dataGridView1.SelectedRows.Count > 0)
			{
				insertNewRowAt = dataGridView1.SelectedRows[0].Index;
				insertNewRowAt++;
			}

			CreateNewItem(newItem, insertNewRowAt);

			dataGridView1.FirstDisplayedScrollingRowIndex = insertNewRowAt;
			dataGridView1.CurrentCell = dataGridView1["ItemName", insertNewRowAt];
			dataGridView1.Rows[insertNewRowAt].Selected = true;

			ConfigHandler.SaveConfig();
		}

		dataGridView1.Focus();
	}

	private void CreateNewItem(DescriptionField newItem, int insertNewRowAt)
	{
		ConfigHandler.DescriptionFields.Insert(insertNewRowAt, newItem);

		int index = dataGridView1.Rows.Add();
		DataGridViewRow row = dataGridView1.Rows[index];
		row.Cells["ItemName"].Value = newItem.DisplayText;
		row.Cells["ItemName"].ToolTipText = newItem.DisplayText;

		dataGridView1.Rows.RemoveAt(index);
		dataGridView1.Rows.Insert(insertNewRowAt, row);

		if (newItem.UseForImage)
		{
			ReloadUseForImage(newItem.DisplayText);
		}

		_anyChanges = true;
	}

	private void EditButton_Click(object sender, EventArgs e)
	{
		Edit();
	}

	private void Edit()
	{
		List<DescriptionField> newItems = ConfigHandler.DescriptionFields;

		bool save = false;
		string newName = null;
		bool setImage = false;

		foreach (DescriptionField item in newItems)
		{
			if (dataGridView1.SelectedCells[1].Value.ToString() == item.DisplayText)
			{
				HandleDescriptionFieldForm form = new HandleDescriptionFieldForm(_connectionString);
				form.SetValues(item);
				DialogResult result = form.ShowDialog();

				if (result.ToString() == "OK")
				{
					DescriptionField newItem = form.GetValue();
					item.DisplayText = newItem.DisplayText;
					item.DatabaseFieldName = newItem.DatabaseFieldName;
					item.UseForImage = newItem.UseForImage;
					item.Information = newItem.Information;
					newName = newItem.DisplayText;

					if (item.UseForImage)
					{
						setImage = true;
					}

					dataGridView1.SelectedCells[1].Value = newItem.DisplayText;
					save = true;
				}

				break;
			}
		}

		if (save)
		{
			if (setImage)
			{
				ReloadUseForImage(newName);
			}

			ConfigHandler.DescriptionFields = newItems;
			_anyChanges = true;
			ConfigHandler.SaveConfig();
		}

		dataGridView1.Focus();
	}

	private void ReloadUseForImage(string useForImageItemName)
	{
		foreach (DescriptionField item in ConfigHandler.DescriptionFields)
		{
			if (useForImageItemName.ToLower() == item.DisplayText.ToLower())
			{
				item.UseForImage = true;
			}
			else
			{
				item.UseForImage = false;
			}
		}

		SetUseForImage();
	}

	private void DeleteButton_Click(object sender, EventArgs e)
	{
		Delete();
	}

	private void Delete()
	{
		DialogResult result = MessageBox.Show("Delete selected Description Fields?", ConfigHandler.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

		if (result.ToString() == "Yes")
		{
			List<DescriptionField> itemsToBeDeleted = new List<DescriptionField>();

			foreach (DataGridViewRow row in dataGridView1.SelectedRows)
			{
				foreach (DescriptionField item in ConfigHandler.DescriptionFields)
				{
					if (row.Cells["ItemName"].Value.ToString() == item.DisplayText)
					{
						itemsToBeDeleted.Add(item);
					}
				}
			}

			DoDelete(itemsToBeDeleted);
			EnableItems();
			dataGridView1.Focus();
		}
	}

	private void DoDelete(List<DescriptionField> itemsToBeDeleted)
	{
		List<int> indexesToBeRemoved = new List<int>();

		for (int r = 0; r < dataGridView1.Rows.Count; r++)
		{
			for (int i = 0; i < itemsToBeDeleted.Count; i++)
			{
				if (dataGridView1.Rows[r].Cells["ItemName"].Value.ToString() == itemsToBeDeleted[i].DisplayText)
				{
					indexesToBeRemoved.Add(dataGridView1.Rows[r].Index);
				}
			}
		}

		indexesToBeRemoved.Sort(new SortIntDescending());

		for (int i = 0; i < indexesToBeRemoved.Count; i++)
		{
			dataGridView1.Rows.RemoveAt(indexesToBeRemoved[i]);
			ConfigHandler.DescriptionFields.RemoveAt(indexesToBeRemoved[i]);
		}

		_anyChanges = true;
		ConfigHandler.SaveConfig();
	}

	private class SortIntDescending : IComparer<int>
	{
		int IComparer<int>.Compare(int a, int b)
		{
			if (a > b)
			{
				return -1;
			}
			if (a < b)
			{
				return 1;
			}
			else
			{
				return 0;
			}
		}
	}

	private void ImportToolStripMenuItem_Click(object sender, EventArgs e)
	{
		DialogResult result = openFileDialog1.ShowDialog();

		if (result.ToString() == "OK")
		{
			bool doimport = true;

			if (dataGridView1.Rows.Count > 0)
			{
				DialogResult overwrite = MessageBox.Show("All existing Description Fields will be replaced with the new ones.\r\n\r\nContinue?", ConfigHandler.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

				if (overwrite.ToString() != "Yes")
				{
					doimport = false;
				}
			}

			if (doimport)
			{
				string xml = File.ReadAllText(openFileDialog1.FileName, Encoding.UTF8);
				ConfigHandler.DescriptionFields = ConfigHandler.XmlToDescriptionFields(xml);
				_anyChanges = true;
				ConfigHandler.SaveConfig();

				FillList();
				SelectFirstItem();
			}
		}
	}

	private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
	{
		DialogResult result = saveFileDialog1.ShowDialog();

		if (result.ToString() == "OK")
		{
			if (File.Exists(saveFileDialog1.FileName))
			{
				File.Delete(saveFileDialog1.FileName);
			}

			string xml = ConfigHandler.GetDescriptionFieldListToXml(ConfigHandler.DescriptionFields);

			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);

			XmlTextWriter xmlTextWriter = new XmlTextWriter(saveFileDialog1.FileName, Encoding.UTF8);
			xmlTextWriter.IndentChar = '\t';
			xmlTextWriter.Indentation = 1;
			xmlTextWriter.Formatting = Formatting.Indented;
			xmlDocument.WriteContentTo(xmlTextWriter);

			xmlTextWriter.Flush();
			xmlTextWriter.Close();
		}
	}

	private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Close();
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (dataGridView1.Focused)
		{
			if (msg.WParam.ToInt32() == (int)Keys.Enter)
			{
				if (dataGridView1.SelectedRows.Count == 1)
				{
					Edit();
				}

				return true;
			}

			if ((int)keyData == 196644) // Keys.Shift && Keys.Control && Keys.Home
			{
				SendKeys.Send("+^{UP}");
				return true;
			}
		}

		return base.ProcessCmdKey(ref msg, keyData);
	}

	private void DataGridView1_SelectionChanged(object sender, EventArgs e)
	{
		EnableItems();
	}

	private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex == -1)
		{
			return;
		}

		if (dataGridView1.SelectedRows.Count == 0)
		{
			DisableItems();
		}
		else if (dataGridView1.SelectedRows.Count == 1)
		{
			Edit();
		}
	}

	private void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (dataGridView1.SelectedRows.Count == 0)
		{
			DisableItems();
		}

		if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
		{
			if (!dataGridView1.Rows[e.RowIndex].Selected)
			{
				dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
			}

			Rectangle r = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
			contextMenuStrip1.Show((Control)sender, r.Left + e.X, r.Top + e.Y);
		}
	}

	private void DataGridView1_DragDrop(object sender, DragEventArgs e)
	{
		Point clientPoint = dataGridView1.PointToClient(new Point(e.X, e.Y));
		_rowIndexOfItemUnderMouseToDrop = dataGridView1.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

		if (e.Effect == DragDropEffects.Move)
		{
			if (_rowIndexOfItemUnderMouseToDrop != -1)
			{
				if (_rowIndexFromMouseDown != _rowIndexOfItemUnderMouseToDrop)
				{
					MoveItem(_rowIndexFromMouseDown, _rowIndexOfItemUnderMouseToDrop);
				}
			}
		}
	}

	private void DataGridView1_DragOver(object sender, DragEventArgs e)
	{
		e.Effect = DragDropEffects.Move;
	}

	private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
	{
		if (dataGridView1.Rows.Count >= 1)
		{
			EnableItems();
		}

		if (e.KeyData == Keys.Delete)
		{
			if (dataGridView1.SelectedRows.Count >= 1)
			{
				Delete();
			}
		}
		else if (e.KeyCode == Keys.N && e.Modifiers == Keys.Control)
		{
			Create();
		}
		else if (e.KeyCode == Keys.X && e.Modifiers == Keys.Control)
		{
			if (dataGridView1.SelectedRows.Count >= 1)
			{
				Cut();
			}
		}
		else if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
		{
			if (dataGridView1.SelectedRows.Count >= 1)
			{
				Copy();
			}
		}
		else if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
		{
			if (_copiedItems.Count > 0)
			{
				Paste();
			}
		}
		else if (e.KeyCode == Keys.U && e.Modifiers == Keys.Control)
		{
			if (dataGridView1.SelectedRows.Count == 1)
			{
				if (dataGridView1.Rows[0].Selected || dataGridView1.SelectedRows.Count == 0)
				{
					return;
				}

				MoveUp();
			}
		}
		else if (e.KeyCode == Keys.D && e.Modifiers == Keys.Control)
		{
			if (dataGridView1.SelectedRows.Count == 1)
			{
				if (dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected || dataGridView1.SelectedRows.Count == 0)
				{
					return;
				}

				MoveDown();
			}
		}
	}

	private void DataGridView1_MouseDown(object sender, MouseEventArgs e)
	{
		_rowIndexFromMouseDown = dataGridView1.HitTest(e.X, e.Y).RowIndex;

		if (_rowIndexFromMouseDown != -1)
		{
			Size dragSize = SystemInformation.DragSize;
			_dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
		}
		else
		{
			_dragBoxFromMouseDown = Rectangle.Empty;
		}
	}

	private void DataGridView1_MouseMove(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			if (_dragBoxFromMouseDown != Rectangle.Empty && !_dragBoxFromMouseDown.Contains(e.X, e.Y))
			{
				dataGridView1.DoDragDrop(dataGridView1.Rows[_rowIndexFromMouseDown], DragDropEffects.Move);
			}
		}
	}

	private int GetCorrectIndexOfSelectedRow(DataGridViewRow selectedRow)
	{
		foreach (DataGridViewRow row in dataGridView1.Rows)
		{
			if (row == selectedRow)
			{
				return row.Index;
			}
		}

		return -1;
	}

	private string FirstNameOfSelectedRows()
	{
		int firstIndex = dataGridView1.Rows.Count + 1;
		string firstName = null;

		foreach (DescriptionField item in ConfigHandler.DescriptionFields)
		{
			foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
			{
				if (selectedRow.Cells["ItemName"].Value.ToString() == item.DisplayText)
				{
					if (GetCorrectIndexOfSelectedRow(selectedRow) < firstIndex)
					{
						firstIndex = GetCorrectIndexOfSelectedRow(selectedRow);
						firstName = item.DisplayText;
					}
				}
			}
		}

		return firstName;
	}

	private string LastNameOfSelectedRows()
	{
		int lastIndex = 0;
		string lastName = null;

		foreach (DescriptionField item in ConfigHandler.DescriptionFields)
		{
			foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
			{
				if (selectedRow.Cells["ItemName"].Value.ToString() == item.DisplayText)
				{
					if (GetCorrectIndexOfSelectedRow(selectedRow) >= lastIndex)
					{
						lastIndex = GetCorrectIndexOfSelectedRow(selectedRow);
						lastName = item.DisplayText;
					}
				}
			}
		}

		return lastName;
	}

	private void Cut()
	{
		_cutActivated = true;
		DoCopy();
	}

	private void Copy()
	{
		_cutActivated = false;
		DoCopy();
	}

	private void DoCopy()
	{
		_copiedItems.Clear();

		for (int i = 0; i < ConfigHandler.DescriptionFields.Count; i++)
		{
			for (int r = 0; r < dataGridView1.SelectedRows.Count; r++)
			{
				if (dataGridView1.SelectedRows[r].Cells["ItemName"].Value.ToString() == ConfigHandler.DescriptionFields[i].DisplayText)
				{
					_copiedItems.Add(ConfigHandler.DescriptionFields[i]);
				}
			}
		}
	}

	private int GetIndexOfRowFromName(string name)
	{
		foreach (DataGridViewRow row in dataGridView1.Rows)
		{
			if (row.Cells["ItemName"].Value.ToString() == name)
			{
				return row.Index;
			}
		}

		return -1;
	}

	private void Paste()
	{
		int insertNewRowAt;

		if (_cutActivated)
		{
			string firstNameOfSelectedRows = FirstNameOfSelectedRows();
			insertNewRowAt = GetIndexOfRowFromName(firstNameOfSelectedRows);

			int totalRows = dataGridView1.Rows.Count;
			int spaceLeft = totalRows - insertNewRowAt;

			if (spaceLeft < _copiedItems.Count)
			{
				insertNewRowAt = totalRows - _copiedItems.Count;
			}

			if (insertNewRowAt < 0)
			{
				insertNewRowAt = 0;
			}
		}
		else
		{
			string lastNameOfSelectedRows = LastNameOfSelectedRows();
			insertNewRowAt = GetIndexOfRowFromName(lastNameOfSelectedRows) + 1;
		}

		int insertNewRowAtOriginal = insertNewRowAt;

		if (_cutActivated)
		{
			DoDelete(_copiedItems);
		}

		List<string> nameList = new List<string>();

		foreach (DescriptionField itemToBeCopied in _copiedItems)
		{
			string name = GetNewItemName(itemToBeCopied.DisplayText);
			bool useForImage;

			if (_cutActivated)
			{
				useForImage = itemToBeCopied.UseForImage;
			}
			else
			{
				useForImage = false;
			}

			DescriptionField newItem = new DescriptionField(name, itemToBeCopied.DatabaseFieldName, useForImage, itemToBeCopied.Information);
			CreateNewItem(newItem, insertNewRowAt);
			insertNewRowAt++;

			nameList.Add(name);
		}

		foreach (DataGridViewRow row in dataGridView1.Rows)
		{
			row.Selected = false;
		}

		dataGridView1.CurrentCell = dataGridView1["ItemName", insertNewRowAtOriginal];
		SelectRows(nameList);

		ConfigHandler.SaveConfig();
		dataGridView1.Focus();
	}

	private void SelectRows(List<string> nameList)
	{
		foreach (DataGridViewRow row in dataGridView1.Rows)
		{
			foreach (string name in nameList)
			{
				if (row.Cells["ItemName"].Value.ToString() == name)
				{
					row.Selected = true;
				}
			}
		}
	}

	private static string GetNewItemName(string name)
	{
		bool uniqueName;

		do
		{
			uniqueName = true;

			foreach (DescriptionField item in ConfigHandler.DescriptionFields)
			{
				if (item.DisplayText == name)
				{
					uniqueName = false;
					break;
				}
			}

			if (!uniqueName)
			{
				name = string.Format("{0} (copy)", name);
			}
		} while (!uniqueName);

		return name;
	}

	private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Create();
	}

	private void EditToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Edit();
	}

	private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Delete();
	}

	private void MoveUpToolStripMenuItem_Click(object sender, EventArgs e)
	{
		MoveUp();
	}

	private void MoveDownToolStripMenuItem_Click(object sender, EventArgs e)
	{
		MoveDown();
	}

	private void DeleteAllToolStripMenuItem_Click(object sender, EventArgs e)
	{
		DeleteAll();
	}

	private void DeleteAll()
	{
		DialogResult overwrite = MessageBox.Show("Delete all Description Fields?", ConfigHandler.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

		if (overwrite.ToString() == "Yes")
		{
			ConfigHandler.DescriptionFields = null;
			_anyChanges = true;
			ConfigHandler.SaveConfig();
			FillList();
		}
	}

	private void DeleteAllMenuItem_Click(object sender, EventArgs e)
	{
		DeleteAll();
	}

	private void CreateMenuItem_Click(object sender, EventArgs e)
	{
		Create();
	}

	private void EditMenuItem_Click(object sender, EventArgs e)
	{
		Edit();
	}

	private void DeleteMenuItem_Click(object sender, EventArgs e)
	{
		Delete();
	}

	private void MoveUpMenuItem_Click(object sender, EventArgs e)
	{
		MoveUp();
	}

	private void MoveDownMenuItem_Click(object sender, EventArgs e)
	{
		MoveDown();
	}

	private void ContextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
	{
		HandleToolStripOpening();
	}

	private void ActionToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
	{
		HandleToolStripOpening();
	}

	private void HandleToolStripOpening()
	{
		if (_copiedItems.Count > 0)
		{
			pasteMenuItem.Enabled = true;
			pasteMenuItem1.Enabled = true;
		}
		else
		{
			pasteMenuItem.Enabled = false;
			pasteMenuItem1.Enabled = false;
		}

		if (dataGridView1.Rows.Count > 0)
		{
			deleteAllMenuItem1.Enabled = true;
			deleteAllMenuItem.Enabled = true;
		}
		else
		{
			deleteAllMenuItem1.Enabled = false;
			deleteAllMenuItem.Enabled = false;
		}
	}

	private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Copy();
	}

	private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Paste();
	}

	private void CopyToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		Copy();
	}

	private void PasteToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		Paste();
	}

	private void CutToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		Cut();
	}

	private void CutToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Cut();
	}

	private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
	{
		dataGridView1.SelectAll();
	}

	private void SelectAllToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		dataGridView1.SelectAll();
	}
}
