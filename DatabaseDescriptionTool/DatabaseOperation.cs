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
using System.Text;

public class DatabaseOperation : Dal
{
	public DatabaseOperation(string connectionString)
		: base(connectionString)
	{
	}

	public DataSet GetMatches(string searchTerm, bool name, bool description, ObjectsSelectorForm objectsSelectorForm, bool matchWholeWord, bool matchCase)
	{
		string databaseFieldNames = "";
		string searchInDescriptions = "0";

		if (description)
		{
			searchInDescriptions = "1";

			StringBuilder selectedDatabaseFieldNames = new StringBuilder();

			if (!objectsSelectorForm.allCheckBox.Checked)
			{
				for (int i = 0; i < objectsSelectorForm.fieldNameListView.SelectedItems.Count; i++)
				{
					if (i > 0)
					{
						selectedDatabaseFieldNames.Append(", ");
					}

					selectedDatabaseFieldNames.Append(string.Format("'{0}'", objectsSelectorForm.fieldNameListView.SelectedItems[i].Name.Replace("'", "''")));
				}
			}

			if (selectedDatabaseFieldNames.Length > 0)
			{
				databaseFieldNames = string.Format("\r\nand u.DatabaseFieldName in ({0})", selectedDatabaseFieldNames);
			}
		}

		string searchInNames = "1";

		if (!name)
		{
			searchInNames = "0";
		}

		string searchWord = searchTerm.Replace("'", "''");

		if (!matchWholeWord)
		{
			searchWord = string.Format("%{0}%", searchWord);
		}

		string collate = "";

		if (matchCase)
		{
			collate = " collate Latin1_General_BIN";
		}

		string objectSelectorSqlNames = "";
		string objectSelectorSqlDescriptions = "";

		if (!objectsSelectorForm.AllCheckBoxChecked() || !AllIndividualObjectCheckBoxChecked(objectsSelectorForm))
		{
			objectSelectorSqlNames = GetObjectSelectorSql(objectsSelectorForm, "n");
			objectSelectorSqlDescriptions = GetObjectSelectorSql(objectsSelectorForm, "u");
		}

		return ExecuteDataSet(string.Format(DatabaseDescriptionTool.Properties.Resources.GetMatches, ConfigHandler.SchemaName, searchInDescriptions, searchInNames, searchWord, collate, databaseFieldNames, objectSelectorSqlNames, objectSelectorSqlDescriptions));
	}

	public string GetObjectDefinition(string objectName)
	{
		return ExecuteDataTable(string.Format("select object_definition (object_id(N'{0}.{1}')) ddl", ConfigHandler.SchemaName, objectName)).Rows[0]["ddl"].ToString();
	}

	public DataTable GetSchemas()
	{
		return ExecuteDataTable(DatabaseDescriptionTool.Properties.Resources.GetSchemas);
	}

	public DataTable GetDescriptionsToDelete()
	{
		return ExecuteDataTable(string.Format(DatabaseDescriptionTool.Properties.Resources.GetDescriptionsToDelete, ConfigHandler.SchemaName));
	}

	public DataTable GetDescriptionFields()
	{
		return ExecuteDataTable(DatabaseDescriptionTool.Properties.Resources.GetDescriptionFields);
	}

	public int GetIdFromName(string level1Type, string level1Name, string level2Type, string level2Name)
	{
		DataTable dt = ExecuteDataTable(string.Format(DatabaseDescriptionTool.Properties.Resources.GetIdFromName, ConfigHandler.SchemaName, level1Type, level1Name, level2Type, level2Name));

		if (dt.Rows.Count == 1)
		{
			return Convert.ToInt32(dt.Rows[0]["id"]);
		}
		else
		{
			return -1;
		}
	}

	public string GetDescription(CustomNode node, string databaseFieldName)
	{
		DataTable dt = ExecuteDataTable(ExtendedPropertiesHelper.GetCheckSql(node, databaseFieldName));

		if (dt.Rows.Count == 1)
		{
			return dt.Rows[0]["value"].ToString();
		}
		else
		{
			return "";
		}
	}

	public void SaveDescription(CustomNode node, string description, string databaseFieldName)
	{
		DataTable dt = ExecuteDataTable(ExtendedPropertiesHelper.GetCheckSql(node, databaseFieldName));
		string sql;

		if (dt.Rows.Count == 0)
		{
			sql = ExtendedPropertiesHelper.GetAddSql(node, description, databaseFieldName);
		}
		else
		{
			if (description == "")
			{
				sql = ExtendedPropertiesHelper.GetDropSql(node, databaseFieldName);
			}
			else
			{
				sql = ExtendedPropertiesHelper.GetUpdateSql(node, description, databaseFieldName);
			}
		}

		Execute(sql);
	}

	public void SaveDescription(string level1Type, string level1Name, string level2Type, string level2Name, string description, string databaseFieldName)
	{
		DataTable dt = ExecuteDataTable(ExtendedPropertiesHelper.GetCheckSql(level1Type, level1Name, level2Type, level2Name, databaseFieldName));
		string sql;

		if (dt.Rows.Count == 0)
		{
			sql = ExtendedPropertiesHelper.GetAddSql(level1Type, level1Name, level2Type, level2Name, description, databaseFieldName);
		}
		else
		{
			if (description == "")
			{
				sql = ExtendedPropertiesHelper.GetDropSql(level1Type, level1Name, level2Type, level2Name, databaseFieldName);
			}
			else
			{
				sql = ExtendedPropertiesHelper.GetUpdateSql(level1Type, level1Name, level2Type, level2Name, description, databaseFieldName);
			}
		}

		sql = CheckWrapper.GetCheckWrapper(level1Type, level1Name, level2Type, level2Name, sql);
		Execute(sql);
	}

	public DataTable GetDatabases()
	{
		return ExecuteDataTable("select d.name from sys.databases d where d.name not in('master', 'tempdb', 'msdb', 'model') and d.state = 0 order by d.name");
	}

	public DataTable GetTableNames()
	{
		return ExecuteDataTable(string.Format("select t.name from sys.tables t where schema_name(t.schema_id) = '{0}' order by t.name", ConfigHandler.SchemaName));
	}

	public DataTable GetViewNames()
	{
		return ExecuteDataTable(string.Format("select v.name from sys.views v where schema_name(v.schema_id) = '{0}' order by v.name", ConfigHandler.SchemaName));
	}

	public DataTable GetProcedureNames()
	{
		return ExecuteDataTable(string.Format("select p.name from sys.procedures p where schema_name(p.schema_id) = '{0}' order by p.name", ConfigHandler.SchemaName));
	}

	public DataTable GetTableValuedFunctionNames()
	{
		return ExecuteDataTable(string.Format("select o.name from sys.objects o where o.type in ('TF', 'IF') and schema_name(o.schema_id) = '{0}' order by o.name", ConfigHandler.SchemaName));
	}

	public DataTable GetScalarValuedFunctionNames()
	{
		return ExecuteDataTable(string.Format("select o.name from sys.objects o where o.type = 'FN' and schema_name(o.schema_id) = '{0}' order by o.name", ConfigHandler.SchemaName));
	}

	public DataTable GetTables()
	{
		return ExecuteDataTable(string.Format("select t.name, e.value Description, e.name DatabaseFieldName from sys.tables t left join sys.extended_properties e on e.major_id = t.object_id and minor_id = 0 and e.class = 1 where schema_name(t.schema_id) = '{0}' and isnull(e.name, '') != 'microsoft_database_tools_support' order by t.name", ConfigHandler.SchemaName));
	}

	public DataTable GetViews()
	{
		return ExecuteDataTable(string.Format("select v.name, e.value Description, e.name DatabaseFieldName from sys.views v left join sys.extended_properties e on e.major_id = v.object_id and minor_id = 0 and e.class = 1 where schema_name(v.schema_id) = '{0}' and isnull(e.name, '') != 'microsoft_database_tools_support' order by v.name", ConfigHandler.SchemaName));
	}

	public DataTable GetProcedures()
	{
		return ExecuteDataTable(string.Format("select p.name, e.value Description, e.name DatabaseFieldName from sys.procedures p left join sys.extended_properties e on e.major_id = p.object_id and minor_id = 0 and e.class = 1 where schema_name(p.schema_id) = '{0}' and isnull(e.name, '') != 'microsoft_database_tools_support' order by p.name", ConfigHandler.SchemaName));
	}

	public DataTable GetTableValuedFunctions()
	{
		return ExecuteDataTable(string.Format("select o.name, e.value Description, e.name DatabaseFieldName from sys.objects o left join sys.extended_properties e on e.major_id = o.object_id and minor_id = 0 and e.class = 1 where o.type in ('TF', 'IF') and schema_name(o.schema_id) = '{0}' and isnull(e.name, '') != 'microsoft_database_tools_support' order by o.name", ConfigHandler.SchemaName));
	}

	public DataTable GetScalarValuedFunctions()
	{
		return ExecuteDataTable(string.Format("select o.name, e.value Description, e.name DatabaseFieldName from sys.objects o left join sys.extended_properties e on e.major_id = o.object_id and minor_id = 0 and e.class = 1 where o.type = 'FN' and schema_name(o.schema_id) = '{0}' and isnull(e.name, '') != 'microsoft_database_tools_support' order by o.name", ConfigHandler.SchemaName));
	}

	public DataTable GetTableColumns(string tableName)
	{
		return ExecuteDataTable(string.Format("select c.name, e.value Description, e.name DatabaseFieldName from sys.columns c inner join sys.tables t on t.object_id = c.object_id left join sys.extended_properties e on e.major_id = c.object_id and e.minor_id = c.column_id and e.class = 1 where schema_name(t.schema_id) = '{0}' and t.name = '{1}' order by c.column_id", ConfigHandler.SchemaName, tableName.Replace("'", "''")));
	}

	public DataTable GetTableKeys(string tableName)
	{
		return ExecuteDataTable(string.Format("select object_name(o.object_id) name, e.value Description, e.name DatabaseFieldName from sys.objects o left join sys.extended_properties e on e.major_id = o.object_id and e.class = 1 where schema_name(o.schema_id) = '{0}' and o.type in ('F', 'PK', 'UQ') and object_name(parent_object_id) = '{1}' order by case when o.type = 'PK' then 1 when o.type = 'F' then 2 when o.type = 'UQ' then 3 end, object_name(object_id)", ConfigHandler.SchemaName, tableName.Replace("'", "''")));
	}

	public DataTable GetTableConstraints(string tableName)
	{
		return ExecuteDataTable(string.Format("select object_name(o.object_id) name, e.value Description, e.name DatabaseFieldName from sys.objects o left join sys.extended_properties e on e.major_id = o.object_id and e.class = 1 where schema_name(o.schema_id) = '{0}' and o.type in ('D', 'C') and object_name(parent_object_id) = '{1}' order by case when o.type = 'C' then 1 when o.type = 'D' then 2 end, object_name(object_id)", ConfigHandler.SchemaName, tableName.Replace("'", "''")));
	}

	public DataTable GetTableTriggers(string tableName)
	{
		return ExecuteDataTable(string.Format("select o.name, e.value Description, e.name DatabaseFieldName from sys.triggers o inner join sys.tables t on t.object_id = o.parent_id left join sys.extended_properties e on e.major_id = o.object_id and e.class = 1 where schema_name(t.schema_id) = '{0}' and t.name = '{1}' order by o.name", ConfigHandler.SchemaName, tableName.Replace("'", "''")));
	}

	public DataTable GetTableIndexes(string tableName)
	{
		return ExecuteDataTable(string.Format("select i.name, e.value Description, e.name DatabaseFieldName from sys.indexes i inner join sys.tables t on t.object_id = i.object_id left join sys.extended_properties e on e.major_id = i.object_id and e.minor_id = i.index_id and e.class = 7 where schema_name(t.schema_id) = '{0}' and t.name = '{1}' and i.index_id > 0 order by i.name", ConfigHandler.SchemaName, tableName.Replace("'", "''")));
	}

	public DataTable GetViewColumns(string viewName)
	{
		return ExecuteDataTable(string.Format("select c.name, e.value Description, e.name DatabaseFieldName from sys.columns c inner join sys.views v on v.object_id = c.object_id left join sys.extended_properties e on e.major_id = c.object_id and e.minor_id = c.column_id and e.class = 1 where schema_name(v.schema_id) = '{0}' and v.name = '{1}' order by c.column_id", ConfigHandler.SchemaName, viewName.Replace("'", "''")));
	}

	public DataTable GetViewTriggers(string viewName)
	{
		return ExecuteDataTable(string.Format("select o.name, e.value Description, e.name DatabaseFieldName from sys.triggers o inner join sys.views v on v.object_id = o.parent_id left join sys.extended_properties e on e.major_id = o.object_id and e.class = 1 where schema_name(v.schema_id) = '{0}' and v.name = '{1}' order by o.name", ConfigHandler.SchemaName, viewName.Replace("'", "''")));
	}

	public DataTable GetViewIndexes(string viewName)
	{
		return ExecuteDataTable(string.Format("select i.name, e.value Description, e.name DatabaseFieldName from sys.indexes i inner join sys.views v on v.object_id = i.object_id left join sys.extended_properties e on e.major_id = i.object_id and e.minor_id = i.index_id and e.class = 7 where schema_name(v.schema_id) = '{0}' and v.name = '{1}' and i.index_id > 0 order by i.name", ConfigHandler.SchemaName, viewName.Replace("'", "''")));
	}

	public DataTable GetTableValuedFunctionParameters(string functionName)
	{
		return ExecuteDataTable(string.Format("select a.name, e.value Description, e.name DatabaseFieldName from sys.objects o inner join sys.parameters a on a.object_id = o.object_id left join sys.extended_properties e on e.major_id = o.object_id and e.minor_id = a.parameter_id and e.class = 2 where schema_name(o.schema_id) = '{0}' and o.type in ('TF', 'IF') and o.name = '{1}' order by a.parameter_id", ConfigHandler.SchemaName, functionName.Replace("'", "''")));
	}

	public DataTable GetScalarValuedFunctionParameters(string functionName)
	{
		return ExecuteDataTable(string.Format("select a.name, e.value Description, e.name DatabaseFieldName from sys.objects o inner join sys.parameters a on a.object_id = o.object_id left join sys.extended_properties e on e.major_id = o.object_id and e.minor_id = a.parameter_id and e.class = 2 where schema_name(o.schema_id) = '{0}' and o.type = 'FN' and o.name = '{1}' and a.name != '' order by a.parameter_id", ConfigHandler.SchemaName, functionName.Replace("'", "''")));
	}

	public DataTable GetStoredProcedureParameters(string procedureName)
	{
		return ExecuteDataTable(string.Format("select a.name, e.value Description, e.name DatabaseFieldName from sys.procedures p inner join sys.parameters a on a.object_id = p.object_id left join sys.extended_properties e on e.major_id = p.object_id and e.minor_id = a.parameter_id and e.class = 2 where schema_name(p.schema_id) = '{0}' and p.name = '{1}' order by a.parameter_id", ConfigHandler.SchemaName, procedureName.Replace("'", "''")));
	}

	private static string GetObjectSelectorSql(ObjectsSelectorForm objectsSelectorForm, string prefix)
	{
		StringBuilder sb = new StringBuilder("\r\nand (");

		sb.Append(string.Format("{0}", GetObjectSelectorTablesSql(objectsSelectorForm, prefix)));
		sb.Append("\r\nor ");
		sb.Append(string.Format("{0}", GetObjectSelectorViewsSql(objectsSelectorForm, prefix)));
		sb.Append("\r\nor ");
		sb.Append(string.Format("{0}", GetObjectSelectorStoredProceduresSql(objectsSelectorForm, prefix)));
		sb.Append("\r\nor ");
		sb.Append(string.Format("{0}", GetObjectSelectorTableValuedFunctionsSql(objectsSelectorForm, prefix)));
		sb.Append("\r\nor ");
		sb.Append(string.Format("{0}", GetObjectSelectorScalarValuedFunctionsSql(objectsSelectorForm, prefix)));

		sb.Append("\r\n)");
		return sb.ToString();
	}

	private static string GetObjectSelectorTablesSql(ObjectsSelectorForm objectsSelectorForm, string prefix)
	{
		if (!objectsSelectorForm.AnyCheckBoxCheckedIndividualTables())
		{
			return "(1 = 0)";
		}

		StringBuilder level2Type = new StringBuilder();

		if (objectsSelectorForm.tablesCheckBox.Checked)
		{
			level2Type.Append("''");
		}

		if (objectsSelectorForm.tablesCheckBox.Checked)
		{
			if (level2Type.Length > 0)
			{
				level2Type.Append(", ");
			}

			level2Type.Append("'Column'");
		}

		if (objectsSelectorForm.tableKeysCheckBox.Checked)
		{
			if (level2Type.Length > 0)
			{
				level2Type.Append(", ");
			}

			level2Type.Append("'Key'");
		}

		if (objectsSelectorForm.tableTriggersCheckBox.Checked)
		{
			if (level2Type.Length > 0)
			{
				level2Type.Append(", ");
			}

			level2Type.Append("'Trigger'");
		}

		if (objectsSelectorForm.tableIndexesCheckBox.Checked)
		{
			if (level2Type.Length > 0)
			{
				level2Type.Append(", ");
			}

			level2Type.Append("'Index'");
		}

		StringBuilder level1Name = new StringBuilder();

		if (level2Type.Length > 0)
		{
			if (!objectsSelectorForm.AllCheckBoxCheckedIndividualTables())
			{
				List<string> objectNames = objectsSelectorForm.GetSelectedIndividualTables();

				for (int i = 0; i < objectNames.Count; i++)
				{
					if (level1Name.Length > 0)
					{
						level1Name.Append(", ");
					}

					level1Name.Append(string.Format("'{0}'", objectNames[i].Replace("'", "''")));
				}
			}
		}

		string level1NameIn = "";

		if (level1Name.Length > 0)
		{
			level1NameIn = string.Format(" and {0}.Level1Name in ({1})", prefix, level1Name);
		}

		return string.Format("({0}.Level1Type = 'Table' and {0}.Level2Type in ({1}){2})", prefix, level2Type, level1NameIn);
	}

	private static string GetObjectSelectorViewsSql(ObjectsSelectorForm objectsSelectorForm, string prefix)
	{
		if (!objectsSelectorForm.AnyCheckBoxCheckedIndividualViews())
		{
			return "(1 = 0)";
		}

		StringBuilder level2Type = new StringBuilder();

		if (objectsSelectorForm.viewsCheckBox.Checked)
		{
			level2Type.Append("''");
		}

		if (objectsSelectorForm.viewColumnsCheckBox.Checked)
		{
			if (level2Type.Length > 0)
			{
				level2Type.Append(", ");
			}

			level2Type.Append("'Column'");
		}

		if (objectsSelectorForm.viewTriggersCheckBox.Checked)
		{
			if (level2Type.Length > 0)
			{
				level2Type.Append(", ");
			}

			level2Type.Append("'Trigger'");
		}

		if (objectsSelectorForm.viewIndexesCheckBox.Checked)
		{
			if (level2Type.Length > 0)
			{
				level2Type.Append(", ");
			}

			level2Type.Append("'Index'");
		}

		StringBuilder level1Name = new StringBuilder();

		if (level2Type.Length > 0)
		{
			if (!objectsSelectorForm.AllCheckBoxCheckedIndividualViews())
			{
				List<string> objectNames = objectsSelectorForm.GetSelectedIndividualViews();

				for (int i = 0; i < objectNames.Count; i++)
				{
					if (level1Name.Length > 0)
					{
						level1Name.Append(", ");
					}

					level1Name.Append(string.Format("'{0}'", objectNames[i].Replace("'", "''")));
				}
			}
		}

		string level1NameIn = "";

		if (level1Name.Length > 0)
		{
			level1NameIn = string.Format(" and {0}.Level1Name in ({1})", prefix, level1Name);
		}

		return string.Format("({0}.Level1Type = 'View' and {0}.Level2Type in ({1}){2})", prefix, level2Type, level1NameIn);
	}

	private static string GetObjectSelectorStoredProceduresSql(ObjectsSelectorForm objectsSelectorForm, string prefix)
	{
		if (!objectsSelectorForm.AnyCheckBoxCheckedIndividualStoredProcedures())
		{
			return "(1 = 0)";
		}

		StringBuilder level2Type = new StringBuilder();

		if (objectsSelectorForm.storedProceduresCheckBox.Checked)
		{
			level2Type.Append("''");
		}

		if (objectsSelectorForm.storedProcedureParametersCheckBox.Checked)
		{
			if (level2Type.Length > 0)
			{
				level2Type.Append(", ");
			}

			level2Type.Append("'Parameter'");
		}

		StringBuilder level1Name = new StringBuilder();

		if (level2Type.Length > 0)
		{
			if (!objectsSelectorForm.AllCheckBoxCheckedIndividualStoredProcedures())
			{
				List<string> objectNames = objectsSelectorForm.GetSelectedIndividualStoredProcedures();

				for (int i = 0; i < objectNames.Count; i++)
				{
					if (level1Name.Length > 0)
					{
						level1Name.Append(", ");
					}

					level1Name.Append(string.Format("'{0}'", objectNames[i].Replace("'", "''")));
				}
			}
		}

		string level1NameIn = "";

		if (level1Name.Length > 0)
		{
			level1NameIn = string.Format(" and {0}.Level1Name in ({1})", prefix, level1Name);
		}

		return string.Format("({0}.Level1Type = 'Procedure' and {0}.Level2Type in ({1}){2})", prefix, level2Type, level1NameIn);
	}

	private static string GetObjectSelectorTableValuedFunctionsSql(ObjectsSelectorForm objectsSelectorForm, string prefix)
	{
		if (!objectsSelectorForm.AnyCheckBoxCheckedIndividualTableValuedFunctions())
		{
			return "(1 = 0)";
		}

		StringBuilder level2Type = new StringBuilder();

		if (objectsSelectorForm.tableValuedFunctionsCheckBox.Checked)
		{
			level2Type.Append("''");
		}

		if (objectsSelectorForm.tableValuedFunctionParametersCheckBox.Checked)
		{
			if (level2Type.Length > 0)
			{
				level2Type.Append(", ");
			}

			level2Type.Append("'Parameter'");
		}

		StringBuilder level1Name = new StringBuilder();

		if (level2Type.Length > 0)
		{
			if (!objectsSelectorForm.AllCheckBoxCheckedIndividualTableValuedFunctions())
			{
				List<string> objectNames = objectsSelectorForm.GetSelectedIndividualTableValuedFunctions();

				for (int i = 0; i < objectNames.Count; i++)
				{
					if (level1Name.Length > 0)
					{
						level1Name.Append(", ");
					}

					level1Name.Append(string.Format("'{0}'", objectNames[i].Replace("'", "''")));
				}
			}
		}

		string level1NameIn = "";

		if (level1Name.Length > 0)
		{
			level1NameIn = string.Format(" and {0}.Level1Name in ({1})", prefix, level1Name);
		}

		return string.Format("({0}.Level1Type = 'Table-valued Function' and {0}.Level2Type in ({1}){2})", prefix, level2Type, level1NameIn);
	}

	private static string GetObjectSelectorScalarValuedFunctionsSql(ObjectsSelectorForm objectsSelectorForm, string prefix)
	{
		if (!objectsSelectorForm.AnyCheckBoxCheckedIndividualScalarValuedFunctions())
		{
			return "(1 = 0)";
		}

		StringBuilder level2Type = new StringBuilder();

		if (objectsSelectorForm.scalarValuedFunctionsCheckBox.Checked)
		{
			level2Type.Append("''");
		}

		if (objectsSelectorForm.scalarValuedFunctionParametersCheckBox.Checked)
		{
			if (level2Type.Length > 0)
			{
				level2Type.Append(", ");
			}

			level2Type.Append("'Parameter'");
		}

		StringBuilder level1Name = new StringBuilder();

		if (level2Type.Length > 0)
		{
			if (!objectsSelectorForm.AllCheckBoxCheckedIndividualScalarValuedFunctions())
			{
				List<string> objectNames = objectsSelectorForm.GetSelectedIndividualScalarValuedFunctions();

				for (int i = 0; i < objectNames.Count; i++)
				{
					if (level1Name.Length > 0)
					{
						level1Name.Append(", ");
					}

					level1Name.Append(string.Format("'{0}'", objectNames[i].Replace("'", "''")));
				}
			}
		}

		string level1NameIn = "";

		if (level1Name.Length > 0)
		{
			level1NameIn = string.Format(" and {0}.Level1Name in ({1})", prefix, level1Name);
		}

		return string.Format("({0}.Level1Type = 'Scalar-valued Function' and {0}.Level2Type in ({1}){2})", prefix, level2Type, level1NameIn);
	}

	private static bool AllIndividualObjectCheckBoxChecked(ObjectsSelectorForm objectsSelectorForm)
	{
		if (!objectsSelectorForm.AllCheckBoxCheckedIndividualTables())
		{
			return false;
		}

		if (!objectsSelectorForm.AllCheckBoxCheckedIndividualViews())
		{
			return false;
		}

		if (!objectsSelectorForm.AllCheckBoxCheckedIndividualStoredProcedures())
		{
			return false;
		}

		if (!objectsSelectorForm.AllCheckBoxCheckedIndividualTableValuedFunctions())
		{
			return false;
		}

		if (!objectsSelectorForm.AllCheckBoxCheckedIndividualScalarValuedFunctions())
		{
			return false;
		}

		return true;
	}
}
