using System;
using Monsters;
using Players;
using MainMenu;
using Sound;
using DungeonMap;
using System.Threading;

namespace Combat
{
	class DungeonFight
	{
		Random rand = new Random();
		Menu m = new Menu();
		int monsters_left = 0;

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

		public decimal getPlayer_XP (int hp_statM, int def_statP, int atk_statM)
		{
			decimal XP = ( hp_statM / (atk_statM - def_statP) ) + hp_statM;
			return Math.Truncate(XP);
		}

		public int FightMonster (Skeleton itsSkeleton, Player itsP1, int itsStartHealthP, int its_levels_advanced, ScreenBuffer itsBuffer)
		{
			int dmg_dealtP = 0;
			int dmg_takenM = 0;

			int dmg_dealtM = 0;
			int dmg_takenP = 0;
			int tempLvl = 0;


			int xp_to_lvl_main = itsP1.get_XP_for_next_level ();
			int xp_gained = Convert.ToInt32 (getPlayer_XP (itsSkeleton.health, itsP1.defence, itsSkeleton.attack));
			string chosen_attack_ability = "";

			/*string flush = string.Empty;
			for (int i = 0; i < 49; i++) {
					flush += "\n";

			}*/

			//Console.SetCursorPosition(0,51);
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
							Console.ReadLine();
							Console.WriteLine ("You died!");
							Console.WriteLine ("Game over!");
							System.Environment.Exit (1);
						}
					} else {
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine ("You blocked an attack by {0} !", itsSkeleton.name);
						SoundEffects sfx = new SoundEffects("shield_block.wav");
						sfx.playSFX();
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
								Console.ReadLine();	
								Console.WriteLine ("You died!");
								Console.WriteLine ("Game over!");
								System.Environment.Exit (1);
							}
						} 
					}
				}

				chosen_attack_ability = itsP1.get_attack (itsP1.mana, itsP1.xp); // allows the player to enter what attack ability they want to use
				dmg_dealtP = itsP1.get_attack_damage (itsP1.attack, chosen_attack_ability); //assigns the damage dealt by the player determined by the attack ability and the base attack of the player
				dmg_takenM = damage_TakenM (dmg_dealtP, itsSkeleton.defence);
				itsSkeleton.health -= dmg_takenM;


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
					xp_to_lvl_main = itsP1.get_XP_for_next_level (); //*broken in current version

					Console.WriteLine ("You currently have {0} XP and need {1} XP to progress to the next level.", itsP1.xp, xp_to_lvl_main); //*
					//Console.WriteLine ("You looted a {0}, from {1}.", monster_loot, nameM);
		}
	} while (itsSkeleton.health > 0); 

			Console.WriteLine ("\nPress enter to exit fight mode.");
			ConsoleKeyInfo tempKeyInfo;
			tempKeyInfo = Console.ReadKey ();
			if (tempKeyInfo.Key != ConsoleKey.Enter) {
				Thread.Sleep (5000);
			}
	return its_levels_advanced;
}
	
		public void spider_fight_scene()
		{
			Console.WriteLine("\n\n");
			Console.WriteLine("\n    /  /      \\ "+
							  "\n    \\  \\  ,,  /  /"+
							  "\n     '-.`\\()/`.-'"+
							  "\n    .--_'(  )'_--."+
							  "\n   / /` /`\"\"`\\ `\\ \\"+
							  "\n    |  |  ><  |  |"+
						 	  "\n    \\  \\      /  /"+
							  "\njgs     '.__.'");
		}
		/*
		 *  TODO: Write a generic fight method, which takes any monster,
		 *		  and depending on the monster the fight outputs and inputs
		 *		  are varied. As the current method is very inefficient
		 *
		 *		  Make spider ASCII art
		*/

		public int FightSpider (FleshEatingSpider itsSpider, Player itsP1, int itsStartHealthP, int its_levels_advanced, ScreenBuffer itsBuffer) 
		{
			spider_fight_scene ();
			int dmg_dealtP = 0;
			int dmg_takenM = 0;

			int dmg_dealtM = 0;
			int dmg_takenP = 0;
			int tempLvl = 0;


			int xp_to_lvl_main = itsP1.get_XP_for_next_level ();
			int xp_gained = Convert.ToInt32 (getPlayer_XP (itsSpider.health, itsP1.defence, itsSpider.attack));
			string chosen_attack_ability = "";

			/*string flush = string.Empty;
			for (int i = 0; i < 49; i++) {
					flush += "\n";

			}*/

			//Console.SetCursorPosition(0,51);
			Console.WriteLine ("\nFIGHT!\n");
			do {

				//calculates damage dealt by monster and health of the player as a result of the monster's attack

					dmg_dealtM = getMonster_Damage (itsSpider.attack);
					dmg_takenP = damage_TakenM (dmg_dealtM, itsP1.defence);
					itsP1.health -= dmg_takenP;

					if (dmg_takenP > 0) {
						Console.Write ("You lost ");
						Console.ForegroundColor = ConsoleColor.Red;
						Console.Write ("{0} HP ", dmg_takenP);
						m.reset_colours();
						Console.Write ("due to an attack by {0}.\n", itsSpider.name);

						if (itsP1.health > 0) {
							Console.WriteLine ("{0} is now on {1} HP .\n", itsP1.name, itsP1.health);
						}

						else if (itsP1.health < 0)
						{
							Console.ReadLine();
							Console.WriteLine ("You died!");
							Console.WriteLine ("Game over!");
							System.Environment.Exit (1);
						}
					} else {
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine ("You blocked an attack by {0} !", itsSpider.name);
						SoundEffects sfx = new SoundEffects("shield_block.wav");
						sfx.playSFX();
						m.reset_colours();
					}

				

				chosen_attack_ability = itsP1.get_attack (itsP1.mana,itsP1.xp); // allows the player to enter what attack ability they want to use
				dmg_dealtP = itsP1.get_attack_damage (itsP1.attack, chosen_attack_ability); //assigns the damage dealt by the player determined by the attack ability and the base attack of the player
				dmg_takenM = damage_TakenM (dmg_dealtP, itsSpider.defence);
				itsSpider.health -= dmg_takenM;


				Console.Write ("{0} lost ", itsSpider.name);
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write ("{0} HP ", dmg_takenM);
				m.reset_colours();
				Console.Write ("due to your attack!\n");

				if (itsSpider.health > 0) {
					Console.WriteLine ("{0} is now on {1} HP .\n", itsSpider.name, itsSpider.health);
				} else {
					monsters_left--;
					Console.WriteLine ("\nYou have slain {0} !", itsSpider.name);


					Console.Write ("You currently on");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write(" {0} HP",itsP1.health);
					m.reset_colours();

					Console.WriteLine ("\nYou gained {0} XP.", xp_gained);

					itsP1.xp += xp_gained;

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
					xp_to_lvl_main = itsP1.get_XP_for_next_level (); //*broken in current version

					Console.WriteLine ("You currently have {0} XP and need {1} XP to progress to the next level.", itsP1.xp, xp_to_lvl_main); //*
					//Console.WriteLine ("You looted a {0}, from {1}.", monster_loot, nameM);
				}
			} while (itsSpider.health > 0); 

			Console.WriteLine ("\nPress enter to exit fight mode.");
			ConsoleKeyInfo tempKeyInfo;
			tempKeyInfo = Console.ReadKey ();
			if (tempKeyInfo.Key != ConsoleKey.Enter) {
				Thread.Sleep (5000);
			}
			return its_levels_advanced;
		}

		public void boss_fight_scene()
		{
			Console.WriteLine("                                      .\"\"--..__\n"+
				"                     _                      []       ``-.._\n"+
				".                  '` `'.                   ||__           `-._\n"+
				"                  /    ,-.\\                 ||_ ```---..__     `-.\n"+
				"                 /    /:::\\               /|//}          ``--._  `.\n"+
				"                 |    |:::||              |////}                `-. \\ \n"+
				"                 |    |:::||             //'///                    `.\\ \n"+
				"                 |    |:::||            //   ||                      `|\n"+
				"jgs             /     |:::|/        _,-//\\  ||\n"+
				"hh             /`     |:::|`-,__,-'`  |/  \\ ||\n"+
				"             /`  |    |'' ||           \\   |||\n"+
				"           /`    \\   |   ||            |  / ||\n"+
				"         |`       |  |   |)            \\ |  ||\n"+
				"        |          \\ |   /      ,.__    \\|  ||\n"+
				"        /           `         /`    `\\   |  ||\n"+
				"       |                     /        \\  /  ||\n"+
				"       |                     |        | /   ||\n"+
				"       /         /           |        `(    ||\n"+
				"      /          .           /          )   ||\n"+
				"     |            \\          |     ________ ||\n"+
				"    /             |          /     `-------.||\n"+
				"   |\\            /          |               ||\n"+
				"   \\/`-._       |           /               ||\n"+
				"     //   `.    /`           |              ||\n"+
				"    //`.    `. |             \\              ||\n"+
				"   ///\\ `-._  )/             |              ||\n"+
				"  //// )   .(/               |              ||\n"+
				"  ||||   ,'` )               /             //\n"+
				"  ||||  /                    /             || \n"+
				"  `\\\\` /`                    |            // \n"+
				"      |`                     \\            ||  \n"+
				"     /                        |           //  \n"+
				"   /`                          \\         //   \n"+
				" /`                            |         ||   \n "+
				"`-.___,-.      .-.        ___,'        (/  \n\n");
		}

		public int Boss_Fight (Wraith itsWraith, Player itsP1, int itsStartHealthP, int its_levels_advanced)
		{
			boss_fight_scene ();
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


				chosen_attack_ability = itsP1.get_attack (itsP1.mana, itsP1.xp); // allows the player to enter what attack ability they want to use
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
						Console.ReadLine();
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
			Console.WriteLine ("\nPress enter to exit fight mode.");
			ConsoleKeyInfo tempKeyInfo;
			tempKeyInfo = Console.ReadKey ();
			if (tempKeyInfo.Key != ConsoleKey.Enter) {
				Thread.Sleep (5000);
			}
			Console.Clear ();
			return its_levels_advanced;
		}
public DungeonFight ()
{

}
}
}
		