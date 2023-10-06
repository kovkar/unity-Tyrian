using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefab : MonoBehaviour
{
    [SerializeField]
    float _speed;

    private int movingDirectionX;

    // Start is called before the first frame update
    void Start()
    {
        // start moving towards closer further away
        movingDirectionX = (this.transform.position.x > EnvironmentProps.Instance.playAreaBounds.center.x) ? 1 : -1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;
        if (pos.x >= EnvironmentProps.Instance.maxX())
        {
            pos.x = EnvironmentProps.Instance.maxX();
            pos.z -= this.transform.localScale.z;
            movingDirectionX *= -1;
        }
        else if (pos.x <= EnvironmentProps.Instance.minX())
        {
            pos.x = EnvironmentProps.Instance.minX();
            pos.z -= this.transform.localScale.z;
            movingDirectionX *= -1;
        } 
        pos.x += _speed * movingDirectionX * Time.deltaTime;

        this.transform.position = pos;
    }

    void OnTriggerExit(Collider other)
    {
        // Destroy meteors that leaves the play area
        if (other.gameObject == EnvironmentProps.Instance.gameObject) {
            EnemiesFacotry.Instance.decreaseEnemyCountByOne();
            Destroy(this.gameObject);
        }
    }
}
