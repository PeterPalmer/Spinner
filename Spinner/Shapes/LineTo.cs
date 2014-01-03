using System.Windows;
using System.Windows.Media;

namespace Spinner.Shapes
{
	class LineTo : IShape
	{
		private Coordinate _endPoint;
		private bool _isStroked;

		public Point EndPoint
		{
			get
			{
				return new Point(_endPoint.X, _endPoint.Y);
			}
		}

		public LineTo(Point endPoint, bool isStroked)
		{
			_endPoint = new Coordinate(endPoint.X, endPoint.Y, 0);
			_isStroked = isStroked;
		}

		public LineTo(double x, double y, bool isStroked)
		{
			_endPoint = new Coordinate(x, y, 0);
			_isStroked = isStroked;
		}

		public void Draw(StreamGeometryContext ctx)
		{
			ctx.LineTo(_endPoint.ProjectedPoint, _isStroked, false);
		}

		public void Pitch(double angle)
		{
			_endPoint.Pitch(angle);
		}

		public void Yaw(double angle)
		{
			_endPoint.Yaw(angle);
		}

		public void Move(double x, double y, double z)
		{
			_endPoint.Move(x, y, z);
		}

		public void Resize(double scaleFactor)
		{
			_endPoint.Resize(scaleFactor);
		}
	}
}
