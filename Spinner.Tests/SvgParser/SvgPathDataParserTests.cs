using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spinner.SvgParser;
using System.Windows;

namespace Spinner.Tests
{
	[TestClass]
	public class SvgPathDataParserTests
	{
		[TestMethod]
		public void FindEges()
		{
			var parser = new SvgPathDataParser();
			parser.AddShapes(new SvgPath("M-100,50C8,11,-100,7,15,-75z", "", "", "1"));
			parser.AddShapes(new SvgPath("M150,-75c150,30,40,40,30,50z", "", "", "1"));
			double minX, maxX, minY, maxY;

			parser.GetEdges(out minX, out maxX, out minY, out maxY);

			Assert.AreEqual(-100, minX);
			Assert.AreEqual(300, maxX);
			Assert.AreEqual(-75, minY);
			Assert.AreEqual(50, maxY);
		}

	}

}
