namespace OOP_Chess
{
    /// <summary>
    /// Represents a position on the chess board
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Properties
        /// </summary>
        
        public int Row { get; set; }
        public int Col { get; set; }

        /// <summary>
        /// Creates a new position
        /// </summary>
        /// <param name="row">The row (0-7)</param>
        /// <param name="col">The column (0-7)</param>
        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
} 