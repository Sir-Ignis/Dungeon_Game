using System;
using System.Media;
using System.Collections.Generic;
using System.Threading;
using Players;
using Monsters;
using Items;
using MainMenu;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Text;
using DungeonMap;

namespace Dungeons
{
	class Dungeon
	{
		//Menu m = new Menu();
		Armor A = new Armor("",0,0); //generic
		Weapon W = new Weapon("",0,0); //generic
		Item I = new Item("",0); //generic

		public Random rand = new Random();
		public char player = 'X';
		public int monsters_left = 0;
		public bool boss_slain = false;

		public string generate_loot ()
		{
			int loot_roll = rand.Next (1, 7);
			string[] loot = {"Iron dagger","Leather cowl","Wooden shield","Leather trousers","Leather shoes","Leather armour","Iron sword"};

			return loot[loot_roll];
		}

		public Armor Chest_Loot_Armor ()
		{
			int lootID = 0;

			Armor its_armor_returned = new Armor ("", 0, 0);

				lootID = rand.Next (1,6);
				its_armor_returned = A.initialise_armors(lootID);

			return its_armor_returned;
		}

		public Weapon Chest_Loot_Weapons ()
		{
			int lootID = 0;
			int dice = 0;
			Weapon its_weapon_returned = new Weapon ("", 0, 0);

			dice = rand.Next (1,7);

			if (dice == 6) {
				lootID = 2;
			} else {
				lootID = 1;
			}
			its_weapon_returned = W.initialise_weapons(lootID);

			return its_weapon_returned;
		}

		public bool Parse_Chest_Loot (int its_Coin, Armor its_chest_armor_loot, Weapon its_chest_weapon_loot, Player itsP1, Items.Container playerBackpack)
		{
			bool itemLooted = false;
			/*
			Armor Leather_cowl = new Armor ("Leather cowl", 2, 2); //id = 1;
			Armor Wooden_shield = new Armor ("Wooden shield", 12, 15); //id = 2;
			Armor Leather_trousers = new Armor ("Leather trousers", 5, 3); //id = 3;
			Armor Leather_shoes = new Armor ("Leather shoes", 1, 3); //id = 4
			Armor Leather_armor = new Armor ("Leather armour", 5, 10); //id = 5
			*/
			if (its_Coin == 1) {
				if ((itsP1.cap >= its_chest_armor_loot.weight) && (playerBackpack.slots > 0)) {
					I.name = its_chest_armor_loot.name;
					I.weight = its_chest_armor_loot.weight;
					playerBackpack = playerBackpack.parse_Backpack (I, playerBackpack);
					itsP1.cap = itsP1.cap - its_chest_armor_loot.weight;
					/*switch (its_chest_armor_loot.name) {
					/*case "Iron dagger":
						itsP1.items_equipped [2] = "Iron dagger";
						itsP1.wielding_weapon = true;
						break;
					
					case "Iron sword":
						itsP1.items_equipped [2] = "Iron sword";
						itsP1.wielding_weapon = true;
						break;*/
					
					/*case "Wooden shield":
						itsP1.items_equipped [3] = "Wooden shield";
						break;
					
					case "Leather boots":
						itsP1.items_equipped [5] = "Leather boots";
						break;
					*/

					switch (its_chest_armor_loot.name) {
					/*case "Iron dagger":
						Console.WriteLine ("\n\nYou looted an {0} from the chest.", its_chest_armor_loot.name);
						break;*/
					
					case "leather boots":
						Console.WriteLine ("\n\nYou looted a pair of {0} from the chest.", its_chest_armor_loot.name);
						break;

					default:
						Console.WriteLine ("\n\nYou looted a {0} from the chest.", its_chest_armor_loot.name);
						break;
					}
					Console.WriteLine ("You now have {0} oz cap.", itsP1.cap);
					itemLooted = true;
				} 
				else 
				{
					if (playerBackpack.slots == 0)
					{
						Console.WriteLine ("You do not have any free slots in your backpack to take this item.");
					}
					Console.WriteLine ("You do not have enough cap to take the {0} from the chest, as it weighs {1} oz", its_chest_armor_loot.name, its_chest_armor_loot.weight);
				}
			} 
			else if (its_Coin == 2)
			{ 
				if ((itsP1.cap >= its_chest_weapon_loot.weight) && (playerBackpack.slots > 0)) 
				{
					I.name = its_chest_weapon_loot.name;
					I.weight = its_chest_weapon_loot.weight;
					playerBackpack = playerBackpack.parse_Backpack(I,playerBackpack);
					itsP1.cap = itsP1.cap - its_chest_weapon_loot.weight;

					Console.WriteLine ("\n\nYou looted an {0} from the chest.", its_chest_weapon_loot.name);
					Console.WriteLine ("You now have {0} oz cap.", itsP1.cap);
					itemLooted = true;
				}
				else 
				{
					if (playerBackpack.slots == 0)
					{
						Console.WriteLine ("You do not have any free slots in your backpack to take this item.");
					}
					Console.WriteLine ("You do not have enough cap to take the {0} from the chest, as it weighs {1} oz", its_chest_weapon_loot.name, its_chest_weapon_loot.weight);
				}

			}
			return itemLooted;
		}
		
		public void Finish ()
		{
			Console.WriteLine ("\n\nYou made out the dungeon alive!");
			Console.WriteLine ("Game over!");
			Console.ReadLine ();
			System.Environment.Exit (1);
		}

		public Dungeon (char [,] itsMap)
		{
			//itsMap = initialise_Map();
		}
	}
}

