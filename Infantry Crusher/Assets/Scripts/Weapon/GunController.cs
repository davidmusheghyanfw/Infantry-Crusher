using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    protected bool AddBulletSpread = true;
    [SerializeField]
    protected Vector3 BulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField]
    protected ParticleSystem ShootingSystem;
    [SerializeField]
    protected Transform BulletSpawnPoint;
    [SerializeField]
    protected ParticleSystem ImpactParticleSystem;
    [SerializeField]
    protected TrailRenderer BulletTrail;
    [SerializeField]
    protected float ShootDelay = 0.5f;
    [SerializeField]
    protected LayerMask Mask;
    [SerializeField]
    protected float BulletSpeed = 100;
    [SerializeField]
    protected float BulletDamage = 50;

    protected float LastShootTime;

  
    public virtual void Shoot()
    {
       
    }

    public virtual Vector3 GetDirection()
    {
        return Vector3.zero;
    }

    
}