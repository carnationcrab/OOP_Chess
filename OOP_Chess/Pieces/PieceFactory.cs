using System;
using OOP_Chess.Game.Strategies;

namespace OOP_Chess.Pieces
{
    /// <summary>
    /// Factory class for creating chess pieces
    /// </summary>
    public class PieceFactory
    {
        /// <summary>
        /// Creates a chess piece based on the specified type and color
        /// </summary>
        /// <param name="pieceType">The type of piece to create</param>
        /// <param name="isWhite">Whether the piece is white or black</param>
        /// <returns>A new chess piece</returns>
        public static Piece CreatePiece(PieceType pieceType, bool isWhite)
        {
            IMoveStrategy moveStrategy;
            ICaptureStrategy captureStrategy;

            switch (pieceType)
            {
                case PieceType.Pawn:
                       moveStrategy = new PawnMoveStrategy();
                    captureStrategy = new NormalCaptureStrategy();
                    return new Piece(isWhite, isWhite ? "♙" : "♟", moveStrategy, pieceType, captureStrategy);
                case PieceType.Rook:
                       moveStrategy = new RookMoveStrategy();
                    captureStrategy = new NormalCaptureStrategy();
                    return new Piece(isWhite, isWhite ? "♖" : "♜", moveStrategy, pieceType, captureStrategy);
                case PieceType.Knight:
                       moveStrategy = new KnightMoveStrategy();
                    captureStrategy = new NormalCaptureStrategy();
                    return new Piece(isWhite, isWhite ? "♘" : "♞", moveStrategy, pieceType, captureStrategy);
                case PieceType.Bishop:
                       moveStrategy = new BishopMoveStrategy();
                    captureStrategy = new NormalCaptureStrategy();
                    return new Piece(isWhite, isWhite ? "♗" : "♝", moveStrategy, pieceType, captureStrategy);
                case PieceType.Queen:
                       moveStrategy = new QueenMoveStrategy();
                    captureStrategy = new NormalCaptureStrategy();
                    return new Piece(isWhite, isWhite ? "♕" : "♛", moveStrategy, pieceType, captureStrategy);
                case PieceType.King:
                       moveStrategy = new KingMoveStrategy();
                    captureStrategy = new KingCaptureStrategy();
                    return new Piece(isWhite, isWhite ? "♔" : "♚", moveStrategy, pieceType, captureStrategy);
                default:
                    throw new ArgumentException($"Unknown piece type: {pieceType}");
            }
        }
    }

    /// <summary>
    /// Enum representing the different types of chess pieces
    /// </summary>
    public enum PieceType
    {
        Pawn,
        Rook,
        Knight,
        Bishop,
        Queen,
        King
    }
} 