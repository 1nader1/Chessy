using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessConsoleApp;

namespace Chess_game
{
    public static class Pieces
    {
        //Return the appropriate movement strategy for pices
        public static IMovementStrategy GetMovementStrategy(string piece)
        {
            switch (piece[1])
            {
                case 'P': return new PawnMovementStrategy();
                case 'R': return new RookMovementStrategy();
                case 'N': return new KnightMovementStrategy();
                case 'B': return new BishopMovementStrategy();
                case 'Q': return new QueenMovementStrategy();
                case 'K': return new KingMovementStrategy();
                default: throw new ArgumentException("Invalid piece type");
            }
        }

        public static bool IsMoveValid(string move, bool isWhiteTurn)
        {
            if (move.Length != 4)
                return false;

            char startCol = move[0];
            char startRow = move[1];
            char endCol = move[2];
            char endRow = move[3];

            if (startCol < 'A' || startCol > 'H' || endCol < 'A' || endCol > 'H' ||
                startRow < '1' || startRow > '8' || endRow < '1' || endRow > '8')
                return false;

            int startX = 8 - (startRow - '0');
            int startY = startCol - 'A';
            int endX = 8 - (endRow - '0');
            int endY = endCol - 'A';

            string piece = Board.board[startX, startY];

            if (piece == " -")
                return false;

            IMovementStrategy strategy = GetMovementStrategy(piece);
            return strategy.IsValidMove(Board.board, move, isWhiteTurn);
        }

        public static void MovePiece(string move)
        {
            char startCol = move[0];
            char startRow = move[1];
            char endCol = move[2];
            char endRow = move[3];

            int startX = 8 - (startRow - '0');
            int startY = startCol - 'A';
            int endX = 8 - (endRow - '0');
            int endY = endCol - 'A';

            Board.board[endX, endY] = Board.board[startX, startY];
            Board.board[startX, startY] = " -";
        }

        public static bool IsKingInCheck(bool isWhiteTurn)
        {
            // Find the king's position

            string king = isWhiteTurn ? "wK" : "bK";
            int kingX = -1, kingY = -1;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Board.board[i, j] == king)
                    {
                        kingX = i;
                        kingY = j;
                        break;
                    }
                }
                if (kingX != -1) break;
            }

            // Check if any piece can attack the king

            bool isCheck = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((isWhiteTurn && Board.board[i, j].StartsWith("b")) || (!isWhiteTurn && Board.board[i, j].StartsWith("w")))
                    {
                        string move = $"{(char)('A' + j)}{(8 - i)}{(char)('A' + kingY)}{(8 - kingX)}";
                        if (IsMoveValid(move, !isWhiteTurn))
                        {
                            isCheck = true;
                            break;
                        }

                    }
                }
                if (isCheck) break;
            }

            return isCheck;
        }

        public static bool HasAnyLegalMoves(bool isWhiteTurn)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((isWhiteTurn && Board.board[i, j].StartsWith("w")) || (!isWhiteTurn && Board.board[i, j].StartsWith("b")))
                    {
                        for (int x = 0; x < 8; x++)
                        {
                            for (int y = 0; y < 8; y++)
                            {
                                string move = $"{(char)('A' + j)}{(8 - i)}{(char)('A' + y)}{(8 - x)}";
                                if (IsMoveValid(move, isWhiteTurn))
                                {
                                    string originalPiece = Board.board[x, y];
                                    MovePiece(move);
                                    bool stillInCheck = IsKingInCheck(isWhiteTurn);
                                    MovePiece($"{(char)('A' + y)}{(8 - x)}{(char)('A' + j)}{(8 - i)}");
                                    Board.board[x, y] = originalPiece;

                                    if (!stillInCheck) return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }



        public static bool IsCheckmate(bool isWhiteTurn)
        {
            return IsKingInCheck(isWhiteTurn) && !HasAnyLegalMoves(isWhiteTurn);
        }

        public static bool IsStalemate(bool isWhiteTurn)
        {
            return !IsKingInCheck(isWhiteTurn) && !HasAnyLegalMoves(isWhiteTurn);
        }
    }
}
