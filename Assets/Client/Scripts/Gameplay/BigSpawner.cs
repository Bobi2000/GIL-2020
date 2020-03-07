using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class BigSpawner 
{
    private static int count=0;
    public static int maxEnemys = 5;

     static BigSpawner()
    {

    }
    public  void KillEnemy()
    {
        count--;
        if (count<0)
        {
            count = 0;
        }
    }
    public  void SpawnEnemy()
    {
        count++;
    }
    public  bool CanISpawn()
    {
        if (count<=maxEnemys)
        {
            return true;
        }
        return false;
    }
           
}
