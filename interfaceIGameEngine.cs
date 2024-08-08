public interface IGameEngine
{
    void InitializeBoard();
    void PrintBoard();
    bool IsMoveValid(string move, bool isWhiteTurn);
    void MovePiece(string move);
    bool IsCheckmate(bool isWhiteTurn);
    bool IsStalemate(bool isWhiteTurn);
}
