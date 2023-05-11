using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool isLevelStart;
    public bool IsLevelStart { get { return isLevelStart; } }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GameInit();
    }

    private void GameInit()
    {
        MenuView.instance.InitMenuView();
        GameView.instance.InitGameView();
        InLevel();
    }
    public void InLevel()
    {
        isLevelStart = false;
        MenuView.instance.SetActive(true);
      
    }

    public void LevelStart()
    {
        EnemyManager.instance.InitEnemyManager();
        GameView.instance.SetActive(true);
        GameView.instance.InitLevelUI();
        isLevelStart = true;
    }
    public void GameStart()
    {
      
    }



    private void Pause()
    {

    }

    private void Resume()
    {

    }
}
