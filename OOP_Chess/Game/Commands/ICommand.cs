namespace OOP_Chess
{
    /// <summary>
    /// Interface for the Command pattern
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes the command
        /// </summary>
        /// <returns>True if the command was executed successfully, false otherwise</returns>
        bool Execute();

        /// <summary>
        /// Undoes the command
        /// </summary>
        void Undo();

        /// <summary>
        /// Redoes the command
        /// </summary>
        /// <returns>True if the command was redone successfully, false otherwise</returns>
        bool Redo();
    }
} 