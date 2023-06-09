using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggers : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Zona"))
        {
            enemy.InShootingPlace();
        }
    }
}
