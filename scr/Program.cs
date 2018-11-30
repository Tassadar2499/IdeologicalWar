using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameEngine
{//
	static partial class Program
	{
		private static void DrawGame(Window window, float dt)
		{

		}

		private static void UpdateGame(Window window, float dt)
		{

		}

		private static void Main()
		{
			var window = CreateRenderWindow(600, 600, "Ideological War 1948", new ContextSettings());

			var clock = new Clock();
			while (window.IsOpen)
			{
				var dt = clock.ElapsedTime.AsMicroseconds() / 0.000001f;

				window.DispatchEvents();
				UpdateGame(window, dt);
				window.Clear();
				DrawGame(window, dt);
				window.Display();
			}
		}

		private static RenderWindow CreateRenderWindow(uint x, uint y, string logo, ContextSettings settings)
		{
			var window = new RenderWindow(new VideoMode(x, y), logo, Styles.Default, settings);

			window.SetFramerateLimit(120);
			window.SetKeyRepeatEnabled(false);

			window.Closed += OnClosed;
			window.KeyPressed += OnKeyPressed;
			window.KeyReleased += OnKeyReleased;
			window.MouseButtonPressed += OnMouseButtonPressed;
			window.MouseButtonReleased += OnMouseButtonReleased;
			window.MouseMoved += OnMouseMoved;
			window.LostFocus += OnLostFocus;
			window.GainedFocus += OnGainedFocus;

			return window;
		}
	}
}
