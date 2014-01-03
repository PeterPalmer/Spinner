using System;
using System.Windows;
using Spinner.Extensions;
using Spinner.Shapes;

namespace Spinner.SvgParser.CommandParsers
{
	class EllipticalArcRelativeCommandParser : EllipticalArcCommandParser
	{
		public override IShape CreateShape(CommandParameters parameters)
		{
			var arcParameters = parameters as EllipticalArcParameterList;

			Point currentpoint = arcParameters.StartPoint;
			foreach (var arcParam in arcParameters.ArcParameters)
			{
				arcParam.EndPoint = arcParam.EndPoint.Add(currentpoint);
				currentpoint = arcParam.EndPoint;
			}

			return base.CreateShape(arcParameters);
		}
	}
}
