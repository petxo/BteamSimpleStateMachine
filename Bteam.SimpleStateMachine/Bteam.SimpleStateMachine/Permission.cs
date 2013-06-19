using System;

namespace Bteam.SimpleStateMachine
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    internal class Permission<TState>
    {
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public TState State { get; set; }

        /// <summary>
        /// Occurs when [on trigger].
        /// </summary>
        public event TriggerEventHanlder OnTrigger;

        /// <summary>
        /// Gets or sets the condition.
        /// </summary>
        /// <value>The condition.</value>
        public Func<bool> Condition { get; set; }

        /// <summary>
        /// Invokes the on trigger.
        /// </summary>
        public void InvokeOnTrigger()
        {
            TriggerEventHanlder handler = OnTrigger;
            if (handler != null) handler(new TriggerArs());
        }
    }
}