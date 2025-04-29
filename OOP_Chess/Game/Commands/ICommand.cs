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
        void Execute();

        /// <summary>
        /// Undoes the command
        /// </summary>
        void Undo();
    }
} 