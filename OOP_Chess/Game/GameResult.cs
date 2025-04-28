public enum GameWinner
{
    None,
    White,
    Black,
    Draw
}

public class GameResult
{
    public GameWinner Winner { get; set; }
    public string Reason { get; set; } // optional: like "Checkmate", "Resignation", etc.

    public bool IsGameOver => Winner != GameWinner.None;

    public GameResult()
    {
        Winner = GameWinner.None;
        Reason = string.Empty;
    }
}
