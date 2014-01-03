using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spinner.Shapes;
using Spinner.SvgParser.CommandParsers;

namespace Spinner.Tests.SvgParser.CommandParsers
{
	[TestClass]
	public class SmoothCurveRelativeCommandParserTests
	{
		[TestMethod]
		public void SmoothCurveRelativeCommand_CreateShape()
		{
			// Arrange
			Point[] points = new Point[] { new Point(1, 2), new Point(3, 4) };
			PointsParameters parameters = new PointsParameters(points);
			parameters.StartPoint = new Point(10, 10);
			parameters.ControlPoint = new Point(5, 5);
			var parser = new SmoothCurveRelativeCommandParser();

			// Act
			IShape shape = parser.CreateShape(parameters);
			Point controlPoint = parser.GetControlPoint();
			Point endPoint = parser.GetEndPoint();

			// Assert
			Assert.IsInstanceOfType(shape, typeof(Bezier));
			Bezier bezier = shape as Bezier;
			Assert.AreEqual(15, bezier.Point1.X);
			Assert.AreEqual(15, bezier.Point1.Y);
			Assert.AreEqual(11, bezier.Point2.X);
			Assert.AreEqual(12, bezier.Point2.Y);
			Assert.AreEqual(13, bezier.Point3.X);
			Assert.AreEqual(14, bezier.Point3.Y);
			Assert.AreEqual(controlPoint.X, bezier.Point2.X);
			Assert.AreEqual(controlPoint.Y, bezier.Point2.Y);
			Assert.AreEqual(endPoint.X, bezier.Point3.X);
			Assert.AreEqual(endPoint.Y, bezier.Point3.Y);
		}
	}
}
