using System;
using System.Collections.Generic;
using OOP_Chess.Game.Commands;

namespace OOP_Chess.Game
{
    public class MoveLog
    {
        private        List<MoveInfo> moves;
        public event Action<MoveInfo> MoveAdded;
        public event Action           MoveUndone;
        public event Action           MoveRedone;

        public MoveLog()
        {
            moves = new List<MoveInfo>();
        }

        public void AddMove(MoveInfo move)
        {
            moves.Add(move);
            MoveAdded?.Invoke(move);
        }

        public void Clear()
        {
            moves.Clear();
        }

        public IReadOnlyList<MoveInfo> GetMoves()
        {
            return moves.AsReadOnly();
        }

        public void OnMoveUndone()
        {
            MoveUndone?.Invoke();
        }

        public void OnMoveRedone()
        {
            MoveRedone?.Invoke();
        }
    }

    public class MoveInfo
    {
        public Position From      { get; }
        public Position To        { get; }
        public string   PieceName { get; }
        public bool     IsWhite   { get; }
        public bool     IsCapture { get; }

        public MoveInfo(Position from, Position to, string pieceName, bool isWhite, bool isCapture)
        {
            From      = from;
            To        = to;
            PieceName = pieceName;
            IsWhite   = isWhite;
            IsCapture = isCapture;
        }

        public override string ToString()
        {
            string moveText = $"{PieceName} {(char)('a' + From.Col)}{From.Row + 1} → {(char)('a' + To.Col)}{To.Row + 1}";
            if (IsCapture)
                moveText += " (capture)";
            return moveText;
        }
    }
} 