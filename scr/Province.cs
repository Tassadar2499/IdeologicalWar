using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

using GameEngine.Tools;

namespace GameEngine
{
	public class Province : Drawable
	{
		private Text name;
		private ConvexShape shape;
		private Color color;

		public Province(string name, ConvexShape shape)
		{
			var shapeCenter =
			this.name = new Text(name, new Font(@"data\times.ttf"), 16);
			this.name.Origin = new Vector2f(this.name.GetLocalBounds().Width / 2f, this.name.GetLocalBounds().Height / 2f);
			this.name.Position = Utils.GetShapeCenter(shape);
			this.name.Color = Color.White;

			this.shape = shape;
			this.color = shape.FillColor;
		}

		public void Draw(RenderTarget target, RenderStates states)
		{
			target.Draw(shape, states);
			target.Draw(name, states);
		}

		public void Update(RenderWindow window, Time dt)
		{
			var pos = window.MapPixelToCoords(Mouse.GetPosition(window));

			if (shape.Contain(pos))
				shape.FillColor = Color.Red;
			else
				shape.FillColor = color;
		}
	}
}
