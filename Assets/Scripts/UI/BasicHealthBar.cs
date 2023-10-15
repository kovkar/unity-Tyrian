using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHealthBar : HealthBar
{
    [SerializeField]
    private RectTransform healthbarfFill;

    void Start()
    {
        base.SetReferenceToActor();
        if (healthbarfFill == null) { Debug.LogError(this.gameObject.name + " missing healtbar fill refference "); }
    }

    public override void DecreaseBy(float percAmount)
    {
        this.SetTo(healthbarfFill.localScale.x - percAmount);
    }

    public override void IncreaseBy(float percAmount)
    {
        this.SetTo(healthbarfFill.localScale.x + percAmount);
    }

    public override void SetTo(float perc)
    {
        healthbarfFill.localScale = new Vector3(Mathf.Clamp(perc, 0.0f, 1.0f), 1.0f, 1.0f);
    }
}
