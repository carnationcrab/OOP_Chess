namespace OOP_Chess
{
    /// <summary>
    /// Represents the result of a chess game
    /// </summary>
    public class GameResult
    {
        /// <summary>
        /// Gets or sets whether the game is over
        /// </summary>
        public bool IsGameOver { get; set; }

        /// <summary>
        /// Gets or sets the winner of the game
        /// </summary>
        public GameWinner Winner { get; set; }

        /// <summary>
        /// Gets or sets the reason for the game ending
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Creates a new game result
        /// </summary>
        public GameResult()
        {
            IsGameOver = false;
            Winner = GameWinner.None;
            Reason = null;
        }
    }
}
