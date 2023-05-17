using Lofelt.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Enemy
{
    
    private Vector3 worldDeltaPosition = Vector3.zero;
    public override void InitEnemy()
    {
        base.InitEnemy();
        canvas.enabled = false;
        healthBar.minValue = 0;
        healthBar.value = healthBar.maxValue = maxHealth;

        Move();
    }
    public override void Die()
    {
        canvas.enabled = false;
        navMesh.enabled = false;
       
        StopMoveToPointRoutine();
        StopShootingRoutine();
      
        animator.Play("Die");
        PointerManager.Instance.RemoveFromList(this);
        GameView.instance.IncreaseProgressBar();
        Destroy(gameObject, 5);
    }

    public void TakeDamage(float damage, EnemyBodyPart bodyPart)
    {
        if (currentHealth <= 0) return;

        if (bodyPart.GetBodyPart() == EnemyBody.Head)
        {
            currentHealth = 0;
        }
        else currentHealth -= damage;
        
        
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.HeavyImpact);
        
        healthBar.value = currentHealth;
        
        if(!canvas.isActiveAndEnabled) canvas.enabled = true;
        if (currentHealth <= 0) Die();
    }

    public override void Move()
    {
        //if (rout.Count < 0) return;
        navMesh.updatePosition = false;
        StartMoveToPointRoutine();
        navMesh.SetDestination(player.position);
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
            worldDeltaPosition = navMesh.nextPosition - transform.position;

            if (worldDeltaPosition.magnitude > navMesh.radius)
            {
                navMesh.nextPosition = transform.position + 0.9f * worldDeltaPosition;
                navMesh.SetDestination(player.position);
            }
            //if (index < rout.Count)
            //{
            //    if (Vector3.Distance(transform.position, rout[index]) < 4f)
            //    {
            //        index++;
            //        if (index < rout.Count) transform.LookAt(rout[index]);
            //        else transform.LookAt(Player);
            //    }
            //}
            //else if (Vector3.Distance(transform.position, Player.position) < 5f)
            //{
            //    animator.SetBool("IsStopping", true);
            //    StopMoveToPointRoutine();
            //}
            canvas.transform.LookAt(canvas.transform.position + Camera.main.transform.rotation * -Vector3.back,
            Camera.main.transform.rotation * -Vector3.down);
            yield return new WaitForEndOfFrame();
        }

    }

    public override void InShootingPlace()
    {
        navMesh.enabled = false;
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
                transform.position.y,
                Random.Range(-randomBorder.z, randomBorder.z)
            );

        return pos;
    }

}
public enum EnemyBody { Head, Other};