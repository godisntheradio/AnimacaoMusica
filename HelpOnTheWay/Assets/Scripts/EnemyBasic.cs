using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : Enemy
{
    Vector3 P0, P1, P2, P3;
    float t;
    float LifeSpan = 30;
   
    public override void Initialize(ShipSettings shipSettings)
    {
        base.Initialize(shipSettings);
        t = 0.0f; 
        Clock = 0;
        
    }
    public void InitializeCurve(Vector3 inital, Vector2 Bounds)
    {
        P0 = inital;
        P0.x -= Bounds.x;
        Move(P0);
        float min = Bounds.x; float max = Bounds.y; float height = inital.y; float depth = inital.z ;
        P0 = TraceRoute(min, max, height, depth);
        P1 = TraceRoute(min, max, height, depth - (depth * 0.2f));
        P2 = TraceRoute(min, max, height, depth - (depth * 0.4f));
        P3 = TraceRoute(min, max, height, depth - (depth * 0.8f));
    }
    public override void Birth()
    {
        State = EnemyState.BEHAVIOUR;
        //play spawnSound
    }
    float Clock = 0;
    public override void Behaviour()
    {
        Clock += Time.deltaTime;
        t += Time.deltaTime / Settings.MovementSpeed;
        Vector3 novaPos = new Vector3();
        novaPos = Mathf.Pow(1 - t, 3) * P0 + 3 * t * Mathf.Pow(1 - t, 2) * P1 +
            3 * Mathf.Pow(t, 2) * (1 - t) * P2 + Mathf.Pow(t, 3) * P3;
        transform.position = novaPos;
        if (Clock > LifeSpan)
        {

            State = EnemyState.EXPIRE;
        }
    }
   
    public override void Death()
    {
        AudioSystem.PlayDeathSound();
        if (AudioSystem.hasDeathSound && !AudioSystem.IsPlayingDeathSound())
        {
            gameObject.SetActive(false);
        }
    }
    public override void Expire()
    {
        gameObject.SetActive(false);
    }
    Vector3 TraceRoute(float min, float max, float height, float depth)
    {
        float x = Random.Range(min, max);
        return new Vector3(x, height, depth);
    }
}
