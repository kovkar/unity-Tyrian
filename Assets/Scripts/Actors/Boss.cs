/*using UnityEngine;

public class Boss : Actor
{
    private enum State
    {
        ARRIVE,
        LONG_RANGE_ATTACK,
        SHORT_RANGE_ATTACK
    }

    [Header("Refferences to set in inspector!")]
    public Transform target;

    [Header("Movement settings")]
    public float maxSpeed = 8;
    public float maxAccel = 25;

    private State _activeState;
    private Vector3 _velocity = Vector3.zero;
    private bool isTooCloseToTarget = false;

    private BoxCollider collider;

    // **************** UNITY METHODS **************** //

    void Awake()
    {
        collider = GetComponent<BoxCollider>();
    }


    private void Update()
    {
        var utils = GameUtils.Instance;

        _velocity = utils.ComputeSeekVelocity(transform.position, _velocity, maxSpeed, maxAccel, target.position, Time.deltaTime);
        var pos = utils.ComputeEulerStep(transform.position, _velocity, Time.deltaTime);

        switch (_activeState)
        {
            case State.SHORT_RANGE_ATTACK:
                transform.position = EnvironmentProps.Instance.IntoArea(pos, collider.size.x, collider.size.z);
                break;
        }


        pos = EnvironmentProps.Instance.IntoArea(pos, collider.size.x, collider.size.y);
    }

    // **************** PRIVATE METHODS **************** //

    private void ShortRangeAttack() 
    {
        var currentPos = transform.position;
        var targetPos  = seekTargetTransform.position;
        var targetDir  = targetPos - currentPos;
        var targetDist = targetDir.magnitude;

        var seekPos = currentPos + targetDir.normalized * (targetDist - minDistShortRange);

        

        var newPos = transform.position = GameUtils.Instance.ComputeEulerStep(currentPos, 
        _velocity, Time.deltaTime);
        transform.position = EnvironmentProps.Instance.IntoArea(newPos, collider.size.x, collider.size.y);

    }

    private void LongRangeAttack()
    {

    }
}
*/