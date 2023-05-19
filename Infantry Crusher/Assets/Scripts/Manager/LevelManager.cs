using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] EnemyPullDefinition enemyPull;
    [SerializeField] private int maxStageCout = 2;
    private int currentStage = 1;
    private int enemyCount;
    public int EnemyCount { get { return enemyCount; } }

    private void Awake()
    {
        instance = this;
    }

    public void InitLevelManager()
    {
        CalculateEnemyCountInLevel();
        EnemyManager.instance.SetEnemyPull(enemyPull.stagePulls[currentStage]);
        InitStage();
    }

    public void ToNextStage()
    {
        currentStage++;
        InitStage();
    }

    public void InitStage()
    {
        PlayerController.instance.ToNextGun(currentStage);
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
}
