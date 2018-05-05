using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLogic : MonoBehaviour {

    public GameObject explosionPrefab;

    // Trap specific logic
    public int userId;
    public int trapId;
    public string type;

    private float blastRadius;

    // Detonation timer for the trap, after this the trap animation will play and it will destory itself.
    [SerializeField]
    private float detonationTimer;

    [SerializeField]
    private float animationTimer;

    [SerializeField]
    private bool activated;
    [SerializeField]
    private bool exploding;

    private MovePlayer player;

    public void Initialize(int userId, int trapId, string type, float detonationTimer, float blastRadius) {
        this.userId = userId;
        this.trapId = trapId;
        this.type = type;
        this.detonationTimer = detonationTimer;
        this.blastRadius = blastRadius;

        this.activated = false;
        this.exploding = false;
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MovePlayer>();
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
            if (!exploding) {
                ExplosionStarted();
            }
            UpdateAnimationTimer();
        }
    }

    private void UpdateAnimationTimer() {
        animationTimer -= Time.deltaTime;
        if (animationTimer <= 0.0f) {
            ExplosionFinished();
        }
    }

    private void ExplosionStarted() {
        Debug.Log("Harassing player!");
        FindObjectOfType<AudioManager>().PlayFromLocation("BombExplosion", transform.position);
        exploding = true;

        if (explosionPrefab != null) {
            GameObject explosionObject = GameObject.Instantiate<GameObject>(explosionPrefab);
            explosionObject.transform.SetParent(transform, false);

            animationTimer = explosionObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        }

        // TOOD: Check if player is in range and block the movement
        if (Vector3.Distance(player.transform.position, transform.position) <= blastRadius) {
            player.SetMovementEnabled(false);
        }
    }

    private void ExplosionFinished() {
        Debug.Log("Stop harassing player!");
        Destroy(gameObject);
        // TODO: Check if player is in range and unblock the movement
        if (Vector3.Distance(player.transform.position, transform.position) <= blastRadius) {
            player.SetMovementEnabled(true);
        }
    }

    public void Activate() {
        FindObjectOfType<AudioManager>().PlayFromLocation("BombBeeps", transform.position);
        activated = true;
    }
    
}
