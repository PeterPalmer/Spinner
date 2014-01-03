using System;
using System.Diagnostics;
using System.Windows;

namespace Spinner.Shapes
{
	[DebuggerDisplay("{_x} {_y} {_z}")]
	public class Coordinate
	{
		private double _x;
		private double _y;
		private double _z;

		private static double _xOffset = 250;
		private static double _yOffset = 200;

		public double ProjectedX
		{
			get
			{
				return (Constants.Perspective * _x) / (Constants.Perspective - this._z) + _xOffset;
			}
		}

		public double ProjectedY
		{
			get
			{
				return (Constants.Perspective * _y) / (Constants.Perspective - this._z) + _yOffset;
			}
		}

		public Point ProjectedPoint
		{
			get
			{
				return new Point(this.ProjectedX, this.ProjectedY);
			}
		}

		internal double X
		{
			get
			{
				return _x;
			}
		}

		internal double Y
		{
			get
			{
				return _y;
			}
		}

		public Coordinate(double x, double y, double z)
		{
			_x = x;
			_y = y;
			_z = z;
		}

		public static void SetOffsets(double xOffset, double yOffset)
		{
			_xOffset = xOffset;
			_yOffset = yOffset;
		}

		public void Pitch(double angle)
		{
			double oldZ = _z;
			_z = _z * Math.Cos(angle) + _y * Math.Sin(angle);
			_y = _y * Math.Cos(angle) - oldZ * Math.Sin(angle);
		}

		public void Yaw(double angle)
		{
			double oldX = _x;
			_x = _x * Math.Cos(angle) + _z * Math.Sin(angle);
			_z = _z * Math.Cos(angle) - oldX * Math.Sin(angle);
		}

		public void Move(double xDistance, double yDistance, double zDistance)
		{
			_x += xDistance;
			_y += yDistance;
			_z += zDistance;
		}

		public void Resize(double scaleFactor)
		{
			_x *= scaleFactor;
			_y *= scaleFactor;
			_z *= scaleFactor;
		}

	}
}
