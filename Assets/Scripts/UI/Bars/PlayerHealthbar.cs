/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthbar : HealthBar
{ 
    void Start()
    {
        if (GameManager.Instance.healthbarfFill == null) { Debug.LogError("GameManager is missing healtbar fill refference "); }
    }

    public override void DecreaseBy(float percAmount)
    {
        this.SetTo(GameManager.Instance.healthbarfFill.localScale.x - percAmount);
    }

    public override void IncreaseBy(float percAmount)
    {
        this.SetTo(GameManager.Instance.healthbarfFill.localScale.x + percAmount);
    }

    public override void SetTo(float perc)
    {
        GameManager.Instance.healthbarfFill.localScale = new Vector3(Mathf.Clamp(perc, 0.0f, 1.0f), 1.0f, 1.0f);
    }
}
*/