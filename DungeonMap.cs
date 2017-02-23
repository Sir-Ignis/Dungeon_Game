using System;
using Dungeons;

namespace DungeonMap
{
	public class Game
	{
		public static int roomWidth = 50;
		public static int roomHeight = 51;

		public static void setRoomDimensions(int rw, int rh)
		{
			roomWidth = rw;
			roomHeight = rh;
		}

		public Game()
		{
		}
	}
	public class Map
	{
		public Random rand = new Random();
		int monsters_left = 0;

		public char [,] cMap  = new char[50,50];
		char player = 'X';
		bool boss_slain = false;

		//string sMap = " ";

		public char [,] initialise_Map ()
		{
			int r = 0;
			int s = 0;
			/*SYMBOL * MEANING* ID
			 * M     * Monster* 4
			 * C     * Chest  * 3
			 * S	 * Stairs * 2
			 * .	 * Empty  * 0
			 * #     * Wall	  * 1
			*/ 

			const string symbols = "MCS.#";
			//string symbols = " MCS.#";

			char[,] map = new char [50, 50];

			//[0,0] ,i.e (0,0), defined as origin

			//sets horizontal walls
			for (int i = 0; i < 50; i++) {
				map [i, 0] = symbols [4];
				map [i, 49] = symbols [4];
			}

			//sets vertical walls
			for (int j = 0; j < 50; j++) {
				map [0, j] = symbols [4];
				map [49, j] = symbols [4];
			}

			//spawns monsters and chests
			for (int x = 1; x < 49; x++) {
				/*M = symbols [0];
				 *C = symbols [1];
				 *S = symbols [2];
				 *. = symbols [3];
				 *# = symbols [4];
				*/
				//Thread.Sleep (100);
				for (int y = 1; y < 49; y++) {

					r = rand.Next (1, 101);
					{
						if ((r >= 44) && (r <= 45)) {
							map [x, y] = symbols [0];
						} else if (r == 1) {
							map [x, y] = symbols [1];
							monsters_left++; //increments number of monsters in the dungeon
						} else if (((r >= 2) && (r <= 43)) || ((r >= 46) && (r <= 100))) {
							map [x, y] = symbols [3];
						}
					}
				}
			}
			bool playerSpawned = false;
			do
			{
				r = rand.Next (1,48);
				s = rand.Next (1,48);
				if (map[r,s] == '.')
				{
					map[r,s] = player;
					playerSpawned = true;
				}
			}while (playerSpawned == false);

			return map;
		}

		public char [,] spawn_boss (char[,] itsMap)
		{
			int r = 0;
			int s = 0;

			//monsters_left = 0; //for testing

			if (monsters_left == 0) {
				do {
					r = rand.Next (1, 49);
					s = rand.Next (1, 49);
				} while ( (itsMap [r,s] != '.'));
				itsMap[r,s] = 'B';
			}

			return itsMap;
		}

		public char [,] spawn_stairs (char[,] itsMap)
		{
			int r = 0;
			int s = 0;

			//for testing
			/*boss_slain = true; 
			monsters_left = 0;*/
			//^
			/*Console.WriteLine(monsters_left);
			Console.WriteLine(boss_slain);
			Thread.Sleep(3000);*/ //for debugging
			if ((monsters_left == 0) && (boss_slain == true))
			{
				do {
					r = rand.Next (1, 49);
					s = rand.Next (1, 49);
				} while ( itsMap [r,s] != '.');

				itsMap[r,s] = 'S';
			}
			return itsMap;
		}

		public string parseMap (char [,] itsMap)
		{
			int mapSize = cMap.GetLength(0);
			int c = 0;
			string map = " ";
			for (int i = 0; i < mapSize; i++){
				for (int j = 0; j < mapSize; j++) {
					if (c % 50 == 0) {
						map += "\n";
					}
					switch (cMap [i, j]) {
					case 'X':
						map += 'X';
						break;
					case 'M':
						map += 'M';
						break;
					case 'C':
						map += 'C';
						break;
					default:
						map += cMap[i,j];
						break;
					}
					}
				}
			return map;
		}

		public void draw_map ()
		{
			int i = 0;
			foreach (char chr in ScreenBuffer.screenBuffer) 
			{

				if (i % Game.roomWidth == 50) {
					Console.WriteLine ();
				}
				/*if (tempMapVisible [x, y] == true) {*/ //FIXME
				switch (ScreenBuffer.screenBuffer[i]) {
				case 'C':
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.Write (ScreenBuffer.screenBuffer[i]);
					break;
				case 'M':
					Console.ForegroundColor = ConsoleColor.Magenta;
					Console.Write (ScreenBuffer.screenBuffer[i]);
					break;
				case 'X':
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write (ScreenBuffer.screenBuffer[i]);
					break;
				default:
					Console.ForegroundColor = ConsoleColor.White;
					Console.Write (ScreenBuffer.screenBuffer[i]);
					break;
				}
				Console.ForegroundColor = ConsoleColor.White;
				i++;
			}
		}
			
		public Map ()
		{
			cMap = initialise_Map ();
			//sMap = parseMap (cMap);
		}
	}

	//THANKS TO: http://cgp.wikidot.com/consle-screen-buffer
	public class ScreenBuffer
	{
		//initiate important variables
		public static char[,] screenBufferArray = new char[Game.roomWidth+1,Game.roomHeight]; //main buffer array
		public static string screenBuffer; //buffer as string (used when drawing)
		//public static Char[] arr; //temporary array for drawing string
		public static char [,] arr = new char[50,51];
		public static int i = 0; //keeps track of the place in the array to draw to
		public static int xc = 0; //keeps track of the x-coordinate of the cursor
		public static int yc = 0; //keeps track of the y-coordinate of the cursor

		//this method takes a string, and a pair of coordinates and writes it to the buffer
		public static void Draw(string text, int x, int y)
		{
			//split text into array
			int c =0;
			for (y=0; y < 51; y++) {
				for (x=0; x < 50; x++) {
					arr [x, y] = text [c];
					c++;
					}
				}

			//arr = text.ToCharArray(0,text.Length);
			//iterate through the array, adding values to buffer 
			//i = 0;
			//foreach (char c in arr)
			//{
				/*if (c == '\n') {
					screenBufferArray[x+i, y] = '\n';
				} else {
				*/
					//screenBufferArray [x+i, y] = c;
				//}
					//i++;
			for (y=0; y < 51; y++) {
				for (x=0; x < 50; x++) {
					screenBufferArray [x, y] = arr[x,y];
				}
			}
			   
		}

		public static void setCursorPosition(int ix, int iy)
		{
			xc = ix;
			yc = iy;
		}

		public static void DrawScreen()
		{
			screenBuffer = "";
			//int c = 0;
			//iterate through buffer, adding each value to screenBuffer
			for (int iy = 0; iy < Game.roomHeight; iy++)
			{
				for (int ix = 0; ix < Game.roomWidth; ix++)
				{
					/*if (c % Game.roomWidth == 0) {
						screenBuffer += "\n";
					} */
					/*else {*/
						screenBuffer += screenBufferArray [ix, iy];
					/*}
					c++;*/
				}
			}
			//set cursor position to top left and draw the string
			Console.SetCursorPosition(xc, yc);
			Console.Write(screenBuffer);
			screenBufferArray = new char[Game.roomWidth, Game.roomHeight];
			//note that the screen is NOT cleared at any point as this will simply overwrite the existing values on screen. Clearing will cause flickering again.
		}
	}
}


