using System;
using OOP_Chess.Game;
using OOP_Chess.Pieces;

namespace OOP_Chess.Game.Commands
{
    /// <summary>
    /// Command class for moving pieces on the board
    /// </summary>
    public class MoveCommand : IMoveCommand
    {
        private readonly Board board;
        private readonly Position from;
        private readonly Position to;
        private readonly Piece capturedPiece;
        private readonly GameManager gameManager;

        /// <summary>
        /// Creates a new move command
        /// </summary>
        /// <param name="board">The chess board</param>
        /// <param name="from">Starting position</param>
        /// <param name="to">Target position</param>
        /// <param name="capturedPiece">The piece that was captured (if any)</param>
        /// <param name="gameManager">The game manager instance</param>
        public MoveCommand(Board board, Position from, Position to, Piece capturedPiece, GameManager gameManager)
        {
            this.board = board;
            this.from = from;
            this.to = to;
            this.capturedPiece = capturedPiece;
            this.gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
        }

        /// <summary>
        /// Performs a move command
        /// </summary>
        /// <returns>True if the move was executed successfully, false otherwise</returns>
        public bool Execute()
        {
            try
            {
                var piece = board.GetPiece(from);
                if (piece == null) return false;
                
                board.MovePiece(from, to);
                gameManager.LogMove(from, to, piece, capturedPiece != null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Undoes the move command
        /// </summary>
        public void Undo()
        {
            var piece = board.GetPiece(to);
            board.SetPiece(from, piece);
            board.SetPiece(to, capturedPiece);
            if (capturedPiece != null)
            {
                capturedPiece.Revive();
            }
        }

        /// <summary>
        /// Redoes the previously undone move
        /// </summary>
        /// <returns>True if the move was redone successfully, false otherwise</returns>
        public bool Redo()
        {
            return Execute();
        }
    }
} 