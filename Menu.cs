using System;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MainMenu
{
	class Menu
	{
		[DllImport("user32.dll")] //*
		public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow); //*

		//*only for windows
		public void Maximize() //*
		{
			Process p = Process.GetCurrentProcess();
			ShowWindow(p.MainWindowHandle, 3); //SW_MAXIMIZE = 3
		}

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

			x = Convert.ToInt32(Math.Truncate(20M/2));
			centre(x);
			Console.WriteLine ("ARX LUDUS © - VERSION 0.1"); //0 = Alpha

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

		public void reset_colours ()
		{
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
		}
		public Menu ()
		{
		}
	}
}

