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

public static class CheckWrapper
{
	public static string GetCheckWrapper(CustomNode node, string sqlToWrap)
	{
		string level1Type = ExtendedPropertiesHelper.GetLevel1Type(node.Type);
		string level1Name = ExtendedPropertiesHelper.GetLevel1Name(node);

		string level2Type = ExtendedPropertiesHelper.GetLevel2Type(node.Type);
		string level2Name = ExtendedPropertiesHelper.GetLevel2Name(node);

		return GetCheckWrapper(level1Type, level1Name, level2Type, level2Name, sqlToWrap);
	}

	public static string GetCheckWrapper(string level1Type, string level1Name, string level2Type, string level2Name, string sqlToWrap)
	{
		switch (level1Type)
		{
			case "TABLE":
				if (level2Type == "")
				{
					return CheckTableWrapper(level1Name, sqlToWrap);
				}
				else
				{
					switch (level2Type)
					{
						case "TRIGGER":
							return CheckTableTriggerWrapper(level1Name, level2Name, sqlToWrap);
						case "CONSTRAINT":
							return CheckTableConstraintWrapper(level1Name, level2Name, sqlToWrap);
						case "INDEX":
							return CheckTableIndexWrapper(level1Name, level2Name, sqlToWrap);
						case "COLUMN":
							return CheckTableColumnWrapper(level1Name, level2Name, sqlToWrap);
					}
				}
				break;
			case "VIEW":
				if (level2Type == "")
				{
					return CheckViewWrapper(level1Name, sqlToWrap);
				}
				else
				{
					switch (level2Type)
					{
						case "TRIGGER":
							return CheckViewTriggerWrapper(level1Name, level2Name, sqlToWrap);
						case "INDEX":
							return CheckViewIndexWrapper(level1Name, level2Name, sqlToWrap);
						case "COLUMN":
							return CheckViewColumnWrapper(level1Name, level2Name, sqlToWrap);
					}
				}
				break;
			case "PROCEDURE":
				if (level2Type == "")
				{
					return CheckProcedureWrapper(level1Name, sqlToWrap);
				}
				else
				{
					switch (level2Type)
					{
						case "PARAMETER":
							return CheckProcedureParameterWrapper(level1Name, level2Name, sqlToWrap);
					}
				}
				break;
			case "FUNCTION":
				if (level2Type == "")
				{
					return CheckFunctionWrapper(level1Name, sqlToWrap);
				}
				else
				{
					switch (level2Type)
					{
						case "PARAMETER":
							return CheckFunctionParameterWrapper(level1Name, level2Name, sqlToWrap);
					}
				}
				break;
		}

		return null;
	}

	private static string CheckTableWrapper(string tableName, string sqlToWrap)
	{
		return string.Format("if exists (select 1 from sys.objects o where o.is_ms_shipped = 0 and schema_name(o.schema_id) = '{2}' and o.name = '{0}' and o.type = 'U')\r\nbegin\r\n{1}\r\nend", tableName, sqlToWrap, ConfigHandler.SchemaName);
	}

	private static string CheckViewWrapper(string viewName, string sqlToWrap)
	{
		return string.Format("if exists (select 1 from sys.objects o where o.is_ms_shipped = 0 and schema_name(o.schema_id) = '{2}' and o.name = '{0}' and o.type = 'V')\r\nbegin\r\n{1}\r\nend", viewName, sqlToWrap, ConfigHandler.SchemaName);
	}

	private static string CheckProcedureWrapper(string procedureName, string sqlToWrap)
	{
		return string.Format("if exists (select 1 from sys.objects o where o.is_ms_shipped = 0 and schema_name(o.schema_id) = '{2}' and o.name = '{0}' and o.type = 'P')\r\nbegin\r\n{1}\r\nend", procedureName, sqlToWrap, ConfigHandler.SchemaName);
	}

	private static string CheckFunctionWrapper(string functionName, string sqlToWrap)
	{
		return string.Format("if exists (select 1 from sys.objects o where o.is_ms_shipped = 0 and schema_name(o.schema_id) = '{2}' and o.name = '{0}' and o.type in ('TF', 'FN', 'IF'))\r\nbegin\r\n{1}\r\nend", functionName, sqlToWrap, ConfigHandler.SchemaName);
	}

	private static string CheckTableTriggerWrapper(string tableName, string triggerName, string sqlToWrap)
	{
		return string.Format("if exists (select 1 from sys.objects o inner join sys.triggers t on t.parent_id = o.object_id where o.is_ms_shipped = 0 and schema_name(o.schema_id) = '{3}' and o.name = '{0}' and o.type = 'U' and t.name = '{1}')\r\nbegin\r\n{2}\r\nend", tableName, triggerName, sqlToWrap, ConfigHandler.SchemaName);
	}

	private static string CheckViewTriggerWrapper(string viewName, string triggerName, string sqlToWrap)
	{
		return string.Format("if exists (select 1 from sys.objects o inner join sys.triggers t on t.parent_id = o.object_id where o.is_ms_shipped = 0 and schema_name(o.schema_id) = '{3}' and o.name = '{0}' and o.type = 'V' and t.name = '{1}')\r\nbegin\r\n{2}\r\nend", viewName, triggerName, sqlToWrap, ConfigHandler.SchemaName);
	}

	private static string CheckTableConstraintWrapper(string tableName, string constraintName, string sqlToWrap)
	{
		return string.Format("if exists (select 1 from sys.objects o inner join sys.objects p on p.object_id = o.parent_object_id where p.is_ms_shipped = 0 and schema_name(p.schema_id) = '{3}' and p.name = '{0}' and p.type = 'U' and o.type in ('C', 'UQ', 'F', 'D', 'PK') and o.name = '{1}')\r\nbegin\r\n{2}\r\nend", tableName, constraintName, sqlToWrap, ConfigHandler.SchemaName);
	}

	private static string CheckTableIndexWrapper(string tableName, string indexName, string sqlToWrap)
	{
		return string.Format("if exists (select 1 from sys.objects o inner join sys.indexes i on i.object_id = o.object_id where o.is_ms_shipped = 0 and schema_name(o.schema_id) = '{3}' and o.name = '{0}' and o.type = 'U' and i.index_id > 0 and i.name = '{1}')\r\nbegin\r\n{2}\r\nend", tableName, indexName, sqlToWrap, ConfigHandler.SchemaName);
	}

	private static string CheckViewIndexWrapper(string viewName, string indexName, string sqlToWrap)
	{
		return string.Format("if exists (select 1 from sys.objects o inner join sys.indexes i on i.object_id = o.object_id where o.is_ms_shipped = 0 and schema_name(o.schema_id) = '{3}' and o.name = '{0}' and o.type = 'V' and i.index_id > 0 and i.name = '{1}')\r\nbegin\r\n{2}\r\nend", viewName, indexName, sqlToWrap, ConfigHandler.SchemaName);
	}

	private static string CheckTableColumnWrapper(string tableName, string columnName, string sqlToWrap)
	{
		return string.Format("if exists (select 1 from sys.objects o inner join sys.columns c on c.object_id = o.object_id where o.is_ms_shipped = 0 and schema_name(o.schema_id) = '{3}' and o.name = '{0}' and o.type = 'U' and c.name = '{1}')\r\nbegin\r\n{2}\r\nend", tableName, columnName, sqlToWrap, ConfigHandler.SchemaName);
	}

	private static string CheckViewColumnWrapper(string viewName, string columnName, string sqlToWrap)
	{
		return string.Format("if exists (select 1 from sys.objects o inner join sys.columns c on c.object_id = o.object_id where o.is_ms_shipped = 0 and schema_name(o.schema_id) = '{3}' and o.name = '{0}' and o.type = 'V' and c.name = '{1}')\r\nbegin\r\n{2}\r\nend", viewName, columnName, sqlToWrap, ConfigHandler.SchemaName);
	}

	private static string CheckProcedureParameterWrapper(string procedureName, string parameterName, string sqlToWrap)
	{
		return string.Format("if exists (select 1 from sys.objects o inner join sys.parameters p on p.object_id = o.object_id where o.is_ms_shipped = 0 and schema_name(o.schema_id) = '{3}' and o.name = '{0}' and o.type = 'P' and p.name = '{1}')\r\nbegin\r\n{2}\r\nend", procedureName, parameterName, sqlToWrap, ConfigHandler.SchemaName);
	}

	private static string CheckFunctionParameterWrapper(string functionName, string parameterName, string sqlToWrap)
	{
		return string.Format("if exists (select 1 from sys.objects o inner join sys.parameters p on p.object_id = o.object_id where o.is_ms_shipped = 0 and schema_name(o.schema_id) = '{3}' and o.name = '{0}' and o.type in ('TF', 'FN', 'IF') and p.name = '{1}')\r\nbegin\r\n{2}\r\nend", functionName, parameterName, sqlToWrap, ConfigHandler.SchemaName);
	}
}
