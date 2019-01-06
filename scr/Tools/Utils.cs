using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Tools
{
	public static class Utils
	{
		public static Color MultiplyColor(Color target, float factor)
		{
			return new Color((byte)(target.R * factor), (byte)(target.G * factor), (byte)(target.B * factor));
		}

		public static Vector2f GetPixselMousePosition(object sender, int x, int y, bool defaultView = false)
		{
			var window = sender as RenderWindow;
			var vector = new Vector2i(x, y);

			if (defaultView)
				return window.MapPixelToCoords(vector, window.DefaultView);
			else
				return window.MapPixelToCoords(vector);
		}

		public static bool VectorCircleIntersect(Vector2f S, Vector2f E, Vector2f C, float Radius)
		{
			var g = new Vector2f(S.X - C.X, S.Y - C.Y);
			var f = new Vector2f(E.X - C.X, E.Y - C.Y);
			var d = new Vector2f(f.X - g.X, f.Y - g.Y);

			float a = d.X * d.X + d.Y * d.Y;
			float b = 2.0f * (g.X * d.X + g.Y * d.Y);
			float c = g.X * g.X + g.Y * g.Y - Radius * Radius;

			if (-b < 0) return (c < 0);
			if (-b < (2.0f * a)) return (4.0f * a * c - b * b < 0);
			return (a + b + c < 0);
		}

		public static float GetLenght(this Vector2f v)
		{
			return (float)Math.Sqrt(v.X * v.X + v.Y * v.Y);
		}

		public static Vector2f GetUnitVector(this Vector2f vector)
		{
			if (vector.Equals(new Vector2f()))
				return vector;

			return vector / vector.GetLenght();
		}

		public static Vector2f GetUnitVectorToTarget(this Vector2f start, Vector2f end)
		{
			if (start.Equals(end))
				return new Vector2f(0, 0);

			var vector = end - start;
			var lenght = GetLenght(vector);

			return vector / lenght;
		}

		public static Vector2f GetShapeCenter(Shape shape)
		{
			var pos = new Vector2f();

			var count = shape.GetPointCount();
			for (uint i = 0; i < count; i++)
				pos += shape.GetPoint(i);

			return pos / count;
		}
	}
}
