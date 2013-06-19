using System.Linq;
using System.Collections.Generic;
using System;

namespace Bteam.SimpleStateMachine
{
    public class StateMachine<TState> : IStateMachine<TState>
    {

        private readonly IDictionary<TState, IList<Permission<TState>>> _permissionsState;

        /// <summary>
        /// Initializes a new instance of the <see cref="StateMachine&lt;TState&gt;"/> class.
        /// </summary>
        /// <param name="intitalState">State of the intital.</param>
        internal StateMachine(TState intitalState)
        {
            _permissionsState = new Dictionary<TState, IList<Permission<TState>>>();
            CurrentState = intitalState;
        }

        /// <summary>
        /// Gets or sets the state of the current.
        /// </summary>
        /// <value>The state of the current.</value>
        public TState CurrentState { get; private set; }

        /// <summary>
        /// Changes the state.
        /// </summary>
        /// <param name="nextState">State of the next.</param>
        public void ChangeState(TState nextState)
        {
            if (CurrentState.Equals(nextState)) return;

            if (!_permissionsState.ContainsKey(CurrentState)) return;

            var permission = _permissionsState[CurrentState].First(p => p.State.Equals(nextState));

            if (permission.Condition == null || permission.Condition.Invoke())
            {
                CurrentState = nextState;
                permission.InvokeOnTrigger();
            }

        }

        /// <summary>
        /// Changes the state.
        /// </summary>
        /// <param name="nextState">State of the next.</param>
        /// <param name="condition">The condition.</param>
        public void ChangeState(TState nextState, Func<bool> condition)
        {
            if (CurrentState.Equals(nextState)) return;

            if(!_permissionsState.ContainsKey(CurrentState)) return;

            var permission = _permissionsState[CurrentState].First(p => p.State.Equals(nextState));

            if (condition.Invoke())
            {
                if (permission.Condition == null || permission.Condition.Invoke())
                {
                    CurrentState = nextState;
                    permission.InvokeOnTrigger();
                }
            }
        }


        /// <summary>
        /// Determines whether this instance [can change state] the specified next state.
        /// </summary>
        /// <param name="nextState">State of the next.</param>
        /// <returns>
        /// 	<c>true</c> if this instance [can change state] the specified next state; otherwise, <c>false</c>.
        /// </returns>
        public bool CanChangeState(TState nextState)
        {
            return !CurrentState.Equals(nextState) && _permissionsState.ContainsKey(CurrentState) &&
                   _permissionsState[CurrentState].Count(p => p.State.Equals(nextState)) > 0;
        }

        /// <summary>
        /// Tries the state of the change.
        /// </summary>
        /// <param name="nextState">State of the next.</param>
        /// <returns></returns>
        public bool TryChangeState(TState nextState)
        {
            if (CanChangeState(nextState))
            {
                ChangeState(nextState);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Permits the specified state inital.
        /// </summary>
        /// <param name="initialState">The state inital.</param>
        /// <param name="finalState">The state final.</param>
        /// <returns></returns>
        public IStateMachine<TState> Permit(TState initialState, TState finalState)
        {
            return Permit(initialState, finalState, null, null);
        }

        /// <summary>
        /// Permits the specified state inital.
        /// </summary>
        /// <param name="initialState">The state inital.</param>
        /// <param name="finalState">The state final.</param>
        /// <param name="trigger">The trigger.</param>
        /// <returns></returns>
        public IStateMachine<TState> Permit(TState initialState, TState finalState, TriggerEventHanlder trigger)
        {
            return Permit(initialState, finalState, trigger, null);
        }

        /// <summary>
        /// Permits the specified initial state.
        /// </summary>
        /// <param name="initialState">The initial state.</param>
        /// <param name="finalState">The final state.</param>
        /// <param name="condition">The condition.</param>
        /// <returns></returns>
        public IStateMachine<TState> Permit(TState initialState, TState finalState, Func<bool> condition)
        {
            return Permit(initialState, finalState, null, condition);
        }

        /// <summary>
        /// Permits the specified state inital.
        /// </summary>
        /// <param name="initialState">The state inital.</param>
        /// <param name="finalState">The state final.</param>
        /// <param name="trigger">The trigger.</param>
        /// <param name="condition">The condition.</param>
        /// <returns></returns>
        public IStateMachine<TState> Permit(TState initialState, TState finalState, TriggerEventHanlder trigger, Func<bool> condition)
        {
            if (initialState.Equals(finalState))
            {
                return this;
            }

            if (!_permissionsState.ContainsKey(initialState))
            {
                var permissions = new List<Permission<TState>>();
                _permissionsState.Add(initialState, permissions);
            }

            var permission = new Permission<TState>
            {
                State = finalState
            };
            permission.OnTrigger += trigger;
            permission.Condition = condition;

            _permissionsState[initialState].Add(permission);


            return this;
        }

    }
}