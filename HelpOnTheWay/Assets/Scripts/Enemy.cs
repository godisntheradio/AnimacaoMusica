﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    BIRTH, BEHAVIOUR, DEATH
}



public class Enemy : Ship , IDamageable, IEnemy
{

    EnemyState State;
	void Start ()
    {
        base.Start();
	}
    public virtual void Initialize(ShipSettings shipSettings)
    {
        base.Initialize(shipSettings);
        State = EnemyState.BIRTH;
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
    
    
    public void TakeDamage(float damage)
    {
        Settings.HitPoints -= damage;
        if (Settings.HitPoints <= 0)
        {
            State = EnemyState.DEATH;
        }
    }

    public virtual void Birth()
    {
        AudioSystem.PlayTestSound();
        State = EnemyState.BEHAVIOUR;
    }
    float TestClock = 0;
    public virtual void Behaviour()
    {
        TestClock += Time.deltaTime;
        if (TestClock > 20)
        {
            State = EnemyState.DEATH;
            TestClock = 0;
        }
    }

    public virtual void Death()
    {
        AudioSystem.PlayDeathSound();
        if (!AudioSystem.IsPlayingSound())
        {
            gameObject.SetActive(false);
        }
    }
}
