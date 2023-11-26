using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

	[SerializeField]
	private AudioSource mainMusicAudioSource;
	[SerializeField]
	private float standardFadeDuration = 1.0f;

	private bool _isMainMusicFadeOnGoing = false;

	void Awake()
    {
        Instance = this;
    }

	public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
	{
		_isMainMusicFadeOnGoing = true;

		float startVolume = audioSource.volume;
		while (audioSource.volume > 0)
		{
			audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
			yield return null;
		}

		audioSource.Stop();
		audioSource.volume = startVolume;

		_isMainMusicFadeOnGoing = false;
	}

	public IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
	{
		_isMainMusicFadeOnGoing = true;
		audioSource.volume = 0.01f;
		audioSource.Play();
		while (audioSource.volume < 1.0f)
		{
			audioSource.volume += audioSource.volume * Time.deltaTime / FadeTime;
			yield return null;
		}

		_isMainMusicFadeOnGoing = false;
	}

	public bool ToggleMainMusic()
    {
		if (mainMusicAudioSource == null || _isMainMusicFadeOnGoing)
			return false;

		if (mainMusicAudioSource.isPlaying)
		{
			StartCoroutine(FadeOut(mainMusicAudioSource, standardFadeDuration));
		}
		else
		{
			StartCoroutine(FadeIn(mainMusicAudioSource, standardFadeDuration));
		}

		return true;
	}
}
