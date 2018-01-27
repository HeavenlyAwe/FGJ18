using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public Sound[] sounds;

	public void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

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
		StartGameMusic();
	}

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
		menuLoop.source.PlayScheduled (AudioSettings.dspTime + menuIn.clip.length);
	}

	public void StartGameMusic() {
		Stop ("MenuIn");
		Stop ("MenuLoop");

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
