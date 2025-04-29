using System;

namespace OOP_Chess
{
    /// <summary>
    /// Manages the chess game
    /// </summary>
    public class GameManager
    {
        private Game currentGame;

        /// <summary>
        /// Event raised when the turn changes
        /// </summary>
        public event Action TurnChanged;

        /// <summary>
        /// Event raised when the board changes
        /// </summary>
        public event Action BoardChanged;

        /// <summary>
        /// Event raised when the game ends
        /// </summary>
        public event Action GameEnded;

        /// <summary>
        /// Gets whether it is white's turn
        /// </summary>
        public bool IsWhiteTurn => currentGame.Board.IsWhiteTurn;

        /// <summary>
        /// Gets the current game result
        /// </summary>
        public GameResult CurrentResult { get; private set; }

        /// <summary>
        /// Creates a new game manager
        /// </summary>
        public GameManager()
        {
            StartNewGame();
        }

        /// <summary>
        /// Starts a new game
        /// </summary>
        public void StartNewGame()
        {
            currentGame = new Game();
            CurrentResult = new GameResult();
            BoardChanged?.Invoke();
            TurnChanged?.Invoke();
        }

        /// <summary>
        /// Gets a snapshot of the current board state
        /// </summary>
        /// <returns>A 2D array representing the board state</returns>
        public Piece[,] GetBoardSnapshot()
        {
            return currentGame.Board.GetBoardSnapshot();
        }

        /// <summary>
        /// Attempts to move a piece from one position to another
        /// </summary>
        /// <param name="from">The starting position</param>
        /// <param name="to">The target position</param>
        /// <returns>True if the move was successful, false otherwise</returns>
        public bool TryMove(Position from, Position to)
        {
            if (CurrentResult.IsGameOver)
                return false;

            if (currentGame.TryMove(from, to))
            {
                BoardChanged?.Invoke();
                TurnChanged?.Invoke();

                // Check for checkmate
                if (IsInCheckmate(!IsWhiteTurn))
                {
                    CurrentResult.IsGameOver = true;
                    CurrentResult.Winner = IsWhiteTurn ? GameWinner.Black : GameWinner.White;
                    CurrentResult.Reason = "Checkmate";
                    GameEnded?.Invoke();
                }
                // Check for stalemate
                else if (IsInStalemate(!IsWhiteTurn))
                {
                    CurrentResult.IsGameOver = true;
                    CurrentResult.Winner = GameWinner.Draw;
                    CurrentResult.Reason = "Stalemate";
                    GameEnded?.Invoke();
                }
                // Check for insufficient material
                else if (HasInsufficientMaterial())
                {
                    CurrentResult.IsGameOver = true;
                    CurrentResult.Winner = GameWinner.Draw;
                    CurrentResult.Reason = "Insufficient Material";
                    GameEnded?.Invoke();
                }

                return true;
            }
            return false;
        }

        /// <summary>
        /// Undoes the last move
        /// </summary>
        /// <returns>True if a move was undone, false if there are no moves to undo</returns>
        public bool UndoMove()
        {
            if (currentGame.UndoMove())
            {
                // Reset game result if game was over
                if (CurrentResult.IsGameOver)
                {
                    CurrentResult = new GameResult();
                }
                BoardChanged?.Invoke();
                TurnChanged?.Invoke();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Redoes the last undone move
        /// </summary>
        /// <returns>True if a move was redone, false if there are no moves to redo</returns>
        public bool RedoMove()
        {
            if (currentGame.RedoMove())
            {
                BoardChanged?.Invoke();
                TurnChanged?.Invoke();
                return true;
            }
            return false;
        }

        private bool IsInCheckmate(bool isWhite)
        {
            // TODO: Implement checkmate detection
            return false;
        }

        private bool IsInStalemate(bool isWhite)
        {
            // TODO: Implement stalemate detection
            return false;
        }

        private bool HasInsufficientMaterial()
        {
            // TODO: Implement insufficient material detection
            return false;
        }
    }
}
