using System;
using System.Collections;
using System.Numerics;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class MiniGameManager : MonoBehaviour {
    [SerializeField] private int countDownStart = 3;
    [SerializeField] private TextMeshProUGUI countDownLabel;
    [SerializeField] private string countDownFinishText;
    [SerializeField] private CanvasGroup label;
    [SerializeField] private AudioLibrary.MusicType miniGameTheme;
    private int countDown;


    private void Start() {
        countDown = countDownStart;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(CountDownRoutine());
    }

    private IEnumerator CountDownRoutine() {
        if (countDown > 0)
            countDownLabel.text = countDown.ToString();
        else
            countDownLabel.text = countDownFinishText;

        switch (countDown) {
            case 0:
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Countdown_Go);
                break;
            case 1:
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Countdown_One);
                SoundsManager.Instance.ChangeTrack(miniGameTheme);
                break;
            case 2:
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Countdown_Two);
                break;
            case 3:
                SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Countdown_Three);
                break;
            
            
        }
        
        label.DOFade(1, .3f).SetUpdate(true).OnComplete(() =>
            label.DOFade(0f, .3f).SetUpdate(true).SetDelay(.3f));

        RectTransform labelRect = (RectTransform)label.transform;
        
        labelRect.DOScale(Vector3.one, .3f).SetUpdate(true).SetUpdate(true).OnComplete(() =>
            labelRect.DOScale(Vector3.zero, .3f).SetUpdate(true).SetDelay(.3f));

        yield return new WaitForSecondsRealtime(1);
        countDown--;
        if (countDown >= 0)
            StartCoroutine(CountDownRoutine());
        else {
            label.gameObject.SetActive(false);
        }
    }
}
