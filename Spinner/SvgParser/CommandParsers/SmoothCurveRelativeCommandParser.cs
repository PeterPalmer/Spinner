﻿using System;
using System.Windows;
using Spinner.Extensions;
using Spinner.Shapes;

namespace Spinner.SvgParser.CommandParsers
{
	class SmoothCurveRelativeCommandParser : SmoothCurveCommandParser
	{
		public override IShape CreateShape(CommandParameters parameters)
		{
			var points = parameters as PointsParameters;
			if (points.Length != 2)
			{
				throw new ArgumentException("Invalid number of parameters in Curve Command!");
			}

			Point currentPoint = parameters.StartPoint;
			for (int i = 0; i < points.Length; i++)
			{
				points[i] = points[i].Add(currentPoint);

				if (i % 2 == 1)
				{
					currentPoint = points[i];
				}
			}

			return base.CreateShape(points);
		}
	}
}
