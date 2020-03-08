using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    private Vector2 NextPosition;
    private Vector2 tempPos;

    [SerializeField] Animator anim;

    private PlayerController playerController;

    private string url = @"https://webaplicationgameserver20200307081805.azurewebsites.net";

    Rigidbody2D rb;

    private void Start()
    {
        this.playerController = this.gameObject.GetComponent<PlayerController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        /*NextPosition.x = this.transform.position.x;
        NextPosition.y = this.transform.position.y;
        if (tempPos.x!=NextPosition.x&&tempPos.y!=NextPosition.y)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        tempPos.x = NextPosition.x;
        tempPos.y = NextPosition.y;*/
        /*if (this.gameObject.transform.position == new Vector3(0, 0, 0))
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
        }*/

        if (this.playerController.isPlayer)
        {
            if (Input.GetMouseButtonDown(1))
            {
                this.NextPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var Vector2Position = new Vector2(NextPosition.x, NextPosition.y);
                Vector3 lookpos = Camera.main.ScreenToViewportPoint(Vector2Position);
                float angle = Mathf.Atan2(lookpos.y, lookpos.x) * Mathf.Rad2Deg;
                this.playerController.GetComponent<SpriteRenderer>().transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                var urll = $@"{url}/api/values/{this.playerController.username}/{this.NextPosition.x:F2}/{this.NextPosition.y:F2}/type";
                StartCoroutine(MoveToServer(urll));
                
            }
        }

        this.Move();
    }

    private IEnumerator MoveToServer(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError("Request Error: " + request.error);
            }
            else
            {
            }
        }
    }

    private void Move()
    {
        //if (this.playerController.isPlayer)
        {
            Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
            player.transform.position = Vector2.MoveTowards(playerPosition, this.NextPosition, Time.deltaTime * 10);
        }
    }

    public void SetMovement(Vector2 vector)
    {
        this.NextPosition = vector;
    }
}
