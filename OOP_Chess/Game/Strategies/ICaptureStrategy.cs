using OOP_Chess.Pieces;
using OOP_Chess.Game;

namespace OOP_Chess.Game.Strategies
{
    public interface ICaptureStrategy
    {
        bool CanCapture(Piece piece, int targetX, int targetY, Board board);
        void Capture(Piece piece, GameManager gameManager);
    }
} 