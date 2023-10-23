using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private int movingDirectionX = -1;

    private GameObject body;

    private void Start()
    {
        var child = this.transform.Find("Body");
        if (child != null) { this.body = child.GameObject(); }
        else { Debug.LogWarning(this.gameObject.name + "does not have child with name 'Body'."); }
    }

    void Update()
    {
        checkDirectionChange();
        this.transform.position += new Vector3(speed * movingDirectionX * Time.deltaTime, 0, 0);
    }

    // Chcecks if enemy is touching x-axis play area edge
    private void checkDirectionChange() 
    {
        float minX = EnvironmentProps.Instance.minX();
        float maxX = EnvironmentProps.Instance.maxX();
        Vector3 pos = this.transform.position;

        if (pos.x >= maxX | pos.x <= minX)
        {
            movingDirectionX *= -1;                         // change its x direction
            pos.z -= this.transform.localScale.z + 0.2f;    // move one row closer to the ship
            pos.x = (pos.x <= minX) ? minX : maxX;          // clip x into play area
            this.transform.position = pos;
        }
    }
}
