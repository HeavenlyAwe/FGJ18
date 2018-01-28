using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public int level;

    public Sprite[] levelBitmaps;

    private MovePlayer mp;

	// Use this for initialization
	void Start () {
        level = 0;
        mp = GameObject.FindGameObjectWithTag("Player").GetComponent<MovePlayer>();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
