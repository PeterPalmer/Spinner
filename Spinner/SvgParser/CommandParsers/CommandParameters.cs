using System.Windows;

namespace Spinner.SvgParser.CommandParsers
{
	public class CommandParameters
	{
		public bool IsFilled { get; set; }
		public bool IsStroked { get; set; }
		public Point StartPoint { get; set; }
		public Point PathStartPoint { get; set; }
		public Point ControlPoint { get; set; }
	}
}
