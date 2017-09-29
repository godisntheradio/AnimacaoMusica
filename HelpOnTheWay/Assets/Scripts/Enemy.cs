using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    BIRTH, BEHAVIOUR, DEATH
}

public class Enemy : MonoBehaviour , IDamageable, IEnemy
{
    float HP;
    float RateOfFire;
    GameObject bullet;

    EnemyState State;

    List<GameObject> bulletpool;


	void Start ()
    {
		
	}
	void Initialize(float hp, GameObject typeOfBullet, float rof)
    {
        HP = hp;
        bullet = typeOfBullet;
        RateOfFire = rof;
    }
    void Update ()
    {
        switch (State)
        {
            case EnemyState.BIRTH:
                Birth();
                break;
            case EnemyState.BEHAVIOUR:
                Behaviour();
                break;
            case EnemyState.DEATH:
                Death();
                break;
            default:
                break;
        }
    }
    
    void Shoot()
    {

    }
    GameObject GetGameObjectFromPool()
    {
        if (bulletpool.Count < 20)
        {
            GameObject obj = Instantiate(bullet, transform.position, transform.rotation);
            bulletpool.Add(obj);
            return obj;
        }
        else
        {
            foreach (GameObject item in bulletpool)
            {
                if (!item.activeSelf)
                {
                    item.transform.position = transform.position;
                    item.transform.rotation = transform.rotation;
                    item.SetActive(true);
                    return item;
                }
            }
            return null;
        }
    }
    public void TakeDamage(float damage)
    {
        HP += damage;
        if (HP <= 0)
        {
            State = EnemyState.DEATH;
        }
    }

    public void Birth()
    {
        throw new NotImplementedException();
    }

    public void Behaviour()
    {
        throw new NotImplementedException();
    }

    public void Death()
    {
        throw new NotImplementedException();
    }
}
