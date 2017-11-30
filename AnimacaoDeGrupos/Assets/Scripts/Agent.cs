using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.StateMachineThingies;
[System.Serializable]
public struct AgentParameters
{
    public float Speed;
    public float MaxForce;
    public int ID;
    public float Radius;
    public float SeparationFactor;
    public float CohesionFactor;
    public float AlignmentFactor;
    public float SeekLeaderFactor;
    public float FleeLeaderFactor;

    public AgentParameters(int id,float speed, float maxForce, float radius, float cohesion, float alignment, float separation, float seek, float flee)
    {

        Speed = speed;
        MaxForce = maxForce;
        ID = id;
        Radius = radius;
        SeparationFactor = separation;
        CohesionFactor = cohesion;
        AlignmentFactor = alignment;
        SeekLeaderFactor = seek;
        FleeLeaderFactor = flee;
}

}

public class Agent : MonoBehaviour
{
    public AgentParameters Parameters;
    public Manager Group;
    public Rigidbody RefRigidbody;
    public SphereCollider RefSphere;

    public StateMachine.StateMachine FSM;

    List<Agent> Neighbors;

    public virtual void Start ()
    {
        RefRigidbody = GetComponent<Rigidbody>();
        RefSphere = GetComponent<SphereCollider>();
        Neighbors = new List<Agent>();
        FSM = new StateMachine.StateMachine();



        InitialState initial = new InitialState(this);
        GroupState group = new GroupState(this);
        ExitState exit = new ExitState(this);

        JoinGroupTransition JG = new JoinGroupTransition();
        JG.SetTargetState(group);
        ExitTriggerTransition ET = new ExitTriggerTransition();
        ET.SetTargetState(exit);
        ExitFormationTransition EF = new ExitFormationTransition();
        EF.SetTargetState(group);

        initial.AddTransitions(JG);
        group.AddTransitions(ET);
        exit.AddTransitions(EF);

        FSM.AddState(initial);
        FSM.AddState(group);
        FSM.AddState(exit);

        FSM.Start(initial);
    }
    public void Initialize(Manager agents, AgentParameters stats)
    {
        Group = agents;
        Parameters = stats;
    }
    public void UpdateParameters(AgentParameters stats)
    {
        Parameters.MaxForce = stats.MaxForce;
        Parameters.Speed = stats.Speed;
        Parameters.Radius = stats.Radius;
        Parameters.SeparationFactor = stats.SeparationFactor ;
        Parameters.CohesionFactor = stats.CohesionFactor;
        Parameters.AlignmentFactor = stats.AlignmentFactor;
        Parameters.FleeLeaderFactor = stats.FleeLeaderFactor;
        Parameters.SeekLeaderFactor = stats.SeekLeaderFactor;
    }
    public virtual void ApplySteerings(Vector3 Target)
    {
        Vector3 total = new Vector3();
        FindNeighbors();
        total += Steerings.Separation(this) * Parameters.SeparationFactor;
        total += Steerings.Alignment(this) * Parameters.AlignmentFactor;
        total += Steerings.Cohesion(this) * Parameters.CohesionFactor;
        total += Steerings.Seek(this, Target) * Parameters.SeekLeaderFactor;
        total += Steerings.Flee(this, Target) * Parameters.FleeLeaderFactor;

        RefRigidbody.AddForce(total);
        if (total != Vector3.zero)
        {
            RefRigidbody.MoveRotation( Quaternion.LookRotation(total));
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        AgentBaseState cast = (AgentBaseState)FSM.CurrentState;
        cast.CollisionReaction(collision);
    }
    public List<Agent> GetNeighbors()
    {
        return Neighbors;
    }
    public void FindNeighbors()
    {
        Neighbors.Clear();
        foreach (Agent agent in Group.GetGroup())
        {
            if (this.Parameters.ID != agent.Parameters.ID && agent.Parameters.ID != Group.Leader.Parameters.ID && Vector3.Distance(transform.position, agent.transform.position) < Parameters.Radius)
            {
                Neighbors.Add(agent);
            }
        }
    } 
}
