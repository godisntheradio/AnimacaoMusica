using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class EnemyEmitter : MonoBehaviour
{
    Mesh meshRef;
    ObjectPool Pool;
    ObjectPool Pool2;
    [SerializeField]
    //spawns per second
    float SpawnRate;
    [SerializeField]
    GameObject enemyType1;
    [SerializeField]
    GameObject enemyType2;
    [SerializeField]
    int Limit = 20;
    WaveAnalyser Analyser;
    [SerializeField]
    List<float> FrequencyList;

    //
    [SerializeField]
    GameObject bullet;
    //

	void Start ()
    {
        meshRef = gameObject.GetComponent<MeshFilter>().mesh;
        Pool = new ObjectPool(enemyType1, transform, Limit);
        Pool2 = new ObjectPool(enemyType2, transform, Limit);


        Analyser = new WaveAnalyser();
        
    }
    public void SetEmiterSource(AudioSource src)
    {
        Analyser.Initialize(src);
        
    }
    [SerializeField]
    float median;
    public float frequencyNow;
    public float Clock = 0;
	void FixedUpdate ()
    {
        Clock += Time.deltaTime;
        if (Clock >= SpawnRate)
        {
            float scale = 10000;
            median = Analyser.GetMedianOfSpectrum() * scale;
            foreach (float frequency in FrequencyList)
            {
                ShipSettings setts = new ShipSettings();
                //lows
                float currentFreq = frequency;
                Vector3 initial = GetARandomTreePos();
                initial.x = Analyser.GetFrequencyVolume(frequency) * scale / 10;
                frequencyNow = initial.x ;
                if (initial.x > 15 )
                {
                    BulletSettings bSetts = new BulletSettings();
                    bSetts.Damage = 5;
                    bSetts.Speed = 3;
                    bSetts.Type = BulletType.LINEAR;
                    setts.Bullet = bullet;
                    setts.BulletParameters = bSetts;
                    setts.HitPoints = 50;
                    setts.MovementSpeed = 1;
                    setts.RateOfFire = 0.5f;
                    SpawnBasic(setts, initial, GetEmiterBounds());
                }
                //mids
                currentFreq *= 3;
                setts = new ShipSettings();
                initial = GetARandomTreePos();
                initial.x = Analyser.GetFrequencyVolume(frequency) * scale / 10;
                if (initial.x > 30)
                {
                    BulletSettings bSetts = new BulletSettings();
                    bSetts.Damage = 5;
                    bSetts.Speed = 3;
                    bSetts.Type = BulletType.LINEAR;
                    setts.Bullet = bullet;
                    setts.BulletParameters = bSetts;
                    setts.HitPoints = 10;
                    setts.MovementSpeed = 15;
                    setts.RateOfFire = 0.5f;
                    SpawnBasic(setts, initial, GetEmiterBounds());
                }

            }
        
            Clock = 0;
        }
        

	}

    
    public Vector2 GetEmiterBounds()
    {
        Bounds bounds = meshRef.bounds;
        Vector3 center = bounds.center;
        Vector3 size = new Vector3();
        size.x = (bounds.extents.x * transform.localScale.x);
        return new Vector2( size.x, -size.x);
                    
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
    void Spawn(ShipSettings setts)
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
            

            enemy.Initialize(setts);  
        }
    }
    void SpawnBasic(ShipSettings setts, Vector3 initialPos, Vector2 bounds)
    {
        GameObject newEnemy = Pool2.GetGameObjectFromPool();
        if (newEnemy)
        {
            EnemyBasic enemy = newEnemy.GetComponent<EnemyBasic>();
            if (!enemy)
            {
                enemy = newEnemy.AddComponent<EnemyBasic>();
            }
            BulletSettings bSetts = new BulletSettings();
            bSetts.Damage = 10;
            bSetts.Speed = 5;
            bSetts.Type = BulletType.LINEAR;
            setts.Bullet = bullet;
            setts.BulletParameters = bSetts;
            setts.HitPoints = 10;
            setts.MovementSpeed = 10;
            setts.RateOfFire = 0.5f;

            enemy.InitializeCurve(initialPos, bounds);
            enemy.Initialize(setts);
        }
    }


}
