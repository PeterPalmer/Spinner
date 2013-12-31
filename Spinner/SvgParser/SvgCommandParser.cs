using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using Spinner.Shapes;
using Spinner.SvgParser;
using Spinner.Extensions;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Spinner
{
	public class SvgCommandParser
	{
		private List<ShapeCollection> _shapeCollections = new List<ShapeCollection>();
		private double _minX, _maxX, _minY, _maxY;
		private Dictionary<char, Func<Point[], Point, Point, IShape>> _commandParsers;
		private readonly NumberFormatInfo _numberFormat;
		private bool _isStroked;

		public SvgCommandParser()
		{
			_minX = double.MaxValue;
			_maxX = double.MinValue;
			_minY = double.MaxValue;
			_maxY = double.MinValue;

			var culture = new CultureInfo("en-US");
			_numberFormat = culture.NumberFormat;

			_commandParsers = new Dictionary<char, Func<Point[], Point, Point, IShape>>();
			_commandParsers.Add('m', ParseMoveCommand);
			_commandParsers.Add('M', ParseMoveCommand);
			_commandParsers.Add('z', ParseClosePathCommand);
			_commandParsers.Add('Z', ParseClosePathCommand);
			_commandParsers.Add('C', ParseCurveCommand);
			_commandParsers.Add('c', ParseRelativeCurveCommand);
			_commandParsers.Add('S', ParseSmoothCurveCommand);
			_commandParsers.Add('s', ParseRelativeSmoothCurveCommand);
			_commandParsers.Add('L', ParseLineCommand);
			_commandParsers.Add('l', ParseRelativeLineCommand);
			_commandParsers.Add('H', ParseHorizontalLineCommand);
			_commandParsers.Add('h', ParseRelativeHorizontalLineCommand);
			_commandParsers.Add('V', ParseVerticalLineCommand);
			_commandParsers.Add('v', ParseRelativeVerticalLineCommand);
		}

		private IShape ParseHorizontalLineCommand(Point[] points, Point startPoint, Point controlPoint)
		{
			points[0].Y = startPoint.Y;
			return new LineTo(points[0], _isStroked);
		}

		private IShape ParseRelativeHorizontalLineCommand(Point[] points, Point startPoint, Point controlPoint)
		{
			points[0].X += startPoint.X;
			points[0].Y = startPoint.Y;
			return new LineTo(points[0], _isStroked);
		}

		private IShape ParseVerticalLineCommand(Point[] points, Point startPoint, Point controlPoint)
		{
			points[0].Y = points[0].X;
			points[0].X = startPoint.X;
			return new LineTo(points[0], _isStroked);
		}

		private IShape ParseRelativeVerticalLineCommand(Point[] points, Point startPoint, Point controlPoint)
		{
			points[0].Y = points[0].X + startPoint.Y;
			points[0].X = startPoint.X;
			return new LineTo(points[0], _isStroked);
		}

		private IShape ParseMoveCommand(Point[] points, Point startPoint, Point controlPoint)
		{
			return new MovePenTo(points[0].X, points[0].Y, Constants.InitialZ);
		}

		private IShape ParseClosePathCommand(Point[] points, Point startPoint, Point controlPoint)
		{
			return null;
		}

		private IShape ParseCurveCommand(Point[] points, Point startPoint, Point controlPoint)
		{
			if (points.Length != 3)
			{
				throw new ArgumentException("Invalid number of parameters in Curve Command!");
			}

			return new Bezier(points[0], points[1], points[2], _isStroked);
		}

		private IShape ParseRelativeCurveCommand(Point[] points, Point startPoint, Point controlPoint)
		{
			for (int i = 0; i < points.Length; i++)
			{
				points[i].X += startPoint.X;
				points[i].Y += startPoint.Y;
			}

			return new Bezier(points[0], points[1], points[2], _isStroked);
		}

		private IShape ParseSmoothCurveCommand(Point[] points, Point startPoint, Point controlPoint)
		{
			if (points.Length != 2)
			{
				throw new ArgumentException("Invalid number of parameters in Curve Command!");
			}

			Point firstControlPoint = new Point(2 * startPoint.X - controlPoint.X, 2 * startPoint.Y - controlPoint.Y);

			return new Bezier(firstControlPoint, points[0], points[1], _isStroked);
		}

		private IShape ParseRelativeSmoothCurveCommand(Point[] points, Point startPoint, Point controlPoint)
		{
			if (points.Length != 2)
			{
				throw new ArgumentException("Invalid number of parameters in Curve Command!");
			}

			for (int i = 0; i < points.Length; i++)
			{
				points[i].X += startPoint.X;
				points[i].Y += startPoint.Y;
			}

			Point firstControlPoint = new Point(2 * startPoint.X - controlPoint.X, 2 * startPoint.Y - controlPoint.Y);

			return new Bezier(firstControlPoint, points[0], points[1], _isStroked);
		}

		private IShape ParseLineCommand(Point[] points, Point startPoint, Point controlPoint)
		{
			if (points.Length != 1)
			{
				throw new ArgumentException("Invalid number of parameters in Line Command!");
			}

			LineTo line = new LineTo(points[0], _isStroked);
			return line;
		}

		private IShape ParseRelativeLineCommand(Point[] points, Point startPoint, Point controlPoint)
		{
			if (points.Length != 1)
			{
				throw new ArgumentException("Invalid number of parameters in Line Command!");
			}

			points[0].X += startPoint.X;
			points[0].Y += startPoint.Y;

			LineTo line = new LineTo(points[0], _isStroked);
			return line;
		}

		public SpinnerModel ConstructModel()
		{
			return new SpinnerModel(_shapeCollections);
		}

		public void AddShapes(SvgPath svgPath)
		{
			ShapeCollection shapeCollection = new ShapeCollection();
			shapeCollection.Fill = svgPath.GetFillBrush();
			shapeCollection.Stroke = svgPath.GetStrokeBrush();
			_isStroked = shapeCollection.Stroke != null;
			Point endPoint = new Point(0, 0);
			Point controlPoint = new Point(0, 0);

			var splittedCommands = Regex.Split(svgPath.Data, "([a-zA-Z][0-9.,-]*)");

			foreach (string command in splittedCommands)
			{
				if (String.IsNullOrWhiteSpace(command))
				{
					continue;
				}

				char commandChar = command[0];
				if (!_commandParsers.ContainsKey(commandChar))
				{
					throw new NotImplementedException("Unknown command: " + commandChar);
				}

				Point[] points = ToPointArray(command.Substring(1));
				IShape shape = _commandParsers[commandChar](points, endPoint, controlPoint);

				if(points.Length > 0)
				{
					endPoint = points[points.Length-1];
					this.CompareToMaxMin(endPoint);
				}

				if (shape is Bezier)
				{
					controlPoint = points[points.Length - 2];
				}
				else
				{
					controlPoint = endPoint;
				}

				if (shape == null)
				{
					continue;
				}

				shapeCollection.Add(shape);
			}

			_shapeCollections.Add(shapeCollection);
		}

		public void AddShapesApprox(SvgPath svgPath)
		{

			Path parsedPath = (Path)XamlReader.Parse(svgPath.ToString());
			if (!(parsedPath.Data is StreamGeometry))
			{
				return;
			}

			ShapeCollection shapes = new ShapeCollection();
			shapes.Fill = parsedPath.Fill;
			shapes.Stroke = parsedPath.Stroke;
			_shapeCollections.Add(shapes);

			var pathGeo = ((StreamGeometry)parsedPath.Data).GetFlattenedPathGeometry();

			foreach (var fig in pathGeo.Figures)
			{
				var startPos = this.GetStartPosition(fig.Segments[0]);

				shapes.Add(new MovePenTo(startPos.X, startPos.Y, Constants.InitialZ));

				foreach (var segment in fig.Segments)
				{
					if (segment is PolyLineSegment)
					{
						var polyLineSeg = segment as PolyLineSegment;
						shapes.Add(new PolyLine(polyLineSeg));

						polyLineSeg.Points.ForEach(this.CompareToMaxMin);
					}
					else if (segment is PolyBezierSegment)
					{
						var polyBezierSeg = segment as PolyBezierSegment;
						shapes.Add(new PolyBezier(polyBezierSeg));

						polyBezierSeg.Points.ForEach(this.CompareToMaxMin);
					}
					else if (segment is BezierSegment)
					{
						var bezierSeg = segment as BezierSegment;
						shapes.Add(new Bezier(bezierSeg));
						this.CompareToMaxMin(bezierSeg.Point1);
					}
					else
					{
						throw new NotImplementedException("Unknown segment type: " + segment.GetType().ToString());
					}
				}
			}
		}

		private void CompareToMaxMin(double x, double y)
		{
			if (x < _minX) { _minX = x; }
			if (x > _maxX) { _maxX = x; }
			if (y < _minY) { _minY = y; }
			if (y > _maxY) { _maxY = y; }
		}

		private void CompareToMaxMin(Point p)
		{
			this.CompareToMaxMin(p.X, p.Y);
		}

		private void CompareToMaxMin(Point[] points)
		{
			foreach (Point p in points)
			{
				this.CompareToMaxMin(p);
			}
		}

		private Point GetStartPosition(PathSegment segment)
		{
			if (segment is PolyLineSegment)
			{
				return ((PolyLineSegment)segment).Points[0];
			}
			else if (segment is PolyBezierSegment)
			{
				return ((PolyBezierSegment)segment).Points[0];
			}
			else if (segment is BezierSegment)
			{
				return ((BezierSegment)segment).Point1;
			}

			throw new NotImplementedException();
		}

		public void GetEdges(out double minX, out double maxX, out double minY, out double maxY)
		{
			minX = _minX;
			maxX = _maxX;
			minY = _minY;
			maxY = _maxY;
		}

		private double ToDouble(string value)
		{
			double result;

			if (!Double.TryParse(value, System.Globalization.NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, _numberFormat, out result))
			{
				throw new InvalidCastException(value + " is not a valid double!");
			}

			return result;
		}

		internal double[] ToDoubleArray(string value)
		{
			string[] doubleStrings = value.Replace("-", ",-").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			double[] result = new double[doubleStrings.Length];

			for (int i = 0; i < doubleStrings.Length; i++)
			{
				result[i] = ToDouble(doubleStrings[i].Trim());
			}

			return result;
		}

		internal Point[] ToPointArray(string inputString)
		{
			double[] doubles = this.ToDoubleArray(inputString);

			if (doubles.Length == 1)
			{
				return new Point[] { new Point(doubles[0], 0) };
			}

			if (doubles.Length % 2 == 1)
			{
				throw new ArgumentException("Input string must contain even number of doubles.");
			}

			Point[] points = new Point[doubles.Length / 2];

			for (int i = 0; i < points.Length; i++)
			{
				points[i].X = doubles[i*2];
				points[i].Y = doubles[i*2 + 1];
			}

			return points;
		}
	}
}
