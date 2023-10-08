using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    [SerializeField]
    float _speed;

    void Start() {}

    void Update()
    {
        transform.position -= new Vector3(0, 0, _speed * Time.deltaTime);
    }

    // destroy projectile on hit, what can be hit by projectile defined by collision matrix
    public void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
