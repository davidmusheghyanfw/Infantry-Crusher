using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySolderBullet : Bullet
{
    public override void BulletInit(float _damage, float _speed, Vector3 _direction, bool _isExplosive)
    {
        base.BulletInit(_damage, _speed,_direction, _isExplosive);
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
        if (gameObject.activeSelf) gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerController.instance.Damaged(damage);
        }
        Destroy(gameObject);

    }
    public override float GetDamage()
    {
        return base.GetDamage();
    }
    public override BulletType GetBulletType()
    {
        return base.GetBulletType();
    }
}
