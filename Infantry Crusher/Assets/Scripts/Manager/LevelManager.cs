using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] LevelDefinition levelDefinition;
    private int currentStage = 0;
    private int enemyCount;
    private int enemyCountInStage;
    public int EnemyCount { get { return enemyCount; } }
    public int EnemyCountInStage { get { return enemyCountInStage; } }

    private GameObject level;

    private void Awake()
    {
        instance = this;
    }

    public void InitLevelManager()
    {
        level = Instantiate(levelDefinition.level.gameObject, Vector3.zero, Quaternion.identity);
        CalculateEnemyCountInLevel();
        InitPlayerGuns();
        
        InitStage();
    }

    public void ToNextStage()
    {
        currentStage++;
        InitStage();
    }

    public void InitPlayerGuns()
    {
        for (int i = 0; i < levelDefinition.level.GetLevelSegmentCount(); i++)
        {
            PlayerController.instance.AddNewGun(levelDefinition.level.levelSegments[i].playerPos);
        }
    }
    public void InitStage()
    {
        CalculateEnemyInCurrentStage();
        EnemyManager.instance.SetEnemyPull(levelDefinition.enemyPull.stagePulls[currentStage]);
        PlayerController.instance.ToNextGun(currentStage);
        EnemyManager.instance.SetEnemySpawnPosInCurrentStage(levelDefinition.level.levelSegments[currentStage].enemyPosesInSegment);
    }


    private void CalculateEnemyCountInLevel()
    {
        enemyCount = 0;
        for (int k = 0; k < levelDefinition.enemyPull.stagePulls.Count; k++)
        {
            for (int i = 0; i < levelDefinition.enemyPull.stagePulls[k].wavePulls.Count; i++)
            {
                for (int j = 0; j < levelDefinition.enemyPull.stagePulls[k].wavePulls[i].inWaves.Count; j++)
                {
                    enemyCount += levelDefinition.enemyPull.stagePulls[k].wavePulls[i].inWaves[j].Count;
                }
            }
        }
    
    }

    private void CalculateEnemyInCurrentStage()
    {
        enemyCountInStage = 0;
        StagePull stage = levelDefinition.enemyPull.stagePulls[currentStage];

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
}
