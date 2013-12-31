using System.Windows.Media;

namespace Spinner.Shapes
{
	public interface IShape
	{
		void Draw(StreamGeometryContext ctx);
		void Pitch(double angle);
		void Yaw(double angle);
		void Move(int x, int y);
		void Resize(double scaleFactor);
	}
}
