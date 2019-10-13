using System.Collections.Generic;

namespace StateMachineStuff
{
    public class State
    {
        public virtual void StateEnter() { }
        public virtual string StateUpdate() => null;
        public virtual void StateExit() { }
    }

    public class MidState : State
    {

        protected IDictionary<string, State> SubStatesMap;

        protected State CurrentSubState;

        public void addSubState(string name, State state)
        {
            SubStatesMap.Add(new KeyValuePair<string, State>(name, state));
        }
        protected void updateState(string NewState)
        {
            if (NewState != null)
            {
                if (SubStatesMap[NewState] != CurrentSubState)
                {
                    CurrentSubState.StateExit();
                    CurrentSubState = SubStatesMap[NewState];
                    CurrentSubState.StateEnter();
                }
            }
        }
    }

    public class BottomState : State
    {
        protected State Owner = null;

        public BottomState(State _owner)
        {
            Owner = _owner;
        }
    }
}

