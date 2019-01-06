using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
	public class Country
	{
		public readonly string Name;
		public Resource Resources { get; set; }
		public Army Armies { get; set; }

		public Country(string name)
		{
			Name = name;
		}
	}
}
