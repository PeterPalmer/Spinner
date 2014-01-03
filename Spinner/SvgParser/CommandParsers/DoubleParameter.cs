
namespace Spinner.SvgParser.CommandParsers
{
	class DoubleParameter : CommandParameters
	{
		private double _value;

		public double Value
		{
			get
			{
				return _value; 
			}
			set
			{
				_value = value;
			}
		}

		public DoubleParameter(double value)
		{
			_value = value;
		}
	}
}
