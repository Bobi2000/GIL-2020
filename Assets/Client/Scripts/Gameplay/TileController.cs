using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TileController : MonoBehaviour
{
    private Vector2 currentPosition;

    public GameObject Turret;

    private GameObject building;

    public bool isBuiltOn = false;

    private string url = @"https://webaplicationgameserver20200307081805.azurewebsites.net";
    private void Update()
    {
        if (this.building!=null)
        {
           var currentHp= building.GetComponent<TowerController>().CurrentHealth;
            if (currentHp<=0)
            {
                Destroy(gameObject);
                isBuiltOn = false;
            }
        }
    }
    private void OnMouseOver()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.7f);
        if (Input.GetKeyDown(KeyCode.Q)&&isBuiltOn==false)
        {
            var vector3 = new Vector3(this.transform.position.x, this.transform.position.y, 1);
            building=Instantiate(Turret, vector3, Quaternion.identity);
            isBuiltOn = true;

            StartCoroutine(SendRequest($@"{url}/api/values/{vector3.x}/{vector3.y}/{vector3.z}/100/{ClientController.playerController.username}"));

        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("build wall");
        }
    }
    public void CreateTower()
    {
        var vector3 = new Vector3(this.transform.position.x, this.transform.position.y, 1);
        Instantiate(Turret, vector3, Quaternion.identity);
        isBuiltOn = true;
    }
    private IEnumerator SendRequest(string url)
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

    }
}
