using UnityEngine;
using System.Threading.Tasks;

/// <summary>
/// Class controling behaviour of final boss.
/// Boss (FSM) has 4 attack modes (States) and 3 different cannons.
/// <list type="bullet">
///   <item> <description>Power attack (used whenever ready)</description></item>
///   <item> <description>Long range attack (used when power cannon in cooldown and long cannon not overheated)</description></item>
///   <item> <description>Short range attack (used when both power and long cannon in cooldown)</description></item>
///   <item> <description>Idle (not attacking)</description></item>
///  </list>
/// </summary>
public class Boss : MonoBehaviour
{
    /// <summary>
    /// Defines type of boss movement and guns activated.
    /// </summary>
    private enum State
    {
        /// <value>idle, <c>powerCannon</c> active</value>
        POWER_ATTACK,
        /// <value>seek with high acceleration, all guns deactivated</value>
        POWER_ATTACK_SEEK,
        /// <value>long range seek, <c>longCannon</c> active</value>
        LONG_RANGE_ATTACK,
        /// <value>seek, <c>shortCannon</c> active</value>
        SHORT_RANGE_ATTACK,
        /// <value>idle, all guns deactivated</value>
        IDLE
    }

    // **************** SETTINGS **************** //

    [Header("Refferences")]
    /// <value><c>Actor</c> be seeked by boss</value>
    [SerializeField] Actor  target;
    /// <value><c>PowerCannon</c> whose <c>ProjectileShower</c> function will be called in power attack</value>
    [SerializeField] PowerCannon powerCannon;
    /// <value><c>Cannon</c> to be used (set active) during long range attack</value>
    [SerializeField] Cannon longCannon;
    /// <value><c>Cannon</c> to be used (set active) during short range attack</value>
    [SerializeField] Cannon shortCannon;

    [Header("Power attack settings")]
    /// <value><c>Boss</c> max speed when power attack is ready</value>
    [SerializeField] float  maxPowerSpeed = 7;
    /// <value><c>Boss</c> max acceleration when power attack is ready</value>
    [SerializeField] float  maxPowerAccel = 50;
    /// <value>minimal distance to <c>target</c> for power attack to be activated</value>
    [SerializeField] float  minPowerRange = 5;
    /// <value>power attack cooldown after use</value>
    [SerializeField] int    powerCooldown = 15;

    [Header("Long range attack settings")]
    /// <value><c>Boss</c> max speed when in long range attack</value>
    [SerializeField] float  maxLongSpeed = 5;
    /// <value><c>Boss</c >max acceleration when in long range attack</value>
    [SerializeField] float  maxLongAccel = 20;
    /// <value><c>longCannon</c> cooldown after being overheated</value>
    [SerializeField] int    longCannonCooldown = 10;
    /// <value><c>longCannon</c> overheat threshold</value>
    [SerializeField] float  maxTemperature = 100;
    /// <value>increase of <c>longCannon</c> temperature per second when used</value>
    [SerializeField] int    heatingFactor = 6;
    /// <value>decrease of <c>longCannon</c> temperature per second when unused</value>
    [SerializeField] int    coolingFactor = 4;

    [Header("Short range attack settings")]
    /// <value><c>Boss</c> max speed when in short range attack</value>
    [SerializeField] float  maxShortSpeed = 5;
    /// <value><c>Boss</c > max acceleration when in long short attack</value>
    [SerializeField] float  maxShortAccel = 10;


    // **************** PROPERTIES **************** //

    /// <value><c>IsPowerAttackReady</c> property backfield</value>
    private bool isPowerAttackready = false;
    /// <value><c>IsLongCannonReady</c> property backfield</value>
    private bool isLongCannonReady  = true;
    /// <value><c>IsTargetInPowerRange</c> property backfield</value>
    private bool isTargetInPowerRange = false;


    /// <summary>
    /// One of the properties defining boss current <c>State</c>
    /// </summary>
    private bool IsPowerAttackReady
    {
        get => isPowerAttackready;
        set { isPowerAttackready = value; OnPropertyChanged(); }
    }

    /// <summary>
    /// One of the properties defining boss current <c>State</c>
    /// </summary>
    private bool IsLongCannonReady
    {
        get => isLongCannonReady;
        set { isLongCannonReady = value; OnPropertyChanged(); }
    }

    /// <summary>
    /// One of the properties defining boss current <c>State</c>
    /// </summary>
    private bool IsTargetInPowerRange
    {
        get => isTargetInPowerRange;
        set { isTargetInPowerRange = value; OnPropertyChanged(); }
    }


    // **************** VARIABLES **************** //

    /// <value>current boss <c>State</c></value>
    private State _state = State.IDLE;
    /// <value>maximal boss acceleration in <c>_state</c></value>
    private float _maxAccel = 0;
    /// <value>maximal boss speed in <c>_state</c></value>
    private float _maxSpeed = 0;
    /// <value>current temperature of <c>longCannon</c></value>
    private float _temperature = 0;
    /// <value>boss valocity</value>
    public Vector3 _velocity { get; private set; } = Vector3.zero;
    /// <value>time in seconds until power attack is ready</value>
    private float _powerCooldown;

    /// <value><c>Bar</c> showing cooldown until power attack ready</value>
    private Bar powerCooldownBar;
    /// <value><c>Bar</c> showing temperature of <c>longCannon</c></value>
    private Bar temperatureBar;

    // **************** UNITY **************** //

    private void Start()
    {
        OnPropertyChanged();
        _powerCooldown = powerCooldown;
        powerCooldownBar = HUDManager.Instance.BossPowerCooldownBar;
        temperatureBar = HUDManager.Instance.BossCannonTemperatureBar;
    }

    private void Update()
    {
        UpdatePosition();
        UpdateTemperature();
        UpdatePowerCooldown();
    }


    // **************** PRIVATE **************** //

    /// <summary>
    /// Updates position of this game object based on current <c>_state</c>.
    /// </summary>
    private void UpdatePosition() 
    {
        var dt    = Time.deltaTime;
        var pos   = gameObject.transform.position;
        var utils = GameUtils.Instance;
        var props = EnvironmentProps.Instance;

        // get position to seek
        var seekPos = target.gameObject.transform.position;
        if (_state is State.LONG_RANGE_ATTACK)
        {
            seekPos.z = EnvironmentProps.Instance.maxZ();
        }

        // update velocity
        _velocity = utils.ComputeSeekVelocity(pos, _velocity, _maxSpeed, _maxAccel, seekPos, dt);

        // compute new position
        var new_pos = utils.ComputeEulerStep(transform.position, _velocity, dt);

        // clip into play area
        transform.position = props.IntoArea(new_pos, 0, 0);
    }

    /// <summary>
    /// Updates <c>_temperature</c> of long cannon based on <c>_state</c> and checks if not overheated.
    /// Controlls <c>powerCannonCooldownBar</c>.
    /// </summary>
    private void UpdateTemperature()
    {
        if (!IsLongCannonReady) return;

        float deltaTemp;
        float deltaTime = Time.deltaTime;

        // calculate temperature delta
        if (_state == State.LONG_RANGE_ATTACK)
        {
            deltaTemp = heatingFactor * deltaTime;
        }
        else
        {
            deltaTemp = -coolingFactor * deltaTime;
        }

        // update temperature
        _temperature += deltaTemp;

        // update temperature bar
        temperatureBar?.SetTo(_temperature / maxTemperature);

        // check for overheat
        if (_temperature >= maxTemperature)
        {
            IsLongCannonReady = false;
            _ = CannonCooldownAsync();
        } 
    }

    /// <summary>
    /// Updates <c>_powerCooldown</c> and checks if power attack is ready.
    /// Controlls <c>powerCannonTemperatureBar</c>.
    /// </summary>
    private void UpdatePowerCooldown()
    {
        if (_state is State.POWER_ATTACK or State.POWER_ATTACK_SEEK) return;

        // update cooldown time
        _powerCooldown -= Time.deltaTime;

        // update power cooldown bar
        powerCooldownBar?.SetTo(1 - (_powerCooldown / powerCooldown));

        // check if cooldown over == power attack ready
        if (_powerCooldown <= 0)
        {
            IsPowerAttackReady = true;
            _ = PowerAttackSeekAsync();
        }
    }

    /// <summary>
    /// Called when property <c>IsLongCannnonReady</c> or <c>IsPowerAttackReady</c> 
    /// is changed. Updates dependent values.
    /// </summary>
    private void OnPropertyChanged()
    {
        ChangeState();
        ChangeSpeedAndAccel();
        ChangeCannons();
    }

    /// <summary>
    /// Updates <c>_state</c> based on values of properties
    /// <c>IsPowerAttackReady</c> and <c>IsLongCannonOverheated</c>.
    /// </summary>
    private void ChangeState()
    {
        if (IsPowerAttackReady)
        {
            if (IsTargetInPowerRange)
            {
                _state = State.POWER_ATTACK;
            }
            else
            {
                _state = State.POWER_ATTACK_SEEK;
            }
            return;
        }
        if (IsLongCannonReady)
        {
            _state = State.LONG_RANGE_ATTACK;
        }
        else
        {
            _state = State.SHORT_RANGE_ATTACK;
        }
    }

    /// <summary>
    /// Update <c>_maxSpeed</c> and <c>_maxAccel</c> based on current<c>_state</c>.
    /// </summary>
    private void ChangeSpeedAndAccel()
    {
        switch (_state)
        {
            case State.SHORT_RANGE_ATTACK:
                _maxSpeed = maxShortSpeed;
                _maxAccel = maxShortAccel;
                return;

            case State.LONG_RANGE_ATTACK:
                _maxSpeed = maxLongSpeed;
                _maxAccel = maxLongAccel;
                return;

            case State.POWER_ATTACK_SEEK:
                _maxSpeed = maxPowerSpeed;
                _maxAccel = maxPowerAccel;
                return;

            default:
                _maxAccel = 0;
                _maxSpeed = 0;
                return;
        }
    }


    /// <summary>
    /// Activates cannons allowed in current<c>_state</c>, deactivate others.
    /// </summary>
    private void ChangeCannons()
    {
        powerCannon?.gameObject.SetActive(_state == State.POWER_ATTACK);
        longCannon?.gameObject.SetActive(_state == State.LONG_RANGE_ATTACK);
    }

    /// <summary>
    /// Sets <c>IsLongCannonReady</c> to true after <c>longCannonCooldown</c> seconds.
    /// </summary>
    /// <returns></returns>
    private async Task CannonCooldownAsync()
    {
        await Task.Delay(longCannonCooldown * 1000);
        _temperature = 0;
        IsLongCannonReady = true;
    }

    private async Task PowerAttackSeekAsync()
    {
        // seek until boss is close enough to perform power attack
        while (Vector3.Distance(target.transform.position, transform.position) > minPowerRange)
        {
            await Task.Delay(250);
        }
        IsTargetInPowerRange = true;
        _ = PowerAttackAsync();

    }

    private async Task PowerAttackAsync()
    {
        await powerCannon.ProjectileShowerAsync();
        _powerCooldown = powerCooldown;
        IsPowerAttackReady = false;
        IsTargetInPowerRange = false;
    }
}
