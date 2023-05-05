using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    [SerializeField] EnemyPullDefinition pull;
    [SerializeField] List<EnemyData> allEnemies;
    [SerializeField] List<Transform> spawnPos;
    [SerializeField] Transform centerPos;
    [SerializeField] Transform playerPos;
    [SerializeField] private int nextWaveTime;
    [SerializeField] List<Enemy> spawnedEnemies;

    private Vector3 waveSpawnPos;
    private void Awake()
    {
        instance = this;   
    }
    private void Start()
    {
        InitEnemyManager();
    }
    public void InitEnemyManager()
    {
        StartEnemySpawnRoutine();
    }


    private void StartEnemySpawnRoutine()
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
        while(i< pull.wavePulls.Count)
        {
            WavePull wavePull = pull.wavePulls[i];
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

                GameObject enemy = Instantiate(allEnemies[i].enemy.gameObject, waveSpawnPos, Quaternion.identity, this.transform);
                enemy.transform.LookAt(centerPos);
                Enemy script = enemy.GetComponent<Enemy>();
                script.AddToRout(centerPos);
                script.Player = playerPos;
                spawnedEnemies.Add(script);
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
