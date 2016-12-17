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

		public Weapon initialise_weapons (int x)
		{
			Weapon weapon_returned = new Weapon ("", 0, 0);
			Weapon Iron_dagger = new Weapon ("Iron dagger", 5, 10);
			
			weapon_returned = Iron_dagger;
			return weapon_returned;
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

		public Armor initialise_armors (int x)
		{
			Armor armor_returned = new Armor ("",0,0);
			Armor Leather_cowl = new Armor ("Leather cowl", 2, 2); //id = 1;
			Armor Wooden_shield = new Armor ("Wooden shield", 12, 15); //id = 2;
			Armor Leather_trousers = new Armor ("Leather trousers", 5, 3); //id = 3;
			Armor Leather_shoes = new Armor ("Leather shoes", 1, 3); //id = 4
			Armor Leather_armor = new Armor ("Leather armor", 5, 10); //id = 5

			switch (x) 
			{
			case 1: 
				armor_returned = Leather_cowl;
				break;
			case 2:
				armor_returned = Wooden_shield;
				break;
			case 3:
				armor_returned = Leather_trousers;
				break;
			case 4:
				armor_returned = Leather_shoes;
				break;
			case 5:
				armor_returned = Leather_armor;
				break;
			}
			return armor_returned;
		}
	}
}
