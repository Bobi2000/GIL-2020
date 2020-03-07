using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator 
{
    public int upper = 100;
    public int upperUpdate = 10;
    public int lower = 1;
    public int[] RandomTurretStatsOnCreate(int badLuck)
    {
        upper -=badLuck/10;
        var arr = new int[3];
        var rnd = new System.Random();
        arr[0]=rnd.Next(lower,upper);
        arr[1]=rnd.Next(lower,upper);
        arr[2]=rnd.Next(lower,upper);
        return arr;

    }
    public int[] RandomTurretStatsOnUpdate(int badLuck)
    {
        upperUpdate -= badLuck / 10;
        var arr = new int[3];
        var rnd = new System.Random();
        arr[0] = rnd.Next(lower, upperUpdate);
        arr[1] = rnd.Next(lower, upperUpdate);
        arr[2] = rnd.Next(lower, upperUpdate);
        return arr;

    }


}
