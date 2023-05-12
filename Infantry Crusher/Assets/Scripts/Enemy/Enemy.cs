using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currentHealth;
    [SerializeField] protected Slider healthBar;
    protected Transform player;
    public Transform Player { get { return player; } set { player = value; } }
    [SerializeField] protected Vector3 randomBorder;
    [SerializeField] protected List<Vector3> rout;
    [SerializeField] protected float shootingTime;
    [SerializeField] protected float damage;
    [SerializeField] protected Bullet bullet;
    [SerializeField] protected Transform shootPos;
    [SerializeField] protected Canvas canvas;
   
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