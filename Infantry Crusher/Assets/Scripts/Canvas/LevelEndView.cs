using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelEndView : MonoBehaviour
{
    public static LevelEndView instance;
    [SerializeField] GameObject levelWin;
    [SerializeField] TMP_Text lvlWinKillsTxt;
    [SerializeField] GameObject levelLoose;

    private void Awake()
    {
        instance = this;
    }

    public void InitLeveEndView()
    {
        levelWin.SetActive(false);
        levelLoose.SetActive(false);
    }

    public void ActiveLevelWin()
    {
        levelWin.SetActive(true);
        lvlWinKillsTxt.text = "KILLS:" + LevelManager.instance.EnemyCount;
    }
    public void ActiveLevelLoose()
    {
        levelLoose.SetActive(true);
    }

    public void OnLevelWin()
    {
        GameManager.instance.LevelWin();
        levelWin.SetActive(false);
    }
    public void OnLevelLoose()
    {
        GameManager.instance.LeveLoose();
        levelLoose.SetActive(false);
    }
    
}
