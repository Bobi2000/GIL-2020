using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    private Vector2 currentPosition;
    void Start()
    {
    }

  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("build turret");
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("build wall");
        }
    }
    private void OnMouseOver()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.7f);
        this.currentPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
    }
    private void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }
    void OnMouseDown()
    {
        // this object was clicked - do something
        Debug.Log("dasdas");
    }
}
