using System.Windows;
using Spinner.Shapes;

namespace Spinner.SvgParser.CommandParsers
{
	public class MoveToCommandParser : CommandParser
	{
		public override IShape CreateShape(CommandParameters parameters)
		{
			var points = parameters as PointsParameters;
			_endPoint = points[points.Length - 1];
			return new MovePenTo(points, points.IsFilled, points.IsStroked);
		}
	}
}
