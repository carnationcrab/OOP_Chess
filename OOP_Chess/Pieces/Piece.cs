using System;

namespace OOP_Chess
{
    /// <summary>
    /// Represents a chess piece
    /// </summary>
    public class Piece
    {
        private readonly IMoveStrategy moveStrategy;

        /// <summary>
        /// Gets whether the piece is white
        /// </summary>
        public bool IsWhite { get; }

        /// <summary>
        /// Gets the symbol representing this piece
        /// </summary>
        public string Symbol { get; }

        /// <summary>
        /// Creates a new chess piece
        /// </summary>
        /// <param name="isWhite">Whether the piece is white</param>
        /// <param name="symbol">The symbol representing this piece</param>
        /// <param name="moveStrategy">The strategy for validating moves</param>
        public Piece(bool isWhite, string symbol, IMoveStrategy moveStrategy)
        {
            IsWhite = isWhite;
            Symbol = symbol;
            this.moveStrategy = moveStrategy ?? throw new ArgumentNullException(nameof(moveStrategy));
        }

        /// <summary>
        /// Validates if a move is legal according to the piece's rules
        /// </summary>
        /// <param name="from">Starting position</param>
        /// <param name="to">Target position</param>
        /// <param name="board">Current board state</param>
        /// <returns>True if the move is valid, false otherwise</returns>
        public bool IsValidMove(Position from, Position to, Board board)
        {
            return moveStrategy.IsValidMove(from, to, board, IsWhite);
        }

        /// <summary>
        /// Gets the symbol representing this piece
        /// </summary>
        /// <returns>The piece's symbol</returns>
        public override string ToString() => Symbol;
    }
}