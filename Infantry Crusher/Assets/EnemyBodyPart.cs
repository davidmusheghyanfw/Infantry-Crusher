using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyPart : MonoBehaviour, IDestroyable
{
    [SerializeField] private Soldier soldier;
    [SerializeField] private EnemyBody enemyBody;
    public void Damaged(float damage)
    {
        soldier.TakeDamage(damage, this);
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
