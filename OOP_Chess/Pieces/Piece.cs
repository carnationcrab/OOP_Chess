namespace OOP_Chess
{
    public abstract class Piece
    {
        public bool IsWhite { get; private set; }

        public Piece(bool isWhite)
        {
            IsWhite = isWhite;
        }

        public abstract bool IsValidMove(Position from, Position to, Board board);
        public abstract string GetSymbol();
    }
}