using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

public class ServerTransport : MonoBehaviour {

    public int maxConnections = 10;

    int reliableChannelID;

    int socketId;
    int socketPort = 8888;

    int connectionId;

    // Use this for initialization
    void Start() {
        NetworkTransport.Init();
        ConnectionConfig config = new ConnectionConfig();

        reliableChannelID = config.AddChannel(QosType.Reliable);

        HostTopology topology = new HostTopology(config, maxConnections);

        socketId = NetworkTransport.AddHost(topology, socketPort);
        Debug.Log("Socket Open. SocketId is: " + socketId);
    }

    // Update is called once per frame
    void Update() {
        int recHostId;
        int recConnectionId;
        int recChannelId;
        byte[] recBuffer = new byte[1024];
        int bufferSize = 1024;
        int dataSize;
        byte error;
        NetworkEventType recNetworkEvent = NetworkTransport.Receive(out recHostId, out recConnectionId, out recChannelId, recBuffer, bufferSize, out dataSize, out error);

        switch (recNetworkEvent) {
            case NetworkEventType.Nothing:
                break;
            case NetworkEventType.ConnectEvent:
                Debug.Log("incoming connection event received");
                break;
            case NetworkEventType.DataEvent:
                Stream stream = new MemoryStream(recBuffer);
                BinaryFormatter formatter = new BinaryFormatter();
                string message = formatter.Deserialize(stream) as string;
                Debug.Log("incoming message event received: " + message);
                break;
            case NetworkEventType.DisconnectEvent:
                Debug.Log("remote client event disconnected");
                break;
        }
    }

    public void Connect() {
        byte error;
        connectionId = NetworkTransport.Connect(socketId, "localhost", socketPort, 0, out error);
        Debug.Log("Connected to server. ConnectionId: " + connectionId);
    }

    public void SendSocketMessage() {
        byte error;
        byte[] buffer = new byte[1024];
        Stream stream = new MemoryStream(buffer);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, "HelloServer");

        int bufferSize = 1024;

        NetworkTransport.Send(socketId, connectionId, reliableChannelID, buffer, bufferSize, out error);
    }
}
