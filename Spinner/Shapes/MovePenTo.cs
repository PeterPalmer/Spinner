using System;
using System.Windows.Media;

namespace Spinner.Shapes
{
	public class MovePenTo : IShape
	{
		private readonly Coordinate _startPosition;

		public Coordinate StartPosition
		{
			get { return _startPosition; }
		}

		public MovePenTo(double x, double y, double z)
		{
			_startPosition = new Coordinate(x, y, z);
		}

		public void Draw(StreamGeometryContext ctx)
		{
			ctx.BeginFigure(_startPosition.ProjectedPoint, true, false);
		}

		public void Pitch(double angle)
		{
			_startPosition.Pitch(angle);
		}

		public void Yaw(double angle)
		{
			_startPosition.Yaw(angle);
		}

		public void Move(int x, int y)
		{
			_startPosition.Move(x, y);
		}

		public void Resize(double scaleFactor)
		{
			_startPosition.Resize(scaleFactor);
		}
	}
}
