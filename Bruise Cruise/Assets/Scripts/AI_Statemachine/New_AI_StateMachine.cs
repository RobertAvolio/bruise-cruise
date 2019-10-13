using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachineStuff;

public class New_AI_StateMachine
{
    public class EnemySM : MidState
    {
        public override string StateUpdate()
        {

            updateState(CurrentSubState.StateUpdate());

            return null;
        }
    }

    EnemySM EnemySM_topstate = new EnemySM();

    EnemySM_topstate.updateState();
}


