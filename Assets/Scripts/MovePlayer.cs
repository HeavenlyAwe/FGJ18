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

    // Use this for initialization
    void Start () {
        lvlController = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelController>();
        level = lvlController.level;
        levelBitmap = lvlController.levelBitmaps[level];
        bitmapWidth = levelBitmap.texture.width;
        bitmapHeight = levelBitmap.texture.height;
        bitmapColors = levelBitmap.texture.GetPixels32();

    }

    // Update is called once per frame
    void FixedUpdate () {

        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        angle = horiz * rotSpeed * Time.deltaTime;

        transform.Rotate(new Vector3(0f, 1f, 0f), angle);

        dPos = transform.forward * speed * Time.deltaTime * vert;
        Vector3 pos = transform.position + dPos;

        int ix = (int)pos.x;
        int iz = (int)pos.z;

        Vector3 rot = transform.rotation.eulerAngles;
        Debug.Log(rot);
        if (rot.y >= 0 && rot.y < 90)
        {
            Debug.Log(isFloor(ix, iz - 1));
            if (isFloor(ix + 1, iz) && isFloor(ix + 1, iz + 1) && isFloor(ix, iz + 1))
            {
                transform.position = pos;
            }
        }
        if (rot.y >= 90 && rot.y < 180)
        {
            if (isFloor(ix - 1, iz) && isFloor(ix - 1, iz + 1) && isFloor(ix, iz + 1))
            {
                transform.position = pos;
            }
        }
        if (rot.y >= 180 && rot.y < 270)
        {
            if (isFloor(ix - 1, iz) && isFloor(ix - 1, iz - 1) && isFloor(ix, iz - 1))
            {
                transform.position = pos;
            }
        }
        if (rot.y >= 270 && rot.y < 360)
        {
            if (isFloor(ix + 1, iz) && isFloor(ix + 1, iz - 1) && isFloor(ix, iz - 1))
            {
                transform.position = pos;
            }
        }

    }

    void OnTriggerEnter(Collider other) {
        string tag = other.tag;
        if(tag=="Wall") {
            Vector3 pos = transform.position - dPos;
            transform.position = pos;
            hitCountdown = .1f;
        }
    }

    bool isFloor(int x, int z)
    {
        if(x<0 || z<0 || x>=bitmapWidth || z>=bitmapHeight)
        {
            return false;
        }
        return cFloor.Equals(bitmapColors[x + z * bitmapWidth]);
    }

}
