using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Player playerRef;
    [SerializeField]
    ShipSettings settings;
    [SerializeField]
    EnemyEmitter emiterRef;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public void BeginGame()
    {

        playerRef.Initialize(settings);
    }
}
