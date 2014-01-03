using System.Globalization;

namespace Spinner
{
	class Constants
	{
		public const int Perspective = 420;
		public const double SpeedLimit = 50D;
		public static NumberFormatInfo NumberFormatUS = new CultureInfo("en-US").NumberFormat;
	}
}
