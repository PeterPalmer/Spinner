using System;
using System.Windows;
using Spinner.Extensions;
using Spinner.Shapes;

namespace Spinner.SvgParser.CommandParsers
{
	public class CurveRelativeCommandParser : CurveCommandParser
	{
		public override IShape CreateShape(CommandParameters parameters)
		{
			var points = parameters as PointsParameters;
			Point currentPoint = parameters.StartPoint;
			for (int i = 0; i < points.Length; i++)
			{
				points[i] = points[i].Add(currentPoint);

				if (i % 3 == 2)
				{
					currentPoint = points[i];
				}
			}

			return base.CreateShape(points);
		}
	}
}
