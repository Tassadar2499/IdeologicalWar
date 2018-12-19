using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Text.RegularExpressions;

namespace GameEngine
{
	static partial class Program
	{
		private static RenderWindow GameWindow;
		private static GameData CurrentGameData;

		private static bool IsUpPressed = false;
		private static bool IsLeftPressed = false;
		private static bool IsRightPressed = false;
		private static bool IsDownPressed = false;

		const float speedFactor = 0.0001f;

		private static void DrawGame(RenderWindow window, Time dt)
		{
			foreach (var province in CurrentGameData.Provinces)
				window.Draw(province);
		}

		private static void UpdateGame(RenderWindow window, Time dt)
		{
			foreach (var province in CurrentGameData.Provinces)
				province.Update(window, dt);
			UpdateMovement(dt);
		}

		private static void Main()
		{
			GameWindow = CreateRenderWindow(600, 600, "Ideological War 1948", new ContextSettings());

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
				GameWindow.Clear();
				DrawGame(GameWindow, dt);
				GameWindow.Display();
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
			window.MouseWheelMoved += OnWheelMoved;
			window.LostFocus += OnLostFocus;
			window.GainedFocus += OnGainedFocus;

			return window;
		}

		private static void Zoom(float factor)
		{
			var view = GameWindow.GetView();
			view.Zoom(factor);
			GameWindow.SetView(view);
		}

		private static void UpdateMovement(Time dt)
		{
			var view = GameWindow.GetView();
			var range = 200f * dt.AsSeconds();

			if (IsUpPressed)
				view.Move(new Vector2f(0, -range));
			if (IsDownPressed)
				view.Move(new Vector2f(0, range));

			if (IsRightPressed)
				view.Move(new Vector2f(range, 0));
			if (IsLeftPressed)
				view.Move(new Vector2f(-range, 0));

			GameWindow.SetView(view);
		}
	}
}
