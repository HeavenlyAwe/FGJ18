using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveScript : MonoBehaviour {
	
	public Transform VialTransform;
	private Transform PlayerTransform;

	public Image VialAcquiredImage;
	public Image GotOutImage;

	public bool IsPickedUp = false;

	public void Update () {
		PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		float DistanceToPlayer = Vector3.Distance(VialTransform.position, PlayerTransform.position);
		if (DistanceToPlayer < 0.5f && Input.GetKeyDown(KeyCode.E)) {
			//Remove vial object from sight
			VialAcquiredImage.enabled = true;
			IsPickedUp = true;
		}
	}

}
