using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "LevelDefinition", order = 1)]
public class LevelDefinition : ScriptableObject
{
    public LevelPrefab level;
    public EnemyPullDefinition enemyPull;
}
