using System.Collections;
using UnityEngine;

/// <summary>
/// Gun subclass shooting burst of <c>burstCount</c> projectiles every <c>burstDelay</c> seconds if collision is detected.
/// </summary>
public class BurstCannon : Cannon
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
    private Transform target;

    /// refference to running BurstShooting() corutine
    private IEnumerator shooting;


    // **************** UNITY **************** //

    private void Start()
    {
        target = GameManager.Instance.Ship;
        shooting = BurstShooting();
        StartCoroutine(shooting);
    }

    private void OnEnable()
    {
        shooting = BurstShooting();
        StartCoroutine(shooting);
    }

    private void OnDisable()
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
                Shoot(projSpeed, Vector3.Normalize(target.position - transform.position));
                yield return new WaitForSeconds(1.0f / burstCadence);
            }
            yield return new WaitForSeconds(burstDelay);
        }
    }
}
