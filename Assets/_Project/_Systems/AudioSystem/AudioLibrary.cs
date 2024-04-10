using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        Minigame_Win,
        Countdown_Three,
        Countdown_Two,
        Countdown_One,
        Countdown_Go,
        Rocks,
        Correct_Person,
        Incorrect_Person,
        Complete,
        Grass,
        Water,
        Zap,
        Jar_Filling,
        Belt,
        Jar_Fail,
        Jar_Win,
        Yay,
        Wrong_Order,
        Correct_Order,
        Bell_Press,
        Pick_Sauce,
        Pick_Ingredient,
        New_Order
        
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