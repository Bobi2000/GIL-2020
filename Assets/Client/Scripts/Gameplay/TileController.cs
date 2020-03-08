using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class TileController : MonoBehaviour
{
    private Vector2 currentPosition;

    public GameObject Turret;
    public GameObject HorizontalWall;
    public GameObject VerticalWall;

    private GameObject building;
   

    public bool isBuiltOn = false;

    public bool isPlayers = false;

    public static TowerController currentlySelectedTower;

    //tower popup texts
    GameObject popUP;
    TextMeshProUGUI Health;
    TextMeshProUGUI Attack;
    TextMeshProUGUI Range;
    TextMeshProUGUI UpgradeCost;

    private void Start()
    {
        var gm = GameObject.Find("TowerCanvas").transform.GetChild(0);
        this.popUP = gm.gameObject;
        this.Health = gm.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        this.Attack = gm.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        this.Range = gm.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        this.UpgradeCost = gm.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
    }


    //end popup

    private string url = @"https://webaplicationgameserver20200307081805.azurewebsites.net";
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            this.popUP.SetActive(false);
        }

        if (this.building != null)
        {
            
             var currentHp = building.GetComponent<TowerController>().CurrentHealth;                    
            if (currentHp <= 0)
            {
              
                isBuiltOn = false;
            }
        }
      

        //if (Input.GetButtonDown("Fire1") && popUP.activeSelf &&
        //     RectTransformUtility.RectangleContainsScreenPoint(
        //         popUP.GetComponent<RectTransform>(),
        //         Input.mousePosition,
        //         Camera.main))
        //{
        //    popUP.SetActive(false);
        //}
    }
    private void OnMouseOver()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.7f);

        if (Input.GetButtonDown("Fire1") && isBuiltOn)
        {
            try
            {
                TowerController tempTower = building.GetComponent<TowerController>();
                currentlySelectedTower = tempTower;
                //Debug.Log(tempTower.CurrentHealth + "/" + tempTower.MaxHealth);
                popUP.SetActive(true);
                Health.text = "Health: " + tempTower.CurrentHealth + "/" + tempTower.MaxHealth;
                Attack.text = "Attack: " + tempTower.Damage;
                Range.text = "Range: " + tempTower.Range;
                UpgradeCost.text = "UpgradeCost: " + tempTower.NextUpgradeCost;
            }
            catch (System.Exception) { }

        }

        if (Input.GetKeyDown(KeyCode.Q) && isBuiltOn == false)
        {
            var vector3 = new Vector3(this.transform.position.x, this.transform.position.y, 1);
            building = Instantiate(Turret, vector3, Quaternion.identity);
            isBuiltOn = true;
            isPlayers = true;

            StartCoroutine(SendRequest($@"{url}/api/values/{vector3.x}/{vector3.y}/{vector3.z}/100/{ClientController.playerController.username}"));


        }
        else if (Input.GetKeyDown(KeyCode.W) && isBuiltOn == false)
        {
            var vector3 = new Vector3(this.transform.position.x, this.transform.position.y, 1);
            Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            var rotaion = Quaternion.Euler(0f, 0f, rot_z - 90);
            building = Instantiate(HorizontalWall, vector3, rotaion);
            isBuiltOn = true;

            StartCoroutine(SendBaricadeRequest($@"{url}/api/values/{vector3.x}/{vector3.y}/{vector3.z}/{rotaion.x}/{rotaion.y}/{rotaion.z}"));
        }
        
    }
    public void CreateTower()
    {
        var vector3 = new Vector3(this.transform.position.x, this.transform.position.y, 1);
        Instantiate(Turret, vector3, Quaternion.identity);
        isBuiltOn = true;
    }
    public void CreateBaricade(float x,float y,float z)
    {
        var vector3 = new Vector3(this.transform.position.x, this.transform.position.y, 1);
        var rotaion = Quaternion.Euler(x, y,z);
        building = Instantiate(HorizontalWall, vector3, rotaion);
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

    private IEnumerator SendBaricadeRequest(string url)
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

    public static void Upgrade(int x)
    {
        if (x == 1)
        {
            //Upgrade tower
            Debug.Log("Upgrade tower somehow!");
            currentlySelectedTower.Upgrade();
        }
        else if (x == 2)
        {
            currentlySelectedTower.Sell();
        }
        
    }
}
