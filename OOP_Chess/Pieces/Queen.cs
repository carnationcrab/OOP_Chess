namespace OOP_Chess
{
    public class Queen : Piece
    {
        public Queen(bool isWhite) : base(isWhite) { }

        public override bool IsValidMove(Position from, Position to, Board board)
        {
            var rook = new Rook(IsWhite);
            var bishop = new Bishop(IsWhite);
            return rook.IsValidMove(from, to, board) || bishop.IsValidMove(from, to, board);
        }

        public override string GetSymbol() => IsWhite ? "♕" : "♛";
    }
}