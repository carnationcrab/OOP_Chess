using System;
using OOP_Chess.Pieces;
using OOP_Chess.Game;
using OOP_Chess.Game.Commands;

namespace OOP_Chess.Game
{
    /// <summary>
    /// Represents the chess board
    /// </summary>
    public class Board
    {
        private Piece[,] grid = new Piece[8, 8];
        private readonly MoveCommandManager commandManager;
        private OOP_Chess.Game.GameManager gameManager;
        private bool isWhiteTurn;

        private Position whiteKingPosition;
        private Position blackKingPosition;

        /// <summary>
        /// Creates a new chess board with standard initialization
        /// </summary>
        public Board()
        {
            grid = new Piece[8, 8];
            commandManager = new MoveCommandManager();
            isWhiteTurn = true;
            InitializeBoard();
        }

        /// <summary>
        /// Sets the game manager for this board
        /// </summary>
        /// <param name="gameManager">The game manager to use</param>
        public void SetGameManager(OOP_Chess.Game.GameManager gameManager)
        {
            this.gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
        }

        public Piece[,] GetBoardSnapshot()
        {
            var snapshot = new Piece[8, 8];
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    snapshot[row, col] = grid[row, col];
                }
            }
            return snapshot;
        }

        public Piece GetPiece(Position position)
        {
            if (!IsValidPosition(position))
                return null;
            return grid[position.Row, position.Col];
        }

        public void SetPiece(Position position, Piece piece)
        {
            if (!IsValidPosition(position))
                throw new ArgumentException("Invalid position");

            grid[position.Row, position.Col] = piece;

            if (piece != null && piece.IsKing())
            {
                if (piece.IsWhite)
                    whiteKingPosition = position;
                else
                    blackKingPosition = position;
            }
        }

        public void MovePiece(Position from, Position to)
        {
            if (!IsValidPosition(from) || !IsValidPosition(to))
                throw new ArgumentException("Invalid position");

            var movingPiece = GetPiece(from);
            var capturedPiece = GetPiece(to);

            if (capturedPiece != null)
            {
                capturedPiece.Capture(gameManager);
            }

            if (movingPiece != null && movingPiece.IsKing())
            {
                if (movingPiece.IsWhite) whiteKingPosition = to;
                else blackKingPosition = to;
            }

            grid[to.Row, to.Col] = grid[from.Row, from.Col];
            grid[from.Row, from.Col] = null;
        }

        public bool TryMove(Position from, Position to)
        {
            if (!IsValidPosition(from) || !IsValidPosition(to)) return false;

            var piece = GetPiece(from);
            if (piece == null || piece.IsWhite != isWhiteTurn || !piece.IsValidMove(from, to, this))
                return false;

            var capturedPiece = GetPiece(to);
            var command = new MoveCommand(this, from, to, capturedPiece, gameManager);
            commandManager.ExecuteCommand(command);

            isWhiteTurn = !isWhiteTurn;
            return true;
        }

        public bool UndoMove()
        {
            if (commandManager.Undo())
            {
                isWhiteTurn = !isWhiteTurn;
                return true;
            }
            return false;
        }

        public bool RedoMove()
        {
            if (commandManager.Redo())
            {
                isWhiteTurn = !isWhiteTurn;
                return true;
            }
            return false;
        }

        public void InitializeBoard()
        {
            grid = new Piece[8, 8];
            isWhiteTurn = true;

            for (int col = 0; col < 8; col++)
            {
                SetPiece(new Position(1, col), OOP_Chess.Pieces.PieceFactory.CreatePiece(PieceType.Pawn, true));
                SetPiece(new Position(6, col), OOP_Chess.Pieces.PieceFactory.CreatePiece(PieceType.Pawn, false));
            }

            SetPiece(new Position(0, 0), OOP_Chess.Pieces.PieceFactory.CreatePiece(PieceType.Rook, true));
            SetPiece(new Position(0, 7), OOP_Chess.Pieces.PieceFactory.CreatePiece(PieceType.Rook, true));
            SetPiece(new Position(7, 0), OOP_Chess.Pieces.PieceFactory.CreatePiece(PieceType.Rook, false));
            SetPiece(new Position(7, 7), OOP_Chess.Pieces.PieceFactory.CreatePiece(PieceType.Rook, false));

            SetPiece(new Position(0, 1), OOP_Chess.Pieces.PieceFactory.CreatePiece(PieceType.Knight, true));
            SetPiece(new Position(0, 6), OOP_Chess.Pieces.PieceFactory.CreatePiece(PieceType.Knight, true));
            SetPiece(new Position(7, 1), OOP_Chess.Pieces.PieceFactory.CreatePiece(PieceType.Knight, false));
            SetPiece(new Position(7, 6), OOP_Chess.Pieces.PieceFactory.CreatePiece(PieceType.Knight, false));

            SetPiece(new Position(0, 2), OOP_Chess.Pieces.PieceFactory.CreatePiece(PieceType.Bishop, true));
            SetPiece(new Position(0, 5), OOP_Chess.Pieces.PieceFactory.CreatePiece(PieceType.Bishop, true));
            SetPiece(new Position(7, 2), OOP_Chess.Pieces.PieceFactory.CreatePiece(PieceType.Bishop, false));
            SetPiece(new Position(7, 5), OOP_Chess.Pieces.PieceFactory.CreatePiece(PieceType.Bishop, false));

            // White: Queen on d1 (white square), King on e1 (black square)
            SetPiece(new Position(0, 3), OOP_Chess.Pieces.PieceFactory.CreatePiece(PieceType.Queen, true));
            SetPiece(new Position(0, 4), OOP_Chess.Pieces.PieceFactory.CreatePiece(PieceType.King, true));

            // Black: King on d8 (black square), Queen on e8 (white square)
            SetPiece(new Position(7, 3), OOP_Chess.Pieces.PieceFactory.CreatePiece(PieceType.King, false));
            SetPiece(new Position(7, 4), OOP_Chess.Pieces.PieceFactory.CreatePiece(PieceType.Queen, false));

            commandManager.Clear();
        }

        public bool IsWhiteTurn => isWhiteTurn;

        public Position WhiteKingPosition => whiteKingPosition;
        public Position BlackKingPosition => blackKingPosition;

        private bool IsValidPosition(Position position)
        {
            return position.Row >= 0 && position.Row < 8 && position.Col >= 0 && position.Col < 8;
        }
    }
}
