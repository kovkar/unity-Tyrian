using System;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
///  Subclass of <c>Actor</c> representing player ship:
/// </summary>
public class PlayerActor : Actor
{
    private void Start()
    {
        healthBar = GameManager.Instance.playerHealthbar;
    }

    private void OnCollisionStay(UnityEngine.Collision collision)
    {
        BossActor boss = collision.gameObject.GetComponent<BossActor>();
        if (boss != null) TakeDamage(boss.getDamage() * Time.deltaTime);
    }

    // **************** OVVERIDE METHODS **************** //
    protected override void TakeDamage(float damage)
    {
        if (!Cheats.Instance.isImmortal()) { base.TakeDamage(damage); }
    }

    protected override void SelfDestroy()
    {
        GameManager.Instance.EndGame(false);
    }

}
