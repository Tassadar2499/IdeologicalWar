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
{
	static class Program
	{
		static void Main()
		{
			var window = new RenderWindow(new VideoMode(600, 600), "Ideological War 1948");
			window.Closed += OnClose;

			while(window.IsOpen)
			{
				window.DispatchEvents();
				window.Clear();
				window.Display();
			}
		}

		private static void OnClose(object sender, EventArgs e)
		{
			(sender as Window).Close();
		}
	}
}
