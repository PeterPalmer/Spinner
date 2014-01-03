using System.Windows;
using System.Windows.Media;

namespace Spinner.Shapes
{
	public class Bezier : IShape
	{
		private readonly Coordinate _point1, _point2, _point3;
		private readonly bool _isStroked;

		internal Coordinate Point1 { get { return _point1; } }
		internal Coordinate Point2 { get { return _point2; } }
		internal Coordinate Point3 { get { return _point3; } }

		public Bezier(double x1, double y1, double x2, double y2, double x3, double y3, bool isStroked)
		{
			_isStroked = isStroked;
			_point1 = new Coordinate(x1, y1, 0);
			_point2 = new Coordinate(x2, y2, 0);
			_point3 = new Coordinate(x3, y3, 0);
		}

		public Bezier(Point p1, Point p2, Point p3, bool isStroked)
		{
			_isStroked = isStroked;
			_point1 = new Coordinate(p1.X, p1.Y, 0);
			_point2 = new Coordinate(p2.X, p2.Y, 0);
			_point3 = new Coordinate(p3.X, p3.Y, 0);
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

		public void Move(double x, double y, double z)
		{
			_point1.Move(x, y, z);
			_point2.Move(x, y, z);
			_point3.Move(x, y, z);
		}

		public void Resize(double scaleFactor)
		{
			_point1.Resize(scaleFactor);
			_point2.Resize(scaleFactor);
			_point3.Resize(scaleFactor);

		}
	}
}
