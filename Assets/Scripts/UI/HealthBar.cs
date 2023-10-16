using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class HealthBar : MonoBehaviour
{
    public void SetReferenceToActor()
    {
        Actor actor = this.GetComponent<Actor>();
        if (actor != null) 
        { 
            actor.SetHealthBarRefference(this);
        } 
        else 
        { 
            Debug.LogError(this.gameObject.name + " is not an Actor but has HealthBar assigned."); 
        }
    }

    public abstract void SetTo(float perc);
    public abstract void IncreaseBy(float percAmount);
    public abstract void DecreaseBy(float percAmount);
}
