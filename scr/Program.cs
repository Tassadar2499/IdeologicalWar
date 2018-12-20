using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameEngine
{
	static partial class Program
	{
		private enum SideOfScreen
		{
			None = 0,
			Left = 1,
			Right = 2,
			Top = 4,
			Bottom = 8
		}
		const float speedFactor = 0.0001f;

		private static RenderWindow GameWindow;
		private static GameData CurrentGameData;
		private static Dictionary<Keyboard.Key, bool> IsKeyPressed
			= new Dictionary<Keyboard.Key, bool>()
			{
				[Keyboard.Key.W] = false,
				[Keyboard.Key.A] = false,
				[Keyboard.Key.S] = false,
				[Keyboard.Key.D] = false
			};
		private static SideOfScreen cursorState = SideOfScreen.None;

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

			if (IsKeyPressed[Keyboard.Key.W] || cursorState.HasFlag(SideOfScreen.Top))
				view.Move(new Vector2f(0, -range));
			if (IsKeyPressed[Keyboard.Key.S] || cursorState.HasFlag(SideOfScreen.Bottom))
				view.Move(new Vector2f(0, range));

			if (IsKeyPressed[Keyboard.Key.D] || cursorState.HasFlag(SideOfScreen.Right))
				view.Move(new Vector2f(range, 0));
			if (IsKeyPressed[Keyboard.Key.A] || cursorState.HasFlag(SideOfScreen.Left))
				view.Move(new Vector2f(-range, 0));


			GameWindow.SetView(view);
		}
	}
}
