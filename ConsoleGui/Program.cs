using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe;

namespace ConsoleGui
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            DrawBoard(game);

            while (game.IsNotOver)
            {
                int fieldNumber = 0;

                Console.WriteLine("Input field number (form 0 to 8)");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.WriteLine(Environment.NewLine);

                while (KeyIsNotValidInteger(keyInfo, out fieldNumber)) {
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("You must write number [0-8]:");
                    keyInfo = Console.ReadKey();
                }

                try
                {
                    game.Move(fieldNumber);
                    DrawBoard(game);
                }
                catch (InvalidOperationException e) {
                    Console.WriteLine(Environment.NewLine + e.Message + Environment.NewLine);
                }
                catch (ArgumentOutOfRangeException e)  {
                    Console.WriteLine(Environment.NewLine + e.Message + Environment.NewLine);
                }                
            }

            DrawBoard(game);

            if (game.CheckResult() == GameResult.CrossWin)
                Console.WriteLine("Game over! Cros win X");
            if (game.CheckResult() == GameResult.NoughtWin)
                Console.WriteLine("Game over! Nought win O");
            if (game.CheckResult() == GameResult.Draw)
                Console.WriteLine("Game over! Nobody win!");

            Console.ReadKey(true);
        }

        static private bool KeyIsNotValidInteger(ConsoleKeyInfo keyInfo, out int integer)
        {
            return !int.TryParse(keyInfo.KeyChar.ToString(), out integer);
        }

        static private void DrawBoard(Game game)
        {
            char[] t = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8' };

            for (int i = 0; i < 9; i++)
            {
                BoardFieldState status = game.FieldState(i);
                if (status == BoardFieldState.Cross) t[i] = 'X';
                if (status == BoardFieldState.Nought) t[i] = 'O';                
            }
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(" _" + t[0] + "_|_" + t[1] + "_|_" + t[2] + "_ ");
            Console.WriteLine(" _" + t[3] + "_|_" + t[4] + "_|_" + t[5] + "_ ");
            Console.WriteLine("  " + t[6] + " | " + t[7] + " | " + t[8] + "  ");
            Console.WriteLine(Environment.NewLine);
        }
    }   

}
