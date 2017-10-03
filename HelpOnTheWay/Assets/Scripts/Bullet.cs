using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    LINEAR, CIRCULAR, ESPIRAL, CURVE
}
[System.Serializable]
public struct BulletSettings
{
    //type of movement
    public BulletType Type;
    //how much damage it does
    public float Damage;
    public float Speed;
}

public class Bullet : MonoBehaviour
{
    BulletSettings Settings;
    float LifeSpan = 10;
    float LifeSpanClock = 0;
    public void Initialize(BulletSettings settings)
    {
        Settings = new BulletSettings();
        Settings = settings;
        LifeSpanClock = 0;
    }
	void Start ()
    {
		
	}
	
	void Update ()
    {
        LifeSpanClock += Time.deltaTime;
        if (LifeSpanClock < LifeSpan)
        {
            Behaviour();
        }
        else
        {
            KillBullet();
        }
	}
    private void OnCollisionEnter(Collision collision)
    {
        //call take damage from Enemy
        IDamageable damageable = null;
        if (collision.gameObject.GetComponent<Enemy>())
        {
            damageable = (IDamageable)collision.gameObject.GetComponent<Enemy>();
        }
        else if (collision.gameObject.GetComponent<Player>())
        {
            damageable = (IDamageable)collision.gameObject.GetComponent<Player>();
        }
        if (damageable != null)
        {
            damageable.TakeDamage(Settings.Damage);
        }
    }
    private void KillBullet()
    {
        //animation
        gameObject.SetActive(false);
    }
    public void Move(Vector3 movement)
    {
        transform.position += transform.forward * Settings.Speed;
    }
    public virtual void Behaviour()
    {
        Move(new Vector3());
    }
}
