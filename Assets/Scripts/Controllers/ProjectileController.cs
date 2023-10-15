using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private float _speed;
    private float _radius;

    void Update()
    {
        transform.position += new Vector3(0, 0, _speed * Time.deltaTime);
    }

    // used by gun to set paramters
    public void Set(float speed, float radius)
    {
        _speed = speed;
        _radius = radius;
        transform.localScale = new Vector3(_radius, _radius, _radius);
    }

}
