using System;
using Items;
using Monsters;
using Dungeons;
using Players;
using MainMenu;
using System.Threading;

namespace Dungeon_Game
{

	class MainClass
	{
		public static void Main (string[] args)
		{
			Random rand = new Random ();
			Menu m = new Menu ();

			if (Environment.OSVersion.Platform == PlatformID.Win32NT) 
			{
				m.Maximize ();
			}
			m.reset_colours();
			Console.Clear ();
			m.print_Menu ();

			Console.Write ("USER: ");
			string response = Console.ReadLine ();

			if ((response == "exit") || (response == "Exit") || (response == "EXIT")) {
				System.Environment.Exit (1);
			}

			Console.Clear ();
			//m.loading(); //<--- remove when coding
			Console.Clear ();

			Skeleton skele = new Skeleton ("Default Skeleton", 60, 10, 10, 5);


			/*string nameM =*/ skele.name = "Bob";
			/*int healthM =*/  skele.health = 30;
			/*int manaM = */ skele.mana = 5;
			/*int defenceM =*/  skele.defence = 0;
			/*int attackM = */ skele.attack = 10;

			/* <--- for debug only
			Console.WriteLine ("A skeleton called {0} with:" +
				"\n{1} HP" +
				"\n{2} MP" +
				"\n{3} DEF" +
				"\n{4} ATK" +
				"\nhas been created...", nameM, healthM, manaM, defenceM, attackM); //M = Monster
			*/

			const int player_start_cap = 180;
			Player p1 = new Player ("Default Player", 0, 0, 0, 0, 0, player_start_cap);
			p1.cap = player_start_cap;

			string nameP = p1.name = System.Security.Principal.WindowsIdentity.GetCurrent ().Name;

			int healthP = p1.health = 30;
			int manaP = p1.mana = 5;
			int defenceP = p1.defence = 0;
			int attackP = p1.attack = 10;
			//int cap = p1.cap;
			int xpP = p1.xp;
			//int xp_to_lvl_main = p1.get_XP_for_next_level ();

			Console.WriteLine ("\nWelcome {0} !", nameP);

			for (int j = 0; j < 5; j++) { //cycles through and assigns local player stats random values from the Player get_stats function
				switch (j) {
				case 1:
					healthP = p1.health = p1.get_stats (j);
					break;
				case 2:
					manaP = p1.mana = p1.get_stats (j);
					break;
				case 3:
					defenceP = p1.defence = p1.get_stats (j);
					break;
				case 4:
					attackP = p1.attack = p1.get_stats (j);
					break;

				}
			}

			Console.WriteLine ("A player called {0} with:\n" +
				"\n{1} HP" +
				"\n{2} MP" +
				"\n{3} DEF" +
				"\n{4} ATK" +
				"\n{5} XP" +
				"\nhas been created...\n", nameP, healthP, manaP, defenceP, attackP, xpP); //P = Player

			/* <--- being transferred to Dungeon.cs
			int dmg_dealtP = 0;
			int dmg_takenM = 0;

			int dmg_dealtM = 0;
			int dmg_takenP = 0;
			*/

			char [,] map = new char [50, 50];
			Dungeon D1 = new Dungeon (map);

			map = D1.initialise_Map ();

			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine ("MAP");
			m.reset_colours();

			//prints map
			int c = 0;
			for (int x = 0; x < 50; x++) 
			{

				for (int y = 0; y < 50; y++) 
				{
					//c++;

					if (c % 50 == 0)
					{
						Console.WriteLine();
						Console.Write (map[x,y]);
					}
					else
					{
						switch (map[x,y])
						{
							case 'X':
									Console.ForegroundColor = ConsoleColor.Red;
									Console.Write ("X");
									m.reset_colours();
									break;
							case 'M':
									Console.ForegroundColor = ConsoleColor.Magenta;
									Console.Write ("M");
									m.reset_colours();
									break;
							case 'C':
									Console.ForegroundColor = ConsoleColor.Yellow;
									Console.Write ("C");
									m.reset_colours();
									break;
							default:
									Console.Write (map[x,y]);
									break;
						}
					}
					c++;
				}
			}

			Console.WriteLine ();
			//Console.ReadLine ();

			ConsoleKeyInfo keyInfo = Console.ReadKey ();
			Monster M = new Monster("",0,0,0,0);
			string player_string_cords = p1.get_player_cords_s(map);
			Armor chest_armor_loot = new Armor("",0,0);
			Weapon chest_weapon_loot = new Weapon ("",0,0);
			int [,] player_int_cords = p1.get_player_cords(map);
			int Coin = 0;
			bool canMove = p1.check_move(player_int_cords,map,keyInfo);
			char item = ' ';
			do
			{
			keyInfo = Console.ReadKey ();

			player_string_cords = p1.get_player_cords_s(map);
			Console.WriteLine (player_string_cords); /*<--- for debugging only*/
			player_int_cords = p1.get_player_cords(map);
			//Console.WriteLine("({0}, {1})",player_int_cords[0,0],player_int_cords[1,1]);  /*<--- for debugging only*/

			/*if (map[player_int_cords[0,0],player_int_cords[1,1]] == '*')
				{
					System.Environment.Exit (1);
				}*/
			canMove = p1.check_move(player_int_cords,map,keyInfo);
				item = p1.move(canMove,map,player_int_cords,keyInfo);

				switch (item)
				{
				case 'M':
					skele.health = M.resetHealth(skele);
					D1.Fight(skele,p1);
					map[player_int_cords[0,0],player_int_cords[1,1]] = 'X';
					break;
					
				case 'C':
					Coin = rand.Next (1,3);

					if (Coin == 1) //armor
					{
						chest_armor_loot = D1.Chest_Loot_Armor();
					}
					else //Coin == 2
					{
						chest_weapon_loot = D1.Chest_Loot_Weapons();
						p1.wielding_weapon = true;
					}
					D1.Parse_Chest_Loot(Coin, chest_armor_loot,chest_weapon_loot,p1);
					Console.WriteLine("{0},{1}",player_int_cords[0,0],player_int_cords[1,1]); //for debugging
					map[player_int_cords[0,0],player_int_cords[1,1]] = 'X';
					break;
					
				case 'S':
					D1.Finish();
					break;
				}
			c = 0;
			for (int x = 0; x < 50; x++) 
			{
				
				for (int y = 0; y < 50; y++) 
				{
					//c++;
					
					if (c % 50 == 0)
					{
						Console.WriteLine();
							Console.Write (map[x,y]);
					}
					else
					{
						switch (map[x,y])
						{
							case 'X':
								Console.ForegroundColor = ConsoleColor.Red;
								Console.Write ("X");
								m.reset_colours();
								break;
							case 'M':
								Console.ForegroundColor = ConsoleColor.Magenta;
								Console.Write ("M");
								m.reset_colours();
								break;
							case 'C':
								Console.ForegroundColor = ConsoleColor.Yellow;
								Console.Write ("C");
								m.reset_colours();
								break;
							default:
								Console.Write (map[x,y]);
								break;
						}
					}
					c++;
				}
			}

			}while (true);

			//still needs to be implemented
			/*
			string [] items_worn = p1.items_equipped;
			items_worn = p1.player_items_equiped (items_worn);
			p1.print_worn_items (items_worn);

			p1.save_starting_stats (nameP, healthP, manaP, defenceP, attackP, xpP, items_worn);

			string chosen_attack_ability = "";

			Console.WriteLine ("You can carry {0} more kgs worth of items.", cap);
			Console.WriteLine ("\nDo you wish to save your character stats?");

			Console.Write ("User (y/n): ");
			response = Console.ReadLine ();

			if (response == "y") {
				//p1.quick_save(nameP, healthP, manaP, defenceP, attackP, xpP, items_worn); //comment out this line for the .exe to work on other computers
				Console.WriteLine ("File successfully saved!");

			}*/
		}
	}
}
