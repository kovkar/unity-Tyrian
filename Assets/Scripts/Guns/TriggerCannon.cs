using System.Collections;
using UnityEngine;

/// <summary>
/// Gun subclass shooting burst of <c>burstCount</c> projectiles every <c>burstDelay</c> seconds if collision is detected.
/// </summary>
public class TriggerCannon : Cannon
{
    [Header("Collider cannon")]
    /// <value>burstCount</value> How many projectiles per burst will be fired.
    [SerializeField] private int burstCount = 1;
    /// <value>cadence</value> Delay between two consecutive bursts.
    [SerializeField] private float burstDelay = 1;
    /// <value>burstDelay</value> Delay between projectiles in one burst.
    [SerializeField] private float burstCadence = 1;
    /// <value>projSpeed</value> Speed of projectiles.
    [SerializeField] private float projSpeed = 5.0f;
    /// <value>firstShotDelay</value> Delay before first projectile is fired after <c>shootKey</c> is pressed.
    [SerializeField] private float firstShotDelay = 1;
    /// <value>direction</value> shooting direction of gun
    [SerializeField] private Vector3 direction = new Vector3(0, 0, -1);

    /// refference to running BurstShooting() corutine
    private IEnumerator shooting;


    // **************** UNITY **************** //

    private void OnTriggerEnter(Collider other)
    {
        shooting = BurstShooting();
        StartCoroutine(shooting);
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(shooting);
    }


    // **************** PRIVATE **************** //

    /// <summary>
    /// Corutine for shooting bursts of projectiles.
    /// </summary>
    private IEnumerator BurstShooting()
    {
        yield return new WaitForSeconds(firstShotDelay);
        while (true)
        {
            for (int _ = 0; _ < burstCount; _++)
            {
                Shoot(projSpeed, direction);
                yield return new WaitForSeconds(1.0f / burstCadence);
            }
            yield return new WaitForSeconds(burstDelay);
        }
    }
}
