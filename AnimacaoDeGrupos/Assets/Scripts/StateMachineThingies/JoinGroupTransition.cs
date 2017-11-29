using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StateMachine;

namespace Assets.Scripts.StateMachineThingies
{
    class JoinGroupTransition : Transition
    {
        State TargetState;
        public State GetTargetState()
        {
            return TargetState;
        }

        public bool IsTriggered()
        {
            AgentBaseState cast = (AgentBaseState)TargetState;
            if (cast.GetOwner().Parameters.ID != 0)
            {
                return true;
            }
            return false;
        }

        public void SetTargetState(State targetState)
        {
            TargetState = targetState ;
        }
    }
}
