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
        directionToCrosshair = new Vector3(Screen.width / 2, Screen.height / 2, 1000f);
        Vector3 directionToCrosshairWorldPoint = Camera.main.ScreenToWorldPoint(directionToCrosshair);

        if (LastShootTime + ShootDelay < Time.time)
        {
            ShootingSystem.Play();

            //Vector3 angle = directionToCrosshairWorldPoint - BulletSpawnPoint.position;
            //BulletSpawnPoint.rotation = Quaternion.LookRotation(angle);
            Bullet bullet = bulletController.GetSpareBullet();
            bullet.transform.position = BulletSpawnPoint.position;
            bullet.transform.rotation = BulletSpawnPoint.rotation;
            bullet.TrailRenderer.Clear();
            bullet.gameObject.SetActive(true);
            bullet.BulletInit(BulletDamage, bullet.transform.position.normalized,false);

            LastShootTime = Time.time;
            //Vector3 direction = GetulletDirection();

            //bool hit = Physics.Raycast(Camera.main.transform.position, direction, out RaycastHit target, float.MaxValue);

            //directionToCrosshair = target.point - BulletSpawnPoint.position;
            //BulletSpawnPoint.rotation = Quaternion.LookRotation(directionToCrosshair);

            //TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);
            //if (hit)
            //{
            //    if (target.transform.TryGetComponent(out IDestroyable destroyable))
            //    {
            //        StartCoroutine(SpawnTrail(trail, target.point, target.normal, false));
            //        destroyable.Damaged(BulletDamage);
            //    }

            //    else
            //        StartCoroutine(SpawnTrail(trail, target.point, target.normal, true));

            //    LastShootTime = Time.time;

            //}
            //else
            //{
            //    StartCoroutine(SpawnTrail(trail, BulletSpawnPoint.position + GetulletDirection() * 100, Vector3.zero, false));

            //    LastShootTime = Time.time;
            //}
        }

    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, Vector3 HitNormal, bool MadeImpact)
    {

        Vector3 startPosition = Trail.transform.position;
        float distance = Vector3.Distance(Trail.transform.position, HitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (remainingDistance / distance));

            remainingDistance -= BulletSpeed * Time.deltaTime;

            yield return null;
        }

        Trail.transform.position = HitPoint;
        if (MadeImpact)
        {
            Instantiate(ImpactParticleSystem, HitPoint, Quaternion.LookRotation(HitNormal));
        }

        Destroy(Trail.gameObject, Trail.time);
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
