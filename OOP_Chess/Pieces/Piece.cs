using System;
using OOP_Chess.Game.Strategies;
using OOP_Chess.Game;

namespace OOP_Chess.Pieces
{
    /// <summary>
    /// Represents a chess piece
    /// </summary>
    public class Piece
    {
        /// <summary>
        /// Properties
        /// </summary>
        /// 

        private readonly IMoveStrategy moveStrategy;
        private readonly OOP_Chess.Game.Strategies.ICaptureStrategy captureStrategy;
        private readonly PieceType pieceType;

        public bool   IsCaptured { get; private set; }
        public bool   IsWhite    { get; }
        public string Symbol     { get; } // icon for piece
        public PieceType Type => pieceType;

        public Piece(bool isWhite, string symbol, IMoveStrategy moveStrategy, PieceType pieceType, OOP_Chess.Game.Strategies.ICaptureStrategy captureStrategy)
        {
            this.IsWhite = isWhite;
            this.Symbol = symbol;
            this.moveStrategy = moveStrategy;
            this.pieceType = pieceType;
            this.captureStrategy = captureStrategy;
            this.IsCaptured = false;
        }

        public void MarkAsCaptured()
        {
            this.IsCaptured = true;
        }

        public void Capture(OOP_Chess.Game.GameManager gameManager)
        {
            captureStrategy.Capture(this, gameManager);
        }

        public void Revive()
        {
            this.IsCaptured = false;
        }

        /// <summary>
        /// Validates if a move is legal according to the piece's rules
        /// </summary>
        /// <param name="from">Starting position</param>
        /// <param name="to">Target position</param>
        /// <param name="board">Current board state</param>
        /// <returns>True if the move is valid, false otherwise</returns>
        public bool IsValidMove(Position from, Position to, Board board)
        {
            if (IsCaptured) return false;
            return moveStrategy.IsValidMove(from, to, board, IsWhite);
        }

        /// <summary>
        /// Gets the symbol representing this piece
        /// </summary>
        /// <returns>The piece's symbol</returns>
        public override string ToString() => Symbol;

        /// <summary>
        /// Type checking methods for each piece type
        /// </summary>
        public bool IsPawn() => pieceType == PieceType.Pawn;
        public bool IsRook() => pieceType == PieceType.Rook;
        public bool IsKnight() => pieceType == PieceType.Knight;
        public bool IsBishop() => pieceType == PieceType.Bishop;
        public bool IsQueen() => pieceType == PieceType.Queen;
        public bool IsKing() => pieceType == PieceType.King;
    }
}