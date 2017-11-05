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

public static class NodeDataGenerator
{
	public static void GenerateDatabasesNode(CustomNode parentNode, DatabaseOperation databaseOperation)
	{
		DataTable dt = databaseOperation.GetDatabases();
		parentNode.Nodes.Clear();

		int orderBy = 0;

		foreach (DataRow dr in dt.Rows)
		{
			orderBy++;
			GenerateDatabaseNode(parentNode, dr["name"].ToString(), orderBy);
		}
	}

	public static void GenerateDatabaseNode(CustomNode parentNode, string databaseName, int orderBy)
	{
		CustomNode databaseNode = new CustomNode(NodeType.Database, parentNode, databaseName, 0, orderBy);

		CustomNode tablesNode = new CustomNode(NodeType.Tables, databaseNode, "Tables", NodeImage.Folder, 1);
		CustomNode viewsNode = new CustomNode(NodeType.Views, databaseNode, "Views", NodeImage.Folder, 2);
		CustomNode programmabilityNode = new CustomNode(NodeType.Programmability, databaseNode, "Programmability", NodeImage.Folder, 3);
		CustomNode proceduresNode = new CustomNode(NodeType.StoredProcedures, programmabilityNode, "Stored Procedures", NodeImage.Folder, 1);
		CustomNode functionsNode = new CustomNode(NodeType.Functions, programmabilityNode, "Functions", NodeImage.Folder, 2);
		CustomNode tableValuedFunctionsNode = new CustomNode(NodeType.TableValuedFunctions, functionsNode, "Table-valued Functions", NodeImage.Folder, 1);
		CustomNode scalarValuedFunctionsNode = new CustomNode(NodeType.ScalarValuedFunctions, functionsNode, "Scalar-valued Functions", NodeImage.Folder, 2);

		tablesNode.Nodes.Add(new CustomNode(NodeType.Dummy));
		viewsNode.Nodes.Add(new CustomNode(NodeType.Dummy));
		proceduresNode.Nodes.Add(new CustomNode(NodeType.Dummy));
		tableValuedFunctionsNode.Nodes.Add(new CustomNode(NodeType.Dummy));
		scalarValuedFunctionsNode.Nodes.Add(new CustomNode(NodeType.Dummy));

		databaseNode.Nodes.Add(tablesNode);
		databaseNode.Nodes.Add(viewsNode);
		functionsNode.Nodes.Add(tableValuedFunctionsNode);
		functionsNode.Nodes.Add(scalarValuedFunctionsNode);
		programmabilityNode.Nodes.Add(proceduresNode);
		programmabilityNode.Nodes.Add(functionsNode);
		databaseNode.Nodes.Add(programmabilityNode);

		parentNode.Nodes.Add(databaseNode);
	}

	public static void GenerateTablesNode(CustomNode parentNode, DatabaseOperation databaseOperation)
	{
		DataTable dt = databaseOperation.GetTables();
		parentNode.Nodes.Clear();

		string previousName = null;

		foreach (DataRow dr in dt.Rows)
		{
			if (dr["name"].ToString() != previousName)
			{
				previousName = dr["name"].ToString();

				List<DescriptionItem> descriptions = GetDescriptions(dt, dr["name"].ToString());

				CustomNode tableNode = new CustomNode(NodeType.Table, parentNode, dr["name"].ToString(), descriptions, null);

				CustomNode tableColumnsNode = new CustomNode(NodeType.TableColumns, tableNode, "Columns", NodeImage.Folder, 1);
				CustomNode tableKeysNode = new CustomNode(NodeType.TableKeys, tableNode, "Keys", NodeImage.Folder, 2);
				CustomNode tableConstraintsNode = new CustomNode(NodeType.TableConstraints, tableNode, "Constraints", NodeImage.Folder, 3);
				CustomNode tableTriggersNode = new CustomNode(NodeType.TableTriggers, tableNode, "Triggers", NodeImage.Folder, 4);
				CustomNode tableIndexesNode = new CustomNode(NodeType.TableIndexes, tableNode, "Indexes", NodeImage.Folder, 5);

				tableColumnsNode.Nodes.Add(new CustomNode(NodeType.Dummy));
				tableKeysNode.Nodes.Add(new CustomNode(NodeType.Dummy));
				tableConstraintsNode.Nodes.Add(new CustomNode(NodeType.Dummy));
				tableTriggersNode.Nodes.Add(new CustomNode(NodeType.Dummy));
				tableIndexesNode.Nodes.Add(new CustomNode(NodeType.Dummy));

				tableNode.Nodes.Add(tableIndexesNode);
				tableNode.Nodes.Add(tableTriggersNode);
				tableNode.Nodes.Add(tableConstraintsNode);
				tableNode.Nodes.Add(tableKeysNode);
				tableNode.Nodes.Add(tableColumnsNode);

				parentNode.Nodes.Add(tableNode);
			}
		}
	}

	public static void GenerateViewsNode(CustomNode parentNode, DatabaseOperation databaseOperation)
	{
		DataTable dt = databaseOperation.GetViews();
		parentNode.Nodes.Clear();

		string previousName = null;

		foreach (DataRow dr in dt.Rows)
		{
			if (dr["name"].ToString() != previousName)
			{
				previousName = dr["name"].ToString();

				List<DescriptionItem> descriptions = GetDescriptions(dt, dr["name"].ToString());

				CustomNode viewNode = new CustomNode(NodeType.View, parentNode, dr["name"].ToString(), descriptions, null);

				CustomNode viewColumnsNode = new CustomNode(NodeType.ViewColumns, viewNode, "Columns", NodeImage.Folder, 1);
				CustomNode viewTriggersNode = new CustomNode(NodeType.ViewTriggers, viewNode, "Triggers", NodeImage.Folder, 2);
				CustomNode viewIndexesNode = new CustomNode(NodeType.ViewIndexes, viewNode, "Indexes", NodeImage.Folder, 3);

				viewColumnsNode.Nodes.Add(new CustomNode(NodeType.Dummy));
				viewTriggersNode.Nodes.Add(new CustomNode(NodeType.Dummy));
				viewIndexesNode.Nodes.Add(new CustomNode(NodeType.Dummy));

				viewNode.Nodes.Add(viewColumnsNode);
				viewNode.Nodes.Add(viewTriggersNode);
				viewNode.Nodes.Add(viewIndexesNode);

				parentNode.Nodes.Add(viewNode);
			}
		}
	}

	public static void GenerateProceduresNode(CustomNode parentNode, DatabaseOperation databaseOperation)
	{
		DataTable dt = databaseOperation.GetProcedures();
		parentNode.Nodes.Clear();

		string previousName = null;

		foreach (DataRow dr in dt.Rows)
		{
			if (dr["name"].ToString() != previousName)
			{
				previousName = dr["name"].ToString();

				List<DescriptionItem> descriptions = GetDescriptions(dt, dr["name"].ToString());

				CustomNode procedureNode = new CustomNode(NodeType.StoredProcedure, parentNode, dr["name"].ToString(), descriptions, null);

				CustomNode procedureParametersNode = new CustomNode(NodeType.StoredProcedureParameters, procedureNode, "Parameters", NodeImage.Folder, 1);
				procedureParametersNode.Nodes.Add(new CustomNode(NodeType.Dummy));
				procedureNode.Nodes.Add(procedureParametersNode);

				parentNode.Nodes.Add(procedureNode);
			}
		}
	}

	public static void GenerateTableValuedFunctionsNode(CustomNode parentNode, DatabaseOperation databaseOperation)
	{
		DataTable dt = databaseOperation.GetTableValuedFunctions();
		parentNode.Nodes.Clear();

		string previousName = null;

		foreach (DataRow dr in dt.Rows)
		{
			if (dr["name"].ToString() != previousName)
			{
				previousName = dr["name"].ToString();

				List<DescriptionItem> descriptions = GetDescriptions(dt, dr["name"].ToString());

				CustomNode tableValuedFunctionNode = new CustomNode(NodeType.TableValuedFunction, parentNode, dr["name"].ToString(), descriptions, null);

				CustomNode tableValuedFunctionParametersNode = new CustomNode(NodeType.TableValuedFunctionParameters, tableValuedFunctionNode, "Parameters", NodeImage.Folder, 1);
				tableValuedFunctionParametersNode.Nodes.Add(new CustomNode(NodeType.Dummy));
				tableValuedFunctionNode.Nodes.Add(tableValuedFunctionParametersNode);

				parentNode.Nodes.Add(tableValuedFunctionNode);
			}
		}
	}

	public static void GenerateScalarValuedFunctionsNode(CustomNode parentNode, DatabaseOperation databaseOperation)
	{
		DataTable dt = databaseOperation.GetScalarValuedFunctions();
		parentNode.Nodes.Clear();

		string previousName = null;

		foreach (DataRow dr in dt.Rows)
		{
			if (dr["name"].ToString() != previousName)
			{
				previousName = dr["name"].ToString();

				List<DescriptionItem> descriptions = GetDescriptions(dt, dr["name"].ToString());

				CustomNode scalarValuedFunctionNode = new CustomNode(NodeType.ScalarValuedFunction, parentNode, dr["name"].ToString(), descriptions, null);

				CustomNode scalarValuedFunctionParametersNode = new CustomNode(NodeType.ScalarValuedFunctionParameters, scalarValuedFunctionNode, "Parameters", NodeImage.Folder, 1);
				scalarValuedFunctionParametersNode.Nodes.Add(new CustomNode(NodeType.Dummy));
				scalarValuedFunctionNode.Nodes.Add(scalarValuedFunctionParametersNode);

				parentNode.Nodes.Add(scalarValuedFunctionNode);
			}
		}
	}

	public static void GenerateTableColumnsNode(CustomNode parentNode, string tableName, DatabaseOperation databaseOperation)
	{
		DataTable dt = databaseOperation.GetTableColumns(tableName);
		parentNode.Nodes.Clear();

		string previousName = null;

		foreach (DataRow dr in dt.Rows)
		{
			if (dr["name"].ToString() != previousName)
			{
				previousName = dr["name"].ToString();

				List<DescriptionItem> descriptions = GetDescriptions(dt, dr["name"].ToString());

				CustomNode columnNode = new CustomNode(NodeType.TableColumn, parentNode, dr["name"].ToString(), descriptions, tableName);
				parentNode.Nodes.Add(columnNode);
			}
		}
	}

	public static void GenerateViewColumnsNode(CustomNode parentNode, string viewName, DatabaseOperation databaseOperation)
	{
		DataTable dt = databaseOperation.GetViewColumns(viewName);
		parentNode.Nodes.Clear();

		string previousName = null;

		foreach (DataRow dr in dt.Rows)
		{
			if (dr["name"].ToString() != previousName)
			{
				previousName = dr["name"].ToString();

				List<DescriptionItem> descriptions = GetDescriptions(dt, dr["name"].ToString());

				CustomNode columnNode = new CustomNode(NodeType.ViewColumn, parentNode, dr["name"].ToString(), descriptions, viewName);
				parentNode.Nodes.Add(columnNode);
			}
		}
	}

	public static void GenerateTableTriggersNode(CustomNode parentNode, string tableName, DatabaseOperation databaseOperation)
	{
		DataTable dt = databaseOperation.GetTableTriggers(tableName);
		parentNode.Nodes.Clear();

		string previousName = null;

		foreach (DataRow dr in dt.Rows)
		{
			if (dr["name"].ToString() != previousName)
			{
				previousName = dr["name"].ToString();

				List<DescriptionItem> descriptions = GetDescriptions(dt, dr["name"].ToString());

				CustomNode triggerNode = new CustomNode(NodeType.TableTrigger, parentNode, dr["name"].ToString(), descriptions, tableName);
				parentNode.Nodes.Add(triggerNode);
			}
		}
	}

	public static void GenerateTableIndexesNode(CustomNode parentNode, string tableName, DatabaseOperation databaseOperation)
	{
		DataTable dt = databaseOperation.GetTableIndexes(tableName);
		parentNode.Nodes.Clear();

		string previousName = null;

		foreach (DataRow dr in dt.Rows)
		{
			if (dr["name"].ToString() != previousName)
			{
				previousName = dr["name"].ToString();

				List<DescriptionItem> descriptions = GetDescriptions(dt, dr["name"].ToString());

				CustomNode indexNode = new CustomNode(NodeType.TableIndex, parentNode, dr["name"].ToString(), descriptions, tableName);
				parentNode.Nodes.Add(indexNode);
			}
		}
	}

	public static void GenerateViewTriggersNode(CustomNode parentNode, string viewName, DatabaseOperation databaseOperation)
	{
		DataTable dt = databaseOperation.GetViewTriggers(viewName);
		parentNode.Nodes.Clear();

		string previousName = null;

		foreach (DataRow dr in dt.Rows)
		{
			if (dr["name"].ToString() != previousName)
			{
				previousName = dr["name"].ToString();

				List<DescriptionItem> descriptions = GetDescriptions(dt, dr["name"].ToString());

				CustomNode triggerNode = new CustomNode(NodeType.ViewTrigger, parentNode, dr["name"].ToString(), descriptions, viewName);
				parentNode.Nodes.Add(triggerNode);
			}
		}
	}

	public static void GenerateViewIndexesNode(CustomNode parentNode, string viewName, DatabaseOperation databaseOperation)
	{
		DataTable dt = databaseOperation.GetViewIndexes(viewName);
		parentNode.Nodes.Clear();

		string previousName = null;

		foreach (DataRow dr in dt.Rows)
		{
			if (dr["name"].ToString() != previousName)
			{
				previousName = dr["name"].ToString();

				List<DescriptionItem> descriptions = GetDescriptions(dt, dr["name"].ToString());

				CustomNode indexNode = new CustomNode(NodeType.ViewIndex, parentNode, dr["name"].ToString(), descriptions, viewName);
				parentNode.Nodes.Add(indexNode);
			}
		}
	}

	public static void GenerateTableKeysNode(CustomNode parentNode, string tableName, DatabaseOperation databaseOperation)
	{
		DataTable dt = databaseOperation.GetTableKeys(tableName);
		parentNode.Nodes.Clear();

		string previousName = null;

		foreach (DataRow dr in dt.Rows)
		{
			if (dr["name"].ToString() != previousName)
			{
				previousName = dr["name"].ToString();

				List<DescriptionItem> descriptions = GetDescriptions(dt, dr["name"].ToString());

				CustomNode keyNode = new CustomNode(NodeType.TableKey, parentNode, dr["name"].ToString(), descriptions, tableName);
				parentNode.Nodes.Add(keyNode);
			}
		}
	}

	public static void GenerateTableValuedFunctionParametersNode(CustomNode parentNode, string functionName, DatabaseOperation databaseOperation)
	{
		DataTable dt = databaseOperation.GetTableValuedFunctionParameters(functionName);
		parentNode.Nodes.Clear();

		string previousName = null;

		foreach (DataRow dr in dt.Rows)
		{
			if (dr["name"].ToString() != previousName)
			{
				previousName = dr["name"].ToString();

				List<DescriptionItem> descriptions = GetDescriptions(dt, dr["name"].ToString());

				CustomNode parameterNode = new CustomNode(NodeType.TableValuedFunctionParameter, parentNode, dr["name"].ToString(), descriptions, functionName);
				parentNode.Nodes.Add(parameterNode);
			}
		}
	}

	public static void GenerateScalarValuedFunctionParametersNode(CustomNode parentNode, string functionName, DatabaseOperation databaseOperation)
	{
		DataTable dt = databaseOperation.GetScalarValuedFunctionParameters(functionName);
		parentNode.Nodes.Clear();

		string previousName = null;

		foreach (DataRow dr in dt.Rows)
		{
			if (dr["name"].ToString() != previousName)
			{
				previousName = dr["name"].ToString();

				List<DescriptionItem> descriptions = GetDescriptions(dt, dr["name"].ToString());

				CustomNode parameterNode = new CustomNode(NodeType.ScalarValuedFunctionParameter, parentNode, dr["name"].ToString(), descriptions, functionName);
				parentNode.Nodes.Add(parameterNode);
			}
		}
	}

	public static void GenerateStoredProcedureParametersNode(CustomNode parentNode, string procedureName, DatabaseOperation databaseOperation)
	{
		DataTable dt = databaseOperation.GetStoredProcedureParameters(procedureName);
		parentNode.Nodes.Clear();

		string previousName = null;

		foreach (DataRow dr in dt.Rows)
		{
			if (dr["name"].ToString() != previousName)
			{
				previousName = dr["name"].ToString();

				List<DescriptionItem> descriptions = GetDescriptions(dt, dr["name"].ToString());

				CustomNode parameterNode = new CustomNode(NodeType.StoredProcedureParameter, parentNode, dr["name"].ToString(), descriptions, procedureName);
				parentNode.Nodes.Add(parameterNode);
			}
		}
	}

	public static void GenerateTableConstraintsNode(CustomNode parentNode, string tableName, DatabaseOperation databaseOperation)
	{
		DataTable dt = databaseOperation.GetTableConstraints(tableName);
		parentNode.Nodes.Clear();

		string previousName = null;

		foreach (DataRow dr in dt.Rows)
		{
			if (dr["name"].ToString() != previousName)
			{
				previousName = dr["name"].ToString();

				List<DescriptionItem> descriptions = GetDescriptions(dt, dr["name"].ToString());

				CustomNode constraintNode = new CustomNode(NodeType.TableConstraint, parentNode, dr["name"].ToString(), descriptions, tableName);
				parentNode.Nodes.Add(constraintNode);
			}
		}
	}

	private static List<DescriptionItem> GetDescriptions(DataTable dt, string name)
	{
		List<DescriptionItem> itemList = new List<DescriptionItem>();
		List<DescriptionField> types = ConfigHandler.DescriptionFields;

		foreach (DataRow dr in dt.Rows)
		{
			if (dr["name"].ToString() == name)
			{
				foreach (DescriptionField type in types)
				{
					if (type.DatabaseFieldName.ToLower() == GetDatabaseFieldNameValue(dr["DatabaseFieldName"]))
					{
						DescriptionItem item = new DescriptionItem(type.DisplayText, type.DatabaseFieldName, type.UseForImage, type.Information);
						item.Description = GetDescriptionValue(dr["Description"]);
						itemList.Add(item);
					}
				}
			}
		}

		return itemList;
	}

	private static string GetDescriptionValue(object o)
	{
		if (o == DBNull.Value || o.ToString() == "")
		{
			return null;
		}
		else
		{
			return o.ToString();
		}
	}

	private static string GetDatabaseFieldNameValue(object o)
	{
		if (o == DBNull.Value || o.ToString() == "")
		{
			return null;
		}
		else
		{
			return o.ToString().ToLower();
		}
	}
}
