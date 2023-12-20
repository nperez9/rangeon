using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SfxManager : MonoBehaviour
{
	public static SfxManager instance;
	private AudioSource audioSource;

	[SerializeField] AudioClip[] audioClips;
	[SerializeField] string[] clipsKey;

	[SerializeField] public Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();

	private void Awake()
	{
		buildClips();
		audioSource = GetComponent<AudioSource>();
		instance = this;
	}

	private void buildClips()
	{
		try
		{
			if (audioClips.Length != clipsKey.Length)
			{
				throw new Exception("The two arrays must have the same Lenght");
			}

			for (int i = 0; i < audioClips.Length; i++)
			{
				clips.Add(clipsKey[i], audioClips[i]);
			}
		}
		catch (Exception e)
		{
			Debug.LogException(e);
			Debug.LogError("Error building AudioClips");
		}
	}

	public void PlaySfx(AudioClip sfx, float volume = 1f)
	{
		audioSource.PlayOneShot(sfx, volume);
	}

	public void PlayNotCash(float volume = 1f)
	{
		PlayClip("notCash", volume);
	}

	public void PlayClip(string clipKey, float volume = 1f)
	{
		if (!clips[clipKey])
		{
			Debug.LogError("Invalind clipKey: " + clipsKey);
			return;
		}
		audioSource.PlayOneShot(clips[clipKey], volume);
	}

	public void MuteAudio(bool muted)
	{
		audioSource.mute = muted;
	}
}
