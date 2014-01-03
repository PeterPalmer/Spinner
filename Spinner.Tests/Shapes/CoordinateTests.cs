using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spinner.Shapes;

namespace Spinner.Tests.Shapes
{
	[TestClass]
	public class CoordinateTests
	{
		[TestMethod]
		public void Coordinate_Move()
		{
			Coordinate coord = new Coordinate(0D, 0D, 0D);
			Coordinate.SetOffsets(0, 0);

			coord.Move(50D, 75D, 0D);

			Assert.AreEqual(50D, coord.ProjectedX);
			Assert.AreEqual(75D, coord.ProjectedY);
		}

		[TestMethod]
		public void Coordinate_Resize()
		{
			Coordinate coord = new Coordinate(10D, 20D, 0D);
			Coordinate.SetOffsets(0, 0);

			coord.Resize(3);

			Assert.AreEqual(30D, coord.ProjectedX);
			Assert.AreEqual(60D, coord.ProjectedY);
		}
	}
}
