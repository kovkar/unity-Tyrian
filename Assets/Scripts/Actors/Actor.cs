using UnityEngine;

/// <summary>
///  Base class for all actors (everything that can deal or take damage). This class is:
///  <list type="bullet">
///   <item>
///    <description>handling collision events between actors</description>
///   </item>
///   <item>
///    <description>tracking <Actor>'s health</description>
///   </item>
///   <item>
///    <description>updating <Actor>'s health bar if provided</description>
///   </item>
///  </list>
/// </summary>
public class Actor : MonoBehaviour
{
    /// <value>Attribute <c>health</c> represents <c>Actor</c>'s current health value.</value>
    private int health;

    [Header("Actor settings")]

    /// <value>Attribute <c>damage</c> represents damage dealt to other <c>Actor</c> in collision.</value>
    [SerializeField] private int damage;

    /// <value>Attribute <c>maxHealth</c> defines <c>Actor</c>'s maximal allowed health value.</value>
    [SerializeField] private int maxHealth;


    [Header("Refferences")]
    /// <value>Attribute healthBar is refference to bar displaying <c>Actor</c>'s health</value>
    [SerializeField] private Bar healthBar = null;


    // **************** PUBLIC METHODS **************** //

    public int getDamage() { return damage; }

    public int getHealth() { return health; }

    public int getMaxHealth() { return maxHealth; }

    // **************** UNITY METHODS **************** //

    private void Awake()
    {
        health = maxHealth;

        // check if actor is set up correctly for collisions
        if (!TryGetComponent<Collider>(out Collider _))
            Debug.LogError($"{gameObject.name} is missing Collider component required by Actor component.");
        if (!TryGetComponent<Rigidbody>(out Rigidbody rb))
            Debug.LogError($"{gameObject.name} is missing Rigidbody component required by Actor component.");
        else
        {
            if (!rb.isKinematic)
                Debug.LogWarning($"Actor {gameObject.name} should be kinematic.");
            if (rb.useGravity)
                Debug.LogWarning($"Actor {gameObject.name} should not use gravity.");
        }
    }

    /// <summary>
    /// Method handling collisions between <c>Actor</c>'s. 
    /// When Actors 'A' and 'B' collide, health of 'A' is decreased by damage of 'B'. 
    /// </summary>
    protected virtual void OnCollisionEnter(Collision collision)
    {
        // check if other game object is Actor
        if (!TryGetComponent<Actor>(out Actor otherActor))
            Debug.LogWarning($"{otherActor.name} is not an Actor, but collision with {gameObject.name} is allowed by matrix.");
        else
        {
            // update health
            TakeDamage(otherActor.damage);
        }
    }


    // **************** PROTECTED METHODS **************** //

    /// <summary>
    /// Base method for lowering <c>health</c>.
    /// Calls </c>SelfDestroy<c> method if health is non-positive.
    /// </summary>
    /// <param name="damage">value to substract from <c>health</c></param>
    protected virtual void TakeDamage(int damage)
    {
        health -= damage;
        healthBar?.DecreaseBy( (float) damage / maxHealth);
        if (health <= 0) SelfDestroy();
    }

    /// <summary>
    /// Base method for destroying <c>Actor</c>.
    /// </summary>
    protected virtual void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
