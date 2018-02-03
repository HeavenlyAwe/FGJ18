using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public bool isAudioManagerForMenu;
	private int numberOfTrapsDetonated; 


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
			numberOfTrapsDetonated = 0;
			StartGameMusic();
		}
	}
		

	//This method's here only for testing, shouldn't really do anything
	public void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			IntensifyGameThemeByTrap ();
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
		Play("LowIntensity");
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

		if (high.source.isPlaying) {
			Debug.Log ("Fully intensified");
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
			medium.source.time = low.source.time + remainder;
			medium.source.PlayScheduled(AudioSettings.dspTime + remainder);
			low.source.SetScheduledEndTime(AudioSettings.dspTime + remainder);
		}
	}

	public void IntensifyGameThemeByTimer() {
		Stop("LowIntensity");
		Stop("MediumIntensity");
		Stop("HighIntensity");

		Play("CountdownIntensity");
		}

	public void Play(string sound)
	{
		Sound s = GetSound (sound);
		if (s == null)
		{
			return;
		}

		s.source.Play();
	}

	public void PlayWithoutDuplicate(string sound) {
		Sound s = GetSound (sound);
		if (s == null || s.source.isPlaying)
		{
			return;
		}
		s.source.Play();
	}

	public void PlayFromLocation(string sound, Vector3 position) {
		Sound s = GetSound (sound);
		if (s == null || s.source.isPlaying)
		{
			return;
		}
		AudioSource.PlayClipAtPoint(s.clip, position, s.volume);
	}

	public void Stop(string sound)
	{
		Sound s = GetSound (sound);
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
