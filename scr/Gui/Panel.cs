using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using GameEngine.Tools;

namespace GameEngine.Gui
{
	public partial class Panel : Drawable
	{
		private const byte mainPanelAlpha = 210;
		private const byte dragPanelAlpha = 230;
		private const uint dragPanelHeight = 20;
		private const float outlineThickness = 2.5f;

		private RectangleShape mainPanel;
		private RectangleShape dragPanel;
		private LineShape exitLine;
		private LineShape[] exitMark;
		private Text logo;

		private RectangleShape exitCheckBox;
		private bool mouseClikedInExitCheckBox = false;
		private bool mouseClikedInDragPanel = false;
		private Vector2f lastMousePosition;

		public bool ToDelete { get; private set; } = false;

		public string LogoString
			=> logo.DisplayedString;

		public Vector2f Size
		{
			get => new Vector2f(mainPanel.Size.X, mainPanel.Size.Y + dragPanel.Size.Y);
			set => ChangeSize(value);
		}

		public Vector2f Position
		{
			get => dragPanel.Position;
			set => Move(value);
		}

		private void ChangeSize(Vector2f newSize)
		{
			var horizontalOffset = new Vector2f(newSize.X - Size.X, 0);
			dragPanel.Size = new Vector2f(newSize.X, dragPanel.Size.Y);
			mainPanel.Size = new Vector2f(newSize.X, newSize.Y - dragPanelHeight);
			exitLine.Position += horizontalOffset;
			exitMark[0].Position += horizontalOffset;
			exitMark[1].Position += horizontalOffset;
			exitCheckBox.Position += horizontalOffset;
		}

		private void Move(Vector2f vector)
		{
			var offset = vector - Position;
			mainPanel.Position += offset;
			dragPanel.Position += offset;
			exitLine.Position += offset;
			exitMark[0].Position += offset;
			exitMark[1].Position += offset;
			exitCheckBox.Position += offset;
			logo.Position += offset;
		}

		public Panel(string logoText, Window window, FloatRect panelRect, Color fillColor)
		{
			window.MouseButtonPressed += OnMousePressed;
			window.MouseButtonReleased += OnMouseReleased;
			window.MouseMoved += OnMouseMoved;

			var windowSize = window.Size;

			mainPanel = CreateMainPanel(panelRect, fillColor, windowSize);
			dragPanel = CreateDragPanel(mainPanel, panelRect, fillColor, windowSize);
			exitLine = CreateExitLine(dragPanel, fillColor);
			exitMark = CreateExitMark(dragPanel, Utils.MultiplyColor(fillColor, 1.5f), outlineThickness);

			logo = new Text(logoText, new Font(@"data\times.ttf"), dragPanelHeight - 8)
			{
				Position = Position + new Vector2f(outlineThickness, outlineThickness),
				Color = Color.White
			};

			exitCheckBox = new RectangleShape()
			{
				Position =
					new Vector2f(dragPanel.Position.X + dragPanel.Size.X - dragPanelHeight, dragPanel.Position.Y),
				Size =
					new Vector2f(dragPanelHeight, dragPanelHeight)
			};
		}

		public void Draw(RenderTarget target, RenderStates states)
		{
			if (ToDelete)
				throw new Exception("call Draw but ToDelete == true");

			var view = new View(target.GetView());
			target.SetView(target.DefaultView);

			target.Draw(mainPanel, states);
			target.Draw(dragPanel, states);
			target.Draw(exitLine, states);
			foreach (var line in exitMark)
				target.Draw(line, states);
			target.Draw(logo, states);

			target.SetView(view);
		}

		private void OnMousePressed(object sender, MouseButtonEventArgs e)
		{
			if (e.Button != Mouse.Button.Left)
				return;

			var position = Utils.GetPixselMousePosition(sender, e.X, e.Y, true);

			if (exitCheckBox.GetGlobalBounds().Contains(position.X, position.Y))
				mouseClikedInExitCheckBox = true;

			else if (dragPanel.GetGlobalBounds().Contains(position.X, position.Y))
			{
				mouseClikedInDragPanel = true;
				lastMousePosition = position;
			}

		}

		private void OnMouseMoved(object sender, MouseMoveEventArgs e)
		{
			if (mouseClikedInDragPanel)
			{
				var position = Utils.GetPixselMousePosition(sender, e.X, e.Y, true);
				var offset = position - lastMousePosition;
				this.Position += offset;
				lastMousePosition = position;
			}
		}

		private void OnMouseReleased(object sender, MouseButtonEventArgs e)
		{
			if (e.Button != Mouse.Button.Left)
				return;

			mouseClikedInDragPanel = false;

			if (mouseClikedInExitCheckBox)
			{
				mouseClikedInExitCheckBox = false;

				var position = Utils.GetPixselMousePosition(sender, e.X, e.Y, true);
				if (exitCheckBox.GetGlobalBounds().Contains(position.X, position.Y))
					ToDelete = true;
			}
		}
	}
}
