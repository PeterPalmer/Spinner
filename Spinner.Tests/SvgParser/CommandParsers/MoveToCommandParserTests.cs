using System;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spinner.Shapes;
using Spinner.SvgParser.CommandParsers;

namespace Spinner.Tests.SvgParser.CommandParser
{
	[TestClass]
	public class MoveToCommandParserTests
	{
		[TestMethod]
		public void MoveToCommandParser_CreateShape()
		{
			MoveToCommandParser parser = new MoveToCommandParser();
			Point[] points = new Point[] { new Point(12, 34) };
			PointsParameters parameters = new PointsParameters(points);

			IShape shape = parser.CreateShape(parameters);

			Assert.IsInstanceOfType(shape, typeof(MovePenTo));
			MovePenTo movePen = shape as MovePenTo;
			Assert.AreEqual(12, movePen.StartPosition.X);
			Assert.AreEqual(34, movePen.StartPosition.Y);
		}
	}
}
