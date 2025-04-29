using System;
using OOP_Chess.Game;

namespace OOP_Chess.Game.Strategies
{
    /// <summary>
    /// Strategy for rook movement
    /// </summary>
    public class RookMoveStrategy : IMoveStrategy
    {
        /// <summary>
        /// Validates if a rook move is legal
        /// </summary>
        /// <param name="from">Starting position</param>
        /// <param name="to">Target position</param>
        /// <param name="board">Current board state</param>
        /// <param name="isWhite">Whether the piece is white</param>
        /// <returns>True if the move is valid, false otherwise</returns>
        public bool IsValidMove(Position from, Position to, Board board, bool isWhite)
        {
            // Rook can only move horizontally or vertically
            if (from.Row != to.Row && from.Col != to.Col)
                return false;

            // Check if the path is clear
            int rowStep = from.Row == to.Row ? 0 : (to.Row - from.Row) / Math.Abs(to.Row - from.Row);
            int colStep = from.Col == to.Col ? 0 : (to.Col - from.Col) / Math.Abs(to.Col - from.Col);

            int currentRow = from.Row + rowStep;
            int currentCol = from.Col + colStep;

            // Check all squares between from and to
            while (currentRow != to.Row || currentCol != to.Col)
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