// ===== File: BoardForm.cs =====
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

        private Panel sidebarPanel;
        private ListBox classListBox;
        private Button toggleSidebarButton;
        private bool sidebarVisible = true;

        public BoardForm()
        {
            InitializeComponent();
            InitializeSidebar();
            InitializeBoard();
            game = new Game();
            DrawBoard();
        }

        private void InitializeSidebar()
        {
            // Sidebar Panel
            sidebarPanel = new Panel();
            sidebarPanel.Size = new Size(250, 640);
            sidebarPanel.Dock = DockStyle.Right;
            sidebarPanel.BackColor = Color.LightGray;
            this.Controls.Add(sidebarPanel);

            // ListBox inside sidebar
            classListBox = new ListBox();
            classListBox.Dock = DockStyle.Fill;
            classListBox.Font = new Font(FontFamily.GenericSansSerif, 12);
            classListBox.SelectedIndexChanged += ClassListBox_SelectedIndexChanged;
            sidebarPanel.Controls.Add(classListBox);

            // Add all classes
            classListBox.Items.AddRange(new string[]
            {
                "Piece", "Pawn", "Knight", "Bishop", "Rook", "Queen", "King",
                "Board", "Game", "Position"
            });

            // Toggle Sidebar Button
            toggleSidebarButton = new Button();
            toggleSidebarButton.Text = "Hide Code Structures";
            toggleSidebarButton.Size = new Size(180, 30);
            toggleSidebarButton.Location = new Point(10, 10);
            toggleSidebarButton.Click += ToggleSidebarButton_Click;
            this.Controls.Add(toggleSidebarButton);
        }

        private void ToggleSidebarButton_Click(object sender, EventArgs e)
        {
            sidebarVisible = !sidebarVisible;
            sidebarPanel.Visible = sidebarVisible;
            toggleSidebarButton.Text = sidebarVisible ? "Hide Code Structures" : "Show Code Structures";
        }

        private void ClassListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (classListBox.SelectedItem != null)
            {
                string selectedClass = classListBox.SelectedItem.ToString();
                HighlightClass(selectedClass);
            }
        }

        private void HighlightClass(string className)
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var button = buttons[row, col];
                    var piece = game.Board.GetPiece(new Position(row, col));

                    // Reset color first
                    button.BackColor = (row + col) % 2 == 0 ? Color.Beige : Color.Brown;

                    if (className == "Board" || className == "Game")
                    {
                        // Highlight all squares for Board or Game
                        button.BackColor = Color.LightPink;
                    }
                    else if (className == "Position")
                    {
                        // Highlight all squares for Position
                        button.BackColor = Color.MediumPurple;
                    }
                    else if (className == "Piece")
                    {
                        // Highlight all pieces
                        if (piece != null)
                        {
                            button.BackColor = Color.HotPink;
                        }
                    }
                    else if (piece != null && piece.GetType().Name == className)
                    {
                        // Highlight matching piece type
                        button.BackColor = Color.HotPink;
                    }
                }
            }
        }

        private Position selectedPosition = null;

        private void InitializeBoard()
        {
            this.ClientSize = new Size(890, 690); // Enough for board + sidebar

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var button = new Button();
                    button.Size = new Size(80, 80);
                    button.Location = new Point(col * 80, 50 + row * 80); // offset to leave space under toggle button
                    button.Click += Button_Click;
                    button.Tag = new Position(row, col);
                    button.Font = new Font(FontFamily.GenericSansSerif, 24, FontStyle.Bold);
                    button.BackColor = (row + col) % 2 == 0 ? Color.Beige : Color.Brown;
                    buttons[row, col] = button;
                    this.Controls.Add(button);
                }
            }
        }

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

                    if (!sidebarVisible || classListBox.SelectedItem == null)
                    {
                        buttons[row, col].BackColor = (row + col) % 2 == 0 ? Color.Beige : Color.Brown;
                    }
                }
            }
        }
    }
}
