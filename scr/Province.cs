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
		private string name;
		private ConvexShape shape;
		private Color color;

		public Province(string name, ConvexShape shape)
		{
			this.name = name;
			this.shape = shape;
			this.color = shape.FillColor;
		}

		public void Draw(RenderTarget target, RenderStates states)
		{
			target.Draw(shape, states);
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
