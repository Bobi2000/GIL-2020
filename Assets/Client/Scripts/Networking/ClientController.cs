using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.Networking;
using System.Collections;

public class ClientController : MonoBehaviour
{
    private TcpClient client;
    private StreamReader reader;
    private StreamWriter writer;

    public GameObject playerPrefab;

    public PlayerMovement playerMovement;

    public GameObject button;
    public TextMeshProUGUI usernameText;
    public string username;

    public IList<GameObject> players = new List<GameObject>();
    private DownloadHandler downloadHandler;

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
        //new Thread(this.ReadFromServer).Start();
    }

    private void Update()
    {
        this.ReadFromServer();
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

    private void SetUp()
    {
        StartCoroutine(SendRequest(@"https://webaplicationgameserver20200307081805.azurewebsites.net/api/values"));
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
                Debug.Log(downloadHandler.text);
            }
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Exiting...");
        this.WriteToServer("@:logout");
    }
}

/*
        Debug.Log(this.usernameText.text);
        try
        {
            this.username = this.usernameText.text;

            this.client = new TcpClient("127.0.0.1", 1300);
            Debug.Log("Connected!");

            this.reader = new StreamReader(this.client.GetStream());
            this.writer = new StreamWriter(this.client.GetStream());

            this.WriteToServer($"@:login:{this.username}");
        }
        catch (Exception)
        {
            Debug.Log("Can't connect to Master Server!");
            Debug.Log("Retrying!");
            //Thread.Sleep(400);
            this.SetUp();
        }
*/
