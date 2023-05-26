using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    
    [SerializeField] List<EnemyData> allEnemies;
    [SerializeField] private Vector3 randomSpawnPos;
    [SerializeField] List<Transform> spawnPos;
    [SerializeField] Transform centerPos;
    Transform character;
    private Transform dropSpawnPos;
    [SerializeField] private int nextWaveTime;
    [SerializeField] List<Enemy> spawnedEnemies;
    private StagePull stagePull;
    
    

    private Vector3 waveSpawnPos;
    private void Awake()
    {
        instance = this;   
    }
    private void Start()
    {
      
    }
    public void InitEnemyManager()
    {
      ClearAllEnemies();
    }

    
    public void SetCharacter(Transform obj)
    {
        character = obj;
    }

    public void SetEnemyPull(StagePull _stagePull)
    {
       
        stagePull = _stagePull;
    }
    public void SetEnemySpawnPosInCurrentStage(List<Transform> enemyPoses,Transform dropPos)
    {
        dropSpawnPos = dropPos;
        spawnPos = enemyPoses;
    }

    public void StartEnemySpawnRoutine()
    {
        if (EnemySpawnRoutineC != null) StopCoroutine(EnemySpawnRoutineC);
        EnemySpawnRoutineC = StartCoroutine(EnemySpawnRoutine());
    }

    public void StopEnemySpawnRoutine()
    {
        if (EnemySpawnRoutineC != null) StopCoroutine(EnemySpawnRoutineC);
    }

    Coroutine EnemySpawnRoutineC;
    private IEnumerator EnemySpawnRoutine()
    {
        for (int i = 0; i < stagePull.wavePulls.Count; i++)
        {
            for (int j = 0; j < stagePull.wavePulls[i].inWaves.Count; j++)
            {
                waveSpawnPos = GetSpawnPos().position;
                for (int k = 0; k < stagePull.wavePulls[i].inWaves[j].Count; k++)
                {
                    InstantiateEnemy(stagePull.wavePulls[i].inWaves[j]);
                    yield return new WaitForSecondsRealtime(0.2f);
                }
            }
            yield return new WaitForSeconds(nextWaveTime);
        }
        //int i = 0;
        //while(i < stagePull.wavePulls.Count)
        //{
        //    int j = 0;
        //    while(j < stagePull.wavePulls[i].inWaves.Count)
        //    {
        //        waveSpawnPos = GetSpawnPos().position;

        //        int k = 0;
        //        while(k< stagePull.wavePulls[i].inWaves[j].Count)
        //        {
        //            InstantiateEnemy(stagePull.wavePulls[i].inWaves[j]);

        //            yield return new WaitForSecondsRealtime(0.2f);
        //            k++;
        //        }
        //        j++;
        //    }
        //    yield return new WaitForSeconds(nextWaveTime);
        //    i++;

        //}
        StopEnemySpawnRoutine();
    }

    private void InstantiateEnemy(InWave inWave)
    {
        for (int i = 0; i < allEnemies.Count; i++)
        {
            if (allEnemies[i].enemyType == inWave.enemyType && allEnemies[i].enemyDifficult == inWave.enemyDifficult)
            {
                Vector3 randomPos = Vector3.zero;
                if (inWave.enemyType is EnemyType.Flying)
                {
                    randomPos = UnityEngine.Random.insideUnitCircle * 2;
                    randomPos.Set(randomPos.x, 0, randomPos.y);
                    randomPos += dropSpawnPos.position;
                }
                else
                {
                    randomPos.Set(UnityEngine.Random.Range(-randomSpawnPos.x, randomSpawnPos.x),
                                                    randomSpawnPos.y,
                                                    UnityEngine.Random.Range(-randomSpawnPos.z, randomSpawnPos.z));
                    randomPos += waveSpawnPos;
                }
                Enemy enemy = Instantiate(allEnemies[i].enemyPrefab, randomPos, Quaternion.identity, this.transform);
                
                enemy.Character = character;
                PointerManager.Instance.AddToList(enemy);
                enemy.InitEnemy();
                spawnedEnemies.Add(enemy);
                return;
            }
        }
        throw new Exception("Enemy not found");
    }

    private Transform GetSpawnPos()
    {
        return spawnPos[UnityEngine.Random.Range(0, spawnPos.Count)];
    }

    public void RemoveFromList(Enemy enemy)
    {
        spawnedEnemies.Remove(enemy);
    }
    public void ClearAllEnemies()
    {
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            PointerManager.Instance.RemoveFromList(spawnedEnemies[i]);
            Destroy(spawnedEnemies[i].gameObject);
        }
        spawnedEnemies.Clear();
    }

    public void EnemyDied(Enemy enemy)
    {
        RemoveFromList(enemy);
        PointerManager.Instance.RemoveFromList(enemy);
        PlayerController.instance.IncreaseAdditionalGunCounter();
        LevelManager.instance.DiedEnemyCount++;
        GameView.instance.IncreaseProgressBar();
        LevelManager.instance.CheckLevelState();
    }
}
