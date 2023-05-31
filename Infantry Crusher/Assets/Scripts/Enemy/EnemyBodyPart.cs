using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyPart : MonoBehaviour, IDestroyable
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Soldier soldier;
    [SerializeField] private EnemyBody enemyBody;
    public void Damaged(Bullet bullet)
    {
        soldier.TakeDamage(bullet, this);
    }

    public Rigidbody GetRigidbody()
    {
        return rb;
    }
    GameObject IDestroyable.gameObject()
    {
        return this.gameObject;
    }

    public EnemyBody GetBodyPart()
    {
        return enemyBody;
    }
}
