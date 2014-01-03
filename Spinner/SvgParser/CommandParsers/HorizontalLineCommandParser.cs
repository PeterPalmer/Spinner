using System.Windows;
using Spinner.Shapes;

namespace Spinner.SvgParser.CommandParsers
{
	class HorizontalLineCommandParser : CommandParser
	{
		public override IShape CreateShape(CommandParameters parameters)
		{
			DoubleParameter doubleParameter = parameters as DoubleParameter;
			_endPoint = new Point(doubleParameter.Value, parameters.StartPoint.Y);

			return new LineTo(_endPoint, parameters.IsStroked);
		}

		public override CommandParameters ParseParams(string paramString)
		{
			double xValue = base.ToDouble(paramString.Trim());

			return new DoubleParameter(xValue);
		}
	}
}
