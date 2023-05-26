using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : AdditionalGun
{
    private Bullet bullet;
    public override void InitAdditionalGun(Transform spawnPos)
    {
        base.InitAdditionalGun(spawnPos);
        SetupBullet();
    }
    public override void Shoot()
    {
        bullet.BulletInit(damage, speed, bulletSpawnPos.position, true);
        bullet.transform.parent = null;
        bullet.FlyingProcess();
    }

    private void SetupBullet()
    {
        if (bullet is not null) return;
            bullet = Instantiate(rpgBullet, bulletSpawnPos.position, Quaternion.identity,bulletSpawnPos);
        bullet.gameObject.SetActive(true);
    }
    public override void Hide()
    {
        base.Hide();
        SetupBullet();
    }

    public override void Show()
    {
        base.Show();
    }
}
