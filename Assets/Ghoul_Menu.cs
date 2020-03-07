using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul_Menu : MonoBehaviour
{
    Vector2 currentPos;
    float speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < 20)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else
        {
            transform.position = currentPos;
        }
       
        //transform.position = currentPos;
        

    }
}
