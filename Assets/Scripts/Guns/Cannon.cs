using UnityEngine;

/// <summary>
/// Basic gun class for instantiating projectiles.
/// </summary>
public class Cannon : MonoBehaviour
{
    [Header("Refferences")]
    /// <value>projectile</value> projectile to be instancieated
    [SerializeField] private ProjectileController projectile;
    /// <value>shotSound</value> audio too be played when shooting
    [SerializeField] private AudioSource shotSound;


    // **************** PUBLIC **************** //

    /// <summary>
    /// Instantiates <c>projectile</c> and sets its parameters.
    /// </summary>
    /// <param name="speed">projectile speed</param>
    /// <param name="direction">projectile direction</param>
    public void Shoot(float speed, Vector3 direction)
    {
        // spawn
        var projGO = Instantiate(projectile, transform.position, Quaternion.identity);
        // set
        (projGO as ProjectileController)?.Set(speed, direction);
        // play audio if provided
        if (shotSound) shotSound.PlayOneShot(shotSound.clip);
    }
}
