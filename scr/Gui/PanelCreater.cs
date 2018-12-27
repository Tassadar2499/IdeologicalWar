using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Gui
{
	public partial class Panel
	{
		private static RectangleShape CreateDragPanel(RectangleShape mainPanel, FloatRect rect, Color fillColor, Vector2u windowSize)
		{
			return new RectangleShape()
			{
				Position = new Vector2f(mainPanel.Position.X, windowSize.Y * rect.Top),
				Size = new Vector2f(mainPanel.Size.X, dragPanelHeight - outlineThickness),
				FillColor = new Color(fillColor) { A = dragPanelAlpha },
				OutlineColor = fillColor,
				OutlineThickness = outlineThickness
			};
		}

		private static LineShape[] CreateExitMark(RectangleShape dragPanel, Color color, float thickness)
		{
			var position = new Vector2f(
				dragPanel.Position.X + dragPanel.Size.X - dragPanelHeight + outlineThickness * 2,
				dragPanel.Position.Y + outlineThickness
			);

			var size =
				new Vector2f(dragPanelHeight - outlineThickness * 3, dragPanelHeight - outlineThickness * 3);

			var exitMark = new LineShape[2];
			exitMark[0] = new LineShape(position, position + size, color, thickness);
			exitMark[1] = new LineShape(
				new Vector2f(position.X + size.X, position.Y),
				new Vector2f(position.X, position.Y + size.Y),
				color, thickness
			);

			return exitMark;
		}

		private static LineShape CreateExitLine(RectangleShape dragPanel, Color fillColor)
		{
			return new LineShape(
				new Vector2f(dragPanel.Position.X + dragPanel.Size.X - dragPanelHeight, dragPanel.Position.Y),
				new Vector2f(dragPanel.Position.X + dragPanel.Size.X - dragPanelHeight, dragPanel.Position.Y + dragPanel.Size.Y),
				fillColor,
				outlineThickness
			);
		}

		private RectangleShape CreateMainPanel(FloatRect rect, Color fillColor, Vector2u windowSize)
		{
			return new RectangleShape()
			{
				Position = new Vector2f(windowSize.X * rect.Left, windowSize.Y * rect.Top + dragPanelHeight),
				Size = new Vector2f(windowSize.X * (rect.Width - rect.Left), windowSize.Y * (rect.Height - rect.Top) - dragPanelHeight),
				FillColor = new Color(fillColor) { A = mainPanelAlpha },
				OutlineColor = fillColor,
				OutlineThickness = outlineThickness
			};
		}
	}
}
