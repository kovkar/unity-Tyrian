using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    [SerializeField]
    float speed;

    void Start() {}

    void Update()
    {
        transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
    }
}
