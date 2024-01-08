using System.Collections;
using System.Data;
using System.Threading.Tasks;
using UnityEngine;

public class AsyncBos : MonoBehaviour
{
    [Header("Power attack settings")]
    [SerializeField] private float powerSpeed;
    [SerializeField] private float powerAccel;
    [SerializeField] private int powerCooldown;
    [SerializeField] private float powerAttackRange;

    [Header("Primary (long range) attack settings")]
    [SerializeField] private float primarySpeed;
    [SerializeField] private float primaryAccel;
    [SerializeField] private int timeToOverheat;
    [SerializeField] private int primaryCooldown;

    [Header("Secondary (close range) attack settings")]
    [SerializeField] private float shortSpeed;
    [SerializeField] private float shortAccel;

    [Header("Refferences!")]
    [SerializeField] private Cannon primaryCannon;
    [SerializeField] private Cannon secondaryCannon;
    [SerializeField] private Cannon powerCannon;
    [SerializeField] private Transform seekTarget;

    private State currentState = State.IDLE;
    private Vector3 velocity = Vector3.zero;
    private bool isPrimaryCannonReady = false;

    public enum State
    {
        IDLE,
        SECONDARY_ATTACK,   
        PRIMARY_ATTACK,     
        POWER_ATTACK_SEEK,
        POWER_ATTACK,
    }

    private enum Trigger
    {
        PRIMARY_CANNON_OVERHEATED,
        PRIMARY_CANNON_READY,
        POWER_ATTACK_FINISHED,
        POWER_ATTACK_READY
    }


    // **************** UNITY *************** //

    private void Start()
    {
        _ = TriggerTimer(Trigger.POWER_ATTACK_READY, powerCooldown);
        SwitchToState(State.PRIMARY_ATTACK);
    }

    // updates position based on state
    private void Update()
    {
        Vector3 seekPos; float seekSpeed; float seekAccel;

        switch (currentState)
        {
            case State.SECONDARY_ATTACK:
                seekPos = seekTarget.position;
                seekSpeed = shortSpeed;
                seekAccel = shortAccel;
                break;

            case State.PRIMARY_ATTACK:
                seekPos = seekTarget.position;
                seekPos.z = EnvironmentProps.Instance.maxZ();
                seekSpeed = primarySpeed;
                seekAccel = primaryAccel;
                break;

            case State.POWER_ATTACK_SEEK:
                seekPos = seekTarget.position;
                seekSpeed = powerSpeed;
                seekAccel = powerAccel;

                if (Vector3.Distance(transform.position, seekTarget.position) < powerAttackRange)
                {
                    SwitchToState(State.POWER_ATTACK);
                }
                break;

            default: return;
        }

        // compute velocity
        velocity = GameUtils.Instance.ComputeSeekVelocity(transform.position, velocity, seekSpeed, seekAccel, seekPos, Time.deltaTime);
        // compute new position
        var new_pos = GameUtils.Instance.ComputeEulerStep(transform.position, velocity, Time.deltaTime);
        // clip into play area
        transform.position = EnvironmentProps.Instance.IntoArea(new_pos, 0, 0);
    }


    // **************** PRIVATE **************** //

    private async Task TriggerTimer(Trigger trigger, int time)
    {
        await Task.Delay(time * 1000);
        ProcessTrigger(trigger);
    }

    private void SwitchToState(State newState)
    {
        if (newState == currentState) return;

        // activate/deactivate cannons based on state
        primaryCannon?.gameObject.SetActive(newState == State.PRIMARY_ATTACK);
        secondaryCannon?.gameObject.SetActive(newState == State.SECONDARY_ATTACK);
        powerCannon?.gameObject.SetActive(newState == State.POWER_ATTACK);

        // if to long range -> start overheat timer
        if (newState == State.PRIMARY_ATTACK) _ = TriggerTimer(Trigger.PRIMARY_CANNON_OVERHEATED, timeToOverheat);

        // if from long range -> start primary cooldown timer
        if (currentState == State.PRIMARY_ATTACK) _ = TriggerTimer(Trigger.PRIMARY_CANNON_READY, primaryCooldown);

        // if from power attack -> start power cooldown timer
        if (currentState == State.POWER_ATTACK) _ = TriggerTimer(Trigger.POWER_ATTACK_READY, powerCooldown);

        if (newState == State.POWER_ATTACK) StartCoroutine(PowerAttackCorutine());

        currentState = newState;

        Debug.Log(currentState);
    }

    private void ProcessTrigger(Trigger trigger)
    {

        switch (trigger, currentState)
        {
            case (Trigger.POWER_ATTACK_READY, not State.POWER_ATTACK or State.POWER_ATTACK_SEEK):
                SwitchToState(State.POWER_ATTACK_SEEK); 
                return;
            
            case (Trigger.PRIMARY_CANNON_OVERHEATED, State.PRIMARY_ATTACK):
                SwitchToState(State.SECONDARY_ATTACK);
                return;

            case (Trigger.PRIMARY_CANNON_READY, State.SECONDARY_ATTACK):
                isPrimaryCannonReady = true;
                SwitchToState(State.PRIMARY_ATTACK);
                return;

            case (Trigger.POWER_ATTACK_FINISHED, _ ) when isPrimaryCannonReady:
                SwitchToState(State.PRIMARY_ATTACK);
                return;

            case (Trigger.POWER_ATTACK_FINISHED, _ ) when !isPrimaryCannonReady:
                SwitchToState(State.SECONDARY_ATTACK);
                return;
        }
    }

    private IEnumerator PowerAttackCorutine()
    {
        yield return new WaitForSeconds(3);
        ProcessTrigger(Trigger.POWER_ATTACK_FINISHED);
    }
}
