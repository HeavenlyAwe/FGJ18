using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

	public float timeLeft;
	
	[HideInInspector]
	public int minutes;
	[HideInInspector]
	public int seconds;
	public Text timeShown;

	private bool musicIntensified = false;

	void Start() {
		minutes = Mathf.FloorToInt(timeLeft/60);
		seconds = Mathf.CeilToInt(timeLeft%60);
		string countdownString = seconds < 10 ?  ":0" : ":";
		timeShown.text =  minutes + countdownString + seconds;
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;

		minutes = Mathf.FloorToInt(timeLeft/60);
		seconds = Mathf.CeilToInt(timeLeft%60);
		string countdownString = seconds < 10 ?  ":0" : ":";
		timeShown.text =  minutes + countdownString + seconds;
		
		if (timeLeft < 0f) {
			timeShown.text = "0:00";
			Destroy(this);
		}
		
		if (timeLeft <= 30f && !musicIntensified) {
			FindObjectOfType<AudioManager>().IntensifyGameThemeByTimer();
			musicIntensified = true;
		}
	}
}
