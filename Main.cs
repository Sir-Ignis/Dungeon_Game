using System;
using Items;
using Monsters;
using Dungeons;
using Players;
using MainMenu;

namespace Dungeon_Game
{

	class MainClass
	{
		public static void Main (string[] args)
		{
			Menu m = new Menu ();
			m.print_Menu ();

			Console.Write ("USER: ");
			string response = Console.ReadLine ();

			if ((response == "exit") || (response == "Exit") || (response == "EXIT")) {
				System.Environment.Exit (1);
			}
			
			Skeleton skele = new Skeleton ("Default Skeleton", 60, 10, 10, 5);

			string nameM = skele.name = "Bob";
			int healthM = skele.health = 30;
			int manaM = skele.mana = 5;
			int defenceM = skele.defence = 0;
			int attackM = skele.attack = 10;

			Console.WriteLine ("A skeleton called {0} with:" +
				"\n{1} HP" +
				"\n{2} MP" +
				"\n{3} DEF" +
				"\n{4} ATK" +
				"\nhas been created...", nameM, healthM, manaM, defenceM, attackM); //M = Monster

			Player p1 = new Player ("Default Player", 0, 0, 0, 0, 0);

			string nameP = p1.name = System.Security.Principal.WindowsIdentity.GetCurrent ().Name;
			int healthP = p1.health = 30;
			int manaP = p1.mana = 5;
			int defenceP = p1.defence = 0;
			int attackP = p1.attack = 10;
			int xpP = p1.xp;
			int xp_to_lvl_main = p1.get_XP_for_next_level ();

			Console.WriteLine ("\nWelcome {0} !", nameP);

			for (int j = 0; j < 4; j++) { //cycles through and assigns local player stats random values from the Player get_stats function
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
					attackM = p1.attack = p1.get_stats (j);
					break;

				}
			}

			Console.WriteLine ("A player called {0} with:" +
				"\n{1} HP" +
				"\n{2} MP" +
				"\n{3} DEF" +
				"\n{4} ATK" +
				"\n{5} XP" +
				"\nhas been created...\n", nameP, healthP, manaP, defenceP, attackP, xpP); //P = Player

			int dmg_dealtP = 0;
			int dmg_takenM = 0;

			int dmg_dealtM = 0;
			int dmg_takenP = 0;

			Dungeon D1 = new Dungeon ();
			
			int xp_gained = Convert.ToInt32 (D1.getPlayer_XP (healthM, defenceP, attackM));

			p1.save_starting_stats (nameP, healthP, manaP, defenceP, attackP, xpP);

			string monster_loot = D1.generate_loot ();

			Weapon Iron_dagger = new Weapon ("Iron dagger", 5, 10);
			Armor Leather_cowl = new Armor ("Leather cowl", 2, 2);
			Armor Wooden_shield = new Armor ("Wooden shield", 12, 15);
			Armor Leather_trousers = new Armor ("Leather trousers", 5, 3);
			Armor Leather_shoes = new Armor ("Leather shoes", 1, 3);
			Armor Leather_armor = new Armor ("Leather armor", 5, 10);

			Console.WriteLine ("\nFIGHT!\n");
			do {
				//calculates damage dealt by player and health of the monster as a result of the player's attack
				dmg_dealtP = D1.getPlayer_Damage (attackP);
				dmg_takenM = D1.damage_TakenM (dmg_dealtP, defenceM);
				healthM = skele.health -= dmg_takenM;

				//calculates damage dealt by monster and health of the player as a result of the monster's attack
				dmg_dealtM = D1.getMonster_Damage (attackM);
				dmg_takenP = D1.damage_TakenM (dmg_dealtM, defenceP);
				healthP = p1.health -= dmg_takenP;

				//Console.WriteLine ("{0} lost {1} HP due to your attack!",nameM, dmg_takenM); <--- old unformatted string

				if (dmg_takenP > 0) {
					Console.Write ("You lost ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write ("{0} HP ", dmg_takenP);
					Console.ResetColor ();
					Console.Write ("due to an attack by {0}.\n", nameM);

					if (p1.health > 0) {
						Console.WriteLine ("{0} is now on {1} HP .\n", nameP, healthP);
					}
				} else {
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine ("You blocked an attack by {0} !", nameM);
					Console.ResetColor ();
				}

				Console.Write ("{0} lost ", nameM);
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write ("{0} HP ", dmg_takenM);
				Console.ResetColor ();
				Console.Write ("due to your attack!\n");

				if (skele.health > 0) {
					Console.WriteLine ("{0} is now on {1} HP .\n", nameM, healthM);
				} else {
					Console.WriteLine ("\nYou have slain {0} !", nameM);
					Console.WriteLine ("You gained {0} XP.", xp_gained);
					p1.xp = p1.xp + xp_gained;
					xpP = xpP + p1.xp;

					xp_to_lvl_main = p1.get_XP_for_next_level ();

					Console.WriteLine ("You currently have {0} XP and need {1} XP to progress to the next level.", xpP, xp_to_lvl_main);
					Console.WriteLine ("You looted a {0}, from {1}.", monster_loot, nameM);

					switch (monster_loot) {
					//string[] loot = {"Iron dagger","Leather cowl","Wooden shield","Leather trousers","Leather shoes","Leather armor"};
					case "Iron dagger":
						p1.attack = p1.attack + Iron_dagger.attackPoints;
						attackP = p1.attack;
						Console.WriteLine ("Your new attack points total {0}", attackP);
						break;

					case "Leather cowl":	
						p1.defence = p1.defence + Leather_cowl.defencePoints;
						defenceP = p1.defence;
						Console.WriteLine ("Your new defence points total {0}", defenceP);
						break;

					case "Wooden shield":
						p1.defence = p1.defence + Wooden_shield.defencePoints;
						defenceP = p1.defence;
						Console.WriteLine ("Your new defence points total {0}", defenceP);
						break;

					case "Leather trousers":
						p1.defence = p1.defence + Leather_trousers.defencePoints;
						defenceP = p1.defence;
						Console.WriteLine ("Your new defence points total {0}", defenceP);
						break;

					case "Leather shoes":
						p1.defence = p1.defence + Leather_shoes.defencePoints;
						defenceP = p1.defence;
						Console.WriteLine ("Your new defence points total {0}", defenceP);
						break;

					case "Leather armor":
						p1.defence = p1.defence + Leather_armor.defencePoints;
						defenceP = p1.defence;
						Console.WriteLine ("Your new defence points total {0}", defenceP);
						break;
					}
				}
				System.Threading.Thread.Sleep (500);
			} while (skele.health > 0);

			Console.WriteLine ("\nDo you wish to save your character stats?");

			Console.Write ("User (y/n): ");
			response = Console.ReadLine ();

			if (response == "y") 
			{
				p1.quick_save(nameP, healthP, manaP, defenceP, attackP, xpP);
				Console.WriteLine ("File successfully saved!");
			}
		}
	}
}
