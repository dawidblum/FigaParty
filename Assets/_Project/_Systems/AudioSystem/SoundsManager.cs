using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundsManager : Singleton<SoundsManager> {
    private AudioSource mainAudio;
    private AudioSource musicAudio;

    private AudioLibrary audioLibrary;
    
    [SerializeField] private float fadeDuration = 2f;
    private float currentVolume = 1f;
    
    public void PlayAudioShot(AudioClip _clip, Vector2? _pitchRange = null, Vector2? _volumeRange = null)
    {
        HandleAudioPitch(_pitchRange);
        HandleAudioVolume(_volumeRange);
        
        mainAudio.PlayOneShot(_clip);
    }

    public void PlayAudioShot(AudioLibrary.SoundType _type, Vector2? _pitchRange = null, Vector2? _volumeRange = null)
    {
        HandleAudioPitch(_pitchRange);
        HandleAudioVolume(_volumeRange);

        var sfx = audioLibrary.GetAudioClip(_type);
        mainAudio.PlayOneShot(sfx);
    }
    
    private void HandleAudioVolume(Vector2? _volumeRange)
    {
        if (_volumeRange != null)
            mainAudio.volume = Random.Range(_volumeRange.Value.x, _volumeRange.Value.y);
        else
            mainAudio.volume = 1;
    }

    private void HandleAudioPitch(Vector2? _pitchRange)
    {
        if (_pitchRange != null)
            mainAudio.pitch = Random.Range(_pitchRange.Value.x, _pitchRange.Value.y);
        else
            mainAudio.pitch = 1;
    }

    public void ChangeTrack(AudioLibrary.MusicType _type) {
        var music = audioLibrary.GetAudioClip(_type);
        StartCoroutine(ChangeMusicRoutine(music));
    }
    
    private IEnumerator ChangeMusicRoutine(AudioClip _newTrack)
    {
        float currentTime = 0f;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            currentVolume = Mathf.Lerp(1f, 0f, currentTime / fadeDuration);
            musicAudio.volume = currentVolume;
            yield return null;
        }

        musicAudio.clip = _newTrack;
        musicAudio.Play();

        currentTime = 0f;
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            currentVolume = Mathf.Lerp(0f, 1f, currentTime / fadeDuration);
            musicAudio.volume = currentVolume;
            yield return null;
        }
    }
    
}