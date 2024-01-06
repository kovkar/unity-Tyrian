using UnityEngine;

/// <summary>
/// Implementation of <c>Bar</c>, controlling size of <var>fill</var>.
/// </summary>
public class BasicBar : Bar
{
    [Header("Refference to set in inspecotr!")]
    [SerializeField] private RectTransform fill;

    // **************** UNITY METHODS **************** //

    void Awake()
    {
        if (fill is null)
            Debug.LogError($"Fill refference in {gameObject.name} not set!");
    }


    // **************** OVERRIDE METHODS **************** // 
    
    public override void DecreaseBy(float percAmount)
    {
        SetTo(fill.localScale.x - percAmount);
    }

    public override void IncreaseBy(float percAmount)
    {
        SetTo(fill.localScale.x + percAmount);
    }

    public override void SetTo(float perc)
    {
        fill.localScale = new Vector3(Mathf.Clamp(perc, 0.0f, 1.0f), 1.0f, 1.0f);
    }
}
