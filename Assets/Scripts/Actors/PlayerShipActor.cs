using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipActor : Actor
{
    public override void SelfDestroy()
    {
        GameManager.Instance.endGame();
    }

}
