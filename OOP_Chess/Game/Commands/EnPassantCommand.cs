using OOP_Chess.Pieces;
using System;
using OOP_Chess.Game;

namespace OOP_Chess.Game.Commands
{
    /// <summary>
    /// Command class for handling en passant captures
    /// </summary>
    public class EnPassantCommand : ICommand
    {
        private readonly Board board;
        private readonly Position pawnFrom;
        private readonly Position pawnTo;
        private readonly Position capturedPawnPosition;
        private readonly Piece capturedPawn;

        /// <summary>
        /// Creates a new en passant command
        /// </summary>
        /// <param name="board">The chess board</param>
        /// <param name="pawnFrom">Starting position of the capturing pawn</param>
        /// <param name="pawnTo">Target position of the capturing pawn</param>
        /// <param name="capturedPawnPosition">Position of the captured pawn</param>
        /// <param name="capturedPawn">The captured pawn piece</param>
        public EnPassantCommand(Board board, Position pawnFrom, Position pawnTo, Position capturedPawnPosition, Piece capturedPawn)
        {
            this.board = board;
            this.pawnFrom = pawnFrom;
            this.pawnTo = pawnTo;
            this.capturedPawnPosition = capturedPawnPosition;
            this.capturedPawn = capturedPawn;
        }

        /// <summary>
        /// Executes the en passant command
        /// </summary>
        /// <returns>True if the en passant was executed successfully, false otherwise</returns>
        public bool Execute()
        {
            try
            {
                board.MovePiece(pawnFrom, pawnTo);
                board.SetPiece(capturedPawnPosition, null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Undoes the en passant command
        /// </summary>
        public void Undo()
        {
            board.MovePiece(pawnTo, pawnFrom);
            board.SetPiece(capturedPawnPosition, capturedPawn);
        }

        /// <summary>
        /// Redoes the en passant command
        /// </summary>
        /// <returns>True if the en passant was redone successfully, false otherwise</returns>
        public bool Redo()
        {
            return Execute();
        }
    }
} 