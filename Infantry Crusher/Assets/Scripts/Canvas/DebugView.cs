using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugView : MonoBehaviour
{
    public static DebugView instance;
    [SerializeField] private Slider sensetivitySlider;
    [SerializeField] private TMP_Text sensetivityTxt;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
    }
    public void InitDebug()
    {
        sensetivityTxt.text = PlayerController.instance.RotationControll.ToString();
        sensetivitySlider.value = PlayerController.instance.RotationControll;
        SetActive(false);
    }
    public void OnSLiderChange()
    {
        sensetivityTxt.text = sensetivitySlider.value.ToString();
        PlayerController.instance.RotationControll = sensetivitySlider.value; 
    }

    public void OnClose()
    {
        SetActive(false);
        GameManager.instance.IsPlayerInteractble = true;
    }
    public void OnOpen()
    {
        SetActive(true);
        GameManager.instance.IsPlayerInteractble = false;
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
}
