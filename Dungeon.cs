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

namespace Dungeons
{
	class Dungeon
	{
		Menu m = new Menu();
		Armor A = new Armor("",0,0); //generic
		Weapon W = new Weapon("",0,0); //generic
		Item I = new Item("",0); //generic

		public Random rand = new Random();
		public char player = 'X';
		public int monsters_left = 0;
		public bool boss_slain = false;

		//public char [,] map = initialise_Map();
		public char [,] initialise_Map ()
		{
			int r = 0;
			int s = 0;
			/*SYMBOL * MEANING* ID
			 * M     * Monster* 4
			 * C     * Chest  * 3
			 * S	 * Stairs * 2
			 * .	 * Empty  * 0
			 * #     * Wall	  * 1
			*/ 

			/*find out why this doesn't work -->*/
			const string symbols = "MCS.#";
			//string symbols = " MCS.#";

			char [,] map = new char [50, 50];

			//[0,0] ,i.e (0,0), defined as origin

			//sets horizontal walls
			for (int i = 0; i < 50; i++) {
				map [i, 0] = symbols [4];
				map [i, 49] = symbols [4];//changed from: map [i, 50] = symbols [4];
			}

			//sets vertical walls
			for (int j = 0; j < 50; j++) {
				map [0, j] = symbols [4];
				map [49, j] = symbols [4];//changed from: map [j, 50] = symbols [4];
			}

			//spawns monsters and chests
			for (int x = 1; x < 49; x++) {
				/*M = symbols [0];
				 *C = symbols [1];
				 *S = symbols [2];
				 *. = symbols [3];
				 *# = symbols [4];
				*/
				//Thread.Sleep (100);
				for (int y = 1; y < 49; y++) {
					//problem here:
					/*
					r = rand.Next (1,61);

					if (r == 4)
					{
						map[x,y] = symbols[0];
					}
					else
					{
						map[x,y] = symbols[4];
					}

					r = rand.Next (1,81);

					if (r == 3)
					{
						map[x,y] = symbols[1];
					}
					else
					{
						map[x,y] = symbols[4];
					}*/

					r = rand.Next (1, 101);
					{
						if ((r >= 44) && (r <= 45)) {
							map [x, y] = symbols [0];
						} else if (r == 1) {
							map [x, y] = symbols [1];
							monsters_left++; //increments number of monsters in the dungeon
						} else if (((r >= 2) && (r <= 43)) || ((r >= 46) && (r <= 100))) {
							map [x, y] = symbols [3];
						}
					}
				}
			}

			/*r = rand.Next (1, 49);
			s = rand.Next (1, 49);

			map [r, s] = symbols [2];*/

			bool playerSpawned = false;
			do
			{
				r = rand.Next (1,48);
				s = rand.Next (1,48);
				if (map[r,s] == '.')
				{
					map[r,s] = player;
					playerSpawned = true;
				}
			}while (playerSpawned == false);

			return map;
		}

		public char [,] spawn_boss (char[,] itsMap)
		{
			int r = 0;
			int s = 0;

			//monsters_left = 0; //for testing

			if (monsters_left == 0) {
				do {
					r = rand.Next (1, 49);
					s = rand.Next (1, 49);
				} while ( (itsMap [r,s] != '.'));
				itsMap[r,s] = 'B';
			}

			return itsMap;
		}

		public char [,] spawn_stairs (char[,] itsMap)
		{
			int r = 0;
			int s = 0;

			//for testing
			/*boss_slain = true; 
			monsters_left = 0;*/
			//^
			/*Console.WriteLine(monsters_left);
			Console.WriteLine(boss_slain);
			Thread.Sleep(3000);*/ //for debugging
			if ((monsters_left == 0) && (boss_slain == true))
			{
				do {
					r = rand.Next (1, 49);
					s = rand.Next (1, 49);
				} while ( itsMap [r,s] != '.');

				itsMap[r,s] = 'S';
			}
			return itsMap;
		}

		public int getMonster_Damage (int atk_statM) //takes monster's attack stat as argument
		{
			int dmg_roll = rand.Next (1,atk_statM);
			return dmg_roll;
		}

		public int getPlayer_Damage (int atk_statP) //takes player's attack stat as argument
		{
			int dmg_roll = rand.Next (1,atk_statP);
			return dmg_roll;
		}

		public int damage_TakenM (int dmg_receivedM, int def_statM) //takes player's dmg; monster's def as arguments
		{
			int dmg_takenM = 0;
			if ((dmg_receivedM - def_statM) > 0) 
			{
				dmg_takenM = (dmg_receivedM - def_statM);
				return dmg_takenM;
			}

			else return dmg_takenM; // will return 0
		}

		public decimal getPlayer_XP (int hp_statM, int def_statP, int atk_statM)
		{
			decimal XP = ( hp_statM / (atk_statM - def_statP) ) + hp_statM;
			return Math.Truncate(XP);
		}

		public string generate_loot ()
		{
			int loot_roll = rand.Next (1, 6);
			string[] loot = {"Iron dagger","Leather cowl","Wooden shield","Leather trousers","Leather shoes","Leather armour"};

			return loot[loot_roll];
		}

		public int Fight (Skeleton itsSkeleton, Player itsP1, int itsStartHealthP, int its_levels_advanced)
		{
			int dmg_dealtP = 0;
			int dmg_takenM = 0;
			
			int dmg_dealtM = 0;
			int dmg_takenP = 0;
			int tempLvl = 0;

			int xp_to_lvl_main = itsP1.get_XP_for_next_level ();
			int xp_gained = Convert.ToInt32 (getPlayer_XP (itsSkeleton.health, itsP1.defence, itsSkeleton.attack));

			string chosen_attack_ability = "";

			Console.WriteLine ("\nFIGHT!\n");
			do {

				skeleton_Fight_Scene();

			//calculates damage dealt by monster and health of the player as a result of the monster's attack
			
			int n = 0;
			n = rand.Next (1,7);
			if (n != 6)
			{
					dmg_dealtM = getMonster_Damage (itsSkeleton.attack);
					dmg_takenP = damage_TakenM (dmg_dealtM, itsP1.defence);
					itsP1.health -= dmg_takenP;

					if (dmg_takenP > 0) {
						Console.Write ("You lost ");
						Console.ForegroundColor = ConsoleColor.Red;
						Console.Write ("{0} HP ", dmg_takenP);
						m.reset_colours();
						Console.Write ("due to an attack by {0}.\n", itsSkeleton.name);
						
						if (itsP1.health > 0) {
							Console.WriteLine ("{0} is now on {1} HP .\n", itsP1.name, itsP1.health);
						}
						
						else if (itsP1.health < 0)
						{
							Console.WriteLine ("You died!");
							Console.WriteLine ("Game over!");
							System.Environment.Exit (1);
						}
					} else {
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine ("You blocked an attack by {0} !", itsSkeleton.name);
						SoundPlayer player = new SoundPlayer();
						player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "shield_block.wav";
						player.Play();
						m.reset_colours();
					}
			}
			else //n == 6 => life drain
				{
					if (itsSkeleton.mana > 5)
					{
					itsSkeleton.mana -= 5;
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("{0} used life drain!",itsSkeleton.name);
					Console.ForegroundColor = ConsoleColor.White;
					dmg_dealtM = 5;
					dmg_takenP = dmg_dealtM; // true damage
					itsP1.health -= dmg_takenP;
					if (dmg_takenP > 0) {
						Console.Write ("You lost ");
						Console.ForegroundColor = ConsoleColor.Red;
						Console.Write ("{0} HP ", dmg_takenP);
						m.reset_colours();
						Console.Write ("due to an attack by {0}.\n", itsSkeleton.name);
						
						if (itsP1.health > 0) {
							Console.WriteLine ("{0} is now on {1} HP .\n", itsP1.name, itsP1.health);
						}
						
						else if (itsP1.health < 0)
						{
							Console.WriteLine ("You died!");
							Console.WriteLine ("Game over!");
							System.Environment.Exit (1);
						}
					} /*else {
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine ("You blocked an attack by {0} !", itsSkeleton.name);
						SoundPlayer player = new SoundPlayer();
						player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "../../shield_block.wav";
						player.Play();
						m.reset_colours();
					}*/
					}
				}

			chosen_attack_ability = itsP1.get_attack (itsP1.mana); // allows the player to enter what attack ability they want to use
			dmg_dealtP = itsP1.get_attack_damage (itsP1.attack, chosen_attack_ability); //assigns the damage dealt by the player determined by the attack ability and the base attack of the player
			dmg_takenM = damage_TakenM (dmg_dealtP, itsSkeleton.defence);
			itsSkeleton.health -= dmg_takenM;

			/*if (dmg_takenP > 0) {
				Console.Write ("You lost ");
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write ("{0} HP ", dmg_takenP);
				m.reset_colours();
				Console.Write ("due to an attack by {0}.\n", itsSkeleton.name);

				if (itsP1.health > 0) {
					Console.WriteLine ("{0} is now on {1} HP .\n", itsP1.name, itsP1.health);
				}
				
				else if (itsP1.health < 0)
					{
						Console.WriteLine ("You died!");
						Console.WriteLine ("Game over!");
						System.Environment.Exit (1);
					}
			} else {
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine ("You blocked an attack by {0} !", itsSkeleton.name);
				m.reset_colours();
			}*/

			Console.Write ("{0} lost ", itsSkeleton.name);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write ("{0} HP ", dmg_takenM);
			m.reset_colours();
			Console.Write ("due to your attack!\n");

			if (itsSkeleton.health > 0) {
				Console.WriteLine ("{0} is now on {1} HP .\n", itsSkeleton.name, itsSkeleton.health);
			} else {
				monsters_left--;
				Console.WriteLine ("\nYou have slain {0} !", itsSkeleton.name);


				Console.Write ("You currently on");
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write(" {0} HP",itsP1.health);
				m.reset_colours();
				Console.WriteLine ("\nYou gained {0} XP.", xp_gained);

				itsP1.xp += xp_gained;

					/*if ((itsP1.xp >= 162) || (itsP1.xp >= 477) || (itsP1.xp >= 989) || (itsP1.xp >= 1743)) //bugged
					{
						tempLvl = itsP1.xp_lvl;
						itsP1.xp_lvl++;
						itsP1.health = itsStartHealthP;
						Console.WriteLine("Congratulations you advanced from level {0} to level {1}.",tempLvl,tempLvl+1);
					}*/

					if ((itsP1.xp >= 162) && (its_levels_advanced == 0))
					{
						its_levels_advanced++;
						tempLvl = 1;
						itsP1.xp_lvl = tempLvl + 1;
						itsP1.health = itsStartHealthP;
						Console.WriteLine("Congratulations you advanced from level {0} to level {1}.",tempLvl,itsP1.xp_lvl);
					}
					else if ((itsP1.xp >= 477) && (its_levels_advanced == 1))
					{
						its_levels_advanced++;
						tempLvl = 2;
						itsP1.xp_lvl = tempLvl + 1;
						itsP1.health = itsStartHealthP;
						Console.WriteLine("Congratulations you advanced from level {0} to level {1}.",tempLvl,itsP1.xp_lvl);
					}
					else if ((itsP1.xp >= 989) && (its_levels_advanced == 2))
					{
						its_levels_advanced++;
						tempLvl = 3;
						itsP1.xp_lvl = tempLvl + 1;
						itsP1.health = itsStartHealthP;
						Console.WriteLine("Congratulations you advanced from level {0} to level {1}.",tempLvl,itsP1.xp_lvl);
					} 
					else if ((itsP1.xp >= 1743) && (its_levels_advanced == 3))
					{
						its_levels_advanced++;
						tempLvl = 4;
						itsP1.xp_lvl = tempLvl + 1;
						itsP1.health = itsStartHealthP;
						Console.WriteLine("Congratulations you advanced from level {0} to level {1}.",tempLvl,itsP1.xp_lvl);
					}
				xp_to_lvl_main = itsP1.get_XP_for_next_level ();
				
				Console.WriteLine ("You currently have {0} XP and need {1} XP to progress to the next level.", itsP1.xp, xp_to_lvl_main);
				//Console.WriteLine ("You looted a {0}, from {1}.", monster_loot, nameM);
			}
			} while (itsSkeleton.health > 0); 

			return its_levels_advanced;
		}

		public void skeleton_Fight_Scene ()
		{
			Console.WriteLine("\n\n");
			Console.WriteLine("▒▒▒░░░░░░░░░░▄▐░░░░"+
							  "\n▒░░░░░░▄▄▄░░▄██▄░░░"+
			                  "\n░░░░░░▐▀█▀▌░░░░▀█▄░"+
			                  "\n░░░░░░▐█▄█▌░░░░░░▀█▄"+
			                  "\n░░░░░░░▀▄▀░░░▄▄▄▄▄▀▀"+
			                  "\n░░░░░▄▄▄██▀▀▀▀░░░░░"+
			                  "\n░░░░█▀▄▄▄█░▀▀░░░░░░"+
			                  "\n░░░░▌░▄▄▄▐▌▀▀▀░░░░░"+
			                  "\n░▄░▐░░░▄▄░█░▀▀░░░░░"+
			                  "\n░▀█▌░░░▄░▀█▀░▀░░░░░"+
			                  "\n░░░░░░░░▄▄▐▌▄▄░░░░░"+
			                  "\n░░░░░░░░▀███▀█░▄░░░"+
			                  "\n░░░░░░░▐▌▀▄▀▄▀▐▄░░░"+
			                  "\n░░░░░░░▐▀░░░░░░▐▌░░"+
			                  "\n░░░░░░░█░░░░░░░░█░░"+
							  "\n░░░░░░▐▌░░░░░░░░░█░");
			Console.WriteLine("\n\n");
		}

		public void Wraith_Fight_Scene ()
		{

		}

		public int Boss_Fight (Wraith itsWraith, Player itsP1, int itsStartHealthP, int its_levels_advanced)
		{
			int dmg_dealtP = 0;
			int dmg_takenM = 0;
			
			int dmg_dealtM = 0;
			int dmg_takenP = 0;
			int tempLvl = 0;
			
			int xp_to_lvl_main = itsP1.get_XP_for_next_level ();
			int xp_gained = Convert.ToInt32 (getPlayer_XP (itsWraith.health, itsP1.defence, itsWraith.attack));
			
			string chosen_attack_ability = "";
			
			Console.WriteLine ("\nFIGHT!\n");
			do {				
				//calculates damage dealt by monster and health of the player as a result of the monster's attack
				
				dmg_dealtM = getMonster_Damage (itsWraith.attack);
				dmg_takenP = damage_TakenM (dmg_dealtM, itsP1.defence);
				itsP1.health -= dmg_takenP;
				
				
				chosen_attack_ability = itsP1.get_attack (itsP1.mana); // allows the player to enter what attack ability they want to use
				dmg_dealtP = itsP1.get_attack_damage (itsP1.attack, chosen_attack_ability); //assigns the damage dealt by the player determined by the attack ability and the base attack of the player
				dmg_takenM = damage_TakenM (dmg_dealtP, itsWraith.defence);
				itsWraith.health -= dmg_takenM;
				
				if (dmg_takenP > 0) {
					Console.Write ("You lost ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write ("{0} HP ", dmg_takenP);
					m.reset_colours();
					Console.Write ("due to an attack by {0}.\n", itsWraith.name);
					
					if (itsP1.health > 0) {
						Console.WriteLine ("{0} is now on {1} HP .\n", itsP1.name, itsP1.health);
					}
					
					else if (itsP1.health < 0)
					{
						Console.WriteLine ("You died!");
						Console.WriteLine ("Game over!");
						System.Environment.Exit (1);
					}
				} else {
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine ("You blocked an attack by {0} !", itsWraith.name);
					m.reset_colours();
				}
				
				Console.Write ("{0} lost ", itsWraith.name);
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write ("{0} HP ", dmg_takenM);
				m.reset_colours();
				Console.Write ("due to your attack!\n");
				
				if (itsWraith.health > 0) {
					Console.WriteLine ("{0} is now on {1} HP .\n", itsWraith.name, itsWraith.health);
				} else {
					Console.WriteLine ("\nYou have slain {0} !", itsWraith.name);
					
					
					Console.Write ("You currently on");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write(" {0} HP",itsP1.health);
					m.reset_colours();
					Console.WriteLine ("\nYou gained {0} XP.", xp_gained);
					
					itsP1.xp += xp_gained;
					
					/*if ((itsP1.xp >= 162) || (itsP1.xp >= 477) || (itsP1.xp >= 989) || (itsP1.xp >= 1743)) //bugged
					{
						tempLvl = itsP1.xp_lvl;
						itsP1.xp_lvl++;
						itsP1.health = itsStartHealthP;
						Console.WriteLine("Congratulations you advanced from level {0} to level {1}.",tempLvl,tempLvl+1);
					}*/
					
					if ((itsP1.xp >= 162) && (its_levels_advanced == 0))
					{
						its_levels_advanced++;
						tempLvl = 1;
						itsP1.xp_lvl = tempLvl + 1;
						itsP1.health = itsStartHealthP;
						Console.WriteLine("Congratulations you advanced from level {0} to level {1}.",tempLvl,itsP1.xp_lvl);
					}
					else if ((itsP1.xp >= 477) && (its_levels_advanced == 1))
					{
						its_levels_advanced++;
						tempLvl = 2;
						itsP1.xp_lvl = tempLvl + 1;
						itsP1.health = itsStartHealthP;
						Console.WriteLine("Congratulations you advanced from level {0} to level {1}.",tempLvl,itsP1.xp_lvl);
					}
					else if ((itsP1.xp >= 989) && (its_levels_advanced == 2))
					{
						its_levels_advanced++;
						tempLvl = 3;
						itsP1.xp_lvl = tempLvl + 1;
						itsP1.health = itsStartHealthP;
						Console.WriteLine("Congratulations you advanced from level {0} to level {1}.",tempLvl,itsP1.xp_lvl);
					} 
					else if ((itsP1.xp >= 1743) && (its_levels_advanced == 3))
					{
						its_levels_advanced++;
						tempLvl = 4;
						itsP1.xp_lvl = tempLvl + 1;
						itsP1.health = itsStartHealthP;
						Console.WriteLine("Congratulations you advanced from level {0} to level {1}.",tempLvl,itsP1.xp_lvl);
					}
					xp_to_lvl_main = itsP1.get_XP_for_next_level ();
					
					Console.WriteLine ("You currently have {0} XP and need {1} XP to progress to the next level.", itsP1.xp, xp_to_lvl_main);
					//Console.WriteLine ("You looted a {0}, from {1}.", monster_loot, nameM);
				}
			} while (itsWraith.health > 0); 
			
			return its_levels_advanced;
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
			Weapon its_weapon_returned = new Weapon ("", 0, 0);

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
					//itsP1.cap = itsP1.cap - its_chest_armor_loot.weight;
					switch (its_chest_armor_loot.name) {
					case "Iron dagger":
						itsP1.items_equipped [2] = "Iron dagger";
						itsP1.wielding_weapon = true;
						break;
					
					case "Wooden shield":
						itsP1.items_equipped [3] = "Wooden shield";
						break;
					
					case "Leather boots":
						itsP1.items_equipped [5] = "Leather boots";
						break;
					}

					switch (its_chest_armor_loot.name) {
					case "Iron dagger":
						Console.WriteLine ("\nYou looted an {0} from the chest.", its_chest_armor_loot.name);
						break;
					
					case "Leather boots":
						Console.WriteLine ("\nYou looted a pair of {0} from the chest.", its_chest_armor_loot.name);
						break;

					default:
						Console.WriteLine ("\nYou looted a {0} from the chest.", its_chest_armor_loot.name);
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
			else //itsCoin == 2
			{ 
				if ((itsP1.cap >= its_chest_weapon_loot.weight) && (playerBackpack.slots > 0)) 
				{
					I.name = its_chest_weapon_loot.name;
					I.weight = its_chest_weapon_loot.weight;
					playerBackpack = playerBackpack.parse_Backpack(I,playerBackpack);
					//itsP1.cap = itsP1.cap - its_chest_weapon_loot.weight;

					Console.WriteLine ("\nYou looted {0} from the chest.", its_chest_weapon_loot.name);
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
			System.Environment.Exit (1);
		}

		public Dungeon (char [,] itsMap)
		{
			itsMap = initialise_Map();
		}
	}

	public class ConsoleReader
	{
		//source: http://stackoverflow.com/questions/12355378/read-from-location-on-console-c-sharp
		public static IEnumerable<string> ReadFromBuffer(short x, short y, short width, short height)
		{
			IntPtr buffer = Marshal.AllocHGlobal(width * height * Marshal.SizeOf(typeof(CHAR_INFO)));
			if (buffer == null)
				throw new OutOfMemoryException();
			
			try
			{
				COORD coord = new COORD();
				SMALL_RECT rc = new SMALL_RECT();
				rc.Left = x;
				rc.Top = y;
				rc.Right = (short)(x + width - 1);
				rc.Bottom = (short)(y + height - 1);
				
				COORD size = new COORD();
				size.X = width;
				size.Y = height;
				
				const int STD_OUTPUT_HANDLE = -11;
				if (!ReadConsoleOutput(GetStdHandle(STD_OUTPUT_HANDLE), buffer, size, coord, ref rc))
				{
					// 'Not enough storage is available to process this command' may be raised for buffer size > 64K (see ReadConsoleOutput doc.)
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
				
				IntPtr ptr = buffer;
				for (int h = 0; h < height; h++)
				{
					StringBuilder sb = new StringBuilder();
					for (int w = 0; w < width; w++)
					{
						CHAR_INFO ci = (CHAR_INFO)Marshal.PtrToStructure(ptr, typeof(CHAR_INFO));
						char[] chars = Console.OutputEncoding.GetChars(ci.charData);
						sb.Append(chars[0]);
						ptr += Marshal.SizeOf(typeof(CHAR_INFO));
					}
					yield return sb.ToString();
				}
			}
			finally
			{
				Marshal.FreeHGlobal(buffer);
			}
		}
		
		[StructLayout(LayoutKind.Sequential)]
		private struct CHAR_INFO
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
			public byte[] charData;
			public short attributes;
		}
		
		[StructLayout(LayoutKind.Sequential)]
		private struct COORD
		{
			public short X;
			public short Y;
		}
		
		[StructLayout(LayoutKind.Sequential)]
		private struct SMALL_RECT
		{
			public short Left;
			public short Top;
			public short Right;
			public short Bottom;
		}
		
		[StructLayout(LayoutKind.Sequential)]
		private struct CONSOLE_SCREEN_BUFFER_INFO
		{
			public COORD dwSize;
			public COORD dwCursorPosition;
			public short wAttributes;
			public SMALL_RECT srWindow;
			public COORD dwMaximumWindowSize;
		}
		
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool ReadConsoleOutput(IntPtr hConsoleOutput, IntPtr lpBuffer, COORD dwBufferSize, COORD dwBufferCoord, ref SMALL_RECT lpReadRegion);
		
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetStdHandle(int nStdHandle);
	}
}

