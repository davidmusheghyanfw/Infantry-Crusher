using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuView : MonoBehaviour
{
    public static MenuView instance;

    private void Awake()
    {
        instance = this;
    }

    public void InitMenuView()
    {
        SetActive(false);
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    public void OnLevelStart()
    {
        SetActive(false);
        GameManager.instance.LevelStart();
    }
}
