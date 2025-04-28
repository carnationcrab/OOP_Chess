using System;
using System.Drawing;
using System.Windows.Forms;

namespace OOP_Chess
{
    public class BoardPanel : Panel
    {
        public const int SquareSize = 80;
        private Button[,] buttons = new Button[8, 8];
        private GameManager gameManager;

        public event Action PositionSelected;

        public Position SelectedPosition { get; private set; }

        public BoardPanel(GameManager gameManager)
        {
            this.gameManager = gameManager;
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            this.Size = new Size(SquareSize * 8, SquareSize * 8);
            this.BackColor = Color.White;

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var button = new Button();
                    button.Size = new Size(SquareSize, SquareSize);
                    button.Location = new Point(col * SquareSize, row * SquareSize);
                    button.Tag = new Position(row, col);
                    button.Font = new Font(FontFamily.GenericSansSerif, 24, FontStyle.Bold);
                    button.BackColor = GetDefaultSquareColor(row, col);
                    button.Click += Button_Click;
                    buttons[row, col] = button;
                    this.Controls.Add(button);
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var pos = (Position)button.Tag;

            if (SelectedPosition == null)
            {
                var snapshot = gameManager.GetBoardSnapshot();
                var piece = snapshot[pos.Row, pos.Col];
                if (piece != null && piece.IsWhite == gameManager.IsWhiteTurn)
                {
                    SelectedPosition = pos;
                }
            }
            else
            {
                if (gameManager.TryMove(SelectedPosition, pos))
                {
                    SelectedPosition = null;
                    DrawBoard();
                }
                else
                {
                    SelectedPosition = null;
                }
            }

            PositionSelected?.Invoke();
        }

        public void DrawBoard()
        {
            var snapshot = gameManager.GetBoardSnapshot();

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var piece = snapshot[row, col];
                    buttons[row, col].Text = piece?.GetSymbol() ?? "";
                    buttons[row, col].BackColor = GetDefaultSquareColor(row, col);
                }
            }
        }

        public void HighlightClass(string className)
        {
            var snapshot = gameManager.GetBoardSnapshot();

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var button = buttons[row, col];
                    var piece = snapshot[row, col];

                    button.BackColor = GetDefaultSquareColor(row, col);

                    if (className == "Board" || className == "Game")
                    {
                        button.BackColor = Color.LightPink;
                    }
                    else if (className == "Position")
                    {
                        button.BackColor = Color.MediumPurple;
                    }
                    else if (className == "Piece" && piece != null)
                    {
                        button.BackColor = Color.HotPink;
                    }
                    else if (piece != null && piece.GetType().Name == className)
                    {
                        button.BackColor = Color.HotPink;
                    }
                }
            }
        }

        private Color GetDefaultSquareColor(int row, int col)
        {
            return (row + col) % 2 == 0 ? Color.Beige : Color.Brown;
        }
    }
}
