using System;
using System.Windows;
using Spinner.Shapes;

namespace Spinner.SvgParser.CommandParsers
{
	public class LineCommandParser : CommandParser
	{
		public override IShape CreateShape(CommandParameters parameters)
		{
			var points = parameters as PointsParameters;
			if (points.Length == 0)
			{
				throw new ArgumentException("Missing parameters for Line Command!");
			}

			if (points.Length > 1)
			{
				return CreatePolyLine(points);
			}

			_endPoint = points[0];
			LineTo line = new LineTo(_endPoint, parameters.IsStroked);

			return line;
		}

		protected IShape CreatePolyLine(PointsParameters points)
		{
			_endPoint = points[points.Length - 1];
			return new PolyLine(points, points.IsStroked);
		}
	}
}
