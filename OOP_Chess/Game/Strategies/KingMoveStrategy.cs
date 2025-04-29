using System;

namespace OOP_Chess
{
    /// <summary>
    /// Strategy for king movement
    /// </summary>
    public class KingMoveStrategy : IMoveStrategy
    {
        /// <summary>
        /// Validates if a king move is legal
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

            // King can move one square in any direction
            if (rowDiff > 1 || colDiff > 1)
                return false;

            // Check if target square is empty or contains an enemy piece
            var targetPiece = board.GetPiece(to);
            return targetPiece == null || targetPiece.IsWhite != isWhite;
        }
    }
} 