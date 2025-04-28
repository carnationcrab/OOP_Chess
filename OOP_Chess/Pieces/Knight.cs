using System;

namespace OOP_Chess
{
    public class Knight : Piece
    {
        public Knight(bool isWhite) : base(isWhite) { }

        public override bool IsValidMove(Position from, Position to, Board board)
        {
            int dRow = Math.Abs(from.Row - to.Row);
            int dCol = Math.Abs(from.Col - to.Col);
            return (dRow == 2 && dCol == 1) || (dRow == 1 && dCol == 2);
        }

        public override string GetSymbol() => IsWhite ? "♘" : "♞";
    }
}
