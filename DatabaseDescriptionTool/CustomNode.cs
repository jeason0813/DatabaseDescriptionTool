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
using System.Windows.Forms;

public class CustomNode : TreeNode
{
	public readonly NodeType Type;
	public readonly CustomNode ParentNode;
	public readonly int OrderBy;
	public readonly bool DescriptionChangeable;
	public readonly string ParentName;
	public List<DescriptionItem> Descriptions;

	public void UpdateImage()
	{
		SelectedImageIndex = Convert.ToInt32(GetNodeImage(Descriptions));
		ImageIndex = Convert.ToInt32(GetNodeImage(Descriptions));
	}

	public void ClearDescriptions()
	{
		Descriptions.Clear();
	}

	public void AddDescription(DescriptionItem item)
	{
		Descriptions.Add(item);
	}

	public CustomNode(NodeType type, CustomNode parentNode, string text, NodeImage image, int orderBy)
	{
		Type = type;
		ParentNode = parentNode;
		Text = text;
		Name = text;
		SelectedImageIndex = Convert.ToInt32(image);
		ImageIndex = Convert.ToInt32(image);
		OrderBy = orderBy;
	}

	public CustomNode(NodeType type, CustomNode parentNode, string text, List<DescriptionItem> descriptions, string parentName)
	{
		Type = type;
		ParentNode = parentNode;
		Text = text;
		Name = text;
		DescriptionChangeable = true;
		ParentName = parentName;
		Descriptions = descriptions;
		SelectedImageIndex = Convert.ToInt32(GetNodeImage(Descriptions));
		ImageIndex = Convert.ToInt32(GetNodeImage(Descriptions));
	}

	public CustomNode(NodeType type)
	{
		Type = type;
	}

	private static NodeImage GetNodeImage(List<DescriptionItem> descriptions)
	{
		foreach (DescriptionItem item in descriptions)
		{
			if (item.Type.UseForImage)
			{
				if (item.Description != null)
				{
					return NodeImage.BookAdd;
				}
			}
		}

		return NodeImage.Book;
	}
}
