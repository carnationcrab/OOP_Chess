using OOP_Chess.Pieces;
using OOP_Chess.Game;

namespace OOP_Chess.Game.Strategies
{
    public class NormalCaptureStrategy : ICaptureStrategy
    {
        public bool CanCapture(Piece piece, int targetX, int targetY, Board board)
        {
            Position targetPos = new Position(targetX, targetY);
            Piece targetPiece = board.GetPiece(targetPos);
            return targetPiece != null && targetPiece.IsWhite != piece.IsWhite;
        }

        public void Capture(Piece piece, GameManager gameManager)
        {
            piece.MarkAsCaptured();
        }
    }
}
