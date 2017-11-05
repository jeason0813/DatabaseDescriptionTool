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

using System.Drawing;
using System.Windows.Forms;

public static class SplitContainerGrip
{
	public static void PaintGrip(object sender, PaintEventArgs e)
	{
		Point[] points = new Point[3];
		SplitContainer control = sender as SplitContainer;

		if (control != null)
		{
			const int distanceBetweenPoints = 8;

			if (control.Orientation == Orientation.Horizontal)
			{
				points[0] = new Point((control.Width / 2), control.SplitterDistance + (control.SplitterWidth / 2));
				points[1] = new Point(points[0].X - distanceBetweenPoints, points[0].Y);
				points[2] = new Point(points[0].X + distanceBetweenPoints, points[0].Y);
			}
			else
			{
				points[0] = new Point(control.SplitterDistance + (control.SplitterWidth / 2), (control.Height / 2));
				points[1] = new Point(points[0].X, points[0].Y - distanceBetweenPoints);
				points[2] = new Point(points[0].X, points[0].Y + distanceBetweenPoints);
			}
		}

		foreach (Point p in points)
		{
			p.Offset(-1, -2);
			e.Graphics.FillEllipse(SystemBrushes.Window, new Rectangle(p, new Size(3, 3)));

			p.Offset(-1, -1);
			e.Graphics.FillEllipse(SystemBrushes.ControlDark, new Rectangle(p, new Size(3, 3)));
		}
	}
}
