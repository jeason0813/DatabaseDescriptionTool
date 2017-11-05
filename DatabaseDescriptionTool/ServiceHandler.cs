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
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Services.Description;
using System.Xml;

public class ServiceHandler
{
	private readonly Dictionary<string, Type> _availableTypes;
	private readonly Assembly _webServiceAssembly;
	private readonly List<string> _services;

	public List<string> AvailableServices
	{
		get
		{
			return _services;
		}
	}

	public ServiceHandler(Uri webServiceUri)
	{
		_services = new List<string>();
		_availableTypes = new Dictionary<string, Type>();
		_webServiceAssembly = BuildAssemblyFromWSDL(webServiceUri);
		Type[] types = _webServiceAssembly.GetExportedTypes();

		foreach (Type type in types)
		{
			_services.Add(type.FullName);
			_availableTypes.Add(type.FullName, type);
		}
	}

	public List<string> EnumerateServiceMethods(string serviceName)
	{
		List<string> methods = new List<string>();

		if (!_availableTypes.ContainsKey(serviceName))
		{
			throw new Exception("Service Not Available");
		}
		else
		{
			Type type = _availableTypes[serviceName];

			foreach (MethodInfo minfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly))
			{
				methods.Add(minfo.Name);
			}

			return methods;
		}
	}

	public T InvokeMethod<T>(string serviceName, string methodName, params object[] args)
	{
		object obj = _webServiceAssembly.CreateInstance(serviceName);
		Type type = obj.GetType();
		return (T)type.InvokeMember(methodName, BindingFlags.InvokeMethod, null, obj, args);
	}

	private static ServiceDescriptionImporter BuildServiceDescriptionImporter(XmlTextReader xmlreader)
	{
		if (!ServiceDescription.CanRead(xmlreader))
		{
			throw new Exception("Invalid Web Service Description");
		}

		ServiceDescription serviceDescription = ServiceDescription.Read(xmlreader);

		ServiceDescriptionImporter descriptionImporter = new ServiceDescriptionImporter();
		descriptionImporter.ProtocolName = "Soap";
		descriptionImporter.AddServiceDescription(serviceDescription, null, null);
		descriptionImporter.Style = ServiceDescriptionImportStyle.Client;
		descriptionImporter.CodeGenerationOptions = System.Xml.Serialization.CodeGenerationOptions.GenerateProperties;

		return descriptionImporter;
	}

	private static Assembly CompileAssembly(ServiceDescriptionImporter descriptionImporter)
	{
		CodeNamespace codeNamespace = new CodeNamespace();
		CodeCompileUnit codeUnit = new CodeCompileUnit();
		codeUnit.Namespaces.Add(codeNamespace);

		ServiceDescriptionImportWarnings importWarnings = descriptionImporter.Import(codeNamespace, codeUnit);

		if (importWarnings == 0)
		{
			CodeDomProvider compiler = CodeDomProvider.CreateProvider("CSharp");
			string[] references = { "System.Web.Services.dll", "System.Xml.dll" };
			CompilerParameters parameters = new CompilerParameters(references);
			CompilerResults results = compiler.CompileAssemblyFromDom(parameters, codeUnit);

			if (results.Errors.Count > 0)
			{
				throw new Exception("Compilation Error Creating Assembly");
			}

			return results.CompiledAssembly;
		}
		else
		{
			throw new Exception("Invalid WSDL");
		}
	}

	private static Assembly BuildAssemblyFromWSDL(Uri webServiceUri)
	{
		if (string.IsNullOrEmpty(webServiceUri.ToString()))
		{
			throw new Exception("Web Service Not Found");
		}

		XmlTextReader xmlreader = new XmlTextReader(string.Format("{0}?wsdl", webServiceUri));
		ServiceDescriptionImporter descriptionImporter = BuildServiceDescriptionImporter(xmlreader);

		return CompileAssembly(descriptionImporter);
	}
}
