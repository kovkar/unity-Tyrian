using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipContoller : MonoBehaviour
{
    public float speed;

    private CapsuleCollider collider;

    void Awake()
    {
        collider = GetComponent<CapsuleCollider>();
        //here should be some null check...
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            pos.z += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos.z -= speed * Time.deltaTime;
        }

        // limit movement of the object to an area defined in env. properties
        pos = EnvironmentProps.Instance.IntoArea(pos, collider.radius);

        transform.position = pos;
    }
}
