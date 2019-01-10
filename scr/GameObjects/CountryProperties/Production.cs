using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameObjects
{
	public class Production
	{
		public List<Factory> Factories { get; set; }
		public List<ShipYard> ShipYards { get; set; }
	}
}
