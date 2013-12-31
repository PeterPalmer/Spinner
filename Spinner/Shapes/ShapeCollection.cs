using System.Collections.Generic;
using System.Windows.Media;

namespace Spinner.Shapes
{
	public class ShapeCollection : List<IShape>
	{
		public Brush Stroke { get; set; }
		public Brush Fill { get; set; }
	}
}
