/*
Copyright (C) 2005 SharpDevelop

Modified 2017 by Lars Hove Christiansen
http://virtcore.com

This file is a part of ICSharpCode.TextEditor

	This library is free software; you can redistribute it and/or modify it
	under the terms of the GNU Lesser General Public License as published
	by the Free Software Foundation; either version 2.1 of the License, or
	(at your option) any later version.

	This library is distributed in the hope that it will be useful, but
	WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser
	General Public License for more details.

	You should have received a copy of the GNU Lesser General Public
	License along with this library; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
*/

using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace ICSharpCode.TextEditor.Document
{
	public class SyntaxMode
	{
		private string fileName;
		private string name;
		private string[] extensions;

		public string FileName
		{
			get
			{
				return fileName;
			}
			set
			{
				fileName = value;
			}
		}

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public string[] Extensions
		{
			get
			{
				return extensions;
			}
			set
			{
				extensions = value;
			}
		}

		public SyntaxMode(string fileName, string name, string extensions)
		{
			this.fileName = fileName;
			this.name = name;
			this.extensions = extensions.Split(';', '|', ',');
		}

		public SyntaxMode(string fileName, string name, string[] extensions)
		{
			this.fileName = fileName;
			this.name = name;
			this.extensions = extensions;
		}

		public static List<SyntaxMode> GetSyntaxModes(Stream xmlSyntaxModeStream)
		{
			XmlTextReader reader = new XmlTextReader(xmlSyntaxModeStream);
			List<SyntaxMode> syntaxModes = new List<SyntaxMode>();

			while (reader.Read())
			{
				switch (reader.NodeType)
				{
					case XmlNodeType.Element:
						switch (reader.Name)
						{
							case "SyntaxModes":
								string version = reader.GetAttribute("version");

								if (version != "1.0")
								{
									throw new HighlightingDefinitionInvalidException("Unknown syntax mode file defininition with version " + version);
								}
								break;
							case "Mode":
								syntaxModes.Add(new SyntaxMode(reader.GetAttribute("file"), reader.GetAttribute("name"), reader.GetAttribute("extensions")));
								break;
							default:
								throw new HighlightingDefinitionInvalidException("Unknown node in syntax mode file :" + reader.Name);
						}
						break;
				}
			}

			reader.Close();
			return syntaxModes;
		}

		public override string ToString()
		{
			return string.Format("[SyntaxMode: FileName={0}, Name={1}, Extensions=({2})]", fileName, name, string.Join(",", extensions));
		}
	}
}
