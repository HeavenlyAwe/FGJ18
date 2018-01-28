using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public bool isAudioManagerForMenu;

	public Sound[] sounds;

	public void Awake()
	{

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}

	public void Start() {
		if (isAudioManagerForMenu) {
			PlayMainMenuThemes();
		} else {
			StartGameMusic();
		}
	}

	public void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			IntensifyGameThemeByTrap ();
		}
		if (Input.GetKeyDown(KeyCode.T)) {
			IntensifyGameThemeByTimer ();
		}
	}

	public void PlayMainMenuThemes() {
		Sound menuIn = GetSound("MenuIn");
		Sound menuLoop = GetSound("MenuLoop");
		if (menuIn == null || menuLoop == null)
		{
			Debug.LogWarning("Sounds for main menu theme not found!");
			return;
		}

		menuIn.source.Play();
		// plus 0.058s was only for own performance issues, reason hasn't been checked
		menuLoop.source.PlayScheduled (AudioSettings.dspTime + menuIn.clip.length + 0.058f);
	}

	public void StartGameMusic() {
		Sound low = GetSound("LowIntensity");
		if (low == null)
		{
			Debug.LogWarning("Low intensity game theme not found!");
			return;
		}

		low.source.Play();
	}


	public void IntensifyGameThemeByTrap() {
		Sound low = GetSound ("LowIntensity");
		Sound medium = GetSound ("MediumIntensity");
		Sound high = GetSound ("HighIntensity");
		float clipLength = low.clip.length;

		if (low == null || medium == null || high == null)
		{
			Debug.LogWarning("Sounds for game theme not found!");
			return;
		}

		if (medium.source.isPlaying) {
			float remainder = 3.75f - low.source.time % 3.75f;
			high.source.time = medium.source.time + remainder;
			high.source.PlayScheduled(AudioSettings.dspTime + remainder);
			medium.source.SetScheduledEndTime(AudioSettings.dspTime + remainder);
		}

		if (low.source.isPlaying) {
			float remainder = 3.75f - low.source.time % 3.75f;
			Debug.LogWarning ("Remainder:" + remainder);
			medium.source.time = low.source.time + remainder;
			medium.source.PlayScheduled(AudioSettings.dspTime + remainder);
			low.source.SetScheduledEndTime(AudioSettings.dspTime + remainder);
		}
	}

	public void IntensifyGameThemeByTimer() {
		Sound countdown = GetSound("CountdownIntensity");

		if (countdown == null)
		{
			Debug.LogWarning("Sounds for countdown game theme not found!");
			return;
		}
		Stop("LowIntensity");
		Stop("MediumIntensity");
		Stop("HighIntensity");

		countdown.source.Play();
		}

	private void Play(string sound)
	{
		Sound s = GetSound (name);
		if (s == null)
		{
			return;
		}

		s.source.Play();
	}

	private void Stop(string sound)
	{
		Sound s = GetSound (name);
		if (s == null)
		{
			return;
		}
		s.source.Stop();
	}

	private Sound GetSound(string name) {
		Sound s = Array.Find (sounds, item => item.name == name);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return null;
		}
		return s;
	}

}
