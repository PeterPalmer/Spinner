using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Spinner.Shapes;

namespace Spinner
{
	public class SpinnerModel
	{
		private List<ShapeCollection> _shapes;

		public List<ShapeCollection> Shapes
		{
			get
			{
				return _shapes;
			}
		}

		public SpinnerModel(List<ShapeCollection> shapes)
		{
			_shapes = shapes;
		}

		public void Draw(Canvas canvas)
		{
			foreach (var shapeCollection in _shapes)
			{
				var geometry = new StreamGeometry();
				using (var ctx = geometry.Open())
				{
					foreach (var shape in shapeCollection)
					{
						shape.Draw(ctx);
					}

					/*ctx.BeginFigure(new System.Windows.Point(100, 100), true, true);
					ctx.LineTo(new System.Windows.Point(200, 100), true, false);
					ctx.LineTo(new System.Windows.Point(200, 200), true, false);
					ctx.LineTo(new System.Windows.Point(100, 200), true, false);
					ctx.LineTo(new System.Windows.Point(100, 100), true, false);*/
				}

				//geometry.FillRule = FillRule.Nonzero;
				geometry.Freeze();

				var path = new Path()
				{
					Fill = shapeCollection.Fill,
					Stroke = shapeCollection.Stroke,
					StrokeThickness = shapeCollection.StrokeWidth,
					Data = geometry
				};


				canvas.Children.Add(path);
			}

		}

		public void Pitch(double pitchSpeed)
		{
			_shapes.ForEach(sc => sc.ForEach(s => s.Pitch(pitchSpeed)));
		}

		public void Yaw(double yawSpeed)
		{
			_shapes.ForEach(sc => sc.ForEach(s => s.Yaw(yawSpeed)));
		}

		public void Move(double x, double y, double z)
		{
			_shapes.ForEach(sc => sc.ForEach(s => s.Move(x, y, z)));
		}

		public void Resize(double factor)
		{
			_shapes.ForEach(sc => sc.ForEach(s => s.Resize(factor)));
			_shapes.ForEach(sc => sc.StrokeWidth *= factor);
		}
	}
}
