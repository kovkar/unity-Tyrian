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
    /// <value>Attribute <c>_health</c> represents <c>Actor</c>'s current health value.</value>
    private int health { get; set; }
    /// <value>Attribute <c>_damage</c> represents damage dealt to other <c>Actor</c>s durring collision.</value>
    private int damage { get; set; }

    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private HealthBar healthBar;

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Actor otherActor = collision.gameObject.GetComponent<Actor>();
        if (otherActor == null) {
            Debug.LogWarning(collision.gameObject.name + " is not an Actor, but collision is allowed by matrix.");
            return;
        }
        this.TakeDamage(otherActor.damage);
        if (healthBar!= null & maxHealth != 0) { 
            healthBar.DecreaseBy(otherActor.damage / maxHealth); 
        }
    }

    public virtual void Heal(int healAmount) 
    {
        health += healAmount;
    }

    public virtual void TakeDamage(int damage) 
    {
        health -= damage;
        if (health < 0) { SelfDestroy(); }
    }

    public virtual void SelfDestroy() 
    {
        Destroy(this.gameObject);
    }
}
