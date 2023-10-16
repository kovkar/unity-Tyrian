using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipActor : Actor
{
    public override void TakeDamage(int damage)
    {
        if (!Cheats.Instance.isImmortal()) { base.TakeDamage(damage); }
    }

    public override void DecreaseHealthBar(int damage)
    {
        if (!Cheats.Instance.isImmortal()) { base.DecreaseHealthBar(damage); }
    }
    public override void SelfDestroy()
    {
        GameManager.Instance.endGame();
    }

}
