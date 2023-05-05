using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected List<Transform> route;
    protected float currentHealth;

    private void Start()
    {
        InitEnemy();
    }

    public virtual void InitEnemy()
    {
        currentHealth = maxHealth;
    }

    public virtual void Shoot()
    {

    }

    public virtual void Die()
    {

    }

    public virtual void Move()
    {

    }

}
