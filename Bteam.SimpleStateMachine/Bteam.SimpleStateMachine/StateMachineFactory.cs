namespace Bteam.SimpleStateMachine
{
    /// <summary>
    /// 
    /// </summary>
    public static class StateMachineFactory
    {
        /// <summary>
        /// Creates the specified initial state.
        /// </summary>
        /// <typeparam name="TState">The type of the state.</typeparam>
        /// <param name="initialState">The initial state.</param>
        /// <returns></returns>
        public static IStateMachine<TState> Create<TState>(TState initialState)
        {
            return new StateMachine<TState>(initialState);
        }
    }
}