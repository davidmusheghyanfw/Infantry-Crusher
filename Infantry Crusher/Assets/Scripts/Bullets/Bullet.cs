using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected float damage;
    protected bool is≈xplosive;
    protected Vector3 direction;
    [SerializeField] protected Rigidbody rb;

    public virtual void BulletInit(float _damage, Vector3 _direction, bool _isExplosive)
    {
        damage = _damage;
        direction = _direction;
        is≈xplosive = _isExplosive;
    }
    public virtual void FlyingProcess()
    {

    }

}
