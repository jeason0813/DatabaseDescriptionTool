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

using System.Collections;
using System.Windows.Forms;

public class TreeViewHandler
{
	public delegate void PrefetchDdlEventHandler(CustomNode node);
	public event PrefetchDdlEventHandler PrefetchDdlEvent;

	private readonly DatabaseOperation _databaseOperation;
	private string _currentDatabase;
	private readonly TreeView _treeView;

	public TreeViewHandler(DatabaseOperation databaseOperation, TreeView treeView)
	{
		_databaseOperation = databaseOperation;
		_currentDatabase = null;
		_treeView = treeView;

		_treeView.AfterExpand += TreeView_AfterExpand;
		_treeView.KeyDown += TreeView_KeyDown;
		_treeView.BeforeSelect += TreeView_BeforeSelect;

		_treeView.TreeViewNodeSorter = new NodeSorter();
	}

	public CustomNode GoToNode(string databaseName, NodeType level1Type, string level1Name, NodeType level2Type, string level2Name)
	{
		return NavigateTree.GoToNode(_treeView, databaseName, level1Type, level1Name, level2Type, level2Name);
	}

	public void RefreshNode(CustomNode node)
	{
		if (node.Type == NodeType.Database || node.Type == NodeType.Programmability || node.Type == NodeType.Functions)
		{
			foreach (CustomNode child in node.Nodes)
			{
				child.Collapse();
			}
		}
		else
		{
			GenerateChildNodes(node);
		}
	}

	public void Reload()
	{
		_treeView.Nodes.Clear();
		GenerateImageList(_treeView);
		GenerateServerNode(_treeView);
	}

	public static CustomNode GetDatabaseNode(CustomNode nodeItem)
	{
		CustomNode nodeAfterRootNode = new CustomNode(NodeType.Database);

		while (nodeItem.Parent != null)
		{
			nodeAfterRootNode = nodeItem;
			nodeItem = (CustomNode)nodeItem.Parent;
		}

		return nodeAfterRootNode;
	}

	public void CheckDatabase(CustomNode node)
	{
		string database = GetDatabaseNode(node).Text;

		if (database != _currentDatabase)
		{
			if (database != "")
			{
				_databaseOperation.SetDatabase(database);
			}
			else
			{
				_databaseOperation.SetDatabase("master");
			}

			_currentDatabase = database;
		}
	}

	private void TreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
	{
		CheckDatabase((CustomNode)e.Node);
	}

	private class NodeSorter : IComparer
	{
		public int Compare(object x, object y)
		{
			CustomNode tx = (CustomNode)x;
			CustomNode ty = (CustomNode)y;

			return tx.OrderBy - ty.OrderBy;
		}
	}

	private static void GenerateServerNode(TreeView treeView)
	{
		CustomNode serverNode = new CustomNode(NodeType.Server, null, ConfigHandler.ServerName, NodeImage.ServerDatabase, 0);
		serverNode.Nodes.Add(new CustomNode(NodeType.Dummy));
		serverNode.Expand();
		treeView.Nodes.Add(serverNode);
	}

	private static void TreeView_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Multiply)
		{
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void TreeView_AfterExpand(object sender, TreeViewEventArgs e)
	{
		CustomNode node = (CustomNode)e.Node;
		CheckDatabase(node);
		GenerateChildNodes(node);
		FirePrefetchDdlEvent(node);
	}

	private void FirePrefetchDdlEvent(CustomNode node)
	{
		if (PrefetchDdlEvent != null)
		{
			PrefetchDdlEvent(node);
		}
	}

	private void GenerateChildNodes(CustomNode node)
	{
		switch (node.Type)
		{
			case NodeType.Server:
				NodeDataGenerator.GenerateDatabasesNode(node, _databaseOperation);
				break;
			case NodeType.Tables:
				NodeDataGenerator.GenerateTablesNode(node, _databaseOperation);
				break;
			case NodeType.Views:
				NodeDataGenerator.GenerateViewsNode(node, _databaseOperation);
				break;
			case NodeType.StoredProcedures:
				NodeDataGenerator.GenerateProceduresNode(node, _databaseOperation);
				break;
			case NodeType.TableValuedFunctions:
				NodeDataGenerator.GenerateTableValuedFunctionsNode(node, _databaseOperation);
				break;
			case NodeType.ScalarValuedFunctions:
				NodeDataGenerator.GenerateScalarValuedFunctionsNode(node, _databaseOperation);
				break;
			case NodeType.TableColumns:
				NodeDataGenerator.GenerateTableColumnsNode(node, node.ParentNode.Text, _databaseOperation);
				break;
			case NodeType.ViewColumns:
				NodeDataGenerator.GenerateViewColumnsNode(node, node.ParentNode.Text, _databaseOperation);
				break;
			case NodeType.TableKeys:
				NodeDataGenerator.GenerateTableKeysNode(node, node.ParentNode.Text, _databaseOperation);
				break;
			case NodeType.TableConstraints:
				NodeDataGenerator.GenerateTableConstraintsNode(node, node.ParentNode.Text, _databaseOperation);
				break;
			case NodeType.TableTriggers:
				NodeDataGenerator.GenerateTableTriggersNode(node, node.ParentNode.Text, _databaseOperation);
				break;
			case NodeType.TableIndexes:
				NodeDataGenerator.GenerateTableIndexesNode(node, node.ParentNode.Text, _databaseOperation);
				break;
			case NodeType.ViewTriggers:
				NodeDataGenerator.GenerateViewTriggersNode(node, node.ParentNode.Text, _databaseOperation);
				break;
			case NodeType.ViewIndexes:
				NodeDataGenerator.GenerateViewIndexesNode(node, node.ParentNode.Text, _databaseOperation);
				break;
			case NodeType.TableValuedFunctionParameters:
				NodeDataGenerator.GenerateTableValuedFunctionParametersNode(node, node.ParentNode.Text, _databaseOperation);
				break;
			case NodeType.ScalarValuedFunctionParameters:
				NodeDataGenerator.GenerateScalarValuedFunctionParametersNode(node, node.ParentNode.Text, _databaseOperation);
				break;
			case NodeType.StoredProcedureParameters:
				NodeDataGenerator.GenerateStoredProcedureParametersNode(node, node.ParentNode.Text, _databaseOperation);
				break;
		}
	}

	private static void GenerateImageList(TreeView treeView)
	{
		ImageList imageList = new ImageList();
		imageList.Images.Add(DatabaseDescriptionTool.Properties.Resources.database);
		imageList.Images.Add(DatabaseDescriptionTool.Properties.Resources.folder);
		imageList.Images.Add(DatabaseDescriptionTool.Properties.Resources.book);
		imageList.Images.Add(DatabaseDescriptionTool.Properties.Resources.server_database);
		imageList.Images.Add(DatabaseDescriptionTool.Properties.Resources.book_add);
		treeView.ImageList = imageList;
	}
}
