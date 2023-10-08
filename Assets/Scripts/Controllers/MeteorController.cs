using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    private float _speed = 20.0f;
    private float _radius = 1.0f;

    private void Start()
    { 

    }

    void Update()
    { 
         transform.position += new Vector3(0, 0, -_speed * Time.deltaTime);
    }

    // destroy meteor on hit, what can be hit by meteor defined by collision matrix
    public void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    // used by factory to set paramters
    public void Set(float speed, float radius)
    {
        _speed = speed;
        _radius = radius;
        transform.localScale = new Vector3(_radius, _radius, _radius);
    }
}
