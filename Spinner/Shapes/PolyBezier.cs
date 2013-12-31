using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Spinner.Shapes
{
	public class PolyBezier : PolyLine
	{
		public PolyBezier(PolyBezierSegment segment)
		{
			_isStroked = segment.IsStroked;
			_coordinates = new List<Coordinate>();
			foreach (Point point in segment.Points)
			{
				_coordinates.Add(new Coordinate(point.X, point.Y, Constants.InitialZ));
			}
		}

		public override void Draw(StreamGeometryContext ctx)
		{
			ctx.PolyBezierTo(this.GetProjectedPoints(), _isStroked, true);
		}
	}
}
