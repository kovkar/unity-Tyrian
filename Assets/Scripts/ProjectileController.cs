using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private float _speed;
    private float _radius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move projectile down
        transform.position += new Vector3(0, 0, _speed * Time.deltaTime);
    }

    // destroy everything hitted by projectile,
    // what can be hitted defined by collision matrix
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

    // used by factory to set paramters
    public void Set(float speed, float radius)
    {
        _speed = speed;
        _radius = radius;
        transform.localScale = new Vector3(_radius, _radius, _radius);
    }

}
