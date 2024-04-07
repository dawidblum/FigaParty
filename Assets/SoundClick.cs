using System;
using UnityEngine;

public class SoundClick : MonoBehaviour {
    [SerializeField] private AudioLibrary.SoundType soundType;    
    private void OnMouseDown() {
        SoundsManager.Instance.PlayAudioShot(soundType, new Vector2(.8f, 1.2f), new Vector2(.8f,1.1f));
    }
}
