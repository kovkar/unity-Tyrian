using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActor : Actor
{
    private const int PLAYER_SHIP_LAYER = 6;

    public override void OnCollisionEnter(Collision collision)
    {
        this.updateCurrencies(collision);
        base.OnCollisionEnter(collision);
    }

    public override void SelfDestroy()
    {
        EnemiesFacotry.Instance.decreaseEnemyCountByOne();
        base.SelfDestroy();
    }

    private void updateCurrencies(Collision collision)
    {
        if (Currencies.Instance == null) { return; }

        GameObject otherGO = collision.gameObject;
        Actor otherActor = otherGO.GetComponent<Actor>();

        if (otherActor == null) { return; }

        int healthTaken = otherActor.getDamage();
        bool isCrash = otherGO.gameObject.layer == PLAYER_SHIP_LAYER;
        bool isKill = healthTaken >= this.getHealth();

        Currencies.Instance.OnCollisionCurrencies(healthTaken, isCrash, isKill);
    }
}
