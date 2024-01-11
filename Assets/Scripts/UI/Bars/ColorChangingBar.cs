using UnityEngine;
using UnityEngine.UI;

public class ColorChangingBar : BasicBar
{
    [Header("Color Settings")]
    [SerializeField] private Gradient gradient = new Gradient();

    private Image img;

    private void Start()
    {
        img = fill.GetComponent<Image>();
        if (img is null) Debug.LogError($"{fill} has no Image component to change color!");
    }

    public override void SetTo(float perc)
    {
        img.color = gradient.Evaluate(perc);
        base.SetTo(perc);
    }
}
