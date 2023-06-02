using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Data")]
    public EnemyDifficult enemyDifficult;
    public EnemyType enemyType;
    
    [Header("Components")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected NavMeshAgent navMesh;
    [Header("Health")]
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currentHealth;
    [SerializeField] protected Slider healthBar;
    protected Transform character;
    public Transform Character { get { return character; } set { character = value; } }

    [Header("Rout")]
    [SerializeField] protected Vector3 randomBorder;
    [SerializeField] protected List<Vector3> rout;
    [Header("Shooting")]
    [SerializeField] protected float shootingTime;
    [SerializeField] protected float damage;
    [SerializeField] protected Bullet bullet;
    [SerializeField] protected Transform shootPos;
    [SerializeField] protected Canvas canvas;
    [SerializeField] protected ParticleSystem shootingParticle;
   
    public virtual void InitEnemy()
    {
        currentHealth = maxHealth;
    }

    public virtual IEnumerator ShootingRoutine()
    {
        yield return null;
    }
     public virtual void InShootingPlace()
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
        rout.Add(GetRandomPlaceNearPoint(value.position));
    }

    public virtual Vector3 GetRandomPlaceNearPoint(Vector3 pos)
    {
        return pos;
    }
}
public enum EnemyDifficult { Easy,Medium,Hard };
public enum EnemyType {Flying,Walking}