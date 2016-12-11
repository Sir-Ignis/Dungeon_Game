using System;
using System.IO;

namespace Players
{
	class Player
	{
		public string name = "";
		public int health = 0;
		public int mana = 0;
		public int defence = 0;
		public int attack = 0;
		public int xp = 0;
		public int xp_lvl = 1;
		public bool wielding_weapon = false;
		public string [] items_equiped = {"","","","","",""}; //6 empty string place holders for 6 different types of items

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
						items_worn[i] = "empty";
						break;
					
					case "Iron dagger":
						wielding_weapon = true;
						break;
				}
			}
			return items_worn;
		}

		public void save_starting_stats (string playerName, int hp, int mp, int def, int atk, int xp_s) //s = start
		{
			string [] lines = {"","","","","","","","","","",""};
			System.IO.File.WriteAllLines (@"/home/daniel/monodevelop/Dungeon_game/monsters/Monsters/Monsters/Start_Stats.txt", lines);
			
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
				}
			}

			using (System.IO.StreamWriter file = 
			       new System.IO.StreamWriter(@"/home/daniel/monodevelop/Dungeon_game/monsters/Monsters/Monsters/Start_Stats2.txt", true))
			foreach (string line in lines) 
			{
				file.WriteLine(line);
			}

		}

		public void quick_save (string playerName, int hp, int mp, int def, int atk, int xp_s) //for quick_save.txt
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
				}
			}
			
			using (System.IO.StreamWriter file = 
			       new System.IO.StreamWriter(@"/home/daniel/monodevelop/Dungeon_game/monsters/Monsters/Monsters/quick_save.txt", true))
				foreach (string line in lines) 
			{
				file.WriteLine(line);
			}
		}

		public int get_stats (int x)
		{
			Random rand = new Random ();
			int rand_hp = rand.Next (1, 20) + 50;
			int rand_mp = rand.Next (1, 10) + 25;
			int rand_df = rand.Next (1, 5) + 3;
			int rand_ak = rand.Next (1, 10) + 3;


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
				Console.Write ("{0}", attacks [i]);
			}

			do {
				Console.Write ("\nUser:");
				attack_ability = Console.ReadLine ();
			} while (( (attack_ability != "Punch") || (attack_ability != "punch") ) && ( (attack_ability != "Stab") || (attack_ability != "stab") ) && ( (attack_ability != "Kick") || (attack_ability != "kick") ) );

			if ((attack_ability == "Stab") && wielding_weapon == false) 
			{
				Console.WriteLine ("You cannot use this attack ability, you are not wielding a weapon.");
			}

			return attack_ability;
		}

		public int get_XP_for_next_level ()
		{
			double e = Math.E;
			int xp_to_lvl = 0;
			//formula for lvl = n[10+(e*n)]^2 , where n = level
			xp_to_lvl =  Convert.ToInt32(Math.Truncate( xp_lvl * ((10 + (e * xp_lvl) ) * (10 + (e * xp_lvl) )) )) - xp;
			return xp_to_lvl;
		}

		public Player (string itsName, int itsHealth, int itsMana, int itsDefence, int itsAttack,int itsXP)
		{
			xp = itsXP;
		}
	}
}

