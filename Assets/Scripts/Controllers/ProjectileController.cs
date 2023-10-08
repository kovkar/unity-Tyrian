using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private float _speed;
    private float _radius;

    void Start() {}

    void Update()
    {
        transform.position += new Vector3(0, 0, _speed * Time.deltaTime);
    }

    // destroy projectile on hit, what can be hit by projectile defined by collision matrix
    public void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    // used by gun to set paramters
    public void Set(float speed, float radius)
    {
        _speed = speed;
        _radius = radius;
        transform.localScale = new Vector3(_radius, _radius, _radius);
    }

}
