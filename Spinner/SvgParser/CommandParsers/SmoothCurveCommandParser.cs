using System;
using System.Windows;
using Spinner.Shapes;

namespace Spinner.SvgParser.CommandParsers
{
	class SmoothCurveCommandParser : CommandParser
	{
		private Point _secondControlPoint;

		public override IShape CreateShape(CommandParameters parameters)
		{
			var points = parameters as PointsParameters;
			if (points.Length != 2)
			{
				throw new ArgumentException("Invalid number of parameters in Curve Command!");
			}

			Point firstControlPoint = new Point(2 * parameters.StartPoint.X - parameters.ControlPoint.X, 2 * parameters.StartPoint.Y - parameters.ControlPoint.Y);
			_secondControlPoint = points[0]; 
			_endPoint = points[1];

			return new Bezier(firstControlPoint, points[0], points[1], parameters.IsStroked);
		}

		public override Point GetControlPoint()
		{
			return _secondControlPoint;
		}
	}
}
