using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelPrefab : MonoBehaviour
{
    public NavMeshSurface[] surfaces;
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

    public void BakeLevel()
    {
        Debug.Log("s");
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }
}

[System.Serializable]
public class LevelSegment
{
    public Transform playerPos;
    public List<Transform> enemyPosesInSegment;
}
