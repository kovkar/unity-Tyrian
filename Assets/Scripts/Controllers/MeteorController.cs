using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    private float _speed = 20.0f;
    private float _radius = 1.0f;
    private Animation anim;
    private Rigidbody body;

    private void Start()
    {
        anim = this.gameObject.GetComponent<Animation>();
        body = this.gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    { 
         transform.position += new Vector3(0, 0, -_speed * Time.deltaTime);
    }

    // what can be hit by meteor defined by collision matrix
    public void OnCollisionEnter(Collision collision)
    {
        anim.Play();
        Destroy(body);
    }

    private void destroyAfterAnimation() { Destroy(this.gameObject); }

    // used by factory to set paramters
    public void Set(float speed, float radius)
    {
        _speed = speed;
        _radius = radius;
        transform.localScale = new Vector3(_radius, _radius, _radius);
    }
}
