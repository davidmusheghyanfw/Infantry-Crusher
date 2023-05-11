using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public static GameView instance;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider progressBar;
    private void Awake()
    {
        instance = this;
    }
    public void InitGameView()
    {
        SetActive(false);
    }

    public void InitLevelUI()
    {
        InitHealthBar();
        InitProgressBar();
    }

    private void InitHealthBar()
    {
        healthBar.minValue = 0;
        healthBar.value = healthBar.maxValue = PlayerController.instance.Health;
    }
    private void InitProgressBar()
    {
        progressBar.minValue = progressBar.value = 0;
        progressBar.maxValue = 100;
    }

    public void ChangeHealtBarValue()
    {
        healthBar.value = PlayerController.instance.Health;
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
}
