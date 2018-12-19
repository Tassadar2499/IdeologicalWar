using SFML.Graphics;
using System;

namespace GameEngine.Tools
{
	public static class GameRandom
	{
		public static Random Randomizer { get; set; } = new Random(1488);

		public static Color GetRandomColor()
		{
			var rgb = new byte[3];
			Randomizer.NextBytes(rgb);
			return new Color(rgb[0], rgb[1], rgb[2]);
		}
	}
}
