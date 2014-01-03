using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spinner.Shapes;
using Spinner.SvgParser.CommandParsers;

namespace Spinner.Tests.SvgParser.CommandParsers
{
	[TestClass]
	public class LineCommandParserTests
	{
		[TestMethod]
		public void LineCommandParser_CreateShape()
		{
			Point[] points = new Point[] { new Point(5, 10) };
			PointsParameters parameters = new PointsParameters(points);
			LineCommandParser parser = new LineCommandParser();

			IShape shape = parser.CreateShape(parameters);

			Assert.IsInstanceOfType(shape, typeof(LineTo));
			LineTo line = shape as LineTo;
			Assert.AreEqual(5, line.EndPoint.X);
			Assert.AreEqual(10, line.EndPoint.Y);
		}
	}
}
