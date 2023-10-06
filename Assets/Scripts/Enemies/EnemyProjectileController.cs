using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    [SerializeField]
    float _speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0, 0, _speed * Time.deltaTime);
    }

    // destroy everything hitted by projectile,
    // what can be hitted defined by collision matrix
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
