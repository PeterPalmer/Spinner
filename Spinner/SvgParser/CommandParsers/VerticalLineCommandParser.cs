using System.Windows;
using Spinner.Shapes;

namespace Spinner.SvgParser.CommandParsers
{
	class VerticalLineCommandParser : CommandParser
	{
		public override IShape CreateShape(CommandParameters parameters)
		{
			DoubleParameter doubleParameter = parameters as DoubleParameter;
			_endPoint = new Point(parameters.StartPoint.X, doubleParameter.Value);

			return new LineTo(_endPoint, parameters.IsStroked);
		}

		public override CommandParameters ParseParams(string paramString)
		{
			double yValue = base.ToDouble(paramString);

			return new DoubleParameter(yValue);
		}
	}
}
