using System;
using System.Windows;
using Spinner.Extensions;
using Spinner.Shapes;

namespace Spinner.SvgParser.CommandParsers
{
	public class LineRelativeCommandParser : LineCommandParser
	{
		public override IShape CreateShape(CommandParameters parameters)
		{
			var points = parameters as PointsParameters;
			Point currentPoint = parameters.StartPoint;
			for (int i = 0; i < points.Length; i++)
			{
				points[i] = points[i].Add(currentPoint);
				currentPoint = points[i];
			}

			if (points.Length == 0)
			{
				throw new ArgumentException("Missing parameters for Line Command!");
			}

			if (points.Length > 1)
			{
				return base.CreatePolyLine(points);
			}

			_endPoint = points[0];
			LineTo line = new LineTo(_endPoint, parameters.IsStroked);

			return line;
		}
	}
}
