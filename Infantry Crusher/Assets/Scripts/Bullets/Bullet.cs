using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected float damage;
    protected bool isExplosive;
    protected Vector3 direction;
    protected float speed;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected TrailRenderer trailRenderer;
    [SerializeField] protected float explosionRadius;
    public TrailRenderer TrailRenderer { get { return trailRenderer; } }

    public virtual void BulletInit(float _damage, float _speed, Vector3 _direction, bool _isExplosive)
    {
        speed = _speed;
        damage = _damage;
        direction = _direction;
        isExplosive = _isExplosive;
        
    }
    public virtual void FlyingProcess()
    {

    }
    public virtual void Impact(Collision collision)
    {
        
        if (isExplosive)
        {
            var surroundingObjects = Physics.OverlapSphere(transform.position, explosionRadius);

            for (int i = 0; i < surroundingObjects.Length; i++)
            {
                if (surroundingObjects[i] is null) continue;
                if (surroundingObjects[i].TryGetComponent<IDestroyable>(out IDestroyable destroyable))
                {
                    destroyable.Damaged(damage);
                }
            }
        }
        else
        {
            if (collision.transform.TryGetComponent(out IDestroyable destroyable))
            {
                destroyable.Damaged(damage);
                
            }
        }
            
    }
}
