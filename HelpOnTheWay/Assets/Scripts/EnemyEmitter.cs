using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEmitter : MonoBehaviour
{
    Mesh meshRef;
    ObjectPool Pool;
    [SerializeField]
    //spawns per second
    float SpawnRate;
    [SerializeField]
    GameObject enemyType1;
    [SerializeField]
    int Limit = 20;

    //
    [SerializeField]
    GameObject bullet;
    //

	void Start ()
    {
        meshRef = gameObject.GetComponent<MeshFilter>().mesh;
        Pool = new ObjectPool(enemyType1, transform, Limit);
    }
    float Clock = 0;
	void Update ()
    {
        Clock += Time.deltaTime;
        if (Clock >= SpawnRate)
        {
            Spawn();
            Clock = 0;
        }

	}
    public Vector3 GetARandomTreePos()
    {

        Bounds bounds = meshRef.bounds;
        Vector3 center = bounds.center;
        Vector3 size = new Vector3();
        size.x = (bounds.extents.x * transform.localScale.x) ;
        size.y = (bounds.extents.y * transform.localScale.y) ;
        size.z = (bounds.extents.z * transform.localScale.z) ;

        return transform.position + center + new Vector3(
                    Random.Range(size.x, - size.x),
                    transform.position.y,
                    Random.Range(size.z, -size.z)
                 );
    }
    void Spawn()
    {
        GameObject newEnemy = Pool.GetGameObjectFromPool();
        if (newEnemy)
        {
            Enemy enemy = newEnemy.GetComponent<Enemy>();
            if (!enemy)
            {
                enemy = newEnemy.AddComponent<Enemy>();
            }
            enemy.Move(GetARandomTreePos());
            ShipSettings setts = new ShipSettings();
            BulletSettings bSetts = new BulletSettings();
            bSetts.Damage = 10;
            bSetts.Speed = 5;
            bSetts.Type = BulletType.LINEAR;
            setts.Bullet = bullet;
            setts.BulletParameters = bSetts;
            setts.HitPoints = 25;
            setts.MovementSpeed = 10;
            setts.RateOfFire = 0.5f;

            enemy.Initialize(setts);  
        }
        else
        {
            Debug.Log("nao rolô spawna o bixo");
        }
    }


}
