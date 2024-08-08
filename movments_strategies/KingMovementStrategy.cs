using static ChessConsoleApp.Chessy;

public class KingMovementStrategy : IMovementStrategy
{
    public bool IsValidMove(string[,] board, string move, bool isWhiteTurn)
    {
        char startCol = move[0];
        char startRow = move[1];
        char endCol = move[2];
        char endRow = move[3];

        int startX = 8 - (startRow - '0');
        int startY = startCol - 'A';
        int endX = 8 - (endRow - '0');
        int endY = endCol - 'A';

        int deltaX = Math.Abs(endX - startX);
        int deltaY = Math.Abs(endY - startY);

        bool isValidKingMove = deltaX <= 1 && deltaY <= 1;

        if (isValidKingMove)
        {
            string targetPiece = board[endX, endY];
            if (targetPiece == " -" || targetPiece[0] != (isWhiteTurn ? 'w' : 'b'))
            {
                return true;
            }
        }

        return false;
    }
}