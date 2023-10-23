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
    private float _meteorRadius;
    [SerializeField]
    private float _meteorSpeed;
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

        //choose position for new spawn
        //horizontal
        float x = Random.Range(EnvironmentProps.Instance.minX() + _meteorRadius,
                               EnvironmentProps.Instance.maxX() - _meteorRadius);
        //vertical
        float z = EnvironmentProps.Instance.maxZ() + _meteorRadius;

        // create new instance of prefab at given position
        var meteorGO = Instantiate(meteorPrefabs[Random.Range(0, meteorPrefabs.Length)], 
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

}
