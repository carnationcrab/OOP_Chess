using System;
using System.Drawing;
using System.Windows.Forms;
using OOP_Chess.Game;
using OOP_Chess.UI.Board;

namespace OOP_Chess.UI
{
    public partial class CodeTreePanel : Panel
    {
        private TreeView treeView;
        private GameManager gameManager;
        private BoardPanel boardPanel;

        public event Action<string> OnClassSelected;

        public CodeTreePanel(GameManager gameManager, BoardPanel boardPanel)
        {
            this.gameManager = gameManager;
            this.boardPanel = boardPanel;
            InitializeComponent();
            InitializeTreeView();
            UpdateTreeView();
        }

        private void InitializeTreeView()
        {
            treeView = new TreeView();
            treeView.Dock = DockStyle.Fill;
            treeView.Font = new Font(FontFamily.GenericSansSerif, 10);
            treeView.AfterSelect += TreeView_AfterSelect;
            this.Controls.Add(treeView);
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                string className = e.Node.Text;
                OnClassSelected?.Invoke(className);
                HighlightClass(className);
            }
        }

        public void UpdateTreeView()
        {
            treeView.Nodes.Clear();

            // Game node
            var gameNode = new TreeNode("Game");
            treeView.Nodes.Add(gameNode);

            // Board node
            var boardNode = new TreeNode("Board");
            gameNode.Nodes.Add(boardNode);

            // Pieces node
            var piecesNode = new TreeNode("Pieces");
            boardNode.Nodes.Add(piecesNode);

            // Add pieces
            var board = gameManager.GetBoardSnapshot();
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var piece = board[row, col];
                    if (piece != null)
                    {
                        var pieceNode = new TreeNode($"{piece.Symbol} at ({row}, {col})");
                        piecesNode.Nodes.Add(pieceNode);
                    }
                }
            }

            // Turn node
            var turnNode = new TreeNode($"Turn: {(gameManager.IsWhiteTurn ? "White" : "Black")}");
            gameNode.Nodes.Add(turnNode);

            // Expand all nodes
            treeView.ExpandAll();
        }

        public void HighlightClass(string className)
        {
            if (boardPanel != null)
            {
                boardPanel.HighlightClass(className);
            }
        }
    }
}
