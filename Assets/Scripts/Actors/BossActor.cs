/// <summary>
///  Implementation of <c>Actor</c> class representing final boss.
///  Overrides <name>SelfDestroy</name> to proceed to endgame scene.
/// </summary>
public class BossActor : Actor
{
    // **************** OVERRIDE METHODS ****************//

    protected override void SelfDestroy()
    {
        // game ends
        return;
    }
}
