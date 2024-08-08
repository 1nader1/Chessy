using static ChessConsoleApp.Chessy;

public class BishopMovementStrategy : IMovementStrategy
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

        if (deltaX == deltaY && deltaX!=0 && deltaY!=0)
        {

                int xDirection = (endX - startX) / deltaX;
                int yDirection = (endY - startY) / deltaY;

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
        }
        

        return false;
    }
}