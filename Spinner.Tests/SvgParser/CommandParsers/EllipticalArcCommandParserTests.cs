using System;
using System.Linq;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spinner.Shapes;
using Spinner.SvgParser.CommandParsers;

namespace Spinner.Tests.SvgParser.CommandParsers
{
	[TestClass]
	public class EllipticalArcCommandParserTests
	{
		[TestMethod]
		public void EllipticalArcCommandParser_ParseParams()
		{
			var parser = new EllipticalArcCommandParser();

			var parameters = parser.ParseParams("150,150 3 1,0 150,-150");
			var arcParams = parameters as EllipticalArcParameterList;
			arcParams.StartPoint = new Point(2, 3);

			var param = arcParams.ArcParameters.First();
			Assert.AreEqual(150, param.Size.Width);
			Assert.AreEqual(150, param.Size.Height);
			Assert.AreEqual(3, param.RotationAngle);
			Assert.AreEqual(true, param.IsLarge);
			Assert.AreEqual(false, param.Clockwise);
			Assert.AreEqual(150, param.EndPoint.X);
			Assert.AreEqual(-150, param.EndPoint.Y);
		}

		[TestMethod]
		public void EllipticalArcCommandParser_CreateShape()
		{
			var paramsList = new EllipticalArcParameterList();
			paramsList.StartPoint = new Point(2, 3);
			var arcParam = new EllipticalArcParameters();
			arcParam.Size = new Size(150, 150);
			arcParam.RotationAngle = 3;
			arcParam.EndPoint = new Point(150, -150);
			paramsList.ArcParameters.Add(arcParam);
			var parser = new EllipticalArcCommandParser();

			IShape shape = parser.CreateShape(paramsList);
			var arc = shape as Arc;

			Assert.AreEqual(150, arc.EndPoint.X);
			Assert.AreEqual(-150, arc.EndPoint.Y);
		}

		[TestMethod]
		public void EllipticalArcRelativeCommandParser_CreateShape()
		{
			var paramsList = new EllipticalArcParameterList();
			paramsList.StartPoint = new Point(2, 3);
			var arcParams = new EllipticalArcParameters();
			arcParams.Size = new Size(150, 150);
			arcParams.RotationAngle = 3;
			arcParams.EndPoint = new Point(150, -150);
			paramsList.ArcParameters.Add(arcParams);
			var parser = new EllipticalArcRelativeCommandParser();

			IShape shape = parser.CreateShape(paramsList);
			var arc = shape as Arc;

			Assert.AreEqual(152, arc.EndPoint.X);
			Assert.AreEqual(-147, arc.EndPoint.Y);
		}
	}
}
