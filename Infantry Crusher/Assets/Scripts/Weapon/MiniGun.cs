using Lofelt.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGun : Gun
{

    public static MiniGun instance;
    [SerializeField] private MiniGunVisual visual;

    Vector3 directionToCrosshair;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DynamicCrosshair.instance.SetShootingDelay(ShootDelay);
    }

    public override Vector3 GetulletDirection()
    {
        Vector3 direction = Camera.main.transform.forward;

        if (AddBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-BulletSpreadVariance.x, BulletSpreadVariance.x),
                Random.Range(-BulletSpreadVariance.y, BulletSpreadVariance.y),
                Random.Range(-BulletSpreadVariance.z, BulletSpreadVariance.z)
            );

            direction.Normalize();
        }

        return direction;
    }

    public override void Shoot()
    {

        if (LastShootTime + ShootDelay < Time.time)
        {
            ShootingSystem.Play();
            DynamicCrosshair.instance.SetCrosshairSize();
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.SoftImpact);

            Bullet bullet = bulletController.GetSpareBullet();
            bullet.transform.position = BulletSpawnPoint.position;
            bullet.transform.rotation = BulletSpawnPoint.rotation;
            bullet.TrailRenderer.Clear();
            bullet.gameObject.SetActive(true);
            bullet.BulletInit(BulletDamage, BulletSpeed, bullet.transform.position.normalized, false);

            LastShootTime = Time.time;
        }
    }
    public override void StartVisual()
    {
        visual.StartFireVisual();
    }

    public override void StopVisual()
    {
        visual.StopFireVisual();
    }
}
