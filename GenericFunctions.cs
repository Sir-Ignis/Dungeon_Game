using System;

namespace genericFunctions
{
	public class GenericFunctions
	{
		public char genericKeyInput (int optionsType)
		{
			string startMessage =  "Press the";
			string exitHelpStartS = "'E'/'H'/'S' key for";
			string exitHelpStartL = "exit/help/start";

			string actionsMenuS = "'0'/'1'/'2'/'3'/'4' for";
			string actionsMenuL = "backpack (0), throw away (1) or equip items (2), print worn items (3) or stats (4)";

			ConsoleKeyInfo itsKeyInfo;
			char keyPressed = ' ';
			switch (optionsType) {

			//case x: //keyHandled1/keyHandled2/keyHandled3 ... => option x

			case 2: //e/h/s => optionsType == 2
				do
				{
					Console.WriteLine("\n{0} {1} {2}.",startMessage,exitHelpStartS,exitHelpStartL);
					Console.Write("User: ");
					itsKeyInfo = Console.ReadKey();
					keyPressed = (char)itsKeyInfo.Key;
				}
				while ((itsKeyInfo.Key != ConsoleKey.E) && (itsKeyInfo.Key != ConsoleKey.H) && (itsKeyInfo.Key != ConsoleKey.S));
				break;
			
			case 3: //0/1/2/3/4 => optionsType == 3
				do
				{
					Console.WriteLine("\n{0} {1} {2}.",startMessage,actionsMenuS,actionsMenuL);
					Console.Write("User: ");
					itsKeyInfo = Console.ReadKey();
					keyPressed = (char)itsKeyInfo.Key;
				}
				while ((itsKeyInfo.Key != ConsoleKey.D0) && (itsKeyInfo.Key != ConsoleKey.D1) 
				       && (itsKeyInfo.Key != ConsoleKey.D2) && (itsKeyInfo.Key != ConsoleKey.D3) && (itsKeyInfo.Key != ConsoleKey.D4));
				break;
	
			default: //y/n => optionsType == 0
				do 
				{
				Console.WriteLine ("\nPress the 'Y'/'N' key for yes/no.");
				Console.Write("User: ");
				itsKeyInfo = Console.ReadKey();
				keyPressed = (char)itsKeyInfo.Key;
				}
				while ((itsKeyInfo.Key != ConsoleKey.Y) && (itsKeyInfo.Key != ConsoleKey.N));
				break;
			}
			return keyPressed;
		}

		public GenericFunctions ()
		{
		}
	}
	
}

