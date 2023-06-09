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
        CameraController.instance.CameraInMenu();
        MenuView.instance.SetActive(true);

    }

    public void PrepareLevel()
    {
        CameraController.instance.CameraInGame();
        LevelManager.instance.InitLevelManager();
        MedalManager.instance.InitMedalManager();
        EnemyManager.instance.InitEnemyManager();
        CharacterController.instance.InitCharacterController();
        GameView.instance.InitLevelUI();
        GameView.instance.SetActive(false);
        
    }
    public void StageStart()
    {
        PlayerController.instance.InitPlayer();
        DynamicCrosshair.instance.SetActive(true);
        this.Timer(1f, () =>
        {
            EnemyManager.instance.StartEnemySpawnRoutine();
            GameView.instance.SetActive(true);
           
            isPlayerInteractble = true;
        });
        
    }

    public void GameStart()
    {
      
    }

    public void ClearLevel()
    {

        PlayerController.instance.ClearAllGuns();
        LevelManager.instance.ClearLevel();
        Destroy(CharacterController.instance.gameObject);
        EnemyManager.instance.StopEnemySpawnRoutine();
        EnemyManager.instance.ClearAllEnemies();
    }

    public void LevelWin()
    {
        ClearLevel();
        LevelManager.instance.IncreaseLevelNumber();
        InGame();
    }

    public void LeveLose()
    {
        ClearLevel();
        InGame();

    }

    public void FastWin()
    {
        EnemyManager.instance.StopEnemySpawnRoutine();
        EnemyManager.instance.ClearAllEnemies();
        GameView.instance.FastWin();
    }

    public void Restart()
    {
        ClearLevel();
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
            FastWin();
        }
    }
#endif
}
