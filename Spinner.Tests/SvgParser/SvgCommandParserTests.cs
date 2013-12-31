using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spinner.SvgParser;
using System.Windows;

namespace Spinner.Tests
{
	[TestClass]
	public class SvgCommandParserTests
	{
		[TestMethod]
		public void FindEges()
		{
			var parser = new SvgCommandParser();
			parser.AddShapes(new SvgPath("M-100,50C8,11,-100,7,15,-75z", "", ""));
			parser.AddShapes(new SvgPath("M150,-75c150,30,40,40,30,50z", "", ""));
			double minX, maxX, minY, maxY;

			parser.GetEdges(out minX, out maxX, out minY, out maxY);

			Assert.AreEqual(-100, minX);
			Assert.AreEqual(180, maxX);
			Assert.AreEqual(-75, minY);
			Assert.AreEqual(50, maxY);
		}

		[TestMethod]
		public void AddShapes()
		{
			var parser = new SvgCommandParser();
			//builder.AddShapes(new SvgPath("M12,34C5,6,7,8,9,10z", "", ""));
			parser.AddShapes(new SvgPath("M16,1.466C7.973,1.466,1.466,7.973,1.466,16c0,8.027,6.507,14.534,14.534,14.534c8.027,0,14.534-6.507,14.534-14.534C30.534,7.973,24.027,1.466,16,1.466zM16,29.534C8.539,29.534,2.466,23.462,2.466,16C2.466,8.539,8.539,2.466,16,2.466c7.462,0,13.535,6.072,13.535,13.533C29.534,23.462,23.462,29.534,16,29.534zM11.104,14c0.932,0,1.688-1.483,1.688-3.312s-0.755-3.312-1.688-3.312s-1.688,1.483-1.688,3.312S10.172,14,11.104,14zM20.729,14c0.934,0,1.688-1.483,1.688-3.312s-0.756-3.312-1.688-3.312c-0.932,0-1.688,1.483-1.688,3.312S19.798,14,20.729,14zM8.143,21.189C10.458,24.243,13.148,26,16.021,26c2.969,0,5.745-1.868,8.11-5.109c-2.515,1.754-5.292,2.734-8.215,2.734C13.164,23.625,10.54,22.756,8.143,21.189z", "#000000", ""));
		}

		[TestMethod]
		public void ToDoubleArray()
		{
			// Arrange
			var parser = new SvgCommandParser();
			string inputString = "1.1,2.2,3.3,4.4";

			// Act
			var doubles = parser.ToDoubleArray(inputString);

			// Assert
			Assert.AreEqual(4, doubles.Length);
			Assert.AreEqual(1.1D, doubles[0]);
			Assert.AreEqual(2.2D, doubles[1]);
			Assert.AreEqual(3.3D, doubles[2]);
			Assert.AreEqual(4.4D, doubles[3]);
		}

		[TestMethod]
		public void ToPointArray()
		{
			// Arrange
			var parser = new SvgCommandParser();
			string inputString = "1.1,2.2,3.3,4.4";

			// Act
			Point[] points = parser.ToPointArray(inputString);

			// Assert
			Assert.AreEqual(2, points.Length);
			Assert.AreEqual(1.1D, points[0].X);
			Assert.AreEqual(2.2D, points[0].Y);
			Assert.AreEqual(3.3D, points[1].X);
			Assert.AreEqual(4.4D, points[1].Y);
		}

		[TestMethod]
		public void ToPointArray_EmptyString()
		{
			// Arrange
			var parser = new SvgCommandParser();
			string inputString = "";

			// Act
			Point[] points = parser.ToPointArray(inputString);

			// Assert
			Assert.AreEqual(0, points.Length);
		}
	}

}
