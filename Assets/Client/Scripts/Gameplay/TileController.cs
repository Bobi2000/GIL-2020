using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    private Vector2 currentPosition;

    public GameObject Turret;

    private bool isBuiltOn=false;

    private GameObject currentBuilding;

    private void Update()
    {
        if (currentBuilding!=null)
        {
            if (currentBuilding.GetComponent<TowerController>().CurrentHealth<=0)
            {
                this.isBuiltOn = false;
            }
        }
    }
    private void OnMouseOver()
    {
        if (!isBuiltOn)
        {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.7f);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0.7f);
        }
        if (Input.GetKeyDown(KeyCode.Q)&&isBuiltOn==false)
        {
            var vector3 = new Vector3(this.transform.position.x, this.transform.position.y, 1);
            currentBuilding  = Instantiate(Turret, vector3, Quaternion.identity);
           
            this.isBuiltOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("build wall");
        }

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
