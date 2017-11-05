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

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace ICSharpCode.TextEditor.Document
{
	public class FileSyntaxModeProvider : ISyntaxModeFileProvider
	{
		private readonly string directory;
		private List<SyntaxMode> syntaxModes;

		public ICollection<SyntaxMode> SyntaxModes
		{
			get
			{
				return syntaxModes;
			}
		}

		public FileSyntaxModeProvider(string directory)
		{
			this.directory = directory;
			UpdateSyntaxModeList();
		}

		public void UpdateSyntaxModeList()
		{
			string syntaxModeFile = Path.Combine(directory, "SyntaxModes.xml");

			if (File.Exists(syntaxModeFile))
			{
				Stream s = File.OpenRead(syntaxModeFile);
				syntaxModes = SyntaxMode.GetSyntaxModes(s);
				s.Close();
			}
			else
			{
				syntaxModes = ScanDirectory(directory);
			}
		}

		public XmlTextReader GetSyntaxModeFile(SyntaxMode syntaxMode)
		{
			string syntaxModeFile = Path.Combine(directory, syntaxMode.FileName);

			if (!File.Exists(syntaxModeFile))
			{
				throw new HighlightingDefinitionInvalidException("Can't load highlighting definition " + syntaxModeFile + " (file not found)!");
			}

			return new XmlTextReader(File.OpenRead(syntaxModeFile));
		}

		private static List<SyntaxMode> ScanDirectory(string directory)
		{
			string[] files = Directory.GetFiles(directory);
			List<SyntaxMode> modes = new List<SyntaxMode>();

			foreach (string file in files)
			{
				if (Path.GetExtension(file).Equals(".XSHD", StringComparison.OrdinalIgnoreCase))
				{
					XmlTextReader reader = new XmlTextReader(file);

					while (reader.Read())
					{
						if (reader.NodeType == XmlNodeType.Element)
						{
							switch (reader.Name)
							{
								case "SyntaxDefinition":
									string name = reader.GetAttribute("name");
									string extensions = reader.GetAttribute("extensions");
									modes.Add(new SyntaxMode(Path.GetFileName(file), name, extensions));
									goto bailout;
								default:
									throw new HighlightingDefinitionInvalidException("Unknown root node in syntax highlighting file :" + reader.Name);
							}
						}
					}

					bailout:
					reader.Close();
				}
			}
			return modes;
		}
	}
}
