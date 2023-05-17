using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lofelt.NiceVibrations;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    

    private bool isPlayerInteractble;
    public bool IsPlayerInteractble { get { return isPlayerInteractble; } set { isPlayerInteractble = value; } }

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
        TabGroupManager.instance.InitTabGroupManager();
        GameView.instance.InitGameView();
        LevelEndView.instance.InitLeveEndView();
        DebugView.instance.InitDebug();
        InLevel();
    }
    public void InLevel()
    {
        isPlayerInteractble = false;
        MenuView.instance.SetActive(true);

    }

    public void LevelStart()
    {
        EnemyManager.instance.InitEnemyManager();
        GameView.instance.SetActive(true);
        GameView.instance.InitLevelUI();
        isPlayerInteractble = true;
    }
    public void GameStart()
    {
      
    }

    public void LevelWin()
    {
        DataManager.instance.IncreaseLevelNumber();
        InLevel();
    }

    public void LeveLoose()
    {
        
    }

    private void Pause()
    {

    }

    private void Resume()
    {

    }

    private void Restart()
    {
        GameInit();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Break();
        }
        else if (Input.GetKeyDown("r"))
        {
            Restart();
        }
        else if (Input.GetKeyDown("w"))
        {
           
        }
    }
#endif
}
