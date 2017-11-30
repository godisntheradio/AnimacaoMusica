using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Scripts.StateMachineThingies
{
    class ExitState : AgentBaseState
    {
        public ExitState(Agent owner) : base(owner)
        {

        }
        public override void Action()
        {
            LeaderAgent leader = (LeaderAgent)agent.Group.Leader;
            Vector3 SlotInWorldSpace = leader.transform.TransformPoint(leader.FormationSlots[agent.Parameters.ID - 1]);

            //agent.ApplySteerings(SlotInWorldSpace);
            agent.RefRigidbody.AddForce(Steerings.Arrive(agent, SlotInWorldSpace));
            agent.RefRigidbody.MoveRotation(leader.transform.rotation);
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
