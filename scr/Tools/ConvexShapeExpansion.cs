using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using System;
using System.Linq;

namespace GameEngine.Tools
{
	public static class ConvexShapeExpansion
	{
		private static bool IsBetween(Vector2f p1, Vector2f p2, Vector2f point)
		{
			return (p1.Y >= point.Y && p2.Y <= point.Y) || (p2.Y >= point.Y && p1.Y <= point.Y);
		}

		public static float RangeBetweenPoint(Vector2f a, Vector2f b)
		{
			return (float)Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
		}

		public static bool Contain(this ConvexShape convexShape, Vector2f point)
		{
			var shapePointCount = convexShape.GetPointCount();
			var count = 0;

			for (uint i = 0, j = shapePointCount - 1; i < shapePointCount; j = i++)
			{
				var current = convexShape.GetPoint(i);
				var last = convexShape.GetPoint(j);

				if (IsBetween(current, last, point) &&
					(point.X > (last.X - current.X) * (point.Y - current.Y) / (last.Y - current.Y) + current.X))
					count++;
			}

			return count % 2 == 1;
		}

		public static IEnumerable<Vector2f> GetPoints(this ConvexShape shape)
		{
			for (uint i = 0; i < shape.GetPointCount(); i++)
				yield return shape.GetPoint(i);
		}
	}
}
