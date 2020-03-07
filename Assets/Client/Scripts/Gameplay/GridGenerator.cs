using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    private int rows = 5;
    private int cols = 8;
    private float tileSize = 4;

    public GameObject grassTile;
    void Start()
    {
        Generategrid();
    }

    private void Generategrid()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject tile = (GameObject)Instantiate(grassTile, transform);
                float posX = col * tileSize;
                float posY = row * -tileSize;

                tile.transform.position = new Vector2(posX, posY);
            }
        }
        

    }

    // Update is called once per frame
    void Update()
    {

    }
}
