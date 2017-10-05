using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    BIRTH, BEHAVIOUR, DEATH, EXPIRE
}



public class Enemy : Ship , IDamageable, IEnemy
{

    protected EnemyState State;
	
    public virtual void Initialize(ShipSettings shipSettings)
    {
        base.Initialize(shipSettings);
        
        State = EnemyState.BIRTH;
    }
    public virtual void Update ()
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
            case EnemyState.EXPIRE:
                Expire();
                break;
            default:
                break;
        }
    }
    
    
    public void TakeDamage(float damage)
    {
        Settings.HitPoints -= damage;
        AudioSystem.PlayHitSound();

        if (Settings.HitPoints <= 0)
        {
            State = EnemyState.DEATH;

        }
    }

    public virtual void Birth()
    {
        //State = EnemyState.BEHAVIOUR;
    }
    //float TestClock = 0;
    public virtual void Behaviour()
    {
        //TestClock += Time.deltaTime;
        //if (TestClock > 20)
        //{
        //    State = EnemyState.EXPIRE;
        //    TestClock = 0;
        //}
    }

    public virtual void Death()
    {
        //AudioSystem.PlayDeathSound();
        //if (AudioSystem.hasDeathSound && !AudioSystem.IsPlayingDeathSound())
        //{
        //gameObject.SetActive(false);

        //}
    }
    public virtual void Expire()
    {
        //gameobject.setactive(false);
    }
}
