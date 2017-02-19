using System;
using System.Media;
using Items;
using Monsters;
using Dungeons;
using Players;
using MainMenu;
using System.Threading;
using System.Diagnostics;
using System.IO;
using genericFunctions;

namespace Dungeon_Game
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Random rand = new Random ();
			Menu m = new Menu ();
			SoundPlayer player = new SoundPlayer ();
			player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Dungeon_Ambience_Music.wav";
			player.Play ();
			string response = "";
			if (Environment.OSVersion.Platform == PlatformID.Win32NT) {
				m.Maximize ();
			}

			GenericFunctions g = new GenericFunctions ();
			char readChar = ' ';

			m.reset_colours ();
			Console.Clear ();

			do
			{
			m.print_Menu ();
			readChar = g.genericKeyInput (2);


			switch (readChar) 
			{
			case 'S':
				Console.Clear ();
				break;
			
			case 'H':
				m.printHelp();
				Console.WriteLine ("\nPress any key to return to the main menu...");
				Console.ReadKey();
				Console.Clear();
				break;
			case 'E':
				System.Environment.Exit (1);
				break;
			}
			} while ((readChar != 'S') && (readChar != 'E'));

			bool musicOn = true;

			Console.WriteLine ("Would you like the dungeon music on?");
			readChar = g.genericKeyInput(0);

			ConsoleKeyInfo tempKeyInfo;

			if (readChar == 'N') 
			{
				musicOn = false;
			}

			Console.Clear ();
			player.Stop ();
			player.Dispose();

			m.loading(); //<--- remove when coding
			Console.Clear ();

			m.print_cave();

			Skeleton skele = new Skeleton ("Default Skeleton", 60, 10, 10, 5);
			Wraith Boss = new Wraith ("Default Wraith", 100, 50, 5, 12);

			/*string nameM =*/
			skele.name = skele.generate_random_name ();
			/*int healthM =*/
			skele.health = 30;
			/*int manaM = */
			skele.mana = 5;
			/*int defenceM =*/
			skele.defence = 0;
			/*int attackM = */
			skele.attack = 10;

			Boss.name = "Wraith";

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
			p1.xp = 1;
			int xpP = p1.xp;
			//int start_xpP = xpP;
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

			for (int k = 0; k < 5; k++) { //cycles through and assigns skeleton stats random values from the Skeleton get_stats function
				switch (k) {
				case 1:
					skele.health = skele.get_stats (k);
					break;
				case 2:
					skele.mana = skele.get_stats (k);
					break;
				case 3:
					skele.defence = skele.get_stats (k);
					break;
				case 4:
					skele.attack = skele.get_stats (k);
					break;
					
				}
			}

			for (int l = 0; l < 5; l++) { //cycles through and assigns skeleton stats random values from the Skeleton get_stats function
				switch (l) {
				case 1:
					Boss.health = Boss.boss1_stats (l);
					break;
				case 2:
					Boss.mana = Boss.boss1_stats (l);
					break;
				case 3:
					Boss.defence = Boss.boss1_stats (l);
					break;
				case 4:
					Boss.attack = Boss.boss1_stats (l);
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

			int startHealthP = healthP;
			/* <--- being transferred to Dungeon.cs
			int dmg_dealtP = 0;
			int dmg_takenM = 0;

			int dmg_dealtM = 0;
			int dmg_takenP = 0;
			*/

			char [,] map = new char [50, 50];
			Dungeon D1 = new Dungeon (map);

			map = D1.initialise_Map ();
			//Console.WriteLine(D1.monsters_left); //for debugging

			tempKeyInfo = Console.ReadKey ();
			//Monster M = new Monster("",0,0,0,0);
			string player_string_cords = p1.get_player_cords_s(map);
			Armor chest_armor_loot = new Armor ("", 0, 0);
			Weapon chest_weapon_loot = new Weapon ("", 0, 0);
			int [,] player_int_cords = p1.get_player_cords (map);
			bool item_looted = false;
			int levelsAdvanced = 0;

			string [] player_items = new string[20];
			//string [] player_items_worn = new string[6];
			for (int i = 0; i < 20; i++) {
				player_items [i] = "";
			}

			Container playerBackpack = new Container ("", 0, 0, player_items);
			Utility Torch = new Utility ("Torch", 5, true);
			playerBackpack = playerBackpack.initialise_container ();
			playerBackpack = playerBackpack.parse_Backpack(Torch,playerBackpack);
			p1.cap -= playerBackpack.weight;
			int Coin = 0;
			bool canMove = p1.check_move (player_int_cords, map, tempKeyInfo);
			char item = ' ';

			SoundPlayer mainTheme = new SoundPlayer ();
			mainTheme.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "8bit Dungeon Music.wav";

			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine ("MAP");
			m.reset_colours ();

			string admin_password = "20H4CKER17";
			
			Console.Write ("\n\n\nAdmin password:");
			response = Console.ReadLine ();

			if (response == admin_password) {
				int l = 0;
				p1.attack = 100;
				attackP = p1.attack;

				for (int x = 0; x < 50; x++) {
					for (int y = 0; y < 50; y++) {
						//c++;
						if (map [x, y] == 'M') {
							map [x, y] = '.';
						}
						if (l % 50 == 0) {
							Console.WriteLine ();
							Console.Write (map [x, y]);
						} else {
							switch (map [x, y]) {
							case 'X':
								Console.ForegroundColor = ConsoleColor.Red;
								Console.Write ("X");
								m.reset_colours ();
								break;
							case 'M':
								Console.ForegroundColor = ConsoleColor.Magenta;
								Console.Write ("M");
								m.reset_colours ();
								break;
							case 'C':
								Console.ForegroundColor = ConsoleColor.Yellow;
								Console.Write ("C");
								m.reset_colours ();
								break;
							default:
								Console.Write (map [x, y]);
								break;
							}
						}
						l++;
					}
				}
				D1.monsters_left = 0;
				map = D1.spawn_boss (map);
				Console.Clear();
			}

			//prints map
			int torchLight = 5;

			/*************
			 *     ^     *
			 *     ^     *
			 *     ^     *
			 *     ^     *
			 *     ^     *
			 *<<<<<X>>>>>*
			 *     V     *
			 *     V     *
			 *     V     *
			 *     V     *
			 *     V     *
			 *************
			 */

			bool squareIsVisible = true;
			bool[,] mapVisible = new bool[50, 50];

			if (((player_int_cords [0, 0] < 44) && (player_int_cords [1, 1] < 44))
			    || ((player_int_cords [0, 0] > 4) && (player_int_cords [1, 1] > 4))) {
				for (int e = player_int_cords [0, 0] - 4; e < player_int_cords [0, 0] + 4; e++) {
					for (int f = player_int_cords [1, 1] - 4; f < player_int_cords [1, 1] + 4; f++) {
						//assign visibleSquares array around player square visible = true;
						mapVisible [e, f] = squareIsVisible;
					}
				}
			} 
			else 
			{
				for (int e = player_int_cords [0, 0]; (e >= -3) && (e < 50); e++) {
					for (int f = player_int_cords [1, 1]; (f >= -3) && (f < 50); f++) {
						squareIsVisible = false;
						mapVisible [e, f] = squareIsVisible;
					}
				}
			}

			int c = 0;
			for (int x = 0; x < 50; x++) {

				for (int y = 0; y < 50; y++) {
					//c++;

					if (c % 50 == 0) {
						Console.WriteLine ();
						Console.Write (map [x, y]);
					} else {

						if (mapVisible[x,y] == true) {
							switch (map [x, y]) {
							case 'X':
								Console.ForegroundColor = ConsoleColor.Red;
								Console.Write ("X");
								m.reset_colours ();
								break;
							case 'M':
								Console.ForegroundColor = ConsoleColor.Magenta;
								Console.Write ("M");
								m.reset_colours ();
								break;
							case 'C':
								Console.ForegroundColor = ConsoleColor.Yellow;
								Console.Write ("C");
								m.reset_colours ();
								break;
							default:
								Console.Write (map [x, y]);
								break;
							}
						} 
						else  //mapVisible[x,y] == false
						{ 
							Console.Write (' ');
						}
					}
					c++;
				}
			}

			Console.WriteLine ();
			//Console.ReadLine ();

			/*tempKeyInfo = Console.ReadKey ();
			//Monster M = new Monster("",0,0,0,0);
			//string player_string_cords = p1.get_player_cords_s(map);
			Armor chest_armor_loot = new Armor ("", 0, 0);
			Weapon chest_weapon_loot = new Weapon ("", 0, 0);
			int [,] player_int_cords = p1.get_player_cords (map);
			bool item_looted = false;
			int levelsAdvanced = 0;

			string [] player_items = new string[20];
			//string [] player_items_worn = new string[6];
			for (int i = 0; i < 20; i++) {
				player_items [i] = "";
			}
			Container playerBackpack = new Container ("", 0, 0, player_items);
			playerBackpack = playerBackpack.initialise_container ();
			p1.cap -= playerBackpack.weight;
			int Coin = 0;
			bool canMove = p1.check_move (player_int_cords, map, tempKeyInfo);
			char item = ' ';

			SoundPlayer mainTheme = new SoundPlayer ();
			mainTheme.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "8bit Dungeon Music.wav";*/

			if (musicOn == true) 
			{
				var timer = new System.Threading.Timer (
				e => mainTheme.Play (),  
				null, 
				TimeSpan.Zero, 
				TimeSpan.FromMinutes (4));
			}
				

			do
			{
				ConsoleKeyInfo keyInfo = Console.ReadKey();
				if (keyInfo.Key == ConsoleKey.M)
				{

						readChar = g.genericKeyInput(3);

						switch (readChar)
						{
						case '0':
							playerBackpack.print_Backpack(playerBackpack);
							Console.WriteLine("\nPress the 'enter' key to exit the menu.");
							keyInfo = Console.ReadKey ();
							if (keyInfo.Key != ConsoleKey.Enter)
							{
								Thread.Sleep(5000);
							}
							break;

						case '1':
							Console.WriteLine();
							playerBackpack = playerBackpack.throw_away_items(playerBackpack);
							break;
							
						case '2':
							Console.WriteLine();
							SoundPlayer openingBackpack = new SoundPlayer();
							openingBackpack.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "openingBackpack.wav";
							openingBackpack.Play();
							player_items = p1.player_items_equiped(player_items,playerBackpack,p1);
							break;
							
						case '3':
							p1.print_worn_items(player_items);
							
							Console.WriteLine("\nPress the 'enter' key to exit the menu.");
							keyInfo = Console.ReadKey ();
							if (keyInfo.Key != ConsoleKey.Enter)
							{
								Thread.Sleep(5000);
							}	
							break;
							
						case '4':
							p1.printStats();

							Console.WriteLine("\nPress the 'enter' key to exit the menu.");
							keyInfo = Console.ReadKey ();
							if (keyInfo.Key != ConsoleKey.Enter)
							{
								Thread.Sleep(5000);
							}
							break;
							
							
						}
						/*Console.WriteLine ("Would you like to: \n>print backpack \n>throw away items \n>equip items \n>print worn items \n>print stats");
						do
						{
							Console.Write("\n\nUser:");
							response = Console.ReadLine();
						}while ( ( ((response != "throw away items") && (response != "throw away")) && ((response != "equip items") && (response != "equip")) && 
						          ((response != "print worn items") && (response != "worn items")) && ((response != "print stats") && (response != "stats"))   
						          && ((response != "print backpack") && (response != "backpack")) ));
						
						switch (response)
						{
						case "backpack": case "print backpack":
							playerBackpack.print_Backpack(playerBackpack);
							keyInfo = Console.ReadKey ();
							if (keyInfo.Key != ConsoleKey.Enter)
							{
								Thread.Sleep(5000);
							}
							break;
							
						case "throw away items": case "throw away":
							playerBackpack = playerBackpack.throw_away_items(playerBackpack);
							break;
							
						case "equip items": case "equip":
							SoundPlayer openingBackpack = new SoundPlayer();
							openingBackpack.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "openingBackpack.wav";
							openingBackpack.Play();
							player_items = p1.player_items_equiped(player_items,playerBackpack,p1);
							break;
						case "print worn items": case "worn items":
							p1.print_worn_items(player_items);
							keyInfo = Console.ReadKey ();
							if (keyInfo.Key != ConsoleKey.Enter)
							{
								Thread.Sleep(5000);
							}
							break;
						case "print stats": case "stats":
							p1.printStats();
							keyInfo = Console.ReadKey ();
							if (keyInfo.Key != ConsoleKey.Enter)
							{
								Thread.Sleep(5000);
							}
							break;
						}*/
					}


			player_string_cords = p1.get_player_cords_s(map);
			Console.WriteLine (player_string_cords); /*<--- for debugging only*/
			player_int_cords = p1.get_player_cords(map);
				
					if (((player_int_cords [0, 0] < 44) && (player_int_cords [1, 1] < 44))
						|| ((player_int_cords [0, 0] > 4) && (player_int_cords [1, 1] > 4))) {
				for (int e = player_int_cords [0, 0] - 4; e < player_int_cords [0, 0] + 4; e++) 
				{
					for (int f = player_int_cords [1, 1] - 4; f < player_int_cords [1, 1] + 4; f++) 
					{
						//assign visibleSquares array around player square visible = true;
						mapVisible[e,f] = squareIsVisible;
					}
				}
					}
					else
					{
					for (int e = player_int_cords [0, 0]; (e >= 0) && (e < 50); e++) {
						for (int f = player_int_cords [1, 1]; (f >= 0) && (f < 50); f++) {
							squareIsVisible = false;
							mapVisible [e, f] = squareIsVisible;
						}
					}
					}
			//Console.WriteLine("({0}, {1})",player_int_cords[0,0],player_int_cords[1,1]);  /*<--- for debugging only*/

			/*if (map[player_int_cords[0,0],player_int_cords[1,1]] == '*')
				{
					System.Environment.Exit (1);
				}*/
			canMove = p1.check_move(player_int_cords,map,keyInfo);
			/*if (canMove == true)
				{
					SoundPlayer footsteps = new SoundPlayer();
					footsteps.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "footsteps.wav";
					footsteps.Play();
					footsteps.Stop();
				}*/
				item = p1.move(canMove,map,player_int_cords,keyInfo);

				switch (item)
				{

				//case for 'B' (Boss) still needs to be implemented
				case 'M':
					skele.health = skele.resetHealth(skele);
					levelsAdvanced = D1.Fight(skele,p1,startHealthP,levelsAdvanced);
					skele.name = skele.generate_random_name();
					map[player_int_cords[0,0],player_int_cords[1,1]] = 'X';
					//Console.WriteLine(D1.monsters_left); //for debugging
					//Thread.Sleep(3000); //for debugging
					map = D1.spawn_stairs(map);
					map = D1.spawn_boss(map);
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
					item_looted = D1.Parse_Chest_Loot(Coin, chest_armor_loot,chest_weapon_loot,p1,playerBackpack);

					if (item_looted == true)
					{
						p1.cap = player_start_cap - playerBackpack.weight;
					}
					//Console.WriteLine("{0},{1}",player_int_cords[0,0],player_int_cords[1,1]); //for debugging
					map[player_int_cords[0,0],player_int_cords[1,1]] = 'X';


					Console.WriteLine ("\n\nWould you like to print your backpack (0), throw away (1) or equip items (2), print worn items (3) or stats (4)? (Answer with 'yes'/'no')");

					do
					{
						Console.Write("\n\nUser:");
						response = Console.ReadLine();
					}while ( ( (response != "yes") && (response != "Yes") && (response != "YES") ) && ( (response != "no") && (response != "No") && (response != "NO") ));

					if ((response == "yes") || (response == "Yes") || (response == "YES")) 
					{
						/*Console.WriteLine ("Would you like to: \n>print backpack \n>throw away items \n>equip items \n>print worn items \n>print stats");
						do
						{
							Console.Write("\n\nUser:");
							response = Console.ReadLine();
						}while ( ( (response != "throw away items") && (response != "equip items") && (response != "print worn items") ) && ( (response != "print stats") && (response != "print backpack") ));*/

						Console.Write ("User: ");
						keyInfo = Console.ReadKey ();
						Console.WriteLine();
						switch (keyInfo.Key)
						{
							case ConsoleKey.D0:
								playerBackpack.print_Backpack(playerBackpack);
								keyInfo = Console.ReadKey ();
								if (keyInfo.Key != ConsoleKey.Enter)
								{
									Thread.Sleep(5000);
								}
								break;

							case ConsoleKey.D1:
								playerBackpack = playerBackpack.throw_away_items(playerBackpack);
								break;

							case ConsoleKey.D2:
								SoundPlayer openingBackpack = new SoundPlayer();
								openingBackpack.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "openingBackpack.wav";
								openingBackpack.Play();
								player_items = p1.player_items_equiped(player_items,playerBackpack,p1);
								break;

							case ConsoleKey.D3:
								p1.print_worn_items(player_items);

								keyInfo = Console.ReadKey ();
								if (keyInfo.Key != ConsoleKey.Enter)
								{
									Thread.Sleep(5000);
								}	
								break;

							case ConsoleKey.D4:
								p1.printStats();
								keyInfo = Console.ReadKey ();
								if (keyInfo.Key != ConsoleKey.Enter)
								{
									Thread.Sleep(5000);
								}
								break;


						}
						switch (response)
						{
						case "print backpack": case "backpack":
													playerBackpack.print_Backpack(playerBackpack);
													keyInfo = Console.ReadKey ();
													if (keyInfo.Key != ConsoleKey.Enter)
													{
														Thread.Sleep(5000);
													}
													break;

						case "throw away items": case "throw away":
													playerBackpack = playerBackpack.throw_away_items(playerBackpack);
													break;

						case "equip items": case "equip":
													SoundPlayer openingBackpack = new SoundPlayer();
													openingBackpack.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "openingBackpack.wav";
													openingBackpack.Play();
													player_items = p1.player_items_equiped(player_items,playerBackpack,p1);
													break;
						case "print worn items": case "worn items":
													p1.print_worn_items(player_items);
												
													keyInfo = Console.ReadKey ();
													if (keyInfo.Key != ConsoleKey.Enter)
													{
														Thread.Sleep(5000);
													}	
													break;
						case "print stats": case "stats":
													p1.printStats();
													keyInfo = Console.ReadKey ();
													if (keyInfo.Key != ConsoleKey.Enter)
													{
														Thread.Sleep(5000);
													}
													break;
						}
					}
					break;
				
				case 'B':
					D1.boss_slain = true;
					/*Console.WriteLine(D1.monsters_left); 
					Thread.Sleep(3000);*/ //for debugging
					levelsAdvanced = D1.Boss_Fight(Boss,p1,startHealthP,levelsAdvanced);
					map[player_int_cords[0,0],player_int_cords[1,1]] = 'X';
					map = D1.spawn_stairs(map);
					break;
				case 'S':
					D1.Finish();
					break;
				 }
			c = 0;
			Console.Clear(); //causes flashing think of a better method
			//Console.WriteLine(D1.monsters_left); //for debugging
			
			/*foreach (string line in ConsoleReader.ReadFromBuffer(0, 0, (short)Console.BufferWidth, 50)) // TO BE IMPLEMENTED
			{
					//source: http://stackoverflow.com/questions/12355378/read-from-location-on-console-c-sharp
			}*/

				/*string path = @"c:\temp\MyTest.txt";
				
				try 
				{
					if (File.Exists(path)) 
					{
						File.Delete(path);
					}
					
					using (StreamWriter sw = new StreamWriter(path)) 
					{*/
						/*int j = 0;
						for (int x = 0; x < 50; x++) {
							
							for (int y = 0; y < 50; y++) {
								//c++;
								
								if (j % 50 == 0) {
									Console.WriteLine ();
									Console.Write (map [x, y]);
								} else {
									switch (map [x, y]) {
									case 'X':
										Console.ForegroundColor = ConsoleColor.Red;
										Console.Write ("X");
										m.reset_colours ();
										break;
									case 'M':
										Console.ForegroundColor = ConsoleColor.Magenta;
										Console.Write ("M");
										m.reset_colours ();
										break;
									case 'C':
										Console.ForegroundColor = ConsoleColor.Yellow;
										Console.Write ("C");
										m.reset_colours ();
										break;
									default:
										Console.Write (map [x, y]);
										break;
									}
								}
								j++;
							}
						}
					//}
					
					/*using (StreamReader sr = new StreamReader(path)) 
					{
						//This is an arbitrary size for this example.
						char[] chr = null;
						
						while (sr.Peek() >= 0) 
						{
							chr = new char[100];
							sr.Read(chr, 0, chr.Length);
							Console.WriteLine(chr);
						}
					}
				} 
				catch (Exception e) 
				{
					Console.WriteLine("The process failed: {0}", e.ToString());
				}*/

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
									if (mapVisible[x,y] == true) {
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
									else //mapVisible[x,y] == false
									{
										Console.Write (' ');
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

			Console.ReadLine();

			mainTheme.Stop();
		}
	}
}
