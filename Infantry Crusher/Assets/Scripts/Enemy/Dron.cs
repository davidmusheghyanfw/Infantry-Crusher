using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dron : Enemy, IDestroyable
{
    float currentHealth = 0f;

    public override void Die()
    {
        base.Die();
    }

    public override void InitEnemy()
    {
        base.InitEnemy();
    }
    public override void Move()
    {
        base.Move();
    }
    public override void InShootingPlace()
    {
        base.InShootingPlace();
    }
    void IDestroyable.Damaged(float damage)
    {
        throw new System.NotImplementedException();
    }

    GameObject IDestroyable.gameObject()
    {
        throw new System.NotImplementedException();
    }
}
