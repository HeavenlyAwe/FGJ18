using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ServerClient {

    public int connectionId;
    public string playerName;
}

public class Server : MonoBehaviour {

    private const int MAX_CONNECTION = 8;

    private int port = 5701;

    private int hostId;
    private int webHostId;

    private int reliableChannel;
    private int unReliableChannel;

    private bool isStarted = false;

    private byte error;

    private CollectPlayers collectPlayers;

    public List<ServerClient> clientList;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Use this for initialization
    void Start() {
        collectPlayers = GameObject.FindGameObjectWithTag("CollectPlayers").GetComponent<CollectPlayers>();

        NetworkTransport.Init();
        ConnectionConfig config = new ConnectionConfig();

        reliableChannel = config.AddChannel(QosType.Reliable);
        unReliableChannel = config.AddChannel(QosType.Unreliable);

        HostTopology topology = new HostTopology(config, MAX_CONNECTION);

        hostId = NetworkTransport.AddHost(topology, port, null);
        webHostId = NetworkTransport.AddWebsocketHost(topology, port, null);

        isStarted = true;

        Debug.Log("Server Started: " + hostId + " : " + webHostId);

        clientList = new List<ServerClient>();

        // insert dummy
        ServerClient serverClient = new ServerClient();
        serverClient.connectionId = 1234;
        serverClient.playerName = collectPlayers.getRandomName(); // "TEMP";
        clientList.Add(serverClient);

    }

    // Update is called once per frame
    void Update() {
        if (!isStarted) {
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
            case NetworkEventType.ConnectEvent:    //2
                Debug.Log("Player: " + connectionId + " has connected");
                OnConnection(connectionId);
                break;

            case NetworkEventType.DataEvent:       //3
                string message = Encoding.Unicode.GetString(recBuffer, 0, dataSize);
                Debug.Log("Player: " + connectionId + " has sent " + message);

                string[] commandParts = message.Split('|');

                switch (commandParts[0]) {
                    case "CLIENT_NAME":
                        OnClientName(connectionId, commandParts[1]);
                        break;
                    case "CLIENT_PLACE_TRAP":
                        OnClientPlaceTrap(connectionId, commandParts[1], commandParts[2].Split('%'));
                        break;
                    default:
                        Debug.Log("Invalid command : " + message);
                        break;
                }
                break;

            case NetworkEventType.DisconnectEvent: //4
                Debug.Log("Player: " + connectionId + " has disconnected");
                OnDisconnection(connectionId);
                break;
        }
    }

    private void OnConnection(int connectionId) {
        // Add the player to a list
        ServerClient serverClient = new ServerClient();
        serverClient.connectionId = connectionId;
        serverClient.playerName = collectPlayers.getRandomName(); // "TEMP";

        clientList.Add(serverClient);

        // Inform the player of his ID
        // Request his name and send the name of all other players
        string message = "ASK_CLIENT_NAME|" + connectionId + "|";
        foreach (ServerClient client in clientList) {
            message += client.playerName + "%" + client.connectionId + "|";
        }
        message = message.Trim('|');

        SendMessage(message, reliableChannel, connectionId);
    }

    private void OnDisconnection(int connectionId) {
        // Remove the client from the client list
        clientList.Remove(clientList.Find(x => x.connectionId == connectionId));

        // Send the disconnect message to all players
        SendMessage("CLIENT_DISCONNECTED|" + connectionId, reliableChannel, clientList);
    }


    private void OnClientName(int connectionId, string playerName) {
        // Link playerName to connectionId

        clientList.Find(x => x.connectionId == connectionId).playerName = playerName;

        // Tell everybody that a new player has connected
        SendMessage("CLIENT_CONNECTED|" + playerName + "|" + connectionId, reliableChannel, clientList);
    }


    private void OnClientPlaceTrap(int connectionId, string trapType, string[] data) {
        Debug.Log("Player: " + connectionId + " is placing [" + trapType + "]");
        float x = float.Parse(data[0]);
        float z = float.Parse(data[1]);
    }


    private void SendMessage(string message, int channelId, int connectionId) {
        List<ServerClient> clients = new List<ServerClient>();
        clients.Add(clientList.Find(x => x.connectionId == connectionId));
        SendMessage(message, channelId, clients);
    }

    private void SendMessage(string message, int channelId, List<ServerClient> clients) {
        Debug.Log("Sending message: " + message);
        byte[] messageBuffer = Encoding.Unicode.GetBytes(message);
        foreach (ServerClient client in clients) {
            NetworkTransport.Send(hostId, client.connectionId, channelId, messageBuffer, message.Length * sizeof(char), out error);
        }
    }

    public List<ServerClient> getClients()
    {
        return clientList;
    }

    public int getClientCount()
    {
        return clientList.Count;
    }

}
