﻿using System;
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
		private static void OnClosed(object sender, EventArgs e)
		{
			(sender as Window).Close();
		}

		private static void OnKeyReleased(object sender, KeyEventArgs e)
		{
			IsKeyPressed[e.Code] = false;
		}

		private static void OnKeyPressed(object sender, KeyEventArgs e)
		{
			if (e.Code == Keyboard.Key.Escape)
				GameWindow.Close();

			IsKeyPressed[e.Code] = true;
		}

		private static void OnMouseMoved(object sender, MouseMoveEventArgs e)
		{
			var maxX = GameWindow.Size.X;
			var maxY = GameWindow.Size.Y;
			var currentX = e.X;
			var currentY = e.Y;
			var delta = 10;

			cursorState = SideOfScreen.None; //чтобы экран останавливался после передвижения

			if (currentX > maxX - delta)
				cursorState |= SideOfScreen.Right;
			else if (currentX < delta)
				cursorState |= SideOfScreen.Left;

			if (currentY > maxY - delta)
				cursorState |= SideOfScreen.Bottom;
			else if (currentY < delta)
				cursorState |= SideOfScreen.Top;
		}

		private static void OnMouseButtonReleased(object sender, MouseButtonEventArgs e)
		{

		}

		private static void OnMouseButtonPressed(object sender, MouseButtonEventArgs e)
		{

		}

		private static void OnWheelMoved(object sender, MouseWheelEventArgs e)
		{
			Zoom(-0.2f * e.Delta + 1.0f);
		}

		private static void OnGainedFocus(object sender, EventArgs e)
		{

		}

		private static void OnLostFocus(object sender, EventArgs e)
		{

		}
	}
}
