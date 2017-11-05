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

using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Text;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

public class DdlHandler
{
	public delegate void PrefetchCompleteEventHandler(bool requestPending);
	public event PrefetchCompleteEventHandler PrefetchCompleteEvent;

	private readonly DatabaseOperation _databaseOperation;
	private Database _database;
	private Scripter _scripter;
	private string _initializedOnDatabase;
	private BackgroundWorker _prefetchWorker;
	private bool _requestPending;
	private string _requestedInDatabase;

	public DdlHandler(DatabaseOperation databaseOperation)
	{
		_databaseOperation = databaseOperation;
	}

	public string GetDdl(CustomNode node)
	{
		_databaseOperation.FireBeginCommEvent();

		if (_prefetchWorker.IsBusy && !ShouldGetUsingObjectDefinition(node))
		{
			if (!_requestPending)
			{
				_requestedInDatabase = _databaseOperation.GetDatabaseName();
			}

			_requestPending = true;

			return "Generating Object Definition Script...";
		}
		else
		{
			return DoGetDdl(node);
		}
	}

	public void Prefetch()
	{
		if (_prefetchWorker != null && _prefetchWorker.IsBusy)
		{
			return;
		}

		if (_databaseOperation.GetDatabaseName() != _initializedOnDatabase)
		{
			_prefetchWorker = new BackgroundWorker();
			_prefetchWorker.DoWork += PrefetchWorker_DoWork;
			_prefetchWorker.RunWorkerCompleted += PrefetchWorker_RunWorkerCompleted;
			_prefetchWorker.RunWorkerAsync(_databaseOperation.GetDatabaseName());
		}
	}

	private void PrefetchWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		if (_requestPending && _requestedInDatabase != _databaseOperation.GetDatabaseName())
		{
			_requestedInDatabase = _databaseOperation.GetDatabaseName();
			Prefetch();
		}
		else
		{
			FirePrefetchCompleteEvent(_requestPending);

			_requestPending = false;
		}
	}

	private void PrefetchWorker_DoWork(object sender, DoWorkEventArgs e)
	{
		string prefetchInDatabase = e.Argument.ToString();

		SqlConnection sqlConnection = new SqlConnection(_databaseOperation.GetConnectionString().ToString());
		ServerConnection serverConnection = new ServerConnection(sqlConnection);
		Server server = new Server(serverConnection);

		_database = server.Databases[prefetchInDatabase];
		_database.DefaultSchema = ConfigHandler.SchemaName;
		_database.PrefetchObjects(typeof(Table));

		_scripter = new Scripter(server);
		_scripter.Options.DriAllConstraints = true;

		_initializedOnDatabase = prefetchInDatabase;
	}

	private void FirePrefetchCompleteEvent(bool requestPending)
	{
		if (PrefetchCompleteEvent != null)
		{
			PrefetchCompleteEvent(requestPending);
		}
	}

	private string DoGetDdl(CustomNode node)
	{
		string ddl;

		if (ShouldGetUsingObjectDefinition(node))
		{
			ddl = GetUsingObjectDefinition(_databaseOperation, node);

			if (ddl.StartsWith("\n"))
			{
				return ddl.Substring(1);
			}
		}
		else
		{
			ddl = GetUsingSmo(node);
		}

		_databaseOperation.FireEndCommEvent();

		return ddl;
	}

	private static bool ShouldGetUsingObjectDefinition(CustomNode node)
	{
		if (node.Type == NodeType.TableTrigger || node.Type == NodeType.View || node.Type == NodeType.ViewColumn || node.Type == NodeType.ViewTrigger || node.Type == NodeType.StoredProcedure || node.Type == NodeType.TableValuedFunction || node.Type == NodeType.ScalarValuedFunction || node.Type == NodeType.TableValuedFunctionParameter || node.Type == NodeType.ScalarValuedFunctionParameter || node.Type == NodeType.StoredProcedureParameter)
		{
			return true;
		}

		return false;
	}

	private static string GetUsingObjectDefinition(DatabaseOperation databaseOperation, CustomNode node)
	{
		string nodeNameToUse = "";

		switch (node.Type)
		{
			case NodeType.TableValuedFunction:
			case NodeType.ScalarValuedFunction:
			case NodeType.View:
			case NodeType.ViewTrigger:
			case NodeType.TableTrigger:
			case NodeType.StoredProcedure:
				nodeNameToUse = node.Name;
				break;
			case NodeType.TableValuedFunctionParameter:
			case NodeType.ScalarValuedFunctionParameter:
			case NodeType.StoredProcedureParameter:
			case NodeType.ViewColumn:
				nodeNameToUse = node.ParentName;
				break;
		}

		return databaseOperation.GetObjectDefinition(nodeNameToUse);
	}

	private string GetUsingSmo(CustomNode node)
	{
		StringCollection stringCollection = new StringCollection();

		switch (node.Type)
		{
			case NodeType.TableValuedFunction:
			case NodeType.ScalarValuedFunction:
				stringCollection = _scripter.Script(new[] { _database.UserDefinedFunctions[node.Name].Urn });
				break;
			case NodeType.TableValuedFunctionParameter:
			case NodeType.ScalarValuedFunctionParameter:
				stringCollection = _scripter.Script(new[] { _database.UserDefinedFunctions[node.ParentName].Urn });
				break;
			case NodeType.StoredProcedure:
				stringCollection = _scripter.Script(new[] { _database.StoredProcedures[node.Name].Urn });
				break;
			case NodeType.StoredProcedureParameter:
				stringCollection = _scripter.Script(new[] { _database.StoredProcedures[node.ParentName].Urn });
				break;
			case NodeType.Table:
				stringCollection = _scripter.Script(new[] { _database.Tables[node.Name].Urn });
				break;
			case NodeType.TableConstraint:
			case NodeType.TableColumn:
				stringCollection = _scripter.Script(new[] { _database.Tables[node.ParentName].Urn });
				break;
			case NodeType.TableTrigger:
				stringCollection = _scripter.Script(new[] { _database.Tables[node.ParentName].Triggers[node.Name].Urn });
				break;
			case NodeType.ViewTrigger:
				stringCollection = _scripter.Script(new[] { _database.Views[node.ParentName].Triggers[node.Name].Urn });
				break;
			case NodeType.View:
				stringCollection = _scripter.Script(new[] { _database.Views[node.Name].Urn });
				break;
			case NodeType.ViewColumn:
				stringCollection = _scripter.Script(new[] { _database.Views[node.ParentName].Urn });
				break;
			case NodeType.TableKey:
				if (_database.Tables[node.ParentName].Indexes[node.Name] != null)
				{
					stringCollection = _scripter.Script(new[] { _database.Tables[node.ParentName].Indexes[node.Name].Urn });
				}
				else
				{
					stringCollection = _scripter.Script(new[] { _database.Tables[node.ParentName].ForeignKeys[node.Name].Urn });
				}
				break;
			case NodeType.TableIndex:
				stringCollection = _scripter.Script(new[] { _database.Tables[node.ParentName].Indexes[node.Name].Urn });
				break;
			case NodeType.ViewIndex:
				stringCollection = _scripter.Script(new[] { _database.Views[node.ParentName].Indexes[node.Name].Urn });
				break;
		}

		return StringCollectionToString(stringCollection);
	}

	private static string StringCollectionToString(StringCollection stringCollection)
	{
		StringBuilder sb = new StringBuilder();

		foreach (string s in stringCollection)
		{
			sb.AppendLine(s);
		}

		return sb.ToString();
	}
}
