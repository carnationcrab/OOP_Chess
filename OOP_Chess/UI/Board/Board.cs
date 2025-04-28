using OOP_Chess;

namespace OOP_Chess
{
    public class Board
    {
        private Piece[,] grid = new Piece[8, 8];

        public Board()
        {
            SetupInitialPosition();
        }

        public Piece GetPiece(Position pos)
        {
            return grid[pos.Row, pos.Col];
        }

        public void SetPiece(Position pos, Piece piece)
        {
            grid[pos.Row, pos.Col] = piece;
        }

        public void MovePiece(Position from, Position to)
        {
            var piece = GetPiece(from);
            SetPiece(to, piece);
            SetPiece(from, null);
        }

        public void SetupInitialPosition()
        {
            // Set pawns
            for (int col = 0; col < 8; col++)
            {
                SetPiece(new Position(1, col), new Pawn(true));  // White pawns
                SetPiece(new Position(6, col), new Pawn(false)); // Black pawns
            }

            // Set rooks
            SetPiece(new Position(0, 0), new Rook(true));
            SetPiece(new Position(0, 7), new Rook(true));
            SetPiece(new Position(7, 0), new Rook(false));
            SetPiece(new Position(7, 7), new Rook(false));

            // Set knights
            SetPiece(new Position(0, 1), new Knight(true));
            SetPiece(new Position(0, 6), new Knight(true));
            SetPiece(new Position(7, 1), new Knight(false));
            SetPiece(new Position(7, 6), new Knight(false));

            // Set bishops
            SetPiece(new Position(0, 2), new Bishop(true));
            SetPiece(new Position(0, 5), new Bishop(true));
            SetPiece(new Position(7, 2), new Bishop(false));
            SetPiece(new Position(7, 5), new Bishop(false));

            // Set queens
            SetPiece(new Position(0, 3), new Queen(true));
            SetPiece(new Position(7, 3), new Queen(false));

            // Set kings
            SetPiece(new Position(0, 4), new King(true));
            SetPiece(new Position(7, 4), new King(false));
        }

        public Piece[,] GetBoardSnapshot()
        {
            var snapshot = new Piece[8, 8];
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    snapshot[row, col] = grid[row, col];
                }
            }
            return snapshot;
        }
    }
}
