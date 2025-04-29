using System;

namespace OOP_Chess
{
    /// <summary>
    /// Strategy for bishop movement
    /// </summary>
    public class BishopMoveStrategy : IMoveStrategy
    {
        /// <summary>
        /// Validates if a bishop move is legal
        /// </summary>
        /// <param name="from">Starting position</param>
        /// <param name="to">Target position</param>
        /// <param name="board">Current board state</param>
        /// <param name="isWhite">Whether the piece is white</param>
        /// <returns>True if the move is valid, false otherwise</returns>
        public bool IsValidMove(Position from, Position to, Board board, bool isWhite)
        {
            int rowDiff = Math.Abs(to.Row - from.Row);
            int colDiff = Math.Abs(to.Col - from.Col);

            // Bishop can only move diagonally
            if (rowDiff != colDiff)
                return false;

            // Check if the path is clear
            int rowStep = (to.Row - from.Row) / rowDiff;
            int colStep = (to.Col - from.Col) / colDiff;

            int currentRow = from.Row + rowStep;
            int currentCol = from.Col + colStep;

            // Check all squares between from and to
            while (currentRow != to.Row)
            {
                if (board.GetPiece(new Position(currentRow, currentCol)) != null)
                    return false;

                currentRow += rowStep;
                currentCol += colStep;
            }

            // Check if target square is empty or contains an enemy piece
            var targetPiece = board.GetPiece(to);
            return targetPiece == null || targetPiece.IsWhite != isWhite;
        }
    }
} 