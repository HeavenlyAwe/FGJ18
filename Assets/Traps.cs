using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap
{
    public int userId;
    public int trapId;
    public float x;
    public float z;
    public string type;
    public float timer;
    public bool activated;

    public Trap(int userId, int trapId, float x, float z, string type, float timer)
    {
        this.userId = userId;
        this.trapId = trapId;
        this.x = x;
        this.z = z;
        this.type = type;
        this.timer = timer;
        this.activated = false;
    }
}

public class Traps : MonoBehaviour {

    public GameObject trapGO;

    private Server server;

    private float startDelay;
    private List<Trap> placedTraps;
    private List<int> removeTraps;
    private GameObject player;

    public bool enableTrapPlacement = false;

    // Use this for initialization
    void Start () {
        startDelay = 5f;
        placedTraps = new List<Trap>();
        removeTraps = new List<int>();
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update () {

        // countdown to disable placing of traps in a too early stage of the game!
        if(!enableTrapPlacement)
        {
            startDelay -= Time.deltaTime;
            if(startDelay<0)
            {
                enableTrapPlacement = true;
            }
        }
        for(int idx = 0; idx<placedTraps.Count; idx++) 
        {
            Trap t = placedTraps[idx];
            t.timer -= Time.deltaTime;
            if(t.activated && t.timer<0) {
                // KABOOM!!!!
                detonateTrap(t, idx);
            }
        }
        // remove detonated traps
        //Debug.Log("count: " + removeTraps.Count);
        for (int idx = 0; idx < removeTraps.Count; idx++)
        {
            placedTraps.RemoveAt(removeTraps[idx]);
        }
        removeTraps.RemoveRange(0, removeTraps.Count);

    }

    void placeTrap(int userId, int trapId, float x, float z, string type)
    {
        float timer = 2f; // should be taken from traptype!
        placedTraps.Add(new Trap(userId, trapId, x, z, type, timer));
    }

    public void detonateTrap(Trap t, int idx)
    {
        Vector3 pos = player.transform.position;
        float dist = (pos - new Vector3(t.x, 0, t.z)).sqrMagnitude;
        if (dist < 25f)
        {
            // Hit by trap
            player.GetComponent<MovePlayer>().hitByTrap(t, dist);
        }
        // add detonated trap to removelist
        removeTraps.Add(idx);

    }

    public void activateTrap(int userId, int trapId)
    {
        for(int idx = 0; idx<placedTraps.Count; idx++)
        {
            Trap t = placedTraps[idx];
            if(t.userId==userId && t.trapId==trapId)
            {
                t.activated = true;
            }
        }
    }

    public void getPlayerGO()
    {
        Debug.Log("GO");
        player = GameObject.FindGameObjectWithTag("Player");

        // make some dummytraps:
        placedTraps.Add(new global::Trap(111, 0, 3, 3, "test", 1f));
        activateTrap(111, 0);
        placedTraps.Add(new global::Trap(111, 1, 5, 5, "test", 2f));
        //activateTrap(111, 1);
        placedTraps.Add(new global::Trap(111, 2, 2, 2, "test", 3f));
        //activateTrap(111, 2);
        Debug.Log("Traps Placed");
        foreach(Trap t in placedTraps)
        {
            Instantiate(trapGO, new Vector3(t.x, 2, t.z), Quaternion.identity);
        }
    }
}
