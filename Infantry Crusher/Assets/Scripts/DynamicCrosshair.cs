using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicCrosshair : MonoBehaviour
{
    public static DynamicCrosshair instance;
    [SerializeField] private RectTransform borderTransform;
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;
    [SerializeField] private float toDefaultDelay;
    float currentSize;

    private void Awake()
    {
        instance = this;   
    }
    private void Start()
    {
        
      
    }
    public void SetCrosshairSize()
    {
       
        borderTransform.sizeDelta = new Vector2(maxSize, maxSize);
        currentSize = maxSize;
       
        StartToDefaultSizeRoutine();
    }
    private void StartToDefaultSizeRoutine()
    {
        if (ToDefaultSizeRoutineR != null) StopCoroutine(ToDefaultSizeRoutineR);
        ToDefaultSizeRoutineR = StartCoroutine(ToDefaultSizeRoutine());
    }

    private void StopToDefaultSizeRoutine()
    {
        if (ToDefaultSizeRoutineR != null) StopCoroutine(ToDefaultSizeRoutineR);
    }

    Coroutine ToDefaultSizeRoutineR;
    private IEnumerator ToDefaultSizeRoutine()
    {
        float t = 0;
        float startTime = Time.fixedTime;

        while (t<1)
        {
            t = (Time.fixedTime - startTime) / toDefaultDelay;
            currentSize = Mathf.Lerp(currentSize, minSize, t);
            borderTransform.sizeDelta = new Vector2(currentSize,currentSize);
            
            yield return new WaitForEndOfFrame();
        }
    }

    public void SetShootingDelay(float value)
    {
        toDefaultDelay = value;
    }
}
