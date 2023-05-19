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
        InGame();
    }
    public void InGame()
    {
        isPlayerInteractble = false;
        MenuView.instance.SetActive(true);

    }

    public void PrepareLevel()
    {
        LevelManager.instance.InitLevelManager();
        CharacterController.instance.InitCharacterController();
        GameView.instance.InitLevelUI();
        GameView.instance.SetActive(false);
        this.Timer(1f, () => {
            CharacterController.instance.RunToPos();
        });
    }
    public void StageStart()
    {
        PlayerController.instance.InitPlayer();
        this.Timer(1f, () =>
        {
            
            EnemyManager.instance.StartEnemySpawnRoutine();
            GameView.instance.SetActive(true);
           
            isPlayerInteractble = true;
        });
        
    }

    public void ToNextStage()
    {
        CharacterController.instance.RunToPos(); 
        

    }
    public void GameStart()
    {
      
    }

    public void LevelWin()
    {
        DataManager.instance.IncreaseLevelNumber();
        InGame();
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
