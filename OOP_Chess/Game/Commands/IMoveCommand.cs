using System;

namespace OOP_Chess
{
    /// <summary>
    /// Interface for move commands using the Command pattern
    /// </summary>
    public interface IMoveCommand : ICommand
    {
        /// <summary>
        /// Executes the move command
        /// </summary>
        /// <returns>True if the move was successful, false otherwise</returns>
        new bool Execute();

        /// <summary>
        /// Undoes the move command
        /// </summary>
        void Undo();

        /// <summary>
        /// Redoes the move command
        /// </summary>
        /// <returns>True if the move was redone successfully, false otherwise</returns>
        bool Redo();
    }
} 