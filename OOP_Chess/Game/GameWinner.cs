namespace OOP_Chess
{
    /// <summary>
    /// Represents the possible outcomes of a chess game
    /// </summary>
    public enum GameWinner
    {
        /// <summary>
        /// The game is not over
        /// </summary>
        None,

        /// <summary>
        /// White won the game
        /// </summary>
        White,

        /// <summary>
        /// Black won the game
        /// </summary>
        Black,

        /// <summary>
        /// The game ended in a draw
        /// </summary>
        Draw
    }
} 