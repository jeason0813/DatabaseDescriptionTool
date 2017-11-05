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
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

public partial class MainForm : Form
{
	private DatabaseOperation _databaseOperation;
	private CustomNode _selectedNode;
	private TreeViewHandler _treeViewHandler;
	private TreeNode _previousSelectedNode;
	private SearchTextForm _searchTextForm;
	private bool _caretPositionChangeFromSearch;
	private CheckForUpdatesForm _checkForUpdatesForm;
	private SearchListForm _searchListForm;
	private bool _selectionChangeFromSearch;
	private bool _showDdlScript;
	private string _currentSearchDatabase;
	private DdlHandler _ddlHandler;

	public MainForm()
	{
		InitializeComponent();
		Initialize();
	}

	protected override void OnLoad(EventArgs args)
	{
		if (Site == null || (Site != null && !Site.DesignMode))
		{
			base.OnLoad(args);
			Application.Idle += OnLoaded;
		}
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (ddlTextEditorControl1.ActiveTextAreaControl.TextArea.Focused)
		{
			if ((int)keyData == 131142) // Keys.Control && Keys.F
			{
				SearchDdl();
				return true;
			}

			if (editButton.Text == "  Cancel")
			{
				if ((int)keyData == 131155) // Keys.Control && Keys.S
				{
					Save();
					return true;
				}
				else if ((int)keyData == 27) // Keys.Escape
				{
					Cancel();
					return true;
				}
			}
		}

		if ((int)keyData == 131141) // Keys.Control && Keys.E
		{
			if (editButton.Enabled)
			{
				Edit();
				return true;
			}
		}

		if (treeView1.Focused)
		{
			if ((int)keyData == 131139) // Keys.Control && Keys.C
			{
				CopyAllSql();
				return true;
			}

			if ((int)keyData == 131142) // Keys.Control && Keys.F
			{
				if (_selectedNode.Type != NodeType.Server)
				{
					SearchObjects();
				}

				return true;
			}

			if ((int)keyData == 114) // Keys.F3
			{
				if (_selectedNode.Type != NodeType.Server)
				{
					SearchInSubNodes();
				}

				return true;
			}

			if ((int)keyData == 46) // Keys.Delete
			{
				if (_selectedNode.Type != NodeType.Server)
				{
					DeleteDescriptionsInSubNodes();
				}

				return true;
			}
		}

		return base.ProcessCmdKey(ref msg, keyData);
	}

	private void Initialize()
	{
		ConfigHandler.LoadConfig();
		Text = ConfigHandler.ApplicationName;
		objectLabel.Text = "";
		SetSize();
		CheckForUpdates();
		InitializeDatabaseOperation();
		InitializeTextSearch();
		InitializeListSearch();
		InitializeTreeView();
		InitializeTabPages();
		InitializeDescriptionTemplates();
		InitializeTextEditorControl();
		InitializeDescriptionFont();
		InitializeButtonToolTips();
		InitializeShowDdlScript();
		InitializeDdlHandler();
		InitializeSchemaDropdown();
		SetConnectedToLabel();

		SetNode((CustomNode)treeView1.Nodes[0]);
		treeView1.Focus();
	}

	private void OnLoaded(object sender, EventArgs args)
	{
		Application.Idle -= OnLoaded;

		splitContainer1.SplitterMoved += SplitContainer1_SplitterMoved;
		splitContainer2.SplitterMoved += SplitContainer2_SplitterMoved;
	}

	private void InitializeSchemaDropdown()
	{
		DataTable dataTable = _databaseOperation.GetSchemas();

		if (dataTable != null)
		{
			foreach (DataRow schemaRow in dataTable.Rows)
			{
				ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
				toolStripMenuItem.Text = schemaRow["name"].ToString();
				toolStripMenuItem.Click += SchemaToolStripMenuItem_Click;

				if (schemaRow["name"].ToString() == ConfigHandler.SchemaName)
				{
					toolStripMenuItem.Checked = true;
				}

				useSchemaToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
			}
		}
	}

	private void InitializeButtonToolTips()
	{
		ToolTip toolTip = new ToolTip();
		toolTip.SetToolTip(editButton, "Edit (Ctrl+E) / Cancel (Esc)");
		toolTip.SetToolTip(saveButton, "Save (Ctrl+S)");
	}

	private void InitializeTextSearch()
	{
		_searchTextForm = new SearchTextForm();
		_searchTextForm.SetTitle("Search");
		_searchTextForm.SearchEvent += SearchTextForm_SearchEvent;
	}

	private void InitializeListSearch()
	{
		_searchListForm = new SearchListForm(_databaseOperation);
		_searchListForm.SearchEvent += SearchListForm_SearchEvent;
		_searchListForm.RequestUpdateListEvent += SearchListForm_RequestUpdateListEvent;
	}

	private void InitializeTextEditorControl()
	{
		ddlTextEditorControl1.SetHighlighting("SQL");
		ddlTextEditorControl1.Document.HighlightingStrategy = HighlightingManager.Manager.FindHighlighter("SQL");

		ddlTextEditorControl1.TextEditorProperties.Font = new Font(ConfigHandler.TextDataFontFamily, float.Parse(ConfigHandler.TextDataFontSize));
		ddlTextEditorControl1.Font = new Font(ConfigHandler.TextDataFontFamily, float.Parse(ConfigHandler.TextDataFontSize));

		ddlTextEditorControl1.ActiveTextAreaControl.Caret.PositionChanged += Caret_PositionChanged;
	}

	private void InitializeDescriptionFont()
	{
		Font font = new Font(ConfigHandler.DescriptionFontFamily, float.Parse(ConfigHandler.DescriptionFontSize));
		SetDescriptionFont(font);
	}

	private void InitializeShowDdlScript()
	{
		if (ConfigHandler.ShowDdlScript == "True")
		{
			_showDdlScript = true;
		}

		objectDefinitionScriptToolStripMenuItem.Checked = _showDdlScript;
		splitContainer2.Panel2Collapsed = !_showDdlScript;
	}

	private void InitializeDdlHandler()
	{
		_ddlHandler = new DdlHandler(_databaseOperation);
		_ddlHandler.PrefetchCompleteEvent += DdlHandler_PrefetchCompleteEvent;
	}

	private void DdlHandler_PrefetchCompleteEvent(bool requestPending)
	{
		if (requestPending)
		{
			SetDDL();
		}
	}

	private void CheckForUpdates()
	{
		if (Convert.ToBoolean(ConfigHandler.CheckForUpdatesOnStart))
		{
			_checkForUpdatesForm = new CheckForUpdatesForm();
			_checkForUpdatesForm.UpdateCheckCompleteEvent += UpdateCheckCompleteEvent;
			_checkForUpdatesForm.CheckForUpdates();
		}
	}

	private void UpdateCheckCompleteEvent(string errorMessage, Version version, bool anyUpdates, bool anyErrors)
	{
		if (!anyErrors && anyUpdates)
		{
			_checkForUpdatesForm.ShowDialog();

			if (_checkForUpdatesForm.GetForceQuit())
			{
				Environment.Exit(0);
			}
		}
	}

	private void SetTextDataFont(Font font)
	{
		ddlTextEditorControl1.TextEditorProperties.Font = font;
		ddlTextEditorControl1.Font = font;
		ddlTextEditorControl1.ActiveTextAreaControl.TextArea.Refresh();
	}

	private void SetDescriptionFont(Font font)
	{
		foreach (TabPage tabPage in tabControl1.TabPages)
		{
			foreach (Control control in tabPage.Controls)
			{
				if (control is TabPageUserControl)
				{
					((TabPageUserControl)control).SetDescriptionFont(font);
				}
			}
		}
	}

	private void SetConnectedToLabel()
	{
		string server = _databaseOperation.GetSqlServerName();
		connectedToLabel.Text = string.Format("Connected to: {0}", server);
	}

	private void SetSize()
	{
		int x = Convert.ToInt32(ConfigHandler.WindowSize.Split(';')[0]);
		int y = Convert.ToInt32(ConfigHandler.WindowSize.Split(';')[1]);

		if (x > Screen.PrimaryScreen.Bounds.Width || y > Screen.PrimaryScreen.Bounds.Height)
		{
			WindowState = FormWindowState.Maximized;
			return;
		}

		if (x >= 570 && y >= 434)
		{
			Size = new Size(x, y);
		}

		splitContainer1.SplitterDistance = Convert.ToInt32(ConfigHandler.SplitterDistanceVertical);
		splitContainer2.SplitterDistance = Convert.ToInt32(ConfigHandler.SplitterDistanceHorizontal);
	}

	private void InitializeDescriptionTemplates()
	{
		insertDescriptionTemplateToolStripMenuItem.DropDownItems.Clear();

		foreach (DescriptionTemplate item in ConfigHandler.DescriptionTemplates)
		{
			ToolStripMenuItem templateToolStripMenuItem = new ToolStripMenuItem();
			templateToolStripMenuItem.Name = item.Name;
			templateToolStripMenuItem.Text = item.Name;
			templateToolStripMenuItem.Image = DatabaseDescriptionTool.Properties.Resources.book_edit;
			templateToolStripMenuItem.Click += TemplateToolStripMenuItem_Click;

			insertDescriptionTemplateToolStripMenuItem.DropDownItems.Add(templateToolStripMenuItem);
		}
	}

	private void TemplateToolStripMenuItem_Click(object sender, EventArgs e)
	{
		string clickedItem = sender.ToString();

		foreach (TabPage tabPage in tabControl1.TabPages)
		{
			if (tabControl1.SelectedTab == tabPage)
			{
				foreach (Control control in tabPage.Controls)
				{
					if (control is TabPageUserControl)
					{
						bool insert = true;

						if (((TabPageUserControl)control).GetDescription() != "")
						{
							DialogResult result = MessageBox.Show("Replace current description with template?", ConfigHandler.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

							if (result.ToString() != "Yes")
							{
								insert = false;
							}
						}

						if (insert)
						{
							foreach (DescriptionTemplate template in ConfigHandler.DescriptionTemplates)
							{
								if (template.Name == clickedItem)
								{
									DoEdit();

									string templateText = template.Template;
									templateText = templateText.Replace("{datetime}", DateTime.Now.ToString());
									templateText = templateText.Replace("{user}", Environment.UserName);

									((TabPageUserControl)control).SetText(templateText);
									((TabPageUserControl)control).SetDescriptionChanged(true);
								}
							}
						}
					}
				}
			}
		}
	}

	private void InitializeTabPages()
	{
		tabControl1.TabPages.Clear();

		foreach (DescriptionField item in ConfigHandler.DescriptionFields)
		{
			AddTabPage(item.DisplayText, item.Information);
		}
	}

	private TabPageUserControl GetNewTabPageUserControl(string information)
	{
		TabPageUserControl tabPageUserControl = new TabPageUserControl();
		tabPageUserControl.Dock = DockStyle.Fill;
		tabPageUserControl.SetCharacterCountEvent += TabPageUserControl_SetCharacterCountEvent;
		tabPageUserControl.SaveEvent += TabPageUserControl_SaveEvent;
		tabPageUserControl.CancelEvent += TabPageUserControl_CancelEvent;
		tabPageUserControl.InvokeEditEvent += TabPageUserControl_InvokeEditEvent;
		tabPageUserControl.SetInformation(information);

		return tabPageUserControl;
	}

	private void TabPageUserControl_SaveEvent()
	{
		Save();
	}

	private void TabPageUserControl_CancelEvent()
	{
		Cancel();
	}

	private void TabPageUserControl_InvokeEditEvent()
	{
		if (editButton.Text == "  Edit")
		{
			DoEdit();
		}
	}

	private void AddTabPage(string name, string information)
	{
		TabPage newTabPage = new TabPage();
		newTabPage.Name = name;
		newTabPage.Padding = new Padding(1, 3, 1, 1);
		newTabPage.Text = name;
		newTabPage.Controls.Add(GetNewTabPageUserControl(information));
		newTabPage.ToolTipText = information;

		ImageList imageList = new ImageList();
		imageList.Images.Add(DatabaseDescriptionTool.Properties.Resources.book);
		tabControl1.ImageList = imageList;
		newTabPage.ImageIndex = 0;

		tabControl1.TabPages.Add(newTabPage);
	}

	private void TabPageUserControl_SetCharacterCountEvent(string text)
	{
		toolStripStatusLabel1.Text = string.Format("Characters left: {0}", 7500 - text.Length);
	}

	private void InitializeTreeView()
	{
		treeView1.AfterSelect += TreeView_AfterSelect;
		treeView1.KeyUp += TreeView1_KeyUp;

		_treeViewHandler = new TreeViewHandler(_databaseOperation, treeView1);
		_treeViewHandler.PrefetchDdlEvent += TreeViewHandler_PrefetchDdlEvent;
		_treeViewHandler.Reload();
	}

	private void TreeViewHandler_PrefetchDdlEvent(CustomNode node)
	{
		if (node.Type != NodeType.Server)
		{
			if (_showDdlScript)
			{
				_ddlHandler.Prefetch();
			}
		}
	}

	private void ReloadTreeView()
	{
		foreach (TabPage tabPage in tabControl1.TabPages)
		{
			foreach (Control control in tabPage.Controls)
			{
				if (control is TabPageUserControl)
				{
					((TabPageUserControl)control).SetText("");
				}
			}
		}

		_treeViewHandler.Reload();
		SetNode((CustomNode)treeView1.Nodes[0]);
		treeView1.Focus();
	}

	private void RefreshNode()
	{
		CustomNode selectedNode = ((CustomNode)treeView1.SelectedNode);
		_treeViewHandler.RefreshNode(selectedNode);
		SetNode(selectedNode);
		treeView1.Focus();
	}

	private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
	{
		CustomNode selectedNode = (CustomNode)e.Node;

		if (selectedNode.Type != NodeType.Server)
		{
			if (_showDdlScript)
			{
				_ddlHandler.Prefetch();
			}
		}

		if (!_selectionChangeFromSearch && _searchListForm.IsShown())
		{
			if (_databaseOperation.GetDatabaseName() != _currentSearchDatabase)
			{
				_searchListForm.InvokeNewSearch();
			}

			SetSearchStartPosition(selectedNode);

			_currentSearchDatabase = _databaseOperation.GetDatabaseName();
		}

		if (e.Action == TreeViewAction.ByMouse)
		{
			SetNode(selectedNode);
			treeView1.Focus();
		}
	}

	private void SetSearchStartPosition(CustomNode selectedNode)
	{
		int id = SearchTreeHandler.CustomNodeToIndex(_databaseOperation, selectedNode);

		if (selectedNode.Type == NodeType.Database || selectedNode.Type == NodeType.Tables || selectedNode.Type == NodeType.Views || selectedNode.Type == NodeType.Programmability || selectedNode.Type == NodeType.TableColumns || selectedNode.Type == NodeType.TableKeys || selectedNode.Type == NodeType.TableConstraints || selectedNode.Type == NodeType.TableTriggers || selectedNode.Type == NodeType.TableIndexes || selectedNode.Type == NodeType.ViewColumns || selectedNode.Type == NodeType.ViewTriggers || selectedNode.Type == NodeType.ViewIndexes || selectedNode.Type == NodeType.StoredProcedures || selectedNode.Type == NodeType.Functions || selectedNode.Type == NodeType.TableValuedFunctions || selectedNode.Type == NodeType.ScalarValuedFunctions || selectedNode.Type == NodeType.StoredProcedureParameters || selectedNode.Type == NodeType.TableValuedFunctionParameters || selectedNode.Type == NodeType.ScalarValuedFunctionParameters)
		{
			_searchListForm.SetFolderSelected(true);

			if (_searchListForm.GetSearchDirection() == SearchListForm.SearchDirection.Down)
			{
				id = id - 1;
			}

			_searchListForm.SetServerNodeSelected(false);
		}
		else if (selectedNode.Type == NodeType.Server)
		{
			_searchListForm.SetServerNodeSelected(true);
		}
		else
		{
			_searchListForm.SetServerNodeSelected(false);
			_searchListForm.SetFolderSelected(false);
		}

		_searchListForm.SetFindButton();
		_searchListForm.Reset(id);
	}

	private void TreeView1_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyData != Keys.F5)
		{
			SetNode(((CustomNode)treeView1.SelectedNode));
			treeView1.Focus();
		}
	}

	private void SetNode(CustomNode node)
	{
		_selectedNode = node;
		_previousSelectedNode = node;
		SetButtons();

		if (_showDdlScript)
		{
			SetDDL();
		}

		if (node.Type == NodeType.Server)
		{
			searchToolStripButton.Enabled = false;
		}
		else
		{
			searchToolStripButton.Enabled = true;
		}

		treeView1.SelectedNode = node;
		SetObjectMenus(node);
	}

	private void SetDDL()
	{
		if (_selectedNode.DescriptionChangeable)
		{
			ddlTextEditorControl1.Text = _ddlHandler.GetDdl(_selectedNode);
			ddlTextEditorControl1.ActiveTextAreaControl.Refresh();
			ddlTextEditorControl1.Enabled = true;
		}
		else
		{
			ddlTextEditorControl1.Text = "";
			ddlTextEditorControl1.ActiveTextAreaControl.Refresh();
			ddlTextEditorControl1.Enabled = false;
		}
	}

	private void InitializeDatabaseOperation()
	{
		ConnectionDialogForm form = new ConnectionDialogForm(_databaseOperation);
		form.ShowDialog();

		Application.DoEvents();

		bool success = form.ConnectionChanged;

		if (success)
		{
			_databaseOperation = form.GetDatabaseOperation();
		}

		if (success)
		{
			_databaseOperation.BeginCommEvent += BeginCommEvent;
			_databaseOperation.EndCommEvent += EndCommEvent;
		}
		else
		{
			if (_databaseOperation == null)
			{
				Environment.Exit(-1);
			}
		}
	}

	private void BeginCommEvent()
	{
		statusStrip1.SuspendLayout();
		dalToolStripStatusLabel.Image = DatabaseDescriptionTool.Properties.Resources.bullet_orange;
		statusStrip1.Refresh();
		statusStrip1.ResumeLayout();
	}

	private void EndCommEvent()
	{
		statusStrip1.SuspendLayout();
		dalToolStripStatusLabel.Image = DatabaseDescriptionTool.Properties.Resources.bullet_green;
		statusStrip1.Refresh();
		statusStrip1.ResumeLayout();
	}

	private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void SplitContainer1_Paint(object sender, PaintEventArgs e)
	{
		SplitContainerGrip.PaintGrip(sender, e);
	}

	private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		bool discardChanges = CheckDescriptionChanged();

		if (!discardChanges)
		{
			e.Cancel = true;
		}
		else
		{
			Hide();
			Environment.Exit(0);
		}
	}

	private void ChangeConnectionToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		ChangeConnection();
	}

	private void ChangeConnection()
	{
		ConnectionDialogForm form = new ConnectionDialogForm(_databaseOperation);
		form.ShowInTaskbar = false;
		form.ShowDialog();

		Application.DoEvents();

		if (form.ConnectionChanged)
		{
			ReloadTreeView();
			SetConnectedToLabel();
		}
	}

	private void MainForm_Resize(object sender, EventArgs e)
	{
		if (Width < MinimumSize.Width || Width == 0)
		{
			return;
		}

		if (WindowState == FormWindowState.Maximized)
		{
			splitContainer1.SplitterDistance = Convert.ToInt32(ConfigHandler.SplitterDistanceVertical);
			splitContainer2.SplitterDistance = Convert.ToInt32(ConfigHandler.SplitterDistanceHorizontal);
		}

		splitContainer1.SplitterDistance++;
		splitContainer1.SplitterDistance--;
		splitContainer2.SplitterDistance++;
		splitContainer2.SplitterDistance--;

		splitContainer1.Invalidate();
		splitContainer2.Invalidate();

		ConfigHandler.WindowSize = string.Format("{0}; {1}", Size.Width, Size.Height);
		ConfigHandler.SaveConfig();
	}

	private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
	{
		DoExport();
	}

	private void DoExport()
	{
		_databaseOperation.BeginCommEvent -= BeginCommEvent;
		_databaseOperation.EndCommEvent -= EndCommEvent;
		ExportForm form = new ExportForm(_databaseOperation);
		form.ShowDialog();
		_databaseOperation.BeginCommEvent += BeginCommEvent;
		_databaseOperation.EndCommEvent += EndCommEvent;
	}

	private void DoExport(CustomNode node)
	{
		_databaseOperation.BeginCommEvent -= BeginCommEvent;
		_databaseOperation.EndCommEvent -= EndCommEvent;
		ExportForm form = new ExportForm(_databaseOperation);
		form.SetIndividualObjects(node);
		form.ShowDialog();
		_databaseOperation.BeginCommEvent += BeginCommEvent;
		_databaseOperation.EndCommEvent += EndCommEvent;
	}

	private void AboutToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		AboutForm form = new AboutForm();
		form.ShowDialog();
	}

	private void EditButton_Click(object sender, EventArgs e)
	{
		Edit();
	}

	private void Edit()
	{
		if (editButton.Text == "  Edit")
		{
			DoEdit();
		}
		else if (editButton.Text == "  Cancel")
		{
			Cancel();
		}
	}

	private void DoEdit()
	{
		saveButton.Enabled = true;
		editButton.Text = "  Cancel";
		treeView1.Enabled = false;
		changeConnectionToolStripMenuItem1.Enabled = false;
		exportToolStripMenuItem.Enabled = false;
		importToolStripMenuItem.Enabled = false;
		manageDescriptionFieldsToolStripMenuItem.Enabled = false;
		refreshToolStripMenuItem.Enabled = false;
		treeViewToolStrip1.Enabled = false;
		editObjectToolStripMenuItem1.Enabled = false;
		editToolStripButton.Enabled = false;
		importObjectToolStripMenuItem1.Enabled = false;
		exportObjectToolStripMenuItem1.Enabled = false;
		copySQLToToolStripMenuItem1.Enabled = false;
		searchInSubNodesToolStripMenuItem1.Enabled = false;
		deleteDescriptionsInSubNodesToolStripMenuItem.Enabled = false;
		useSchemaToolStripMenuItem.Enabled = false;

		readonlyLabel.Visible = false;

		PrepareTabControlForEditing();
	}

	private bool CheckDescriptionChanged()
	{
		bool descriptionChanged = false;
		string modifiedTabs = "";

		foreach (TabPage tabPage in tabControl1.TabPages)
		{
			foreach (Control control in tabPage.Controls)
			{
				if (control is TabPageUserControl)
				{
					if (((TabPageUserControl)control).GetDescriptionChanged())
					{
						descriptionChanged = true;
						modifiedTabs = string.Format("{0}{1}\r\n", modifiedTabs, tabPage.Name);
					}
				}
			}
		}

		if (descriptionChanged)
		{
			ExtendedMessageBoxForm form = new ExtendedMessageBoxForm();
			form.SetTextLabel("Discard changes on the following Description Fields?:");
			form.SetTextBox(modifiedTabs);
			form.ShowDialog();

			if (form.YesButtonClicked)
			{
				return true;
			}
		}
		else
		{
			return true;
		}

		return false;
	}

	private bool CheckValidDescriptionLength()
	{
		string invalidTabs = "";

		foreach (TabPage tabPage in tabControl1.TabPages)
		{
			foreach (Control control in tabPage.Controls)
			{
				if (control is TabPageUserControl)
				{
					int length = (((TabPageUserControl)control).GetDescriptionLength());

					if (length > 7500)
					{
						invalidTabs = string.Format("{0}{1}, ", invalidTabs, tabPage.Name);
					}
				}
			}
		}

		if (invalidTabs.Length > 0)
		{
			MessageBox.Show(string.Format("Number of characters exceeds 7500 on {0}.\r\n\r\nOnly 7500 characters are allowed.", invalidTabs.Substring(0, invalidTabs.Length - 2)), ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			return false;
		}
		else
		{
			return true;
		}
	}

	private void PrepareTabControlForEditing()
	{
		foreach (TabPage tabPage in tabControl1.TabPages)
		{
			foreach (Control control in tabPage.Controls)
			{
				if (control is TabPageUserControl)
				{
					((TabPageUserControl)control).StartEdit();
				}
			}

			if (tabControl1.SelectedTab == tabPage)
			{
				foreach (Control control in tabPage.Controls)
				{
					if (control is TabPageUserControl)
					{
						((TabPageUserControl)control).GetCharacterCount();
						((TabPageUserControl)control).SetFocus();
					}
				}
			}
		}
	}

	private void Cancel()
	{
		bool discardChanges = CheckDescriptionChanged();

		if (discardChanges)
		{
			SetButtons();
			treeView1.Focus();
		}
	}

	private void SaveButton_Click(object sender, EventArgs e)
	{
		Save();
	}

	private void Save()
	{
		if (OverwriteChangesinDatabase() && CheckValidDescriptionLength())
		{
			DoSave();
		}
	}

	private void DoSave()
	{
		bool useForImageNodeChanged = false;
		_selectedNode.ClearDescriptions();

		foreach (TabPage tabPage in tabControl1.TabPages)
		{
			foreach (Control control in tabPage.Controls)
			{
				if (control is TabPageUserControl)
				{
					string description = ((TabPageUserControl)control).GetDescription();
					bool useForImage = GetUseForImage(tabPage.Name);

					DescriptionItem item = new DescriptionItem(tabPage.Name, GetDatabaseFieldName(tabPage.Name), useForImage, GetInformation(tabPage.Name));
					item.Description = description;

					if (useForImage)
					{
						useForImageNodeChanged = true;
					}

					_databaseOperation.SaveDescription(_selectedNode, description, GetDatabaseFieldName(tabPage.Name));

					if (description != "")
					{
						_selectedNode.AddDescription(item);
					}
				}
			}
		}

		if (useForImageNodeChanged)
		{
			_selectedNode.UpdateImage();
		}

		SetButtons();
		treeView1.Focus();
	}

	private bool OverwriteChangesinDatabase()
	{
		foreach (TabPage tabPage in tabControl1.TabPages)
		{
			foreach (Control control in tabPage.Controls)
			{
				if (control is TabPageUserControl)
				{
					string descriptionInDatabase = _databaseOperation.GetDescription(_selectedNode, GetDatabaseFieldName(tabPage.Name));

					if (((TabPageUserControl)control).GetDescriptionBeforeEdit() != descriptionInDatabase && descriptionInDatabase != "")
					{
						string text = string.Format("\"{0}\" has changed in the database since you started editing.\r\n\r\nSave your changes and overwrite the description in the database?", tabPage.Name);
						DialogResult result = MessageBox.Show(text, ConfigHandler.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

						if (result.ToString() != "Yes")
						{
							return false;
						}
					}
				}
			}
		}

		return true;
	}

	private static string GetDatabaseFieldName(string displayText)
	{
		List<DescriptionField> types = ConfigHandler.DescriptionFields;

		foreach (DescriptionField type in types)
		{
			if (type.DisplayText == displayText)
			{
				return type.DatabaseFieldName;
			}
		}

		return null;
	}

	private static string GetInformation(string displayText)
	{
		List<DescriptionField> types = ConfigHandler.DescriptionFields;

		foreach (DescriptionField type in types)
		{
			if (type.DisplayText == displayText)
			{
				return type.Information;
			}
		}

		return null;
	}

	private static bool GetUseForImage(string displayText)
	{
		List<DescriptionField> types = ConfigHandler.DescriptionFields;

		foreach (DescriptionField type in types)
		{
			if (type.DisplayText == displayText)
			{
				return type.UseForImage;
			}
		}

		return false;
	}

	private void SetButtons()
	{
		editButton.Text = "  Edit";
		treeView1.Enabled = true;
		changeConnectionToolStripMenuItem1.Enabled = true;
		exportToolStripMenuItem.Enabled = true;
		importToolStripMenuItem.Enabled = true;
		manageDescriptionFieldsToolStripMenuItem.Enabled = true;
		refreshToolStripMenuItem.Enabled = true;
		saveButton.Enabled = false;
		treeViewToolStrip1.Enabled = true;
		importObjectToolStripMenuItem1.Enabled = true;
		exportObjectToolStripMenuItem1.Enabled = true;
		copySQLToToolStripMenuItem1.Enabled = true;
		searchInSubNodesToolStripMenuItem1.Enabled = true;
		deleteDescriptionsInSubNodesToolStripMenuItem.Enabled = true;
		useSchemaToolStripMenuItem.Enabled = true;

		DisableTextBoxes();

		if (_selectedNode.DescriptionChangeable && tabControl1.TabCount > 0)
		{
			editButton.Enabled = true;
			editObjectToolStripMenuItem.Enabled = true;
			editObjectToolStripMenuItem1.Enabled = true;
			editToolStripButton.Enabled = true;
			readonlyLabel.Visible = true;
			objectLabel.Text = _selectedNode.Name;
		}
		else
		{
			editButton.Enabled = false;
			editObjectToolStripMenuItem.Enabled = false;
			editObjectToolStripMenuItem1.Enabled = false;
			editToolStripButton.Enabled = false;
			readonlyLabel.Visible = false;
			objectLabel.Text = "";
		}

		objectLabel.Refresh();
		readonlyLabel.Refresh();

		SetDescriptions();
		toolStripStatusLabel1.Text = "";
	}

	private void SetDescriptions()
	{
		foreach (TabPage tabPage in tabControl1.TabPages)
		{
			string text = "";

			if (_selectedNode.DescriptionChangeable)
			{
				foreach (DescriptionItem item in _selectedNode.Descriptions)
				{
					if (tabPage.Name == item.Type.DisplayText)
					{
						text = item.Description;
					}
				}
			}

			foreach (Control control in tabPage.Controls)
			{
				if (control is TabPageUserControl)
				{
					((TabPageUserControl)control).SetText(text);
				}
			}
		}
	}

	private void DisableTextBoxes()
	{
		foreach (TabPage tabPage in tabControl1.TabPages)
		{
			foreach (Control control in tabPage.Controls)
			{
				if (control is TabPageUserControl)
				{
					((TabPageUserControl)control).StopEdit();
				}
			}
		}
	}

	private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
	{
		foreach (TabPage tabPage in tabControl1.TabPages)
		{
			if (tabControl1.SelectedTab == tabPage)
			{
				foreach (Control control in tabPage.Controls)
				{
					if (control is TabPageUserControl)
					{
						((TabPageUserControl)control).GetCharacterCount();
						((TabPageUserControl)control).SetFocus();
					}
				}
			}
		}
	}

	private void ImportToolStripMenuItem_Click(object sender, EventArgs e)
	{
		DoImport();
	}

	private void DoImport()
	{
		_databaseOperation.BeginCommEvent -= BeginCommEvent;
		_databaseOperation.EndCommEvent -= EndCommEvent;
		ImportForm form = new ImportForm(_databaseOperation);
		form.ShowDialog();
		_databaseOperation.BeginCommEvent += BeginCommEvent;
		_databaseOperation.EndCommEvent += EndCommEvent;

		if (form.GetReloadTreeViewAfterRun())
		{
			ReloadTreeView();
		}
	}

	private void DoImport(CustomNode node)
	{
		_databaseOperation.BeginCommEvent -= BeginCommEvent;
		_databaseOperation.EndCommEvent -= EndCommEvent;
		ImportForm form = new ImportForm(_databaseOperation);
		form.SetIndividualObjects(node);
		form.ShowDialog();
		_databaseOperation.BeginCommEvent += BeginCommEvent;
		_databaseOperation.EndCommEvent += EndCommEvent;

		if (form.GetReloadTreeViewAfterRun())
		{
			ReloadTreeView();
		}
	}

	private void ManageDescriptionTemplatesToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ManageDescriptionTemplatesForm form = new ManageDescriptionTemplatesForm();
		form.ShowDialog();

		if (form.AnyChanges())
		{
			InitializeDescriptionTemplates();
		}
	}

	private void ManageDescriptionFieldsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		SqlConnectionStringBuilder newConnString = new SqlConnectionStringBuilder(_databaseOperation.GetConnectionString().ToString());

		ManageDescriptionFieldsForm form = new ManageDescriptionFieldsForm(newConnString);
		form.ShowDialog();

		if (form.AnyChanges())
		{
			ReloadTreeView();
			InitializeTabPages();
			InitializeDescriptionFont();
			treeView1.Focus();
		}
	}

	private void SplitContainer1_MouseUp(object sender, MouseEventArgs e)
	{
		if (splitContainer1.CanFocus)
		{
			ActiveControl = tabControl1;
		}
	}

	private void SplitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
	{
		ConfigHandler.SplitterDistanceVertical = splitContainer1.SplitterDistance.ToString();
		ConfigHandler.SaveConfig();
	}

	private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
	{
		if (e.Button == MouseButtons.Right)
		{
			TreeNode node = treeView1.GetNodeAt(e.X, e.Y);

			if (node.Bounds.Contains(e.X, e.Y))
			{
				if (e.Node != _previousSelectedNode)
				{
					SetNode((CustomNode)e.Node);
					treeView1.Focus();
				}

				if ((((CustomNode)e.Node).Type) != NodeType.Server)
				{
					treeViewContextMenuStrip.Show(treeView1, new Point(e.X, e.Y));
				}
			}
		}
	}

	private void SetObjectMenus(CustomNode node)
	{
		string objectName = "";
		bool copyOption = false;

		objectToolStripMenuItem.Enabled = true;

		switch (node.Type)
		{
			case NodeType.Server:
				objectToolStripMenuItem.Enabled = false;
				break;
			case NodeType.Database:
				objectName = "Database";
				break;
			case NodeType.Table:
				objectName = "Table";
				copyOption = true;
				break;
			case NodeType.View:
				objectName = "View";
				copyOption = true;
				break;
			case NodeType.StoredProcedure:
				objectName = "Stored Procedure";
				copyOption = true;
				break;
			case NodeType.TableValuedFunction:
				objectName = "Table-valued Function";
				copyOption = true;
				break;
			case NodeType.ScalarValuedFunction:
				objectName = "Scalar-valued Function";
				copyOption = true;
				break;
			case NodeType.Tables:
				objectName = "Tables";
				break;
			case NodeType.Views:
				objectName = "Views";
				break;
			case NodeType.StoredProcedures:
				objectName = "Stored Procedures";
				break;
			case NodeType.TableValuedFunctions:
				objectName = "Table-valued Functions";
				break;
			case NodeType.ScalarValuedFunctions:
				objectName = "Scalar-valued Functions";
				break;
			case NodeType.Functions:
				objectName = "Functions";
				break;
			case NodeType.Programmability:
				objectName = "Programmability";
				break;
			case NodeType.TableColumns:
				objectName = "Columns";
				break;
			case NodeType.TableTriggers:
				objectName = "Triggers";
				break;
			case NodeType.TableIndexes:
				objectName = "Indexes";
				break;
			case NodeType.TableConstraints:
				objectName = "Constraints";
				break;
			case NodeType.TableKeys:
				objectName = "Keys";
				break;
			case NodeType.ViewColumns:
				objectName = "Columns";
				break;
			case NodeType.ViewTriggers:
				objectName = "Triggers";
				break;
			case NodeType.ViewIndexes:
				objectName = "Indexes";
				break;
			case NodeType.StoredProcedureParameters:
				objectName = "Parameters";
				break;
			case NodeType.TableValuedFunctionParameters:
				objectName = "Parameters";
				break;
			case NodeType.ScalarValuedFunctionParameters:
				objectName = "Parameters";
				break;
			case NodeType.TableColumn:
				copyOption = true;
				break;
			case NodeType.TableKey:
				copyOption = true;
				break;
			case NodeType.TableConstraint:
				copyOption = true;
				break;
			case NodeType.TableTrigger:
				copyOption = true;
				break;
			case NodeType.TableIndex:
				copyOption = true;
				break;
			case NodeType.ViewColumn:
				copyOption = true;
				break;
			case NodeType.ViewTrigger:
				copyOption = true;
				break;
			case NodeType.ViewIndex:
				copyOption = true;
				break;
			case NodeType.StoredProcedureParameter:
				copyOption = true;
				break;
			case NodeType.TableValuedFunctionParameter:
				copyOption = true;
				break;
			case NodeType.ScalarValuedFunctionParameter:
				copyOption = true;
				break;
		}

		if (objectName != "")
		{
			exportObjectToolStripMenuItem.Visible = true;
			exportObjectToolStripMenuItem1.Visible = true;
			exportObjectToolStripMenuItem.Text = string.Format("Export {0} Descriptions...", objectName);
			exportObjectToolStripMenuItem1.Text = string.Format("Export {0} Descriptions...", objectName);

			importObjectToolStripMenuItem.Visible = true;
			importObjectToolStripMenuItem1.Visible = true;
			importObjectToolStripMenuItem.Text = string.Format("Import {0} Descriptions...", objectName);
			importObjectToolStripMenuItem1.Text = string.Format("Import {0} Descriptions...", objectName);

			editToolStripSeparator.Visible = true;
			editToolStripSeparator1.Visible = true;
			toolStripSeparator6.Visible = true;
			toolStripSeparator7.Visible = true;

			searchInSubNodesToolStripMenuItem.Visible = true;
			searchInSubNodesToolStripMenuItem1.Visible = true;
			searchInSubNodesToolStripMenuItem.Text = string.Format("Search in {0}...", objectName);
			searchInSubNodesToolStripMenuItem1.Text = string.Format("Search in {0}...", objectName);

			deleteDescriptionsInSubNodesToolStripMenuItem.Visible = true;
			deleteDescriptionsInSubNodesToolStripMenuItem1.Visible = true;
			deleteDescriptionsInSubNodesToolStripMenuItem.Text = string.Format("Delete Descriptions {0}...", objectName);
			deleteDescriptionsInSubNodesToolStripMenuItem1.Text = string.Format("Delete Descriptions in {0}...", objectName);
		}
		else
		{
			exportObjectToolStripMenuItem.Visible = false;
			exportObjectToolStripMenuItem1.Visible = false;
			importObjectToolStripMenuItem.Visible = false;
			importObjectToolStripMenuItem1.Visible = false;
			editToolStripSeparator.Visible = false;
			editToolStripSeparator1.Visible = false;
			searchInSubNodesToolStripMenuItem.Visible = false;
			searchInSubNodesToolStripMenuItem1.Visible = false;
			deleteDescriptionsInSubNodesToolStripMenuItem.Visible = false;
			deleteDescriptionsInSubNodesToolStripMenuItem1.Visible = false;
			toolStripSeparator6.Visible = false;
			toolStripSeparator7.Visible = false;
		}

		if (copyOption)
		{
			if (ConfigHandler.DescriptionFields.Count > 0)
			{
				copySQLToToolStripMenuItem.Visible = true;
				copySQLToToolStripMenuItem1.Visible = true;
				copySQLToToolStripMenuItem.Enabled = true;
				copySQLToToolStripMenuItem1.Enabled = true;
				copySQLToToolStripSeparator.Visible = true;
				copySQLToToolStripSeparator1.Visible = true;
				GenerateCopySqlScriptMenu();
			}
			else
			{
				copySQLToToolStripMenuItem.Enabled = false;
				copySQLToToolStripMenuItem1.Enabled = false;
				copySQLToToolStripSeparator.Visible = false;
				copySQLToToolStripSeparator1.Visible = false;
			}
		}
		else
		{
			copySQLToToolStripMenuItem.Visible = false;
			copySQLToToolStripMenuItem1.Visible = false;
			copySQLToToolStripSeparator.Visible = false;
			copySQLToToolStripSeparator1.Visible = false;
		}
	}

	private void GenerateCopySqlScriptMenu()
	{
		DoGenerateCopySqlScriptMenu(copySQLToToolStripMenuItem);
		DoGenerateCopySqlScriptMenu(copySQLToToolStripMenuItem1);
	}

	private void DoGenerateCopySqlScriptMenu(ToolStripMenuItem toolStripMenuItem)
	{
		toolStripMenuItem.DropDownItems.Clear();

		ToolStripMenuItem subMenuItem = new ToolStripMenuItem();
		subMenuItem.Name = "All";
		subMenuItem.Text = "All";
		subMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
		subMenuItem.Click += CopyAllSqlSubMenuItem_Click;

		toolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { subMenuItem, new ToolStripSeparator() });

		foreach (DescriptionField descriptionField in ConfigHandler.DescriptionFields)
		{
			subMenuItem = new ToolStripMenuItem();
			subMenuItem.Name = descriptionField.DisplayText;
			subMenuItem.Text = descriptionField.DisplayText;
			subMenuItem.Click += CopySqlSubMenuItem_Click;

			toolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { subMenuItem });
		}
	}

	private void CopyAllSqlSubMenuItem_Click(object sender, EventArgs e)
	{
		ToolStripMenuItem subMenuItem = (ToolStripMenuItem)sender;
		string descriptionField = subMenuItem.Name;
		CustomNode clickedNode = _selectedNode;

		GenerateCopySqlScript(clickedNode, descriptionField, true);
	}

	private void CopyAllSql()
	{
		if (_selectedNode.DescriptionChangeable)
		{
			GenerateCopySqlScript(_selectedNode, null, true);
		}
	}

	private void CopySqlSubMenuItem_Click(object sender, EventArgs e)
	{
		ToolStripMenuItem subMenuItem = (ToolStripMenuItem)sender;
		string descriptionField = subMenuItem.Name;
		CustomNode clickedNode = _selectedNode;

		GenerateCopySqlScript(clickedNode, descriptionField, false);
	}

	private static void GenerateCopySqlScript(CustomNode clickedNode, string descriptionField, bool all)
	{
		StringBuilder stringBuilder = new StringBuilder();

		foreach (DescriptionItem item in clickedNode.Descriptions)
		{
			if ((item.Type.DisplayText == descriptionField || all) && item.Description != null)
			{
				string databaseFieldName = item.Type.DatabaseFieldName;
				string sql = string.Format("if exists ({0})\r\nbegin\r\n\t{1}\r\nend\r\nelse\r\nbegin\r\n\t{2}\r\nend", ExtendedPropertiesHelper.GetCheckSql(clickedNode, databaseFieldName), ExtendedPropertiesHelper.GetUpdateSql(clickedNode, item.Description, databaseFieldName), ExtendedPropertiesHelper.GetAddSql(clickedNode, item.Description, databaseFieldName));
				sql = CheckWrapper.GetCheckWrapper(clickedNode, sql);
				stringBuilder.Append(string.Format("{0}\r\n", sql));
			}
		}

		string copyText = stringBuilder.ToString();

		if (copyText != "")
		{
			Thread newThread = new Thread(ThreadMethod);
			newThread.SetApartmentState(ApartmentState.STA);
			newThread.Start(copyText);
		}
	}

	private static void ThreadMethod(object text)
	{
		Clipboard.SetText(text.ToString());
	}

	private void ExportObjectToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Export();
	}

	private void Export()
	{
		if (_selectedNode.Type == NodeType.Database)
		{
			DoExport();
		}
		else
		{
			DoExport(_selectedNode);
		}
	}

	private void EditObjectToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Edit();
	}

	private void ImportObjectToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Import();
	}

	private void Import()
	{
		if (_selectedNode.Type == NodeType.Database)
		{
			DoImport();
		}
		else
		{
			DoImport(_selectedNode);
		}
	}

	private void SplitContainer2_Paint(object sender, PaintEventArgs e)
	{
		if (_showDdlScript)
		{
			SplitContainerGrip.PaintGrip(sender, e);
		}
	}

	private void SplitContainer2_MouseUp(object sender, MouseEventArgs e)
	{
		if (splitContainer2.CanFocus)
		{
			ActiveControl = tabControl1;
		}
	}

	private void SplitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
	{
		if (_showDdlScript)
		{
			ConfigHandler.SplitterDistanceHorizontal = splitContainer2.SplitterDistance.ToString();
			ConfigHandler.SaveConfig();
		}
	}

	private void DdlContextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
	{
		if (ddlTextEditorControl1.Document.UndoStack.CanUndo)
		{
			undoToolStripMenuItem.Enabled = true;
		}
		else
		{
			undoToolStripMenuItem.Enabled = false;
		}

		if (ddlTextEditorControl1.Document.UndoStack.CanRedo)
		{
			redoToolStripMenuItem.Enabled = true;
		}
		else
		{
			redoToolStripMenuItem.Enabled = false;
		}
	}

	private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ddlTextEditorControl1.Undo();
	}

	private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ddlTextEditorControl1.Redo();
	}

	private void CutToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ddlTextEditorControl1.ActiveTextAreaControl.TextArea.ClipboardHandler.Cut(sender, e);
	}

	private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ddlTextEditorControl1.ActiveTextAreaControl.TextArea.ClipboardHandler.Copy(sender, e);
	}

	private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ddlTextEditorControl1.ActiveTextAreaControl.TextArea.ClipboardHandler.Paste(sender, e);
	}

	private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ddlTextEditorControl1.ActiveTextAreaControl.TextArea.ClipboardHandler.Delete(sender, e);
	}

	private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
	{
		SelectAll(ddlTextEditorControl1);
	}

	private static void SelectAll(TextEditorControl textEditorControl)
	{
		TextLocation startPosition = new TextLocation(0, 0);

		int textLength = textEditorControl.ActiveTextAreaControl.Document.TextLength;
		TextLocation endPosition = new TextLocation();
		endPosition.Column = textEditorControl.Document.OffsetToPosition(textLength).Column;
		endPosition.Line = textEditorControl.Document.OffsetToPosition(textLength).Line;

		textEditorControl.ActiveTextAreaControl.SelectionManager.SetSelection(startPosition, endPosition);
		textEditorControl.ActiveTextAreaControl.Caret.Position = endPosition;
	}

	private void FontTabControlToolStripButton_Click(object sender, EventArgs e)
	{
		try
		{
			Font font = new Font(ConfigHandler.DescriptionFontFamily, float.Parse(ConfigHandler.DescriptionFontSize));
			fontDialog1.Font = font;
			fontDialog1.ShowDialog();

			if (fontDialog1.Font.Bold || fontDialog1.Font.Italic)
			{
				MessageBox.Show("Bold and Italic fonts are not supported.", ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			SetDescriptionFont(fontDialog1.Font);

			string familyName = fontDialog1.Font.FontFamily.Name;
			string size = fontDialog1.Font.Size.ToString();

			ConfigHandler.DescriptionFontFamily = familyName;
			ConfigHandler.DescriptionFontSize = size;
			ConfigHandler.SaveConfig();
		}
		catch
		{
			MessageBox.Show("New fonts added to Windows while Database Description Tool is running can't be added due to an error in Windows.\r\n\r\nTo add this font, restart Database Description Tool.", ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}

	private void FontDdlToolStripButton_Click(object sender, EventArgs e)
	{
		try
		{
			Font font = new Font(ConfigHandler.TextDataFontFamily, float.Parse(ConfigHandler.TextDataFontSize));
			fontDialog1.Font = font;
			fontDialog1.ShowDialog();

			if (fontDialog1.Font.Bold || fontDialog1.Font.Italic)
			{
				MessageBox.Show("Bold and Italic fonts are not supported.", ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			SetTextDataFont(fontDialog1.Font);

			string familyName = fontDialog1.Font.FontFamily.Name;
			string size = fontDialog1.Font.Size.ToString();

			ConfigHandler.TextDataFontFamily = familyName;
			ConfigHandler.TextDataFontSize = size;
			ConfigHandler.SaveConfig();
		}
		catch
		{
			MessageBox.Show("New fonts added to Windows while Database Description Tool is running can't be added due to an error in Windows.\r\n\r\nTo add this font, restart Database Description Tool.", ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}

	private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
	{
		RefreshNode();
	}

	private void FindDdlToolStripButton_Click(object sender, EventArgs e)
	{
		SearchDdl();
	}

	private void SearchDdl()
	{
		if (_searchTextForm.IsShown())
		{
			_searchTextForm.Activate();
		}
		else
		{
			if (ddlTextEditorControl1.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
			{
				if (!ddlTextEditorControl1.ActiveTextAreaControl.SelectionManager.SelectedText.Contains("\r"))
				{
					_searchTextForm.SetSearchTerm(ddlTextEditorControl1.ActiveTextAreaControl.SelectionManager.SelectedText);
				}
			}

			_searchTextForm.Hide();
			_searchTextForm.Show(this);
		}
	}

	private void SearchTextForm_SearchEvent(int foundIndex, string searchTerm)
	{
		_caretPositionChangeFromSearch = true;

		TextLocation startPosition = ddlTextEditorControl1.Document.OffsetToPosition(foundIndex);
		TextLocation endPosition = ddlTextEditorControl1.Document.OffsetToPosition(foundIndex + searchTerm.Length);

		ddlTextEditorControl1.ActiveTextAreaControl.SelectionManager.SetSelection(startPosition, endPosition);
		ddlTextEditorControl1.ActiveTextAreaControl.CenterViewOn(ddlTextEditorControl1.Document.GetLineNumberForOffset(foundIndex), 0);

		ddlTextEditorControl1.ActiveTextAreaControl.Caret.Line = endPosition.Line;
		ddlTextEditorControl1.ActiveTextAreaControl.Caret.Column = endPosition.Column;

		_caretPositionChangeFromSearch = false;
	}

	private void Caret_PositionChanged(object sender, EventArgs e)
	{
		Caret caret = (Caret)sender;

		if (!_caretPositionChangeFromSearch)
		{
			_searchTextForm.Reset(caret.Offset);
		}
	}

	private void DdlTextEditorControl1_TextChanged(object sender, EventArgs e)
	{
		_searchTextForm.SetSearchText(ddlTextEditorControl1.Text);
		_searchTextForm.Reset(ddlTextEditorControl1.ActiveTextAreaControl.Caret.Offset);
	}

	private void EditObjectToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		Edit();
	}

	private void ImportObjectToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		Import();
	}

	private void ExportObjectToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		Export();
	}

	private void ChangeConnectionToolStripButton_Click(object sender, EventArgs e)
	{
		ChangeConnection();
	}

	private void RefreshTreeToolStripButton_Click(object sender, EventArgs e)
	{
		RefreshNode();
	}

	private void EditToolStripButton_Click(object sender, EventArgs e)
	{
		Edit();
	}

	private void CheckForupdatesToolStripMenuItem_Click(object sender, EventArgs e)
	{
		CheckForUpdatesForm form = new CheckForUpdatesForm();
		form.ShowDialog();
	}

	private void EditToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
	{
		if (insertDescriptionTemplateToolStripMenuItem.DropDownItems.Count > 0 && editButton.Enabled)
		{
			insertDescriptionTemplateToolStripMenuItem.Enabled = true;
		}
		else
		{
			insertDescriptionTemplateToolStripMenuItem.Enabled = false;
		}
	}

	private void SearchToolStripButton_Click(object sender, EventArgs e)
	{
		SearchObjects();
	}

	private void SearchObjects()
	{
		_searchListForm.ResetObjectSelector();
		ShowSearchObjectsForm();
	}

	private void ShowSearchObjectsForm()
	{
		if (_searchListForm.IsShown())
		{
			_searchListForm.Activate();
		}
		else
		{
			SetSearchStartPosition(_selectedNode);
			_searchListForm.Hide();
			_searchListForm.Show(this);
		}
	}

	private void SearchListForm_RequestUpdateListEvent(string searchTerm, bool name, bool description, ObjectsSelectorForm objectsSelectorForm, bool matchWholeWord, bool matchCase)
	{
		DataSet matches = _databaseOperation.GetMatches(searchTerm, name, description, objectsSelectorForm, matchWholeWord, matchCase);

		List<SearchListForm.SearchListObject> searchListObjects = new List<SearchListForm.SearchListObject>();

		foreach (DataRow dataRow in matches.Tables[0].Rows)
		{
			int id = Convert.ToInt32(dataRow["id"]);
			bool match = Convert.ToBoolean(dataRow["Match"]);
			NodeType level1Type = SearchTreeHandler.StringToNodeType(dataRow["Level1Type"].ToString(), null);
			string level1Name = dataRow["Level1Name"].ToString();
			NodeType level2Type = SearchTreeHandler.StringToNodeType(dataRow["Level1Type"].ToString(), dataRow["Level2Type"].ToString());
			string level2Name = dataRow["Level2Name"].ToString();

			searchListObjects.Add(new SearchListForm.SearchListObject(id, match, level1Type, level1Name, level2Type, level2Name));
		}

		int matchCount = Convert.ToInt32(matches.Tables[1].Rows[0]["MatchCount"].ToString());

		_searchListForm.SetSearchList(searchListObjects, matchCount);
	}

	private void SearchListForm_SearchEvent(SearchListForm.SearchListObject foundObject)
	{
		_selectionChangeFromSearch = true;

		CustomNode foundNode = _treeViewHandler.GoToNode(_databaseOperation.GetDatabaseName(), foundObject.Level1Type, foundObject.Level1Name, foundObject.Level2Type, foundObject.Level2Name);

		if (foundNode == null)
		{
			MessageBox.Show("Database schema has changed while searching.\r\n\r\nPlease perform the search again.", ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			_searchListForm.Close();
			_searchListForm.InvokeNewSearch();
		}
		else
		{
			SetNode(foundNode);
		}

		_selectionChangeFromSearch = false;
	}

	private void ObjectDefinitionScriptToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!_showDdlScript && objectDefinitionScriptToolStripMenuItem.Checked)
		{
			_ddlHandler.Prefetch();
			SetDDL();
		}

		_showDdlScript = objectDefinitionScriptToolStripMenuItem.Checked;

		if (_searchTextForm.IsShown())
		{
			_searchTextForm.Hide();
		}

		splitContainer2.Panel2Collapsed = !objectDefinitionScriptToolStripMenuItem.Checked;

		if (objectDefinitionScriptToolStripMenuItem.Checked)
		{
			ConfigHandler.ShowDdlScript = "True";
		}
		else
		{
			ConfigHandler.ShowDdlScript = "False";
		}

		ConfigHandler.SaveConfig();
	}

	private void SearchInSubNodesToolStripMenuItem_Click(object sender, EventArgs e)
	{
		SearchInSubNodes();
	}

	private void SearchInSubNodesToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		SearchInSubNodes();
	}

	private void SearchInSubNodes()
	{
		_searchListForm.SetIndividualObjects(_selectedNode);
		ShowSearchObjectsForm();
	}

	private void ResetWindowSizeToolStripMenuItem_Click(object sender, EventArgs e)
	{
		splitContainer1.SplitterDistance = 216;
		splitContainer2.SplitterDistance = 200;

		Size = new Size(822, 555);
	}

	private void DeleteDescriptionsInSubNodesToolStripMenuItem_Click(object sender, EventArgs e)
	{
		DeleteDescriptionsInSubNodes();
	}

	private void DeleteDescriptionsInSubNodesToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		DeleteDescriptionsInSubNodes();
	}

	private void DeleteDescriptionsInSubNodes()
	{
		_databaseOperation.BeginCommEvent -= BeginCommEvent;
		_databaseOperation.EndCommEvent -= EndCommEvent;
		DeleteDescriptionsInSubNodes form = new DeleteDescriptionsInSubNodes(_databaseOperation);
		form.SetIndividualObjects(_selectedNode);
		form.ShowDialog();

		if (form.GetDeleted())
		{
			ReloadTreeView();
		}

		_databaseOperation.BeginCommEvent += BeginCommEvent;
		_databaseOperation.EndCommEvent += EndCommEvent;
	}

	private void SchemaToolStripMenuItem_Click(object sender, EventArgs e)
	{
		foreach (ToolStripMenuItem toolStripMenu in useSchemaToolStripMenuItem.DropDownItems)
		{
			toolStripMenu.Checked = false;
		}

		ToolStripMenuItem selectedToolStripMenu = (ToolStripMenuItem)sender;
		selectedToolStripMenu.Checked = true;

		ConfigHandler.SchemaName = selectedToolStripMenu.Text;
		ConfigHandler.SaveConfig();

		ReloadTreeView();
	}
}
