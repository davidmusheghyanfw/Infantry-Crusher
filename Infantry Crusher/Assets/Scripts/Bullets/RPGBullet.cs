using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGBullet : Bullet
{
    public override void BulletInit(float _damage, float _speed, Vector3 _direction, bool _isExplosive)
    {
        base.BulletInit(_damage, _speed, _direction, _isExplosive);
    }

    public override void FlyingProcess()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Impact(collision);
    }

    public override void Impact(Collision collision)
    {
        base.Impact(collision);
        Destroy(gameObject);
    }
}
