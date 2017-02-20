using System;

namespace Monsters
{
	class Monster
	{
		public Random rand = new Random();
		public string name = "";
		public int health = 0;
		public int mana = 0;
		public int defence = 0; 
		public int attack = 0;

		public int resetHealth (Skeleton itsSkeleton)
		{
			if ((itsSkeleton.health < 0) || (itsSkeleton.health == 0)) 
			{
				int rand_hp = rand.Next (1, 20) + 30;
				itsSkeleton.health = rand_hp;
			}
			return itsSkeleton.health;
		}
		public string generate_random_name () //currently not fully implemented
		{
			//names generated using: http://listofrandomnames.com/index.cfm?textarea 
			string [] names = {
				"Joaquin", 
				"Earl" ,
				"Tory",  
				"Colin",  
				"Mathew",  
				"Rudy",  
				"Ty",  
				"Marshall",  
				"Mohammed",  
				"Douglass",  
				"Boris",  
				"Chung",  
				"Orlando",
				"Jeremy",  
				"Isaac",  
				"Tomas",  
				"Donnell",  
				"Kelly",  
				"Aldo",  
				"Nathanael",  
				"Antonio",  
				"Dick",  
				"Efrain",  
				"Benjamin",  
				"Timmy",  
				"Emory",  
				"Jesus",  
				"Pasquale",  
				"Jean",  
				"Keneth",  
				"Davis",  
				"Karl",  
				"Francis",  
				"Vance",  
				"Sterling",  
				"Harlan",  
				"Manual",  
				"Elvin",  
				"Spencer",  
				"Leon",  
				"Chase",  
				"Neville",  
				"Bradly",  
				"Preston",  
				"Diego",  
				"Zachariah",  
				"Rich",  
				"Alvin",  
				"Eduardo",  
				"Abe"};
			return names[rand.Next(1,50)];
		}


		public Monster ()
		{
		}
	}

	class Skeleton : Monster
	{
		public Skeleton () : base ()
		{
			name = generate_random_name ();
			health = get_stats (1);
			mana = get_stats (2);
			defence = get_stats (3);
			attack = get_stats (4);
		}
			
		public int get_stats (int x)
		{
			int rand_hp = rand.Next (1, 20) + 30;
			int rand_mp = rand.Next (1, 10) + 5;
			int rand_df = rand.Next (1,2);
			int rand_ak = rand.Next (1, 6) + 4;
			
			
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
	}

	class Wraith : Monster
	{
		public Wraith () : base ()
		{
			name = "Wraith";
			health = boss1_stats (1);
			mana = boss1_stats (2);
			defence = boss1_stats (3);
			attack = boss1_stats (4);
		}

		public int boss1_stats (int x)
		{
			int rand_hp = rand.Next (1, 100) + 30;
			int rand_mp = rand.Next (1, 50) + 5;
			int rand_df = rand.Next (1,5);
			int rand_ak = rand.Next (1, 12) + 4;
		

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

		public string WraithAttacks () //TO BE IMPLEMENTED
		{
			int attackTypeRoll = rand.Next (1, 7);
			string attackName = string.Empty;
			switch (attackTypeRoll) 
			{
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				attackName = "torment"; //normal
				break;

				case 6:
				attackName = "summon undead"; //special	
				break;
			}
			return attackName;
		}

		public int getWraithDamage (string itsAttackName, int itsAttack, int itsPlayerDefence) //TO BE IMPLEMENTED
		{
			int wraithDamage = 0;
			int skeletonsSummoned = 0;
			if (itsAttackName == "torment") {
				wraithDamage = (itsAttack * 2) - itsPlayerDefence;
			} 
			else //special attack
			{
				skeletonsSummoned = rand.Next (1,3);

				if (skeletonsSummoned == 1)
				{
					//Skeleton summon1 = new Skeleton("Joe Bloggs 1",45,0,5,5);
				}
				else //skeletonsSummoned = 2
				{
					//Skeleton summon1 = new Skeleton("Joe Bloggs 1",45,0,5,5);
					//Skeleton summon2 = new Skeleton("Joe Bloggs 2",45,0,5,5);
				}

			}
			return wraithDamage;
		}

		public Skeleton [] returnSummons(Skeleton s1, Skeleton s2) //TO BE IMPLEMENTED
		{
			Skeleton [] summonedSkeletonsArray = {s1,s2};
			return summonedSkeletonsArray;
		}
		
	}
}

