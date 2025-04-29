using System;
using OOP_Chess.Pieces;

namespace OOP_Chess.Game.Commands
{
    /// <summary>
    /// Command class for moving pieces on the board
    /// </summary>
    public class MoveCommand : ICommand
    {
        private readonly Board board;
        private readonly Position from;
        private readonly Position to;
        private readonly Piece capturedPiece;
        private readonly Piece movingPiece;
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
            this.movingPiece = board.GetPiece(from);
            this.gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
        }

        /// <summary>
        /// Performs a move command
        /// </summary>
        public void Execute()
        {
            if (capturedPiece != null)
            {
                capturedPiece.Capture(gameManager);
            }

            board.MovePiece(from, to);
        }

        /// <summary>
        /// Undoes the move command
        /// </summary>
        public void Undo()
        {
            board.MovePiece(to, from);

            if (capturedPiece != null)
            {
                capturedPiece.Revive();
                board.SetPiece(to, capturedPiece);
            }
        }
    }
} 