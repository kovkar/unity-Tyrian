using UnityEngine;

public class BossActor : Actor
{
    [Header("Boss settings")]
    public float maxSpeed = 8;
    public float maxAccel = 25;

    [Header("Refferences to set in inspector")]
    public Transform seekTargetTransform;

    private Vector3 _velocity = Vector3.zero;

    private BoxCollider collider;

    // **************** UNITY METHODS **************** //

    void Awake()
    {
        collider = GetComponent<BoxCollider>();
    }


    private void Update()
    {
        _velocity = GameUtils.Instance.ComputeSeekVelocity(transform.position, _velocity, maxSpeed, maxAccel, seekTargetTransform.position, Time.deltaTime);
        Vector3 pos = GameUtils.Instance.ComputeEulerStep(transform.position, _velocity, Time.deltaTime);
        transform.position = EnvironmentProps.Instance.IntoArea(pos, 0.5f * collider.size.x, 0.5f * collider.size.z);
    }
}
