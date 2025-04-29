using System;

namespace OOP_Chess
{
    /// <summary>
    /// Manages the turn state of the chess game
    /// </summary>
    public class TurnManager
    {
        /// <summary>
        /// Gets or sets whether it is white's turn
        /// </summary>
        public bool IsWhiteTurn { get; private set; } = true;

        /// <summary>
        /// Event raised when the turn changes
        /// </summary>
        public event Action TurnChanged;

        /// <summary>
        /// Advances the turn to the next player
        /// </summary>
        public void AdvanceTurn()
        {
            IsWhiteTurn = !IsWhiteTurn;
            TurnChanged?.Invoke();
        }

        /// <summary>
        /// Gets the text representation of the current turn
        /// </summary>
        /// <returns>"White" or "Black"</returns>
        public string GetTurnText()
        {
            return IsWhiteTurn ? "White" : "Black";
        }
    }
}
