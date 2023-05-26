using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalGun : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected float activateLimit;
    public float ActivateLimit { get { return activateLimit; }}

    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    [SerializeField] protected Bullet rpgBullet;
    [SerializeField] protected Transform bulletSpawnPos;

    private bool isPreparedToShoot = false;

    public bool IsPreparedToShoot { get { return isPreparedToShoot; }}

    public virtual void InitAdditionalGun(Transform spawnPos)
    {
        transform.position = spawnPos.position;
       
    }

   

    public virtual void Show()
    {
        isPreparedToShoot = true;
        animator.Play("Show");
    }

    public virtual void Hide()
    {
        isPreparedToShoot = false;
        animator.Play("Hide");
    }

    public virtual void Shoot()
    {

    }
}
