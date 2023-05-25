using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelPrefab : MonoBehaviour
{
    public List<LevelSegment> levelSegments = new List<LevelSegment>();
    public Transform characterPos;
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
    public Transform gunSpawnPos;
   
    public Transform dronPos;
    public List<Transform> enemyPosesInSegment;
}
