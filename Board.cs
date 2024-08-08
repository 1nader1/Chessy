using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessConsoleApp;

namespace Chess_game
{
    public class Board
    {

        public static string[,] board = new string[8, 8];

        public static void InitializeBoard()
        {
            // Empty squares

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = " -";
                }
            }

            // Pawns

            for (int i = 0; i < 8; i++)
            {
                board[1, i] = "bP";
                board[6, i] = "wP";
            }

            // Rooks

            board[0, 0] = board[0, 7] = "bR";
            board[7, 0] = board[7, 7] = "wR";

            // Knights

            board[0, 1] = board[0, 6] = "bN";
            board[7, 1] = board[7, 6] = "wN";

            // Bishops

            board[0, 2] = board[0, 5] = "bB";
            board[7, 2] = board[7, 5] = "wB";

            // Queens

            board[0, 3] = "bQ";
            board[7, 3] = "wQ";

            // Kings

            board[0, 4] = "bK";
            board[7, 4] = "wK";
        }

        public static void PrintBoard()
        {
            Console.Clear();

            // 1 to 8 order column 

            Console.WriteLine();
            for (int i = 0; i < 8; i++)
            {
                Console.Write(8 - i + "  ");
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(board[i, j] + "  ");
                }
                Console.WriteLine();
            }

            // A to H order row 

            Console.WriteLine();
            Console.Write("   ");
            for (char c = 'A'; c <= 'H'; c++)
            {
                Console.Write(c + "   ");
            }
            Console.WriteLine();

            Console.WriteLine($"White Time: {TimeSpan.FromSeconds(Player.whiteTime)}");
            Console.WriteLine($"Black Time: {TimeSpan.FromSeconds(Player.blackTime)}");
        }
    }
     
}
