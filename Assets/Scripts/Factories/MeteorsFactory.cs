using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MeteorsFactory : MonoBehaviour
{
    // reference to prefab
    [SerializeField]
    private GameObject[] meteorPrefabs;
    [SerializeField]
    private float delayMin;
    [SerializeField]
    private float delayMax;
    [SerializeField]
    private float _meteorRadiusMin;
    [SerializeField]
    private float _meteorRadiusMax;
    [SerializeField]
    private float _meteorBaseSpeed;

    private const float SPEED_MULTIPLIER = 1.0f;
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

        // choose meteor radius and speed
        float _meteorRadius = Random.Range(_meteorRadiusMin, _meteorRadiusMax);
        float _meteorSpeed = (_meteorRadiusMin == _meteorRadiusMax) ? _meteorBaseSpeed : calculateMeteorSpeed(_meteorRadius);

        //choose position for new spawn
        //horizontal
        float x = Random.Range(EnvironmentProps.Instance.minX() + _meteorRadius,
                               EnvironmentProps.Instance.maxX() - _meteorRadius);
        //vertical
        float z = EnvironmentProps.Instance.maxZ() + _meteorRadius;

        // create new instance of prefab at given position
        var meteorGO = Instantiate(meteorPrefabs[Random.Range(0, meteorPrefabs.Length-1)], 
                                   new Vector3(x, 0, z), Quaternion.identity);
        var meteorContr = meteorGO.GetComponentInChildren<MeteorController>();
        if (meteorContr != null)
        {
            meteorContr.Set(_meteorSpeed, _meteorRadius);
        }
        else
        {
            Debug.LogError("Missing MeteorController component");
        }

        // set new delay for next spawn
        _delay = Random.Range(delayMin, delayMax);
    }

    // calculates speed from base speed adjusted based on meteor radius
    private float calculateMeteorSpeed(float _meteorRadius) {
        float radiusRangeSize = _meteorRadiusMax - _meteorRadiusMin;
        float radiusPercentage = (_meteorRadius - _meteorRadiusMin) / radiusRangeSize;

        float speed = _meteorBaseSpeed - ((radiusPercentage - 0.5f) * _meteorBaseSpeed) * SPEED_MULTIPLIER;
        return speed;
    }

}
