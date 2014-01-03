using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace Spinner.SvgParser.CommandParsers
{
	public class PointsParameters : CommandParameters, IEnumerable<Point>
	{
		private Point[] _points;

		public int Length
		{
			get
			{
				return _points.Length;
			}
		}

		public PointsParameters(Point[] points)
		{
			_points = points;
		}

		public IEnumerator<Point> GetEnumerator()
		{
			return ((IEnumerable<Point>)_points).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _points.GetEnumerator();
		}

		public Point this[int i]
		{
			get
			{
				return _points[i];
			}
			set
			{
				_points[i] =  value;
			}
		}
	}
}
