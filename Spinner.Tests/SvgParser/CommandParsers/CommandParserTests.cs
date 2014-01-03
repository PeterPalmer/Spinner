using System;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spinner.SvgParser.CommandParsers;

namespace Spinner.Tests.SvgParser.CommandParser
{
	[TestClass]
	public class CommandParserTests
	{
		private class TestableCommandParser : Spinner.SvgParser.CommandParsers.CommandParser
		{
			public override Spinner.Shapes.IShape CreateShape(CommandParameters parameters)
			{
				throw new NotImplementedException();
			}
		}

		[TestMethod]
		public void CommandParser_ToPointArray()
		{
			// Arrange
			var parser = new TestableCommandParser();
			string inputString = "1.1,2.2-3.3, 4.4";

			// Act
			Point[] points = parser.ToPointArray(inputString);

			// Assert
			Assert.AreEqual(2, points.Length);
			Assert.AreEqual(1.1D, points[0].X);
			Assert.AreEqual(2.2D, points[0].Y);
			Assert.AreEqual(-3.3D, points[1].X);
			Assert.AreEqual(4.4D, points[1].Y);
		}

		[TestMethod]
		public void CommandParser_ToPointArrayEmptyString()
		{
			// Arrange
			var parser = new TestableCommandParser();
			string inputString = String.Empty;

			// Act
			Point[] points = parser.ToPointArray(inputString);

			// Assert
			Assert.AreEqual(0, points.Length);
		}

		[TestMethod]
		public void CommandParser_ToBool_1()
		{
			var parser = new TestableCommandParser();

			bool result = parser.ToBool("1");

			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void CommandParser_ToBool_0()
		{
			var parser = new TestableCommandParser();

			bool result = parser.ToBool("0");

			Assert.AreEqual(false, result);
		}
	}
}
