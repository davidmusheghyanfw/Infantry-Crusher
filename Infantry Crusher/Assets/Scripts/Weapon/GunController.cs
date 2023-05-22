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
    protected BulletController bulletController;
    public BulletController BulletControllerInstance { get { return bulletController; } set { bulletController = value; } }
    [SerializeField]
    protected float BulletSpeed = 100;
    [SerializeField]
    protected float BulletDamage = 50;

    [SerializeField]
    protected float ShootDelay = 0.5f;
  
    protected float LastShootTime;

  
    public virtual void Shoot()
    {
       
    }

    public virtual Vector3 GetulletDirection()
    {
        return Vector3.zero;
    }

    public virtual void StartVisual()
    {

    }
    public virtual void StopVisual()
    {

    }
    
}