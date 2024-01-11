using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TemperatureBar : ColorChangingBar
{
    [SerializeField] private TextMeshProUGUI overheatedText;

    public override void SetTo(float perc)
    {
        overheatedText.gameObject.SetActive(perc >= 1.0f);
        base.SetTo(perc);
    }
}
