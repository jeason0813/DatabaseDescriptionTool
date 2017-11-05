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
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

public partial class CheckForUpdatesForm : Form
{
	public delegate void UpdateCheckCompleteEventHandler(string errorMessage, Version version, bool anyUpdates, bool anyErrors);
	public event UpdateCheckCompleteEventHandler UpdateCheckCompleteEvent;

	private bool _initialCheckForUpdatesOnStart;
	private const string _localFileName = "DatabaseDescriptionTool.msi";
	private bool _downloading;
	private BackgroundWorker _worker;
	private bool _forceQuit;

	public CheckForUpdatesForm()
	{
		InitializeComponent();
		Initialize();
	}

	public void CheckForUpdates()
	{
		_worker.RunWorkerAsync();
	}

	public bool GetForceQuit()
	{
		return _forceQuit;
	}

	private void Initialize()
	{
		Size = new Size(410, 160);
		Text = string.Format("{0} - Check for updates", ConfigHandler.ApplicationName);

		checkOnStartCheckBox.Checked = Convert.ToBoolean(ConfigHandler.CheckForUpdatesOnStart);
		_initialCheckForUpdatesOnStart = checkOnStartCheckBox.Checked;

		infoTextBox.Text = string.Format("{0}\r\nCurrent version {1}", ConfigHandler.ApplicationName, ConfigHandler.Version);

		InitializeWorker();
	}

	private void InitializeWorker()
	{
		_worker = new BackgroundWorker();
		_worker.DoWork += Worker_DoWork;
		_worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
	}

	private void FireUpdateCheckCompleteEvent(string errorMessage, Version version, bool anyUpdates, bool anyErrors)
	{
		if (UpdateCheckCompleteEvent != null)
		{
			UpdateCheckCompleteEvent(errorMessage, version, anyUpdates, anyErrors);
		}
	}

	private static void Worker_DoWork(object sender, DoWorkEventArgs e)
	{
		WorkerResult result = new WorkerResult();

		try
		{
			Version serverVersion = new Version(GetLatestVersion());
			Version currentVersion = new Version(ConfigHandler.Version);

			if (serverVersion > currentVersion)
			{
				string changelogUrl = GetChangelogUrl();

				WebClient webClient = new WebClient();
				webClient.DownloadFile(new Uri(changelogUrl), string.Format(@"{0}\Changelog.txt", ConfigHandler.TempPath));

				result.Changelog = ParseChangelog(currentVersion, serverVersion);
				result.Version = serverVersion;
				result.AnyUpdates = true;
			}
		}
		catch
		{
			result.ErrorMessage = "Could not contact update server.";
			result.AnyErrors = true;
		}

		e.Result = result;
	}

	private static string ParseChangelog(Version currentVersion, Version serverVersion)
	{
		string[] changelogLines = File.ReadAllLines(string.Format(@"{0}\Changelog.txt", ConfigHandler.TempPath));

		StringBuilder sb = new StringBuilder();

		bool allignedWithServerVersionWithRevision = false;
		bool allignedWithServerVersionWithoutRevision = false;

		foreach (string changelogLine in changelogLines)
		{
			if (changelogLine == string.Format("{0}.{1}.{2}", serverVersion.Major, serverVersion.Minor, serverVersion.Build))
			{
				allignedWithServerVersionWithoutRevision = true;
				allignedWithServerVersionWithRevision = false;
			}

			if (changelogLine == string.Format("{0}.{1}.{2}.{3}", serverVersion.Major, serverVersion.Minor, serverVersion.Build, serverVersion.Revision))
			{
				allignedWithServerVersionWithRevision = true;
				allignedWithServerVersionWithoutRevision = false;
			}

			if (allignedWithServerVersionWithoutRevision)
			{
				if (changelogLine != string.Format("{0}.{1}.{2}", currentVersion.Major, currentVersion.Minor, currentVersion.Build))
				{
					sb.AppendLine(changelogLine);
				}
				else
				{
					break;
				}
			}

			if (allignedWithServerVersionWithRevision)
			{
				if (changelogLine != string.Format("{0}.{1}.{2}.{3}", currentVersion.Major, currentVersion.Minor, currentVersion.Build, currentVersion.Revision))
				{
					sb.AppendLine(changelogLine);
				}
				else
				{
					break;
				}
			}
		}

		return sb.ToString().Substring(0, sb.Length - 2);
	}

	private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		WorkerResult result = (WorkerResult)e.Result;

		updateButton.Enabled = true;
		closebutton.Enabled = true;
		_downloading = false;

		if (result.AnyErrors)
		{
			updateButton.Text = "Check";
			infoTextBox.Text = string.Format("{0}\r\nCurrent version {1}\r\n\r\n{2}", ConfigHandler.ApplicationName, ConfigHandler.Version, result.ErrorMessage);
		}
		else
		{
			string newVersionInfo;

			if (result.AnyUpdates)
			{
				Size = new Size(410, 300);
				changelogGroupBox.Visible = true;
				changelogTextBox.Visible = true;
				changelogTextBox.Text = result.Changelog;

				updateButton.Text = "Update";
				newVersionInfo = string.Format("New version {0} available.", result.Version);
			}
			else
			{
				updateButton.Text = "Check";
				newVersionInfo = "Latest version already installed.";
			}

			infoTextBox.Text = string.Format("{0}\r\nCurrent version {1}\r\n\r\n{2}", ConfigHandler.ApplicationName, ConfigHandler.Version, newVersionInfo);
		}

		FireUpdateCheckCompleteEvent(result.ErrorMessage, result.Version, result.AnyUpdates, result.AnyErrors);
	}

	private class WorkerResult
	{
		public string ErrorMessage;
		public Version Version;
		public bool AnyUpdates;
		public bool AnyErrors;
		public string Changelog;
	}

	private void UpdateButton_Click(object sender, EventArgs e)
	{
		if (updateButton.Text == "Check")
		{
			updateButton.Text = "Checking...";
			updateButton.Enabled = false;
			closebutton.Enabled = false;
			_downloading = true;

			_worker.RunWorkerAsync();
		}
		else
		{
			updateButton.Text = "Downloading...";
			updateButton.Enabled = false;
			closebutton.Enabled = false;
			_downloading = true;

			PerformUpdate();
		}
	}

	private void Closebutton_Click(object sender, EventArgs e)
	{
		if (checkOnStartCheckBox.Checked != _initialCheckForUpdatesOnStart)
		{
			ConfigHandler.CheckForUpdatesOnStart = checkOnStartCheckBox.Checked.ToString();
			ConfigHandler.SaveConfig();
		}
	}

	private void PerformUpdate()
	{
		WebClient webClient = new WebClient();
		webClient.DownloadFileCompleted += Completed;

		string downloadUrl = GetDownloadUrl();

		if (downloadUrl != null)
		{
			webClient.DownloadFileAsync(new Uri(downloadUrl), string.Format(@"{0}\{1}", ConfigHandler.TempPath, _localFileName));
		}
	}

	private static string GetDownloadUrl()
	{
		ServiceHandler service = new ServiceHandler(new Uri(ConfigHandler.UpdateServiceUrl));
		object[] args = { ConfigHandler.ApplicationName };
		return service.InvokeMethod<string>("VirtcoreService", "GetDownloadUrl", args);
	}

	private static string GetChangelogUrl()
	{
		ServiceHandler service = new ServiceHandler(new Uri(ConfigHandler.UpdateServiceUrl));
		object[] args = { ConfigHandler.ApplicationName };
		return service.InvokeMethod<string>("VirtcoreService", "GetChangelogUrl", args);
	}

	private static string GetLatestVersion()
	{
		ServiceHandler service = new ServiceHandler(new Uri(ConfigHandler.UpdateServiceUrl));
		object[] args = { ConfigHandler.ApplicationName };
		return service.InvokeMethod<string>("VirtcoreService", "GetLatestVersion", args);
	}

	private void Completed(object sender, AsyncCompletedEventArgs e)
	{
		Exception ex = e.Error;

		if (ex == null)
		{
			updateButton.Text = "Downloaded";
			Process.Start(string.Format(@"{0}\{1}", ConfigHandler.TempPath, _localFileName));

			_downloading = false;
			_forceQuit = true;
			Close();
		}
		else
		{
			MessageBox.Show(string.Format("Error downloading update.\r\n\r\n{0}", ex.Message), ConfigHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			_downloading = false;
			updateButton.Text = "Update";
			updateButton.Enabled = true;
			closebutton.Enabled = true;
		}
	}

	private void CheckForUpdatesForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (_downloading)
		{
			e.Cancel = true;
		}
	}
}
