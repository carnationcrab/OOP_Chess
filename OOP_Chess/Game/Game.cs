using System;

namespace OOP_Chess.Game
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
        /// Gets whether it's white's turn
        /// </summary>
        public bool IsWhiteTurn => Board.IsWhiteTurn;

        /// <summary>
        /// Creates a new chess game with the specified board
        /// </summary>
        /// <param name="board">The board to use</param>
        public Game(Board board)
        {
            Board = board ?? throw new ArgumentNullException(nameof(board));
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