using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedalManager : MonoBehaviour
{
    public static MedalManager instance;

    [SerializeField] private List<Medal> medals;

    [SerializeField] private float spawnRate;

    private void Awake()
    {
        instance = this;
    }

}
