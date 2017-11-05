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

using System.Windows.Forms;

public static class NavigateTree
{
	public static CustomNode GoToNode(TreeView treeView, string databaseName, NodeType level1Type, string level1Name, NodeType level2Type, string level2Name)
	{
		CustomNode serverNode = (CustomNode)treeView.Nodes[0];

		foreach (CustomNode databaseNode in serverNode.Nodes)
		{
			if (databaseNode.Text == databaseName)
			{
				databaseNode.Expand();

				if (level1Type == NodeType.Table)
				{
					databaseNode.Nodes["Tables"].Expand();

					foreach (CustomNode tableNode in databaseNode.Nodes["Tables"].Nodes)
					{
						if (tableNode.Text == level1Name)
						{
							if (level2Type == NodeType.Dummy)
							{
								return tableNode;
							}
							else
							{
								tableNode.Expand();

								if (level2Type == NodeType.TableColumn)
								{
									tableNode.Nodes["Columns"].Expand();

									foreach (CustomNode columnNode in tableNode.Nodes["Columns"].Nodes)
									{
										if (columnNode.Text == level2Name)
										{
											return columnNode;
										}
									}
								}
								else if (level2Type == NodeType.TableKey)
								{
									tableNode.Nodes["Keys"].Expand();

									foreach (CustomNode keyNode in tableNode.Nodes["Keys"].Nodes)
									{
										if (keyNode.Text == level2Name)
										{
											return keyNode;
										}
									}
								}
								else if (level2Type == NodeType.TableConstraint)
								{
									tableNode.Nodes["Constraints"].Expand();

									foreach (CustomNode constraintNode in tableNode.Nodes["Constraints"].Nodes)
									{
										if (constraintNode.Text == level2Name)
										{
											return constraintNode;
										}
									}
								}
								else if (level2Type == NodeType.TableTrigger)
								{
									tableNode.Nodes["Triggers"].Expand();

									foreach (CustomNode triggerNode in tableNode.Nodes["Triggers"].Nodes)
									{
										if (triggerNode.Text == level2Name)
										{
											return triggerNode;
										}
									}
								}
								else if (level2Type == NodeType.TableIndex)
								{
									tableNode.Nodes["Indexes"].Expand();

									foreach (CustomNode indexNode in tableNode.Nodes["Indexes"].Nodes)
									{
										if (indexNode.Text == level2Name)
										{
											return indexNode;
										}
									}
								}
							}

							break;
						}
					}
				}
				else if (level1Type == NodeType.View)
				{
					databaseNode.Nodes["Views"].Expand();

					foreach (CustomNode viewNode in databaseNode.Nodes["Views"].Nodes)
					{
						if (viewNode.Text == level1Name)
						{
							if (level2Type == NodeType.Dummy)
							{
								return viewNode;
							}
							else
							{
								viewNode.Expand();

								if (level2Type == NodeType.ViewColumn)
								{
									viewNode.Nodes["Columns"].Expand();

									foreach (CustomNode columnNode in viewNode.Nodes["Columns"].Nodes)
									{
										if (columnNode.Text == level2Name)
										{
											return columnNode;
										}
									}
								}
								else if (level2Type == NodeType.ViewTrigger)
								{
									viewNode.Nodes["Triggers"].Expand();

									foreach (CustomNode triggerNode in viewNode.Nodes["Triggers"].Nodes)
									{
										if (triggerNode.Text == level2Name)
										{
											return triggerNode;
										}
									}
								}
								else if (level2Type == NodeType.ViewIndex)
								{
									viewNode.Nodes["Indexes"].Expand();

									foreach (CustomNode indexNode in viewNode.Nodes["Indexes"].Nodes)
									{
										if (indexNode.Text == level2Name)
										{
											return indexNode;
										}
									}
								}
							}

							break;
						}
					}
				}
				else if (level1Type == NodeType.StoredProcedure)
				{
					databaseNode.Nodes["Programmability"].Expand();
					databaseNode.Nodes["Programmability"].Nodes["Stored Procedures"].Expand();

					foreach (CustomNode procedureNode in databaseNode.Nodes["Programmability"].Nodes["Stored Procedures"].Nodes)
					{
						if (procedureNode.Text == level1Name)
						{
							if (level2Type == NodeType.Dummy)
							{
								return procedureNode;
							}
							else
							{
								procedureNode.Expand();

								if (level2Type == NodeType.StoredProcedureParameter)
								{
									procedureNode.Nodes["Parameters"].Expand();

									foreach (CustomNode parameterNode in procedureNode.Nodes["Parameters"].Nodes)
									{
										if (parameterNode.Text == level2Name)
										{
											return parameterNode;
										}
									}
								}
							}

							break;
						}
					}
				}
				else if (level1Type == NodeType.TableValuedFunction)
				{
					databaseNode.Nodes["Programmability"].Expand();
					databaseNode.Nodes["Programmability"].Nodes["Functions"].Expand();
					databaseNode.Nodes["Programmability"].Nodes["Functions"].Nodes["Table-valued Functions"].Expand();

					foreach (CustomNode tableValuedFunctionNode in databaseNode.Nodes["Programmability"].Nodes["Functions"].Nodes["Table-valued Functions"].Nodes)
					{
						if (tableValuedFunctionNode.Text == level1Name)
						{
							if (level2Type == NodeType.Dummy)
							{
								return tableValuedFunctionNode;
							}
							else
							{
								tableValuedFunctionNode.Expand();

								if (level2Type == NodeType.TableValuedFunctionParameter)
								{
									tableValuedFunctionNode.Nodes["Parameters"].Expand();

									foreach (CustomNode parameterNode in tableValuedFunctionNode.Nodes["Parameters"].Nodes)
									{
										if (parameterNode.Text == level2Name)
										{
											return parameterNode;
										}
									}
								}
							}

							break;
						}
					}
				}
				else if (level1Type == NodeType.ScalarValuedFunction)
				{
					databaseNode.Nodes["Programmability"].Expand();
					databaseNode.Nodes["Programmability"].Nodes["Functions"].Expand();
					databaseNode.Nodes["Programmability"].Nodes["Functions"].Nodes["Scalar-valued Functions"].Expand();

					foreach (CustomNode scalarValuedFunctionNode in databaseNode.Nodes["Programmability"].Nodes["Functions"].Nodes["Scalar-valued Functions"].Nodes)
					{
						if (scalarValuedFunctionNode.Text == level1Name)
						{
							if (level2Type == NodeType.Dummy)
							{
								return scalarValuedFunctionNode;
							}
							else
							{
								scalarValuedFunctionNode.Expand();

								if (level2Type == NodeType.ScalarValuedFunctionParameter)
								{
									scalarValuedFunctionNode.Nodes["Parameters"].Expand();

									foreach (CustomNode parameterNode in scalarValuedFunctionNode.Nodes["Parameters"].Nodes)
									{
										if (parameterNode.Text == level2Name)
										{
											return parameterNode;
										}
									}
								}
							}

							break;
						}
					}
				}

				break;
			}
		}

		return null;
	}
}
