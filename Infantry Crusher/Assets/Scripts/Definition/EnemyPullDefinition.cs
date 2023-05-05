using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPull", menuName = "Enemy", order = 0)]
public class EnemyPullDefinition : ScriptableObject
{

    public List<WavePull> wavePulls = new List<WavePull>();
   
}

[System.Serializable]
public struct WavePull
{
    public List<InWave> inWaves;
}

[System.Serializable]
public struct InWave
{
    public int Count;
    public EnemyType enemyType;
    public EnemyDifficult enemyDifficult;
}

