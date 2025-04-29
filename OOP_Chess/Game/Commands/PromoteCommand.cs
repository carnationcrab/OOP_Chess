using OOP_Chess.Pieces;
using System;
using OOP_Chess.Game;

namespace OOP_Chess.Game.Commands
{
    /// <summary>
    /// Command class for handling pawn promotion
    /// </summary>
    public class PromoteCommand : ICommand
    {
        private readonly Board board;
        private readonly Position position;
        private readonly Piece originalPiece;
        private readonly Piece promotedPiece;

        /// <summary>
        /// Creates a new promote command
        /// </summary>
        /// <param name="board">The chess board</param>
        /// <param name="position">The position of the pawn to promote</param>
        /// <param name="originalPiece">The original pawn piece</param>
        /// <param name="promotedPiece">The piece to promote to</param>
        public PromoteCommand(Board board, Position position, Piece originalPiece, Piece promotedPiece)
        {
            this.board = board;
            this.position = position;
            this.originalPiece = originalPiece;
            this.promotedPiece = promotedPiece;
        }

        /// <summary>
        /// Executes the promote command
        /// </summary>
        /// <returns>True if the promotion was executed successfully, false otherwise</returns>
        public bool Execute()
        {
            try
            {
                board.SetPiece(position, promotedPiece);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Undoes the promote command
        /// </summary>
        public void Undo()
        {
            board.SetPiece(position, originalPiece);
        }

        /// <summary>
        /// Redoes the promote command
        /// </summary>
        /// <returns>True if the promotion was redone successfully, false otherwise</returns>
        public bool Redo()
        {
            return Execute();
        }
    }
} 