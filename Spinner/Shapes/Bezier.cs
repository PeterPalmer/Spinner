using System.Windows;
using System.Windows.Media;

namespace Spinner.Shapes
{
	public class Bezier : IShape
	{
		private readonly Coordinate _point1, _point2, _point3;
		private readonly bool _isStroked;

		public Bezier(BezierSegment segment)
		{
			_isStroked = segment.IsStroked;
			_point1 = new Coordinate(segment.Point1.X, segment.Point1.Y, Constants.InitialZ);
			_point2 = new Coordinate(segment.Point2.X, segment.Point2.Y, Constants.InitialZ);
			_point3 = new Coordinate(segment.Point3.X, segment.Point3.Y, Constants.InitialZ);
		}

		public Bezier(double x1, double y1, double x2, double y2, double x3, double y3, bool isStroked)
		{
			_isStroked = isStroked;
			_point1 = new Coordinate(x1, y1, Constants.InitialZ);
			_point2 = new Coordinate(x2, y2, Constants.InitialZ);
			_point3 = new Coordinate(x3, y3, Constants.InitialZ);
		}

		public Bezier(Point p1, Point p2, Point p3, bool isStroked)
		{
			_isStroked = isStroked;
			_point1 = new Coordinate(p1.X, p1.Y, Constants.InitialZ);
			_point2 = new Coordinate(p2.X, p2.Y, Constants.InitialZ);
			_point3 = new Coordinate(p3.X, p3.Y, Constants.InitialZ);
		}

		public void Draw(StreamGeometryContext ctx)
		{
			ctx.BezierTo(_point1.ProjectedPoint, _point2.ProjectedPoint, _point3.ProjectedPoint, _isStroked, false);
		}

		public void Pitch(double angle)
		{
			_point1.Pitch(angle);
			_point2.Pitch(angle);
			_point3.Pitch(angle);
		}

		public void Yaw(double angle)
		{
			_point1.Yaw(angle);
			_point2.Yaw(angle);
			_point3.Yaw(angle);
		}

		public void Move(int x, int y)
		{
			_point1.Move(x, y);
			_point2.Move(x, y);
			_point3.Move(x, y);
		}

		public void Resize(double scaleFactor)
		{
			_point1.Resize(scaleFactor);
			_point2.Resize(scaleFactor);
			_point3.Resize(scaleFactor);

		}
	}
}
