using System;
using OOP_Chess.Game;

namespace OOP_Chess.Game.Commands
{
    /// <summary>
    /// Command class for handling castling moves
    /// </summary>
    public class CastleCommand : ICommand
    {
        private readonly Board board;
        private readonly Position kingFrom;
        private readonly Position kingTo;
        private readonly Position rookFrom;
        private readonly Position rookTo;

        /// <summary>
        /// Creates a new castle command
        /// </summary>
        /// <param name="board">The chess board</param>
        /// <param name="kingFrom">Starting position of the king</param>
        /// <param name="kingTo">Target position of the king</param>
        /// <param name="rookFrom">Starting position of the rook</param>
        /// <param name="rookTo">Target position of the rook</param>
        public CastleCommand(Board board, Position kingFrom, Position kingTo, Position rookFrom, Position rookTo)
        {
            this.board = board;
            this.kingFrom = kingFrom;
            this.kingTo = kingTo;
            this.rookFrom = rookFrom;
            this.rookTo = rookTo;
        }

        /// <summary>
        /// Executes the castle command
        /// </summary>
        public void Execute()
        {
            board.MovePiece(kingFrom, kingTo);
            board.MovePiece(rookFrom, rookTo);
        }

        /// <summary>
        /// Undoes the castle command
        /// </summary>
        public void Undo()
        {
            board.MovePiece(kingTo, kingFrom);
            board.MovePiece(rookTo, rookFrom);
        }
    }
} 