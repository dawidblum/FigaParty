using System;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    private void Start() {
        SoundsManager.Instance.ChangeTrack(AudioLibrary.MusicType.Figaro_Theme);
    }
}
