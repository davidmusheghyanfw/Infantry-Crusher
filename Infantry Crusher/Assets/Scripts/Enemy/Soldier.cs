using Lofelt.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soldier : Enemy
{
    
    private Vector3 worldDeltaPosition = Vector3.zero;
    [SerializeField] private List<EnemyBodyPart> bodyPartList;
    [SerializeField] private RectTransform headShotImage;
    [SerializeField] private float animTime;
    float imageStartPos;
    private float imageCurrnetPos;
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
        healthBar.gameObject.SetActive(false);
        navMesh.enabled = false;
      
        StopMoveToPointRoutine();
        StopShootingRoutine();
        animator.enabled = false;
        
        EnemyManager.instance.EnemyDied(this);
        Destroy(gameObject, 3);
    }

    private void StartVisualDying(Bullet killingBullet, EnemyBodyPart bodyPart)
    {
        for (int i = 0; i < bodyPartList.Count; i++)
        {
            bodyPartList[i].GetRigidbody().isKinematic = false;
            if (killingBullet.GetBulletType() is BulletType.explosive) bodyPartList[i].GetRigidbody().AddExplosionForce(killingBullet.GetExplosionForce(),
                                                                                    killingBullet.transform.position,
                                                                                    killingBullet.GetExplosionRadius());
        }
        if (killingBullet.GetBulletType() is BulletType.normal) bodyPart.GetRigidbody().AddForce(killingBullet.transform.position.normalized*2);
    }
    private IEnumerator HeadshotImageAnime()
    {
        float t = 0;
        imageStartPos = headShotImage.anchoredPosition.y;
        float startTime = Time.fixedTime;
        headShotImage.gameObject.SetActive(true);
        while (t < 1 )
        {
            t = (Time.fixedTime - startTime) / animTime;
            imageCurrnetPos = Mathf.Lerp(imageStartPos, imageStartPos+100, t);
            headShotImage.anchoredPosition = new Vector2(0, imageCurrnetPos);
            yield return new WaitForEndOfFrame();
        }
    }
    public void TakeDamage(Bullet _bullet, EnemyBodyPart bodyPart)
    {
        if (currentHealth <= 0) return;

        if (!canvas.isActiveAndEnabled)
        {
            canvas.enabled = true;
            headShotImage.gameObject.SetActive(false);
        }


        if (bodyPart.GetBodyPart() == EnemyBody.Head)
        {
            currentHealth = 0;
            StartCoroutine(HeadshotImageAnime());
        }
        else currentHealth -= _bullet.GetDamage();
        
        
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.HeavyImpact);
        
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            if (_bullet.GetBulletType() is not BulletType.explosive) PlayerController.instance.IncreaseAdditionalGunCounter();
             
            StartVisualDying(_bullet, bodyPart);
            Die();
        }
    }

    public override void Move()
    {
        //if (rout.Count < 0) return;
        navMesh.updatePosition = false;
        StartMoveToPointRoutine();
        navMesh.SetDestination(character.transform.position);
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
        //transform.LookAt(rout[index]);
        while (true)
        {
            worldDeltaPosition = navMesh.nextPosition - transform.position;

            if (worldDeltaPosition.magnitude > navMesh.radius)
            {
                navMesh.nextPosition = transform.position + 0.9f * worldDeltaPosition;
                navMesh.SetDestination(character.transform.position);
            }
         
            canvas.transform.LookAt(canvas.transform.position + Camera.main.transform.rotation * -Vector3.back,
            Camera.main.transform.rotation * -Vector3.down);
            yield return new WaitForEndOfFrame();
        }

    }

    public override void InShootingPlace()
    {
        StopMoveToPointRoutine();
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
        transform.LookAt(character);
        yield return new WaitForSeconds(1f);
        while (true)
        {
            if (currentHealth <= 0) break;
            animator.Play("Firing Rifle");
            Bullet obj = Instantiate(bullet, shootPos.position, Quaternion.identity);
            obj.transform.LookAt(CameraController.instance.Main.transform);
            obj.BulletInit(damage,50, CameraController.instance.Main.transform.position, false);
            shootingParticle.Play();
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