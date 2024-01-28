using System;
using System.Threading.Tasks;
using UnityEngine;

public class PowerCannon : Cannon
{
    [Header("Power Cannon Settings")]
    [SerializeField] float projectileSpeed = 5.0f;
    [SerializeField] float showerDelay = 0.5f;
    [SerializeField] float shotDelay = 0.5f;
    [SerializeField] int directionCount = 5;
    [SerializeField] int waveCount = 10;


    public async Task ProjectileShowerAsync()
    {
        await Task.Delay((int) (showerDelay * 1000));
        var waveOffset = (360 / directionCount) / waveCount;
        for (int i = 0; i < waveCount * 4; i++)
        {
            MultiDirectionalShot(5, waveOffset * i);
            await Task.Delay((int) (shotDelay * 1000));
        }
    }

    private void MultiDirectionalShot(int dirCount, float angleOffset = 0)
    {
        var angle = 2 * MathF.PI / dirCount;
        var offset = angleOffset * Mathf.PI / 180.0f;

        for (int i = 0; i < dirCount; i++)
        {
            var shotAngle = angle * i + offset;
            var shotDirection = new Vector3(Mathf.Cos(shotAngle), 0.0f, Mathf.Sin(shotAngle));
            base.Shoot(projectileSpeed, shotDirection);
        }
    }
}
