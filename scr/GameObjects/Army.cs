using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
	public class Army
	{
		//каждый класс войск будет иметь свои характеристики
		//секретные войска, ОМП и авиация будет иметь отдельные классы

		int Attack { get; set; } //кол-во урона за ход
		int Defence { get; set; } // кол-во поглащения урона, считается по убывающей полезности
		int Onslaught { get; set; } //натиск, пробитие защиты
		int Range { get; set; } //растояние атаки (чем больше расстояние, тем меньше вероятность, что атакают эту дивизию)
		int Armor { get; set; } //броня
		int ArmorPenetration { get; set; } //пробитие брони, если броня > ПБ, то наносится только половина урона 
		/*
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
		*/
	}
}
