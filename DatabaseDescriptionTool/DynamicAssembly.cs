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
using System.IO;
using System.Reflection;

public static class DynamicAssembly
{
	public static void EnableDynamicLoadingForDlls(Assembly assemblyToLoadFrom, string embeddedResourcePrefix)
	{
		AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
		{
			string resName = embeddedResourcePrefix + "." + args.Name.Split(',')[0] + ".dll";

			using (Stream input = assemblyToLoadFrom.GetManifestResourceStream(resName))
			{
				if (input != null)
				{
					return Assembly.Load(StreamToBytes(input));
				}
				else
				{
					return null;
				}
			}
		};
	}

	private static byte[] StreamToBytes(Stream input)
	{
		int capacity;

		if (input.CanSeek)
		{
			capacity = (int)input.Length;
		}
		else
		{
			capacity = 0;
		}

		using (MemoryStream output = new MemoryStream(capacity))
		{
			int readLength;
			byte[] buffer = new byte[4096];

			do
			{
				readLength = input.Read(buffer, 0, buffer.Length);
				output.Write(buffer, 0, readLength);
			}
			while (readLength != 0);

			return output.ToArray();
		}
	}
}
