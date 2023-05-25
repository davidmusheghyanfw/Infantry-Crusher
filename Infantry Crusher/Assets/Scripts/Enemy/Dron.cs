using Lofelt.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dron : Enemy, IDestroyable
{

    [SerializeField] float ToStartPosTime;
    public override void Die()
    {
        healthBar.gameObject.SetActive(false);
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
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
    }
    public override void InShootingPlace()
    {
        base.InShootingPlace();
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
