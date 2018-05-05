using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour {

    public TrapLogic trapLogicPrefab;

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.G)) {
            TrapLogic trapLogic = GameObject.Instantiate<TrapLogic>(trapLogicPrefab, new Vector3(10, 0, 10), Quaternion.identity);
            trapLogic.Initialize(0, 0, "da bomb", 3.0f);
        }
	}
}
