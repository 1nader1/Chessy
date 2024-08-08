using static ChessConsoleApp.Chessy;

public class RookMovementStrategy : IMovementStrategy
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

        if (startX != endX && startY != endY)
        {
            return false;
        }

        int xDirection = startX == endX ? 0 : (endX - startX) / Math.Abs(endX - startX);
        int yDirection = startY == endY ? 0 : (endY - startY) / Math.Abs(endY - startY);

        int x = startX + xDirection;
        int y = startY + yDirection;

        while (x != endX || y != endY)
        {
            if (board[x, y] != " -")
                return false;
            x += xDirection;
            y += yDirection;
        }

        string targetPiece = board[endX, endY];
        if (targetPiece == " -" || targetPiece[0] != (isWhiteTurn ? 'w' : 'b'))
        {
            return true;
        }

        return false;
    }
}