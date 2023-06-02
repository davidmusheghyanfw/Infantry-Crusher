using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MiniGunVisual : MonoBehaviour
{
    public static MiniGunVisual instance;

    [SerializeField] private Transform rotatingObject;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float delayTime;
    private float currentSpeed = 0;
    float rotSpeed = 0;

    private void Start()
    {
        instance = this;
    }
    public void StartFireVisual()
    {
        StartFireVisualRoutine();
    }

    public void StopFireVisual()
    {
        StopFireVisualRoutine();
        currentSpeed = rotSpeed;
    }


    public void Recoil()
    {
        transform.DOComplete();

        transform.DOLocalMoveZ(transform.localPosition.z-0.2f,0.1f).SetEase(Ease.InExpo).OnComplete(()=>{

            transform.DOLocalMoveZ(transform.localPosition.z+0.2f,0.5f).SetEase(Ease.OutSine);
        });
    }
    void StartFireVisualRoutine()
    {
        if(FireRoutineC != null) StopCoroutine(FireRoutineC);
        FireRoutineC = StartCoroutine(FireRoutine());
    }

    private void StopFireVisualRoutine()
    {
        if (FireRoutineC != null) StopCoroutine(FireRoutineC);
    }

    Coroutine FireRoutineC;
    private IEnumerator FireRoutine()
    {
        rotSpeed = currentSpeed;
        while (true)
        {
            
            rotSpeed = Mathf.Lerp(currentSpeed, maxSpeed, delayTime * Time.deltaTime);
         
            rotatingObject.Rotate(rotSpeed,0,0,Space.Self);
            yield return new WaitForEndOfFrame();
        }
    }
}
