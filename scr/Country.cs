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
		public class Resource
		{
			public int Oil { get; set; }
			public int Uranium { get; set; }
		}
		public class Army
		{
			public class Infantry
			{
				public class MotorizedInfantry//Мотострелки
				{

				}
				public class Cannonry//Артиллерия
				{

				}
				public class AirDefense
				{

				}
			}
			public class Fleet
			{

			}
			public class AirForce
			{

			}
			public class NuclearWeapon
			{

			}
			public class SecretService
			{

			}
			public class SpecialForces
			{

			}
			public class MissileDefense
			{

			}
		}
	}
}
