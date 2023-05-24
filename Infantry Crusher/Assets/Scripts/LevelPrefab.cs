using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelPrefab : MonoBehaviour
{
    public List<LevelSegment> levelSegments = new List<LevelSegment>();
   
    public GameObject GetLevel()
    {
        return gameObject;
    }

    public int GetLevelSegmentCount()
    {
        return levelSegments.Count;
    }

    public LevelSegment GetLevelSegment(int index)
    {
        return levelSegments[index];
    }
}

[System.Serializable]
public class LevelSegment
{
    public Transform playerPos;
    public List<Transform> enemyPosesInSegment;
}
