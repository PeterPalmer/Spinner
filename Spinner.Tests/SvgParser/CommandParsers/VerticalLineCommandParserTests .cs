using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spinner.Shapes;
using Spinner.SvgParser.CommandParsers;

namespace Spinner.Tests.SvgParser.CommandParsers
{
	[TestClass]
	public class VerticalLineCommandParserTests
	{
		[TestMethod]
		public void VerticaLineCommandParser_CreateShape()
		{
			DoubleParameter parameter = new DoubleParameter(42);
			parameter.StartPoint = new Point(10, 20);
			var parser = new VerticalLineCommandParser();

			IShape shape = parser.CreateShape(parameter);

			Assert.IsInstanceOfType(shape, typeof(LineTo));
			LineTo line = shape as LineTo;
			Assert.AreEqual(10, line.EndPoint.X);
			Assert.AreEqual(42, line.EndPoint.Y);
		}

		[TestMethod]
		public void VerticalLineRelativeCommandParser_CreateShape()
		{
			DoubleParameter parameter = new DoubleParameter(42);
			parameter.StartPoint = new Point(10, 20);
			var parser = new VerticalLineRelativeCommandParser();

			IShape shape = parser.CreateShape(parameter);

			Assert.IsInstanceOfType(shape, typeof(LineTo));
			LineTo line = shape as LineTo;
			Assert.AreEqual(10, line.EndPoint.X);
			Assert.AreEqual(62, line.EndPoint.Y);
		}
	}
}
