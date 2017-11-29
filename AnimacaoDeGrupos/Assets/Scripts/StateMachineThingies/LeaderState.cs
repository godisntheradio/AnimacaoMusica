using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Scripts.StateMachineThingies
{
    class LeaderState : AgentBaseState
    {
        public LeaderState(Agent owner) : base(owner)
        {

        }
        public override void Action()
        {
            LeaderAgent leader = (LeaderAgent)GetOwner();
            leader.CreateFormation();
            agent.ApplySteerings(agent.Group.Focus);
        }

        public override void EntryAction()
        {
        }

        public override void ExitAction()
        {
            throw new NotImplementedException();
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
