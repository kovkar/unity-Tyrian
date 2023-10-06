using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipContoller : MonoBehaviour
{
    public float speed;

    public Bounds shipArea;

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

        transform.position = this.ClipIntoShipArea(pos);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
        Debug.LogWarning("GAME OVER: killed by " + collision.gameObject.name);
    }

    private Vector3 ClipIntoShipArea(Vector3 pos)
    {
        Vector3 result = pos;
        float dx = transform.localScale.x;
        float dz = transform.localScale.y;

        if      (result.x - dx < shipArea.min.x) { result.x = shipArea.min.x + dx; }
        else if (result.x + dx > shipArea.max.x) { result.x = shipArea.max.x - dx; }

        if      (result.z - dz < shipArea.min.z) { result.z = shipArea.min.z + dz; }
        else if (result.z + dz > shipArea.max.z) { result.z = shipArea.max.z - dz; }

        return result;
    }
}
