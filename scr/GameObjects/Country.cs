using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameObjects
{
	public class Country
	{
		public readonly string Name;
		public Resource Resources { get; set; }
		public StateArmy Army { get; set; }
		public ScorePoints ScorePoints { get; set; }
		public Production Production { get; set; }

		public Country(string name)
		{
			Name = name;
		}
	}
}
