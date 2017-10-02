using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            return null;
        }
    }
}

public class Ship : MonoBehaviour
{
    public  ShipSettings Settings;

    public ObjectPool Pool;

    public ShipAudio AudioSystem;
   
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
	}

    public virtual void Update ()
    {
		
	}
    public virtual void Initialize(ShipSettings shipSettings)
    {
        Settings = shipSettings;
        if (Pool == null)
        {
            Pool = new ObjectPool(shipSettings.Bullet, this.transform, 20);
        }
    }
    public void Shoot()
    {
        GameObject newBullet = Pool.GetGameObjectFromPool();
        if (newBullet.GetComponent<Bullet>())
        {
            newBullet.GetComponent<Bullet>().Initialize(Settings.BulletParameters);
        }
        else
        {
            Bullet component = newBullet.AddComponent<Bullet>();
            component.Initialize(Settings.BulletParameters);
        }
    }
    public void Move()
    {

    }

}
