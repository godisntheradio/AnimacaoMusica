using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StateMachine;
using UnityEngine;
namespace Assets.Scripts.StateMachineThingies
{
    class ExitTriggerTransition : Transition
    {
        State TargetState;
        public State GetTargetState()
        {
            return TargetState;
        }

        public bool IsTriggered()
        {
            return Input.GetKeyDown(KeyCode.F);
        }

        public void SetTargetState(State targetState)
        {
            TargetState = targetState;
        }
    }
}
