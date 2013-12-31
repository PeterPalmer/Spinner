using System;
using System.Collections.Generic;
using System.Xml;

namespace Spinner.SvgParser
{
	public class SvgFileParser
	{
		private double _minX, _maxX, _minY, _maxY;

		public SvgFileParser()
		{
		}

		public SpinnerModel ParseFile(String path)
		{
			IEnumerable<SvgPath> parsedPaths;

			using (var reader = new XmlTextReader(path))
			{
				parsedPaths = this.ParsePathsXml(reader);
			}

			var builder = new SvgCommandParser();

			foreach (var pathData in parsedPaths)
			{
				builder.AddShapes(pathData);
			}

			builder.GetEdges(out _minX, out _maxX, out _minY, out _maxY);

			var model= builder.ConstructModel();
			CenterOverOrigin(model);

			return model;
		}

		private void CenterOverOrigin(SpinnerModel model)
		{
			int xOffset = Convert.ToInt32((_maxX - _minX) / 2 + _minX);
			int yOffset = Convert.ToInt32((_maxY - _minY) / 2 + _minY);

			model.Move(xOffset * -1, yOffset * -1);
		}

		public double CalculateScaleFactor(double canvasWidth, double canvasHeight)
		{
			double scaleFactor = Math.Min(canvasWidth / (_maxX - _minX), canvasHeight / (_maxY - _minY));
			return scaleFactor / 3.5D;
		}

		public IEnumerable<SvgPath> ParsePathsXml(XmlReader reader)
		{
			var paths = new List<SvgPath>();
			reader.MoveToContent();
			while (reader.Read())
			{
				if (reader.NodeType != XmlNodeType.Element)
				{
					continue;
				}

				if (String.Compare(reader.Name, "path", true) == 0)
				{
					string data = reader.GetAttribute("d");
					string fill = reader.GetAttribute("fill");
					string stroke = reader.GetAttribute("stroke");

					paths.Add(new SvgPath(data, fill, stroke));
				}
			}

			return paths;
		}

	}
}
