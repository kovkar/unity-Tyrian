/// <summary>
///  Subclass of <c>Actor</c> representing player ship:
/// </summary>
public class PlayerActor : Actor
{
    // **************** OVVERIDE METHODS **************** //
    protected override void TakeDamage(int damage)
    {
        if (!Cheats.Instance.isImmortal()) { base.TakeDamage(damage); }
    }

    protected override void SelfDestroy()
    {
        GameManager.Instance.endGame();
    }

}
