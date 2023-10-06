using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnvironmentProps : MonoBehaviour
{
    public static EnvironmentProps Instance { get; private set; }

    public Bounds playAreaBounds;

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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public float minX() { return playAreaBounds.min.x; }
    public float maxX() { return playAreaBounds.max.x; }
    public float minZ() { return playAreaBounds.min.z; }
    public float maxZ() { return playAreaBounds.max.z; }

    public Vector3 IntoArea(Vector3 pos, float dx)
    {
        Vector3 result = pos;
        result.x = result.x - dx < minX() ? minX() + dx : result.x;
        result.x = result.x + dx > maxX() ? maxX() - dx : result.x;
        return result;
    }

    public bool EscapedBelow(Vector3 pos, float dz)
    {
        return pos.z + dz < minZ();
    }

    public bool EscapedFromTop(Vector3 pos, float dz)
    {
        return pos.z - dz > maxZ();

    }
}
