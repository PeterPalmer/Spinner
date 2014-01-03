using System.Collections.Generic;
using System.Windows;
using Spinner.Shapes;

namespace Spinner.SvgParser.CommandParsers
{
	class EllipticalArcParameterList : CommandParameters
	{
		public List<EllipticalArcParameters> ArcParameters = new List<EllipticalArcParameters>();
	}

	class EllipticalArcParameters
	{
		public Point EndPoint { get; set; }
		public Size Size { get; set; }
		public double RotationAngle { get; set; }
		public bool IsLarge { get; set; }
		public bool Clockwise { get; set; }
	}
}
