using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StateMachine;
using UnityEngine;
namespace Assets.Scripts.StateMachineThingies
{
    class AgentBaseState : State
    {
        List<Transition> TransitionList;
        protected Agent agent;
        public AgentBaseState(Agent owner)
        {
            TransitionList = new List<Transition>();
            agent = owner;
        }
        public Agent GetOwner() 
        {
            return agent;
        }
        public virtual void Action()
        {
            throw new NotImplementedException();
        }

        public virtual void EntryAction()
        {
            throw new NotImplementedException();
        }

        public virtual void ExitAction()
        {
            throw new NotImplementedException();
        }
        public virtual void CollisionReaction(Collider collision)
        {
            throw new NotImplementedException();
        }

        public List<Transition> GetTransitions()
        {
            return TransitionList;
        }
        public void AddTransitions(Transition newTransition)
        {
            TransitionList.Add(newTransition);
        }
    }
}
