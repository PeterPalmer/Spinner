using System;
using System.Globalization;
using System.Windows;
using Spinner.Shapes;

namespace Spinner.SvgParser.CommandParsers
{
	public abstract class CommandParser
	{
		protected Point _endPoint;

		public abstract IShape CreateShape(CommandParameters parameters);

		public virtual Point GetEndPoint()
		{
			return _endPoint;
		}

		public virtual Point GetControlPoint()
		{
			return _endPoint;
		}

		public virtual CommandParameters ParseParams(string paramString)
		{
			Point[] points = this.ToPointArray(paramString);

			return new PointsParameters(points);
		}

		internal double ToDouble(string value)
		{
			double result;

			if (!Double.TryParse(value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowExponent, Constants.NumberFormatUS, out result))
			{
				throw new InvalidCastException(value + " is not a valid double!");
			}

			return result;
		}

		internal double[] ToDoubleArray(string value)
		{
			string[] doubleStrings = value.Replace("-", ",-").Replace("e,-", "e-").Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
			double[] result = new double[doubleStrings.Length];

			for (int i = 0; i < doubleStrings.Length; i++)
			{
				result[i] = ToDouble(doubleStrings[i].Trim());
			}

			return result;
		}

		internal Point[] ToPointArray(string inputString)
		{
			double[] doubles = this.ToDoubleArray(inputString);

			if (doubles.Length % 2 == 1)
			{
				throw new ArgumentException("Input string must contain even number of doubles.");
			}

			Point[] points = new Point[doubles.Length / 2];

			for (int i = 0; i < points.Length; i++)
			{
				points[i].X = doubles[i * 2];
				points[i].Y = doubles[i * 2 + 1];
			}

			return points;
		}

		internal bool ToBool(string inputString)
		{
			if (String.Compare(inputString, "1") == 0)
			{
				return true;
			}

			if (String.Compare(inputString, "0") == 0)
			{
				return false;
			}

			throw new ArgumentException(inputString + " is not a valid bool.");
		}
	}
}
