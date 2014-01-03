using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spinner.Shapes;
using Spinner.SvgParser.CommandParsers;

namespace Spinner.Tests.SvgParser.CommandParsers
{
	[TestClass]
	public class LineRelativeCommandParserTests
	{
		[TestMethod]
		public void LineRelativeCommandParser_CreateShape()
		{
			Point[] points = new Point[] { new Point(5, 10) };
			PointsParameters parameters = new PointsParameters(points);
			parameters.StartPoint = new Point(3, 3);
			LineRelativeCommandParser parser = new LineRelativeCommandParser();

			IShape shape = parser.CreateShape(parameters);

			Assert.IsInstanceOfType(shape, typeof(LineTo));
			LineTo line = shape as LineTo;
			Assert.AreEqual(8, line.EndPoint.X);
			Assert.AreEqual(13, line.EndPoint.Y);
		}
	}
}
