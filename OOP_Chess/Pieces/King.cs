using System;

namespace OOP_Chess
{
    public class King : Piece
    {
        public King(bool isWhite) : base(isWhite) { }

        public override bool IsValidMove(Position from, Position to, Board board)
        {
            int dRow = Math.Abs(from.Row - to.Row);
            int dCol = Math.Abs(from.Col - to.Col);
            return (dRow <= 1 && dCol <= 1);
        }

        public override string GetSymbol() => IsWhite ? "♔" : "♚";
    }
}