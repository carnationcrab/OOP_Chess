using System;

namespace OOP_Chess
{
    public class GameManager
    {
        private Game currentGame;

        public event Action TurnChanged;
        public event Action BoardChanged;

        public bool IsWhiteTurn => currentGame.TurnManager.IsWhiteTurn;

        public GameManager()
        {
            StartNewGame();
        }

        public void StartNewGame()
        {
            currentGame = new Game();
            currentGame.TurnManager.TurnChanged += OnTurnChanged;
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
            bool moved = currentGame.TryMove(from, to);
            if (moved)
            {
                BoardChanged?.Invoke();
            }
            return moved;
        }
    }
}
