using System;

namespace Items
{
	class Item
	{
		public string name = "";
		public int weight = 0;

		public Item (string itsName, int itsWeight)
		{
			name  = itsName;
			weight = itsWeight;
		}
	}

	class Weapon : Item
	{
		public int attackPoints = 0;

		public Weapon (string itsName, int itsAttackPoints, int itsWeight) : base (itsName, itsWeight)
		{
			name = itsName;
			attackPoints = itsAttackPoints;
			weight = itsWeight;
		}
	}

	class Armor : Item
	{
		public int defencePoints = 0;

		public Armor (string itsName, int itsDefencePoints, int itsWeight) : base (itsName, itsWeight)
		{
			name = itsName;
			defencePoints = itsDefencePoints;
			weight = itsWeight;
		}
	}
}