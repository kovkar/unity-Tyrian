using UnityEngine;

/// <summary>
/// Class controlling boss movement based on its inner <c>State</c>.
/// </summary>
public class BossController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float maxAccel = 10;
    [SerializeField] private float powerSpeed = 10;
    [SerializeField] private float powerAccel = 10;

    [Header("Refferences")]
    [SerializeField] private Transform target;

    private Vector3 _velocity = Vector3.zero;
    private State _state = State.SEEK;


    // **************** PUBLIC **************** //

    /// <summary>
    /// Enum of states defining boss movement.
    /// </summary>
    public enum State
    {
        IDLE,
        SEEK,
        LONG_RANGE_SEEK,
        POWER_SEEK
    }


    /// <summary>
    /// Changes movement state.
    /// </summary>
    /// <param name="state">state to set</param>
    public void ChangeState(State state)
    {
        _state = state;
    }


    // **************** UNITY **************** //

    private void Update()
    {
        if (_state == State.IDLE) return;

        var seekPos = target.position;
        if (_state == State.LONG_RANGE_SEEK) seekPos.z = EnvironmentProps.Instance.maxZ();

        var speed = (_state == State.POWER_SEEK) ? powerSpeed : maxSpeed;
        var accel = (_state == State.POWER_SEEK) ? powerAccel : maxAccel;

        _velocity = GameUtils.Instance.ComputeSeekVelocity(transform.position, _velocity, speed, accel, seekPos, Time.deltaTime);
        var new_pos = GameUtils.Instance.ComputeEulerStep(transform.position, _velocity, Time.deltaTime);

        transform.position = EnvironmentProps.Instance.IntoArea(new_pos, 0, 0);
    }
}
