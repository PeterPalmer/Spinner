using System.Windows;
using Spinner.Shapes;

namespace Spinner.SvgParser.CommandParsers
{
	class ClosePathCommandParser : CommandParser
	{
		public override IShape CreateShape(CommandParameters parameters)
		{
			_endPoint = parameters.PathStartPoint;
			return null;
		}
	}
}
