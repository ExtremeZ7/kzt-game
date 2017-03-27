using UnityEngine;
using Common.FSM;

public class AITest : MonoBehaviour
{
    FSM fsm;
    FSMState PatrolState;
    FSMState IdleState;
    TextAction PatrolAction;
    TextAction IdleAction;

    void Start()
    {
        fsm = new FSM("AITest FSM");
        IdleState = fsm.AddState("IdleState");
        PatrolState = fsm.AddState("PatrolState");
        PatrolAction = new TextAction(PatrolState);
        IdleAction = new TextAction(IdleState);

        //This adds the actions to the state and add state to it's transition map
        PatrolState.AddAction(PatrolAction);
        IdleState.AddAction(IdleAction);

        PatrolState.AddTransition("ToIdle", IdleState);
        IdleState.AddTransition("ToPatrol", PatrolState);

        //This initializes the actions
        PatrolAction.Init("AI on patrol", 3.0f, "ToIdle");
        IdleAction.Init("AI on Idle", 2.0f, "ToPatrol");

        //Starts the FSM
        fsm.Start("IdleState");
    }

    void Update()
    {
        fsm.Update();
    }
}
