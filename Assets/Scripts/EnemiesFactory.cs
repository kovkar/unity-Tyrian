using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemiesFacotry : MonoBehaviour
{
    // reference to prefab
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float spawnDelay;
    [SerializeField]
    private float maxEnemies;

    // delay from last spawn
    private float _spawnDelay;

    private int enemiesCount;

    // Start is called before the first frame update
    void Start()
    {
        _spawnDelay = 0;
        enemiesCount= 0;
    }

    // Update is called once per frame
    void Update()
    {
        _spawnDelay -= Time.deltaTime;

        if (_spawnDelay > 0.0f | enemiesCount >= maxEnemies) { return; }

        float x = Random.Range(EnvironmentProps.Instance.minX() + enemyPrefab.transform.localScale.x / 2 ,
                               EnvironmentProps.Instance.maxX() - enemyPrefab.transform.localScale.x / 2) ;

        float z = EnvironmentProps.Instance.maxZ() + enemyPrefab.transform.localScale.z / 2;

        Instantiate(enemyPrefab, new Vector3(x, 0, z), Quaternion.identity);
        enemiesCount += 1;

        _spawnDelay = spawnDelay;
    }
}
