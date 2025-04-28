using System;

namespace OOP_Chess
{
    public class Bishop : Piece
    {
        public Bishop(bool isWhite) : base(isWhite) { }

        public override bool IsValidMove(Position from, Position to, Board board)
        {
            int dRow = Math.Abs(from.Row - to.Row);
            int dCol = Math.Abs(from.Col - to.Col);
            if (dRow != dCol) return false;

            int rowStep = to.Row > from.Row ? 1 : -1;
            int colStep = to.Col > from.Col ? 1 : -1;
            int currRow = from.Row + rowStep;
            int currCol = from.Col + colStep;

            while (currRow != to.Row && currCol != to.Col)
            {
                if (board.GetPiece(new Position(currRow, currCol)) != null)
                    return false;
                currRow += rowStep;
                currCol += colStep;
            }

            return board.GetPiece(to) == null || board.GetPiece(to).IsWhite != this.IsWhite;
        }

        public override string GetSymbol() => IsWhite ? "♗" : "♝";
    }
}