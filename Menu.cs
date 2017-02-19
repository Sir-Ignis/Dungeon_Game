using System;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MainMenu
{
	class Menu
	{
		[DllImport("user32.dll")] //*
		/// <summary>
		/// Shows the window.
		/// </summary>
		/// <returns><c>true</c>, if window was shown, <c>false</c> otherwise.</returns>
		/// <param name="hWnd">H window.</param>
		/// <param name="cmdShow">Cmd show.</param>
		public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow); //*

		//*only for windows
		/// <summary>
		/// Maximize this instance.
		/// </summary>
		public void Maximize() //*
		{
			Process p = Process.GetCurrentProcess();
			ShowWindow(p.MainWindowHandle, 3); //SW_MAXIMIZE = 3
		}

		/// <summary>
		/// Loading this instance.
		/// </summary>
		public void loading ()
		{
			for(int i = 0; i < 101; ++i)
			{
				Console.Write("\r{0}%  Loaded", i);
				Thread.Sleep(50);
			}
			Console.WriteLine (); //clears up
			Thread.Sleep (250);
		}
		/// <summary>
		/// Centre the specified x.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		public void centre (int x) // trunc(string length / 2 )
		{
			int half_way = System.Console.WindowWidth/2;
			for (int i = 0; i < (half_way) - x; i++) //4 = char length of "MAIN MENU"/2 rounded down
			{
				Console.Write (" ");
			}
		}
		/// <summary>
		/// Prints the menu.
		/// </summary>
		public void print_Menu ()
		{
			int x = 0;

			for (int i = 0; i < System.Console.WindowWidth; i++) 
			{
				Console.Write ("*");
			}

			x = Convert.ToInt32(Math.Truncate(24M/2));
			centre(x);
			Console.WriteLine ("ARX LUDUS © - VERSION 0.4.8"); //0 = Alpha

			/*Version code
			 * X.Y.Z
			 * X = 0/1, 0 = alpha, 1 = beta
			 * Y = major changes to program 
			 * Z = minor changes to program
			*/

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

			Console.WriteLine ("HELP");

			x = Convert.ToInt32(Math.Truncate(4M/2));
			centre (x);

			Console.WriteLine ("EXIT");
			Console.WriteLine();

			for (int i = 0; i < System.Console.WindowWidth; i++) 
			{
				Console.Write ("*");
			}

		}

		/// <summary>
		/// Prints the help.
		/// </summary>
		public void printHelp ()
		{
			Console.Clear ();
			int x = 0;
			
			for (int i = 0; i < System.Console.WindowWidth; i++) {
				Console.Write ("*");
			}

			x = Convert.ToInt32 (Math.Truncate (4M / 2));
			centre (x);
			
			Console.WriteLine ("HELP");

			for (int i = 0; i < System.Console.WindowWidth; i++) {
				Console.Write ("*");
			}
			Console.WriteLine ();

			Console.WriteLine ("\nSYMBOLS");

			Console.WriteLine ("\n>X = player");
			Console.WriteLine (">M = monster");
			Console.WriteLine (">C = chest");
			Console.WriteLine (">S = stairs");
			Console.WriteLine ("># = wall");
			Console.WriteLine (">. = empty");

			Console.WriteLine ("\nMOVEMENT");
			Console.WriteLine ("\n>Use the arrow keys to move your character.");

			Console.WriteLine ("\nCOMBAT");
			Console.WriteLine ("\n>Type the name of the ability you want to use against a monster.");
			Console.WriteLine (">The damage inflicted on the monster using the ability you selected is automated.");
			Console.WriteLine (">Damage that you take is also automated");

			Console.WriteLine ("\nACTIONS");
			Console.WriteLine ("\n>Press the 'm' key to access the menu for the list of actions which you can carry out.");
			Console.WriteLine ("\n>Press a number from 0-4 to carry out an action. The action corresponds to the number" +
			                   "\nshown in the brackets in the list of actions which you can carry out.");

			Console.WriteLine ("\nOBJECTIVE");
			Console.WriteLine ("\n>The objective of the game is to exit the dungeon alive.");
			Console.WriteLine (">Once you have slain all the monsters a boss will spawn somewhere in the map.");
			Console.WriteLine (">You must slay the boss for the stairs to appear.");
			Console.WriteLine (">You can exit the dungeon by moving to the stairs.");
		}

		/// <summary>
		/// Prints the cave.
		/// </summary>
		public void print_cave()
		{
			Console.WriteLine("**************************************************************************************"+ 
			                  "\n*                    /   \\              /'\\       _                                  *"+
			                  "\n*\\_..           /'.,/     \\_         .,'   \\     / \\_                          \t     *"+
			                  "\n*    \\         /            \\     _/       \\_  /    \\     _                    \t     *"+
			                  "\n*     \\__,.   /              \\    /           \\/.,   _|  _/ \\                  \t     *"+
			                  "\n*          \\_/                \\  /',.,''\\      \\_ \\_/  \\/    \\              \t     *"+
			                  "\n*                           _  \\/   /    ',../',.\\    _/      \\                      *"+
			                  "\n*             /           _/m\\  \\  /    |         \\  /.,/'\\   _\\              \t     *"+
			                  "\n*           _/           /MMmm\\  \\_     |          \\/      \\_/  \\             \t     *"+
			                  "\n*          /      \\     |MMMMmm|   \\__   \\          \\_       \\   \\_          \t     *"+
			                  "\n*                  \\   /MMMMMMm|      \\   \\           \\       \\   \\          \t     *"+
			                  "\n*                   \\  |MMMMMMmm\\      \\___            \\_      \\_   \\        \t     *"+
			                  "\n*                    \\|MMMMMMMMmm|____.'  /\\_            \\       \\   \\_       \t     *"+
			                  "\n*                    /'.,___________...,,'   \\            \\   \\        \\       \t     *"+
			                  "\n*                   /       \\          |      \\    |__     \\   \\_       \\     \t     *"+
			                  "\n*                 _/        |           \\      \\_     \\     \\    \\       \\_  \t     *"+
			                  "\n*                /                               \\     \\     \\_   \\        \\  \t     *"+
			                  "\n*                                                 \\     \\      \\   \\__      \\ \t     *"+
			                  "\n*                                                  \\     \\_     \\     \\      \\\t     *"+
			                  "\n*                                                   |      \\     \\     \\      \\      *"+
			                  "\n*                                                    \\ms          |            \\     *"+
			                  "\n**************************************************************************************");
		}

		/// <summary>
		/// Resets the colours.
		/// </summary>
		public void reset_colours ()
		{
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="MainMenu.Menu"/> class.
		/// </summary>
		public Menu ()
		{
		}
	}
}

