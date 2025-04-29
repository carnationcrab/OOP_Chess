using System;
using OOP_Chess.Game;

namespace OOP_Chess.Game.Strategies
{
    /// <summary>
    /// Strategy for knight movement
    /// </summary>
    public class KnightMoveStrategy : IMoveStrategy
    {
        /// <summary>
        /// Validates if a knight move is legal
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

            // Knight moves in an L-shape: 2 squares in one direction and 1 square perpendicular
            if (!((rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2)))
                return false;

            // Check if target square is empty or contains an enemy piece
            var targetPiece = board.GetPiece(to);
            return targetPiece == null || targetPiece.IsWhite != isWhite;
        }
    }
} 