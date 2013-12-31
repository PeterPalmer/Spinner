using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spinner.Shapes;

namespace Spinner.Tests
{
	[TestClass]
	public class SpinnerModelTests
	{
		[TestMethod]
		public void Resize()
		{
			var shapes = new ShapeCollection();
			shapes.Add(new MovePenTo(20, 30, 0));
			var shapeList = new List<ShapeCollection>() {shapes};
			var model = new SpinnerModel(shapeList);

			model.Resize(3);

			var movePen = model.Shapes[0][0] as MovePenTo;

			Assert.AreEqual(60, movePen.StartPosition.ProjectedX);
		}
	}
}
