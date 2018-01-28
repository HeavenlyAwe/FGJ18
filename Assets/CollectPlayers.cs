using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollectPlayers : MonoBehaviour {

    private Text ipAddress;
    private Server server;
    private List<ServerClient> clients;

    public Sprite usedSlotSPR;
    public Sprite unUsedSlotSPR;

    public GameObject[] slots;

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
        server = GameObject.FindGameObjectWithTag("Server").GetComponent<Server>();
        ipAddress = GameObject.FindGameObjectWithTag("IP").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {

        clients = server.getClients();

        
        for(int idx=0; idx<8; idx++)
        {
            slots[idx].GetComponentInChildren<Text>().text = "";
            slots[idx].GetComponent<Image>().sprite = unUsedSlotSPR;
        }

        int i = 0;
        foreach (ServerClient client in clients)
        {
            slots[i].GetComponentInChildren<Text>().text = client.playerName;
            slots[i].GetComponent<Image>().sprite = usedSlotSPR;
            i++;
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
