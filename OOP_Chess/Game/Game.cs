using OOP_Chess;

namespace OOP_Chess
{
    public class Game
    {
        public Board Board { get; private set; }
        public TurnManager TurnManager { get; private set; }


        public Game()
        {
            Board = new Board();
            Board.SetupInitialPosition();
            TurnManager = new TurnManager();
        }

        public bool TryMove(Position from, Position to)
        {
            var piece = Board.GetPiece(from);
            if (piece == null) return false;

            if (piece.IsValidMove(from, to, Board))
            {
                Board.MovePiece(from, to);
                TurnManager.AdvanceTurn();
                return true;
            }

            return false;
        }
    }
}