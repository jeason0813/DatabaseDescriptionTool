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
using System.Windows.Forms;

public partial class SearchListForm : Form
{
	public delegate void SearchEventHandler(SearchListObject foundObject);
	public event SearchEventHandler SearchEvent;

	public delegate void RequestUpdateListEventHandler(string searchTerm, bool name, bool description, ObjectsSelectorForm objectsSelectorForm, bool matchWholeWord, bool matchCase);
	public event RequestUpdateListEventHandler RequestUpdateListEvent;

	private int _currentSearchIndex;
	private int _previousSearchIndex;
	private int _originalSearchIndex;
	private bool _shown;
	private List<SearchListObject> _searchList;
	private string _previousSearchTerm;
	private bool _requestUpdateListEventPending;
	private ObjectsSelectorForm _objectsSelectorForm;
	private readonly DatabaseOperation _databaseOperation;
	private bool _folderSelected;
	private bool _serverNodeSelected;
	private int _matchCount;

	public class SearchListObject
	{
		public int Id;
		public bool Match;
		public NodeType Level1Type;
		public string Level1Name;
		public NodeType Level2Type;
		public string Level2Name;

		public SearchListObject(int id, bool match, NodeType level1Type, string level1Name, NodeType level2Type, string level2Name)
		{
			Id = id;
			Match = match;
			Level1Type = level1Type;
			Level1Name = level1Name;
			Level2Type = level2Type;
			Level2Name = level2Name;
		}
	}

	public SearchListForm(DatabaseOperation databaseOperation)
	{
		InitializeComponent();
		_databaseOperation = databaseOperation;
		Initialize();
	}

	public void ResetObjectSelector()
	{
		if (_objectsSelectorForm != null)
		{
			_objectsSelectorForm.Dispose();
		}

		_objectsSelectorForm = new ObjectsSelectorForm();
		_objectsSelectorForm.InitializeIndividualObjects(_databaseOperation);
	}

	public void SetIndividualObjects(CustomNode node)
	{
		ResetObjectSelector();
		_objectsSelectorForm.SetIndividualObjects(node);
	}

	public void SetFindButton()
	{
		if (searchTermComboBox.Text.Length > 0 && AnySearchInCheckBoxChecked() && _objectsSelectorForm.AnyCheckBoxChecked() && !_serverNodeSelected)
		{
			okButton.Enabled = true;
		}
		else
		{
			okButton.Enabled = false;
		}
	}

	public void SetSearchList(List<SearchListObject> searchList, int matchCount)
	{
		_matchCount = matchCount;
		_searchList = searchList;
		Reset(_currentSearchIndex);
	}

	public bool IsShown()
	{
		return _shown;
	}

	public void ShowNotFoundMessage()
	{
		MessageBox.Show("Not found.", ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		searchTermComboBox.Focus();
	}

	public void ShowNoMoreMatchesMessage()
	{
		MessageBox.Show("No more matches.", ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		searchTermComboBox.Focus();
	}

	public void InvokeNewSearch()
	{
		_requestUpdateListEventPending = true;
		_previousSearchTerm = null;
		ResetObjectSelector();
	}

	public void SetFolderSelected(bool value)
	{
		if (value)
		{
			_folderSelected = true;
		}
		else
		{
			_folderSelected = false;
		}
	}

	public void SetServerNodeSelected(bool value)
	{
		if (value)
		{
			_serverNodeSelected = true;
		}
		else
		{
			_serverNodeSelected = false;
		}

		selectObjectsButton.Enabled = !value;
	}

	public void Reset(int startIndex)
	{
		_currentSearchIndex = startIndex;
		_originalSearchIndex = -1;
	}

	public enum SearchDirection
	{
		Up,
		Down
	}

	public SearchDirection GetSearchDirection()
	{
		if (upRadioButton.Checked)
		{
			return SearchDirection.Up;
		}
		else
		{
			return SearchDirection.Down;
		}
	}

	private void FireSearchEvent(SearchListObject foundObject)
	{
		if (SearchEvent != null)
		{
			SearchEvent(foundObject);
		}
	}

	private void FireRequestUpdateListEvent(string searchTerm, bool name, bool description, ObjectsSelectorForm objectsSelectorForm, bool matchWholeWord, bool matchCase)
	{
		if (RequestUpdateListEvent != null)
		{
			RequestUpdateListEvent(searchTerm, name, description, objectsSelectorForm, matchWholeWord, matchCase);
		}
	}

	private void Initialize()
	{
		Text = string.Format("{0} - Search", ConfigHandler.ApplicationName);
		downRadioButton.Checked = true;
		showNoMoreMatchesMessageCheckBox.Checked = true;
		wrapAroundCheckBox.Checked = true;
		nameCheckBox.Checked = true;
		SearchHistoryHandler.LoadItems(searchTermComboBox, "RecentListSearchHistory");
		_objectsSelectorForm = new ObjectsSelectorForm();
	}

	private void OkButton_Click(object sender, EventArgs e)
	{
		_folderSelected = false;
		searchingLabel.Text = "Searching...";
		searchingLabel.Visible = true;
		Application.DoEvents();

		if (_requestUpdateListEventPending || searchTermComboBox.Text != _previousSearchTerm)
		{
			_requestUpdateListEventPending = false;
			FireRequestUpdateListEvent(searchTermComboBox.Text, nameCheckBox.Checked, descriptionCheckBox.Checked, _objectsSelectorForm, matchWholeWordCheckBox.Checked, matchCaseCheckBox.Checked);
		}

		if (searchTermComboBox.Text != _previousSearchTerm)
		{
			_previousSearchTerm = searchTermComboBox.Text;
			Reset(_currentSearchIndex);
			SearchHistoryHandler.AddItem(searchTermComboBox, searchTermComboBox.Text, "RecentListSearchHistory");
		}

		SearchInList();

		searchingLabel.Text = string.Format("Results found: {0}", _matchCount);
	}

	private void SearchInList()
	{
		int foundIndex = SearchFromIndexToEdge();

		if (foundIndex == -1)
		{
			if (wrapAroundCheckBox.Checked)
			{
				foundIndex = SearchFromEdgeToIndex();
			}

			if (foundIndex == -1)
			{
				ShowNotFoundMessage();
				Reset(_currentSearchIndex);
			}
		}
	}

	private int SearchFromIndexToEdge()
	{
		int foundIndex;

		if (GetSearchDirection() == SearchDirection.Up)
		{
			if (_currentSearchIndex == 0 && wrapAroundCheckBox.Checked)
			{
				_currentSearchIndex = _searchList.Count;
			}

			foundIndex = Search(_currentSearchIndex - 1, 0);
		}
		else
		{
			if (_currentSearchIndex > _searchList.Count - 1 && wrapAroundCheckBox.Checked)
			{
				_currentSearchIndex = 0;
			}

			foundIndex = Search(_currentSearchIndex + 1, _searchList.Count - 1);
		}

		return foundIndex;
	}

	private int SearchFromEdgeToIndex()
	{
		int foundIndex;

		if (GetSearchDirection() == SearchDirection.Up)
		{
			foundIndex = Search(_searchList.Count - 1, _currentSearchIndex);
		}
		else
		{
			foundIndex = Search(0, _currentSearchIndex);
		}

		return foundIndex;
	}

	private int Search(int rangeBegin, int rangeEnd)
	{
		int foundIndex = -1;

		if (GetSearchDirection() == SearchDirection.Up)
		{
			for (int i = rangeBegin; i >= rangeEnd; i--)
			{
				foundIndex = DoSearch(i);

				if (foundIndex == -2)
				{
					break;
				}

				if (foundIndex >= 0)
				{
					return foundIndex;
				}
			}
		}
		else
		{
			for (int i = rangeBegin; i <= rangeEnd; i++)
			{
				foundIndex = DoSearch(i);

				if (foundIndex == -2)
				{
					break;
				}

				if (foundIndex >= 0)
				{
					return foundIndex;
				}
			}
		}

		return foundIndex;
	}

	private int DoSearch(int index)
	{
		if (_searchList.Count == 0)
		{
			return -1;
		}

		SearchListObject foundObject = SearchNormal(_searchList[index]);

		if (foundObject == null)
		{
			return -1;
		}

		int foundIndex = foundObject.Id - 1;

		if (foundIndex >= 0)
		{
			foundIndex = index;

			if (foundIndex == _originalSearchIndex && showNoMoreMatchesMessageCheckBox.Checked)
			{
				ShowNoMoreMatchesMessage();
				Reset(_previousSearchIndex);
				foundIndex = _previousSearchIndex;
			}
			else
			{
				FoundMatch(foundObject);
			}
		}

		return foundIndex;
	}

	private void FoundMatch(SearchListObject foundObject)
	{
		FireSearchEvent(foundObject);

		_previousSearchIndex = foundObject.Id - 1;

		if (_originalSearchIndex == -1)
		{
			_originalSearchIndex = foundObject.Id - 1;
		}

		_currentSearchIndex = foundObject.Id - 1;
	}

	private static SearchListObject SearchNormal(SearchListObject searchListObject)
	{
		if (searchListObject.Match)
		{
			return searchListObject;
		}

		return null;
	}

	private void CancelButton_Click(object sender, EventArgs e)
	{
		_shown = false;
		Owner.Activate();
		Hide();
	}

	private void SearchForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		_shown = false;
		Owner.Activate();
		Hide();
		e.Cancel = true;
	}

	private void SearchForm_Load(object sender, EventArgs e)
	{
		if (Owner != null)
		{
			Location = new System.Drawing.Point(Owner.Location.X + (Owner.Width - Width) / 2, Owner.Location.Y + (Owner.Height - Height) / 2);
		}
	}

	private void UpRadioButton_CheckedChanged(object sender, EventArgs e)
	{
		RadioButton radioButton = (RadioButton)sender;

		if (radioButton.Checked)
		{
			int id = _currentSearchIndex;

			if (_folderSelected)
			{
				id = id + 1;
			}

			Reset(id);
			searchTermComboBox.Focus();
		}
	}

	private void DownRadioButton_CheckedChanged(object sender, EventArgs e)
	{
		RadioButton radioButton = (RadioButton)sender;

		if (radioButton.Checked)
		{
			int id = _currentSearchIndex;

			if (_folderSelected)
			{
				id = id - 1;
			}

			Reset(id);
			searchTermComboBox.Focus();
		}
	}

	private void ShowNoMoreMatchesMessageCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		searchTermComboBox.Focus();
	}

	private void MatchWholeWordCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		Reset(_currentSearchIndex);
		_requestUpdateListEventPending = true;
		searchTermComboBox.Focus();
	}

	private void MatchCaseCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		Reset(_currentSearchIndex);
		_requestUpdateListEventPending = true;
		searchTermComboBox.Focus();
	}

	private void WrapAroundCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		searchTermComboBox.Focus();
	}

	private void SearchForm_Activated(object sender, EventArgs e)
	{
		_shown = true;
		searchTermComboBox.SelectAll();
		searchTermComboBox.Focus();
	}

	private void NameCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		SearchInCheckBoxChanged();
	}

	private void DescriptionCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		SearchInCheckBoxChanged();
	}

	private void SearchInCheckBoxChanged()
	{
		SetFindButton();
		Reset(_currentSearchIndex);
		_requestUpdateListEventPending = true;
		searchTermComboBox.Focus();
	}

	private bool AnySearchInCheckBoxChecked()
	{
		if (!nameCheckBox.Checked && !descriptionCheckBox.Checked)
		{
			return false;
		}

		return true;
	}

	private void SearchTermComboBox_TextChanged(object sender, EventArgs e)
	{
		SetFindButton();
	}

	private void SelectObjectsButton_Click(object sender, EventArgs e)
	{
		_objectsSelectorForm.ShowDialog();

		_requestUpdateListEventPending = true;
		SetFindButton();
	}
}
