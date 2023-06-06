using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class BaseView : MonoBehaviour
{
    [SerializeField] private BaseController baseController;
    
    [SerializeField] private UnityEngine.UI.Button prevBtn;
    [SerializeField] private UnityEngine.UI.Button nextBtn;
    

    public void InitBaseView()
    {
        baseController.InitBaseController();
        CheckBorders();
    }
    private void OnEnable()
    {
        InitBaseView();
    }
    public void DoNext()
    {
        CheckBorders(); 
        baseController.ShowNextBase();
    }

    public void DoPrev()
    {
        CheckBorders();
        baseController.ShowPrevBase();
    }

    private void CheckBorders()
    {
        nextBtn.enabled = baseController.CheckMaxLimit();
        prevBtn.enabled = baseController.CheckMinLimit();
    }

}
