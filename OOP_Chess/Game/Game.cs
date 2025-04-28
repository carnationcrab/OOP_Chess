using OOP_Chess;

namespace OOP_Chess
{
    public class Game
    {
        public Board Board { get; private set; }
        public bool WhiteTurn { get; private set; } = true;

        public Game()
        {
            Board = new Board();
            Board.SetupInitialPosition();
        }

        public bool TryMove(Position from, Position to)
        {
            var piece = Board.GetPiece(from);
            if (piece == null) return false;

            if (piece.IsValidMove(from, to, Board))
            {
                Board.MovePiece(from, to);
                WhiteTurn = !WhiteTurn;
                return true;
            }

            return false;
        }
    }
}