using System;
using UnityEngine;

public class EnvironmentProps : MonoBehaviour
{

    public Bounds playAreaBounds;
    public static EnvironmentProps Instance { get; private set; }

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
    void Start() {}

    // Update is called once per frame
    void Update() {}

    public Vector3 IntoArea(Vector3 pos, float sizeX, float sizeZ)
    {
        pos.x = Math.Clamp(pos.x, minX() + sizeX * 0.5f, maxX() - sizeX * 0.5f);
        pos.z = Math.Clamp(pos.z, minZ() + sizeZ * 0.5f, maxZ() - sizeZ * 0.5f);
        return pos;
    }


    // Destroy meteors that leaves the play area
    void OnTriggerExit(Collider other) { Destroy(other.gameObject); }

    public float minX() { return playAreaBounds.min.x; }
    public float maxX() { return playAreaBounds.max.x; }
    public float minZ() { return playAreaBounds.min.z; }
    public float maxZ() { return playAreaBounds.max.z; }
}
