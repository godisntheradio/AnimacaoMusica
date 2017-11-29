using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMachine
{
    public class StateMachine
    {
        State InitialState;
        public State CurrentState;

        List<State> StateList;

        public StateMachine()
        {
            StateList = new List<State>();
            InitialState= null;
            CurrentState = null;
        }

        public void Start(State initial)
        {
            InitialState = initial;
            CurrentState = initial;
            CurrentState.EntryAction();
        }
        public void Update()
        {

            if (CurrentState != null)
            {
                Transition triggeredTransition= null;
                foreach (Transition transition in CurrentState.GetTransitions())
                {
                    if (transition.IsTriggered())
                    {
                        triggeredTransition = transition;
                    }
                }
                

                if (triggeredTransition != null)
                {
                    State targetState = triggeredTransition.GetTargetState();
                    CurrentState.ExitAction();
                    targetState.EntryAction();
                    CurrentState = targetState;
                }
                else
                {
                    CurrentState.Action();
                }
            }
        }
        public void AddState(State newState)
        {
            StateList.Add(newState);
        }

    }
}
