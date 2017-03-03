using System;
using Sound;
using Items;
using Monsters;
using Dungeons;
using DungeonMap;
using Combat;
using Players;
using MainMenu;
using System.Threading;
using System.Diagnostics;
using System.IO;
using genericFunctions;
using System.Media;

namespace Dungeon_Game
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.CursorVisible = false;
			Random rand = new Random ();
			Menu m = new Menu ();
			Console.Clear ();
			m.printWelcome ();
			Thread.Sleep (5000); //<--- remove when coding
			Music menuMusic = new Music ("Dungeon_Ambience_Music.wav");
			SFX sfx = new SFX ();
		/*
			SoundPlayer sp = new SoundPlayer ();
			sp.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Blip_Select.wav";
			sp.Play();*/

			menuMusic.playMusic ();

			string response = "";
			string gameMode = string.Empty;

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
				sfx.optionSelected();

			switch (readChar) 
			{
			case 'S':
				readChar = g.genericKeyInput(1);
					if (readChar == 'N')
					{
						gameMode = "normal";
					}
					else //readChar == 'A'
					{
						gameMode = "admin";
					}
				Console.Clear ();
				break;
			
			case 'H':
				m.printHelp();
				Console.WriteLine ("\nPress any key to return to the main menu...");
				Console.ReadKey();
				sfx.optionSelected();
				Console.Clear();
				break;
			case 'E':
				Thread.Sleep(1000);
				Console.Clear();
				menuMusic = null;
				sfx = null;
				GC.Collect();
				System.Environment.Exit (1);
				break;
			
			case 'C':
					m.print_credits();
					Console.WriteLine ("\nPress any key to return to the main menu...");
					Console.ReadKey();
					sfx.optionSelected();
					Console.Clear();
					break;
			}
			} while ((readChar != 'S') && (readChar != 'N') && (readChar != 'A') && (readChar != 'E'));

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
			FleshEatingSpider spider = new FleshEatingSpider ();
			spider.name = "Flesh Eating Spider";
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

			int player_starting_cap = p1.cap;
		
			Map map = new Map ();
			Dungeon D1 = new Dungeon (map.cMap);
			DungeonFight Fight = new DungeonFight ();
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

			Console.WriteLine ("\nPress any key to continue...");
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

			Torch.torchLight = 5;
			//int torchesFound = 1; //FIXME

			string admin_password = "20H4CKER17";
			if (gameMode == "admin") {
				Console.Write ("\n\n\nAdmin password:");
				response = Console.ReadLine ();

				if (response == admin_password) {
					//int l = 0;
					p1.attack = 1000;

					ScreenBuffer.Draw (sMap, 0, 0);
					ScreenBuffer.DrawScreen ();

					map.c_monsters_left = 0;
					Torch.torchLight = 50;
					int c = 0;
					for (int r = 0; r < 50; r++) {
						for (int s = 0; s < 50; s++) {
							if (lMap [r, s] == 'M') {
								lMap [r, s] = '.';
								sMap += lMap [r, s];
							}
							if (t % 50 == 0) {
								sMap += '\n';
								sMap += lMap[r,s];
							}
							c++;
						}
					}
					Console.Clear ();
				}
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
			ScreenBuffer.setBufferSize(50,70);
			int monsterRoll = 0;

			do //Main "Game Loop" starts here
			{
				if ((map.c_monsters_left == 0) && (map.bosses_spawned == 0))
				{
					map.bosses_spawned += 1;
					map.spawn_boss(map.cMap);
				}
				for (int l = 0; l < 50; l++) {
					for (int k = 0; k < 50; k++) {
						squareIsVisible = false;
						tempMapVisible [k, l] = squareIsVisible;
					}
				}

				{
					for (int e = (player_int_cords [0, 0] - Torch.torchLight); e < (player_int_cords [0, 0] + Torch.torchLight); e++) {
						for (int f = (player_int_cords [1, 1] - Torch.torchLight); f < (player_int_cords [1, 1] + Torch.torchLight); f++) {
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
				//ScreenBuffer.DrawScreen();
				map.draw_map(sb);

				/*for (int iny = 0; iny < 50; iny++)
				{
					Console.Write("\n");
				}*/

				Console.SetCursorPosition(0,55);
				Console.Write ("\nUser: ");
				ConsoleKeyInfo keyInfo = Console.ReadKey();
				if (keyInfo.Key == ConsoleKey.M)
				{
						Console.WriteLine("\n\n\n");
						readChar = g.genericKeyInput(3);
						sfx.optionSelected();
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
							playerBackpack = playerBackpack.throw_away_items(playerBackpack, p1); //need to fix bug by adding error checking
							p1.cap = player_starting_cap - playerBackpack.weight; //FIXME cap doesn't update

							Console.WriteLine("\nPress the 'enter' key to exit the menu.");
							keyInfo = Console.ReadKey ();
							if (keyInfo.Key != ConsoleKey.Enter)
							{
								Thread.Sleep(10000);
							}
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
							Thread.Sleep(1000);
							menuMusic = null;
							sfx = null;
							mainTheme = null;
							GC.Collect();
							Console.Clear();
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
					monsterRoll = rand.Next(1,6);

					if (monsterRoll == 5) //spawn flesh eating spider
					{
						spider.health = spider.resetHealth(spider); //FIXME does not always reset health
						levelsAdvanced = Fight.FightSpider(spider, p1, startHealthP, levelsAdvanced, sb);
					}
					else //spawn skeleton
					{
					skele.health = skele.resetHealth(skele);
					levelsAdvanced = Fight.FightMonster(skele,p1,startHealthP,levelsAdvanced,sb);
					skele.name = skele.generate_random_name();
					}
					lMap[player_int_cords[0,0],player_int_cords[1,1]] = 'X';

					if (map.c_monsters_left == 0)
					{
					lMap = map.spawn_boss(lMap);
					}
					Console.Clear();
					break;
					
				case 'C':
					Console.Clear(); //temporary solution find a better one
					ScreenBuffer.Draw(sMap,0,0);
					ScreenBuffer.DrawScreen();
					Console.WriteLine();
					Coin = rand.Next (1,3);

					if (Coin == 1) //armour
					{
						chest_armor_loot = D1.Chest_Loot_Armor();
					}
					else if (Coin == 2)
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
					levelsAdvanced = Fight.Boss_Fight(Boss,p1,startHealthP,levelsAdvanced);
					map.boss_slain = true;
					//map.spawn_stairs(map.cMap);
					lMap[player_int_cords[0,0],player_int_cords[1,1]] = 'X';
					lMap = map.spawn_stairs(lMap);
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
