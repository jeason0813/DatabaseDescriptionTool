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

public static class ExtendedPropertiesHelper
{
	public static string GetAddSql(CustomNode node, string description, string databaseFieldName)
	{
		return string.Format("exec sys.sp_addextendedproperty @value = N'{0}', {1}", description.Replace("'", "''"), GetLevelSql(node, databaseFieldName));
	}

	public static string GetAddSql(string level1Type, string level1Name, string level2Type, string level2Name, string description, string databaseFieldName)
	{
		return string.Format("exec sys.sp_addextendedproperty @value = N'{0}', {1}", description.Replace("'", "''"), GetLevelSql(level1Type, level1Name, level2Type, level2Name, databaseFieldName));
	}

	public static string GetUpdateSql(string level1Type, string level1Name, string level2Type, string level2Name, string description, string databaseFieldName)
	{
		return string.Format("exec sys.sp_updateextendedproperty @value = N'{0}', {1}", description.Replace("'", "''"), GetLevelSql(level1Type, level1Name, level2Type, level2Name, databaseFieldName));
	}

	public static string GetUpdateSql(CustomNode node, string description, string databaseFieldName)
	{
		return string.Format("exec sys.sp_updateextendedproperty @value = N'{0}', {1}", description.Replace("'", "''"), GetLevelSql(node, databaseFieldName));
	}

	public static string GetDropSql(CustomNode node, string databaseFieldName)
	{
		return string.Format("exec sys.sp_dropextendedproperty {0}", GetLevelSql(node, databaseFieldName));
	}

	public static string GetDropSql(string level1Type, string level1Name, string level2Type, string level2Name, string databaseFieldName)
	{
		return string.Format("exec sys.sp_dropextendedproperty {0}", GetLevelSql(level1Type, level1Name, level2Type, level2Name, databaseFieldName));
	}

	public static string GetCheckSql(CustomNode node, string databaseFieldName)
	{
		string level2Type = "null";
		string level2Name = "null";

		if (AddLevel2(node.Type))
		{
			level2Type = string.Format("N'{0}'", GetLevel2Type(node.Type));
			level2Name = string.Format("N'{0}'", GetLevel2Name(node).Replace("'", "''"));
		}

		return string.Format("select e.value from fn_listextendedproperty (N'{4}', N'SCHEMA', N'{5}', N'{0}', N'{1}', {2}, {3}) e", GetLevel1Type(node.Type), GetLevel1Name(node).Replace("'", "''"), level2Type, level2Name, databaseFieldName.Replace("'", "''"), ConfigHandler.SchemaName);
	}

	public static string GetCheckSql(string level1Type, string level1Name, string level2Type, string level2Name, string databaseFieldName)
	{
		string level2Type1 = "null";
		string level2Name1 = "null";

		if (level2Type != "")
		{
			level2Type1 = string.Format("N'{0}'", level2Type);
			level2Name1 = string.Format("N'{0}'", level2Name.Replace("'", "''"));
		}

		return string.Format("select e.value from fn_listextendedproperty (N'{4}', N'SCHEMA', N'{5}', N'{0}', N'{1}', {2}, {3}) e", level1Type, level1Name.Replace("'", "''"), level2Type1, level2Name1, databaseFieldName.Replace("'", "''"), ConfigHandler.SchemaName);
	}

	public static string GetExcelData(CustomNode node, string description, string databaseFieldName)
	{
		return string.Format("{0}, '{1}'", GetLevelExcel(node, databaseFieldName), description.Replace("'", "''"));
	}

	public static string GetLevel1Type(NodeType nodeType)
	{
		string level1Type = "";

		switch (nodeType)
		{
			case NodeType.StoredProcedure:
				level1Type = "PROCEDURE";
				break;
			case NodeType.TableValuedFunction:
				level1Type = "FUNCTION";
				break;
			case NodeType.ScalarValuedFunction:
				level1Type = "FUNCTION";
				break;
			case NodeType.Table:
				level1Type = "TABLE";
				break;
			case NodeType.View:
				level1Type = "VIEW";
				break;
			case NodeType.TableTrigger:
				level1Type = "TABLE";
				break;
			case NodeType.ViewTrigger:
				level1Type = "VIEW";
				break;
			case NodeType.TableColumn:
				level1Type = "TABLE";
				break;
			case NodeType.ViewColumn:
				level1Type = "VIEW";
				break;
			case NodeType.TableKey:
				level1Type = "TABLE";
				break;
			case NodeType.TableConstraint:
				level1Type = "TABLE";
				break;
			case NodeType.TableIndex:
				level1Type = "TABLE";
				break;
			case NodeType.ViewIndex:
				level1Type = "VIEW";
				break;
			case NodeType.TableValuedFunctionParameter:
				level1Type = "FUNCTION";
				break;
			case NodeType.ScalarValuedFunctionParameter:
				level1Type = "FUNCTION";
				break;
			case NodeType.StoredProcedureParameter:
				level1Type = "PROCEDURE";
				break;
		}

		return level1Type;
	}

	public static string GetLevel2Type(NodeType nodeType)
	{
		string level2Type = "";

		switch (nodeType)
		{
			case NodeType.TableTrigger:
				level2Type = "TRIGGER";
				break;
			case NodeType.ViewTrigger:
				level2Type = "TRIGGER";
				break;
			case NodeType.TableColumn:
				level2Type = "COLUMN";
				break;
			case NodeType.ViewColumn:
				level2Type = "COLUMN";
				break;
			case NodeType.TableKey:
				level2Type = "CONSTRAINT";
				break;
			case NodeType.TableConstraint:
				level2Type = "CONSTRAINT";
				break;
			case NodeType.TableIndex:
				level2Type = "INDEX";
				break;
			case NodeType.ViewIndex:
				level2Type = "INDEX";
				break;
			case NodeType.TableValuedFunctionParameter:
				level2Type = "PARAMETER";
				break;
			case NodeType.ScalarValuedFunctionParameter:
				level2Type = "PARAMETER";
				break;
			case NodeType.StoredProcedureParameter:
				level2Type = "PARAMETER";
				break;
		}

		return level2Type;
	}

	public static string GetLevel1Name(CustomNode node)
	{
		string level1Name = null;

		switch (node.Type)
		{
			case NodeType.StoredProcedure:
				level1Name = node.Text;
				break;
			case NodeType.TableValuedFunction:
				level1Name = node.Text;
				break;
			case NodeType.ScalarValuedFunction:
				level1Name = node.Text;
				break;
			case NodeType.Table:
				level1Name = node.Text;
				break;
			case NodeType.View:
				level1Name = node.Text;
				break;
			case NodeType.TableTrigger:
				level1Name = node.ParentName;
				break;
			case NodeType.ViewTrigger:
				level1Name = node.ParentName;
				break;
			case NodeType.TableColumn:
				level1Name = node.ParentName;
				break;
			case NodeType.ViewColumn:
				level1Name = node.ParentName;
				break;
			case NodeType.TableKey:
				level1Name = node.ParentName;
				break;
			case NodeType.TableConstraint:
				level1Name = node.ParentName;
				break;
			case NodeType.TableIndex:
				level1Name = node.ParentName;
				break;
			case NodeType.ViewIndex:
				level1Name = node.ParentName;
				break;
			case NodeType.TableValuedFunctionParameter:
				level1Name = node.ParentName;
				break;
			case NodeType.ScalarValuedFunctionParameter:
				level1Name = node.ParentName;
				break;
			case NodeType.StoredProcedureParameter:
				level1Name = node.ParentName;
				break;
		}

		return level1Name;
	}

	public static string GetLevel2Name(CustomNode node)
	{
		return node.Text;
	}

	public static bool AddLevel2(NodeType nodeType)
	{
		switch (nodeType)
		{
			case NodeType.TableTrigger:
				return true;
			case NodeType.ViewTrigger:
				return true;
			case NodeType.TableColumn:
				return true;
			case NodeType.ViewColumn:
				return true;
			case NodeType.TableKey:
				return true;
			case NodeType.TableConstraint:
				return true;
			case NodeType.TableIndex:
				return true;
			case NodeType.ViewIndex:
				return true;
			case NodeType.TableValuedFunctionParameter:
				return true;
			case NodeType.ScalarValuedFunctionParameter:
				return true;
			case NodeType.StoredProcedureParameter:
				return true;
		}

		return false;
	}

	private static string GetLevelExcel(CustomNode node, string databaseFieldName)
	{
		return string.Format("{0}{1}{2}", GetLevel1Excel(node), GetLevel2Excel(node), GetLevel0Excel(databaseFieldName));
	}

	private static string GetLevelSql(CustomNode node, string databaseFieldName)
	{
		return string.Format("{0}{1}{2}", GetLevel0(databaseFieldName), GetLevel1(node), GetLevel2(node));
	}

	private static string GetLevelSql(string level1Type, string level1Name, string level2Type, string level2Name, string databaseFieldName)
	{
		return string.Format("{0}{1}{2}", GetLevel0(databaseFieldName), GetLevel1(level1Type, level1Name), GetLevel2(level2Type, level2Name));
	}

	private static string GetLevel0Excel(string databaseFieldName)
	{
		return string.Format("'{0}'", databaseFieldName);
	}

	private static string GetLevel1Excel(CustomNode node)
	{
		return string.Format("'{0}', '{1}', ", GetLevel1Type(node.Type), GetLevel1Name(node).Replace("'", "''"));
	}

	private static string GetLevel2Excel(CustomNode node)
	{
		if (AddLevel2(node.Type))
		{
			return string.Format("'{0}', '{1}', ", GetLevel2Type(node.Type), GetLevel2Name(node).Replace("'", "''"));
		}
		else
		{
			return "'', '', ";
		}
	}

	private static string GetLevel0(string databaseFieldName)
	{
		return string.Format("@name = N'{0}', @level0type = N'SCHEMA', @level0name = N'{1}'", databaseFieldName.Replace("'", "''"), ConfigHandler.SchemaName);
	}

	private static string GetLevel1(CustomNode node)
	{
		return string.Format(", @level1Type = N'{0}', @level1Name = N'{1}'", GetLevel1Type(node.Type), GetLevel1Name(node).Replace("'", "''"));
	}

	private static string GetLevel1(string level1Type, string level1Name)
	{
		return string.Format(", @level1Type = N'{0}', @level1Name = N'{1}'", level1Type, level1Name.Replace("'", "''"));
	}

	private static string GetLevel2(CustomNode node)
	{
		if (AddLevel2(node.Type))
		{
			return string.Format(", @level2Type = N'{0}', @level2Name = N'{1}'", GetLevel2Type(node.Type), GetLevel2Name(node).Replace("'", "''"));
		}
		else
		{
			return "";
		}
	}

	private static string GetLevel2(string level2Type, string level2Name)
	{
		if (level2Type != "")
		{
			return string.Format(", @level2Type = N'{0}', @level2Name = N'{1}'", level2Type, level2Name.Replace("'", "''"));
		}
		else
		{
			return "";
		}
	}
}
