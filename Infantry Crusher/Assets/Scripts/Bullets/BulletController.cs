using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public static BulletController instance;
    [SerializeField] private List<Bullet> bullets = new List<Bullet>();
    private void Awake()
    {
        instance = this;
    }

    public Bullet GetSpareBullet()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].gameObject.activeSelf)
            {

                return bullets[i];
            }
        }
        return null;
    }
}
