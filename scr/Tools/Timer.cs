using SFML.System;
using System;

namespace GameEngine.Tools
{
	class Timer : IUpdate
	{
		public Time Time { get; private set; } = Time.Zero;
		public Time Interval { get; set; }

		public event Action Tick;

		public bool Ticked { get; private set; }

		public Timer(Time interval)
		{
			if (interval.AsMilliseconds() <= 0)
				throw new ArgumentException("Interval must be > 0");

			Interval = interval;
		}

		public void Update(Time dt)
		{
			Time += dt;

			Ticked = Time >= Interval;

			while (Time >= Interval)
			{
				Time -= Interval;
				Tick();
			}				
		}
	}
}
