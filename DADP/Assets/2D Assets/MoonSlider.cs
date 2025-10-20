using UnityEngine;
using UnityEngine.UI;

public class MoonSlider : MonoBehaviour
{
    public Slider slider;
    public RectTransform purpleCover;
    public float moveRange = 100f;

    private Vector2 startPos;

    void Start()
    {
        startPos = purpleCover.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float offset = (slider.value - 0.5f) * moveRange;
        purpleCover.anchoredPosition = new
            Vector2(startPos.x + offset, startPos.y);
    }
}
