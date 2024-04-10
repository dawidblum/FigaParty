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
    [SerializeField] private Animator tutorialStarter;
    [SerializeField] private AudioLibrary.MusicType miniGameTheme;
    private int countDown;
    

    private void Start() {
        countDown = countDownStart;
        //tutorialStarter.SetTrigger("Hide");
        Invoke(nameof(DelayStart),8);
        GameManager.Instance.gameStopped = true;
    }

    private void Update() {
        
    }

    private void DelayStart() {
        StartCoroutine(CountDownRoutine());
    }
    
    private IEnumerator DelayTimeStop() {
        yield return new WaitForSeconds(.3f);
        Time.timeScale = 0;
    }

    private IEnumerator CountDownRoutine() {
        if (countDownLabel != null) {
            if (countDown > 0)
                countDownLabel.text = countDown.ToString();
            else
                countDownLabel.text = countDownFinishText;
        }

        switch (countDown) {
            case 0:
                GameManager.Instance.gameStopped = false;
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
            Time.timeScale = 1;
        }
    }
}
