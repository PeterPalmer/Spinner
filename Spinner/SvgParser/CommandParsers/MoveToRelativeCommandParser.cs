using System.Windows;
using Spinner.Extensions;
using Spinner.Shapes;

namespace Spinner.SvgParser.CommandParsers
{
	public class MoveToRelativeCommandParser : CommandParser
	{
		public override IShape CreateShape(CommandParameters parameters)
		{
			var points = parameters as PointsParameters;

			points[0] = points[0].Add(parameters.StartPoint);
			for (int i = 1; i < points.Length; i++)
			{
				points[i] = points[i].Add(points[i-1]);
			}

			_endPoint = points[points.Length - 1];
			return new MovePenTo(points, points.IsFilled, points.IsStroked);
		}
	}
}
