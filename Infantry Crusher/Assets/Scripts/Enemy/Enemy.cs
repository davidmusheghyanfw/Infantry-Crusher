using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected float maxHealth;
    protected Transform player;
    public Transform Player { get { return player; } set { player = value; } }
    [SerializeField] protected List<Vector3> rout;
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
    public virtual void AddToRout(Transform value)
    {
        rout.Add(value.position);
    }
}
public enum EnemyDifficult { Easy,Medium,Hard };
public enum EnemyType {Flying,Walking}