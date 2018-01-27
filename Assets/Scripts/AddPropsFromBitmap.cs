using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPropsFromBitmap : MonoBehaviour {

	public Dictionary<Color32, int> colorMap = new Dictionary<Color32, int>();
	public GameObject[] preFabs = new GameObject[10];
	public GameObject[] objectGO = new GameObject[10];

	// Use this for initialization
	void Start () {

		SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Texture2D tex = sr.sprite.texture;

		int w = tex.width;
		int h = tex.height;

		Color32[] c = tex.GetPixels32();

		for (int x = 0; x < w; x++) {
			for (int y = 0; y < h; y++) {

				GameObject tmpGO;

                switch((colorMap[c[x+w*y]])) {
                    case 0:
                        //tmpGO = Instantiate(preFabs[0], new Vector3((float)(x*0.2), 0, (float)(y*0.2)), Quaternion.identity);
                        //tmpGO.transform.parent = objectGO[0].transform;
                        break;

                    case 1:
                        //tmpGO = Instantiate(preFabs[1], new Vector3((float)(x*0.2), -0.5f , (float)(y*0.2)), Quaternion.identity);
                        //tmpGO.transform.parent = objectGO[1].transform;
                        break;
                }

			}

		}
		
		sr.enabled = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
