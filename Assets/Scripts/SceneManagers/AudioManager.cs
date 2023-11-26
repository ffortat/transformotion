using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

	[SerializeField]
	private AudioSource mainMusicAudioSource;
	[SerializeField]
	private float standardFadeDuration = 1.0f;

	private bool _isMusicFadingGoingOn = false;
	private bool _isAmbianceMusicChangeOnGoing = false;

	void Awake()
    {
        Instance = this;
    }

	public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
	{
		_isMusicFadingGoingOn = true;
		float startVolume = audioSource.volume;
		while (audioSource.volume > 0)
		{
			audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
			yield return null;
		}

		audioSource.Stop();
		audioSource.volume = startVolume;
		_isMusicFadingGoingOn = false;
	}

	public IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
	{
		_isMusicFadingGoingOn = true;
		audioSource.volume = 0.01f;
		audioSource.Play();
		while (audioSource.volume < 1.0f)
		{
			audioSource.volume += audioSource.volume * Time.deltaTime / FadeTime;
			yield return null;
		}
		_isMusicFadingGoingOn = false;
	}

	public void ToggleMainMusic()
    {
		if (_isMusicFadingGoingOn || mainMusicAudioSource == null)
			return;

		if (mainMusicAudioSource.isPlaying)
		{
			StartCoroutine(FadeOut(mainMusicAudioSource, standardFadeDuration));
		}
		else
		{
			StartCoroutine(FadeIn(mainMusicAudioSource, standardFadeDuration));
		}
	}

	public IEnumerator ChangeAmbianceMusic(AudioClip newAmbiance)
	{
		if (!_isAmbianceMusicChangeOnGoing && newAmbiance != null && mainMusicAudioSource.clip.name != newAmbiance.name)
		{
			_isAmbianceMusicChangeOnGoing = true;
			yield return StartCoroutine(FadeOut(mainMusicAudioSource, standardFadeDuration));
			mainMusicAudioSource.clip = newAmbiance;
			yield return StartCoroutine(FadeIn(mainMusicAudioSource, standardFadeDuration));
			_isAmbianceMusicChangeOnGoing = false;
		}
	}
}
