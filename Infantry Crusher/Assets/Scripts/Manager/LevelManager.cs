using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] EnemyPullDefinition enemyPull;
    [SerializeField] LevelPrefab levelPrefab;
    private int currentStage = 0;
    private int enemyCount;
    private int enemyCountInStage;
    public int EnemyCount { get { return enemyCount; } }
    public int EnemyCountInStage { get { return enemyCountInStage; } }

    private void Awake()
    {
        instance = this;
    }

    public void InitLevelManager()
    {
        CalculateEnemyCountInLevel();
        InitPlayerGuns();
        EnemyManager.instance.SetEnemyPull(enemyPull.stagePulls[currentStage]);
        InitStage();
    }

    public void ToNextStage()
    {
        currentStage++;
        InitStage();
    }

    public void InitPlayerGuns()
    {
        for (int i = 0; i < levelPrefab.GetLevelSegmentCount(); i++)
        {
            PlayerController.instance.AddNewGun(levelPrefab.levelSegments[i].playerPos);
        }
    }
    public void InitStage()
    {
        CalculateEnemyInCurrentStage();
        PlayerController.instance.ToNextGun(currentStage);
        EnemyManager.instance.SetEnemySpawnPosInCurrentStage(levelPrefab.levelSegments[currentStage].enemyPosesInSegment);
    }


    private void CalculateEnemyCountInLevel()
    {
        enemyCount = 0;
        for (int k = 0; k < enemyPull.stagePulls.Count; k++)
        {
            for (int i = 0; i < enemyPull.stagePulls[k].wavePulls.Count; i++)
            {
                for (int j = 0; j < enemyPull.stagePulls[k].wavePulls[i].inWaves.Count; j++)
                {
                    enemyCount += enemyPull.stagePulls[k].wavePulls[i].inWaves[j].Count;
                }
            }
        }
    }

    private void CalculateEnemyInCurrentStage()
    {
        enemyCountInStage = 0;
        StagePull stage = enemyPull.stagePulls[currentStage];

        for (int i = 0; i < stage.wavePulls.Count; i++)
        {
            for (int j = 0; j < stage.wavePulls[i].inWaves.Count; j++)
            {
                enemyCountInStage += stage.wavePulls[i].inWaves[j].Count;
            }
        }
      
    }
}
