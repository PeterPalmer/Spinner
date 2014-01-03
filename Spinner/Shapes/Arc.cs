using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Spinner.Shapes
{
	class ArcParams
	{
		public Coordinate EndPoint { get; set; }
		public Size Size { get; set; }
		public double RotationAngle { get; set; }
		public bool IsLarge { get; set; }
		public SweepDirection SweepDirection { get; set; }
		public bool IsStroked { get; set; }

		public ArcParams(Point endPoint, Size size, double rotationAngle, bool isLarge, bool clockwise, bool isStroked)
		{
			EndPoint = new Coordinate(endPoint.X, endPoint.Y, 0);
			Size = size;
			RotationAngle = rotationAngle;
			IsLarge = isLarge;
			SweepDirection = clockwise ? SweepDirection.Clockwise : SweepDirection.Counterclockwise;
			IsStroked = isStroked;
		}
	}

	class Arc : IShape
	{
		private readonly List<ArcParams> _parameters;

		public Point EndPoint 
		{
			get 
			{
				return new Point(_parameters.Last().EndPoint.X, _parameters.Last().EndPoint.Y);
			}
		}

		public Arc(Point endPoint, Size size, double rotationAngle, bool isLarge, bool clockwise, bool isStroked)
		{
			_parameters = new List<ArcParams>();
			_parameters.Add(new ArcParams(endPoint, size, rotationAngle, isLarge, clockwise, isStroked));
		}

		public void AddArc(Point endPoint, Size size, double rotationAngle, bool isLarge, bool clockwise, bool isStroked)
		{
			_parameters.Add(new ArcParams(endPoint, size, rotationAngle, isLarge, clockwise, isStroked));
		}

		public void Draw(StreamGeometryContext ctx)
		{
			foreach (var arcParams in _parameters)
			{
				ctx.ArcTo(arcParams.EndPoint.ProjectedPoint, arcParams.Size, arcParams.RotationAngle, arcParams.IsLarge, arcParams.SweepDirection, arcParams.IsStroked, false);
			}
		}

		public void Pitch(double angle)
		{
			_parameters.ForEach(prms => prms.EndPoint.Pitch(angle));
		}

		public void Yaw(double angle)
		{
			_parameters.ForEach(prms => prms.EndPoint.Yaw(angle));
		}

		public void Move(double x, double y, double z)
		{
			_parameters.ForEach(prms => prms.EndPoint.Move(x, y, z));
		}

		public void Resize(double scaleFactor)
		{
			foreach (var param in _parameters)
			{
				param.EndPoint.Resize(scaleFactor);
				param.Size = new Size(param.Size.Width * scaleFactor, param.Size.Height * scaleFactor);
			}
		}
	}
}
