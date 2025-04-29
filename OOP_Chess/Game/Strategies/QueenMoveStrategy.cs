using System;
using OOP_Chess.Game;

namespace OOP_Chess.Game.Strategies
{
    /// <summary>
    /// Strategy for queen movement
    /// </summary>
    public class QueenMoveStrategy : IMoveStrategy
    {
        private readonly RookMoveStrategy rookStrategy;
        private readonly BishopMoveStrategy bishopStrategy;

        /// <summary>
        /// Creates a new queen move strategy
        /// </summary>
        public QueenMoveStrategy()
        {
            rookStrategy = new RookMoveStrategy();
            bishopStrategy = new BishopMoveStrategy();
        }

        /// <summary>
        /// Validates if a queen move is legal
        /// </summary>
        /// <param name="from">Starting position</param>
        /// <param name="to">Target position</param>
        /// <param name="board">Current board state</param>
        /// <param name="isWhite">Whether the piece is white</param>
        /// <returns>True if the move is valid, false otherwise</returns>
        public bool IsValidMove(Position from, Position to, Board board, bool isWhite)
        {
            // Queen can move like a rook or bishop
            return rookStrategy.IsValidMove(from, to, board, isWhite) ||
                   bishopStrategy.IsValidMove(from, to, board, isWhite);
        }
    }
} 