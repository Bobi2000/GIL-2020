using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    private Vector2 currentPosition;

    public GameObject Turret;
    void Start()
    {
    }

  
    void Update()
    {
       
    }
    private void OnMouseOver()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.7f);
        if (Input.GetKeyDown(KeyCode.Q))
        {         
            Instantiate(Turret, this.transform.position, Quaternion.identity);

        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("build wall");
        }

    }
    private void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(Turret, this.transform.position, Quaternion.identity);

        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("build wall");
        }
    }
    void OnMouseDown()
    {
        // this object was clicked - do something
        Debug.Log("dasdas");
    }
}
