using System;

namespace OOP_Chess
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

        /// <summary>
        /// Creates a new move command
        /// </summary>
        /// <param name="board">The chess board</param>
        /// <param name="from">Starting position</param>
        /// <param name="to">Target position</param>
        /// <param name="capturedPiece">The piece that was captured (if any)</param>
        public MoveCommand(Board board, Position from, Position to, Piece capturedPiece)
        {
            this.board = board;
            this.from = from;
            this.to = to;
            this.capturedPiece = capturedPiece;
        }

        /// <summary>
        /// Executes the move command
        /// </summary>
        public void Execute()
        {
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
                board.SetPiece(to, capturedPiece);
            }
        }
    }
} 