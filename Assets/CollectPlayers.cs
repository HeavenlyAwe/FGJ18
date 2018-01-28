using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollectPlayers : MonoBehaviour {

    private Text playersText;
    private Text mobilePlayersHeader;
    private Text ipAddress;
    private Server server;
    private List<ServerClient> clients;

    public List<string> playerNames;

    void Awake()
    {
        playerNames.Add("Dirty Harry");
        playerNames.Add("Slutty Sally");
        playerNames.Add("Harley Quinn");
        playerNames.Add("Billy the Kid");
        playerNames.Add("Tricky Tracy");
        playerNames.Add("Al Capone");
        playerNames.Add("Twoface");
        playerNames.Add("The three-legged monster");
    }

    // Use this for initialization
    void Start () {
        playersText = GameObject.FindGameObjectWithTag("PlayersConnected").GetComponent<Text>();
        mobilePlayersHeader = GameObject.FindGameObjectWithTag("MobilePlayersHeader").GetComponent<Text>();
        server = GameObject.FindGameObjectWithTag("Server").GetComponent<Server>();
        ipAddress = GameObject.FindGameObjectWithTag("IP").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {

        clients = server.getClients();

        string txt = "";
        foreach(ServerClient client in clients)
        {
            txt += client.playerName + "\n";
        }
        playersText.text = txt;
        mobilePlayersHeader.text = "Mobile players connected:\n(" + server.getClientCount() + "/8)";

        ipAddress.text = Network.player.ipAddress + ":" + server.getPort();

    }

    public void startGame()
    {
        SceneManager.LoadScene("MainScene");
        server.StartGame();
    }

    public string getRandomName()
    {
        int idx = Random.Range(0, playerNames.Count - 1);
        string name = playerNames[idx];
        playerNames.RemoveAt(idx);
        return name;
    }
}
