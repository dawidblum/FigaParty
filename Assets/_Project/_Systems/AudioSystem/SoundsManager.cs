using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

public class AudioLibrary : MonoBehaviour {
    [Serializable]
    public struct SFXPair {
        public AudioClip clip;
        public SoundType type;
    }
    
    [Serializable]
    public struct MusicPair {
        public AudioClip clip;
        public MusicType type;
    }

    [SerializeField] private List<SFXPair> sfxList = new List<SFXPair>();
    [SerializeField] private List<MusicPair> musicList = new List<MusicPair>();

    public enum SoundType {
        UI_Confirm,
        UI_Cancel,
        UI_Deny,
        UI_Hover,
        Time_Low,
        Minigame_Intro,
        Minigame_Fail,
        Minigame_Win
    } 
    
    public enum MusicType {
        Menu_Theme,
        Find_Figa_Theme,
        Dig_To_Fig_Theme,
        Figa_Makes_Pizza_Theme,
        Figa_Jam_Theme,
        Figaro_Theme
    }

    public AudioClip GetAudioClip(SoundType _type) {
        return sfxList.Where(_sfxPair => _sfxPair.type == _type)
            .Select(_sfxPair => _sfxPair.clip).FirstOrDefault();
    }
    public AudioClip GetAudioClip(MusicType _type) {
        return musicList.Where(_musicPair => _musicPair.type == _type)
            .Select(_musicPair => _musicPair.clip).FirstOrDefault();
    }
    
    
    
    
}
