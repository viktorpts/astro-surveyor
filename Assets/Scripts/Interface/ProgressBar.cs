using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private RectTransform wipe;
    public float progress = 0f;
    public float offsetX = 0f;
    public float offsetY = 0f;

    void Start()
    {
        wipe = transform.GetChild(0).gameObject.GetComponent<RectTransform>();
    }

    void Update()
    {
        // wipe.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, progress);
        wipe.localScale = new Vector3(progress, 1, 1);
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetX, offsetY + 150f);
    }
}