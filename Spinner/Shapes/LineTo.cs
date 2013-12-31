using System.Windows;
using System.Windows.Media;

namespace Spinner.Shapes
{
	class LineTo : IShape
	{
		private Coordinate _endPoint;
		private bool _isStroked;

		public LineTo(Point endPoint, bool isStroked)
		{
			_endPoint = new Coordinate(endPoint.X, endPoint.Y, Constants.InitialZ);
			_isStroked = isStroked;
		}

		public LineTo(double x, double y, bool isStroked)
		{
			_endPoint = new Coordinate(x, y, Constants.InitialZ);
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

		public void Move(int x, int y)
		{
			_endPoint.Move(x, y);
		}

		public void Resize(double scaleFactor)
		{
			_endPoint.Resize(scaleFactor);
		}
	}
}
