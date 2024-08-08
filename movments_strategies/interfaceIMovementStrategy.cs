public interface IMovementStrategy
{
    bool IsValidMove(string[,] board, string move, bool isWhiteTurn);

}