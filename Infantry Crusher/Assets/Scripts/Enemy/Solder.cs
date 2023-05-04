using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solder : Enemy, IDestroyable
{
    public override void Die()
    {
        Destroy(gameObject);
    }

    public void Damaged(float damage)
    {
       currentHealth -= damage;
       if(currentHealth <= 0) Die();
    }
}
