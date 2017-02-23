using System;
using Sound;
using Items;
using Monsters;
using Dungeons;
using DungeonMap;
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
			Console.CursorVisible = false;
			Random rand = new Random ();
			Menu m = new Menu ();
			Music menuMusic = new Music ("Dungeon_Ambience_Music.wav");
			menuMusic.playMusic ();

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
			
			case 'C':
					m.print_credits();
					Console.WriteLine ("\nPress any key to return to the main menu...");
					Console.ReadKey();
					Console.Clear();
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
			menuMusic = null;
			GC.Collect ();

			m.loading(); //<--- remove when coding
			Console.Clear ();

			m.print_cave();

			Skeleton skele = new Skeleton ();
			Wraith Boss = new Wraith ();

			const int player_start_cap = 180;
			Player p1 = new Player ("Default Player", 0, 0, 0, 0, 0, player_start_cap);
			p1.cap = player_start_cap;

			string nameP = p1.name = System.Security.Principal.WindowsIdentity.GetCurrent ().Name;

			p1.xp = 1;
			int startHealthP = p1.health;
			Console.WriteLine ("\nWelcome {0} !", nameP);

			Console.WriteLine ("A player called {0} with:\n" +
				"\n{1} HP" +
				"\n{2} MP" +
				"\n{3} DEF" +
				"\n{4} ATK" +
				"\n{5} XP" +
				"\nhas been created...\n", p1.name, p1.health, p1.mana, p1.defence, p1.attack, p1.xp); //P = Player

		
			Map map = new Map ();
			Dungeon D1 = new Dungeon (map.cMap);
			char [,] lMap = map.cMap;
			string sMap = string.Empty; // here

			int t = 0;
			for (int r = 0; r < 50; r++) {
				for (int s = 0; s < 50; s++) {
					if (t % 50 == 0) {
						sMap += '\n';
						sMap += lMap[r,s];
					} else {
						sMap += lMap[r,s];
					}
					t++;
				}
			}
				
			int [,] player_int_cords = p1.get_player_cords (lMap);
			ScreenBuffer sb = new ScreenBuffer ();
			ScreenBuffer.setCursorPosition (0, 0);
			tempKeyInfo = Console.ReadKey ();
			Armor chest_armor_loot = new Armor ("", 0, 0);
			Weapon chest_weapon_loot = new Weapon ("", 0, 0);
			bool item_looted = false;
			int levelsAdvanced = 0;

			string [] player_items = new string[20];
			for (int i = 0; i < 20; i++) {
				player_items [i] = "";
			}

			Container playerBackpack = new Container ("", 0, 0, player_items);
			Utility Torch = new Utility ("Torch", 5, true);
			playerBackpack = playerBackpack.initialise_container ();
			playerBackpack = playerBackpack.parse_Backpack(Torch,playerBackpack);
			p1.cap -= playerBackpack.weight;
			int Coin = 0;
			bool canMove = p1.check_move (player_int_cords, lMap, tempKeyInfo);
			char item = ' ';

			Music mainTheme = new Music("8bit Dungeon Music.wav");

			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine ("MAP");
			m.reset_colours ();

			string admin_password = "20H4CKER17";
			
			Console.Write ("\n\n\nAdmin password:");
			response = Console.ReadLine ();

			if (response == admin_password) {
				//int l = 0;
				p1.attack = 100;

				ScreenBuffer.Draw (sMap, 0, 0);
				ScreenBuffer.DrawScreen ();

				D1.monsters_left = 0;
				lMap = D1.spawn_boss (lMap);
				Console.Clear();
			}

			//prints map
			//int torchLight = 5;

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

			int torchLight = 5;

			bool squareIsVisible = true;
			bool[,] tempMapVisible = new bool[50, 50];
		//	int lightsDistanceFromXBoundary = 0;
			//int lightsDistanceFromYBoundary = 0;


			Console.WriteLine ();


			if (musicOn == true) 
			{
				var timer = new System.Threading.Timer (
					e => mainTheme.playMusic(),  
				null, 
				TimeSpan.Zero, 
				TimeSpan.FromMinutes (4));
			}
				
			Console.Clear ();
			//ScreenBuffer bufferN = new ScreenBuffer ();
			do //Main "Game Loop" starts here
			{
				for (int l = 0; l < 50; l++) {
					for (int k = 0; k < 50; k++) {
						squareIsVisible = false;
						tempMapVisible [k, l] = squareIsVisible;
					}
				}

				{
					for (int e = (player_int_cords [0, 0] - torchLight); e < (player_int_cords [0, 0] + torchLight); e++) {
						for (int f = (player_int_cords [1, 1] - torchLight); f < (player_int_cords [1, 1] + torchLight); f++) {
							//assign visibleSquares array around player square visible = true;
							try {
								squareIsVisible = true;
								tempMapVisible [e,f] = squareIsVisible;
							}
							catch (IndexOutOfRangeException er) {
								//squareIsVisible = true;
								continue;
							}
						}
					}
				}
				sMap = "";
				t = 0;
				for (int r = 0; r < 50; r++) {
					for (int s = 0; s < 50; s++) {
						if (t % 50 == 0) {
							sMap += '\n';
							sMap += lMap[r,s];
						}
						else if (tempMapVisible[r,s] == true)
						{
							sMap += lMap[r,s];
						}
						else if (tempMapVisible[r,s] == false)
						{
							sMap += ' ';
						}
						t++;
					}
				}

				//Console.WriteLine(sMap);

				ScreenBuffer.Draw(sMap,0,0);
				ScreenBuffer.DrawScreen();

				Console.Write ("\n\nUser: ");
				ConsoleKeyInfo keyInfo = Console.ReadKey();
				if (keyInfo.Key == ConsoleKey.M)
				{
						Console.WriteLine("\n\n\n");
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
							SoundEffects backpack = new SoundEffects("openingBackpack.wav");
							backpack.playSFX();
							backpack = null;
							GC.Collect ();
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
							
						case '5':
							System.Environment.Exit (1);
							break;
							
						}
					Console.Clear();
				}
						
			player_int_cords = p1.get_player_cords(lMap);
				
			canMove = p1.check_move(player_int_cords,lMap,keyInfo);

			item = p1.move(canMove,lMap,player_int_cords,keyInfo);
									
				switch (item)
				{
				case 'M':
					skele.health = skele.resetHealth(skele);
					levelsAdvanced = D1.Fight(skele,p1,startHealthP,levelsAdvanced,sb);
					skele.name = skele.generate_random_name();
					lMap[player_int_cords[0,0],player_int_cords[1,1]] = 'X';
					lMap = D1.spawn_stairs(lMap);
					lMap = D1.spawn_boss(lMap);
					Console.Clear();
					break;
					
				case 'C':
					Console.WriteLine();
					Coin = rand.Next (1,3);

					if (Coin == 1) //armour
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
					lMap[player_int_cords[0,0],player_int_cords[1,1]] = 'X';
					Console.WriteLine();
					break;

					
				case 'B':
					D1.boss_slain = true;
					levelsAdvanced = D1.Boss_Fight(Boss,p1,startHealthP,levelsAdvanced);
					lMap[player_int_cords[0,0],player_int_cords[1,1]] = 'X';
					lMap = D1.spawn_stairs(lMap);
					break;
				case 'S':
					D1.Finish();
					break;
				 }
			//c = 0;
			}while (true);
		}
	}
}
