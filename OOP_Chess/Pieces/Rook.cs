namespace OOP_Chess
{
    public class Rook : Piece
    {
        public Rook(bool isWhite) : base(isWhite) { }

        public override bool IsValidMove(Position from, Position to, Board board)
        {
            if (from.Row != to.Row && from.Col != to.Col)
                return false;

            int rowStep = to.Row > from.Row ? 1 : (to.Row < from.Row ? -1 : 0);
            int colStep = to.Col > from.Col ? 1 : (to.Col < from.Col ? -1 : 0);

            int currRow = from.Row + rowStep;
            int currCol = from.Col + colStep;

            while (currRow != to.Row || currCol != to.Col)
            {
                if (board.GetPiece(new Position(currRow, currCol)) != null)
                    return false;
                currRow += rowStep;
                currCol += colStep;
            }

            return board.GetPiece(to) == null || board.GetPiece(to).IsWhite != this.IsWhite;
        }

        public override string GetSymbol() => IsWhite ? "♖" : "♜";
    }
}