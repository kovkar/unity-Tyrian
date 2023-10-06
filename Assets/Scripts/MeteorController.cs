using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    private float _speed = 20.0f;
    private float _radius = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
         transform.position += new Vector3(0, 0, -_speed * Time.deltaTime);
    }

    void OnTriggerExit(Collider other)
    {
        // Destroy meteors that leaves the play area
        if (other.gameObject == EnvironmentProps.Instance.gameObject) { Destroy(this.gameObject); }
    }

    // used by factory to set paramters
    public void Set(float speed, float radius)
    {
        _speed = speed;
        _radius = radius;
        transform.localScale = new Vector3(_radius, _radius, _radius);
    }

}
