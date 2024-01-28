using Unity.VisualScripting;
using UnityEngine;

public class BossThrusterContoller : MonoBehaviour
{
    [Header("Refferences")]
    [SerializeField] Boss boss;

    void Update()
    {
        if (boss == null || boss._velocity == Vector3.zero) return;
        transform.rotation = Quaternion.LookRotation(boss._velocity);
    }
}
