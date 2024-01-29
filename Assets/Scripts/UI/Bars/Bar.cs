using UnityEngine;

/// <summary>
/// Abstract base class for all status bars.
/// </summary>
public abstract class Bar : MonoBehaviour
{
    // **************** PUBLIC METHODS **************** //

    /// <summary>
    /// Sets bar fill to <paramref name="perc"/>
    /// </summary>
    public abstract void SetTo(float perc);

    /// <summary>
    /// Increase bar fill by <paramref name="percAmount"/>
    /// </summary>
    public abstract void IncreaseBy(float percAmount);

    /// <summary>
    /// Decrease bar fill by <paramref name="percAmount"/>
    /// </summary>
    public abstract void DecreaseBy(float percAmount);
}
