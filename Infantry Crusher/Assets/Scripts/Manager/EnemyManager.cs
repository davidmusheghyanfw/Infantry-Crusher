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
    [SerializeField] Transform playerPos;
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
      
    }

    public void InitEnemySpawnPosInStage(int stage)
    {
        
    }

    public void SetEnemyPull(StagePull _stagePull)
    {
        stagePull = _stagePull;
    }

    public void StartEnemySpawnRoutine()
    {
        if (EnemySpawnRoutineC != null) StopCoroutine(EnemySpawnRoutineC);
        EnemySpawnRoutineC = StartCoroutine(EnemySpawnRoutine());
    }

    private void StopEnemySpawnRoutine()
    {
        if (EnemySpawnRoutineC != null) StopCoroutine(EnemySpawnRoutineC);
    }

    Coroutine EnemySpawnRoutineC;
    private IEnumerator EnemySpawnRoutine()
    {
        int i = 0;
        while(i< stagePull.wavePulls.Count)
        {
            WavePull wavePull = stagePull.wavePulls[i];
            int j = 0;
            while(j< wavePull.inWaves.Count)
            {
                waveSpawnPos = GetSpawnPos().position;
                InWave inWave = wavePull.inWaves[j];
                int k = 0;
                while(k<inWave.Count)
                {
                    InstantiateEnemy(inWave);
                    k++;
                    yield return new WaitForSecondsRealtime(0.2f);
                }
                j++;
            }
            yield return new WaitForSeconds(nextWaveTime);
            i++;
        }
        StopEnemySpawnRoutine();
    }

    private void InstantiateEnemy(InWave inWave)
    {

        for (int i = 0; i < allEnemies.Count; i++)
        {
            if (allEnemies[i].enemyType == inWave.enemyType && allEnemies[i].enemyDifficult == inWave.enemyDifficult)
            {
                Vector3 randomPos = new Vector3(UnityEngine.Random.Range(-randomSpawnPos.x, randomSpawnPos.x),
                                                allEnemies[i].enemyPrefab.transform.position.y,
                                                UnityEngine.Random.Range(-randomSpawnPos.z, randomSpawnPos.z));
                Enemy enemy = Instantiate(allEnemies[i].enemyPrefab, waveSpawnPos+randomPos, Quaternion.identity, this.transform);
                enemy.transform.LookAt(centerPos);

                enemy.AddToRout(centerPos);
                enemy.Player = playerPos;
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

   
}
