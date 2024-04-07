using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIElementHoverScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverScaleAmount = 1.1f;
    public float duration = 0.3f;
    private Vector3 originalScale;
    private Transform uiElementTransform;

    public UnityEvent OnHoverEnter;
    public UnityEvent OnHoverExit;

    [SerializeField] private CanvasGroup hoverImage;

    private void Start()
    {
        uiElementTransform = GetComponent<Transform>();
        originalScale = uiElementTransform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        uiElementTransform.DOScale(originalScale * hoverScaleAmount, duration).SetEase(Ease.OutQuart).SetId(gameObject);
        SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.UI_Hover, new Vector2(.6f, 1.2f), new Vector2(1.5f,1.5f));
        if(hoverImage == null) return;
        hoverImage.DOFade(1, duration).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DOTween.Kill(gameObject);
        uiElementTransform.DOScale(originalScale, duration).SetEase(Ease.OutQuart).SetId(gameObject);
        if(hoverImage == null) return;
        hoverImage.DOFade(0, duration).SetEase(Ease.OutBack);
    }
}