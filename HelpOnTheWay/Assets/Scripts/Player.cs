using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState
{
    BIRTH, ALIVE, DEAD
}
public class Player : Ship, IDamageable
{
    bool godMode = true;
    PlayerState State = PlayerState.BIRTH;
    public void TakeDamage(float damage)
    {
        if (!godMode)
        {
            Settings.HitPoints += damage;
            if (Settings.HitPoints <= 0)
            {
                State = PlayerState.DEAD;
            }
        }
    }

    void Start ()
    {
        base.Start();

	}
	void Update ()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }
        if (true)
        {

        }
	}
	void Spawn()
    {

    }
}
