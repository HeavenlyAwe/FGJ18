using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadLevelFromBitmap : MonoBehaviour {

    public GameObject floor;
    public GameObject wall;
    public GameObject floorGOs;
    public GameObject wallGOs;

    private Color32 cWhite = new Color32(255, 255, 255, 255);

    // Use this for initialization
    void Start () {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Texture2D tex = sr.sprite.texture;

        int w = tex.width;
        int h = tex.height;

        Color32[] c = tex.GetPixels32();

        for (int x = 0; x<w; x++)
        {
            for( int y = 0; y<h; y++)
            {
                if (c[x+w*y].Equals(cWhite))
                {
                    GameObject tmpGO = Instantiate(floor, new Vector3(x, 0, y), Quaternion.identity);
                    tmpGO.transform.parent = floorGOs.transform;
                }
                else {
					GameObject tmpGO = Instantiate(wall, new Vector3(x, (float)(5.2) , y), Quaternion.Euler(-90,0,0));
                    tmpGO.transform.parent = wallGOs.transform;
                }
                
            }
        }
        sr.enabled = false;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
