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

    public Sprite usedSlotSPR;
    public Sprite unUsedSlotSPR;

    public GameObject[] slots;

    public List<string> playerNames;

    void Awake()
    {
        playerNames.Add("Vader");
        playerNames.Add("Pinkie");
        playerNames.Add("Brain");
        playerNames.Add("Twoface");
        playerNames.Add("Ursula");
        playerNames.Add("Scar");
        playerNames.Add("Khan");
        playerNames.Add("Rattigan");
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

        int i = 0;
        
        for(int i=0; i<8; i++)
        {
            slots[i].GetComponentInChildren<Text>().text = "";
            slots[i].GetComponent<Image>().sprite = unUsedSlotSPR;
        }

        foreach (ServerClient client in clients)
        {
            slots[i].GetComponentInChildren<Text>().text = client.playerName;
            slots[i].GetComponent<Image>().sprite = usedSlotSPR;
        }



        ipAddress.text = Network.player.ipAddress;

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
