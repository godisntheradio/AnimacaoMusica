using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct ShipSettings
{
    public float HitPoints;
    public float RateOfFire;
    public float MovementSpeed;
    public BulletSettings BulletParameters;
    public GameObject Bullet;
}

public class ObjectPool
{
    List<GameObject> Pool;
    GameObject toInstantiate;
    Transform Owner;
    int LimitToPool;

    public ObjectPool()
    {
        Pool = new List<GameObject>();
        toInstantiate = null;
        Owner = null;
        LimitToPool = 20;
    }
    public ObjectPool(GameObject toPool, Transform owner, int limit)
    {
        Pool = new List<GameObject>();
        toInstantiate = toPool;
        Owner = owner;
        LimitToPool = limit;
    }

    public GameObject GetGameObjectFromPool()
    {
        if (Pool.Count < LimitToPool)
        {
            GameObject obj = GameObject.Instantiate(toInstantiate, Owner.position, Owner.rotation);
            Pool.Add(obj);
            return obj;
        }
        else
        {
            foreach (GameObject item in Pool)
            {
                if (!item.activeSelf)
                {
                    item.transform.position = Owner.position;
                    item.transform.rotation = Owner.rotation;
                    item.SetActive(true);
                    return item;
                }
            }
            LimitToPool++;
            return GetGameObjectFromPool();
        }
    }
    public int GetActiveCount()
    {
        int active = 0;
        foreach (GameObject item in Pool)
        {
            if (item.activeSelf)
            {
                active++;
            }
        }
        return active;
    }
}

public class Ship : MonoBehaviour
{
    public  ShipSettings Settings;

    public ObjectPool Pool;

    public ShipAudio AudioSystem;

    protected ShipSettings InitialSettings;


    public virtual void Start ()
    {
        if (GetComponent<ShipAudio>() == null)
        {
            AudioSystem = gameObject.AddComponent<ShipAudio>();
        }
        else
        {
            AudioSystem = GetComponent<ShipAudio>();
        }
        AudioSystem.Initialize();
	}

    public virtual void Update ()
    {
		
	}
    public virtual void Initialize(ShipSettings shipSettings)
    {
        Settings = shipSettings;
        InitialSettings = shipSettings;
        if (Pool == null)
        {
            Pool = new ObjectPool(shipSettings.Bullet, transform, 20);
        }
    }
    float FireClock = 0;
    bool CanFire = true; 
    public void Shoot()
    {
        if (CanFire)
        {
            GameObject newBullet = Pool.GetGameObjectFromPool();
            AudioSystem.PlayTestSound();
            if (newBullet)
            {
                if (newBullet.GetComponent<Bullet>())
                {
                    newBullet.GetComponent<Bullet>().Initialize(Settings.BulletParameters);
                }
                else
                {
                    Bullet component = newBullet.AddComponent<Bullet>();
                    component.Initialize(Settings.BulletParameters);
                }
                newBullet.GetComponent<Bullet>().Move(transform.position);
                AudioSystem.PlayShootSound();
                FireClock = 0;
            }
            else
            {
                Debug.Log("erro ao atirar");
            }
            CanFire = false;

        }
        else
        {
            FireClock += Time.deltaTime;
            if (FireClock >= Settings.RateOfFire)
            {
                CanFire = true;
                FireClock = 0;
            }
        }
    }
    public void Move(Vector3 movement)
    {
        movement += Vector3.Scale(transform.position, new Vector3(1,0,1));
        
        if (GetComponent<Rigidbody>())
        {
            GetComponentInChildren<Rigidbody>().angularVelocity = Vector3.zero;
            GetComponentInChildren<Rigidbody>().drag = 0;
            GetComponent<Rigidbody>().MovePosition(movement);
        }
        else if (GetComponentInChildren<Rigidbody>())
        {
            GetComponentInChildren<Rigidbody>().angularVelocity = Vector3.zero;
            GetComponentInChildren<Rigidbody>().drag = 0;
            GetComponentInChildren<Rigidbody>().MovePosition(movement);
        }
        else
        {
            transform.position = movement;
        }
    }

}
