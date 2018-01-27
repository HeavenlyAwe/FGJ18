using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadLevelFromBitmap : MonoBehaviour {
	public Dictionary<Color32, int> colorMap = new Dictionary<Color32, int>();
	public GameObject[] preFabs = new GameObject[10];
	public GameObject[] objectGO = new GameObject[10];

    // Use this for initialization
    void Start () {
        colorMap.Add(new Color32(255, 255, 255, 255),0);
        colorMap.Add(new Color32(0, 0, 0, 255),1);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Texture2D tex = sr.sprite.texture;

        int w = tex.width;
        int h = tex.height;

        Color32[] c = tex.GetPixels32();

        for (int x = 0; x<w; x++)
        {
            for( int y = 0; y<h; y++)
            {
                //int colorInteger = colorMap[c[x+w*y]];
				GameObject tmpGO;

                if(colorMap.ContainsKey(c[x+w*y])) {

                     switch((colorMap[c[x+w*y]])) {
                    case 0:
                        tmpGO = Instantiate(preFabs[0], new Vector3((float)(x*0.2), 0, (float)(y*0.2)), Quaternion.identity);
                        //tmpGO = Instantiate(preFabs[colorInteger], new Vector3(x, 0, y), Quaternion.identity);
                        tmpGO.transform.parent = objectGO[0].transform;
                        break;

                    case 1:
                        tmpGO = Instantiate(preFabs[1], new Vector3((float)(x*0.2), -0.5f , (float)(y*0.2)), Quaternion.identity);
                        tmpGO.transform.parent = objectGO[1].transform;
                        break;
                }
                } else {
                        tmpGO = Instantiate(preFabs[0], new Vector3((float)(x*0.2), 0, (float)(y*0.2)), Quaternion.identity);
                        //tmpGO = Instantiate(preFabs[colorInteger], new Vector3(x, 0, y), Quaternion.identity);
                        tmpGO.transform.parent = objectGO[0].transform;
                }

               
					 
				}
                }
            
        sr.enabled = false;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
