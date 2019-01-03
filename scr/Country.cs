using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
	public class Country
	{
		public string Name;
		public Resources resources { get; set; }

		public Country(string name)
		{
			Name = name;
		}
		public class Resources
		{
			public int Oil { get; set; }
			public int Uranium { get; set; }
		}
	}
}
