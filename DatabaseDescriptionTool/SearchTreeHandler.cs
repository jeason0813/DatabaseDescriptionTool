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

using System.Data;

public class SearchTreeHandler
{
	public static int CustomNodeToIndex(DatabaseOperation databaseOperation, CustomNode node)
	{
		string level1Type = GetLevel1NodeTypeString(node.Type);
		string level1Name;
		string level2Type = GetLevel2NodeTypeString(node.Type);
		string level2Name = "";

		if (node.Type == NodeType.Database || node.Type == NodeType.Tables || node.Type == NodeType.Views || node.Type == NodeType.Programmability || node.Type == NodeType.StoredProcedures || node.Type == NodeType.Functions || node.Type == NodeType.TableValuedFunctions || node.Type == NodeType.ScalarValuedFunctions)
		{
			if (node.Type == NodeType.Database || node.Type == NodeType.Programmability || node.Type == NodeType.Functions)
			{
				level1Name = GetFirstLevel1NodeName(databaseOperation, (CustomNode)node.Nodes[0]);
			}
			else
			{
				level1Name = GetFirstLevel1NodeName(databaseOperation, node);
			}
		}
		else
		{
			if (node.Type == NodeType.TableColumns || node.Type == NodeType.TableKeys || node.Type == NodeType.TableConstraints || node.Type == NodeType.TableTriggers || node.Type == NodeType.TableIndexes || node.Type == NodeType.ViewColumns || node.Type == NodeType.ViewTriggers || node.Type == NodeType.ViewIndexes || node.Type == NodeType.StoredProcedureParameters || node.Type == NodeType.TableValuedFunctionParameters || node.Type == NodeType.ScalarValuedFunctionParameters)
			{
				level1Name = ExtendedPropertiesHelper.GetLevel1Name(node.ParentNode);
				level2Name = GetFirstLevel2NodeName(databaseOperation, node);
			}
			else
			{
				level1Name = ExtendedPropertiesHelper.GetLevel1Name(node);

				if (ExtendedPropertiesHelper.AddLevel2(node.Type))
				{
					level2Name = ExtendedPropertiesHelper.GetLevel2Name(node);
				}
			}
		}

		int treeId = databaseOperation.GetIdFromName(level1Type, level1Name, level2Type, level2Name);

		return treeId - 1;
	}

	public static NodeType StringToNodeType(string Level1Name, string level2Name)
	{
		switch (Level1Name)
		{
			case "Table":
				switch (level2Name)
				{
					case "Column":
						return NodeType.TableColumn;
					case "Key":
						return NodeType.TableKey;
					case "Constraint":
						return NodeType.TableConstraint;
					case "Trigger":
						return NodeType.TableTrigger;
					case "Index":
						return NodeType.TableIndex;
					case "":
						return NodeType.Dummy;
				}

				return NodeType.Table;
			case "View":
				switch (level2Name)
				{
					case "Column":
						return NodeType.ViewColumn;
					case "Trigger":
						return NodeType.ViewTrigger;
					case "Index":
						return NodeType.ViewIndex;
					case "":
						return NodeType.Dummy;
				}

				return NodeType.View;
			case "Procedure":
				switch (level2Name)
				{
					case "Parameter":
						return NodeType.StoredProcedureParameter;
					case "":
						return NodeType.Dummy;
				}

				return NodeType.StoredProcedure;
			case "Table-valued Function":
				switch (level2Name)
				{
					case "Parameter":
						return NodeType.TableValuedFunctionParameter;
					case "":
						return NodeType.Dummy;
				}

				return NodeType.TableValuedFunction;
			case "Scalar-valued Function":
				switch (level2Name)
				{
					case "Parameter":
						return NodeType.ScalarValuedFunctionParameter;
					case "":
						return NodeType.Dummy;
				}

				return NodeType.ScalarValuedFunction;
		}

		return NodeType.Dummy;
	}

	private static string GetFirstLevel1NodeName(DatabaseOperation databaseOperation, CustomNode node)
	{
		if (node.IsExpanded && node.Nodes.Count > 0)
		{
			return node.Nodes[0].Text;
		}
		else
		{
			if (node.Type == NodeType.Tables)
			{
				DataTable dt = databaseOperation.GetTableNames();

				if (dt.Rows.Count > 0)
				{
					return dt.Rows[0]["name"].ToString();
				}
			}
			else if (node.Type == NodeType.Views)
			{
				DataTable dt = databaseOperation.GetViewNames();

				if (dt.Rows.Count > 0)
				{
					return dt.Rows[0]["name"].ToString();
				}
			}
			else if (node.Type == NodeType.StoredProcedures)
			{
				DataTable dt = databaseOperation.GetProcedureNames();

				if (dt.Rows.Count > 0)
				{
					return dt.Rows[0]["name"].ToString();
				}
			}
			else if (node.Type == NodeType.TableValuedFunctions)
			{
				DataTable dt = databaseOperation.GetTableValuedFunctionNames();

				if (dt.Rows.Count > 0)
				{
					return dt.Rows[0]["name"].ToString();
				}
			}
			else if (node.Type == NodeType.ScalarValuedFunctions)
			{
				DataTable dt = databaseOperation.GetScalarValuedFunctionNames();

				if (dt.Rows.Count > 0)
				{
					return dt.Rows[0]["name"].ToString();
				}
			}
		}

		return "";
	}

	private static string GetFirstLevel2NodeName(DatabaseOperation databaseOperation, CustomNode node)
	{
		if (node.IsExpanded && node.Nodes.Count > 0)
		{
			return node.Nodes[0].Text;
		}
		else
		{
			if (node.Type == NodeType.TableColumns)
			{
				DataTable dt = databaseOperation.GetTableColumns(node.ParentNode.Name);

				if (dt.Rows.Count > 0)
				{
					return dt.Rows[0]["name"].ToString();
				}
			}
			else if (node.Type == NodeType.TableKeys)
			{
				DataTable dt = databaseOperation.GetTableKeys(node.ParentNode.Name);

				if (dt.Rows.Count > 0)
				{
					return dt.Rows[0]["name"].ToString();
				}
			}
			else if (node.Type == NodeType.TableConstraints)
			{
				DataTable dt = databaseOperation.GetTableConstraints(node.ParentNode.Name);

				if (dt.Rows.Count > 0)
				{
					return dt.Rows[0]["name"].ToString();
				}
			}
			else if (node.Type == NodeType.TableTriggers)
			{
				DataTable dt = databaseOperation.GetTableTriggers(node.ParentNode.Name);

				if (dt.Rows.Count > 0)
				{
					return dt.Rows[0]["name"].ToString();
				}
			}
			else if (node.Type == NodeType.TableIndexes)
			{
				DataTable dt = databaseOperation.GetTableIndexes(node.ParentNode.Name);

				if (dt.Rows.Count > 0)
				{
					return dt.Rows[0]["name"].ToString();
				}
			}
			else if (node.Type == NodeType.ViewColumns)
			{
				DataTable dt = databaseOperation.GetViewColumns(node.ParentNode.Name);

				if (dt.Rows.Count > 0)
				{
					return dt.Rows[0]["name"].ToString();
				}
			}
			else if (node.Type == NodeType.ViewTriggers)
			{
				DataTable dt = databaseOperation.GetViewTriggers(node.ParentNode.Name);

				if (dt.Rows.Count > 0)
				{
					return dt.Rows[0]["name"].ToString();
				}
			}
			else if (node.Type == NodeType.ViewIndexes)
			{
				DataTable dt = databaseOperation.GetViewIndexes(node.ParentNode.Name);

				if (dt.Rows.Count > 0)
				{
					return dt.Rows[0]["name"].ToString();
				}
			}
			else if (node.Type == NodeType.StoredProcedureParameters)
			{
				DataTable dt = databaseOperation.GetStoredProcedureParameters(node.ParentNode.Name);

				if (dt.Rows.Count > 0)
				{
					return dt.Rows[0]["name"].ToString();
				}
			}
			else if (node.Type == NodeType.TableValuedFunctionParameters)
			{
				DataTable dt = databaseOperation.GetTableValuedFunctionParameters(node.ParentNode.Name);

				if (dt.Rows.Count > 0)
				{
					return dt.Rows[0]["name"].ToString();
				}
			}
			else if (node.Type == NodeType.ScalarValuedFunctionParameters)
			{
				DataTable dt = databaseOperation.GetScalarValuedFunctionParameters(node.ParentNode.Name);

				if (dt.Rows.Count > 0)
				{
					return dt.Rows[0]["name"].ToString();
				}
			}
		}

		return "";
	}

	private static string GetLevel1NodeTypeString(NodeType nodeType)
	{
		string level1Type = "";

		switch (nodeType)
		{
			case NodeType.Database:
			case NodeType.Tables:
			case NodeType.Table:
			case NodeType.TableColumns:
			case NodeType.TableColumn:
			case NodeType.TableKeys:
			case NodeType.TableKey:
			case NodeType.TableConstraints:
			case NodeType.TableConstraint:
			case NodeType.TableTriggers:
			case NodeType.TableTrigger:
			case NodeType.TableIndexes:
			case NodeType.TableIndex:
				level1Type = "Table";
				break;
			case NodeType.Views:
			case NodeType.View:
			case NodeType.ViewColumns:
			case NodeType.ViewColumn:
			case NodeType.ViewTriggers:
			case NodeType.ViewTrigger:
			case NodeType.ViewIndexes:
			case NodeType.ViewIndex:
				level1Type = "View";
				break;
			case NodeType.Programmability:
			case NodeType.StoredProcedures:
			case NodeType.StoredProcedure:
			case NodeType.StoredProcedureParameters:
			case NodeType.StoredProcedureParameter:
				level1Type = "Procedure";
				break;
			case NodeType.Functions:
			case NodeType.TableValuedFunctions:
			case NodeType.TableValuedFunction:
			case NodeType.TableValuedFunctionParameters:
			case NodeType.TableValuedFunctionParameter:
				level1Type = "Table-valued Function";
				break;
			case NodeType.ScalarValuedFunctions:
			case NodeType.ScalarValuedFunction:
			case NodeType.ScalarValuedFunctionParameters:
			case NodeType.ScalarValuedFunctionParameter:
				level1Type = "Scalar-valued Function";
				break;
		}

		return level1Type;
	}

	private static string GetLevel2NodeTypeString(NodeType nodeType)
	{
		string level2Type = "";

		switch (nodeType)
		{
			case NodeType.TableTriggers:
			case NodeType.TableTrigger:
			case NodeType.ViewTriggers:
			case NodeType.ViewTrigger:
				level2Type = "Trigger";
				break;
			case NodeType.TableColumns:
			case NodeType.TableColumn:
			case NodeType.ViewColumns:
			case NodeType.ViewColumn:
				level2Type = "Column";
				break;
			case NodeType.TableKeys:
			case NodeType.TableKey:
				level2Type = "Key";
				break;
			case NodeType.TableConstraints:
			case NodeType.TableConstraint:
				level2Type = "Constraint";
				break;
			case NodeType.TableIndexes:
			case NodeType.TableIndex:
			case NodeType.ViewIndexes:
			case NodeType.ViewIndex:
				level2Type = "Index";
				break;
			case NodeType.StoredProcedureParameters:
			case NodeType.StoredProcedureParameter:
			case NodeType.TableValuedFunctionParameters:
			case NodeType.TableValuedFunctionParameter:
			case NodeType.ScalarValuedFunctionParameters:
			case NodeType.ScalarValuedFunctionParameter:
				level2Type = "Parameter";
				break;
		}

		return level2Type;
	}
}
