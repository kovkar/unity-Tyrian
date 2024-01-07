using UnityEngine;

/// <summary>
/// Class controlling boss movement based on its inner <c>State</c>.
/// </summary>
public class BossController : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxAccel;
    [SerializeField] private float minTargetDist;

    [SerializeField] private Transform target;

    private State _state = State.LONG_RANGE_ATTACK;
    private Vector3 _velocity = Vector3.zero;


    // **************** PUBLIC **************** //

    /// <summary>
    /// Enum of states defining boss movement.
    /// </summary>
    public enum State
    {
        IDLE,
        LONG_RANGE_ATTACK,
        SHORT_RANGE_ATTACK
    }

    /// <summary>
    /// Sets boss movement <c>State</c>.
    /// </summary>
    /// <param name="state">state to set</param>
    public void SetState(State state) { _state = state; }


    // **************** UNITY **************** //

    private void Update()
    {
        var seekPos = transform.position;

        switch (_state)
        {
            case State.SHORT_RANGE_ATTACK: seekPos = target.position; break;
            case State.LONG_RANGE_ATTACK:  seekPos = new Vector3(target.position.x, target.position.y, EnvironmentProps.Instance.maxZ()); break;
        }

        _velocity = GameUtils.Instance.ComputeSeekVelocity(transform.position, _velocity, maxSpeed, maxAccel, seekPos, Time.deltaTime);
        var new_pos = GameUtils.Instance.ComputeEulerStep(transform.position, _velocity, Time.deltaTime);
        transform.position = EnvironmentProps.Instance.IntoArea(new_pos, minTargetDist, minTargetDist);
    }
}
