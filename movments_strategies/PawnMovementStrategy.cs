using static ChessConsoleApp.Chessy;

public class PawnMovementStrategy : IMovementStrategy
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

        int direction = isWhiteTurn ? -1 : 1;
        string targetPiece = board[endX, endY];

        // Regular move
        if (startY == endY && board[endX, endY] == " -")
        {
            if (endX == startX + direction)
            {
                return true;
            }
            else if ((isWhiteTurn && startX == 6) || (!isWhiteTurn && startX == 1))
            {
                if (endX == startX + 2 * direction && board[startX + direction, startY] == " -")
                {
                    return true;
                }
            }
        }

        // Capturing move
        else if (Math.Abs(startY - endY) == 1 && endX == startX + direction && targetPiece != " -" && targetPiece[0] != (isWhiteTurn ? 'w' : 'b'))
        {
            return true;
        }

        return false;
    }
}