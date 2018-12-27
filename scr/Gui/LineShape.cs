using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameEngine.Gui
{
	internal class LineShape : Drawable
	{
		private Vertex[] vertices;
		private Vector2f position;

		public Vector2f Position
		{
			get => position;
			set
			{
				var offset = value - position;
				position = value;
				for (var i = 0; i < vertices.Length; i++)
					vertices[i].Position += offset;

			}
		}

		public LineShape(Vector2f start, Vector2f end, Color color, float thickness)
		{
			vertices = new Vertex[4];
			position = start;

			var direction = end - start;
			var unitDirection = direction / (float)Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);
			var unitPerpendicular = new Vector2f(-unitDirection.Y, unitDirection.X);
			var offset = (thickness / 2f) * unitPerpendicular;

			vertices[0].Position = start + offset;
			vertices[1].Position = end + offset;
			vertices[2].Position = end - offset;
			vertices[3].Position = start - offset;

			for (var i = 0; i < vertices.Length; i++)
				vertices[i].Color = color;
		}

		public void Draw(RenderTarget target, RenderStates states)
		{
			target.Draw(vertices, 0, 4, PrimitiveType.Quads);
		}
	}
}