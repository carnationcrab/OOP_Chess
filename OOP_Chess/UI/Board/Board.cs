using System;

namespace OOP_Chess
{
    /// <summary>
    /// Represents the chess board
    /// </summary>
    public class Board
    {
        private Piece[,] grid = new Piece[8, 8];
        private readonly MoveCommandManager commandManager;
        private bool isWhiteTurn;

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
        /// Creates a new chess board with optional initialization
        /// </summary>
        /// <param name="initialize">Whether to initialize the board with standard setup</param>
        public Board(bool initialize)
        {
            grid = new Piece[8, 8];
            commandManager = new MoveCommandManager();
            isWhiteTurn = true;
            
            if (initialize)
            {
                InitializeBoard();
            }
        }

        /// <summary>
        /// Gets a snapshot of the current board state
        /// </summary>
        /// <returns>A 2D array representing the board state</returns>
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

        /// <summary>
        /// Gets the piece at the specified position
        /// </summary>
        /// <param name="position">The position to check</param>
        /// <returns>The piece at the position, or null if the position is empty</returns>
        public Piece GetPiece(Position position)
        {
            if (!IsValidPosition(position))
                return null;
            return grid[position.Row, position.Col];
        }

        /// <summary>
        /// Places a piece on the board at the specified position
        /// </summary>
        /// <param name="piece">The piece to place</param>
        /// <param name="position">The position to place the piece</param>
        public void SetPiece(Position position, Piece piece)
        {
            if (!IsValidPosition(position))
                throw new ArgumentException("Invalid position");
            
            grid[position.Row, position.Col] = piece;
        }

        /// <summary>
        /// Moves a piece from one position to another
        /// </summary>
        /// <param name="from">The starting position</param>
        /// <param name="to">The target position</param>
        public void MovePiece(Position from, Position to)
        {
            if (!IsValidPosition(from) || !IsValidPosition(to))
                throw new ArgumentException("Invalid position");
            
            grid[to.Row, to.Col] = grid[from.Row, from.Col];
            grid[from.Row, from.Col] = null;
        }

        /// <summary>
        /// Attempts to move a piece from one position to another
        /// </summary>
        /// <param name="from">The starting position</param>
        /// <param name="to">The target position</param>
        /// <returns>True if the move was successful, false otherwise</returns>
        public bool TryMove(Position from, Position to)
        {
            if (!IsValidPosition(from) || !IsValidPosition(to))
                return false;

            var piece = GetPiece(from);
            if (piece == null)
                return false;

            // Check if it's the correct player's turn
            if (piece.IsWhite != isWhiteTurn)
                return false;

            // Validate the move according to the piece's rules
            if (!piece.IsValidMove(from, to, this))
                return false;

            // Store the captured piece (if any)
            var capturedPiece = GetPiece(to);

            // Create and execute the move command
            var command = new MoveCommand(this, from, to, capturedPiece);
            commandManager.ExecuteCommand(command);
            
            // Update turn
            isWhiteTurn = !isWhiteTurn;
            return true;
        }

        /// <summary>
        /// Undoes the last move
        /// </summary>
        /// <returns>True if a move was undone, false if there are no moves to undo</returns>
        public bool UndoMove()
        {
            if (commandManager.Undo())
            {
                isWhiteTurn = !isWhiteTurn;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Redoes the last undone move
        /// </summary>
        /// <returns>True if a move was redone, false if there are no moves to redo</returns>
        public bool RedoMove()
        {
            if (commandManager.Redo())
            {
                isWhiteTurn = !isWhiteTurn;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Initializes the board with the standard chess setup
        /// </summary>
        public void InitializeBoard()
        {
            // Clear the board
            grid = new Piece[8, 8];
            isWhiteTurn = true;

            // Set pawns
            for (int col = 0; col < 8; col++)
            {
                SetPiece(new Position(1, col), PieceFactory.CreatePiece(PieceType.Pawn, true));  // White pawns
                SetPiece(new Position(6, col), PieceFactory.CreatePiece(PieceType.Pawn, false)); // Black pawns
            }

            // Set rooks
            SetPiece(new Position(0, 0), PieceFactory.CreatePiece(PieceType.Rook, true));
            SetPiece(new Position(0, 7), PieceFactory.CreatePiece(PieceType.Rook, true));
            SetPiece(new Position(7, 0), PieceFactory.CreatePiece(PieceType.Rook, false));
            SetPiece(new Position(7, 7), PieceFactory.CreatePiece(PieceType.Rook, false));

            // Set knights
            SetPiece(new Position(0, 1), PieceFactory.CreatePiece(PieceType.Knight, true));
            SetPiece(new Position(0, 6), PieceFactory.CreatePiece(PieceType.Knight, true));
            SetPiece(new Position(7, 1), PieceFactory.CreatePiece(PieceType.Knight, false));
            SetPiece(new Position(7, 6), PieceFactory.CreatePiece(PieceType.Knight, false));

            // Set bishops
            SetPiece(new Position(0, 2), PieceFactory.CreatePiece(PieceType.Bishop, true));
            SetPiece(new Position(0, 5), PieceFactory.CreatePiece(PieceType.Bishop, true));
            SetPiece(new Position(7, 2), PieceFactory.CreatePiece(PieceType.Bishop, false));
            SetPiece(new Position(7, 5), PieceFactory.CreatePiece(PieceType.Bishop, false));

            // Set queens
            SetPiece(new Position(0, 3), PieceFactory.CreatePiece(PieceType.Queen, true));
            SetPiece(new Position(7, 3), PieceFactory.CreatePiece(PieceType.Queen, false));

            // Set kings
            SetPiece(new Position(0, 4), PieceFactory.CreatePiece(PieceType.King, true));
            SetPiece(new Position(7, 4), PieceFactory.CreatePiece(PieceType.King, false));

            // Clear the command history
            commandManager.Clear();
        }

        /// <summary>
        /// Gets whether it is white's turn
        /// </summary>
        public bool IsWhiteTurn => isWhiteTurn;

        /// <summary>
        /// Checks if a position is valid on the board
        /// </summary>
        /// <param name="position">The position to check</param>
        /// <returns>True if the position is valid, false otherwise</returns>
        private bool IsValidPosition(Position position)
        {
            return position.Row >= 0 && position.Row < 8 && position.Col >= 0 && position.Col < 8;
        }
    }
}
