using Spinner.Shapes;

namespace Spinner.SvgParser.CommandParsers
{
	class HorizontalLineRelativeCommandParser : HorizontalLineCommandParser
	{
		public override IShape CreateShape(CommandParameters parameters)
		{
			DoubleParameter doubleParameter = parameters as DoubleParameter;
			doubleParameter.Value += parameters.StartPoint.X;

			return base.CreateShape(doubleParameter);
		}
	}
}
