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
        Destroy(gameObject);
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
        while(true)
        {
            if (index < rout.Count)
            {
                if (Vector3.Distance(transform.position, rout[index]) < 3f)
                {
                    index++;
                    if (index < rout.Count) transform.LookAt(rout[index]);
                    else transform.LookAt(Player);
                }
            }
            else if (Vector3.Distance(transform.position, Player.position) < 5f)
            {
                animator.SetBool("IsStopping", true);
                StopMoveToPointRoutine();
            }

            yield return new WaitForEndOfFrame();
        }

    }
    public override void AddToRout(Transform value)
    {
        base.AddToRout(value);
    }


}
