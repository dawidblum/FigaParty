using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIElementScaler : MonoBehaviour
{
    public float scaleAmount = 1.2f;
    public float duration = 0.3f;
    private Vector3 originalScale;
    private Transform uiElementTransform;

    private void Start()
    {
        uiElementTransform = GetComponent<Transform>();
        originalScale = uiElementTransform.localScale;
    }

    public void ScaleElement()
    {
        Sequence sequence = DOTween.Sequence();
        SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.UI_Confirm, new Vector2(.6f, 1.2f), new Vector2(1.5f,1.5f));
        sequence.Append(uiElementTransform.DOScale(uiElementTransform.localScale * scaleAmount, duration).SetEase(Ease.OutQuart)).SetId(gameObject);
        sequence.Append(uiElementTransform.DOScale(originalScale, duration).SetEase(Ease.InQuart)).SetId(gameObject);
    }
}