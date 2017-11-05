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
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		DynamicAssembly.EnableDynamicLoadingForDlls(Assembly.GetExecutingAssembly(), "DatabaseDescriptionTool.Resources.Assemblies");

		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
		Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
		Application.ThreadException += ApplicationThreadException;
		AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;

		Application.Run(new MainForm());
	}

	private static void ProcessException(Exception ex)
	{
		ErrorForm form = new ErrorForm("Exit", ex.Message, ex.StackTrace);
		form.ShowDialog();
	}

	private static void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
	{
		ProcessException(e.Exception);
	}

	private static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
	{
		ProcessException(e.ExceptionObject as Exception);
	}
}
