using UnityEngine;
using System.Linq;
using System.Collections.Generic;

//all the stuff you would need to build a state, and then a state machine
//TODO: figure out default substates, States should easily have them set when the statemachine is being assembled, and should run their enter function when the game starts
namespace StateMachineStuff
{
    //Generic state class that all states and state machines inherit from
    //<T> is what statemachine the state will be a part of
    public abstract class State<T> where T : State<T>
    {
        //functions to run when entering, exiting, and executing a state
        public virtual void EnterState()
        {
            //run the enter function for the current substate, enter the defaultsubstate if that doesn't exist
            if (defaultSubState != null)
            {
                CurrentSubState = defaultSubState;
                SubStatesMap[CurrentSubState].EnterState();
            }
        }
        public abstract string RunState();
        public virtual void ExitState()
        {
            if (CurrentSubState != null)
            {
                SubStatesMap[CurrentSubState].ExitState();
            }
        }

        //stores all of a states substates, along with their names
        protected Dictionary<string, State<T>> SubStatesMap = new Dictionary<string, State<T>>();

        //stores the current state
        protected string CurrentSubState = null;
        
        //the state all the way at the top of the tree, the 'Statemachine' that all it' states are a part of
        protected T Owner;

        //the state first entered when the game starts
        protected string defaultSubState;

        //retuyrns the current substate
        public string getCurrentSubState()
        {
            return CurrentSubState;
        }

        //sets the owner
        public void setOwner(T owner)
        {
            Owner = owner;
        }

        //set the default substate
        public void setDefaultSubState(string state)
        {
            defaultSubState = state;
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
                updateState(SubStatesMap[CurrentSubState].StateUpdate());
            }
            return this.RunState();
        }

        //Changes the substate, running the ExitState and EnterState functions for it
        public void updateState(string NewState)
        {
            if (NewState != null)
            {
                if (NewState != CurrentSubState)
                {
                    SubStatesMap[CurrentSubState].ExitState();
                    CurrentSubState = NewState;
                    SubStatesMap[CurrentSubState].EnterState();
                }
            }
        }
    }

    //container for all of the states to make construction easier, use this to interact with the structure of the machine
    public class SMContainer<T> where T : State<T>
        {

        //information about how this container is connected to the others and to the machine
        public State<T> State;
        public string Name;     //having actual strings to refer to the states isn't necessary but it makes it easier to code them
        public Dictionary<string, SMContainer<T>> Children;
        private SMContainer<T> Parent;

        //reparent the container
        public void setParent(SMContainer<T> parent)
        {
            Parent?.Children.Remove(Name);
            Parent = parent;
            parent.Children.Add(Name, this);
        }

        //return the parent container
        public SMContainer<T> getParent()
        {
            return Parent;
        }

        //find the container at the top of the tree
        public SMContainer<T> findOwner()
        {
            if (Parent == null)
            {
                return this;
            }
            return Parent.findOwner();
        }

        //initialize the container with it's state and all of it's children
        public SMContainer(string name, State<T> state, List<SMContainer<T>> children)
        {
            Name = name;
            State = state;

            //set the default substate to the first in the list of children
            if (children != null)
                State.setDefaultSubState(children[0].Name);

            Children = new Dictionary<string, SMContainer<T>>();
            children?.ForEach(child =>
            {
                Children.Add(child.Name, child);
            });
        }

        //return a list of all the states in the tree, inculding itself     R E C U R S I V E L Y
        public List<SMContainer<T>> ReturnAllSubStates()
        {
            List<SMContainer<T>> statesList = new List<SMContainer<T>>();
            statesList.Add(this);
            foreach (KeyValuePair<string, SMContainer<T>> child in Children)
            {
                statesList.AddRange(child.Value.ReturnAllSubStates());
            }

            return statesList;
        }

        //add the necessary substates and set the owners for each state and container in the tree
        public void assemble()
        {
            foreach (KeyValuePair<string, SMContainer<T>> child in Children)
            {
                State.addSubState(child.Value.Name, child.Value.State);
                child.Value.assemble();
            }
        }

        //returns the container of the current active substate
        public SMContainer<T> getActiveChild()
        {
            return Children?[State.getCurrentSubState()];
        }

        //return a list of all the active states in the tree, probably acts a little fucky if the container it's called on isn't active
        public List<string> getActiveStates()
        {
            List<string> activeStates = new List<string>() { Name };

            activeStates.Add(getActiveChild().Name);
            activeStates.AddRange(getActiveChild().getActiveStates());

            return activeStates;
        }

        //return the highest level container with a given name
        public SMContainer<T> getState(string stateName)
        {
            if (Children?[stateName] != null)
            {
                return Children[stateName];
            }
            return getActiveChild()?.getState(stateName);
        }

        //for the statemachine into a given state
        public void forceState(string state)
        {
            if (!getActiveStates().Contains(state))
            {
                State<T> parentState = getState(state).Parent.State;
                parentState.updateState(state);
            }
        }
    }
}

