using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLogic : MonoBehaviour {

    // Trap specific logic
    public int userId;
    public int trapId;
    public string type;

    // Detonation timer for the trap, after this the trap animation will play and it will destory itself.
    [SerializeField]
    private float detonationTimer;

    [SerializeField]
    private float animationTimer;

    [SerializeField]
    private bool activated;
    [SerializeField]
    private bool harassing;

    private GameObject player;

    public void Initialize(int userId, int trapId, string type, float detonationTimer) {
        this.userId = userId;
        this.trapId = trapId;
        this.type = type;
        this.detonationTimer = detonationTimer;

        this.activated = false;
        this.harassing = false;

        // TODO: This should be replaced by the animation duration!
        animationTimer = 3.0f;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        if (activated) {
            UpdateDetonationTimer();
        }
    }

    private void UpdateDetonationTimer() {
        detonationTimer -= Time.deltaTime;
        if (detonationTimer <= 0.0f) {
            if (!harassing) {
                StartHarassingPlayer();
            }
            UpdateAnimationTimer();
        }
    }

    private void UpdateAnimationTimer() {
        animationTimer -= Time.deltaTime;
        if (animationTimer <= 0.0f) {
            StopHarassingPlayer();
            Destroy(gameObject);
        }
    }

    private void StartHarassingPlayer() {
        Debug.Log("Harassing player!");
        harassing = true;
    }

    private void StopHarassingPlayer() {
        Debug.Log("Stop harassing player!");
    }

    public void Activate() {
        activated = true;
    }
}
