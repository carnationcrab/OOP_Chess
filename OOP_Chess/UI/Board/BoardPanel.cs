using OOP_Chess.Pieces;
using OOP_Chess.Game;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace OOP_Chess.UI.Board
{
    public class BoardPanel : Panel
    {
        private readonly Button[,] squares;
        private readonly GameManager gameManager;
        private Position selectedPosition;
        private bool hasSelectedPosition;
        private Position lastMoveFromHighlight;
        private Position lastMoveToHighlight;

        public event Action<Position> PositionSelected;

        public BoardPanel(GameManager gameManager)
        {
            this.gameManager = gameManager;
            squares = new Button[BoardConfiguration.Dimensions.BoardSize, BoardConfiguration.Dimensions.BoardSize];
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            this.Size = new Size(BoardConfiguration.Dimensions.TotalBoardSize, BoardConfiguration.Dimensions.TotalBoardSize);
            this.BackColor = BoardConfiguration.Colors.Background;

            for (int row = 0; row < BoardConfiguration.Dimensions.BoardSize; row++)
            {
                for (int col = 0; col < BoardConfiguration.Dimensions.BoardSize; col++)
                {
                    squares[row, col] = CreateSquare(row, col);
                    this.Controls.Add(squares[row, col]);
                }
            }
        }

        private Button CreateSquare(int row, int col)
        {
            var button = new Button
            {
                Size = new Size(BoardConfiguration.Dimensions.SquareSize, BoardConfiguration.Dimensions.SquareSize),
                Location = new Point(col * BoardConfiguration.Dimensions.SquareSize, row * BoardConfiguration.Dimensions.SquareSize),
                Tag = new Position(row, col),
                BackColor = GetDefaultSquareColor(row, col),
                Font = BoardConfiguration.FontSettings.PieceFont
            };
            button.Click += Button_Click;
            return button;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var position = (Position)button.Tag;
            var board = gameManager.GetBoardSnapshot();
            var piece = board[position.Row, position.Col];

            if (!hasSelectedPosition)
            {
                HandleFirstClick(position, piece, button);
            }
            else
            {
                HandleSecondClick(position, piece);
            }
        }

        private void HandleFirstClick(Position position, Piece piece, Button button)
        {
            if (piece != null && piece.IsWhite == gameManager.IsWhiteTurn)
            {
                selectedPosition = position;
                hasSelectedPosition = true;
                button.BackColor = BoardConfiguration.Colors.SelectedSquare;
            }
        }

        private void HandleSecondClick(Position position, Piece piece)
        {
            if (gameManager.TryMove(selectedPosition, position))
            {
                // Update the highlight positions when a move is made
                lastMoveFromHighlight = selectedPosition;
                lastMoveToHighlight = position;
                ResetSelection();
                DrawBoard();
            }
            else if (piece != null && piece.IsWhite == gameManager.IsWhiteTurn)
            {
                UpdateSelection(position);
            }
            else
            {
                ResetSelection();
            }
        }

        private void ResetSelection()
        {
            squares[selectedPosition.Row, selectedPosition.Col].BackColor = GetDefaultSquareColor(selectedPosition.Row, selectedPosition.Col);
            hasSelectedPosition = false;
        }

        private void UpdateSelection(Position newPosition)
        {
            squares[selectedPosition.Row, selectedPosition.Col].BackColor = GetDefaultSquareColor(selectedPosition.Row, selectedPosition.Col);
            selectedPosition = newPosition;
            squares[newPosition.Row, newPosition.Col].BackColor = BoardConfiguration.Colors.SelectedSquare;
        }

        public void DrawBoard()
        {
            var board = gameManager.GetBoardSnapshot();
            for (int row = 0; row < BoardConfiguration.Dimensions.BoardSize; row++)
            {
                for (int col = 0; col < BoardConfiguration.Dimensions.BoardSize; col++)
                {
                    UpdateSquare(row, col, board[row, col]);
                }
            }
        }

        private void UpdateSquare(int row, int col, Piece piece)
        {
            squares[row, col].Text = piece?.Symbol ?? "";
            squares[row, col].BackColor = GetSquareColor(row, col);
        }

        private Color GetSquareColor(int row, int col)
        {
            if (hasSelectedPosition && row == selectedPosition.Row && col == selectedPosition.Col)
            {
                return Color.Orange;
            }
            if (lastMoveFromHighlight != null && row == lastMoveFromHighlight.Row && col == lastMoveFromHighlight.Col)
            {
                return Color.Orange;
            }
            if (lastMoveToHighlight != null && row == lastMoveToHighlight.Row && col == lastMoveToHighlight.Col)
            {
                return Color.Yellow;
            }
            return GetDefaultSquareColor(row, col);
        }

        private Color GetDefaultSquareColor(int row, int col)
        {
            return (row + col) % 2 == 0 ? BoardConfiguration.Colors.LightSquare : BoardConfiguration.Colors.DarkSquare;
        }

        public void UpdateBoard()
        {
            DrawBoard();
        }

        public void HighlightClass(string className)
        {
            var board = gameManager.GetBoardSnapshot();
            for (int row = 0; row < BoardConfiguration.Dimensions.BoardSize; row++)
            {
                for (int col = 0; col < BoardConfiguration.Dimensions.BoardSize; col++)
                {
                    var piece = board[row, col];
                    if (piece != null && piece.GetType().Name == className)
                    {
                        squares[row, col].BackColor = BoardConfiguration.Colors.HighlightedSquare;
                    }
                    else
                    {
                        squares[row, col].BackColor = GetDefaultSquareColor(row, col);
                    }
                }
            }
        }

        public void HighlightMove(Position from, Position to)
        {
            // Update the highlight positions when viewing move history
            lastMoveFromHighlight = from;
            lastMoveToHighlight = to;
            DrawBoard();
        }

        public void ClearHighlights()
        {
            lastMoveFromHighlight = null;
            lastMoveToHighlight = null;
            DrawBoard();
        }
    }
}

