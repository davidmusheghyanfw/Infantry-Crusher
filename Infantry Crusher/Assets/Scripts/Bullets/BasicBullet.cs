using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : Bullet
{
    public override void BulletInit(float _damage, Vector3 _direction, bool _isExplosive)
    {
        base.BulletInit(_damage, _direction, _isExplosive);
        FlyingProcess();
        StartCoroutine(LifeTImeRoutine());
    }
    public override void FlyingProcess()
    {
        rb.velocity = transform.forward * 200f;
    }

    private IEnumerator LifeTImeRoutine()
    {
        yield return new WaitForSeconds(3f);
        if(gameObject.activeSelf)gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out IDestroyable destroyable))
        {
            destroyable.Damaged(damage);
        }
        gameObject.SetActive(false);
    }
}
