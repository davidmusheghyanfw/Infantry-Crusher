using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroupManager : MonoBehaviour
{
    public static TabGroupManager instance;
    [SerializeField] private TabToggle baseToggle;
    [SerializeField] private TabToggle inventoryToggle;
    [SerializeField] private TabToggle mapToggle;
    [SerializeField] private TabToggle shopToggle;
    [SerializeField] private TabToggle leagueToggle;


    private void Awake()
    {
        instance = this;
    }

    public void InitTabGroupManager()
    {
        mapToggle.SetActive();
    }
}
