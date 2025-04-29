using System;
using System.Drawing;
using System.Windows.Forms;
using OOP_Chess.Game;
using OOP_Chess.UI.Board;
using System.Reflection;
using System.Linq;

namespace OOP_Chess.UI
{
    public partial class CodeTreePanel : Panel
    {
        private readonly TreeView treeView;
        private readonly GameManager gameManager;
        private readonly BoardPanel boardPanel;
        private readonly Button clearHighlightButton;
        private readonly Button undoButton;
        private readonly Button redoButton;
        private TreeNode moveHistoryNode;

        public event Action<string> OnClassSelected;

        public CodeTreePanel(GameManager gameManager, BoardPanel boardPanel)
        {
            this.gameManager = gameManager;
            this.boardPanel = boardPanel;

            // Initialize TreeView
            this.treeView = new TreeView
            {
                Location = new Point(0, 0),
                Size = new Size(220, 520),
                Dock = DockStyle.Top
            };
            treeView.AfterSelect += TreeView_AfterSelect;
            this.Controls.Add(treeView);

            // Initialize Clear Highlight button
            this.clearHighlightButton = new Button
            {
                Text = "Clear Highlight",
                Location = new Point(0, 530),
                Size = new Size(220, 30),
                Dock = DockStyle.Top
            };
            clearHighlightButton.Click += (s, e) => boardPanel.ClearHighlights();
            this.Controls.Add(clearHighlightButton);

            // Initialize Undo button
            this.undoButton = new Button
            {
                Text = "Undo Move",
                Location = new Point(0, 560),
                Size = new Size(110, 30),
                Dock = DockStyle.None
            };
            undoButton.Click += (s, e) => gameManager.UndoLastMove();
            this.Controls.Add(undoButton);

            // Initialize Redo button
            this.redoButton = new Button
            {
                Text = "Redo Move",
                Location = new Point(110, 560),
                Size = new Size(110, 30),
                Dock = DockStyle.None
            };
            redoButton.Click += (s, e) => gameManager.RedoLastMove();
            this.Controls.Add(redoButton);

            // Subscribe to game events
            gameManager.MoveAdded += OnMoveAdded;
            gameManager.MoveUndone += UpdateTreeView;
            gameManager.MoveRedone += UpdateTreeView;

            UpdateTreeView();
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Parent == null) return;

            if (e.Node.Parent.Text == "Code Structure")
            {
                string className = e.Node.Text;
                OnClassSelected?.Invoke(className);
            }
            else if (e.Node.Parent.Text == "Move History")
            {
                var moveInfo = e.Node.Tag as MoveInfo;
                if (moveInfo != null)
                {
                    boardPanel.HighlightMove(moveInfo.From, moveInfo.To);
                }
            }
        }

        private void OnMoveAdded(MoveInfo moveInfo)
        {
            if (moveHistoryNode != null)
            {
                var moveNode = new TreeNode(moveInfo.ToString())
                {
                    Tag = moveInfo
                };
                moveHistoryNode.Nodes.Add(moveNode);
                moveHistoryNode.Expand();
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

            // Pieces node (collapsed by default)
            var piecesNode = new TreeNode("Pieces");
            boardNode.Nodes.Add(piecesNode);

            // Add pieces to the Pieces node
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
            piecesNode.Collapse();

            // Move History node
            moveHistoryNode = new TreeNode("Move History");
            gameNode.Nodes.Add(moveHistoryNode);
            foreach (var move in gameManager.GetMoveHistory())
            {
                var moveNode = new TreeNode(move.ToString())
                {
                    Tag = move
                };
                moveHistoryNode.Nodes.Add(moveNode);
            }

            // Code Structure node
            var codeStructureNode = new TreeNode("Code Structure");
            gameNode.Nodes.Add(codeStructureNode);

            // Add class types to Code Structure
            var classTypes = new[]
            {
                "Game",
                "Board",
                "Piece",
                "MoveCommand",
                "CastleCommand",
                "EnPassantCommand",
                "PromoteCommand",
                "PawnMoveStrategy",
                "RookMoveStrategy",
                "KnightMoveStrategy",
                "BishopMoveStrategy",
                "QueenMoveStrategy",
                "KingMoveStrategy"
            };

            foreach (var className in classTypes)
            {
                codeStructureNode.Nodes.Add(new TreeNode(className));
            }
            codeStructureNode.Collapse();

            // Turn node
            var turnNode = new TreeNode($"Turn: {(gameManager.IsWhiteTurn ? "White" : "Black")}");
            gameNode.Nodes.Add(turnNode);
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
