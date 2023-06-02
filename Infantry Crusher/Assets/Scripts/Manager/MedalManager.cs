using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedalManager : MonoBehaviour
{
    public static MedalManager instance;

    [SerializeField] private List<Medal> medals;

    [SerializeField] private int maxMedalCount = 4;
    private int currentMedalCount;
    [SerializeField] private float spawnRate;

    MedalType medalType;

    private void Awake()
    {
        instance = this;
    }

    public void InitMedalManager()
    {
        currentMedalCount = 0;
    }

    public void SpawnMedal(Transform spawnPos)
    {
        if(currentMedalCount>maxMedalCount) return;
        float rnd = Random.Range(0,101);

        if(rnd<spawnRate)
        {
            rnd = Random.Range(0,3);
            for(int i = 0; i<medals.Count;i++)
            {
                if(medals[i].GetMedalType == (MedalType)rnd)
                {
                    Instantiate(medals[i],spawnPos.position,spawnPos.rotation,transform);
                }
            }
        }
        currentMedalCount++;
    }

}

public enum MedalType {bronze,silver,gold }
