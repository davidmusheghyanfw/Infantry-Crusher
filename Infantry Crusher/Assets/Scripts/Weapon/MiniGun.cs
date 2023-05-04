using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGun : Gun
{

    public static MiniGun instance;

    private void Awake()
    {
        instance = this;
    }
    public override Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;

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
            Vector3 direction = GetDirection();

            TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);
            bool hit = Physics.Raycast(BulletSpawnPoint.position, direction, out RaycastHit target, float.MaxValue);
            
            if (hit)
            {
                if (target.transform.TryGetComponent(out IDestroyable destroyable))
                {
                    StartCoroutine(SpawnTrail(trail, target.point, target.normal, false));
                    destroyable.Damaged(BulletDamage);
                }
                else
                    StartCoroutine(SpawnTrail(trail, target.point, target.normal, true));

                LastShootTime = Time.time;

            }
            else
            {
                StartCoroutine(SpawnTrail(trail, BulletSpawnPoint.position + GetDirection() * 100, Vector3.zero, false));

                LastShootTime = Time.time;
            }
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
}
