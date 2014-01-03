using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using Spinner.Extensions;
using Spinner.Shapes;
using Spinner.SvgParser;
using Spinner.SvgParser.CommandParsers;

namespace Spinner
{
	public class SvgPathDataParser
	{
		private List<ShapeCollection> _shapeCollections = new List<ShapeCollection>();
		private double _minX, _maxX, _minY, _maxY;
		private Dictionary<char, CommandParser> _parsers;
		private readonly NumberFormatInfo _numberFormat;

		public SvgPathDataParser()
		{
			_minX = double.MaxValue;
			_maxX = double.MinValue;
			_minY = double.MaxValue;
			_maxY = double.MinValue;

			var culture = new CultureInfo("en-US");
			_numberFormat = culture.NumberFormat;

			_parsers = new Dictionary<char, CommandParser>();
			_parsers.Add('M', new MoveToCommandParser());
			_parsers.Add('m', new MoveToRelativeCommandParser());
			_parsers.Add('Z', new ClosePathCommandParser());
			_parsers.Add('z', _parsers['Z']);
			_parsers.Add('C', new CurveCommandParser());
			_parsers.Add('c', new CurveRelativeCommandParser());
			_parsers.Add('S', new SmoothCurveCommandParser());
			_parsers.Add('s', new SmoothCurveRelativeCommandParser());
			_parsers.Add('L', new LineCommandParser());
			_parsers.Add('l', new LineRelativeCommandParser());
			_parsers.Add('H', new HorizontalLineCommandParser());
			_parsers.Add('h', new HorizontalLineRelativeCommandParser());
			_parsers.Add('V', new VerticalLineCommandParser());
			_parsers.Add('v', new VerticalLineRelativeCommandParser());
			_parsers.Add('A', new EllipticalArcCommandParser());
			_parsers.Add('a', new EllipticalArcRelativeCommandParser());
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
			shapeCollection.StrokeWidth = svgPath.GetStrokeWidth();
			bool isFilled = shapeCollection.Fill != null;
			bool isStroked = shapeCollection.Stroke != null;
			Point endPoint = new Point(0, 0);
			Point controlPoint = new Point(0, 0);
			Point pathStartPoint = new Point(0, 0);
			MovePenTo beginFigure = null;

			var splittedCommands = Regex.Split(svgPath.Data, @"([a-dA-Df-zF-Z][-.,\s0-9e]*)");

			foreach (string command in splittedCommands)
			{
				if (String.IsNullOrWhiteSpace(command))
				{
					continue;
				}

				string commandParams = command.Substring(1);
				char commandChar = command[0];
				if (!_parsers.ContainsKey(commandChar))
				{
					throw new NotImplementedException("Unknown command: " + commandChar);
				}

				var parser = _parsers[commandChar];
				CommandParameters parameters = parser.ParseParams(commandParams.Trim());
				parameters.StartPoint = endPoint;
				parameters.PathStartPoint = pathStartPoint;
				parameters.ControlPoint = controlPoint;
				parameters.IsFilled = isFilled;
				parameters.IsStroked = isStroked;

				IShape shape = parser.CreateShape(parameters);
				endPoint = parser.GetEndPoint();
				controlPoint = parser.GetControlPoint();
				this.CompareToMaxMin(parameters);

				if (shape == null)
				{
					beginFigure.IsClosed = true;
					continue;
				}

				if (shape is MovePenTo)
				{
					beginFigure = shape as MovePenTo;
					pathStartPoint = ((MovePenTo)shape).StartPosition;
				}

				shapeCollection.Add(shape);
			}

			_shapeCollections.Add(shapeCollection);
		}

		private void CompareToMaxMin(double x, double y)
		{
			if (x < _minX) { _minX = x; }
			if (x > _maxX) { _maxX = x; }
			if (y < _minY) { _minY = y; }
			if (y > _maxY) { _maxY = y; }
		}

		private void CompareToMaxMin(CommandParameters parameters)
		{
			if (parameters is PointsParameters)
			{
				this.CompareToMaxMin(parameters as PointsParameters);
				return;
			}

			if (parameters is EllipticalArcParameterList)
			{
				this.CompareToMaxMin(parameters as EllipticalArcParameterList);
				return;
			}

			this.CompareToMaxMin(parameters.StartPoint.X, parameters.StartPoint.Y);
		}

		private void CompareToMaxMin(PointsParameters points)
		{
			points.ForEach((p) => this.CompareToMaxMin(p.X, p.Y));
		}

		private void CompareToMaxMin(EllipticalArcParameterList arcParamList)
		{
			this.CompareToMaxMin(arcParamList.StartPoint);
			arcParamList.ArcParameters.ForEach(prm => this.CompareToMaxMin(prm.EndPoint));
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

		public void GetEdges(out double minX, out double maxX, out double minY, out double maxY)
		{
			minX = _minX;
			maxX = _maxX;
			minY = _minY;
			maxY = _maxY;
		}
	}
}
