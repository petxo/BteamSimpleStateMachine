using System;

namespace Bteam.SimpleStateMachine
{
    public interface IStateMachine<TState>
    {
        /// <summary>
        /// Gets or sets the state of the current.
        /// </summary>
        /// <value>The state of the current.</value>
        TState CurrentState { get; }

        /// <summary>
        /// Changes the state.
        /// </summary>
        /// <param name="nextState">State of the next.</param>
        void ChangeState(TState nextState);

        /// <summary>
        /// Determines whether this instance [can change state] the specified next state.
        /// </summary>
        /// <param name="nextState">State of the next.</param>
        /// <returns>
        /// 	<c>true</c> if this instance [can change state] the specified next state; otherwise, <c>false</c>.
        /// </returns>
        bool CanChangeState(TState nextState);

        /// <summary>
        /// Permits the specified state inital.
        /// </summary>
        /// <param name="initialState">The state inital.</param>
        /// <param name="finalState">The state final.</param>
        /// <returns></returns>
        IStateMachine<TState> Permit(TState initialState, TState finalState);

        /// <summary>
        /// Permits the specified state inital.
        /// </summary>
        /// <param name="initialState">The state inital.</param>
        /// <param name="finalState">The state final.</param>
        /// <param name="trigger">The trigger.</param>
        /// <returns></returns>
        IStateMachine<TState> Permit(TState initialState, TState finalState, TriggerEventHanlder trigger);

        /// <summary>
        /// Tries the state of the change.
        /// </summary>
        /// <param name="nextState">State of the next.</param>
        /// <returns></returns>
        bool TryChangeState(TState nextState);

        /// <summary>
        /// Permits the specified state inital.
        /// </summary>
        /// <param name="initialState">The state inital.</param>
        /// <param name="finalState">The state final.</param>
        /// <param name="trigger">The trigger.</param>
        /// <param name="condition">The condition.</param>
        /// <returns></returns>
        IStateMachine<TState> Permit(TState initialState, TState finalState, TriggerEventHanlder trigger, Func<bool> condition);

        /// <summary>
        /// Permits the specified initial state.
        /// </summary>
        /// <param name="initialState">The initial state.</param>
        /// <param name="finalState">The final state.</param>
        /// <param name="condition">The condition.</param>
        /// <returns></returns>
        IStateMachine<TState> Permit(TState initialState, TState finalState, Func<bool> condition);

        void ChangeState(TState nextState, Func<bool> condition);
    }
}