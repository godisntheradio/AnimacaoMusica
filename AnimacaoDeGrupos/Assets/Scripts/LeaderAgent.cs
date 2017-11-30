using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.StateMachineThingies;
namespace Assets.Scripts
{
    class LeaderAgent : Agent
    {
        public List<Vector3> FormationSlots;
        public override void Start()
        {
            RefRigidbody = GetComponent<Rigidbody>();
            RefSphere = GetComponent<SphereCollider>();
            FSM = new StateMachine.StateMachine();
            FormationSlots = new List<Vector3>();
            LeaderState leader = new LeaderState(this);

            FSM.AddState(leader);

            FSM.Start(leader);
        }
        public override void ApplySteerings(Vector3 Target)
        {
            Target = new Vector3(Target.x, transform.position.y, Target.z);
            Vector3 force = Steerings.Arrive(this, Target);
            RefRigidbody.AddForce(force);
            if (( Target - transform.position).magnitude > 3)
            {
                
                RefRigidbody.MoveRotation(Quaternion.LookRotation(Target - transform.position));
            }
        }
        public void CreateFormation()
        {
            FormationSlots.Clear();
            float HeightOffset = -2;
            float WidthOffset = 2;
            int triangleHeight = 0;
            int slotsInRow = 1;
            int countRowSlot = 0;
            foreach (Agent item in Group.GetGroup())
            {
                Vector3 Slot;
                if (item.Parameters.ID != Parameters.ID)
                {
                    Slot = new Vector3((-(slotsInRow / 2) * WidthOffset) + (countRowSlot * WidthOffset), 0, HeightOffset * triangleHeight);
                }
                else
                {
                    Slot = Vector3.zero;
                }
                FormationSlots.Add(Slot);
                countRowSlot++;
                if (countRowSlot == slotsInRow)
                {
                    triangleHeight++;
                    slotsInRow += 2;
                    countRowSlot = 0;
                }
            }
        }
    }
}
