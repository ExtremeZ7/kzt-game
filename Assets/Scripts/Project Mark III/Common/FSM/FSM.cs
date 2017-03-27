using UnityEngine;
using System.Collections.Generic;

namespace Common.FSM
{
    ///<summary>
    ///This is the main engine of our FSM, without this, you won't be
    ///able to use FSM States and FSM Actions.
    ///</summary>
    public class FSM
    {
        readonly string name;
        FSMState currentState;
        readonly Dictionary<string, FSMState> stateMap;

        public string Name
        {
            get
            {
                return name;
            }
        }

        ///<summary>
        /// This is the constructor that will initialize the FSM and give it
        /// a unique name or id.
        ///</summary>
        public FSM(string name)
        {
            this.name = name;
            currentState = null;
            stateMap = new Dictionary<string, FSMState>();
        }

        ///<summary>
        /// This initializes the FSM. We can indicate the starting State of
        /// the Object that has an FSM.
        ///</summary>
        public void Start(string stateName)
        {
            if (!stateMap.ContainsKey(stateName))
            {
                Debug.LogWarning("The FSM doesn't contain: " + stateName);
                return;
            }

            ChangeToState(stateMap[stateName]);
        }

        public FSMState AddState(string name)
        {
            if (stateMap.ContainsKey(name))
            {
                Debug.LogWarning("The FSM already contains: " + name);
                return null;
            }

            var newState = new FSMState(name, this);
            stateMap[name] = newState;
            return newState;
        }

        ///<summary>
        /// This changes the state of the Object. This also calls the exit
        /// state before doing the next state.
        ///</summary>
        public void ChangeToState(FSMState state)
        {
            if (currentState != null)
            {
                ExitState(currentState);
            }

            currentState = state;
            EnterState(currentState);
        }

        ///<summary>
        /// This changes the state of the Object. It is not advisable to
        /// call this to change state.
        ///</summary>
        public void EnterState(FSMState state)
        {
            ProcessStateAction(state, action => action.OnEnter());

            /*ProcessStateAction (state, delegate(FSMAction action) {
                action.OnEnter ();  
            });*/
        }


        void ExitState(FSMState state)
        {
            FSMState currentStateOnInvoke = currentState;

            ProcessStateAction(state, delegate(FSMAction action)
                {

                    if (currentState != currentStateOnInvoke)
                    {
                        Debug.LogError("State cannont be changed on exit of the specified state");
                    }

                    action.OnExit();   
                });
        }

        ///<summary>
        /// Call this under a MonoBehaviour's Update.
        ///</summary>
        public void Update()
        {
            if (currentState == null)
            {
                return;
            }

            ProcessStateAction(currentState, action => action.OnUpdate());
        }

        ///<summary>
        /// This handles the events that is bound to a state and changes
        /// the state.
        ///</summary>
        public void SendEvent(string eventId)
        {
            FSMState transitonState = ResolveTransition(eventId);

            if (transitonState == null)
            {
                Debug.LogWarning("The current state has no transition for event " + eventId);
            }
            else
            {
                ChangeToState(transitonState);
            }
        }

        private delegate void StateActionProcessor(FSMAction action);

        /// <summary>
        /// This gets all the actions that is inside the state and loop them.
        /// </summary>
        /// <param name="state">State.</param>
        /// <param name="actionProcessor">Action processor.</param>
        void ProcessStateAction(FSMState state, StateActionProcessor actionProcessor)
        {
            FSMState currentStateOnInvoke = currentState;
            IEnumerable<FSMAction> actions = state.GetActions();

            foreach (FSMAction action in actions)
            {
                if (currentState != currentStateOnInvoke)
                {
                    break;
                }

                actionProcessor(action);
            }
        }

        /// <summary>
        /// This gets the next state from the current state.
        /// </summary>
        /// <returns>The transition.</returns>
        /// <param name="eventId">Event identifier.</param>
        FSMState ResolveTransition(string eventId)
        {
            FSMState transitionState = currentState.GetTransition(eventId);
            return transitionState;
        }
    }
}