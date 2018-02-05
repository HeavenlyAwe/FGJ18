using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetOutObjectiveScript : ObjectiveMission {

    public Vector3 goalPosition;
	private GameObject door;
    public Transform player;

    public GameObject endGameScreens;

    public ObjectiveMission[] missions;

    public float goalDistance = 1.0f;

	void Start() {
		door = GameObject.Find ("office_door");
	}
    	
	// Update is called once per frame
	void Update () {
        bool done = true;
        foreach (ObjectiveMission m in missions) {
            if (!m.IsDone()) {
                done = false;
            }
        }

        if (done == false) {
            return;
        }


		if (Input.GetKeyDown(KeyCode.U)) {
			SetDone(true);
			GetComponent<Image>().enabled = true;

			endGameScreens.SetActive(true);
			endGameScreens.GetComponent<EndGame>().winGame();
		}

		if (door != null) {
			if (Vector3.Distance (door.transform.position, player.position) <= goalDistance) {
				SetDone (true);
				GetComponent<Image> ().enabled = true;

				endGameScreens.SetActive (true);
				endGameScreens.GetComponent<EndGame> ().winGame ();
			}
		}
	}
}
