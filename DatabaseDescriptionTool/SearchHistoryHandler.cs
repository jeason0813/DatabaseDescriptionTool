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

using System.Collections.Generic;
using System.Windows.Forms;

public static class SearchHistoryHandler
{
	public static void AddItem(ComboBox comboBox, string item, string registryKeyName)
	{
		List<string> existingItemsExceptFromCurrent = new List<string>();

		foreach (string existingItem in comboBox.Items)
		{
			if (existingItem != item)
			{
				existingItemsExceptFromCurrent.Add(existingItem);
			}
		}

		comboBox.Items.Clear();
		comboBox.Items.Add(item);

		for (int i = 0; i < existingItemsExceptFromCurrent.Count; i++)
		{
			if (i < ConfigHandler.NumberOfSearchHistoryItems - 1 && existingItemsExceptFromCurrent[i] != item)
			{
				comboBox.Items.Add(existingItemsExceptFromCurrent[i]);
			}
		}

		SaveValuesToRegistry(registryKeyName, comboBox);
	}

	public static void LoadItems(ComboBox comboBox, string registryKeyName)
	{
		comboBox.Items.Clear();

		string[] items = LoadValuesFromRegistry(registryKeyName);

		foreach (string item in items)
		{
			comboBox.Items.Add(item);
		}
	}

	private static string[] LoadValuesFromRegistry(string registryKeyName)
	{
		string items = RegistryHandler.ReadFromRegistry(registryKeyName);
		return items.Split(new[] { '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
	}

	private static void SaveValuesToRegistry(string registryKeyName, ComboBox comboBox)
	{
		string items = "";

		foreach (string item in comboBox.Items)
		{
			items = string.Format("{0}{1}\r", items, item);
		}

		items = items.Substring(0, items.Length - 1);

		RegistryHandler.SaveToRegistry(registryKeyName, items);
	}
}
