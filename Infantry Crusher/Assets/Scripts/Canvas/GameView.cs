using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public static GameView instance;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider progressBar;
    [SerializeField] private TMP_Text currentLevelTxt;
    [SerializeField] private TMP_Text nextLevelTxt;
    private void Awake()
    {
        instance = this;
    }
    public void InitGameView()
    {
        SetActive(false);
    }

    public void InitLevelUI()
    {
        InitHealthBar();
        InitProgressBar();
    }

    private void InitHealthBar()
    {
        healthBar.minValue = 0;
        healthBar.value = healthBar.maxValue = PlayerController.instance.Health;
    }
    private void InitProgressBar()
    {
        currentLevelTxt.text = DataManager.instance.GetLevelNumber().ToString();
        nextLevelTxt.text = (DataManager.instance.GetLevelNumber()+1).ToString();
        progressBar.minValue = progressBar.value = 0;
        progressBar.maxValue = LevelManager.instance.EnemyCount;
    }
    public void IncreaseProgressBar()
    {
        progressBar.value++;
        if(progressBar.value == progressBar.maxValue)
        {
            GameManager.instance.IsPlayerInteractble = false;
            LevelEndView.instance.ActiveLevelWin();
        }
        else if(progressBar.value == LevelManager.instance.EnemyCountInStage)
        {
            GameManager.instance.IsPlayerInteractble = false;
            LevelManager.instance.ToNextStage();
        }
    }
    public void ChangeHealtBarValue()
    {
        healthBar.value = PlayerController.instance.Health;
        if(healthBar.value <= healthBar.minValue)
        {
            GameManager.instance.IsPlayerInteractble = false;
            CameraController.instance.SwitchCamera(CameraState.End);

            CameraController.instance.StartTrackedDollAnimRoutine();
                CharacterController.instance.VisualDieAnim();
            this.Timer(2f, () => {
            LevelEndView.instance.ActiveLevelLoose();
            });
        }
    }

    public void FastWin()
    {
        GameManager.instance.IsPlayerInteractble = false;
        LevelEndView.instance.ActiveLevelWin();
    }
    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    public int GetDeadEnemyCount()
    {
        return (int)progressBar.value;
    }
}
