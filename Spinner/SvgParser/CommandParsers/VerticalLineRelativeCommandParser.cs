using Spinner.Shapes;

namespace Spinner.SvgParser.CommandParsers
{
	class VerticalLineRelativeCommandParser : VerticalLineCommandParser
	{
		public override IShape CreateShape(CommandParameters parameters)
		{
			DoubleParameter doubleParameter = parameters as DoubleParameter;
			doubleParameter.Value += parameters.StartPoint.Y;

			return base.CreateShape(doubleParameter);
		}
	}
}
