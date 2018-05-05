using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//public class ClientPlayer {
//    public string playerName;
//    public int connectionId;
//}

public class Client : MonoBehaviour {

    private const int MAX_CONNECTION = 100;

    private int port = 5701;

    private int hostId;
    private int webHostId;

    private int reliableChannel;
    //private int unReliableChannel;

    private bool isConnected = false;
    //private bool isStarted = false;
    //private float connectionTime;

    private byte error;

    private int connectionId;
    private int clientId; // Client ID on Server

    private string playerName;


    public GameObject waitScreen;
    public GameObject joinScreen;

    public Text playerNameText;


    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start() {
        waitScreen.SetActive(false);
        joinScreen.SetActive(true);
    }

    public void Connect() {
        // Fetch the IP address of the Server
        string ipAddress = GameObject.Find("IP InputField").GetComponent<InputField>().text;
        if (ipAddress == "") {
            Debug.Log("You must enter an IP Address!");
            return;
        }

        NetworkTransport.Init();
        ConnectionConfig config = new ConnectionConfig();

        reliableChannel = config.AddChannel(QosType.Reliable);
        //unReliableChannel = config.AddChannel(QosType.Unreliable);

        HostTopology topology = new HostTopology(config, MAX_CONNECTION);

        hostId = NetworkTransport.AddHost(topology, 0);

        connectionId = NetworkTransport.Connect(hostId, ipAddress, port, 0, out error);
        //Debug.Log("Client error code: " + error);
        //connectionTime = Time.time;
        isConnected = true;

        Debug.Log("Connected!");
    }

    public void PlaceTrap(float x, float y, int trapId) {
        string trapType = "bomb";
        Send("CLIENT_PLACE_TRAP|" + trapType + "|" + trapId + "|" + x + "%" + y, reliableChannel);
    }

    public void ActivateTrap(int trapId) {
        Send("CLIENT_ACTIVATE_TRAP|" + trapId, reliableChannel);
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
            case NetworkEventType.DataEvent:
                string message = Encoding.Unicode.GetString(recBuffer, 0, dataSize);
                Debug.Log("Receiving message: " + message);

                string[] commandParts = message.Split('|');

                switch (commandParts[0]) {
                    case "CLIENT_JOINED":
                        playerName = commandParts[2];
                        playerNameText.text = playerName;

                        waitScreen.SetActive(true);
                        joinScreen.SetActive(false);
                        break;

                    case "START_GAME":
                        SceneManager.LoadScene("PhoneGameplayScene");
                        break;

                    default:
                        Debug.Log("Invalid command : " + message);
                        break;
                }
                break;

        }
    }



    private void Send(string message, int channelId) {
        Debug.Log("Sending message: " + message);
        byte[] messageBuffer = Encoding.Unicode.GetBytes(message);
        NetworkTransport.Send(hostId, connectionId, channelId, messageBuffer, message.Length * sizeof(char), out error);
    }
}
