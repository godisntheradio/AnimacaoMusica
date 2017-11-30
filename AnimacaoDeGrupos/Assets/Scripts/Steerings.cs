using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Steerings
{
    
    public static Vector3 Seek(Agent agent, Vector3 Target)
    {
        Vector3 Result = new Vector3();
        Result = Target -  agent.transform.position;
        Result = Result.normalized * agent.Parameters.Speed;
        Result = Vector3.ClampMagnitude(Result, agent.Parameters.MaxForce);
        return Result - agent.RefRigidbody.velocity;
    }
    public static Vector3 Flee(Agent agent, Vector3 target)
    {
        if (Vector3.Distance(agent.transform.position, target) > agent.Parameters.Radius)
        {
            return new Vector3(0, 0, 0);
        }
        Vector3 desired = (agent.transform.position - target).normalized * agent.Parameters.Speed;
        return desired - agent.RefRigidbody.velocity;
    }
    public static Vector3 PathFollowing(Agent agent, Vector3 A, Vector3 B, Vector3 other)
    {
        return new Vector3();
    }
    public static Vector3 OffsetPursuit(Agent agent, Agent leader, Vector3 offset)
    {
        Vector3 WorldOffset = leader.transform.TransformPoint(offset);
        Vector3 ToOffset = WorldOffset - agent.transform.position;

        float LookAhead = ToOffset.magnitude / (agent.Parameters.MaxForce + leader.Parameters.Speed);

        return Arrive(agent, WorldOffset + leader.RefRigidbody.velocity * LookAhead);
    }
    public static Vector3 Arrive(Agent agent, Vector3 Target)
    {
        Vector3 Result = new Vector3();
        Vector3 ToTarget = Target - agent.transform.position;
        float desaceleration = 0.5f;
        if (ToTarget.magnitude > 0)
        {
            float speed = ToTarget.magnitude / desaceleration;
            speed = Mathf.Clamp(speed, 0, agent.Parameters.MaxForce);
            Result = ToTarget * speed / ToTarget.magnitude;
        }
        return Result - agent.RefRigidbody.velocity;
    }
    public static Vector3 Separation(Agent agent)
    {
        Vector3 force = new Vector3();
        foreach (Agent neighbor in agent.GetNeighbors())
        {
            if (agent.Parameters.ID != neighbor.Parameters.ID)
            {
                Vector3 toAgent = agent.transform.position - neighbor.transform.position;
                force += toAgent.normalized / (toAgent.magnitude / neighbor.Parameters.Radius );
            }
        }
        return force;
    }
    public static Vector3 Alignment(Agent agent)
    {
        Vector3 AverageHeading = Vector3.zero;
        foreach (Agent neighbor in agent.GetNeighbors())
        {
            if (agent.Parameters.ID != neighbor.Parameters.ID)
            {
                AverageHeading += neighbor.RefRigidbody.velocity;
            }
        }
        if (agent.GetNeighbors().Count > 0)
        {
            AverageHeading /= agent.GetNeighbors().Count;

            AverageHeading -= agent.RefRigidbody.velocity;
        }
        return AverageHeading;
    }
    public static Vector3 Cohesion(Agent agent)
    {
        Vector3 centerOfMass = Vector3.zero, SteeringForce = Vector3.zero;
        foreach (Agent neighbor in agent.GetNeighbors())
        {
            if (agent.Parameters.ID != neighbor.Parameters.ID)
            {
                centerOfMass += neighbor.transform.position;
            }
        }
        if (agent.GetNeighbors().Count > 0)
        {
            centerOfMass /= agent.GetNeighbors().Count;

            SteeringForce = Seek(agent, centerOfMass);
        }
        return SteeringForce;
    }
    /// <summary>
    /// Projects vector from start to point onto path vector
    /// </summary>
    public static Vector3 GetNormalPoint(Vector3 PathStart, Vector3 PathEnd, Vector3 Point)
    {
        Vector3 NormalPoint = new Vector3();
        Vector3 A = Point - PathStart;
        Vector3 B = PathEnd - PathStart;
        B.Normalize();
        NormalPoint = (B * Vector3.Dot(A, B)) + PathStart;
        return NormalPoint;
    }
}

