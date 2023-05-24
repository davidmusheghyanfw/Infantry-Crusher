using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelEndView : MonoBehaviour
{
    public static LevelEndView instance;
    [SerializeField] GameObject levelWin;
    [SerializeField] TMP_Text lvlWinKillsTxt;
    [SerializeField] GameObject levelLose;
    [SerializeField] TMP_Text lvlLooseKillsTxt;

    private void Awake()
    {
        instance = this;
    }

    public void InitLeveEndView()
    {
        levelWin.SetActive(false);
        levelLose.SetActive(false);
    }

    public void ActiveLevelWin()
    {
        levelWin.SetActive(true);
        lvlWinKillsTxt.text = "KILLS:" + LevelManager.instance.EnemyCount;
    }

    public void ActiveLevelLoose()
    {
        levelLose.SetActive(true);
        lvlLooseKillsTxt.text = "KILLS:" + GameView.instance.GetDeadEnemyCount();
    }

    public void OnLevelWin()
    {
        GameManager.instance.LevelWin();
        levelWin.SetActive(false);
    }
    public void OnLevelLose()
    {
        GameManager.instance.LeveLose();
        levelLose.SetActive(false);
    }
    
}
