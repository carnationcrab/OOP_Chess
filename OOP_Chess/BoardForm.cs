using System;
using System.Drawing;
using System.Windows.Forms;
using OOP_Chess;

namespace OOP_Chess
{
    public partial class BoardForm : Form
    {
        private Button[,] buttons = new Button[8, 8];
        private Game game;

        public BoardForm()
        {
            InitializeComponent();
            InitializeBoard();
            game = new Game();
            DrawBoard();
        }

        private void InitializeBoard()
        {
            this.ClientSize = new Size(640, 640);
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var button = new Button();
                    button.Size = new Size(80, 80);
                    button.Location = new Point(col * 80, row * 80);
                    button.Click += Button_Click;
                    button.Tag = new Position(row, col);
                    button.Font = new Font(FontFamily.GenericSansSerif, 24, FontStyle.Bold);
                    button.BackColor = (row + col) % 2 == 0 ? Color.Beige : Color.Brown;
                    buttons[row, col] = button;
                    this.Controls.Add(button);
                }
            }
        }

        private Position selectedPosition = null;

        private void Button_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var pos = (Position)button.Tag;

            if (selectedPosition == null)
            {
                if (game.Board.GetPiece(pos) != null &&
                    game.Board.GetPiece(pos).IsWhite == game.WhiteTurn)
                {
                    selectedPosition = pos;
                }
            }
            else
            {
                if (game.TryMove(selectedPosition, pos))
                {
                    selectedPosition = null;
                    DrawBoard();
                }
                else
                {
                    selectedPosition = null;
                }
            }
        }

        private void DrawBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var piece = game.Board.GetPiece(new Position(row, col));
                    buttons[row, col].Text = piece?.GetSymbol() ?? "";
                }
            }
        }
    }
}