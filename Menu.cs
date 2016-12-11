using System;

namespace MainMenu
{
	class Menu
	{
		public void centre (int x) // trunc(string length / 2 )
		{
			int half_way = System.Console.WindowWidth/2;
			for (int i = 0; i < (half_way) - x; i++) //4 = char length of "MAIN MENU"/2 rounded down
			{
				Console.Write (" ");
			}
		}
		public void print_Menu ()
		{
			int x = 0;
			for (int i = 0; i < System.Console.WindowWidth; i++) 
			{
				Console.Write ("*");
			}

			Console.WriteLine();

			x = Convert.ToInt32(Math.Truncate(9M/2));
			centre(x);

			Console.WriteLine ("MAIN MENU");

			for (int i = 0; i < System.Console.WindowWidth; i++) 
			{
				Console.Write ("*");
			}

			Console.WriteLine();

			x = Convert.ToInt32(Math.Truncate(4M/2));
			centre (x);
			Console.WriteLine ("START");

			x = Convert.ToInt32(Math.Truncate(4M/2));
			centre (x);

			Console.WriteLine ("EXIT");
			Console.WriteLine();

			for (int i = 0; i < System.Console.WindowWidth; i++) 
			{
				Console.Write ("*");
			}

		}
		public Menu ()
		{
		}
	}
}

