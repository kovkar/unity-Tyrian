using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class RandomMeteorsFactory : MonoBehaviour
{
    // reference to prefab
    [SerializeField]
    private GameObject meteorPrefab;
    [SerializeField]
    private float delayMin;
    [SerializeField]
    private float delayMax;
    [SerializeField]
    private float _meteorSpeed;
    [SerializeField]
    private float radiusMin;
    [SerializeField]
    private float radiusMax;
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

        float meteorRadius = Random.Range(radiusMin, radiusMax);

        //horizontal
        float x = Random.Range(EnvironmentProps.Instance.minX() + meteorRadius,
                               EnvironmentProps.Instance.maxX() - meteorRadius);
        //vertical
        float z = EnvironmentProps.Instance.maxZ() + meteorRadius;

        // create new instance of prefab at given position
        var meteorGO = Instantiate(meteorPrefab, new Vector3(x, 0, z), Quaternion.identity);
        var meteorContr = meteorGO.GetComponent<MeteorController>();
        if (meteorContr != null)
        {
            meteorContr.Set(_meteorSpeed, meteorRadius);
        }
        else
        {
            Debug.LogError("Missing MeteorController component");
        }

        // set new delay for next spawn
        _delay = Random.Range(delayMin, delayMax);
    }
}
