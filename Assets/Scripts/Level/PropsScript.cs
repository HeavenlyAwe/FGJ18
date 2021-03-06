using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsScript : MonoBehaviour {

    //public Dictionary<Color32, int> colorMap = new Dictionary<Color32, int>();
    public GameObject[] prefabs = new GameObject[16];
    public GameObject objectGO; //= new GameObject[16];

    // Use this for initialization

    void Start() {
        //colorMap.Add(new Color32(255, 255, 255, 255),0);     //white
        // colorMap.Add(new Color32(0, 0, 0, 255),0);          //tapet+panel
        // colorMap.Add(new Color32(0, 0, 255, 255),1);     //kakel

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Texture2D tex = sr.sprite.texture;

        int w = tex.width;
        int h = tex.height;

        Color32[] c = tex.GetPixels32();

        for (int x = 0; x < w; x++) {
            for (int y = 0; y < h; y++) {

                GameObject tmpGO;


                int index = (int)(c[x + w * y].r / 16);

                if (x == 74 && y == 4) {
                  //Debug.Log("FIRST    index = " + index + " x: " +  x + " y :" + y);
                }


                if (index == 0) {
                    tmpGO = Instantiate(prefabs[0], new Vector3((float)(x * 0.2), 0, (float)(y * 0.2)), Quaternion.identity);
                    tmpGO.transform.parent = objectGO.transform;
                    //Debug.Log("vial");
                } else if (index < 6) {
                    //Debug.Log(index);
                    tmpGO = Instantiate(prefabs[index], new Vector3((float)(x * 0.2), 0, (float)(y * 0.2)), Quaternion.identity);
                    tmpGO.transform.parent = objectGO.transform;
                } else if (index < 7) {
                  Debug.Log("index = " + index + " x: " +  x + " y :" + y);
                  //int left = x - 1;
                  //int index_l = (int)(c[left + w * y].r / 16);
                  //int right = x + 1;
                  //int index_r = (int)(c[right + w * y].r / 16);
                  //int up = y + 1;
                  //int index_u = (int)(c[x + w * up].r / 16);
                  //int down = y - 1;
                  //int index_d = (int)(c[x + w * down].r / 16);

                  //Debug.Log("index left = " + index_l + " x: " +  left + " y :" + y);
                  //Debug.Log("index right = " + index_r + " x: " +  right + " y :" + y);
                  //Debug.Log("index up = " + index_u + " x: " +  x + " y :" + up);
                  //Debug.Log("index down = " + index_d + " x: " +  x + " y :" + down);

                  tmpGO = Instantiate(prefabs[index], new Vector3((float)(x * 0.2), 0, (float)(y * 0.2)), Quaternion.identity);
                  tmpGO.transform.parent = objectGO.transform;

                }
            }
        }

        sr.enabled = false;
    }

    // Update is called once per frame
    void Update() {

    }
}
