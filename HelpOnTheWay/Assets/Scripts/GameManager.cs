using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject StartPoint;
    [SerializeField]
    GameObject PlayerPrefab;
    [SerializeField]
    Player playerRef;
    [SerializeField]
    ShipSettings settings;
    [SerializeField]
    EnemyEmitter emiterRef;
    
	void Start ()
    {
        emiterRef.SetEmiterSource(GetComponent<WaveEmitter>().GetAudioSource());
		
	}
	
	void Update ()
    {
		
	}

    public void BeginGame()
    {
        if (!playerRef)
        {
            playerRef = Instantiate(PlayerPrefab, StartPoint.transform.position, StartPoint.transform.rotation).GetComponent<Player>();
        }
        playerRef.Initialize(settings);

    }
}
