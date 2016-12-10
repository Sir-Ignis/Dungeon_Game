using System;

namespace Monsters
{
	class Item
	{
		public string name = "";
		public int weight = 0;

		public Item (string itsName, int itsWeight)
		{
			name  = itsName;
			weight = itsWeight;
		}
	}

	class Weapon : Item
	{
		public int attackPoints = 0;

		public Weapon (string itsName, int itsAttackPoints, int itsWeight) : base (itsName, itsWeight)
		{
			name = itsName;
			attackPoints = itsAttackPoints;
			weight = itsWeight;
		}
	}

	class Armor : Item
	{
		public int defencePoints = 0;

		public Armor (string itsName, int itsDefencePoints, int itsWeight) : base (itsName, itsWeight)
		{
			name = itsName;
			defencePoints = itsDefencePoints;
			weight = itsWeight;
		}
	}

	class Monster
	{
		public string name = "";
		public int health = 0;
		public int mana = 0;
		public int defence = 0; 
		public int attack = 0;

		public Monster (string itsName, int itsHealth, int itsMana, int itsDefence, int itsAttack)
		{
		}
	}

	class Skeleton : Monster
	{
		public Skeleton (string itsName, int itsHealth, int itsMana, int itsDefence, int itsAttack) : base (itsName, itsHealth, itsMana,  itsDefence, itsAttack)
		{
		}
	}

	class Player
	{
		public string name = "";
		public int health = 0;
		public int mana = 0;
		public int defence = 0;
		public int attack = 0;

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

		public Player (string itsName, int itsHealth, int itsMana, int itsDefence, int itsAttack)
		{
		}
	}

	class Dungeon
	{
		public Random rand = new Random();
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
			string[] loot = {"Iron dagger","Leather cowl","Wooden shield","Leather trousers","Leather shoes","Leather armor"};

			return loot[loot_roll];
		}
	}

	class MainClass
	{
		public static void Main (string[] args)
		{
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

			Player p1 = new Player ("Default Player", 0, 0, 0, 0);

			string nameP = p1.name = System.Security.Principal.WindowsIdentity.GetCurrent ().Name;
			int healthP = p1.health = 30;
			int manaP = p1.mana = 5;
			int defenceP = p1.defence = 0;
			int attackP = p1.attack = 10;

			Console.WriteLine ("\nWelcome {0} !", nameP);

			for (int j = 0; j < 4; j++) //cycles through and assigns local player stats random values from the Player get_stats function
			{
				switch (j)
				{
					case 1:
						healthP = p1.health = p1.get_stats(j);
						break;
					case 2:
						manaP = p1.mana = p1.get_stats(j);
						break;
					case 3:
						defenceP = p1.defence = p1.get_stats(j);
						break;
					case 4:
						attackM = p1.attack = p1.get_stats(j);
						break;

				}
			}

			Console.WriteLine ("A player called {0} with:" +
			                   "\n{1} HP" +
			                   "\n{2} MP" +
			                   "\n{3} DEF" +
			                   "\n{4} ATK" +
			                   "\nhas been created...\n", nameP, healthP, manaP, defenceP, attackP); //P = Player

			int dmg_dealtP = 0;
			int dmg_takenM = 0;

			int dmg_dealtM = 0;
			int dmg_takenP = 0;

			Dungeon D1 = new Dungeon();
			
			int xp_gained = Convert.ToInt32(D1.getPlayer_XP(healthM,defenceP,attackM));
			string monster_loot = D1.generate_loot();

			Weapon Iron_dagger = new Weapon("Iron dagger", 5, 10);
			Armor Leather_cowl = new Armor("Leather cowl",2,2);
			Armor Wooden_shield = new Armor("Wooden shield",12,15);
			Armor Leather_trousers = new Armor("Leather trousers", 5, 3);
			Armor Leather_shoes = new Armor("Leather shoes",1,3);
			Armor Leather_armor = new Armor("Leather armor",5,10);

			Console.WriteLine ("\nFIGHT!\n");
			do 
			{
				//calculates damage dealt by player and health of the monster as a result of the player's attack
				dmg_dealtP = D1.getPlayer_Damage(attackP);
				dmg_takenM = D1.damage_TakenM(dmg_dealtP, defenceM);
				healthM = skele.health -= dmg_takenM;

				//calculates damage dealt by monster and health of the player as a result of the monster's attack
				dmg_dealtM = D1.getMonster_Damage(attackM);
				dmg_takenP = D1.damage_TakenM(dmg_dealtM, defenceP);
				healthP = p1.health -= dmg_takenP;

				//Console.WriteLine ("{0} lost {1} HP due to your attack!",nameM, dmg_takenM); <--- old unformatted string

				if (dmg_takenP > 0)
				{
					Console.Write ("You lost ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write ("{0} HP ",dmg_takenP);
					Console.ResetColor();
					Console.Write ("due to an attack by {0}.\n",nameM);

					if (p1.health > 0)
					{
						Console.WriteLine ("{0} is now on {1} HP .\n",nameP, healthP);
					}
				}

				else 
				{
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine ("You blocked an attack by {0} !",nameM);
					Console.ResetColor();
				}

				Console.Write ("{0} lost ",nameM);
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write ("{0} HP ",dmg_takenM);
				Console.ResetColor();
				Console.Write ("due to your attack!\n");

				if (skele.health > 0)
				{
					Console.WriteLine ("{0} is now on {1} HP .\n",nameM, healthM);
				}
				else 
				{
					Console.WriteLine ("\nYou have slain {0} !",nameM);
					Console.WriteLine ("You gained {0} XP.", xp_gained);
					Console.WriteLine ("You looted a {0}, from {1}.",monster_loot,nameM);

					switch (monster_loot)
					{
						//string[] loot = {"Iron dagger","Leather cowl","Wooden shield","Leather trousers","Leather shoes","Leather armor"};
						case "Iron dagger":
											p1.attack = p1.attack +Iron_dagger.attackPoints;
											attackP = p1.attack;
											Console.WriteLine ("Your new attack points total {0}",attackP);
											break;

						case "Leather cowl":	
											p1.defence = p1.defence + Leather_cowl.defencePoints;
											defenceP = p1.defence;
											Console.WriteLine ("Your new defence points total {0}",defenceP);
											break;

						case "Wooden shield":
											p1.defence = p1.defence +Wooden_shield.defencePoints;
											defenceP = p1.defence;
											Console.WriteLine ("Your new defence points total {0}",defenceP);
											break;

						case "Leather trousers":
											p1.defence = p1.defence + Leather_trousers.defencePoints;
											defenceP = p1.defence;
											Console.WriteLine ("Your new defence points total {0}",defenceP);
											break;

						case "Leather shoes":
											p1.defence = p1.defence + Leather_shoes.defencePoints;
											defenceP = p1.defence;
											Console.WriteLine ("Your new defence points total {0}",defenceP);
											break;

						case "Leather armor":
											p1.defence = p1.defence + Leather_armor.defencePoints;
											defenceP = p1.defence;
											Console.WriteLine ("Your new defence points total {0}",defenceP);
											break;
					}
				}
				System.Threading.Thread.Sleep(500);
			} while (skele.health > 0);
		}
	}
}
