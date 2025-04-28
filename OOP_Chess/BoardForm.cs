using System;
using System.Drawing;
using System.Windows.Forms;

namespace OOP_Chess
{
    public partial class BoardForm : Form
    {
        private BoardPanel boardPanel;
        private CodeTreePanel architecturePanel;
        private Label turnLabel;
        private GameManager gameManager;

        public BoardForm()
        {
            InitializeComponent();
            InitializeLayout();
            InitializeGameManager();
        }

        private void InitializeLayout()
        {
            this.ClientSize = new Size(900, 700);
            this.Text = "OOP Chess Demo";

            turnLabel = new Label();
            turnLabel.Size = new Size(200, 30);
            turnLabel.Location = new Point(10, 10);
            turnLabel.Font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Bold);
            this.Controls.Add(turnLabel);

            architecturePanel = new CodeTreePanel();
            architecturePanel.Location = new Point(660, 50);
            architecturePanel.Size = new Size(220, 600);
            architecturePanel.OnClassSelected += OnClassSelected;
            this.Controls.Add(architecturePanel);
        }

        private void InitializeGameManager()
        {
            gameManager = new GameManager();
            gameManager.TurnChanged += UpdateTurnDisplay;
            gameManager.BoardChanged += RedrawBoard;
            gameManager.GameEnded += OnGameEnded; // 🔥 Hook up GameEnded

            boardPanel = new BoardPanel(gameManager);
            boardPanel.Location = new Point(10, 50);
            this.Controls.Add(boardPanel);

            RedrawBoard();
            UpdateTurnDisplay();
        }

        private void UpdateTurnDisplay()
        {
            if (gameManager.IsWhiteTurn)
            {
                boardPanel.BackColor = Color.White;
                turnLabel.Text = "White's Turn";
                turnLabel.BackColor = Color.Black;
                turnLabel.ForeColor = Color.White;
            }
            else
            {
                boardPanel.BackColor = Color.Black;
                turnLabel.Text = "Black's Turn";
                turnLabel.BackColor = Color.White;
                turnLabel.ForeColor = Color.Black;
            }
        }

        private void RedrawBoard()
        {
            boardPanel.DrawBoard();
        }

        private void OnClassSelected(string className)
        {
            if (className == "None")
            {
                boardPanel.DrawBoard();
            }
            else
            {
                boardPanel.HighlightClass(className);
            }
        }

        private void OnGameEnded()
        {
            var result = gameManager.CurrentResult;
            string winnerText;

            switch (result.Winner)
            {
                case GameWinner.White:
                    winnerText = "White wins!";
                    break;
                case GameWinner.Black:
                    winnerText = "Black wins!";
                    break;
                case GameWinner.Draw:
                    winnerText = "It's a draw!";
                    break;
                default:
                    winnerText = "Game over!";
                    break;
            }

            string message = $"{winnerText}\nReason: {result.Reason}";
            MessageBox.Show(message, "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
