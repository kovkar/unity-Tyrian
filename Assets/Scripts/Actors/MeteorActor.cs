using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorActor : Actor
{
    public override void SelfDestroy()
    {
        Animation anim = GetComponentInChildren<Animation>();
        Rigidbody body = GetComponentInChildren<Rigidbody>();
        if (anim == null) { Debug.LogError("Animation component not found in " + this.gameObject.name + " or its children!"); return; }
        if (body == null) { Debug.LogError("Animation component not found in " + this.gameObject.name + " or its children!"); return; }
        
        anim.Play();    // play animation
        Destroy(body);  // destroy rigidbody so it doesnt cause collision during animation
    }

    public void ExpandAndDisolveAnimationEnded() { Destroy(this); }
}
