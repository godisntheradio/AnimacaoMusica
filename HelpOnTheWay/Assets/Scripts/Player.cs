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
            Settings.HitPoints -= damage;
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
    float x = 0;
	void Update ()
    {

        Vector3 movement = new Vector3();
        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement += Vector3.right;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement += Vector3.left;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            movement += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            movement += Vector3.back;
        }
        if (movement != Vector3.zero)
        {
            if (x < 1)
            {
                x += Time.deltaTime *2;
            }
            Move(movement * Settings.MovementSpeed * Mathf.Pow(x, 2));
        }
        else
        {
            if (x > 0)
            {
                x -= Time.deltaTime * 2;
            }
        }
    }
	void Spawn()
    {

    }
}
