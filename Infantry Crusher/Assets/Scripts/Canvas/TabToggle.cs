using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabToggle : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private GameObject personalCanvas;

   
    public void OnActive()
    {
        if(toggle.isOn)
        {
            personalCanvas.SetActive(true);
        }
        else
        {
            personalCanvas.SetActive(false);
        }
    }
    public void ChangeState(bool value)
    {
        toggle.isOn = value;
        if (toggle.isOn)
        {
            personalCanvas.SetActive(true);
        }
        else
        {
            personalCanvas.SetActive(false);
        }
    }
    public void SetActive()
    {
        toggle.Select();
        personalCanvas.SetActive(true);
    }
}
