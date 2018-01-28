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

    public float maxTimer = 2f;
    private float maxTimerIncrement = 0;

    public float intervalTimer = 0.2f;
    private float timer = 0;


    enum State {
        ACTIVATED, EXPLODING, UNPLACED, PLACED, USED
    }

    private State currentState = State.UNPLACED;

    // Use this for initialization
    void Start() {
        bombActivated = bombActivated1;
    }

    int binarySelector = 0;

    // Update is called once per frame
    void Update() {
        if (currentState == State.ACTIVATED) {
            timer += Time.deltaTime;
            maxTimerIncrement += Time.deltaTime;
            if (timer >= intervalTimer) {
                timer = 0;
                binarySelector++;
                if (binarySelector % 2 == 0) {
                    bombActivated = bombActivated2;
                } else {
                    bombActivated = bombActivated1;
                }
                GetComponent<Image>().sprite = bombActivated;
            }
            if (maxTimerIncrement >= maxTimer) {
                SetUsed();
            }
        }
    }


    public void SetUsed() {
        currentState = State.USED;
        GetComponent<Image>().sprite = bombUsed;
    }

    public void Activate() {
        currentState = State.ACTIVATED;
    }

    public void Place() {
        currentState = State.PLACED;
        GetComponent<Image>().sprite = bombPlaced;
    }

    public bool IsAvailable() {
        return currentState == State.UNPLACED;
    }

    public bool IsActivated() {
        return currentState == State.ACTIVATED;
    }

    public bool IsPlaced() {
        return currentState == State.PLACED;
    }

    public bool IsExploding() {
        return currentState == State.EXPLODING;
    }

    public bool IsUsed() {
        return currentState == State.USED;
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
