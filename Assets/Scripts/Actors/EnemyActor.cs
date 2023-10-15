using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActor : Actor
{
    public override void SelfDestroy()
    {
        EnemiesFacotry.Instance.decreaseEnemyCountByOne();
        base.SelfDestroy();
    }
}
