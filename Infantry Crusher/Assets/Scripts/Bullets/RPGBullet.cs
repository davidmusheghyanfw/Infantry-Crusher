using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGBullet : Bullet
{
    [SerializeField] private ParticleSystem impactParticle;
    [SerializeField] private ParticleSystem FlyingParicle;

    public override void BulletInit(float _damage, float _speed, Vector3 _direction, bool _isExplosive)
    {
        base.BulletInit(_damage, _speed, _direction, _isExplosive);
    }

    public override void FlyingProcess()
    {
        rb.velocity = transform.forward * -1 * speed;
        gameObject.GetComponent<Collider>().enabled = true;
        //FlyingParicle.gameObject.SetActive(true);
    }

    public override float GetExplosionForce()
    {
        return base.GetExplosionForce();
    }
    public override float GetExplosionRadius()
    {
        return base.GetExplosionRadius();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Zona"))
        Impact(collision);
    }

    public override void Impact(Collision collision)
    {
      
        base.Impact(collision);
        
        Instantiate(impactParticle, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }

   
}
