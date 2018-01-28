using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetOutObjectiveScript : ObjectiveMission {

    public Vector3 goalPosition;
    public Transform player;

    public GameObject endGameScreens;

    public ObjectiveMission[] missions;

    public float goalDistance = 1.0f;
    	
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

		if (Vector3.Distance(goalPosition, player.position) <= goalDistance) {
            SetDone(true);
            GetComponent<Image>().enabled = true;

            endGameScreens.SetActive(true);
            endGameScreens.GetComponent<EndGame>().winGame();
        }
	}
}
