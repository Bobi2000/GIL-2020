using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownSpawner : MonoBehaviour
{
    public GameObject Enemys;

    private BigSpawner spawner=new BigSpawner();

    public float spawnTimer = 10f;
    public int spawnRatio = 100;

    private float timeBtwShots;
    private bool isSite = false;

    private int SpawnLine;
    void Start()
    {
       
        SpawnLine =(int)this.transform.localScale.x / 2;
    }


    void Update()
    {
        if (timeBtwShots <= 0)
        {
            if (spawner.CanISpawn())
            {
            SpawnEnemys();
            }
            timeBtwShots = spawnTimer;

        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

    }

    private void SpawnEnemys()
    {
        var rnd = new System.Random();
        var lineSpawn = rnd.Next(0, SpawnLine);
        var spawnPoint = new Vector2();
        Debug.Log(lineSpawn);
        spawnPoint = new Vector2(Random.Range(-50, 50), this.transform.position.y) ;
        spawner.SpawnEnemy();


        Instantiate(Enemys, spawnPoint, Quaternion.identity);
    }
}
