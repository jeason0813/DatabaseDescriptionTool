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
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

public static class ConfigHandler
{
	public const int NumberOfSearchHistoryItems = 10;

	private static List<DescriptionTemplate> _descriptionTemplates;
	private static List<DescriptionField> _descriptionFields;

	public static string ConnectionString;
	public static string ConnectionStringToSave;
	public static string SaveConnectionString;
	public static string WindowSize;
	public static string SplitterDistanceVertical;
	public static string SplitterDistanceHorizontal;
	public static string SchemaName;
	public static string TextDataFontFamily;
	public static string TextDataFontSize;
	public static string DescriptionFontFamily;
	public static string DescriptionFontSize;
	public static string CheckForUpdatesOnStart;
	public static string ShowDdlScript;
	public static string UpdateServiceUrl;

	public const string ApplicationName = "Database Description Tool";

	public static string Version
	{
		get
		{
			Assembly asm = Assembly.GetExecutingAssembly();

			if (asm.GetName().Version.Revision > 0)
			{
				return string.Format("{0}.{1}.{2}.{3}", asm.GetName().Version.Major, asm.GetName().Version.Minor, asm.GetName().Version.Build, asm.GetName().Version.Revision);
			}
			else
			{
				return string.Format("{0}.{1}.{2}", asm.GetName().Version.Major, asm.GetName().Version.Minor, asm.GetName().Version.Build);
			}
		}
	}

	public static string TempPath
	{
		get
		{
			string tempPath = @"C:\Temp";
			string envTempPath = Environment.GetEnvironmentVariable("TEMP");

			if (envTempPath != null)
			{
				tempPath = envTempPath;
			}

			if (!Directory.Exists(tempPath))
			{
				Directory.CreateDirectory(tempPath);
			}

			if (tempPath.Substring(tempPath.Length - 1, 1) == @"\")
			{
				tempPath = tempPath.Substring(0, tempPath.Length - 1);
			}

			return tempPath;
		}
	}

	public static List<DescriptionField> DescriptionFields
	{
		get
		{
			if (_descriptionFields == null)
			{
				_descriptionFields = new List<DescriptionField>();
			}

			return _descriptionFields;
		}
		set
		{
			_descriptionFields = value;
		}
	}

	public static List<DescriptionTemplate> DescriptionTemplates
	{
		get
		{
			if (_descriptionTemplates == null)
			{
				_descriptionTemplates = new List<DescriptionTemplate>();
			}

			return _descriptionTemplates;
		}
		set
		{
			_descriptionTemplates = value;
		}
	}

	public static string ExcelSheetName
	{
		get
		{
			return "Description";
		}
	}

	public static string ServerName
	{
		get
		{
			SqlConnectionStringBuilder connString = new SqlConnectionStringBuilder(ConnectionString);
			return connString.DataSource;
		}
	}

	public static void SaveConfig()
	{
		RegistryHandler.SaveToRegistry("ConnectionString", ConnectionStringSecurity.Encode(ConnectionStringToSave, "ConnectionString"));
		RegistryHandler.SaveToRegistry("SaveConnectionString", SaveConnectionString);
		RegistryHandler.SaveToRegistry("WindowSize", WindowSize);
		RegistryHandler.SaveToRegistry("SplitterDistanceVertical", SplitterDistanceVertical);
		RegistryHandler.SaveToRegistry("SplitterDistanceHorizontal", SplitterDistanceHorizontal);
		RegistryHandler.SaveToRegistry("DescriptionFields", GetDescriptionFieldListToXml(_descriptionFields));
		RegistryHandler.SaveToRegistry("DescriptionTemplates", GetDescriptionTemplateListToXml(_descriptionTemplates));
		RegistryHandler.SaveToRegistry("TextDataFontFamily", TextDataFontFamily);
		RegistryHandler.SaveToRegistry("TextDataFontSize", TextDataFontSize);
		RegistryHandler.SaveToRegistry("DescriptionFontFamily", DescriptionFontFamily);
		RegistryHandler.SaveToRegistry("DescriptionFontSize", DescriptionFontSize);
		RegistryHandler.SaveToRegistry("CheckForUpdatesOnStart", CheckForUpdatesOnStart);
		RegistryHandler.SaveToRegistry("ShowDdlScript", ShowDdlScript);
		RegistryHandler.SaveToRegistry("SchemaName", SchemaName);
	}

	public static void LoadConfig()
	{
		ConnectionString = ConnectionStringSecurity.Decode(RegistryHandler.ReadFromRegistry("ConnectionString"), "ConnectionString");

		if (ConnectionString == "")
		{
			ConnectionString = @"Data Source=SQLServerName\SQLServerInstance;Initial Catalog=master;Integrated Security=True";
		}

		ConnectionStringToSave = ConnectionStringSecurity.Decode(RegistryHandler.ReadFromRegistry("ConnectionString"), "ConnectionString");

		if (ConnectionStringToSave == "")
		{
			ConnectionStringToSave = @"Data Source=SQLServerName\SQLServerInstance;Initial Catalog=master;Integrated Security=True";
		}

		SaveConnectionString = RegistryHandler.ReadFromRegistry("SaveConnectionString");

		if (SaveConnectionString == "")
		{
			SaveConnectionString = "True";
		}

		DescriptionFields = XmlToDescriptionFields(RegistryHandler.ReadFromRegistry("DescriptionFields"));
		DescriptionTemplates = XmlToDescriptionTemplates(RegistryHandler.ReadFromRegistry("DescriptionTemplates"));

		WindowSize = RegistryHandler.ReadFromRegistry("WindowSize");

		if (WindowSize == "")
		{
			WindowSize = "822; 555";
		}

		SplitterDistanceVertical = RegistryHandler.ReadFromRegistry("SplitterDistanceVertical");

		if (SplitterDistanceVertical == "")
		{
			SplitterDistanceVertical = "216";
		}

		SplitterDistanceHorizontal = RegistryHandler.ReadFromRegistry("SplitterDistanceHorizontal");

		if (SplitterDistanceHorizontal == "")
		{
			SplitterDistanceHorizontal = "200";
		}

		TextDataFontFamily = RegistryHandler.ReadFromRegistry("TextDataFontFamily");

		if (TextDataFontFamily == "")
		{
			TextDataFontFamily = "Courier New";
		}

		TextDataFontSize = RegistryHandler.ReadFromRegistry("TextDataFontSize");

		if (TextDataFontSize == "")
		{
			TextDataFontSize = "10";
		}

		DescriptionFontFamily = RegistryHandler.ReadFromRegistry("DescriptionFontFamily");

		if (DescriptionFontFamily == "")
		{
			DescriptionFontFamily = "Courier New";
		}

		DescriptionFontSize = RegistryHandler.ReadFromRegistry("DescriptionFontSize");

		if (DescriptionFontSize == "")
		{
			DescriptionFontSize = "10";
		}

		CheckForUpdatesOnStart = RegistryHandler.ReadFromRegistry("CheckForUpdatesOnStart");

		if (CheckForUpdatesOnStart == "")
		{
			CheckForUpdatesOnStart = "True";
		}

		ShowDdlScript = RegistryHandler.ReadFromRegistry("ShowDdlScript");

		if (ShowDdlScript == "")
		{
			ShowDdlScript = "True";
		}

		SchemaName = RegistryHandler.ReadFromRegistry("SchemaName");

		if (SchemaName == "")
		{
			SchemaName = "dbo";
		}

		UpdateServiceUrl = RegistryHandler.ReadFromRegistry("UpdateServiceUrl");

		if (UpdateServiceUrl == "")
		{
			UpdateServiceUrl = "http://virtcore.com/VirtcoreService.asmx";
			RegistryHandler.SaveToRegistry("UpdateServiceUrl", UpdateServiceUrl);
		}
	}

	public static List<DescriptionField> XmlToDescriptionFields(string xml)
	{
		if (xml == "")
		{
			return null;
		}

		List<DescriptionField> items = new List<DescriptionField>();

		try
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);

			XmlNodeList xmlNodeList = xmlDocument.SelectNodes("descriptionFields/descriptionField");

			if (xmlNodeList != null)
			{
				foreach (XmlElement xmlElement in xmlNodeList)
				{
					DescriptionField descriptionField = new DescriptionField(xmlElement.GetAttribute("displayText"), xmlElement.GetAttribute("databaseFieldName"), Convert.ToBoolean(xmlElement.GetAttribute("useForImage")), xmlElement.GetAttribute("information"));
					items.Add(descriptionField);
				}
			}
		}
		catch (Exception ex)
		{
			if (ex.Message == "Object reference not set to an instance of an object.")
			{
				MessageBox.Show("Description Field import file is missing one or more elements.", ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			else
			{
				MessageBox.Show(ex.Message, ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		return items;
	}

	public static List<DescriptionTemplate> XmlToDescriptionTemplates(string xml)
	{
		if (xml == "")
		{
			return null;
		}

		List<DescriptionTemplate> items = new List<DescriptionTemplate>();

		try
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);

			XmlNodeList xmlNodeList = xmlDocument.SelectNodes("descriptionTemplates/descriptionTemplate");

			if (xmlNodeList != null)
			{
				foreach (XmlElement xmlElement in xmlNodeList)
				{
					DescriptionTemplate descriptionTemplate = new DescriptionTemplate(xmlElement.GetAttribute("name"), xmlElement.GetAttribute("template"));
					items.Add(descriptionTemplate);
				}
			}
		}
		catch (Exception ex)
		{
			if (ex.Message == "Object reference not set to an instance of an object.")
			{
				MessageBox.Show("Description Template import file is missing one or more elements.", ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			else
			{
				MessageBox.Show(ex.Message, ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		return items;
	}

	public static string GetDescriptionFieldListToXml(List<DescriptionField> descriptionFields)
	{
		if (descriptionFields == null)
		{
			return "";
		}

		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?><descriptionFields>");

		foreach (DescriptionField descriptionField in descriptionFields)
		{
			stringBuilder.Append(string.Format("<descriptionField databaseFieldName=\"{0}\" displayText=\"{1}\" useForImage=\"{2}\" information=\"{3}\" />", System.Security.SecurityElement.Escape(descriptionField.DatabaseFieldName), System.Security.SecurityElement.Escape(descriptionField.DisplayText), descriptionField.UseForImage, System.Security.SecurityElement.Escape(descriptionField.Information)));
		}

		stringBuilder.Append("</descriptionFields>");

		return stringBuilder.ToString();
	}

	public static string GetDescriptionTemplateListToXml(List<DescriptionTemplate> descriptionTemplates)
	{
		if (descriptionTemplates == null)
		{
			return "";
		}

		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?><descriptionTemplates>");

		foreach (DescriptionTemplate descriptionTemplate in descriptionTemplates)
		{
			stringBuilder.Append(string.Format("<descriptionTemplate name=\"{0}\" template=\"{1}\" />", System.Security.SecurityElement.Escape(descriptionTemplate.Name), System.Security.SecurityElement.Escape(descriptionTemplate.Template)));
		}

		stringBuilder.Append("</descriptionTemplates>");

		return stringBuilder.ToString();
	}
}
