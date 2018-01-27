using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Trap
{
    public int id;
    public float x;
    public float z;
    public string type;
    public float timer;

    public Trap(int id, float x, float z, string type, float timer)
    {
        this.id = id;
        this.x = x;
        this.z = z;
        this.type = type;
        this.timer = timer;
    }
}

public class Traps : MonoBehaviour {

    private Server server;

    private float startDelay;
    private List<Trap> placedTraps;


    public bool enableTrapPlacement = false;

    // Use this for initialization
    void Start () {
        startDelay = 5f;
        placedTraps = new List<Trap>();
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
            if(t.timer<0) {
                // KABOOM!!!!
            }
        }

	}

    void placeTrap(int id, float x, float z, string type)
    {
        float timer = 2f; // should be taken from traptype!
        placedTraps.Add(new Trap(id, x, z, type, timer));
    }
}
