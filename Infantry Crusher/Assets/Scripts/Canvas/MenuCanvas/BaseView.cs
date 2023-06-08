using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class BaseView : MonoBehaviour
{
    [SerializeField] private BaseManager baseController;
    
    [SerializeField] private GameObject prevBtn;
    [SerializeField] private GameObject nextBtn;
    

    public void InitBaseView()
    {
        baseController.InitBaseController();
        CheckLimits();
    }
    private void OnEnable()
    {
        InitBaseView();
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

}
