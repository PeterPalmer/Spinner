using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Spinner.Shapes
{
	public class MovePenTo : IShape
	{
		private List<Coordinate> _coordinates;
		private bool _isFilled;
		private bool _isStroked;

		public bool IsClosed { get; set; }

		public Point StartPosition
		{
			get { return new Point(_coordinates[0].X, _coordinates[0].Y); }
		}

		public MovePenTo(double x, double y, double z)
		{
			_coordinates = new List<Coordinate>();
			_coordinates.Add(new Coordinate(x, y, z));
		}

		public MovePenTo(IEnumerable<Point> points, bool isFilled, bool isStroked)
		{
			_isFilled = isFilled;
			_isStroked = isStroked;
			_coordinates = new List<Coordinate>();
			foreach (var point in points)
			{
				_coordinates.Add(new Coordinate(point.X, point.Y, 0));
			}
		}

		public void Draw(StreamGeometryContext ctx)
		{
			ctx.BeginFigure(_coordinates[0].ProjectedPoint, _isFilled, this.IsClosed);

			foreach (var coord in _coordinates.Skip(1))
			{
				ctx.LineTo(coord.ProjectedPoint, _isStroked, false);
			}
		}

		public void Pitch(double angle)
		{
			_coordinates.ForEach(c => c.Pitch(angle));
		}

		public void Yaw(double angle)
		{
			_coordinates.ForEach(c => c.Yaw(angle));
		}

		public void Move(double x, double y, double z)
		{
			_coordinates.ForEach(c => c.Move(x, y, z));
		}

		public void Resize(double scaleFactor)
		{
			_coordinates.ForEach(c => c.Resize(scaleFactor));
		}
	}
}
