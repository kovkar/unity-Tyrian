using UnityEngine;

/// <summary>
///  Subclass of <c>Actor</c> representing meteors. 
///  Overrides <name>SelfDestroy</name> method to play audio and animation on destroy.
/// </summary>
public class MeteorActor : Actor
{
    private AudioSource explosionSound;
    private Animation destroyAnimation;
    private ParticleSystem smokeTrail;
    private Rigidbody rb;


    // **************** PUBLIC METHODS **************** //

    /// <summary>
    /// Method called at the end of the destroy animation.  
    /// </summary>
    public void OnDestroyAnimEnds() { Destroy(this); }

    // **************** UNITY METHODS **************** //

    private void Awake()
    {
        destroyAnimation = GetComponentInChildren<Animation>();
        smokeTrail = GetComponentInChildren<ParticleSystem>();
        explosionSound = GetComponent<AudioSource>();
        rb = GetComponentInChildren<Rigidbody>();

        if (destroyAnimation is null)
            Debug.LogWarning($"Animation component not found in {gameObject.name} or its children!");
        if (smokeTrail is null)
            Debug.LogWarning($"ParticleSystem  not found in {gameObject.name} or its children!");
        if (explosionSound is null)
            Debug.LogWarning($"AudioSource component not found in {gameObject.name} or its children!");
        if (rb is null)
            Debug.LogWarning($"Rigidbody component not found in {gameObject.name} or its children!");
    } 

    // **************** OVERRIDE METHODS **************** //

    protected override void SelfDestroy()
    {
        if (rb is not null) Destroy(rb);                
        smokeTrail?.Stop();   
        explosionSound?.Play();
        destroyAnimation?.Play();
    }
}
