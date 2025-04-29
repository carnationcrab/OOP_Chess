namespace OOP_Chess
{
    /// <summary>
    /// Represents the result of a chess game
    /// </summary>
    public class GameResult
    {
        /// <summary>
        /// Properties
        /// </summary>
        /// 

        public bool       IsGameOver   { get; set; }
        public GameWinner Winner       { get; set; }
        public string     Reason       { get; set; }

        // win conditions

        public bool CurrentPlayerWon => Winner == GameWinner.CurrentPlayer;

        public bool OpponentWon      => Winner == GameWinner.Opponent;

        public bool IsDraw           => Winner == GameWinner.Draw;

        /// <summary>
        /// Creates a new game result
        /// </summary>
        public GameResult()
        {
            IsGameOver = false;
            Winner     = GameWinner.None;
            Reason     = null;
        }
    }
}
