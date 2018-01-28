using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    public float speed;
    public float rotSpeed;

    public float angle=0;

    private Vector3 dPos;
    private float hitCountdown = 0;
    private LevelController lvlController;
    private int level;
    private Sprite levelBitmap;
    private int bitmapWidth;
    private int bitmapHeight;
    private Color32[] bitmapColors;
    private Color32 cFloor = new Color32(255, 255, 255, 255);


    private bool enableMovement = true;
    private float trapHitCountdown;

    private float hitPadding = 2.5f;

    // Use this for initialization
    void Start () {
        lvlController = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelController>();
        level = lvlController.level;
        levelBitmap = lvlController.levelBitmaps[level];
        bitmapWidth = levelBitmap.texture.width;
        bitmapHeight = levelBitmap.texture.height;
        bitmapColors = levelBitmap.texture.GetPixels32();

        // Now the player object is available, get a reference to the gameobject in Traps-script
        GameObject.FindGameObjectWithTag("Traps").GetComponent<Traps>().getPlayerGO();

    }

    // Update is called once per frame
    void Update () {

        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        angle = horiz * rotSpeed * Time.deltaTime;

        transform.Rotate(new Vector3(0f, 1f, 0f), angle);

        dPos = transform.forward * speed * Time.deltaTime * vert;
        Vector3 pos = transform.position ;

        Vector3 rot = transform.rotation.eulerAngles;

        float x = pos.x*5f-.5f;
        float z = pos.z*5f-.5f;
        float dx = dPos.x;
        float dz = dPos.z;
        int ix = (int)(x+dx+hitPadding*Mathf.Sign(dx));
        int iz = (int)(z+dz+hitPadding*Mathf.Sign(dz));

        if(!isFloor(ix, (int)z))
        {
            // hit vertically
            dx = 0;
        }

        if(!isFloor((int)x, iz))
        {
            // hit horizontally
            dz = 0;
        }

        if (enableMovement)
        {
            transform.position = pos + new Vector3(dx,0,dz);

        }
        else
        {
            // movement not enabled.
            trapHitCountdown -= Time.deltaTime;
            if(trapHitCountdown<0)
            {
                enableMovement = true;
                GameObject.FindGameObjectWithTag("Explosion").GetComponent<SpriteRenderer>().enabled = false;
                GameObject.FindGameObjectWithTag("Explosion").GetComponent<Animator>().StopPlayback();
                Camera.main.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                // shake camera
                Camera.main.transform.rotation = Quaternion.Euler(Random.Range(0f, 1f), Random.Range(0f, 1f), 0);
            }
        }

    }

    bool isFloor(int x, int z)
    {
        if(x<0 || z<0 || x>=bitmapWidth || z>=bitmapHeight)
        {
            return false;
        }
        //return cFloor.Equals(bitmapColors[x + z * bitmapWidth]);
        byte rComp = bitmapColors[x + z * bitmapWidth].r;
        bool bFloor = rComp > 48;
        return bFloor;
    }

    public void hitByTrap(Trap t, float dist)
    {
        // get properties of the trap for use!!!
        enableMovement = false;
        GameObject.FindGameObjectWithTag("Explosion").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.FindGameObjectWithTag("Explosion").GetComponent<Animator>().StartPlayback();
        GameObject.FindGameObjectWithTag("Explosion").GetComponent<Animator>().speed = 2;
        trapHitCountdown = 1f; // dist; // is this right??? - propably not...

    }
}
