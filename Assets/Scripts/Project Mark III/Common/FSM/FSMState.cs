using UnityEngine;
using System.Collections.Generic;

namespace Common.FSM
{
    public class FSMState
    {
        List<FSMAction> actions;
        string name;
        FSM owner;
        Dictionary<string, FSMState> transitionMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="Common.FSM.FSMState"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="owner">Owner.</param>
        public FSMState(string name, FSM owner)
        {
            this.name = name;
            this.owner = owner;
            transitionMap = new Dictionary<string, FSMState>();
            actions = new List<FSMAction>();
        }

        /// <summary>
        /// Adds the transition.
        /// </summary>
        public void AddTransition(string id, FSMState destinationState)
        {
            if (transitionMap.ContainsKey(id))
            {
                Debug.LogError(string.Format("state {0} already contains transition for {1}", name, id));
                return;
            }

            transitionMap[id] = destinationState;
        }

        /// <summary>
        /// Gets the transition.
        /// </summary>
        public FSMState GetTransition(string eventId)
        {
            return transitionMap.ContainsKey(eventId)
                ? transitionMap[eventId] : null;

        }

        /// <summary>
        /// Adds the action.
        /// </summary>
        public void AddAction(FSMAction action)
        {
            if (actions.Contains(action))
            {
                Debug.LogWarning("This state already contains " + action);
                return;
            }

            if (action.GetOwner() != this)
            {
                Debug.LogWarning("This state doesn't own " + action);
            }

            actions.Add(action);
        }

        /// <summary>
        /// This gets the actions of this state
        /// </summary>
        /// <returns>The actions.</returns>
        public IEnumerable<FSMAction> GetActions()
        {
            return actions;
        }

        /// <summary>
        /// Sends the event.
        /// </summary>
        public void SendEvent(string eventId)
        {
            owner.SendEvent(eventId);
        }
    }
}