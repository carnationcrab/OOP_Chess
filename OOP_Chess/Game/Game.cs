using System;

namespace OOP_Chess
{
    /// <summary>
    /// Represents a chess game
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Gets the chess board
        /// </summary>
        public Board Board { get; private set; }

        /// <summary>
        /// Creates a new chess game
        /// </summary>
        public Game()
        {
            Board = new Board();
        }

        /// <summary>
        /// Attempts to move a piece from one position to another
        /// </summary>
        /// <param name="from">The starting position</param>
        /// <param name="to">The target position</param>
        /// <returns>True if the move was successful, false otherwise</returns>
        public bool TryMove(Position from, Position to)
        {
            return Board.TryMove(from, to);
        }

        /// <summary>
        /// Undoes the last move
        /// </summary>
        /// <returns>True if a move was undone, false if there are no moves to undo</returns>
        public bool UndoMove()
        {
            return Board.UndoMove();
        }

        /// <summary>
        /// Redoes the last undone move
        /// </summary>
        /// <returns>True if a move was redone, false if there are no moves to redo</returns>
        public bool RedoMove()
        {
            return Board.RedoMove();
        }
    }
}