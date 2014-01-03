using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spinner.Shapes;
using Spinner.SvgParser.CommandParsers;

namespace Spinner.Tests.SvgParser.CommandParsers
{
	[TestClass]
	public class CurveRelativeCommandParserTests
	{
		[TestMethod]
		public void CurveRelativeCommandParser_CreateShape()
		{
			// Arrange
			Point[] points = new Point[] { new Point(1, 2), new Point(3, 4), new Point(5, 6) };
			PointsParameters parameters = new PointsParameters(points);
			parameters.StartPoint = new Point(10, 10);
			var parser = new CurveRelativeCommandParser();

			// Act
			IShape shape = parser.CreateShape(parameters);
			Point controlPoint = parser.GetControlPoint();
			Point endPoint = parser.GetEndPoint();

			// Assert
			Assert.AreEqual(13, controlPoint.X);
			Assert.AreEqual(14, controlPoint.Y);
			Assert.AreEqual(15, endPoint.X);
			Assert.AreEqual(16, endPoint.Y);
			Assert.IsInstanceOfType(shape, typeof(Bezier));
			Bezier bezier = shape as Bezier;
			Assert.AreEqual(11, bezier.Point1.X);
			Assert.AreEqual(12, bezier.Point1.Y);
		}
	}
}
