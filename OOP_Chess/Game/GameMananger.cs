using System;

namespace OOP_Chess
{
    public class GameManager
    {
        private Game currentGame;

        public event Action TurnChanged;
        public event Action BoardChanged;
        public event Action GameEnded;

        public bool IsWhiteTurn => currentGame.TurnManager.IsWhiteTurn;

        public GameResult CurrentResult { get; private set; }

        public GameManager()
        {
            StartNewGame();
        }

        public void StartNewGame()
        {
            currentGame = new Game();
            currentGame.TurnManager.TurnChanged += OnTurnChanged;
            CurrentResult = new GameResult(); // Reset result on new game
            BoardChanged?.Invoke();
            TurnChanged?.Invoke();
        }

        private void OnTurnChanged()
        {
            TurnChanged?.Invoke();
        }

        public Piece[,] GetBoardSnapshot()
        {
            return currentGame.Board.GetBoardSnapshot();
        }

        public bool TryMove(Position from, Position to)
        {
            if (CurrentResult.IsGameOver)
                return false; // Game is over, no moves allowed.

            var capturedPiece = currentGame.Board.GetPiece(to);
            bool moved = currentGame.TryMove(from, to);
            if (moved)
            {
                if (capturedPiece is King)
                {
                    CurrentResult = new GameResult
                    {
                        Winner = currentGame.TurnManager.IsWhiteTurn ? GameWinner.Black : GameWinner.White, // absolute crap, but the turn has already moved on 
                        Reason = "King captured"
                    };
                    GameEnded?.Invoke();
                }

                BoardChanged?.Invoke();
            }
            return moved;
        }

    }
}
