/// <summary>
///  Implementation of <c>Actor</c> class representing final boss.
///  Overrides <name>SelfDestroy</name> to proceed to endgame scene.
/// </summary>
public class BossActor : Actor
{
    private void Start()
    {
        healthBar = GameManager.Instance.bossHealthbar;
    }

    // **************** OVERRIDE METHODS ****************//

    protected override void SelfDestroy()
    {
        GameManager.Instance.EndGame(true);
        return;
    }
}
