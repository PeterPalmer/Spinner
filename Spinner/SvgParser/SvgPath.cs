using System;
using System.Globalization;
using System.Text;
using System.Windows.Media;
using Spinner.Shapes;

namespace Spinner.SvgParser
{
	public class SvgPath
	{
		private readonly string _data;
		private readonly string _fill;
		private readonly string _stroke;
		private readonly string _strokeWidth;

		public string Data
		{
			get { return _data; }
		}

		public string Fill
		{
			get { return _fill; }
		}
		public string Stroke
		{
			get { return _stroke; }
		}

		public SvgPath(string data, string fill, string stroke, string strokeWidth)
		{
			_data = data;
			_fill = fill;
			_stroke = stroke;
			_strokeWidth = strokeWidth;
		}

		public Brush GetFillBrush()
		{
			return StringToBrush(_fill);
		}

		public Brush GetStrokeBrush()
		{
			return StringToBrush(_stroke);
		}

		public double GetStrokeWidth()
		{
			if(String.IsNullOrWhiteSpace(_strokeWidth))
			{
				return 1D;
			}

			return double.Parse(_strokeWidth, NumberStyles.AllowDecimalPoint, Constants.NumberFormatUS);
		}

		private Brush StringToBrush(string colorValue)
		{
			if (String.IsNullOrEmpty(colorValue) || String.Compare(colorValue, "none", true) == 0)
			{
				return null;
			}

			return new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorValue));
		}

		public override string ToString()
		{
			var sb = new StringBuilder("<Path ");

			if(!String.IsNullOrEmpty(_fill) && String.Compare(_fill, "none", true) != 0)
			{
				sb.AppendFormat("Fill='{0}' ", _fill);
			}

			if (!String.IsNullOrEmpty(_stroke) && String.Compare(_stroke, "none", true) != 0)
			{
				sb.AppendFormat("Stroke='{0}' ", _stroke);
			}

			sb.AppendFormat("Data='{0}' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' />", _data);

			return sb.ToString();
		}

	}
}
