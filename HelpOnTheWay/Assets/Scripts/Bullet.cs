using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    LINEAR, CIRCULAR, ESPIRAL, CURVE
}

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

    public void Initialize(BulletSettings settings)
    {
        Settings = new BulletSettings();
        Settings = settings;
    }
	void Start ()
    {
		
	}
	
	void Update ()
    {
		
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

}
