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
				}

				var path = new Path()
				{
					Fill = shapeCollection.Fill,
					Stroke = shapeCollection.Stroke,
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

		public void Move(int x, int y)
		{
			_shapes.ForEach(sc => sc.ForEach(s => s.Move(x,y)));
		}

		public void Resize(double factor)
		{
			_shapes.ForEach(sc => sc.ForEach(s => s.Resize(factor)));
		}
	}
}
