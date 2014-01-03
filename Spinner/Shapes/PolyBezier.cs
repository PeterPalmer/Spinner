using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Spinner.Shapes
{
	public class PolyBezier : PolyLine
	{
		public PolyBezier(IEnumerable<Point> points, bool isStroked)
		{
			_isStroked = isStroked;
			_coordinates = new List<Coordinate>();
			foreach (Point point in points)
			{
				_coordinates.Add(new Coordinate(point.X, point.Y, 0));
			}
		}

		public override void Draw(StreamGeometryContext ctx)
		{
			ctx.PolyBezierTo(this.GetProjectedPoints(), _isStroked, true);
		}
	}
}
