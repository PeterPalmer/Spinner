using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Spinner.Shapes;
using Spinner.SvgParser;

namespace Spinner
{
	public class SpinCanvas : Canvas
	{
		private SpinnerModel _model = null;
		private Size _previousSize = new Size(500, 400);
		private double _pitchSpeed = 0.00D;
		private double _yawSpeed = 0.00D;

		public bool IsInitalized
		{
			get
			{
				return _model != null;
			}
		}

		static SpinCanvas()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(SpinCanvas), new FrameworkPropertyMetadata(typeof(SpinCanvas)));
		}

		public void Initialize()
		{
			CompositionTarget.Rendering += Tick;
			this.MouseMove += MouseMoved;
		}

		private Point previousPos;
		void MouseMoved(object sender, MouseEventArgs e)
		{
			var position = e.GetPosition(this);

			double deltaX = position.X - previousPos.X;
			double deltaY = previousPos.Y - position.Y;

			if (Math.Abs(deltaX) < 5 && Math.Abs(deltaY) < 5)
			{
				return;
			}

			deltaX = Math.Min(deltaX, 40D);
			deltaX = Math.Max(deltaX, -40D);
			deltaY = Math.Min(deltaY, 40D);
			deltaY = Math.Max(deltaY, -40D);

			_yawSpeed = 0.003D * deltaX;
			_pitchSpeed = 0.003D * deltaY;

			previousPos = position;
		}

		public void LoadSvgFile(string path)
		{
			var parser = new SvgFileParser();
			_model = parser.ParseFile(path);

			double scaleFactor = parser.CalculateScaleFactor(_previousSize.Width, _previousSize.Height);
			_model.Resize(scaleFactor);
		}

		public void Resize(Size newSize)
		{
			Coordinate.SetOffsets(this.ActualWidth / 2, this.ActualHeight / 2);

			double newSizeMin = Math.Min(newSize.Width, newSize.Height);
			double prevSizeMin = Math.Min(_previousSize.Width, _previousSize.Height);

			_model.Resize(newSizeMin / prevSizeMin);

			_previousSize = newSize;
		}

		private void Tick(object sender, EventArgs e)
		{
			if (!this.IsInitalized)
			{
				return;
			}

			this.Children.Clear();
			_model.Pitch(_pitchSpeed);
			_model.Yaw(_yawSpeed);
			_model.Draw(this);
		}
	}
}
