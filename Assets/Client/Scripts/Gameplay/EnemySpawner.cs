using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemys;

    public float spawnTimer = 10f;
    public int spawnRatio = 100;

    private float timeBtwShots;

    private int SpawnLine;
    void Start()
    {
        if ((int)this.transform.position.y > (int)this.transform.position.x)
        {
            this.SpawnLine = (int)this.transform.position.y;
        }
        else
        {
            this.SpawnLine = (int)this.transform.position.x;
        }
    }


    void Update()
    {
        if (timeBtwShots <= 0)
        {

            SpawnEnemys();
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
        var spawnPoint = new Vector2(this.transform.position.x, lineSpawn);

        Instantiate(Enemys, spawnPoint, Quaternion.identity);



    }
}
