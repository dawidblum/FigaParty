using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ClickMeText : MonoBehaviour {
    [SerializeField] private CanvasGroup label;
    private void Start() {
        StartCoroutine(ShowClickMeText());
    }

    IEnumerator ShowClickMeText() {
        yield return new WaitForSeconds(10);
        label.DOFade(1, 1).SetEase(Ease.OutBack);
    }
}
