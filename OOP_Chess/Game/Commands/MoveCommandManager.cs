using System;
using System.Collections.Generic;

namespace OOP_Chess
{
    /// <summary>
    /// Manages the command history for undo/redo functionality
    /// </summary>
    public class MoveCommandManager
    {
        private readonly Stack<ICommand> undoStack = new Stack<ICommand>();
        private readonly Stack<ICommand> redoStack = new Stack<ICommand>();

        /// <summary>
        /// Executes a new command
        /// </summary>
        /// <param name="command">The command to execute</param>
        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            undoStack.Push(command);
            redoStack.Clear();
        }

        /// <summary>
        /// Undoes the last command
        /// </summary>
        /// <returns>True if a command was undone, false if there are no commands to undo</returns>
        public bool Undo()
        {
            if (undoStack.Count == 0)
                return false;

            var command = undoStack.Pop();
            command.Undo();
            redoStack.Push(command);
            return true;
        }

        /// <summary>
        /// Redoes the last undone command
        /// </summary>
        /// <returns>True if a command was redone, false if there are no commands to redo</returns>
        public bool Redo()
        {
            if (redoStack.Count == 0)
                return false;

            var command = redoStack.Pop();
            command.Execute();
            undoStack.Push(command);
            return true;
        }

        /// <summary>
        /// Clears the command history
        /// </summary>
        public void Clear()
        {
            undoStack.Clear();
            redoStack.Clear();
        }
    }
} 