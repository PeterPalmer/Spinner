using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Spinner.Shapes
{
	public class PolyLine : IShape
	{
		protected List<Coordinate> _coordinates;
		protected bool _isStroked;

		public PolyLine(params Coordinate[] coords)
		{
			_coordinates = new List<Coordinate>();
			_coordinates.AddRange(coords);
		}

		public PolyLine(IEnumerable<Point> points, bool isStroked)
		{
			_isStroked = isStroked;

			_coordinates = new List<Coordinate>();
			foreach (Point point in points)
			{
				_coordinates.Add(new Coordinate(point.X, point.Y, 0));
			}
		}

		public virtual void Draw(StreamGeometryContext ctx)
		{
			ctx.PolyLineTo(this.GetProjectedPoints(), _isStroked, true);
		}

		protected IList<Point> GetProjectedPoints()
		{
			return _coordinates.Select((c) => c.ProjectedPoint).ToList();
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
