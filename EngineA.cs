using Chess_game;

public class Engine1 : IGameEngine
{
    public void InitializeBoard()
    {
        Board.InitializeBoard();
    }

    public void PrintBoard()
    {
        Board.PrintBoard();
    }

    public bool IsMoveValid(string move, bool isWhiteTurn)
    {
        return Pieces.IsMoveValid(move, isWhiteTurn);
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
