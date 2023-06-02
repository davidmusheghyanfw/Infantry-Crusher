using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Medal : MonoBehaviour, IDestroyable
{
    [SerializeField] private MedalType medalType;
    public MedalType GetMedalType{get{ return medalType;} }

    [SerializeField] private float bonusPercent;
    [SerializeField] private float health;


    private void Start()
    {
        
        transform.DOMoveY(transform.position.y+0.2f,2f).SetLoops(-1, LoopType.Yoyo);
        transform.DOLocalRotate(Vector3.up * 360f, 6f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
        
    }

    public void Damaged(Bullet bullet)
    {
        if(health<=0) return;
        if(bullet.type == BulletType.explosive) return;
        transform.DOShakeScale(0.1f,randomnessMode:ShakeRandomnessMode.Harmonic);
        health -= bullet.Damage;
        if(health<=0) StartImpact();
        
    }

    private void StartImpact()
    {

        PlayerController.instance.IncreaseShootDelay(bonusPercent);
        Destroy(gameObject);
    }

    GameObject IDestroyable.gameObject()
    {
        return this.gameObject;
    }
}
