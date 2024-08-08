

using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChessConsoleApp
{
    class Chessy
    {
        static async Task Main(string[] args)
        {
            IGameEngine gameEngine = null;
            bool gameEngineSelected = false;

            while (!gameEngineSelected)
            {
                Console.Clear();
                Console.WriteLine("Choose the game engine:");
                Console.WriteLine("1. Engine 1");
                Console.WriteLine("2. Engine 2");
                Console.Write("Enter your choice: ");
                string chosenEngine = Console.ReadLine().Trim();

                switch (chosenEngine)
                {
                    case "1":
                        gameEngine = new Engine1();
                        gameEngineSelected = true;
                        break;
                    case "2":
                        gameEngine = new Engine2();
                        gameEngineSelected = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid engine (1 or 2)");
                        Console.WriteLine("Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }

            gameEngine.InitializeBoard();
            gameEngine.PrintBoard();

            Console.WriteLine("Choose your color (White or Black):");
            string chosenColor = Console.ReadLine().Trim().ToLower();

            bool isWhiteTurn = (chosenColor == "white");
            bool validColorChosen = (chosenColor == "white" || chosenColor == "black");

            if (!validColorChosen)
            {
                Console.WriteLine("Invalid color. Defaulting to Black");
                isWhiteTurn = false;
            }

            Task currentTimerTask = Player.RunTimer(isWhiteTurn ? Player.whiteCts.Token : Player.blackCts.Token, isWhiteTurn);

            while (true)
            {
                if (Player.whiteTime == 0)
                {
                    Console.WriteLine("Black wins! White ran out of time.");
                    break;
                }
                else if (Player.blackTime == 0)
                {
                    Console.WriteLine("White wins! Black ran out of time.");
                    break;
                }

                gameEngine.PrintBoard();
                Console.WriteLine($"{(isWhiteTurn ? "White" : "Black")}'s move:");
                string move = Console.ReadLine();

                if (gameEngine.IsMoveValid(move, isWhiteTurn))
                {
                    gameEngine.MovePiece(move);
                    gameEngine.PrintBoard();

                    if (gameEngine.IsCheckmate(!isWhiteTurn))
                    {
                        Console.WriteLine($"{(!isWhiteTurn ? "Black" : "White")} is in checkmate. {(!isWhiteTurn ? "White" : "Black")} wins!");
                        break;
                    }
                    else if (gameEngine.IsStalemate(!isWhiteTurn))
                    {
                        Console.WriteLine("Stalemate! It's a draw.");
                        break;
                    }

                    if (isWhiteTurn)
                    {
                        Player.whiteCts.Cancel();
                        Player.whiteCts = new CancellationTokenSource();
                        Player.whiteTimerTask = Player.RunTimer(Player.whiteCts.Token, false);
                    }
                    else
                    {
                        Player.blackCts.Cancel();
                        Player.blackCts = new CancellationTokenSource();
                        Player.blackTimerTask = Player.RunTimer(Player.blackCts.Token, true);
                    }

                    isWhiteTurn = !isWhiteTurn;
                }
                else
                {
                    Console.WriteLine("Invalid move. Try again.");
                }
            }

            Player.whiteCts.Cancel();
            Player.blackCts.Cancel();
            if (Player.whiteTimerTask != null) await Player.whiteTimerTask;
            if (Player.blackTimerTask != null) await Player.blackTimerTask;
        }
    }
}


