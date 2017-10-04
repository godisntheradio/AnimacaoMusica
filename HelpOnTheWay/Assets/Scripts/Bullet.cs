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
    float LifeSpan = 6;
    float LifeSpanClock = 0;
    public void Initialize(BulletSettings settings)
    {
        Settings = new BulletSettings();
        Settings = settings;
        LifeSpanClock = 0;
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }


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
        Debug.Log("colidiu");
        IDamageable damageable = SearchComponent<Enemy>(collision.transform);
        //search through child
        if (damageable != null)
        {
            damageable.TakeDamage(Settings.Damage);
            Debug.Log("acertou");
        }
    }
    private void KillBullet()
    {
        //animation
        gameObject.SetActive(false);
    }
    public void Move(Vector3 movement)
    {
        //movement *= Time.deltaTime;
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().MovePosition(transform.position + (Vector3.forward * Settings.Speed));
        }
        else
        {
            transform.position  +=  (Vector3.forward * Settings.Speed);
        }
    }
    public virtual void Behaviour()
    {
        Move(new Vector3());
    }
    public T SearchComponent<T>(Transform transform)
    {
        //Search Up
        if (transform)
        {
            T comp = transform.GetComponent<T>();
            if (comp != null)
            {
                return comp;
            }
            else
            {
                comp = SearchComponent<T>(transform.parent);
                if (comp != null)
                {
                    return comp;
                }
            }

            //search Down
            //for (int i = 0; i < transform.childCount; i++)
            //{
            //    T comp = transform.GetChild(i).GetComponent<T>();
            //    if (comp != null)
            //    {
            //        return comp;
            //    }
            //    if (transform.GetChild(i).childCount > 0)
            //    {
            //        SearchComponent<T>(transform.GetChild(i));
            //    }
            //}
        }
        return default(T);
    }

}
