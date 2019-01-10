using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameObjects
{
	public class StateArmy
	{
		public Fleet Fleet { get; set; }
		public AirDefense AirDefense { get; set; }
		public Cannonry Cannonry { get; set; }
		public MotorizedInfantry MotorizedInfantry { get; set; }
		public MissileDefense MissileDefense { get; set; }
		public BiologicalWeapon BiologicalWeapon { get; set; }
		public ChemicalWeapon ChemicalWeapon { get; set; }
		public NuclearWeapon NuclearWeapon { get; set; }
		public Saboteurs Saboteurs { get; set; }
		public SpyService SpyService { get; set; }
		public AirForce AirForce { get; set; }
	}
}
