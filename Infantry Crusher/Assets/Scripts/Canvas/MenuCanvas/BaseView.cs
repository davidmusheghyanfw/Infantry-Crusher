using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BaseView : MonoBehaviour
{
    [SerializeField] private BaseManager baseController;
    
    [SerializeField] private GameObject prevBtn;
    [SerializeField] private GameObject nextBtn;

    [SerializeField] private GameObject fillBtn;

    [SerializeField] private GameObject fillEffectPrefab;
    

    public void InitBaseView()
    {
        baseController.InitBaseController();
        CheckLimits();
    }
    public void DeintiBaseView()
    {
        baseController.DeinitBaseController();
    }
    private void OnEnable()
    {
        InitBaseView();
    }

    private void OnDisable()
    {
        DeintiBaseView();
    }
    public void DoNext()
    {
        baseController.ShowNextBase();
        CheckLimits(); 
    }

    public void DoPrev()
    {
        baseController.ShowPrevBase();
        CheckLimits();
    }

    private void CheckLimits()
    {
        nextBtn.SetActive(baseController.CheckMaxLimit());
        prevBtn.SetActive(baseController.CheckMinLimit());
    }

    public void OnPointerUp()
    {

    }

    public void OnPointerDown()
    {
        Vector3 startPoint = fillBtn.transform.position;   
        Vector3 endPoint = baseController.CurrentBase.CurrentBaseElement.transform.position;
        endPoint = CameraController.instance.Main.WorldToScreenPoint(endPoint);

        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        go.transform.position = startPoint;
        
        GameObject g1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        go.transform.position = endPoint;

    }

}
