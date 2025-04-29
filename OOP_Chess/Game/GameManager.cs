using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_Chess.Pieces;

namespace OOP_Chess.Game
{
    public class GameManager
    {
        private Board board;
        private Game currentGame;
        private MoveLog moveLog;
        public GameResult CurrentResult { get; private set; }
        public event Action GameEnded;
        public event Action BoardChanged;
        public event Action TurnChanged;
        public event Action<MoveInfo> MoveAdded;
        public event Action MoveUndone;
        public event Action MoveRedone;

        public GameManager(Board board)
        {
            this.board = board ?? throw new ArgumentNullException(nameof(board));
            this.currentGame = new Game(board);
            this.moveLog = new MoveLog();
            CurrentResult = new GameResult();

            // Subscribe to move log events
            moveLog.MoveAdded += (move) => MoveAdded?.Invoke(move);
            moveLog.MoveUndone += () => MoveUndone?.Invoke();
            moveLog.MoveRedone += () => MoveRedone?.Invoke();
        }

        public Piece[,] GetBoardSnapshot()
        {
            return board.GetBoardSnapshot();
        }

        public void EndGameDueToKingCapture(GameWinner winner)
        {
            if (!CurrentResult.IsGameOver)
            {
                CurrentResult.IsGameOver = true;
                CurrentResult.Winner = winner;
                CurrentResult.Reason = "King Captured";
                GameEnded?.Invoke();
            }
        }

        public bool IsWhiteTurn => currentGame.IsWhiteTurn;

        /// <summary>
        /// Finds the position of the king for the specified color
        /// </summary>
        /// <param name="isWhite">Whether to find the white or black king</param>
        /// <returns>The position of the king, or null if not found</returns>
        private Position FindKing(bool isCurrentPlayer)
        {
            var board = currentGame.Board.GetBoardSnapshot();
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var piece = board[row, col];
                    if (piece != null && piece.IsWhite == isCurrentPlayer && piece.IsKing())
                    {
                        return new Position(row, col);
                    }
                }
            }
            return null;
        }

        private bool IsInCheckmate(bool isCurrentPlayer)
        {
            // First check if the king is in check
            if (!IsInCheck(isCurrentPlayer))
                return false;

            // Get all pieces of the current player
            var board = currentGame.Board.GetBoardSnapshot();
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var piece = board[row, col];
                    if (piece != null && piece.IsWhite == isCurrentPlayer)
                    {
                        // Try all possible moves for this piece
                        for (int toRow = 0; toRow < 8; toRow++)
                        {
                            for (int toCol = 0; toCol < 8; toCol++)
                            {
                                var from = new Position(row, col);
                                var to = new Position(toRow, toCol);

                                // If any legal move exists that gets out of check, it's not checkmate
                                if (piece.IsValidMove(from, to, currentGame.Board))
                                {
                                    // Make a temporary move
                                    var tempPiece = currentGame.Board.GetPiece(to);
                                    currentGame.Board.SetPiece(to, piece);
                                    currentGame.Board.SetPiece(from, null);

                                    bool stillInCheck = IsInCheck(isCurrentPlayer);

                                    // Undo the temporary move
                                    currentGame.Board.SetPiece(from, piece);
                                    currentGame.Board.SetPiece(to, tempPiece);

                                    if (!stillInCheck)
                                        return false;
                                }
                            }
                        }
                    }
                }
            }

            // If we get here, no legal moves exist that get out of check
            return true;
        }

        private bool IsInCheck(bool isCurrentPlayer)
        {
            // Find the king's position
            Position kingPos = FindKing(isCurrentPlayer);
            if (kingPos == null)
                return false;

            // Check if any opponent's piece can capture the king
            var board = currentGame.Board.GetBoardSnapshot();
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var piece = board[row, col];
                    if (piece != null && piece.IsWhite != isCurrentPlayer)
                    {
                        if (piece.IsValidMove(new Position(row, col), kingPos, currentGame.Board))
                            return true;
                    }
                }
            }
            return false;
        }

        private bool IsInStalemate(bool isCurrentPlayer)
        {
            // If in check, it's not stalemate
            if (IsInCheck(isCurrentPlayer))
                return false;

            // Get all pieces of the current player
            var board = currentGame.Board.GetBoardSnapshot();
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var piece = board[row, col];
                    if (piece != null && piece.IsWhite == isCurrentPlayer)
                    {
                        // Try all possible moves for this piece
                        for (int toRow = 0; toRow < 8; toRow++)
                        {
                            for (int toCol = 0; toCol < 8; toCol++)
                            {
                                var from = new Position(row, col);
                                var to = new Position(toRow, toCol);

                                // If any legal move exists, it's not stalemate
                                if (piece.IsValidMove(from, to, currentGame.Board))
                                {
                                    // Make a temporary move
                                    var tempPiece = currentGame.Board.GetPiece(to);
                                    currentGame.Board.SetPiece(to, piece);
                                    currentGame.Board.SetPiece(from, null);

                                    bool inCheck = IsInCheck(isCurrentPlayer);

                                    // Undo the temporary move
                                    currentGame.Board.SetPiece(from, piece);
                                    currentGame.Board.SetPiece(to, tempPiece);

                                    if (!inCheck)
                                        return false;
                                }
                            }
                        }
                    }
                }
            }

            // If we get here, no legal moves exist
            return true;
        }

        private bool HasInsufficientMaterial()
        {
            // Count the number of pieces for each player
            int whitePieces = 0;
            int blackPieces = 0;
            var board = currentGame.Board.GetBoardSnapshot();

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var piece = board[row, col];
                    if (piece != null)
                    {
                        if (piece.IsWhite)
                            whitePieces++;
                        else
                            blackPieces++;
                    }
                }
            }

            // If either player has only their king, it's insufficient material
            return whitePieces == 1 || blackPieces == 1;
        }

        public bool TryMove(Position from, Position to)
        {
            if (CurrentResult.IsGameOver)
                return false;

            if (currentGame.TryMove(from, to))
            {
                BoardChanged?.Invoke();
                TurnChanged?.Invoke();

                // Check for checkmate on the opponent
                if (IsInCheckmate(!IsWhiteTurn))
                {
                    CurrentResult.IsGameOver = true;
                    CurrentResult.Winner = GameWinner.CurrentPlayer;
                    CurrentResult.Reason = "Checkmate";
                    GameEnded?.Invoke();
                }
                // Check for stalemate
                else if (IsInStalemate(!IsWhiteTurn))
                {
                    CurrentResult.IsGameOver = true;
                    CurrentResult.Winner = GameWinner.Draw;
                    CurrentResult.Reason = "Stalemate";
                    GameEnded?.Invoke();
                }
                // Check for insufficient material
                else if (HasInsufficientMaterial())
                {
                    CurrentResult.IsGameOver = true;
                    CurrentResult.Winner = GameWinner.Draw;
                    CurrentResult.Reason = "Insufficient Material";
                    GameEnded?.Invoke();
                }

                return true;
            }
            return false;
        }

        public void LogMove(Position from, Position to, Piece piece, bool isCapture)
        {
            var moveInfo = new MoveInfo(from, to, piece.GetType().Name, piece.IsWhite, isCapture);
            moveLog.AddMove(moveInfo);
        }

        public IReadOnlyList<MoveInfo> GetMoveHistory()
        {
            return moveLog.GetMoves();
        }

        public bool UndoLastMove()
        {
            if (currentGame.UndoMove())
            {
                moveLog.OnMoveUndone();
                BoardChanged?.Invoke();
                TurnChanged?.Invoke();
                return true;
            }
            return false;
        }

        public bool RedoLastMove()
        {
            if (currentGame.RedoMove())
            {
                moveLog.OnMoveRedone();
                BoardChanged?.Invoke();
                TurnChanged?.Invoke();
                return true;
            }
            return false;
        }
    }
}
