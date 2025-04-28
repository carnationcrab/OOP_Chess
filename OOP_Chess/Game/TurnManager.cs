using System;

namespace OOP_Chess
{
    public class TurnManager
    {
        public bool IsWhiteTurn { get; private set; } = true;

        public event Action TurnChanged;

        public void AdvanceTurn()
        {
            IsWhiteTurn = !IsWhiteTurn;
            TurnChanged?.Invoke();
        }

        public string GetTurnText()
        {
            return IsWhiteTurn ? "White's Turn" : "Black's Turn";
        }
    }
}
