using System;
using System.Windows;
using Spinner.Shapes;

namespace Spinner.SvgParser.CommandParsers
{
	public class CurveCommandParser : CommandParser
	{
		protected Point _controlPoint;

		public override IShape CreateShape(CommandParameters parameters)
		{
			var points = parameters as PointsParameters;

			if (points.Length < 3)
			{
				throw new ArgumentException("Invalid number of parameters in Curve Command!");
			}

			if (points.Length > 3)
			{
				return CreatePolyBezier(points);
			}

			_endPoint = points[2];
			_controlPoint = points[1];

			return new Bezier(points[0], points[1], points[2], parameters.IsStroked);
		}

		public override Point GetControlPoint()
		{
			return _controlPoint;
		}

		protected IShape CreatePolyBezier(PointsParameters points)
		{
			_endPoint = points[points.Length - 1];
			_controlPoint = points[points.Length - 2];

			//System.Collections.Generic.List<Point> lp = new System.Collections.Generic.List<Point>();
			//lp.Insert(0, points.StartPoint);
			//lp.AddRange(points);

			return new PolyBezier(points, points.IsStroked);
		}
	}
}
