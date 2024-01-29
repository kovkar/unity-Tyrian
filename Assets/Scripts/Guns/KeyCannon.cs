using System.Collections;
using UnityEngine;

/// <summary>
/// Gun subclass calling shoot method <c>cadence</c> times
/// per second if <c>shootKey</c> is pressed.
/// </summary>
public class KeyCannon : Cannon
{
    [Header("Player cannon")]
    /// <value>cadence</value> How many projectiles per second will be fired.
    [SerializeField] private float cadence = 1;
    /// <value>projSpeed</value> Speed of projectiles.
    [SerializeField] private float projSpeed = 5.0f;
    /// <value>firstShotDelay</value> Delay before first projectile is fired after <c>shootKey</c> is pressed.
    [SerializeField] private float firstShotDelay = 1;
    /// <value>shootKey</value> Input key triggering shooting.
    [SerializeField] private KeyCode shootKey = KeyCode.Space;
    /// <value>direction</value> shooting direction of gun
    [SerializeField] private Vector3 direction = new Vector3(0, 0, 1);

    /// storing running Shoot() corutine
    private IEnumerator shooting;


    // **************** UNITY **************** //

    private void Update()
    {
        if (Input.GetKeyDown(shootKey))
        {
            shooting = Shooting();
            StartCoroutine(shooting);
        }
        if (Input.GetKeyUp(shootKey)) StopCoroutine(shooting);
    }


    // **************** PRIVATE **************** //

    /// <summary>
    /// Corutine for shooting projectiles.
    /// </summary>
    private IEnumerator Shooting()
    {
        yield return new WaitForSeconds(firstShotDelay);
        while (true)
        {
            Shoot(projSpeed, direction);
            yield return new WaitForSeconds(1.0f / cadence);
        }
    }
}
