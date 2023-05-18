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
    [SerializeField] private List<Image> hiteffect;
    Color32 maxOpacity = new Color32(221,0,0,255);
    Color32 minOpacity = new Color32(221,0,0,0);
    Color32 currentOpacity;
    float currentSize;

    private void Awake()
    {
        instance = this;   
    }
    private void Start()
    {
        
      
    }

    public void SetHit()
    {
        for (int i = 0; i < hiteffect.Count; i++)
        {
            hiteffect[i].color = maxOpacity;
        }
        currentOpacity = maxOpacity;
    }
    public void SetCrosshairSize()
    {
       
        borderTransform.sizeDelta = new Vector2(maxSize, maxSize);
        currentSize = maxSize;
       
        StartToDefaultParametersRoutine();
    }
    private void StartToDefaultParametersRoutine()
    {
        if (ToDefaultParametersRoutineR != null) StopCoroutine(ToDefaultParametersRoutineR);
        ToDefaultParametersRoutineR = StartCoroutine(ToDefaultParametersRoutine());
    }

    private void StopToDefaultParametersRoutine()
    {
        if (ToDefaultParametersRoutineR != null) StopCoroutine(ToDefaultParametersRoutineR);
    }

    Coroutine ToDefaultParametersRoutineR;
    private IEnumerator ToDefaultParametersRoutine()
    {
        float t = 0;
        float g = 0;
        float startTime = Time.fixedTime;

        while (t<1 || g<1)
        {
            t = (Time.fixedTime - startTime) / toDefaultDelay;
            currentSize = Mathf.Lerp(currentSize, minSize, t);
            borderTransform.sizeDelta = new Vector2(currentSize,currentSize);

            g = (Time.fixedTime - startTime) / 1;
            currentOpacity = Color32.LerpUnclamped(currentOpacity, minOpacity, g);
            for (int i = 0; i < hiteffect.Count; i++)
            {
                hiteffect[i].color = currentOpacity;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void SetShootingDelay(float value)
    {
        toDefaultDelay = value;
    }
}
