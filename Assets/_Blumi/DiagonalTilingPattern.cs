using UnityEngine;
using UnityEngine.UI;

public class DiagonalTilingPattern : MonoBehaviour {
    public RawImage rawImage;
    public float speed = 1.0f;
    private RectTransform rectTransform;
    private float x = 0;
    private float y = 0;
    private Rect uvRect;

    private void Start() {
        rectTransform = rawImage.GetComponent<RectTransform>();
    }

    private void Update() {
        uvRect.x += Time.deltaTime * speed;
        uvRect.y += Time.deltaTime * speed;
        uvRect.width = rawImage.uvRect.width;
        uvRect.height = rawImage.uvRect.height;

        // Set the modified uvRect back to the Raw Image
        rawImage.uvRect = uvRect;
    }
}