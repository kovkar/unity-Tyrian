/*using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;

public class Boss : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float maxAccel = 5;

    [Header("Attack settings")]
    [SerializeField] private float powerAttackRange = 3;
    [SerializeField] private int powerAttackCooldown = 20;
    [SerializeField] private int cannonOverheatTime = 10;
    [SerializeField] private int cannonCooldownTime = 10;

    [Header("Refferences")]
    [SerializeField] private Transform seekTarget;
    [SerializeField] private Cannon longRangeCannon;

    private bool isCannonOverheated = false;
    private bool isPowerAttackReady = false;

    private Vector3 _velocity = Vector3.zero;
    private State activeState = State.WAITING;
    enum State
    {
        WAITING,
        SHORT_RANGE_ATTACK,
        LONG_RANGE_ATTACK,
        POWER_ATTACK,
    }

    private void Update()
    {
        activeState = UpdateState();

        switch (activeState) 
        {
            case State.SHORT_RANGE_ATTACK:
                Process_SHORT_RANGE_ATTACK();
                break;
            case State.LONG_RANGE_ATTACK:
                Process_LONG_RANGE_ATTACK();
                return;
            case State.POWER_ATTACK:
                Process_POWER_ATTACK();
                break;
            case State.WAITING:
                break;
        }
    }

    private State UpdateState()
    {
        switch (activeState) 
        {
            case State.WAITING:
                if (Input.anyKeyDown) { _ = CannonOverheatTimer(); return State.LONG_RANGE_ATTACK; }
                break;

            case State.SHORT_RANGE_ATTACK:
                if (isPowerAttackReady)  { _ = PowerAttackCooldown(); return State.POWER_ATTACK; }
                if (!isCannonOverheated) { _ = CannonOverheatTimer(); return State.LONG_RANGE_ATTACK; }
                break;

            case State.LONG_RANGE_ATTACK:
                if (isPowerAttackReady) { _ = PowerAttackCooldown(); return State.POWER_ATTACK; }
                if (isCannonOverheated) { _ = CannonOverheatTimer(); return State.SHORT_RANGE_ATTACK; }
                break;
        }
        return activeState;
    }

    private void Process_SHORT_RANGE_ATTACK()
    {
        longRangeCannon.enabled = false;
        transform.position = Seek();
    }

    private void Process_LONG_RANGE_ATTACK()
    {
        longRangeCannon.enabled = true;
        var pos = Seek();
        pos.z = EnvironmentProps.Instance.maxZ();
        transform.position = pos;
    }

    private void Process_POWER_ATTACK()
    {
        if (isPowerAttackActive) return;

        transform.position = Seek();
        if (Vector3.Distance(transform.position, seekTarget.position) <= powerAttackRange)
        {
            StartCoroutine(PowerAttack());
        }
    }


    private IEnumerator PowerAttack()
    {
        isPowerAttackActive = true;
        isPowerAttackReady = false;

        yield return new WaitForSeconds(1);
        
        isPowerAttackActive = false;
        StartCoroutine(PowerAttackCooldown());
    }

    private IEnumerator PowerAttackCooldown()
    {
        yield return new WaitForSeconds(powerAttackCooldown);
        isPowerAttackReady = true;
    }

    private async Task CannonOverheatTimer()
    {
        await Task.Delay(cannonOverheatTime * 1000);
        isCannonOverheated = true;
    }

    private async Task CannonCooldownTimer()
    {
        await Task.Delay(cannonCooldownTime * 1000);
        isCannonOverheated = false;
    }

    /// Sets 'isPowerAtackReady' to true after 'time' seconds.
    private async Task PowerAttackTimer(int time)
    {
        await Task.Delay(time * 1000);
        isPowerAttackReady = true;
    }


    private IEnumerator CannonCooldown()
    {
        yield return new WaitForSeconds(cannonCooldownTime);
        isCannonOverheated = false;
    }

    private Vector3 Seek()
    {
        _velocity = GameUtils.Instance.ComputeSeekVelocity(transform.position, _velocity, maxSpeed, maxAccel, seekTarget.position, Time.deltaTime);
        var pos = GameUtils.Instance.ComputeEulerStep(transform.position, _velocity, Time.deltaTime);
        return EnvironmentProps.Instance.IntoArea(pos, 0, 0);
    }
}
*/