using UnityEngine;
/// <summary>
///  Implementation of <c>Actor</c> class representing final boss.
///  Overrides <name>SelfDestroy</name> to proceed to endgame scene.
/// </summary>
public class BossActor : Actor
{
    private void Start()
    {
        healthBar = HUDManager.Instance.BossHealthBar;
    }

    // **************** OVERRIDE METHODS ****************//

    protected override void OnCollisionEnter(Collision collision)
    {
        ScoreManager.Instance.ProcessCollision(this, collision);
        base.OnCollisionEnter(collision);

    }

    protected override void SelfDestroy()
    {
        GameManager.Instance.EndGame(true);
        return;
    }
}
