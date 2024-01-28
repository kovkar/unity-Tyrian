using UnityEngine;

/// <summary>
/// Class controlling movement of projectiles.
/// </summary>
public class ProjectileController : MonoBehaviour
{
    private float _speed = 1.0f;
    private Vector3 _direction = new Vector3(0, 0, 1);


    // **************** PUBLIC **************** //

    /// <summary>
    /// Sets projectile parameters.
    /// </summary>
    /// <param name="speed">projectile speed</param>
    /// <param name="direction">projectile moving direction</param>
    public void Set(float speed, Vector3 direction)
    {   
        // set speed and direction
        _speed = speed;
        _direction = direction;

        // rotate projectile to traveling direction
        transform.rotation = Quaternion.LookRotation(direction);
    }


    // **************** UNITY **************** //

    private void Update()
    {
        transform.position = GameUtils.Instance.ComputeEulerStep(transform.position, _direction * _speed, Time.deltaTime);
    }
}
