using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ClientPlayer {
    public string playerName;
    public int connectionId;
}

public class Client : MonoBehaviour {

    private const int MAX_CONNECTION = 100;

    private int port = 5701;

    private int hostId;
    private int webHostId;

    private int reliableChannel;
    private int unReliableChannel;

    private bool isConnected = false;
    private bool isStarted = false;
    private float connectionTime;

    private byte error;

    private int connectionId;
    private int clientId; // Client ID on Server

    private string playerName;

    private Dictionary<int, ClientPlayer> playerDictionary;

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void Connect() {
        // Fetch the IP address of the Server
        string ipAddress = GameObject.Find("IP InputField").GetComponent<InputField>().text;
        if (ipAddress == "") {
            Debug.Log("You must enter an IP Address!");
            return;
        }

        // Does the player have a name?
        string name = GameObject.Find("NameInputField").GetComponent<InputField>().text;
        if (name == "") {
            Debug.Log("You must enter a name!");
            return;
        }
        playerName = name;

        NetworkTransport.Init();
        ConnectionConfig config = new ConnectionConfig();

        reliableChannel = config.AddChannel(QosType.Reliable);
        unReliableChannel = config.AddChannel(QosType.Unreliable);

        HostTopology topology = new HostTopology(config, MAX_CONNECTION);

        hostId = NetworkTransport.AddHost(topology, 0);

        connectionId = NetworkTransport.Connect(hostId, ipAddress, port, 0, out error);
        connectionTime = Time.time;
        isConnected = true;

        Debug.Log("Connected!");

        playerDictionary = new Dictionary<int, ClientPlayer>();
    }

    public void PlaceTrap(int x, int y) {
        string trapType = "bomb";
        Send("CLIENT_PLACE_TRAP|" + trapType + "|" + x + "%" + y, reliableChannel);
    }


    // Update is called once per frame
    void Update() {
        if (!isConnected) {
            return;
        }

        int recHostId;
        int connectionId;
        int channelId;
        byte[] recBuffer = new byte[1024];
        int bufferSize = 1024;
        int dataSize;
        byte error;
        NetworkEventType recData = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, recBuffer, bufferSize, out dataSize, out error);
        switch (recData) {
            //case NetworkEventType.Nothing:         //1
            //    break;
            //case NetworkEventType.ConnectEvent:    //2
            //    break;
            case NetworkEventType.DataEvent:       //3
                string message = Encoding.Unicode.GetString(recBuffer, 0, dataSize);
                Debug.Log("Receiving message: " + message);

                string[] commandParts = message.Split('|');

                switch (commandParts[0]) {
                    case "ASK_CLIENT_NAME":
                        OnAskClientName(commandParts);
                        break;
                    case "CLIENT_CONNECTED":
                        OnClientPlayerConnected(commandParts[1], int.Parse(commandParts[2]));
                        break;
                    case "CLIENT_DISCONNECTED":
                        OnClientPlayerDisconnected(int.Parse(commandParts[1]));
                        break;
                    case "START_GAME":
                        OnStartGame();
                        break;
                    default:
                        Debug.Log("Invalid command : " + message);
                        break;
                }
                break;
                //case NetworkEventType.DisconnectEvent: //4
                //    break;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
            Debug.Log(Input.GetTouch(0).position);
        }
    }


    private void OnAskClientName(string[] data) {
        // First index of data has already been accessed

        // Set this client's ID
        clientId = int.Parse(data[1]);

        // Send the name to the Server
        Send("CLIENT_NAME|" + playerName, reliableChannel);

        // Create all other players
        for (int i = 2; i < data.Length - 1; i++) {
            string[] d = data[i].Split('%');
            OnClientPlayerConnected(d[0], int.Parse(d[1]));
        }
    }

    private void OnClientPlayerConnected(string playerName, int connectionId) {

        // This function should probably be updated. The other player stuff might be redundant
        if (this.connectionId == connectionId) {
            GameObject.Find("Canvas").SetActive(false);
            isStarted = true;
        }

        ClientPlayer player = new ClientPlayer();
        player.playerName = playerName;
        player.connectionId = connectionId;

        playerDictionary.Add(connectionId, player);
    }

    private void OnClientPlayerDisconnected(int connectionId) {
        playerDictionary.Remove(connectionId);
    }

    private void OnStartGame() {
        SceneManager.LoadScene("PhoneGameplayScene");
    }


    private void Send(string message, int channelId) {
        Debug.Log("Sending message: " + message);
        byte[] messageBuffer = Encoding.Unicode.GetBytes(message);
        NetworkTransport.Send(hostId, connectionId, channelId, messageBuffer, message.Length * sizeof(char), out error);
    }
}
