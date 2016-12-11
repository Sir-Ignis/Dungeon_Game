using System;

namespace Dungeons
{
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
}

