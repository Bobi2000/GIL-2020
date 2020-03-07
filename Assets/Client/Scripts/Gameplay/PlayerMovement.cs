using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    private Vector3 NextPosition;

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            this.NextPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        this.Move();
    }

    private void Move()
    {
        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
        player.transform.position = Vector2.MoveTowards(playerPosition, this.NextPosition, Time.deltaTime * 10);
    }
}
