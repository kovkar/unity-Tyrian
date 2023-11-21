using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorActor : Actor
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void SelfDestroy()
    {
        Animation anim = GetComponentInChildren<Animation>();
        Rigidbody body = GetComponentInChildren<Rigidbody>();
        ParticleSystem smoke = GetComponentInChildren<ParticleSystem>();

        if (anim == null)  { Debug.LogError("Animation component not found in " + this.gameObject.name + " or its children!"); return; }
        if (body == null)  { Debug.LogError("Rigidbody component not found in " + this.gameObject.name + " or its children!"); return; }
        if (smoke == null) { Debug.LogError("ParticleSystem named 'Smoke' not found in " + this.gameObject.name + " or its children!"); return; }

        Destroy(body);  // destroy rigidbody so it doesnt cause collision during animation
        smoke.Stop();   // stop smoke trail
        anim.Play();    // play animation
        audioSource.Play();
    }

    public void ExpandAndDisolveAnimationEnded() { Destroy(this); }
}
