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

		public PolyLine(PolyLineSegment segment)
		{
			_isStroked = segment.IsStroked;
			_coordinates = new List<Coordinate>();
			foreach(Point point in segment.Points)
			{
				_coordinates.Add(new Coordinate(point.X, point.Y, Constants.InitialZ));
			}
		}

		public PolyLine(params Coordinate[] coords)
		{
			_coordinates = new List<Coordinate>();
			_coordinates.AddRange(coords);
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

		public void Move(int x, int y)
		{
			_coordinates.ForEach(c => c.Move(x, y));
		}

		public void Resize(double scaleFactor)
		{
			_coordinates.ForEach(c => c.Resize(scaleFactor));
		}
	}
}
