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
using System.Text.RegularExpressions;
using System.Windows.Forms;

public partial class SearchTextForm : Form
{
	public delegate void SearchEventHandler(int foundIndex, string searchTerm);
	public event SearchEventHandler SearchEvent;

	private int _currentSearchIndex;
	private int _previousSearchIndex;
	private int _originalSearchIndex;
	private bool _shown;
	private string _searchText;
	private string _originalSearchTerm;

	public SearchTextForm()
	{
		InitializeComponent();
		Initialize();
	}

	public void SetSearchText(string searchText)
	{
		_searchText = searchText;
	}

	public void SetSearchTerm(string searchTerm)
	{
		searchTermComboBox.Text = searchTerm;
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

	public void Reset(int startIndex)
	{
		_currentSearchIndex = startIndex;
		_originalSearchIndex = -1;
	}

	public void SetTitle(string title)
	{
		Text = title;
	}

	private enum SearchDirection
	{
		Up,
		Down
	}

	private SearchDirection GetSearchDirection()
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

	private void FireSearchEvent(int foundIndex, string searchTerm)
	{
		if (SearchEvent != null)
		{
			SearchEvent(foundIndex, searchTerm);
		}
	}

	private void Initialize()
	{
		downRadioButton.Checked = true;
		showNoMoreMatchesMessageCheckBox.Checked = true;
		wrapAroundCheckBox.Checked = true;
		SearchHistoryHandler.LoadItems(searchTermComboBox, "RecentTextSearchHistory");
	}

	private void OkButton_Click(object sender, EventArgs e)
	{
		if (searchTermComboBox.Text != _originalSearchTerm)
		{
			_originalSearchTerm = searchTermComboBox.Text;
			Reset(_currentSearchIndex);
			SearchHistoryHandler.AddItem(searchTermComboBox, searchTermComboBox.Text, "RecentTextSearchHistory");
		}

		if (GetSearchDirection() == SearchDirection.Up && _originalSearchIndex != -1)
		{
			_currentSearchIndex += (searchTermComboBox.Text.Length - CountLineShifts(searchTermComboBox.Text)) - 1;
		}

		if (GetSearchDirection() == SearchDirection.Down && _originalSearchIndex != -1)
		{
			_currentSearchIndex++;
		}

		SearchInText();
	}

	private void SearchInText()
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
			if (_currentSearchIndex - (searchTermComboBox.Text.Length - CountLineShifts(searchTermComboBox.Text)) < 0 && wrapAroundCheckBox.Checked)
			{
				_currentSearchIndex = _searchText.Length;
			}

			foundIndex = Search(searchTermComboBox.Text, 0, _currentSearchIndex);
		}
		else
		{
			if (_currentSearchIndex + (searchTermComboBox.Text.Length - CountLineShifts(searchTermComboBox.Text)) > _searchText.Length && wrapAroundCheckBox.Checked)
			{
				_currentSearchIndex = 0;
			}

			foundIndex = Search(searchTermComboBox.Text, _currentSearchIndex, _searchText.Length);
		}

		return foundIndex;
	}

	private int SearchFromEdgeToIndex()
	{
		int foundIndex;

		if (GetSearchDirection() == SearchDirection.Up)
		{
			if (_currentSearchIndex + (searchTermComboBox.Text.Length - CountLineShifts(searchTermComboBox.Text)) > _searchText.Length && wrapAroundCheckBox.Checked)
			{
				_currentSearchIndex = 0;
			}

			foundIndex = Search(searchTermComboBox.Text, _currentSearchIndex - searchTermComboBox.Text.Length, _searchText.Length);
		}
		else
		{
			if (_currentSearchIndex - (searchTermComboBox.Text.Length - CountLineShifts(searchTermComboBox.Text)) < 0 && wrapAroundCheckBox.Checked)
			{
				_currentSearchIndex = _searchText.Length;
			}

			foundIndex = Search(searchTermComboBox.Text, 0, _currentSearchIndex + searchTermComboBox.Text.Length);
		}

		return foundIndex;
	}

	private int Search(string searchTerm, int rangeBegin, int rangeEnd)
	{
		if (rangeBegin < 0)
		{
			rangeBegin = 0;
		}

		if (rangeEnd > _searchText.Length)
		{
			rangeEnd = _searchText.Length;
		}

		int foundIndex = DoSearch(searchTerm, rangeBegin, _searchText.Substring(rangeBegin, rangeEnd - rangeBegin));
		return foundIndex;
	}

	private int DoSearch(string searchTerm, int rangeBegin, string partOfSearchText)
	{
		if (_searchText.Length == 0)
		{
			return -1;
		}

		int foundIndex = GetSearchTermInText(searchTerm, partOfSearchText);

		if (foundIndex >= 0)
		{
			foundIndex += rangeBegin;

			if (foundIndex == _originalSearchIndex && showNoMoreMatchesMessageCheckBox.Checked)
			{
				ShowNoMoreMatchesMessage();

				int resetIndex = _previousSearchIndex;

				if (GetSearchDirection() == SearchDirection.Up)
				{
					resetIndex++;
				}
				else
				{
					resetIndex++;
				}

				Reset(resetIndex);
				foundIndex = _previousSearchIndex;
			}
			else
			{
				FoundMatch(foundIndex);
			}
		}

		return foundIndex;
	}

	private static int CountLineShifts(string text)
	{
		text = text.Replace("\\r", "\r").Replace("\\n", "\n");

		int count = 0;

		foreach (char c in text)
		{
			if (c == '\r' || c == '\n')
			{
				count++;
			}
		}

		return count;
	}

	private void FoundMatch(int foundIndex)
	{
		FireSearchEvent(foundIndex, searchTermComboBox.Text.Replace("\\r", "\r").Replace("\\n", "\n"));

		_previousSearchIndex = foundIndex;

		if (_originalSearchIndex == -1)
		{
			_originalSearchIndex = foundIndex;
		}

		_currentSearchIndex = foundIndex;
	}

	private int GetSearchTermInText(string searchTerm, string searchText)
	{
		string checkSearchTerm;
		string checkSearchItem;

		if (matchCaseCheckBox.Checked)
		{
			checkSearchTerm = searchTerm;
			checkSearchItem = searchText;
		}
		else
		{
			checkSearchTerm = searchTerm.ToLower();
			checkSearchItem = searchText.ToLower();
		}

		if (useRegExCheckBox.Checked)
		{
			return SearchRegEx(checkSearchItem, checkSearchTerm);
		}
		else
		{
			return SearchNormal(checkSearchItem, checkSearchTerm);
		}
	}

	private int SearchRegEx(string checkSearchItem, string checkSearchTerm)
	{
		string pattern = checkSearchTerm;

		if (matchWholeWordCheckBox.Checked)
		{
			pattern = string.Format(@"\b({0})\b", checkSearchTerm);
		}

		Match match;

		try
		{
			if (GetSearchDirection() == SearchDirection.Up)
			{
				match = Regex.Match(checkSearchItem, pattern, RegexOptions.RightToLeft);
			}
			else
			{
				match = Regex.Match(checkSearchItem, pattern);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			return -2;
		}

		if (match.Success)
		{
			return match.Index;
		}
		else
		{
			return -1;
		}
	}

	private int SearchNormal(string checkSearchItem, string checkSearchTerm)
	{
		if (matchWholeWordCheckBox.Checked)
		{
			checkSearchItem = string.Format(" {0} ", checkSearchItem);
			checkSearchTerm = string.Format(" {0} ", checkSearchTerm);
		}

		if (GetSearchDirection() == SearchDirection.Up)
		{
			return checkSearchItem.LastIndexOf(checkSearchTerm);
		}
		else
		{
			return checkSearchItem.IndexOf(checkSearchTerm);
		}
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
			Reset(_currentSearchIndex);
			searchTermComboBox.Focus();
		}
	}

	private void DownRadioButton_CheckedChanged(object sender, EventArgs e)
	{
		RadioButton radioButton = (RadioButton)sender;

		if (radioButton.Checked)
		{
			Reset(_currentSearchIndex);
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
		searchTermComboBox.Focus();
	}

	private void MatchCaseCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		Reset(_currentSearchIndex);
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

	private void UseRegExCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		Reset(_currentSearchIndex);
		searchTermComboBox.Focus();
	}

	private void ComboBox1_TextChanged(object sender, EventArgs e)
	{
		if (searchTermComboBox.Text.Length > 0)
		{
			okButton.Enabled = true;
		}
		else
		{
			okButton.Enabled = false;
		}
	}
}
