using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public EnemyType enemyType;
    public EnemyDifficult enemyDifficult;
    public Enemy enemy;

    public EnemyData(Enemy _enemy, EnemyType _enemyType, EnemyDifficult _enemyDifficult)
    {
        enemy = _enemy;
        enemyType = _enemyType;
        enemyDifficult = _enemyDifficult;
    }
}
