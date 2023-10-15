using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
///  <c>Actor</c> class is a base class for all actors (ships and projectiles). This class is:
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

    /// <value>Attribute <c>maxHealth</c> defines <c>Actor</c>'s maximal allowed health value.</value>
    [SerializeField]
    private int maxHealth;

    /// <value>Attribute <c>damage</c> represents damage dealt to other <c>Actor</c>s durring collision.</value>
    [SerializeField]
    private int damage;

    /// <value>Reference to <c>HealtBar</c>. If null <c>HealthBar</c> not attached.</value>
    private HealthBar healthBar;

    void Start()
    {
        this.health = this.maxHealth;
        checkRequiredComponentsAndSettings();
    }

    /// <summary>
    /// Method handling collisions between <c>Actor</c>'s. 
    /// When Actors 'A' and 'B' collide, health of 'A' is decreased by damage of 'B'. 
    /// </summary>
    public virtual void OnCollisionEnter(Collision collision)
    {
        // check if other game object is Actor
        Actor otherActor = collision.gameObject.GetComponent<Actor>();
        if (otherActor == null) { Debug.LogWarning(collision.gameObject.name + " is not an Actor, but collision is allowed by matrix."); }

        // update health
        this.TakeDamage(otherActor.damage);

        // update health bar if attached
        if (healthBar != null & maxHealth != 0) { healthBar.DecreaseBy( (float) otherActor.damage / maxHealth); }
    }

    /// <summary>
    /// Base method for lowering <c>health</c>.
    /// </summary>
    /// <param name="damage">value to substract from <c>health</c></param>
    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) { SelfDestroy(); }
    }

    /// <summary>
    /// Base method for destroying <c>Actor</c>.
    /// </summary>
    public virtual void SelfDestroy()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Method used to pass <c>HealthBar</c>'s reference to <c>Actor</c>. 
    /// (Because Unity doesnt support interface and abstract class variables exposure in inspector)
    /// </summary>
    /// <param name="healthbar"></param>
    public void SetHealthBarRefference(HealthBar healthbar)
    {
        this.healthBar = healthbar;
    }

    public int getDamage() { return this.damage; }
    public int getHealth() { return this.health; }


    /// <summary>
    /// Checking components and their settings needed for correct handling of collisions between Actors.
    /// </summary>
    private void checkRequiredComponentsAndSettings()
    {
        if (!TryGetComponent<Collider>(out Collider cldr)) { Debug.LogError(this.gameObject.name + " is missing Collider component required by Actor component."); }
        if (!TryGetComponent<Rigidbody>(out Rigidbody rb)) { Debug.LogError(this.gameObject.name + " is missing Rigidbody component required by Actor component."); }
        else
        {
            if (!rb.isKinematic) { Debug.LogWarning("Actor " + this.gameObject.name + "should be kinematic."); }
            if (rb.useGravity) { Debug.LogWarning("Actor " + this.gameObject.name + "should not use gravity."); }
        }
    }
}
