using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

using GameEngine.Gui;

namespace GameEngine
{
	static partial class Program
	{
		private static RenderWindow GameWindow;
		private static GameData CurrentGameData;

		private static List<Panel> Panels;

		private static void DrawGame(RenderWindow window, Time dt)
		{
			foreach (var province in CurrentGameData.Provinces)
				window.Draw(province);
		}

		private static void UpdateGame(RenderWindow window, Time dt)
		{
			foreach (var province in CurrentGameData.Provinces)
				province.Update(window, dt);
		}

		private static void Main()
		{
			GameWindow = CreateRenderWindow(1366, 768, "Ideological War 1948", new ContextSettings());

			var WindowMoving = new WindowMoving(GameWindow);
			Panels = new List<Panel>();
			CurrentGameData = new GameData()
			{
				Provinces = MapLoader.LoadProvinces(@"data\country1.svg")
			};

			var clock = new Clock();
			while (GameWindow.IsOpen)
			{
				var dt = clock.Restart();

				GameWindow.DispatchEvents();

				UpdateGame(GameWindow, dt);
				Panels.RemoveAll(p => p.ToDelete);
				WindowMoving.Update(dt);

				GameWindow.Clear();

				DrawGame(GameWindow, dt);
				foreach (var panel in Panels)
					GameWindow.Draw(panel);
				GameWindow.Display();
			}
		}

		private static RenderWindow CreateRenderWindow(uint x, uint y, string logo, ContextSettings settings)
		{
			var window = new RenderWindow(new VideoMode(x, y), logo, Styles.Fullscreen, settings);

			window.SetFramerateLimit(120);
			window.SetKeyRepeatEnabled(false);

			window.Closed += OnClosed;
			window.KeyPressed += OnKeyPressed;
			window.KeyReleased += OnKeyReleased;
			window.MouseButtonPressed += OnMouseButtonPressed;
			window.MouseButtonReleased += OnMouseButtonReleased;
			window.MouseMoved += OnMouseMoved;
			window.MouseWheelMoved += OnWheelMoved;
			window.LostFocus += OnLostFocus;
			window.GainedFocus += OnGainedFocus;

			return window;
		}
	}
}
