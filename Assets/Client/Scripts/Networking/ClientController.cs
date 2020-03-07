using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ClientController : MonoBehaviour
{
    private TcpClient client;
    private StreamReader reader;
    private StreamWriter writer;

    public GameObject playerPrefab;

    public PlayerMovement playerMovement;

    public GameObject grid;
    public GameObject ghouls;

    public GameObject button;
    public GameObject LoginPanel;
    public TextMeshProUGUI usernameText;
    public string username;

    public IList<GameObject> players = new List<GameObject>();
    private DownloadHandler downloadHandler;

    private string url = @"https://webaplicationgameserver20200307081805.azurewebsites.net";

    public void WriteToServer(string message)
    {
        this.writer.WriteLine(message);
        this.writer.Flush();
    }

    public void Login()
    {
        this.button.SetActive(false);
        this.SetUp();
        //new Thread(this.SetUp).Start();
    }

    private void Start()
    {
        InvokeRepeating("GetMovements", 0.1f, 0.1f);
        //new Thread(this.ReadFromServer).Start();
    }



    private void Update()
    {
        //this.ReadFromServer();
    }

    private void ReadFromServer()
    {
        if (this.client != null)
        {
            if (this.client.Available != 0)
            {
                var message = this.reader.ReadLine();

                if (message == null || message == "")
                {
                    return;
                }

                var args = message
                    .Split(new[] { '@', ':' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

                if (args[0] == "movement")
                {
                    var x = float.Parse(args[1]);
                    var y = float.Parse(args[2]);
                }
                else if (args[0] == "login")
                {
                    var currentUsername = args[1];
                    var vector3 = new Vector3(0, 0, 0);

                    var currentPlayer = Instantiate(this.playerPrefab, vector3, Quaternion.identity);
                    currentPlayer.GetComponent<PlayerController>().username = this.username;

                    if (this.username == currentUsername)
                    {
                        currentPlayer.GetComponent<PlayerController>().isPlayer = true;
                    }
                }
            }
        }
    }

    private void GetMovements()
    {
        StartCoroutine(Movement($@"{url}/api/values/"));
    }

    private void SetUp()
    {
        this.username = this.usernameText.text;

        StartCoroutine(SendRequest($@"{url}/api/values/{this.username}/type"));
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Exiting...");

        StartCoroutine(LeaveServer($@"{url}/api/values/{this.username}/type/type"));

        //this.WriteToServer("@:logout");
    }

    private IEnumerator Movement(string url)
    {
        Debug.Log("In movement");
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError("Request Error: " + request.error);
            }
            else
            {
                downloadHandler = request.downloadHandler;
                var text = downloadHandler.text.Split(new[] { '"', '[', '\\', ']', '$' }).Where(a => a.Length > 3).ToList();

                foreach (var item in text)
                {
                    var args = item.Split(':');

                    var username = args[0];

                    if (username == "")
                    {
                        continue;
                    }

                    var args1Number = args[1].Replace(',', '.');
                    var args2Number = args[2].Replace(',', '.');

                    var vector2 = new Vector2(float.Parse(args1Number), float.Parse(args2Number));

                    var player = this.players.Where(p => p.GetComponent<PlayerController>().username == username && !p.GetComponent<PlayerController>().isPlayer).FirstOrDefault();

                    if (player != null)
                    {
                        player.GetComponent<PlayerMovement>().SetMovement(vector2);
                    }
                    /*else if (player == null && this.username != args[0] && this.players.Where(p => p.GetComponent<PlayerController>().username != args[0]).FirstOrDefault() == null)
                    {
                        var newVector2 = new Vector2(float.Parse(args1Number), float.Parse(args2Number));
                        var newPlayer = Instantiate(this.playerPrefab, vector2, Quaternion.identity);
                        newPlayer.GetComponent<PlayerController>().username = args[0];
                        this.players.Add(newPlayer);
                    }*/


                    bool containsItem = this.players.Any(i => i.GetComponent<PlayerController>().username == args[0]);



                    if (!containsItem)
                    {
                        var newVector2 = new Vector2(float.Parse(args1Number), float.Parse(args2Number));
                        var newPlayer = Instantiate(this.playerPrefab, vector2, Quaternion.identity);
                        newPlayer.GetComponent<PlayerController>().username = args[0];
                        newPlayer.GetComponent<PlayerController>().SetName();
                        this.players.Add(newPlayer);
                    }
                }
            }
        }
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
                downloadHandler = request.downloadHandler;
                var text = downloadHandler.text.Split(new[] { '"', '[', '\\', ']', '$' }).Where(a => a.Length > 3).ToList();

                foreach (var item in text)
                {
                    var args = item.Split(':');
                    var vector2 = new Vector2(float.Parse(args[1]), float.Parse(args[2]));

                    if (args[0] == username)
                    {
                        var player = Instantiate(this.playerPrefab, vector2, Quaternion.identity);
                        player.GetComponent<PlayerController>().username = args[0];
                        player.GetComponent<PlayerController>().isPlayer = true;
                        player.GetComponent<PlayerController>().SetName();
                        this.players.Add(player);
                    }

                    this.LoginPanel.SetActive(false);
                    this.grid.SetActive(false);
                    this.ghouls.SetActive(false);
                }
            }
        }
    }

    private IEnumerator LeaveServer(string url)
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
}