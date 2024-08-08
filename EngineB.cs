using Chess_game;

public class Engine2 : IGameEngine
{
    public void InitializeBoard()
    {
        // Empty squares

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Board.board[i, j] = " -";
            }
        }

        // Pawns

        for (int i = 0; i < 8; i++)
        {
            Board.board[1, i] = "wP";
            Board.board[6, i] = "bP";
        }

        // Rooks

        Board.board[0, 0] = Board.board[0, 7] = "wR";
        Board.board[7, 0] = Board.board[7, 7] = "bR";

        // Knights

        Board.board[0, 1] = Board.board[0, 6] = "wN";
        Board.board[7, 1] = Board.board[7, 6] = "bN";

        // Bishops

        Board.board[0, 2] = Board.board[0, 5] = "wB";
        Board.board[7, 2] = Board.board[7, 5] = "bB";

        // Queens

        Board.board[0, 3] = "wQ";
        Board.board[7, 3] = "bQ";

        // Kings

        Board.board[0, 4] = "wK";
        Board.board[7, 4] = "bK";
    }

    public void PrintBoard()
    {
        Console.Clear();

        // 1 to 8 order column 

        Console.WriteLine();
        for (int i = 0; i < 8; i++)
        {
            Console.Write(8 - i + "  ");
            for (int j = 0; j < 8; j++)
            {
                Console.Write(Board.board[i, j] + "  ");
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
    }

    public bool IsMoveValid(string move, bool isWhiteTurn)
    {
        return Pieces.IsMoveValid(move, !isWhiteTurn);
    }

    public void MovePiece(string move)
    {
        Pieces.MovePiece(move);
    }

    public bool IsCheckmate(bool isWhiteTurn)
    {
        return Pieces.IsCheckmate(isWhiteTurn);
    }

    public bool IsStalemate(bool isWhiteTurn)
    {
        return Pieces.IsStalemate(isWhiteTurn);
    }
}
