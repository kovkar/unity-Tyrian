using UnityEngine;

/// <summary>
///  Subclass of <c>Actor</c> class representing enemies.
///  Overrides <name>OnCollisionEnter</name> to update currencies.
///  Overrides <name>SelfDestroy</name> to update enemy count.
/// </summary>
public class EnemyActor : Actor
{
    private static ScoreManager currencies;

    // **************** UNITY METHODS **************** //
    void Start()
    {
        currencies = ScoreManager.Instance;
    }

    // **************** OVERRIDE METHODS **************** //

    protected override void OnCollisionEnter(Collision collision)
    {
        currencies?.ProcessCollision(this, collision);
        base.OnCollisionEnter(collision);

    }

    protected override void SelfDestroy()
    {
        EnemiesFacotry.Instance.decreaseEnemyCountByOne();
        base.SelfDestroy();
    }
}
