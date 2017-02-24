using System;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

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
			Console.WriteLine ("ARX LUDUS © - VERSION 0.5.2"); //0 = Alpha

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

			Console.WriteLine ("CREDITS");

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

		static void ConsoleDraw(IEnumerable<string> lines, int x, int y)
		{
			if (x > Console.WindowWidth) return;
			if (y > Console.WindowHeight) return;

			var trimLeft = x < 0 ? -x : 0;
			int index = y;

			x = x < 0 ? 0 : x;
			y = y < 0 ? 0 : y;

			var linesToPrint =
				from line in lines
				let currentIndex = index++
					where currentIndex > 0 && currentIndex < Console.WindowHeight
				select new {
				Text = new String(line.Skip(trimLeft).Take(Math.Min(Console.WindowWidth - x, line.Length - trimLeft)).ToArray()),
				X = x,
				Y = y++
			};

			Console.Clear();
			foreach (var line in linesToPrint)
			{
				Console.SetCursorPosition(line.X, line.Y);
				Console.Write(line.Text);
			}
		}


		public void print_credits() //TODO
		{
			Console.CursorVisible = false;

			var arr = new[] {
				@"CREDITS",
				@"",
				@"",
				@"CODE",
				@"",
				@">Daniel",
				@"",
				@"",
				@"DESIGN",
				@"",
				@">Daniel",
				@"",
				@"",
				@"TESTERS",
				@"",
				@">Nick",
				@">George",
				@">Daniel",
				@">Demonsrun",
				@">Ollie",
				@"",
				@"ASCII ART",
				@"",
				@">jgs",

			};
			var maxLength = arr.Aggregate(0, (max, line) => Math.Max(max, line.Length));
			var x = Console.BufferWidth/2 - maxLength/2;
			for (int y = -arr.Length; y < Console.WindowHeight + arr.Length; y++)
			{
				ConsoleDraw(arr, x, y);
				Thread.Sleep(100);
			}

		}

		//*THANKS TO: http://stackoverflow.com/questions/33538527/display-a-image-in-a-console-application
		//*
		static int[] cColors = { 0x000000, 0x000080, 0x008000, 0x008080, 0x800000, 0x800080, 0x808000, 0xC0C0C0, 0x808080, 0x0000FF, 0x00FF00, 0x00FFFF, 0xFF0000, 0xFF00FF, 0xFFFF00, 0xFFFFFF };

		public static void ConsoleWritePixel(Color cValue)
		{
			Color[] cTable = cColors.Select(x => Color.FromArgb(x)).ToArray();
			char[] rList = new char[] { (char)9617, (char)9618, (char)9619, (char)9608 }; // 1/4, 2/4, 3/4, 4/4
			int[] bestHit = new int[] { 0, 0, 4, int.MaxValue }; //ForeColor, BackColor, Symbol, Score

			for (int rChar = rList.Length; rChar > 0; rChar--)
			{
				for (int cFore = 0; cFore < cTable.Length; cFore++)
				{
					for (int cBack = 0; cBack < cTable.Length; cBack++)
					{
						int R = (cTable[cFore].R * rChar + cTable[cBack].R * (rList.Length - rChar)) / rList.Length;
						int G = (cTable[cFore].G * rChar + cTable[cBack].G * (rList.Length - rChar)) / rList.Length;
						int B = (cTable[cFore].B * rChar + cTable[cBack].B * (rList.Length - rChar)) / rList.Length;
						int iScore = (cValue.R - R) * (cValue.R - R) + (cValue.G - G) * (cValue.G - G) + (cValue.B - B) * (cValue.B - B);
						if (!(rChar > 1 && rChar < 4 && iScore > 50000)) // rule out too weird combinations
						{
							if (iScore < bestHit[3])
							{
								bestHit[3] = iScore; //Score
								bestHit[0] = cFore;  //ForeColor
								bestHit[1] = cBack;  //BackColor
								bestHit[2] = rChar;  //Symbol
							}
						}
					}
				}
			}
			Console.ForegroundColor = (ConsoleColor)bestHit[0];
			Console.BackgroundColor = (ConsoleColor)bestHit[1];
			Console.Write(rList[bestHit[2] - 1]);
		}


		public static void ConsoleWriteImage(Bitmap source)
		{
			int sMax = 39;
			decimal percent = Math.Min(decimal.Divide(sMax, source.Width), decimal.Divide(sMax, source.Height));
			Size dSize = new Size((int)(source.Width * percent), (int)(source.Height * percent));   
			Bitmap bmpMax = new Bitmap(source, dSize.Width * 2, dSize.Height);
			for (int i = 0; i < dSize.Height; i++)
			{
				for (int j = 0; j < dSize.Width; j++)
				{
					ConsoleWritePixel(bmpMax.GetPixel(j * 2, i));
					ConsoleWritePixel(bmpMax.GetPixel(j * 2 + 1, i));
				}
				System.Console.WriteLine();
			}
			Console.ResetColor();
		}
		//*

		/// <summary>
		/// Initializes a new instance of the <see cref="MainMenu.Menu"/> class.
		/// </summary>

		public void printWelcome()
		{
			int z = 0;
			Bitmap bmpSrc = new Bitmap(@"Shield.png", true);    
			z = Convert.ToInt32(Math.Truncate(100M/2));
			centre (z);
			Console.WriteLine ("Count von Binary Productions Presents: Arx-Ludos"); 
			ConsoleWriteImage(bmpSrc);
			z = Convert.ToInt32(Math.Truncate(100M/2));
			centre (z);
		}

		public Menu ()
		{
		}
	}
}

