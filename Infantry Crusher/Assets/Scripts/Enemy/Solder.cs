using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solder : Enemy, IDestroyable
{
    public override void InitEnemy()
    {
        base.InitEnemy();
        Move();
    }
    public override void Die()
    {
        animator.SetBool("IsDied", true);
        StopMoveToPointRoutine();
        StopShootingRoutine();
        Destroy(gameObject, 5);
    }

    public void Damaged(float damage)
    {
       currentHealth -= damage;
       if(currentHealth <= 0) Die();
    }

    public override void Move()
    {
        if (rout.Count < 0) return;
        StartMoveToPointRoutine();
        
    }
    private void StartMoveToPointRoutine()
    {
        if (MoveToPointRoutineC != null) StopCoroutine(MoveToPointRoutineC);
        MoveToPointRoutineC = StartCoroutine(MoveToPointRoutine());
    }

    private void StopMoveToPointRoutine()
    {
        if (MoveToPointRoutineC != null) StopCoroutine(MoveToPointRoutineC);
    }

    Coroutine MoveToPointRoutineC;
    private IEnumerator MoveToPointRoutine()
    {
        int index = 0;
        transform.LookAt(rout[index]);
        while (true)
        {
            if (index < rout.Count)
            {
                if (Vector3.Distance(transform.position, rout[index]) < 4f)
                {
                    index++;
                    if (index < rout.Count) transform.LookAt(rout[index]);
                    else transform.LookAt(Player);
                }
            }
            //else if (Vector3.Distance(transform.position, Player.position) < 5f)
            //{
            //    animator.SetBool("IsStopping", true);
            //    StopMoveToPointRoutine();
            //}

            yield return new WaitForEndOfFrame();
        }

    }

    public override void InShootingPlace()
    {
        animator.SetBool("IsStopping", true);
        StartShootingRoutine();
    }

    private void StartShootingRoutine()
    {
        if (ShootingRoutineC != null) StopCoroutine(ShootingRoutineC);
        ShootingRoutineC = StartCoroutine(ShootingRoutine());
    }

    private void StopShootingRoutine()
    {
        if (ShootingRoutineC != null) StopCoroutine(ShootingRoutineC);
    }

    Coroutine ShootingRoutineC;

    public override IEnumerator ShootingRoutine()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {

            animator.Play("Firing Rifle");
            Bullet obj = Instantiate(bullet, shootPos.position, Quaternion.identity);
            obj.transform.LookAt(Camera.main.transform);
            obj.BulletInit(damage, Camera.main.transform.position, false);
            yield return new WaitForSeconds(shootingTime);
        }
    }
    public override void AddToRout(Transform value)
    {
       rout.Add(GetRandomPlaceNearPoint(value.position));
    }
    public override Vector3 GetRandomPlaceNearPoint(Vector3 pos)
    {
        pos += new Vector3(
                Random.Range(-randomBorder.x, randomBorder.x),
                0,
                Random.Range(-randomBorder.z, randomBorder.z)
            );

        return pos;
    }

}
