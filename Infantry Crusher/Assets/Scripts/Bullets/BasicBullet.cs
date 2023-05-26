using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : Bullet
{
    public override void BulletInit(float _damage, float _speed, Vector3 _direction, bool _isExplosive)
    {
        base.BulletInit(_damage, _speed, _direction, _isExplosive);
        FlyingProcess();
        StartCoroutine(LifeTImeRoutine());
    }
    public override void FlyingProcess()
    {
        rb.velocity = transform.forward * speed;
        
    }

    private IEnumerator LifeTImeRoutine()
    {
        yield return new WaitForSeconds(3f);
        if(gameObject.activeSelf)gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        DynamicCrosshair.instance.SetHit();
        Impact(collision);
        gameObject.SetActive(false);
    }

    public override void Impact(Collision collision)
    {
        base.Impact(collision);
    }
}
