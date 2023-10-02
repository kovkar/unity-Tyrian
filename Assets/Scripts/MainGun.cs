using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MainGun : MonoBehaviour
{
    // reference to prefab
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float delay;
    [SerializeField]
    private float _projectileRadius;
    [SerializeField]
    private float _projectileSpeed;
    // delay from last spawn
    private float _delay;

    // Start is called before the first frame update
    void Start()
    {
        _delay = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // time elapsed from previous frame
        _delay -= Time.deltaTime;

        if (_delay > 0.0f)
            return;

        // create new instance of prefab at given position
        var projectileGO = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        var projectileContr = projectileGO.GetComponent<ProjectileController>();
        if (projectileContr != null)
        {
            projectileContr.Set(_projectileSpeed, _projectileRadius);
        }
        else
        {
            Debug.LogError("Missing ProjectilCOntroller component");
        }

        // set new delay for next spawn
        _delay = delay;
    }
}
