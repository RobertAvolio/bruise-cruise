using UnityEngine;
using System.Collections.Generic;

//all the stuff you would need to build a state, and then a state machine
//TODO: figure out default substates, States should easily have them set when the statemachine is being assembled, and should run their enter function when the game starts
namespace StateMachineStuff
{
    //Generic state class that all states and state machines inherit from
    //<T> is what statemachine the state will be a part of
    public abstract class State<T>
    {
        //functions to run when entering, exiting, and executing a state
        public abstract void EnterState();
        public abstract string RunState();
        public abstract void ExitState();

        //stores all of a states substates, along with their names
        protected Dictionary<string, State<T>> SubStatesMap = new Dictionary<string, State<T>>();

        //stores the current state, should be protected but I haven't figured out default substates yet so I set it manually
        public State<T> CurrentSubState = null;
        
        //the state all the way at the top of the tree, the 'Statemachine' that all it' states are a part of
        protected T Owner;
        
        //sets the owner
        public void setOwner(T owner)
        {
            Owner = owner;
        }

        //appends a given substate and name to the substates map
        public void addSubState(string name, State<T> state)
        {
            SubStatesMap.Add(name, state);
        }

        //Update function, only runs core state update code, state specific code goes into RunState()
        public string StateUpdate()
        {
            if (CurrentSubState != null)
            {
                updateState(CurrentSubState.StateUpdate());
            }
            return this.RunState();
        }

        //Changes the substate, running the ExitState and EnterState functions for it
        private void updateState(string NewState)
        {
            if (NewState != null)
            {
                if (SubStatesMap[NewState] != CurrentSubState)
                {
                    CurrentSubState.ExitState();
                    CurrentSubState = SubStatesMap[NewState];
                    CurrentSubState.EnterState();
                }
            }
        }
    }
}

