namespace OOP_Chess
{
    /// <summary>
    /// Interface for piece movement strategies
    /// </summary>
    public interface IMoveStrategy
    {
        /// <summary>
        /// Validates if a move is legal according to the piece's rules
        /// </summary>
        /// <param name="from">Starting position</param>
        /// <param name="to">Target position</param>
        /// <param name="board">Current board state</param>
        /// <param name="isWhite">Whether the piece is white</param>
        /// <returns>True if the move is valid, false otherwise</returns>
        bool IsValidMove(Position from, Position to, Board board, bool isWhite);
    }
} 