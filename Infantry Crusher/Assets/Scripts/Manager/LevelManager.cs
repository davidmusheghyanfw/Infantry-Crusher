using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] List<LevelDefinition> levelDefinitions;
    LevelDefinition currentLevelDefinition;
    private int currentStage = 0;
    private int enemyCount;
    private int enemyCountInStage;
    private float diedEnemyCount = 0;

    public float DiedEnemyCount { get { return diedEnemyCount; } set { diedEnemyCount = value; } }
    public int EnemyCount { get { return enemyCount; } }
    public int EnemyCountInStage { get { return enemyCountInStage; } }

    private GameObject level;

    private void Awake()
    {
        instance = this;
    }

    public void InitLevelManager()
    {
        SetCurrentLevel();
        level = Instantiate(currentLevelDefinition.level.gameObject, Vector3.zero, Quaternion.identity);
        diedEnemyCount = 0;
        CalculateEnemyCountInLevel();
        InitPlayerGuns();
        
        InitStage();
    }
    public void SetCurrentLevel()
    {
        int index = DataManager.instance.GetLevelNumber() % levelDefinitions.Count;
        currentLevelDefinition = levelDefinitions[index];
    }

    public void ToNextStage()
    {
        currentStage++;
        InitStage();
    }

    public void InitPlayerGuns()
    {
        for (int i = 0; i < currentLevelDefinition.level.GetLevelSegmentCount(); i++)
        {
            PlayerController.instance.AddNewGun(currentLevelDefinition.level.levelSegments[i].gunSpawnPos);
        }
    }
    public void InitStage()
    {
        CalculateEnemyInCurrentStage();
        EnemyManager.instance.SetEnemyPull(currentLevelDefinition.enemyPull.stagePulls[currentStage]);
        EnemyManager.instance.SetCharacter(currentLevelDefinition.level.characterPos);
        PlayerController.instance.ToNextGun(currentStage);
        EnemyManager.instance.SetEnemySpawnPosInCurrentStage(currentLevelDefinition.level.levelSegments[currentStage].enemyPosesInSegment,
            currentLevelDefinition.level.levelSegments[currentStage].dronPos);
    }


    private void CalculateEnemyCountInLevel()
    {
        enemyCount = 0;
        for (int k = 0; k < currentLevelDefinition.enemyPull.stagePulls.Count; k++)
        {
            for (int i = 0; i < currentLevelDefinition.enemyPull.stagePulls[k].wavePulls.Count; i++)
            {
                for (int j = 0; j < currentLevelDefinition.enemyPull.stagePulls[k].wavePulls[i].inWaves.Count; j++)
                {
                    enemyCount += currentLevelDefinition.enemyPull.stagePulls[k].wavePulls[i].inWaves[j].Count;
                }
            }
        }
    
    }

    private void CalculateEnemyInCurrentStage()
    {
        enemyCountInStage = 0;
        StagePull stage = currentLevelDefinition.enemyPull.stagePulls[currentStage];

        for (int i = 0; i < stage.wavePulls.Count; i++)
        {
            for (int j = 0; j < stage.wavePulls[i].inWaves.Count; j++)
            {
                enemyCountInStage += stage.wavePulls[i].inWaves[j].Count;
            }
        }
      
    }
    public void ClearLevel()
    {
        currentStage = 0;
        Destroy(level);
    }

    public void CheckLevelState()
    {
        if (DiedEnemyCount == EnemyCount)
        {
            GameManager.instance.IsPlayerInteractble = false;
            LevelEndView.instance.ActiveLevelWin();
        }
        else if (DiedEnemyCount == EnemyCountInStage)
        {
            GameManager.instance.IsPlayerInteractble = false;
            ToNextStage();
        }
    }

    public void CheckPlayeHealth()
    {
        if (PlayerController.instance.Health <= 0)
        {
            GameManager.instance.IsPlayerInteractble = false;
            CameraController.instance.SwitchCamera(CameraState.End);

            CameraController.instance.StartTrackedDollAnimRoutine();
            CharacterController.instance.Die();
            this.Timer(2f, () => {
                LevelEndView.instance.ActiveLevelLoose();
            });
        }
    }
}
