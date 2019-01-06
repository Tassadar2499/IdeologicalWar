using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameEngine
{
	class WindowMoving : IUpdate
	{
		private RenderWindow GameWindow;
		public float Speed { get; set; } = 320f;
		private float currentZoom = 1f;
		private float maxZoom = 1.4f;
		private float minZoom = 0.2f;
		private float zoomStep = 0.1f;

		private enum SideOfScreen
		{
			None = 0,
			Left = 1,
			Right = 2,
			Top = 4,
			Bottom = 8
		}

		private static Dictionary<Keyboard.Key, bool> IsKeyPressed
			= new Dictionary<Keyboard.Key, bool>()
			{
				[Keyboard.Key.W] = false,
				[Keyboard.Key.A] = false,
				[Keyboard.Key.S] = false,
				[Keyboard.Key.D] = false
			};

		private static SideOfScreen cursorState = SideOfScreen.None;

		public WindowMoving(RenderWindow window)
		{
			GameWindow = window;
			GameWindow.MouseMoved += OnMouseMoved;
			GameWindow.KeyReleased += OnKeyReleased;
			GameWindow.KeyPressed += OnKeyPressed;
			GameWindow.MouseWheelMoved += OnWheelMoved;
		}

		private void OnKeyReleased(object sender, KeyEventArgs e)
		{
			IsKeyPressed[e.Code] = false;
		}

		private void OnKeyPressed(object sender, KeyEventArgs e)
		{
			if (e.Code == Keyboard.Key.Escape)
				GameWindow.Close();

			IsKeyPressed[e.Code] = true;
		}

		private void OnMouseMoved(object sender, MouseMoveEventArgs e)
		{
			var maxX = GameWindow.Size.X;
			var maxY = GameWindow.Size.Y;
			var currentX = e.X;
			var currentY = e.Y;
			var delta = 10;

			cursorState = SideOfScreen.None;

			if (currentX > maxX - delta)
				cursorState |= SideOfScreen.Right;
			else if (currentX < delta)
				cursorState |= SideOfScreen.Left;

			if (currentY > maxY - delta)
				cursorState |= SideOfScreen.Bottom;
			else if (currentY < delta)
				cursorState |= SideOfScreen.Top;
		}

		private void OnWheelMoved(object sender, MouseWheelEventArgs e)
		{
			var view = GameWindow.GetView();
			var defaultSize = GameWindow.DefaultView.Size;

			if ((e.Delta > 0 && currentZoom <= maxZoom) || (e.Delta < 0 && currentZoom >= minZoom))
			{
				currentZoom += e.Delta * zoomStep;
				view.Size = defaultSize / currentZoom;
			}

			GameWindow.SetView(view);
		}

		public void Update(Time dt)
		{
			var view = GameWindow.GetView();
			var range = Speed * dt.AsSeconds();

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
