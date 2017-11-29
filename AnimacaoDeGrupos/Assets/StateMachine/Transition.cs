using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMachine
{
    public interface Transition
    {
        bool IsTriggered();
        void SetTargetState(State targetState);
        State GetTargetState();
    }
}
