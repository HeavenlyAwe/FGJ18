using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    public float speed;
    public float rotSpeed;

    public float angle = 0;

    private Vector3 dPos;
    //private float hitCountdown = 0;
    //private LevelController lvlController;
    //private int level;
    //private Sprite levelBitmap;
    //private int bitmapWidth;
    //private int bitmapHeight;
    //private Color32[] bitmapColors;
    //private Color32 cFloor = new Color32(255, 255, 255, 255);


    private bool movementEnabled = true;
    //private float trapHitCountdown;

    //private float hitPadding = 2.5f;

    // Use this for initialization
    //void Start() {
        //lvlController = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelController>();
        //level = lvlController.level;
        //levelBitmap = lvlController.levelBitmaps[level];
        //bitmapWidth = levelBitmap.texture.width;
        //bitmapHeight = levelBitmap.texture.height;
        //bitmapColors = levelBitmap.texture.GetPixels32();

        // Now the player object is available, get a reference to the gameobject in Traps-script
        //GameObject.FindGameObjectWithTag("Traps").GetComponent<Traps>().getPlayerGO();
    //}

    // Update is called once per frame
    void Update() {

        if (movementEnabled) {

            float horiz = Input.GetAxis("Horizontal");
            float vert = Input.GetAxis("Vertical");
            if (System.Math.Abs(vert) > 0) {
                FindObjectOfType<AudioManager>().PlayWithoutDuplicate("FootSteps");
            }

            angle = horiz * rotSpeed * Time.deltaTime;

            transform.Rotate(new Vector3(0f, 1f, 0f), angle);

            dPos = transform.forward * speed * Time.deltaTime * vert;
            Vector3 pos = transform.position;

            Vector3 rot = transform.rotation.eulerAngles;

            float dx = dPos.x;
            float dz = dPos.z;

            transform.position = pos + new Vector3(dx, 0, dz);

        } else {
            // movement not enabled.
            //trapHitCountdown -= Time.deltaTime;
            //if (trapHitCountdown < 0) {
            //    //// Reset the camera and disable the explosion
            //    //movementEnabled = true;
            //    //GameObject.FindGameObjectWithTag("Explosion").GetComponent<SpriteRenderer>().enabled = false;
            //    //GameObject.FindGameObjectWithTag("Explosion").GetComponent<Animator>().StopPlayback();
            //    Camera.main.transform.localRotation = Quaternion.Euler(10, 0, 0);
            //} else {
            // shake camera
            Camera.main.transform.localRotation = Quaternion.Euler(Random.Range(-5f, 5f) + 10, Random.Range(-5f, 5f), 0);
            //}
        }
    }


    //public void hitByTrap(Trap t, float dist) {
    //    // get properties of the trap for use!!!
    //    movementEnabled = false;
    //    GameObject.FindGameObjectWithTag("Explosion").GetComponent<SpriteRenderer>().enabled = true;
    //    GameObject.FindGameObjectWithTag("Explosion").GetComponent<Animator>().StartPlayback();
    //    GameObject.FindGameObjectWithTag("Explosion").GetComponent<Animator>().speed = 2;
    //    trapHitCountdown = 1f; // dist; // is this right??? - propably not...
    //}

    public void SetMovementEnabled(bool enabled) {
        movementEnabled = enabled;
        if (enabled) {
            Camera.main.transform.localRotation = Quaternion.Euler(10, 0, 0);
        }
    }
}
