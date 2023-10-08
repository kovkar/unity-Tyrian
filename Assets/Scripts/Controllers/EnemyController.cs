using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefab : MonoBehaviour
{
    private float _speed = 10.0f;

    private int _movingDirectionX;

    void Start()
    {
        // initial x moving direction towards closer edge
        _movingDirectionX = (this.transform.position.x > EnvironmentProps.Instance.playAreaBounds.center.x) ? 1 : -1;
    }

    void Update()
    {
        checkDirectionChange();
        this.transform.position += new Vector3(_speed * _movingDirectionX * Time.deltaTime, 0, 0);
    }

    // destroy enemy on hit, what can be hit by enemy defined by collision matrix
    public void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    // Chcecks if enemy is touching x-axis play area edge
    private void checkDirectionChange() 
    {
        float minX = EnvironmentProps.Instance.minX();
        float maxX = EnvironmentProps.Instance.maxX();
        Vector3 pos = this.transform.position;

        if (pos.x >= maxX | pos.x <= minX)
        {
            _movingDirectionX *= -1;                        // change its x direction
            pos.z -= this.transform.localScale.z + 0.2f;    // move one row closer to the ship
            pos.x = (pos.x <= minX) ? minX : maxX;          // clip x into play area
            this.transform.position = pos;
        }
    }

    // used by factory to set paramters
    public void Set(float speed, float radius) { _speed = speed; }
}
