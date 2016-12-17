using System;
using System.IO;
using System.Threading;
using Dungeons;
using Dungeon_Game;
using MainMenu;

namespace Players
{
	class Player
	{
		Menu m = new Menu();
		public string name = "";
		public int health = 0;
		public int mana = 0;
		public int defence = 0;
		public int attack = 0;
		public int xp = 0;
		public int xp_lvl = 1;
		public int cap = 180;
		public bool wielding_weapon = false;
		public string [] items_equipped = {"","","","","",""}; //6 empty string place holders for 6 different types of items

		/*ITEMS EQUIPPED [x]* ITEM
		 * 0				* Helmet
		 * 1				* Chestpiece
		 * 2				* Weapon
		 * 3				* Shield
		 * 4				* Legs
		 * 5				* Shoes
		 */

		public Random rand = new Random();

		public string get_player_cords_s (char [,] itsMap) //gives cords in (x,y) print out, s = string
		{
			string playerCords = "(";
			for (int x = 0; x < 50; x++) 
			{
				
				for (int y = 0; y < 50; y++) 
				{
					if (itsMap[x,y] == 'X')
					{
						playerCords = playerCords + x + ", " + y + ')';
					}
				}
			}
			return playerCords;
		}

		public int [,] get_player_cords (char [,] itsMap)
		{
			int [,] cords = new int [2, 2];

			//for (int i = 48;
			for (int x = 0; x < 50; x++) {
				
				for (int y = 0; y < 50; y++) 
				{
					if (itsMap [x, y] == 'X') 
					{
						cords [0, 0] = x; //x coordinate
						cords [1, 1] = y; //y coordinate
					}
				}
			}
			return cords;
		}

		public bool check_move (int[,] itsParsedPlayerCords, char[,] itsMap, ConsoleKeyInfo keyInfo)
		{
			/*if ( (itsMap[playerX,playerY+1] != '.') || (itsMap[playerX+1,playerY] != '.') || (itsMap[playerX+1,playerY-1] != '.') || (itsMap[playerX-1,playerY] != '.') )
				{
					switch (itsMap[playerX,playerY+1])
					{
						//in order of decreasing likelyhood 
						case 'M': //monster
								canMove = true;
								break;
						case 'C': //chest
								canMove = true;
								break;
						case '#': //wall
								canMove = false;
								break;
						case 'S': //stairs
								canMove = true;
								break;
					}
					switch (itsMap[playerX+1,playerY])
					{
						//in order of decreasing likelyhood 
						case 'M': //monster
								canMove = true;
								break;
						case 'C': //chest
								canMove = true;
								break;
						case '#': //wall
								canMove = false;
								break;
						case 'S': //stairs
								canMove = true;
								break;
					} //<-- old idea
				}*/
			bool canMove = true;
			int playerX = itsParsedPlayerCords [0, 0];
			int playerY = itsParsedPlayerCords [1, 1];

			if ((itsMap [playerX, playerY + 1] != '.') || (itsMap [playerX + 1, playerY] != '.') || (itsMap [playerX, playerY - 1] != '.') || (itsMap [playerX - 1, playerY] != '.')) {
				/*if (itsMap [playerX, playerY + 1] == '#')
				{
					canMove = false;
				}
				else if (itsMap [playerX + 1, playerY] == '#')
				{
					canMove = false;
				}
				else if (itsMap [playerX, playerY - 1] == '#')
				{
					canMove = false;
				}
				else if (itsMap [playerX - 1, playerY] == '#')
				{
					canMove = false;
				}*/

				//itsPlayerCords[0,0] = itsPlayerCords[0,0] - 1; moves up (on output)
				//itsPlayerCords[1,1] = itsPlayerCords[1,1] + 1; moves right (on output)
				//itsPlayerCords[0,0] = itsPlayerCords[0,0] + 1; moves down (on output)
				//tsPlayerCords[1,1] = itsPlayerCords[1,1] - 1; moves left (on output)

				if (keyInfo.Key == ConsoleKey.UpArrow) 
				{
					switch (itsMap [playerX - 1, playerY]) 
					{
					case '.':
						canMove = true;
						break;

					case '#':
						canMove = false;
						break;
					case 'C':
						canMove = true;
						break;
				
					case 'M':
						canMove = true;
						break;
				
					case 'S':
						canMove = true;
						break;
					case '*':
						canMove = false;
						break;

					}
				} 

				else if (keyInfo.Key == ConsoleKey.DownArrow) 
				{
					switch (itsMap [playerX + 1, playerY]) 
					{
					case '.':
						canMove = true;
						break;
					case '#':
						canMove = false;
						break;
					case 'C':
						canMove = true;
						break;
					
					case 'M':
						canMove = true;
						break;
					
					case 'S':
						canMove = true;
						break;
					case '*':
						canMove = false;
						break;
					}

				} 

				else if (keyInfo.Key == ConsoleKey.LeftArrow) 
				{

					switch (itsMap [playerX, playerY - 1]) 
					{
					case '.':
						canMove = true;
						break;
					case '#':
						canMove = false;
						break;
					case 'C':
						canMove = true;
						break;
					
					case 'M':
						canMove = true;
						break;
					
					case 'S':
						canMove = true;
						break;
				
					case '*':
						canMove = false;
						break;
					}

				} 

				else if (keyInfo.Key == ConsoleKey.RightArrow) 
				{
					switch (itsMap [playerX, playerY + 1]) 
					{
					case '.':
						canMove = true;
						break;
					case '#':
						canMove = false;
						break;
					case 'C':
						canMove = true;
						break;
					
					case 'M':
						canMove = true;
						break;
					
					case 'S':
						canMove = true;
						break;
				
					case '*':
						canMove = false;
						break;

					}
				}

				
				else if ((keyInfo.Key != ConsoleKey.UpArrow) && (keyInfo.Key != ConsoleKey.RightArrow) && (keyInfo.Key != ConsoleKey.DownArrow) && (keyInfo.Key != ConsoleKey.LeftArrow)) 
				{
					Console.WriteLine ("You cannot move in this direction.");
				}
			}
			//Console.WriteLine(canMove); //for debugging
			return canMove;
		}

		public char move (bool canMove, char[,] itsMap, int[,] itsPlayerCords,ConsoleKeyInfo keyInfo)
		{
			char item = ' ';
			//ConsoleKeyInfo keyInfo = Console.ReadKey ();
			if (keyInfo.Key == ConsoleKey.UpArrow) {
				if (canMove == true) {
					if (itsMap [itsPlayerCords [0, 0] - 1, itsPlayerCords [1, 1]] == '.') {
						itsMap [itsPlayerCords [0, 0], itsPlayerCords [1, 1]] = '.';
						itsPlayerCords [0, 0] = itsPlayerCords [0, 0] - 1; //moves up (on output)
						itsMap [itsPlayerCords [0, 0], itsPlayerCords [1, 1]] = 'X';
					} else {
						item = itsMap [itsPlayerCords [0, 0] - 1, itsPlayerCords [1, 1]];
						itsMap [itsPlayerCords [0, 0], itsPlayerCords [1, 1]] = '.';
						itsPlayerCords [0, 0] = itsPlayerCords [0, 0] - 1;
						itsMap [itsPlayerCords [0, 0], itsPlayerCords [1, 1]] = '*';
					}
				}
			} else if (keyInfo.Key == ConsoleKey.RightArrow) {
				if (canMove == true) {
					if (itsMap [itsPlayerCords [0, 0], itsPlayerCords [1, 1] + 1] == '.') {
						itsMap [itsPlayerCords [0, 0], itsPlayerCords [1, 1]] = '.';
						itsPlayerCords [1, 1] = itsPlayerCords [1, 1] + 1; //moves right (on output)
						itsMap [itsPlayerCords [0, 0], itsPlayerCords [1, 1]] = 'X';
					} else {
						//item = itsMap [itsPlayerCords [0, 0] - 1, itsPlayerCords [1, 1]]; <-- before
						item = itsMap [itsPlayerCords [0, 0], itsPlayerCords [1, 1]+1]; //after
						itsMap [itsPlayerCords [0, 0], itsPlayerCords [1, 1]] = '.';
						itsPlayerCords [1, 1] = itsPlayerCords [1, 1] + 1;
						itsMap [itsPlayerCords [0, 0], itsPlayerCords [1, 1]] = '*';
					}
				}
			} else if (keyInfo.Key == ConsoleKey.DownArrow) {
				if (canMove == true) {
					if (itsMap [itsPlayerCords [0, 0] + 1, itsPlayerCords [1, 1]] == '.') {
						itsMap [itsPlayerCords [0, 0], itsPlayerCords [1, 1]] = '.';
						itsPlayerCords [0, 0] = itsPlayerCords [0, 0] + 1;//moves down (on output)
						itsMap [itsPlayerCords [0, 0], itsPlayerCords [1, 1]] = 'X';
					} else {
						//item = itsMap [itsPlayerCords [0, 0] - 1, itsPlayerCords [1, 1]]; <-- before
						item = itsMap [itsPlayerCords [0, 0] + 1, itsPlayerCords [1, 1]]; //after
						itsMap [itsPlayerCords [0, 0], itsPlayerCords [1, 1]] = '.';
						itsPlayerCords [0, 0] = itsPlayerCords [0, 0] + 1;//moves down (on output)
						itsMap [itsPlayerCords [0, 0], itsPlayerCords [1, 1]] = '*';
					}
				}
			} else if (keyInfo.Key == ConsoleKey.LeftArrow) {
				if (canMove == true) {
					if (itsMap [itsPlayerCords [0, 0], itsPlayerCords [1, 1] - 1] == '.') {
						itsMap [itsPlayerCords [0, 0], itsPlayerCords [1, 1]] = '.';
						itsPlayerCords [1, 1] = itsPlayerCords [1, 1] - 1; //moves left (on output)
						itsMap [itsPlayerCords [0, 0], itsPlayerCords [1, 1]] = 'X';
					} else {
						//item = itsMap [itsPlayerCords [0, 0] - 1, itsPlayerCords [1, 1]]; <-- before
						item = itsMap [itsPlayerCords [0, 0], itsPlayerCords [1, 1]-1]; //after
						itsMap [itsPlayerCords [0, 0], itsPlayerCords [1, 1]] = '.';
						itsPlayerCords [1, 1] = itsPlayerCords [1, 1] - 1; //moves left (on output)
						itsMap [itsPlayerCords [0, 0], itsPlayerCords [1, 1]] = '*';
					}
				}
			} 
			return item;
		}

		public string get_PlayerClass() //still needs to be implemented...
		{
			string [] Classes = {"Knight","Wizard","Archer"};
			string current_class = "";
			
			Console.WriteLine ("Which class would you like to be ? ");
			for (int i = 0; i < Classes.Length; i++)
			{
				current_class = Classes[i];
				Console.Write ("{0} ",current_class);
			}
			
			do 
			{
				Console.Write ("\nUser: ");
				current_class = Console.ReadLine();
			}
			while( ( (current_class != "Knight") || (current_class != "knight") ) && ( (current_class != "Wizard") || (current_class != "wizard") ) &&  ( (current_class != "Archer") || (current_class != "archer") ) );
			
			return current_class;
		}

		public string [] player_items_equiped (string[] items_worn)
		{
			/*helmet value = 1;
			 *weapon value = 2;
			 *armor value = 4;
			 *shield value = 8;
			 *trousers value = 16;
			 *shoes value = 32;
			*/

			for (int i = 0; i < items_worn.Length; i++) 
			{
				switch (items_worn[i])
				{
					case "": //blank
						items_worn[i] = "nothing";
						break;
					
					case "Iron dagger":
						wielding_weapon = true;
						break;
				}
			}
			return items_worn;
		}

		public void print_worn_items (string[] items_worn)
		{
			string slot_item = "";

			Console.Write ("You are wearing a ");
			for (int i = 0; i < items_worn.Length; i++) 
			{
				slot_item = items_worn[i];

				if (i < items_worn.Length - 1)
				{
					Console.Write ("{0}, ",slot_item);
				}
				else //last item
				{
					Console.WriteLine ("{0} .",slot_item);
				}
			}
		}

		public void save_starting_stats (string playerName, int hp, int mp, int def, int atk, int xp_s, string[] items_equipped) //s = start
		{
			string [] lines = {"","","","","","","","","","","","","","",""};
			//System.IO.File.WriteAllLines (@"/home/daniel/monodevelop/Dungeon_game/monsters/Monsters/Monsters/Start_Stats.txt", lines);
			
			for (int i = 0; i < lines.Length; i++) {
				switch (i) {
				case 1:
					lines [i] = "Name: " + playerName;
					break;
				case 2:
					lines [i] = "Health: " + hp;
					break;
				case 3:
					lines [i] = "Mana: " + mp;
					break;
				case 4:
					lines [i] = "Defence: " + def;
					break;
				case 5:
					lines [i] = "Attack: " + atk;
					break;
				case 6:
					lines [i] = "Experience: " + xp_s;
					break;
				case 7: 
					lines [i] = "Items equipped: ";
					for (int j = 0; j < items_equipped.Length; j++) {
						lines [i] = lines [i] + " " + items_equipped [j];
					}

					break;
				}
			}

			string homePath = (Environment.OSVersion.Platform == PlatformID.Unix || 
				Environment.OSVersion.Platform == PlatformID.MacOSX)
				? Environment.GetEnvironmentVariable ("HOME")
					: Environment.ExpandEnvironmentVariables ("%HOMEDRIVE%%HOMEPATH%");

			if (Environment.OSVersion.Platform == PlatformID.Unix) //linux
			{
				using (System.IO.StreamWriter file = 
				       new System.IO.StreamWriter(@"/home/daniel/monodevelop/Dungeon_Game_V2/Dungeon_game/monsters/Monsters/Monsters/Start_Stats2.txt", true))
					
					foreach (string line in lines) 
				{
					file.WriteLine (line);
				}
			}

			else //windows
			{
				string fileName = "save.txt";
				string pathString = System.IO.Path.Combine(homePath, fileName);
				using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathString,true))
				foreach (string line in lines) 
				{
					file.WriteLine (line);
				}
					
			} 
		}

		public void quick_save (string playerName, int hp, int mp, int def, int atk, int xp_s, string [] items_equipped) //for quick_save.txt
		{
			string [] lines = {"","","","","","","","","","",""};
			for (int i = 0; i < lines.Length; i++) {
				switch (i) {
				case 1:
					lines [i] = "Name: " + playerName;
					break;
				case 2:
					lines [i] = "Health: " + hp;
					break;
				case 3:
					lines [i] = "Mana: " + mp;
					break;
				case 4:
					lines [i] = "Defence: " + def;
					break;
				case 5:
					lines [i] = "Attack: " + atk;
					break;
				case 6:
					lines [i] = "Experience: " + xp_s;
					break;
				case 7: 
					lines [i] = "Items equipped: ";
					for (int j = 0; j < items_equipped.Length; j++)
					{
						lines [i] = lines [i] + " "+ items_equipped[j];
					}
					
					break;
				}
			}
			
			using (System.IO.StreamWriter file = 
			       new System.IO.StreamWriter(@"/home/daniel/monodevelop/Dungeon_Game_V2/Dungeon_game/monsters/Monsters/Monsters/quick_save.txt", true))
				foreach (string line in lines) 
			{
				file.WriteLine(line);
			}
		}

		public int get_stats (int x)
		{
			int rand_hp = rand.Next (1, 20) + 50;
			int rand_mp = rand.Next (1, 10) + 25;
			int rand_df = rand.Next (1, 6) + 2;
			int rand_ak = rand.Next (1, 6) + 2;


				switch (x) 
				{
					case 1:
						return rand_hp;

					case 2:
						return rand_mp;

					case 3:
						return rand_df;

					case 4:
						return rand_ak;
				}

			return 0;
		}

		//get_attack must be implemented after item slots function is implemented

		public string get_attack (int mana)
		{
			string [] attacks = {"Punch","Stab","Kick"};
			string attack_ability = "";

			Console.WriteLine ("Which attack would you like to use?");
			Console.Write ("Attacks: ");

			for (int i = 0; i < attacks.Length; i++) {
				Console.Write ("{0} ", attacks [i]);
			}

			do {
				Console.Write ("\nUser:");
				attack_ability = Console.ReadLine ();
			} while (( (attack_ability != "Punch") && (attack_ability != "punch") ) && ( (attack_ability != "Stab") && (attack_ability != "stab") ) && ( (attack_ability != "Kick") && (attack_ability != "kick") ) );

			if ( ((attack_ability == "Stab") || (attack_ability == "stab")) && (wielding_weapon == false)) 
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine ("\nYou cannot use this attack ability, you are not wielding a weapon.");
				m.reset_colours();
				do
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine ("You can only use the attack abilities 'Punch' or 'Kick'.\n");
					m.reset_colours();
					Console.Write ("User: ");
					attack_ability = Console.ReadLine();
				}while ( (attack_ability != "Punch") && (attack_ability != "punch") && ( (attack_ability != "Kick") && (attack_ability != "kick")));
			}
			Console.WriteLine ();
			return attack_ability;
		}

		
		public int get_attack_damage (int attack, string attack_ability)
		{
			int attack_damage = 0;
			switch (attack_ability)
			{
				case "Punch":
							 attack_damage = attack + rand.Next(0,2);
							 break;
				case "Stab":
							attack_damage = attack + rand.Next (2,5);
							break;
				case "Kick":
							attack_damage = attack + rand.Next (0,1);
							break;
				case "punch":
							goto case "Punch";
				case "stab":
							goto case "Stab";
				case "kick":
							goto case "Kick";
			}
			
			return attack_damage;
		}
		
		public int get_XP_for_next_level ()
		{
			double e = Math.E;
			int xp_to_lvl = 0;
			//formula for lvl = n[10+(e*n)]^2 , where n = level
			xp_to_lvl =  Convert.ToInt32(Math.Truncate( xp_lvl * ((10 + (e * xp_lvl) ) * (10 + (e * xp_lvl) )) )) - xp;
			return xp_to_lvl;
		}

		public Player (string itsName, int itsHealth, int itsMana, int itsDefence, int itsAttack,int itsXP, int itsCap)
		{
			name = itsName;
			health = itsHealth;
			mana = itsMana;
			defence = itsDefence;
			attack = itsAttack;
			xp = itsXP;
			cap = itsCap;
		}
	}
}

