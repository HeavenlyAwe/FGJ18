using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VialObjectiveScript : ObjectiveMission {

    public Transform player;
    private GameObject vial;
    private float targetDistance = 1.5f;
	private bool completedObjective; 

 	// Use this for initialization
	void Start () {
        vial = GameObject.Find("vial");
		completedObjective = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (vial != null) {
			if (Vector3.Distance(player.position, vial.transform.position) <= targetDistance && !completedObjective) {
                    SetDone(true);
                    GetComponent<Image>().enabled = true;
                    FindObjectOfType<AudioManager>().Play("CollectVial");
					completedObjective = true;
					MeshRenderer[] ms = vial.GetComponentsInChildren<MeshRenderer> ();
					ms [0].enabled = false;
					
            }
        }

        //if (Input.GetKeyDown(KeyCode.E)) {
        //    SetDone(true);
        //    GetComponent<Image>().enabled = true;
        //    FindObjectOfType<AudioManager>().Play("CollectVial");
        //}
    }
}
