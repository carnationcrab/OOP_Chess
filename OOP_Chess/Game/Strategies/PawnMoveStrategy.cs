using System;
using OOP_Chess.Game;

namespace OOP_Chess.Game.Strategies
{
    /// <summary>
    /// Strategy for pawn movement
    /// </summary>
    public class PawnMoveStrategy : IMoveStrategy
    {
        /// <summary>
        /// Validates if a pawn move is legal
        /// </summary>
        /// <param name="from">Starting position</param>
        /// <param name="to">Target position</param>
        /// <param name="board">Current board state</param>
        /// <param name="isWhite">Whether the piece is white</param>
        /// <returns>True if the move is valid, false otherwise</returns>
        public bool IsValidMove(Position from, Position to, Board board, bool isWhite)
        {
            int direction = isWhite ? 1 : -1;
            int startRow = isWhite ? 1 : 6;

            // Forward move
            if (from.Col == to.Col)
            {
                // Move one square forward
                if (to.Row - from.Row == direction && board.GetPiece(to) == null)
                    return true;
                
                // Move two squares from starting position
                if (from.Row == startRow && to.Row - from.Row == 2 * direction && board.GetPiece(to) == null)
                {
                    // Check if the square in between is empty
                    Position middlePos = new Position(from.Row + direction, from.Col);
                    return board.GetPiece(middlePos) == null;
                }
            }
            // Diagonal capture
            else if (Math.Abs(from.Col - to.Col) == 1 && to.Row - from.Row == direction)
            {
                var targetPiece = board.GetPiece(to);
                if (targetPiece != null && targetPiece.IsWhite != isWhite)
                    return true;
            }
            
            return false;
        }
    }
} 