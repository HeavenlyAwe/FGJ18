using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VialObjectiveScript : ObjectiveMission {

    public Transform player;
    private GameObject vial;
    public float targetDistance = 1.0f;

 	// Use this for initialization
	void Start () {
        vial = GameObject.Find("Vial");
	}
	
	// Update is called once per frame
	void Update () {
        //if (vial != null) {
        //    if (Vector3.Distance(player.position, vial.transform.position) <= targetDistance) {
        //        if (Input.GetKeyDown(KeyCode.E)) {
        //            SetDone(true);
        //            GetComponent<Image>().enabled = true;
        //        }
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.E)) {
            SetDone(true);
            GetComponent<Image>().enabled = true;
        }
    }
}
