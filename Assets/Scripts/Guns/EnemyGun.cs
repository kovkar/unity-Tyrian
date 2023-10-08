using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    // reference to prefab
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float delayMin;
    [SerializeField]
    private float delayMax;

    // delay from last spawn
    private float _delay;

    void Start()
    {
        _delay = Random.Range(delayMin, delayMax);
    }

    void Update()
    {
        _delay -= Time.deltaTime;

        if (_delay > 0.0f)
            return;

        Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        _delay = Random.Range(delayMin, delayMax);
    }
}
