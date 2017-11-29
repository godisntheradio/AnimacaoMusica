using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField]
    GameObject Camera;
    public float CameraHeight = 50;
    //
    List<Agent> AgentList;
    public Agent Leader;
    [SerializeField]
    AgentParameters groupParams;
    int IDcount = 0;


    public LayerMask mask;

    public Vector3 Focus = Vector3.zero;

    void Start ()
    {
        AgentList = new List<Agent>();
        AddToGroup(Leader);
	}
	void Update ()
    {
        Focus = GetFocus();

        GetInputs();

        foreach (Agent agent in AgentList)
        {
            agent.UpdateParameters(groupParams);
            agent.FSM.Update();
        }


        //UpdateCamera();
	}
    public void AddToGroup(Agent newAgent)
    {
        if (newAgent.Parameters.ID == 0)
        {
            IDcount++;
            groupParams.ID = IDcount;
            AgentList.Add(newAgent);
            newAgent.Initialize(this, groupParams);
        }
    }
    public List<Agent> GetGroup()
    {
        return AgentList;
    }
    // Camera Operations
    //public float CorrectionFactor = 10;
    //void UpdateCamera()
    //{
    //    Vector3 NewPos = Focus;
    //    NewPos.x -= Mathf.Pow(CameraHeight / CorrectionFactor, 2);
    //    NewPos.y = CameraHeight;
    //    NewPos.z -= Mathf.Pow(CameraHeight / CorrectionFactor, 2);

    //    Camera.transform.position = Vector3.Lerp(Camera.transform.position, NewPos, Time.deltaTime);
    //}
    Vector3 GetFocus()
    {
        Ray FromCamera = Camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(FromCamera, out hit, 1000, mask.value);
        if (hit.point != Vector3.zero)
        {
            return hit.point;
        }
        else
        {
            return Focus;
        }
    }
    public float CameraSpeed = 5;
    void GetInputs()
    {
        Vector3 input = new Vector3();
        if (Input.GetKey(KeyCode.RightArrow))
        {
            input.x += 1.0f;
            input.z += -1.0f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            input.x += -1.0f;
            input.z += 1.0f;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            input.x += 1.0f;
            input.z += 1.0f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            input.x += -1.0f;
            input.z += -1.0f;
        }
        Camera.transform.position += input * CameraSpeed * Time.deltaTime;
        Camera.transform.position = new Vector3(Camera.transform.position.x, CameraHeight, Camera.transform.position.z); 
    }
    //

    public void SetCohesion(float value)
    {
        groupParams.CohesionFactor = value;
    }
    public void SetAlignment(float value)
    {
        groupParams.AlignmentFactor = value;
    }
    public void SetSeparation(float value)
    {
        groupParams.SeparationFactor = value;
    }
    public void SetSeekFlee(float value)
    {
        groupParams.FleeLeaderFactor = value;
        groupParams.SeekLeaderFactor = 1 - value;
    }

}
