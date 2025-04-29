using System;

namespace OOP_Chess
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
            switch (pieceType)
            {
                case PieceType.Pawn:
                    return new Piece(isWhite, isWhite ? "♙" : "♟", new PawnMoveStrategy());
                case PieceType.Rook:
                    return new Piece(isWhite, isWhite ? "♖" : "♜", new RookMoveStrategy());
                case PieceType.Knight:
                    return new Piece(isWhite, isWhite ? "♘" : "♞", new KnightMoveStrategy());
                case PieceType.Bishop:
                    return new Piece(isWhite, isWhite ? "♗" : "♝", new BishopMoveStrategy());
                case PieceType.Queen:
                    return new Piece(isWhite, isWhite ? "♕" : "♛", new QueenMoveStrategy());
                case PieceType.King:
                    return new Piece(isWhite, isWhite ? "♔" : "♚", new KingMoveStrategy());
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