using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.StateMachineThingies
{
    class InitialState : AgentBaseState
    {
        public InitialState(Agent owner) : base(owner)
        {

        }
        public override void Action()
        {
        }

        public override void EntryAction()
        {
            
        }

        public override void ExitAction()
        {
        }
        public override void CollisionReaction(Collider collision)
        {
            
        }
    }
}
