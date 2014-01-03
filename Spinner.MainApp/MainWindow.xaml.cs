using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace Spinner.MainApp
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			this.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
			this.Arrange(new Rect(0, 0, this.Width, this.Height));
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			canvas.LoadSvgFile(Path.Combine(Directory.GetCurrentDirectory(), @"SVG-files\SvgSpinner.svg"));
			canvas.Initialize();
			canvas.Resize(new Size(500,400));
		}

		private void OpenFileClick(object sender, RoutedEventArgs e)
		{
			var dlg = new OpenFileDialog();
			dlg.InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), "SVG-Files");

			bool? result = dlg.ShowDialog();

			if (!result.HasValue || result.Value == false)
			{
				return;
			}

			canvas.LoadSvgFile(dlg.FileName);
		}

		private IDisposable _timer = null;
		private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (!canvas.IsInitalized)
			{
				return;
			}

			if (_timer != null)
			{
				_timer.Dispose();
			}

			_timer = EasyTimer.SetTimeout(() =>
			{
				canvas.Resize(e.NewSize);
			}, 500);
		}
	}
}
