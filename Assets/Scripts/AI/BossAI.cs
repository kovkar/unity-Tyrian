using System.Collections;
using UnityEngine;

public class BossAI : MonoBehaviour
{ 
    [Header("Settings")]
    [SerializeField] private int longRangeDuration = 10;
    [SerializeField] private int longRangeCooldown = 10;
    [SerializeField] private int powerAttackCooldown = 20;
    [SerializeField] private float powerAttackRange = 5;

    [Header("Refferences")]
    [SerializeField] private BossController controller;
    [SerializeField] private Transform target;
    [SerializeField] private TriggerCannon triggerCannon;

    private IEnumerator current;
    
    private void Start()
    {
        current = Waiting();
        StartCoroutine(current);
        StartCoroutine(PowerAtack());
    }

    private IEnumerator Waiting()
    {
        controller?.ChangeState(BossController.State.IDLE);
        while (Input.anyKeyDown) yield return new WaitForSeconds(0.1f);
        current = LongRangeAttack();
        StartCoroutine(current);
    }

    private IEnumerator LongRangeAttack()
    {
        // change movement behaviour
        controller?.ChangeState(BossController.State.LONG_RANGE_SEEK);
        // switch weapon
        triggerCannon.enabled = true;
        // time for attack
        yield return new WaitForSeconds(longRangeDuration);
        // change attack
        current = CloseRangeAttack();
        StartCoroutine(current);
    }

    private IEnumerator CloseRangeAttack()
    {
        // change movement behaviour
        controller?.ChangeState(BossController.State.SEEK);
        // switch weapon
        triggerCannon.enabled = false;
        // time for attack
        yield return new WaitForSeconds(longRangeCooldown);
        // change attack
        current = LongRangeAttack();
        StartCoroutine(current);
    }

    private IEnumerator PowerAtack()
    {
        do
        {
            // wait until power attack ready
            yield return new WaitForSeconds(powerAttackCooldown);

            // stop current coroutine
            StopCoroutine(current);

            // seek player until close enough
            triggerCannon.gameObject.SetActive(false) ;
            controller?.ChangeState(BossController.State.POWER_SEEK);
            while (Vector3.Distance(transform.position, target.position) > powerAttackRange) 
                yield return new WaitForSeconds(0.5f);

            // stop moving and perform power atack 
            controller?.ChangeState(BossController.State.IDLE);
            yield return (ProjectileSymphony());

            // start previously stoped coroutine
            StartCoroutine(current); 
        }
        while (true);
    } 
    
    private IEnumerator ProjectileSymphony() 
    {
        yield return new WaitForSeconds(3);
    }
}
