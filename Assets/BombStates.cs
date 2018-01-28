using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BombStates : MonoBehaviour {

    public Sprite bombActivated1;
    public Sprite bombActivated2;
    public Sprite bombExploding;
    public Sprite bombUnplaced;
    public Sprite bombPlaced;
    public Sprite bombUsed;


    private Sprite bombActivated;


    enum State {
        ACTIVATED, EXPLODING, UNPLACED, PLACED, USED
    }

    private State currentState = State.UNPLACED;

    // Use this for initialization
    void Start () {
        bombActivated = bombActivated1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Activate() {
        currentState = State.ACTIVATED;
    }

    public void Place() {
        currentState = State.PLACED;
    }

    public bool IsAvailable() {
        return currentState == State.UNPLACED;
    }

    public Sprite GetStateImage() {
        switch (currentState) {
            case State.ACTIVATED:
                return bombActivated;
            case State.EXPLODING:
                return bombExploding;
            case State.UNPLACED:
                return bombUnplaced;
            case State.PLACED:
                return bombPlaced;
            case State.USED:
                return bombUsed;
            default:
                return bombUsed;
        }
    }
}
