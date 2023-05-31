using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : AdditionalGun
{
    private Bullet bullet;
    public override void InitAdditionalGun(Transform spawnPos)
    {
        base.InitAdditionalGun(spawnPos);
    }
    public override void Shoot()
    {
        bullet.BulletInit(damage, speed, bulletSpawnPos.position, true);
        bullet.transform.parent = null;
        bullet.FlyingProcess();
    }

    private void SetupBullet()
    {
        this.Timer(1f, () =>
            {
                bullet = Instantiate(rpgBullet, bulletSpawnPos.position, Quaternion.identity, bulletSpawnPos);
                bullet.GetComponent<Collider>().enabled = true;
                bullet.transform.localRotation = Quaternion.Euler(Vector3.zero);
                bullet.gameObject.SetActive(true);
            });
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
