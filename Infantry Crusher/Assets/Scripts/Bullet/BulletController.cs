using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private Rigidbody rb;

    Vector3 direction;
    public void InitBullet(float _damage, Vector3 _direction)
    {
        damage = _damage;
        direction = _direction;
        rb.AddForce(transform.forward * 300f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {

        }
    }
}
