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

    public static EnemiesFacotry Instance { get; private set; }

    public void Awake()
    {
        // Check, if we do not have any instance yet.
        if (Instance == null)
        {
            // 'this' is the first instance created => save it.
            Instance = this;
        }
        else if (Instance != this)
        {
            // Destroy 'this' object as there exist another instance
            Destroy(this.gameObject);
        }
    }

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

    public void decreaseEnemyCountByOne() { enemiesCount -= 1; }
}
