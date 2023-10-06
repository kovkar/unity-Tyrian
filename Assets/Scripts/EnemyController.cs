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
        // start moving towards border further away
        movingDirectionX = (this.transform.position.x > EnvironmentProps.Instance.playAreaBounds.center.x) ? -1 : 1;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(_speed * movingDirectionX * Time.deltaTime, 0, 0);
        directionChangeCheck();
    }

    void directionChangeCheck()
    {
        Vector3 pos = this.transform.position;
        if (pos.x >= EnvironmentProps.Instance.maxX() | pos.x <= EnvironmentProps.Instance.minX()) {
            movingDirectionX *= -1;
            this.transform.position -= new Vector3(0,0,this.transform.localScale.z);
        }
    }
}
