using System.Collections;
using UnityEngine;

/// <summary>
/// Gun subclass calling shoot method in random intervals.
/// </summary>
public class RandomCannon : Cannon
{
    [Header("Random cannon")]
    /// <value>minDelay</value> minimal delay between consecutive shots
    [SerializeField] private float minDelay = 1.0f;
    /// <value>maxDelay</value> maximal delay between consecutive shots
    [SerializeField] private float maxDelay = 2.0f;
    /// <value>projSpeed</value> Speed of projectiles.
    [SerializeField] private float projSpeed = 5.0f;
    /// <value>direction</value> shooting direction of gun
    [SerializeField] private Vector3 direction = new Vector3(0, 0, -1);


    // **************** UNITY **************** //

    private void Start()
    {
        StartCoroutine(Shooting());
    }


    // **************** PRIVATE **************** //

    /// <summary>
    /// Corutine for shooting projectiles.
    /// </summary>
    private IEnumerator Shooting()
    {
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        while (true)
        {
            Shoot(projSpeed, direction);
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }
    }
}
