using System.Drawing;

namespace OOP_Chess
{
    public class BoardConfiguration
    {
        public static class Dimensions
        {
            public const int BoardSize = 8;
            public const int SquareSize = 80;
            public const int TotalBoardSize = BoardSize * SquareSize;
        }

        public static class Colors
        {
            public static readonly Color LightSquare = Color.Tan;
            public static readonly Color DarkSquare = Color.LightBlue;
            public static readonly Color SelectedSquare = Color.LightBlue;
            public static readonly Color HighlightedSquare = Color.Yellow;
            public static readonly Color Background = Color.White;
        }

        public static class FontSettings
        {
            private static readonly Font pieceFont = new Font("Arial", 24);
            public static Font PieceFont => pieceFont;
        }
    }
} 