using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    private int rows = 30;
    private int cols = 30;
    private float tileSize = 4;

    public GameObject grassTile;
    void Start()
    {
        Generategrid();
    }

    private void Generategrid()
    {
        var x = -60;
        var y = 50;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                var vector2 = new Vector2(x, y);
                GameObject tile = (GameObject)Instantiate(grassTile, vector2, Quaternion.identity);

                tile.AddComponent<BoxCollider2D>();
                
                float posX = col * tileSize;
                float posY = row * -tileSize;
                //tile.transform.position = new Vector2(posX, posY);
                x += 4;
            }
            x = -60;
            y -= 4;
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
