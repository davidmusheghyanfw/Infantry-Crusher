using Lofelt.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dron : Enemy, IDestroyable
{

    [SerializeField] float ToStartPosTime;
    [SerializeField] float SwitchPosTime;
    public override void Die()
    {
        healthBar.gameObject.SetActive(false);
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        StopShootingRoutine();
        StopSwitchPosRoutnie();
        EnemyManager.instance.EnemyDied(this);
        Destroy(gameObject, 3);
    }

    public override void InitEnemy()
    {
        base.InitEnemy();
        canvas.enabled = false;
        healthBar.minValue = 0;
        healthBar.value = healthBar.maxValue = maxHealth;
        Move();
    }
    public override void Move()
    {
        StartCoroutine(GoToStartPos());
    }

    IEnumerator GoToStartPos()
    {
        float t = 0;
      
        float startTime = Time.fixedTime;
        Vector3 finishPos = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
        while (t < 1)
        {
            t = (Time.fixedTime - startTime) / ToStartPosTime;
            transform.position = Vector3.Lerp(transform.position, finishPos, t);
            yield return new WaitForEndOfFrame();
        }
        InShootingPlace();
    }
    public override void InShootingPlace()
    {
        StartSwitchPosRoutnie();
    }
    private void StartSwitchPosRoutnie()
    {
        if (SwitchPosRoutnieC != null) StopCoroutine(SwitchPosRoutnieC);
        SwitchPosRoutnieC = StartCoroutine(SwitchPosRoutnie());
    }

    private void StopSwitchPosRoutnie()
    {
        if (SwitchPosRoutnieC != null) StopCoroutine(SwitchPosRoutnieC);
    }

    Coroutine SwitchPosRoutnieC;
    public IEnumerator SwitchPosRoutnie()
    {
        int direction = 1;
        while (true)
        {
            yield return new WaitForSeconds(2f);
            float t = 0;

            float startTime = Time.fixedTime;
            Vector3 finishPos = new Vector3(transform.position.x + (10f * direction), transform.position.y, transform.position.z);
            Vector3 startDist = transform.position;
            StartShootingRoutine();
            while (t < 1)
            {
                t = (Time.fixedTime - startTime) / SwitchPosTime;

                transform.position = Vector3.Lerp(startDist, finishPos, t);
                
                yield return new WaitForEndOfFrame();
            }
            StopShootingRoutine();
            direction *= -1;
        }
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

        //shootPos.LookAt(LookToPlayerPos);
        while (true)
        {
            Vector3 LookToPlayerPos = new Vector3(shootPos.position.x, CameraController.instance.Main.transform.position.y,
                CameraController.instance.Main.transform.position.z);
            Bullet obj = Instantiate(bullet, shootPos.position, Quaternion.identity);
            obj.transform.LookAt(LookToPlayerPos);
            obj.BulletInit(damage, 50, character.transform.position, false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void IDestroyable.Damaged(float damage)
    {
        if (currentHealth <= 0) return;

        if (!canvas.isActiveAndEnabled)
        {
            canvas.enabled = true;
        }

        currentHealth -= damage;
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.HeavyImpact);

        healthBar.value = currentHealth;

        if (currentHealth <= 0) Die();
    }

    GameObject IDestroyable.gameObject()
    {
        return gameObject;
    }
}
