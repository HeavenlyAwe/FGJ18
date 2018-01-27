using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public Sound[] sounds;

	void Awake()
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

	void Start() {
		PlayMainMenuThemes();
	}

	void PlayMainMenuThemes() {
		Sound menuIn = Array.Find(sounds, item => item.name == "MenuIn");
		Sound menuLoop = Array.Find(sounds, item => item.name == "MenuLoop");
		if (menuIn == null || menuLoop == null)
		{
			Debug.LogWarning("Sounds for main menu theme not found!");
			return;
		}

		menuIn.source.Play();
		menuLoop.source.PlayScheduled (AudioSettings.dspTime + menuIn.clip.length);
	}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.Play();
	}

}
