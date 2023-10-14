using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HealthBar
{
    public void SetTo(float perc);
    public void IncreaseBy(float percAmount);
    public void DecreaseBy(float percAmount);
}
