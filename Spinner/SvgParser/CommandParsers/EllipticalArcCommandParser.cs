using System;
using System.Linq;
using System.Windows;
using Spinner.Shapes;

namespace Spinner.SvgParser.CommandParsers
{
	class EllipticalArcCommandParser : CommandParser
	{
		public override IShape CreateShape(CommandParameters parameters)
		{
			var parameterGroup = parameters as EllipticalArcParameterList;

			var firstParameters = parameterGroup.ArcParameters.First();
			var arc = new Arc(firstParameters.EndPoint, firstParameters.Size, firstParameters.RotationAngle, firstParameters.IsLarge, firstParameters.Clockwise, parameterGroup.IsStroked);

			foreach (var arcParameters in parameterGroup.ArcParameters.Skip(1))
			{
				arc.AddArc(arcParameters.EndPoint, arcParameters.Size, arcParameters.RotationAngle, arcParameters.IsLarge, arcParameters.Clockwise, parameterGroup.IsStroked);
			}

			_endPoint = parameterGroup.ArcParameters.Last().EndPoint;
			return arc;
		}

		public override CommandParameters ParseParams(string paramString)
		{
			var splitParams = paramString.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

			if (splitParams.Length % 7 != 0)
			{
				throw new ArgumentException("Invalid number of parameters for Elliptical Arc.");
			}

			var paramsGroup = new EllipticalArcParameterList();
			for (int i = 0; i < splitParams.Length; i += 7)
			{
				var arcParams = new EllipticalArcParameters();
				arcParams.Size = new Size(base.ToDouble(splitParams[i]), base.ToDouble(splitParams[i + 1]));
				arcParams.RotationAngle = base.ToDouble(splitParams[i + 2]);
				arcParams.IsLarge = base.ToBool(splitParams[i + 3]);
				arcParams.Clockwise = base.ToBool(splitParams[i + 4]);
				arcParams.EndPoint = new Point(base.ToDouble(splitParams[i + 5]), base.ToDouble(splitParams[i + 6]));

				paramsGroup.ArcParameters.Add(arcParams);
			}

			return paramsGroup;
		}
	}
}
