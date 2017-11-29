using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.StateMachineThingies
{
    class GroupState : AgentBaseState
    {
        public GroupState(Agent owner) : base(owner)
        {

        }
        public override void Action()
        {
            agent.ApplySteerings(agent.Group.Focus);
        }

        public override void EntryAction()
        {
        }

        public override void ExitAction()
        {

        }
        public override void CollisionReaction(Collider collision)
        {
            Agent validAgent = collision.gameObject.GetComponent<Agent>();
            if (validAgent)
            {
                agent.Group.AddToGroup(validAgent);
            }
        }
    }
}
