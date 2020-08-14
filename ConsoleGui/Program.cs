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

            Console.ReadKey();
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

            Console.WriteLine(" _" + t[0] + "_|_" + t[1] + "_|_" + t[2] + "_ ");
            Console.WriteLine(" _" + t[3] + "_|_" + t[4] + "_|_" + t[5] + "_ ");
            Console.WriteLine("  " + t[6] + " | " + t[7] + " | " + t[8] + "  ");
            Console.WriteLine(Environment.NewLine);
        }
    }

   

}
