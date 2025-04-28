// ===== File: CodeStructurePanel.cs =====
using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using OOP_Chess;

namespace OOP_Chess
{
    public partial class CodeStructurePanel : UserControl
    {
        public event Action<string> OnClassSelected;

        private TreeView classTreeView;
        private Button toggleButton;
        private bool sidebarVisible = true;

        public CodeStructurePanel()
        {
            InitializeComponent();
            InitializeSidebar();
            PopulateClassTree();
        }

        private void InitializeSidebar()
        {
            this.Dock = DockStyle.Right;
            this.Width = 320;
            this.BackColor = Color.LightGray;

            toggleButton = new Button();
            toggleButton.Text = "Hide Code Structures";
            toggleButton.Size = new Size(200, 30);
            toggleButton.Location = new Point(10, 10);
            toggleButton.Click += ToggleButton_Click;
            this.Controls.Add(toggleButton);

            classTreeView = new TreeView();
            classTreeView.Font = new Font(FontFamily.GenericSansSerif, 10);
            classTreeView.Location = new Point(10, 50);
            classTreeView.Size = new Size(300, 600);
            classTreeView.DrawMode = TreeViewDrawMode.OwnerDrawText;
            classTreeView.DrawNode += ClassTreeView_DrawNode;
            classTreeView.AfterSelect += ClassTreeView_AfterSelect;
            this.Controls.Add(classTreeView);
        }

        private void PopulateClassTree()
        {
            classTreeView.Nodes.Clear();

            classTreeView.Nodes.Add(new TreeNode("None"));

            var allTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass &&
                            (t == typeof(Piece) ||
                             t.IsSubclassOf(typeof(Piece)) ||
                             t == typeof(Board) ||
                             t == typeof(Game) ||
                             t == typeof(Position)))
                .ToList();

            foreach (var type in allTypes)
            {
                var classNode = new TreeNode(type.Name);

                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                if (properties.Any())
                {
                    var propsNode = new TreeNode("Properties");
                    foreach (var prop in properties)
                    {
                        propsNode.Nodes.Add($"{prop.PropertyType.Name} {prop.Name}");
                    }
                    classNode.Nodes.Add(propsNode);
                }

                var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly)
                                  .Where(m => !m.IsSpecialName);
                if (methods.Any())
                {
                    var methodsNode = new TreeNode("Methods");
                    foreach (var method in methods)
                    {
                        string methodSignature = BuildMethodSignature(method);
                        string displayText = method.IsStatic ? $"[Static] {methodSignature}" : methodSignature;
                        methodsNode.Nodes.Add(displayText);
                    }
                    classNode.Nodes.Add(methodsNode);
                }

                classTreeView.Nodes.Add(classNode);
            }
        }

        private string BuildMethodSignature(MethodInfo method)
        {
            var parameters = method.GetParameters()
                .Select(p => $"{p.ParameterType.Name} {p.Name}")
                .ToArray();
            return $"{method.ReturnType.Name} {method.Name}({string.Join(", ", parameters)})";
        }

        private void ToggleButton_Click(object sender, EventArgs e)
        {
            sidebarVisible = !sidebarVisible;
            classTreeView.Visible = sidebarVisible;
            toggleButton.Text = sidebarVisible ? "Hide Code Structures" : "Show Code Structures";
        }

        private void ClassTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null || e.Node.Parent.Text == "None")
            {
                OnClassSelected?.Invoke(e.Node.Text);
            }
            else if (e.Node.Parent != null && e.Node.Parent.Parent == null)
            {
                OnClassSelected?.Invoke(e.Node.Parent.Text);
            }
        }

        private void ClassTreeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            var node = e.Node;
            var g = e.Graphics;
            var bounds = e.Bounds;

            var regularFont = classTreeView.Font;
            var boldFont = new Font(classTreeView.Font, FontStyle.Bold);
            var typeFont = new Font(classTreeView.Font, FontStyle.Bold);

            Brush regularBrush = Brushes.Black;
            Brush typeBrush = Brushes.DarkGreen;
            Brush classBrush = Brushes.DarkBlue;
            Brush headerBrush = Brushes.MediumBlue;

            if (node.Level == 0) // Top-level: Class or "None"
            {
                g.DrawString(node.Text, boldFont, classBrush, bounds.Location);
            }
            else if (node.Level == 1) // Properties or Methods section
            {
                g.DrawString(node.Text, boldFont, headerBrush, bounds.Location);
            }
            else if (node.Level == 2) // Property/Method inside
            {
                string text = node.Text;

                if (text.Contains(" ")) 
                {
                    var parts = text.Split(new[] { ' ' }, 2);
                    string typePart = parts[0];
                    string restPart = parts.Length > 1 ? parts[1] : "";

                    var typeSize = TextRenderer.MeasureText(typePart + " ", typeFont);

                    // Draw type (bold green)
                    g.DrawString(typePart + " ", typeFont, typeBrush, bounds.X, bounds.Y);

                    // Draw name (normal black)
                    g.DrawString(restPart, regularFont, regularBrush, bounds.X + typeSize.Width, bounds.Y);
                }
                else
                {
                    g.DrawString(text, regularFont, regularBrush, bounds.Location);
                }
            }

            e.DrawDefault = false; // Skip normal drawing
        }
    }
}
