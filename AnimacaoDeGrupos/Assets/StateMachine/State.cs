using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMachine
{
    public interface State
    {
        void Action();
	    void EntryAction();
	    void ExitAction();
	    List<Transition> GetTransitions();
    }
}
