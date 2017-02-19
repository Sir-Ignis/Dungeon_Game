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

	class Container : Item
	{
		public int slots;
		public string [] items_contained = new string[20];

		public Container (string itsName, int itsWeight, int itsSlots, string [] its_items_contained) : base (itsName, itsWeight)
		{
			name = itsName;
			weight = itsWeight;
			slots = itsSlots;
			items_contained = its_items_contained;
		}

		public Container initialise_container ()
		{
			string [] items = new string[20];
			for (int i = 0; i < 20; i++) 
			{
				items[i] = "";
			}
			Container Backpack = new Container ("Backpack",18,20,items);
			return Backpack;
		}

		public Container parse_Backpack (Item itsItem, Container itsBackpack)
		{
			int j = 0;
			for (int i = 0; i < 20; i++) 
			{
				if ((items_contained[i] == "") && j != 1)
				{
					items_contained[i] = itsItem.name;
					itsBackpack.weight += itsItem.weight;
					itsBackpack.slots--;
					j++;
				}
			}
			return itsBackpack;
		}

		public void print_Backpack (Container itsBackpack)
		{
			/*string response = "";
			Console.WriteLine ("\nWould you like to print the items in your backpack? ");

			do {
				Console.Write ("\nUser: ");
				response = Console.ReadLine ();
			} while ( ( (response != "yes") && (response != "Yes") && (response != "YES") ) && ( (response != "no") && (response != "No") && (response != "NO") ));

			if ((response == "yes") || (response == "Yes") || (response == "YES")) 
			{*/
				Console.WriteLine ("\nYou have {0} free backpack slots", itsBackpack.slots);
				for (int i = 0; i < 20; i++) {
					if (itsBackpack.items_contained [i] == "") {
						Console.WriteLine ("In slot {0} of your backpack you have nothing.", i);
					} else {
						Console.WriteLine ("In slot {0} of your backpack you have a {1}", i, itsBackpack.items_contained [i]);
					}
				}
			//}
		}

		public Container throw_away_items (Container itsBackpack) //still needs to be implemented
		{
			string response = "";
			int x = 0;
			/*Console.WriteLine ("\nWould you like to throw away items from your backpack? ");

			do {
				Console.Write ("\nUser: ");
				response = Console.ReadLine ();
			} while ( ( (response != "yes") && (response != "Yes") && (response != "YES") ) && ( (response != "no") && (response != "No") && (response != "NO") ));

			if ((response == "yes") || (response == "Yes") || (response == "YES")) 
			{*/
				Console.WriteLine ("From which slot would you like to throw away an item? ");
				do {
					Console.Write ("\nUser: ");
					x = Convert.ToInt32(Console.ReadLine ());
				}while ( (x < 0) || (x > 20) );

				if (itsBackpack.items_contained [x] == "") {
					Console.WriteLine ("In slot {0} of your backpack you have nothing.", x);
				}

				else
				{
				Console.WriteLine ("\n\nThrowing away a {0} ...",itsBackpack.items_contained[x]);
				itsBackpack.items_contained[x] = "";
				Item I = new Item("",0);

				switch (itsBackpack.items_contained[x])
				{
				case "Torch":
					I.weight = 5;
					break;
				case "Iron dagger":
					I.weight = 10;
					break;
				case "Leather cowl":
					I.weight = 2;
					break;
				case "Wooden shield":
					I.weight = 15;
					break;
				case "Leather trousers":
					I.weight = 3;
					break;
				case "Leather shoes":
					I.weight = 3;
					break;
				case "Leather armour":
					I.weight = 10;
					break;
				}
				itsBackpack.weight -= I.weight;
				}
			//}
			return itsBackpack;
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
			//args: (name, defence, weight)
			Armor Leather_cowl = new Armor ("Leather cowl", 2, 2); //id = 1;
			Armor Wooden_shield = new Armor ("Wooden shield", 12, 15); //id = 2;
			Armor Leather_trousers = new Armor ("Leather trousers", 5, 3); //id = 3;
			Armor Leather_shoes = new Armor ("Leather shoes", 1, 3); //id = 4
			Armor Leather_armor = new Armor ("Leather armour", 5, 10); //id = 5

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

	class Utility : Item // for torch (provides 6 adjacent squares all around the player) TO BE IMPLEMENTED
	{
		public bool providesLight = false;

		public Utility (string itsName, int itsWeight, bool itsProvidesLight) : base  (itsName, itsWeight)
		{
			name = itsName;
			providesLight = itsProvidesLight;
			weight = itsWeight;
		}
	}
}