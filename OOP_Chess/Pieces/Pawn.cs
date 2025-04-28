using System;

namespace OOP_Chess
{
    public class Pawn : Piece
    {
        public Pawn(bool isWhite) : base(isWhite) { }

        public override bool IsValidMove(Position from, Position to, Board board)
        {
            int direction = IsWhite ? 1 : -1;
            int startRow = IsWhite ? 1 : 6;

            if (from.Col == to.Col)
            {
                if (to.Row - from.Row == direction && board.GetPiece(to) == null)
                    return true;
                if (from.Row == startRow && to.Row - from.Row == 2 * direction && board.GetPiece(to) == null)
                    return true;
            }
            else if (Math.Abs(from.Col - to.Col) == 1 && to.Row - from.Row == direction)
            {
                if (board.GetPiece(to) != null && board.GetPiece(to).IsWhite != this.IsWhite)
                    return true;
            }
            return false;
        }

        public override string GetSymbol() => IsWhite ? "♙" : "♟";
    }
}
